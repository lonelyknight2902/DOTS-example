using Component;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace System
{
    public partial struct SquareFormationSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
            new SquareFormationJob
            {
                ecb = ecb
            }.ScheduleParallel();
        }
    }
}

[WithAll(typeof(SquareFormation))]
public partial struct SquareFormationJob : IJobEntity
{
    public EntityCommandBuffer ecb;
    public void Execute(ref FormationSpawn formationSpawn)
    {
        SquareSpawn(3, formationSpawn.spawnPosition, formationSpawn.enemy);
    }
    
    private void SquareSpawn(int side, float3 spawnPosition, Entity enemy)
    {
        // float spawnbound = 2f;
        // float randomEne = randomEnemy.NextFloat(-1, 1);
        for (int i = 0; i < side; i++)
        {
            if (i == 0 || i == side - 1)
            {
                for (int j = 0; j < side; j++)
                {
                    Entity newEntity = ecb.Instantiate(enemy);
                    // Entity newEntity;
                    // if (randomEne > 0)
                    // {
                    //     newEntity = state.EntityManager.Instantiate(spawner.ValueRO.bluePrefab);
                    // }
                    // else
                    // {
                    //     newEntity = state.EntityManager.Instantiate(spawner.ValueRO.redPrefab);
                    // }
                    ecb.SetComponent(newEntity, LocalTransform.FromPosition(new float3(spawnPosition.x - j*4, spawnPosition.y, spawnPosition.z - i*4)));
                }
            }
            else
            {
                Entity newEntity1 = ecb.Instantiate(enemy);
                Entity newEntity2 = ecb.Instantiate(enemy);
                // Entity newEntity1;
                // Entity newEntity2;
                // if (randomEne > 0)
                // {
                //     newEntity1 = state.EntityManager.Instantiate(spawner.ValueRO.bluePrefab);
                //     newEntity2 = state.EntityManager.Instantiate(spawner.ValueRO.bluePrefab);
                // }
                // else
                // {
                //     newEntity1 = state.EntityManager.Instantiate(spawner.ValueRO.redPrefab);
                //     newEntity2 = state.EntityManager.Instantiate(spawner.ValueRO.redPrefab);
                // }
                ecb.SetComponent(newEntity1, LocalTransform.FromPosition(new float3(spawnPosition.x, spawnPosition.y, spawnPosition.z - i*4)));
                ecb.SetComponent(newEntity2, LocalTransform.FromPosition(new float3( spawnPosition.x - (side-1)*4, spawnPosition.y, spawnPosition.z - i*4)));
            }
        }
    }
}