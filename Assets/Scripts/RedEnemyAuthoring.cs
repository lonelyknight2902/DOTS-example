using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class RedEnemyAuthoring : MonoBehaviour
{
    public float speed;
    public class RedBaker : Baker<RedEnemyAuthoring>
    {
        public override void Bake(RedEnemyAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Movement
            {
                speed = authoring.speed
            });
            AddComponent(entity, new Enemy
            {
                direction = new float3(1, 0, 0)
            });
        }
    }
}
