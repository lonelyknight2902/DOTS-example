using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class GunAuthoring : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform FirePoint;

    class GunBaker : Baker<GunAuthoring>
    {
        public override void Bake(GunAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Gun
            {
                BulletPrefab = GetEntity(authoring.BulletPrefab, TransformUsageFlags.Dynamic),
                FirePoint = GetEntity(authoring.FirePoint, TransformUsageFlags.Dynamic),
            });
        }
    }
}

public struct Gun : IComponentData
{
    public Entity BulletPrefab;
    public Entity FirePoint;
}
