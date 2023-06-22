using System.Collections;
using System.Collections.Generic;
using Component;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[BurstCompile]
public partial struct DestroyerSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        foreach (var (enemy, entity) in SystemAPI.Query<Destroyed>().WithEntityAccess())
        {
            ecb.DestroyEntity(entity);
        }
        
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
        // var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        // var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
        //
        // var job = new DestroyerJob
        // {
        //     ecb = ecb.AsParallelWriter()
        // };
        //
        // state.Dependency = job.Schedule(state.Dependency);
    }
}

// [WithAll(typeof(Destroyed))]
// public partial struct DestroyerJob : IJobEntity
// {
//     public EntityCommandBuffer.ParallelWriter ecb;
//     public void Execute([ChunkIndexInQuery] int chunkIndex, Entity entity)
//     { 
//         ecb.DestroyEntity(chunkIndex, entity);
//     }
// }
