using System.Collections;
using System.Collections.Generic;
using Component;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct PlayerSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Player>();
        state.RequireForUpdate<GameStatus>();
    }

    public void OnDestroy(ref SystemState state)
    {
        
    }

    public void OnUpdate(ref SystemState state)
    {
        bool inPlay = SystemAPI.GetSingleton<GameStatus>().inPlay;
        if (!inPlay)
        {
            return;
        }
        float deltaTime = SystemAPI.Time.DeltaTime;
        foreach (var (transform, speed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<Move>>().WithAll<Player>())
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.ValueRW.Position += new float3(-1, 0, 0) * speed.ValueRO.speed * deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.ValueRW.Position += new float3(1, 0, 0) * speed.ValueRO.speed * deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.ValueRW.Position += new float3(0, 0, -1) * speed.ValueRO.speed * deltaTime;
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.ValueRW.Position += new float3(0, 0, 1) * speed.ValueRO.speed * deltaTime;
            }
        }
    }
}
