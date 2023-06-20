using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public partial struct HpSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
        foreach (var (hp, entity) in SystemAPI.Query<RefRO<HP>>().WithAll<Alive>().WithEntityAccess())
        {
            if (hp.ValueRO.hitpoint <= 0)
            {
                ecb.RemoveComponent<Alive>(entity);
            }
        }
        
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}

public struct HP : IComponentData
{
    public int hitpoint;
}