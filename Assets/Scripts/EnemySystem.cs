using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct EnemySystem : ISystem
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
        foreach (var (transform, speed, direction) in SystemAPI.Query<RefRW<LocalTransform>,RefRO<Movement>, RefRW<Enemy>>())
        {
            if (transform.ValueRW.Position.x < -22 && direction.ValueRW.direction.x < 0)
            {
                direction.ValueRW.direction = direction.ValueRO.direction * -1;
            }
            
            if (transform.ValueRW.Position.x > 22 && direction.ValueRW.direction.x > 0)
            {
                direction.ValueRW.direction = direction.ValueRO.direction * -1;
            }

            transform.ValueRW.Position += direction.ValueRO.direction * speed.ValueRO.speed * deltaTime;
        }

        foreach (var (transform, rotation) in SystemAPI.Query<RefRW<LocalTransform>,RefRO<Rotation>>())
        {
            transform.ValueRW = transform.ValueRW.RotateY(rotation.ValueRO.rotationSpeed * deltaTime);
        }
    }
}

public struct Enemy: IComponentData
{
    public float3 direction;
}

public struct Rotation : IComponentData
{
    public float rotationSpeed;
}