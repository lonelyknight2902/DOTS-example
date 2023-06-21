using System.Collections;
using System.Collections.Generic;
using Component;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public float speed = 2.0f;

    class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Move
            {
                speed = authoring.speed,
                direction = new float3(0,0,0)
            });
            AddComponent(entity, new Player());
        }
    }
}


