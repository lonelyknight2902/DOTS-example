using System.Collections;
using System.Collections.Generic;
using Component;
using Unity.Entities;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject redPrefab;
    public GameObject bluePrefab;
    public float spawnRate;
    public bool diamond;
    public bool square;
    public bool horizontal;
}

public class SpawnerBaker : Baker<SpawnerAuthoring>
{
    public override void Bake(SpawnerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new Spawner
        {
            bluePrefab = GetEntity(authoring.bluePrefab, TransformUsageFlags.None),
            redPrefab = GetEntity(authoring.redPrefab, TransformUsageFlags.None),
            nextSpawnTime = 0.0f,
            spawnPosition = authoring.transform.position,
            spawnRate = authoring.spawnRate,
            diamond = authoring.diamond,
            square = authoring.square,
            horizontal = authoring.horizontal,
        });
    }
}
