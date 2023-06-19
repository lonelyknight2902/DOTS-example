using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public float speed = 2.0f;

    class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Movement
            {
                speed = authoring.speed,
            });
            AddComponent(entity, new Player());
        }
    }
}

public struct Player : IComponentData
{
}

public struct Movement : IComponentData
{
    public float speed;
}
