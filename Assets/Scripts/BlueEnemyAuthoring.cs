using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BlueEnemyAuthoring : MonoBehaviour
{
    public float speed;
    public float rotation;
    public class BlueBaker : Baker<BlueEnemyAuthoring>
    {
        public override void Bake(BlueEnemyAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Movement
            {
                speed = authoring.speed
            });
            AddComponent(entity, new Enemy
            {
                direction = new float3(-1, 0, 0)
            });
            AddComponent(entity, new Rotation
            {
                rotationSpeed = authoring.rotation
            });
        }
    }
}
