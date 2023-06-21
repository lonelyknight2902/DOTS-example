using Unity.Entities;
using Unity.Mathematics;

namespace Component
{
    public struct Move: IComponentData
    {
        public float speed;
        public float3 direction;
    }
}