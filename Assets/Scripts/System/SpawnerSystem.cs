using System.Collections;
using System.Collections.Generic;
using Component;
using Config;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

[BurstCompile]
public partial struct SpawnerSystem : ISystem
{
    private Unity.Mathematics.Random randomPosition;
    private Unity.Mathematics.Random randomEnemy;
    private Random randomFormation;

    public void OnCreate(ref SystemState state)
    {
        randomPosition = Unity.Mathematics.Random.CreateFromIndex(123456789);
        randomEnemy = Unity.Mathematics.Random.CreateFromIndex(123454321);
        randomFormation = Random.CreateFromIndex(114252289);
    }

    public void OnDestroy(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var config = SystemAPI.GetSingleton<ConfigComponent>();
        foreach (var spawner in SystemAPI.Query<RefRW<Spawner>>())
        {
            if (config.spawnTime > 0 && spawner.ValueRO.nextSpawnTime < SystemAPI.Time.ElapsedTime)
            {
                float randomForm = randomFormation.NextFloat(0f, 3f);
                if (randomForm > 2f)
                {
                    if (spawner.ValueRO.diamond)
                    {
                        DiamondSpawn(ref state, spawner, 3);
                    }
                    else
                    {
                        randomForm -= 1f;
                    }
                }

                if (randomForm > 1f && randomForm <= 2f)
                {
                    if (spawner.ValueRO.square)
                    {
                        SquareSpawn(ref state, spawner, 3);
                    }
                    else
                    {
                        randomForm -= 1f;
                    }
                }

                if (randomForm <= 1f)
                {
                    if (spawner.ValueRO.horizontal)
                    {
                        HorizontalSpawn(ref state, spawner, 3);
                    }
                }

                if (!(spawner.ValueRO.diamond || spawner.ValueRO.square || spawner.ValueRO.horizontal))
                {
                    ProcessSpawner(ref state, spawner);
                }
                // ProcessSpawner(ref state, spawner);
                // HorizontalSpawn(ref state, spawner);
                // SquareSpawn(ref state, spawner, 3);
                spawner.ValueRW.nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.spawnRate;
                SystemAPI.SetSingleton(new ConfigComponent
                {
                    spawnTime = config.spawnTime - 1,
                });
            }
            
        }
    }

    private void ProcessSpawner(ref SystemState state, RefRW<Spawner> spawner)
    {
        float spawnbound = 3f;
        if (spawner.ValueRO.nextSpawnTime < SystemAPI.Time.ElapsedTime)
        {
            float randomEne = randomEnemy.NextFloat(-1, 1);
            Entity newEntity;
            if (randomEne > 0)
            {
                newEntity = state.EntityManager.Instantiate(spawner.ValueRO.bluePrefab);
            }
            else
            {
                newEntity = state.EntityManager.Instantiate(spawner.ValueRO.redPrefab);
            }

            float randomPos = randomPosition.NextFloat(-spawnbound, spawnbound);
            state.EntityManager.SetComponentData(newEntity,
                LocalTransform.FromPosition(new float3(spawner.ValueRO.spawnPosition.x, spawner.ValueRO.spawnPosition.y,
                    spawner.ValueRO.spawnPosition.z + randomPos)));
        }
    }

