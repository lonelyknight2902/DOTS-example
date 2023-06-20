using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BulletAuthoring : MonoBehaviour
{
    class BulletBaker : Baker<BulletAuthoring>
    {
        public override void Bake(BulletAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<Bullet>(entity);
            AddComponent(entity, new Damage
            {
                damage = 5,
            });
        }
    }
}

public struct Bullet : IComponentData
{
    public float3 velocity;
}

public struct Damage : IComponentData
{
    public int damage;
}