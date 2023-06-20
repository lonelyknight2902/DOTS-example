using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public partial struct DestroyerSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        foreach (var (enemy, entity) in SystemAPI.Query<RefRO<Enemy>>().WithNone<Alive>().WithEntityAccess())
        {
            ecb.DestroyEntity(entity);
        }
        
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
