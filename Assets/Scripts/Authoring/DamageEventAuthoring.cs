using Component;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class DamageEventAuthoring : MonoBehaviour
    {
        class DamageEventBaker : Baker<DamageEventAuthoring>
        {
            public override void Bake(DamageEventAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent<DamageEvent>(entity);
            }
        }
    }
}