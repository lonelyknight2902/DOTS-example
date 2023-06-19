using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct BulletSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        
    }

    public void OnDestroy(ref SystemState state)
    {
        
    }

    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        foreach (var (transform, bullet) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<Bullet>>())
        {
            transform.ValueRW.Position += bullet.ValueRO.velocity * deltaTime;
        }
    }
}
