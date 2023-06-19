using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRate;
    
    
}

public class SpawnerBaker : Baker<SpawnerAuthoring>
{
    public override void Bake(SpawnerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new Spawner
        {
            prefab = GetEntity(authoring.prefab, TransformUsageFlags.None),
            nextSpawnTime = 0.0f,
            spawnPosition = authoring.transform.position,
            spawnRate = authoring.spawnRate,
        });
    }
}
