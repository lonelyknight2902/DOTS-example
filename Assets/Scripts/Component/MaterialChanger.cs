using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

namespace Component
{
    public struct MaterialChanger : IComponentData
    {
        public Entity mesh0;
        public Entity mesh1;
        public uint frequency;
        public uint frame;
        public uint active;
    }
}