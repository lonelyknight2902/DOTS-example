using Component;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine.Rendering;

namespace System
{
    [UpdateBefore(typeof(HitSystem))]
    public partial struct MeshChangerSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var meshEntity = SystemAPI.GetSingleton<MeshChanger>();
            foreach (var mmi in SystemAPI.Query<RefRW<MaterialMeshInfo>>().WithAll<Hit>())
            {
                mmi.ValueRW.MeshID = meshEntity.mesh0;
            }
        }
    }
}