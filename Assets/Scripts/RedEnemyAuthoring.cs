using System.Collections;
using System.Collections.Generic;
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
            AddComponent(entity, new Movement
            {
                speed = authoring.speed
            });
            AddComponent(entity, new Direction
            {
                direction = new float3(0, 0, -1)
            });
            AddComponent<Enemy>(entity);
            AddComponent<Alive>(entity);
            AddComponent(entity, new HP
            {
                hitpoint = authoring.hitpoint,
            });
        }
    }
}
