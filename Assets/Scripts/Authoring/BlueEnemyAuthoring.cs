using System.Collections;
using System.Collections.Generic;
using Component;
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
            AddComponent(entity, new Move
            {
                speed = authoring.speed,
                direction = new float3(0,0,-1)
            });
            AddComponent<Enemy>(entity);
            AddComponent(entity, new Hp
            {
                hitpoint = authoring.hitpoint,
            });
        }
    }
}
