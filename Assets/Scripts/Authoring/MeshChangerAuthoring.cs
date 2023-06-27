using Component;
using Unity.Entities;
using Unity.Rendering;
using Unity.VisualScripting;
using UnityEngine;

namespace Authoring
{
    public class MeshChangerAuthoring : MonoBehaviour
    {
        public Mesh mesh0;
        public Mesh mesh1;

        public class MeshChangerBaker : Baker<MeshChangerAuthoring>
        {
            public override void Bake(MeshChangerAuthoring authoring)
            {
                var hybridRenderer = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<EntitiesGraphicsSystem>();
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new MeshChanger
                {
                    mesh0 = hybridRenderer.RegisterMesh(authoring.mesh0),
                    mesh1 = hybridRenderer.RegisterMesh(authoring.mesh1),
                });
            }
        }
    }
}