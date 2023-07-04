using System.Collections;
using System.Collections.Generic;
using Component;
using Unity.Collections;
using Unity.Entities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public partial struct HpSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {   
        state.RequireForUpdate<Player>();
    }

    public void OnUpdate(ref SystemState state)
    {
        // var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        // var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
        Entity player = SystemAPI.GetSingletonEntity<Player>();
        foreach (var (hp, entity) in SystemAPI.Query<RefRO<Hp>>().WithNone<Destroyed>().WithEntityAccess())
        {
            if (hp.ValueRO.hitpoint <= 0)
            {
                ecb.AddComponent<Destroyed>(entity);
                ecb.AddComponent(player, new AddScore
                {
                    score = 5,
                });
            }
            else
            {
                ecb.SetComponentEnabled<Enemy>(entity, true);
            }
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();

        // var job = new HpJob
        // {
        //     ecb = ecb.AsParallelWriter()
        // };
        //
        // state.Dependency = job.Schedule(state.Dependency);
    }
}

// public partial struct HpJob : IJobEntity
// {
//     public EntityCommandBuffer.ParallelWriter ecb;
//
//     public void Execute([ChunkIndexInQuery] int chunkIndex, Entity entity, in Hp hp) 
//     {
//         if (hp.hitpoint <= 0)
//         {
//             ecb.AddComponent<Destroyed>(chunkIndex, entity);
//         }
//         else
//         {
//             ecb.SetComponentEnabled<Enemy>(chunkIndex, entity, true);
//         }
//     }
// }
