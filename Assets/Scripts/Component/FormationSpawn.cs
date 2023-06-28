using Unity.Entities;
using Unity.Mathematics;

namespace Component
{
    public struct FormationSpawn : IComponentData
    {
        public float3 spawnPosition;
    }
}