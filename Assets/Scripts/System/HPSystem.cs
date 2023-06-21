using System.Collections;
using System.Collections.Generic;
using Component;
using Unity.Collections;
using Unity.Entities;
using Unity.VisualScripting;
using UnityEngine;

public partial struct HpSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
        // EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
        // foreach (var (hp, entity) in SystemAPI.Query<RefRO<HP>>().WithAll<Alive>().WithEntityAccess())
        // {
        //     if (hp.ValueRO.hitpoint <= 0)
        //     {
        //         ecb.RemoveComponent<Alive>(entity);
        //     }
        // }

        var job = new HpJob
        {
            ecb = ecb.AsParallelWriter()
        };
        
        state.Dependency = job.Schedule(state.Dependency);
    }
}

public partial struct HpJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter ecb;

    public void Execute([ChunkIndexInQuery] int chunkIndex, Entity entity, in Hp hp) 
    {
        if (hp.hitpoint <= 0)
        {
            ecb.AddComponent<Destroyed>(chunkIndex, entity);
        }
    }
}
