using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct MovingSystemBase : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (transform, speed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<Speed>>())
        {
            transform.ValueRW.Position += new float3(1, 0, 0) * speed.ValueRO.value * SystemAPI.Time.DeltaTime;
        }
    }
}