    private void HorizontalSpawn(ref SystemState state, RefRW<Spawner> spawner, int length)
    {
        int spawnbound = 1;
        float randomEne = randomEnemy.NextFloat(-1, 1);
        float randomPos = randomPosition.NextFloat(-spawnbound, spawnbound);
        Entity enemy;
        if (randomEne > 0)
        {
            enemy = spawner.ValueRO.bluePrefab;
        }
        else
        {
            enemy = spawner.ValueRO.redPrefab;
        }
        for (int i = 0; i < length; i ++)
        {
            Entity newEntity = state.EntityManager.Instantiate(enemy);
            state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(new float3(spawner.ValueRO.spawnPosition.x, spawner.ValueRO.spawnPosition.y, spawner.ValueRO.spawnPosition.z + randomPos+ i*4)));
        }
    }

    private void SquareSpawn(ref SystemState state, RefRW<Spawner> spawner, int side)
    {
        float spawnbound = 2f;
            float randomEne = randomEnemy.NextFloat(-1, 1);
            float randomPos = randomPosition.NextFloat(-spawnbound, spawnbound);
            for (int i = 0; i < side; i++)
            {
                if (i == 0 || i == side - 1)
                {
                    for (int j = 0; j < side; j++)
                    {
                        Entity newEntity;
                        if (randomEne > 0)
                        {
                            newEntity = state.EntityManager.Instantiate(spawner.ValueRO.bluePrefab);
                        }
                        else
                        {
                            newEntity = state.EntityManager.Instantiate(spawner.ValueRO.redPrefab);
                        }
                        state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(new float3(spawner.ValueRO.spawnPosition.x - j*4, spawner.ValueRO.spawnPosition.y, spawner.ValueRO.spawnPosition.z + randomPos - i*4)));
                    }
                }
                else
                {
                    Entity newEntity1;
                    Entity newEntity2;
                    if (randomEne > 0)
                    {
                        newEntity1 = state.EntityManager.Instantiate(spawner.ValueRO.bluePrefab);
                        newEntity2 = state.EntityManager.Instantiate(spawner.ValueRO.bluePrefab);
                    }
                    else
                    {
                        newEntity1 = state.EntityManager.Instantiate(spawner.ValueRO.redPrefab);
                        newEntity2 = state.EntityManager.Instantiate(spawner.ValueRO.redPrefab);
                    }
                    state.EntityManager.SetComponentData(newEntity1, LocalTransform.FromPosition(new float3(spawner.ValueRO.spawnPosition.x, spawner.ValueRO.spawnPosition.y, spawner.ValueRO.spawnPosition.z + randomPos - i*4)));
                    state.EntityManager.SetComponentData(newEntity2, LocalTransform.FromPosition(new float3( spawner.ValueRO.spawnPosition.x - (side-1)*4, spawner.ValueRO.spawnPosition.y, spawner.ValueRO.spawnPosition.z + randomPos - i*4)));

                }
            }
    }

    private void DiamondSpawn(ref SystemState state, RefRW<Spawner> spawner, int radius)
    {
        float spawnbound = 20f;
            float randomEne = randomEnemy.NextFloat(-1, 1);
            float randomPos = randomPosition.NextFloat(-spawnbound, spawnbound);
            Entity enemy;
            if (randomEne > 0)
            {
                enemy = spawner.ValueRO.bluePrefab;
            }
            else
            {
                enemy = spawner.ValueRO.redPrefab;
            }
            for (int i = 0; i < radius * 2 + 1; i++)
            {
                if (i == 0 || i == radius*2)
                {
                    Entity newEnemy = state.EntityManager.Instantiate(enemy);
                    state.EntityManager.SetComponentData(newEnemy, LocalTransform.FromPosition(new float3(randomPos, spawner.ValueRO.spawnPosition.y, spawner.ValueRO.spawnPosition.z + i*2)));
                }
                else
                {
                    Entity newEnemy1 = state.EntityManager.Instantiate(enemy);
                    Entity newEnemy2 = state.EntityManager.Instantiate(enemy);
                    if (i <= radius)
                    {
                        state.EntityManager.SetComponentData(newEnemy1, LocalTransform.FromPosition(new float3(randomPos - 2*i, spawner.ValueRO.spawnPosition.y, spawner.ValueRO.spawnPosition.z + i*2)));
                        state.EntityManager.SetComponentData(newEnemy2, LocalTransform.FromPosition(new float3(randomPos + 2*i, spawner.ValueRO.spawnPosition.y, spawner.ValueRO.spawnPosition.z + i*2)));
                    }
                    else
                    {
                        state.EntityManager.SetComponentData(newEnemy1, LocalTransform.FromPosition(new float3(randomPos - 2*(2*radius-i), spawner.ValueRO.spawnPosition.y, spawner.ValueRO.spawnPosition.z + i*2)));
                        state.EntityManager.SetComponentData(newEnemy2, LocalTransform.FromPosition(new float3(randomPos + 2*(2*radius-i), spawner.ValueRO.spawnPosition.y, spawner.ValueRO.spawnPosition.z + i*2)));
                    }
                }
            }
    }

    private void FormationSpawn(ref SystemState state, RefRW<Spawner> spawner)
    {
        
    }
}
