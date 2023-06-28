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
            // EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
            // new SquareFormationJob
            // {
            //     ecb = ecb
            // }.ScheduleParallel();
            // ecb.Playback(state.EntityManager);
            // ecb.Dispose();
            
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            var job = new SquareFormationJob
            {
                ecb = ecb.AsParallelWriter()
            };
            
            state.Dependency = job.Schedule(state.Dependency);
            
            // state.Dependency.Complete();
            // ecb.Playback(state.EntityManager);
            // ecb.Dispose();
        }
    }
}

public partial struct SquareFormationJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter ecb;
    public void Execute([ChunkIndexInQuery] int chunkIndex, ref FormationSpawn formationSpawn, ref SquareFormation formation, Entity entity)
    {
        SquareSpawn(chunkIndex, formation.side, formationSpawn.spawnPosition, formationSpawn.enemy);
        ecb.DestroyEntity(chunkIndex, entity);
    }
    
    private void SquareSpawn(int index, int side, float3 spawnPosition, Entity enemy)
    {
        // float spawnbound = 2f;
        // float randomEne = randomEnemy.NextFloat(-1, 1);
        for (int i = 0; i < side; i++)
        {
            if (i == 0 || i == side - 1)
            {
                for (int j = 0; j < side; j++)
                {
                    Entity newEntity = ecb.Instantiate(index, enemy);
                    // Entity newEntity;
                    // if (randomEne > 0)
                    // {
                    //     newEntity = state.EntityManager.Instantiate(spawner.ValueRO.bluePrefab);
                    // }
                    // else
                    // {
                    //     newEntity = state.EntityManager.Instantiate(spawner.ValueRO.redPrefab);
                    // }
                    ecb.SetComponent(index, newEntity, LocalTransform.FromPosition(new float3(spawnPosition.x - j*4, spawnPosition.y, spawnPosition.z - i*4)));
                }
            }
            else
            {
                Entity newEntity1 = ecb.Instantiate(index, enemy);
                Entity newEntity2 = ecb.Instantiate(index, enemy);
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
                ecb.SetComponent(index, newEntity1, LocalTransform.FromPosition(new float3(spawnPosition.x, spawnPosition.y, spawnPosition.z - i*4)));
                ecb.SetComponent(index, newEntity2, LocalTransform.FromPosition(new float3( spawnPosition.x - (side-1)*4, spawnPosition.y, spawnPosition.z - i*4)));
            }
        }
    }
}