using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BlueEnemyAuthoring : MonoBehaviour
{
    public float speed;
    public float rotation;
    public int hitpoint;
    public class BlueBaker : Baker<BlueEnemyAuthoring>
    {
        public override void Bake(BlueEnemyAuthoring authoring)
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
            AddComponent(entity, new Rotation
            {
                rotationSpeed = authoring.rotation
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
