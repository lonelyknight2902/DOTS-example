using System.Collections;
using System.Collections.Generic;
using Component;
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
            AddComponent<Move>(entity);
            AddComponent(entity, new Damage
            {
                damage = 5,
            });
            AddComponent<Bullet>(entity);
        }
    }
}

public struct Bullet : IComponentData, IEnableableComponent
{
}
