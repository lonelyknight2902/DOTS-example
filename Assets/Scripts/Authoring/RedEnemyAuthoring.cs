using System.Collections;
using System.Collections.Generic;
using Component;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class RedEnemyAuthoring : MonoBehaviour
{
    public float speed;
    public int hitpoint;
    public class RedBaker : Baker<RedEnemyAuthoring>
    {
        public override void Bake(RedEnemyAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Move
            {
                speed = authoring.speed,
                direction = new float3(1,0,0)
            });
            AddComponent<Enemy>(entity);
            AddComponent(entity, new Hp
            {
                hitpoint = authoring.hitpoint,
            });
        }
    }
}
