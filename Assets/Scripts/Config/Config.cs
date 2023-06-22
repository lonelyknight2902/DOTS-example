using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Config
{
    public class Config : MonoBehaviour
    {
        // public List<int> bulletType;
        public int spawnTime = 3;
        class ConfigBaker : Baker<Config>
        {
            public override void Bake(Config authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                // AddComponent(entity, new ConfigComponent
                // {
                //     bulletType = new NativeArray<int>(bulletType, Allocator.Temp),
                // });
                AddComponent(entity, new ConfigComponent
                {
                    spawnTime = authoring.spawnTime
                });
            }
        }
    }

    public struct ConfigComponent : IComponentData
    {
        public int spawnTime;
    }
}