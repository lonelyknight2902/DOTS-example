using Component;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace System
{
    public partial struct HorizontalFormationSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            var job = new HorizontalFormationJob
            {
                ecb = ecb.AsParallelWriter()
            };
            
            state.Dependency = job.Schedule(state.Dependency);
        }
    }
}

public partial struct HorizontalFormationJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter ecb;
    private void Execute([ChunkIndexInQuery] int chunkIndex, ref FormationSpawn formationSpawn, ref HorizontalFormation formation, Entity entity)
    {
        HorizontalSpawn(chunkIndex, formationSpawn.spawnPosition, formationSpawn.enemy, formation.length);
        ecb.DestroyEntity(chunkIndex, entity);
    }
    
    private void HorizontalSpawn(int index, float3 spawnPosition, Entity enemy, int length)
    {
        // int spawnbound = 1;
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
        for (int i = 0; i < length; i ++)
        {
            Entity newEntity = ecb.Instantiate(index, enemy);
            ecb.SetComponent(index, newEntity, LocalTransform.FromPosition(new float3(spawnPosition.x, spawnPosition.y, spawnPosition.z + i*4)));
        }
    }
}

