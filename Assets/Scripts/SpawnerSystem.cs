using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial struct SpawnerSystem : ISystem
{
    private Unity.Mathematics.Random randomPosition;
    private Unity.Mathematics.Random randomEnemy;

    public void OnCreate(ref SystemState state)
    {
        randomPosition = Unity.Mathematics.Random.CreateFromIndex(123456789);
        randomEnemy = Unity.Mathematics.Random.CreateFromIndex(123454321);
    }

    public void OnDestroy(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var spawner in SystemAPI.Query<RefRW<Spawner>>())
        {
            ProcessSpawner(ref state, spawner);
        }
    }

    private void ProcessSpawner(ref SystemState state, RefRW<Spawner> spawner)
    {
        float spawnbound = 20f;
        if (spawner.ValueRO.nextSpawnTime < SystemAPI.Time.ElapsedTime)
        {
            float randomEne = randomEnemy.NextFloat(-1, 1);
            Entity newEntity;
            if (randomEne > 0)
            {
                newEntity = state.EntityManager.Instantiate(spawner.ValueRW.bluePrefab);
            }
            else
            {
                newEntity = state.EntityManager.Instantiate(spawner.ValueRW.redPrefab);
            }
            float randomPos = randomPosition.NextFloat(-spawnbound, spawnbound);
            state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(new float3(randomPos, spawner.ValueRW.spawnPosition.y, spawner.ValueRW.spawnPosition.z)));
            spawner.ValueRW.nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.spawnRate;
        }
        
    }
}
