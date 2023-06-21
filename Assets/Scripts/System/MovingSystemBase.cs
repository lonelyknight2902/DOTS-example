using System.Collections;
using System.Collections.Generic;
using Component;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct MovingSystemBase : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (transform, move) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<Move>>().WithNone<Player>())
        {
            transform.ValueRW.Position += move.ValueRO.direction * move.ValueRO.speed * SystemAPI.Time.DeltaTime;
        }
    }
}

