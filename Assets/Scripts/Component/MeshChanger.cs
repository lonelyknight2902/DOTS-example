using Unity.Entities;
using UnityEngine.Rendering;

namespace Component
{
    public struct MeshChanger : IComponentData
    {
        public BatchMeshID mesh0;
        public BatchMeshID mesh1;
    }
}