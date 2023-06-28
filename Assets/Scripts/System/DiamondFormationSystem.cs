using Component;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace System
{
    public partial struct DiamondFormationSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            // EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
            // new DiamondFormationJob
            // {
            //     ecb = ecb,
            // }.ScheduleParallel();
            //
            // ecb.Playback(state.EntityManager);
            // ecb.Dispose();
            
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            var job = new DiamondFormationJob
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

public partial struct DiamondFormationJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter ecb;
    private void Execute([ChunkIndexInQuery] int chunkIndex, ref FormationSpawn formationSpawn, ref DiamondFormation formation, Entity entity)
    {
        DiamondSpawn(chunkIndex,formation.radius, formationSpawn.spawnPosition, formationSpawn.enemy);
        ecb.DestroyEntity(chunkIndex, entity);
    }

    private void DiamondSpawn(int index, int radius, float3 spawnPosition, Entity enemy)
    {
        // float spawnbound = 20f;
        // float randomEne = randomEnemy.NextFloat(-1, 1);
        // float randomPos = randomPosition.NextFloat(-spawnbound, spawnbound);
        // Entity enemy;
        // if (randomEne > 0)
        // {
        //     enemy = spawner.ValueRO.bluePrefab;
        // }
        // else
        // {
        //     enemy = spawner.ValueRO.redPrefab;
        // }

        for (int i = 0; i < radius * 2 + 1; i++)
        {
            if (i == 0 || i == radius * 2)
            {
                Entity newEnemy = ecb.Instantiate(index, enemy);
                ecb.SetComponent(index,newEnemy,
                    LocalTransform.FromPosition(new float3(spawnPosition.x, spawnPosition.y,
                        spawnPosition.z + i * 2)));
            }
            else
            {
                Entity newEnemy1 = ecb.Instantiate(index ,enemy);
                Entity newEnemy2 = ecb.Instantiate(index ,enemy);
                if (i <= radius)
                {
                    ecb.SetComponent(index, newEnemy1,
                        LocalTransform.FromPosition(new float3(spawnPosition.x - 2 * i, spawnPosition.y,
                            spawnPosition.z + i * 2)));
                    ecb.SetComponent(index, newEnemy2,
                        LocalTransform.FromPosition(new float3(spawnPosition.x + 2 * i, spawnPosition.y,
                            spawnPosition.z + i * 2)));
                }
                else
                {
                    ecb.SetComponent(index, newEnemy1,
                        LocalTransform.FromPosition(new float3(spawnPosition.x - 2 * (2 * radius - i),
                            spawnPosition.y, spawnPosition.z + i * 2)));
                    ecb.SetComponent(index, newEnemy2,
                        LocalTransform.FromPosition(new float3(spawnPosition.x + 2 * (2 * radius - i),
                            spawnPosition.y, spawnPosition.z + i * 2)));
                }
            }
        }
    }
}