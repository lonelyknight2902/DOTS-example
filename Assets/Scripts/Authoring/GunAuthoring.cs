using System.Collections;
using System.Collections.Generic;
using Component;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class GunAuthoring : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform FirePoint;
    public float ShootRate;

    class GunBaker : Baker<GunAuthoring>
    {
        public override void Bake(GunAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Shoot
            {
                BulletPrefab = GetEntity(authoring.BulletPrefab, TransformUsageFlags.Dynamic),
                FirePoint = GetEntity(authoring.FirePoint, TransformUsageFlags.Dynamic),
                NextSpawnTime = 0.0f,
                ShootRate = authoring.ShootRate
            });
        }
    }
}

