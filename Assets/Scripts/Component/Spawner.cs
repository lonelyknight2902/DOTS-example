using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Component
{
    public struct Spawner : IComponentData
    {
        public Entity bluePrefab;
        public Entity redPrefab;
        public float3 spawnPosition;
        public float nextSpawnTime;
        public float spawnRate;
        public bool diamond;
        public bool square;
        public bool horizontal;
    }
}



