using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct GunSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Gun>();
    }

    public void OnDestroy(ref SystemState state)
    {
        
    }

    public void OnUpdate(ref SystemState state)
    {
            foreach (var (localToWorld, gun) in SystemAPI.Query<RefRW<LocalToWorld>, RefRW<Gun>>())
            {
                if (gun.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
                {
                    var bulletInstance = state.EntityManager.Instantiate(gun.ValueRO.BulletPrefab);
                    state.EntityManager.SetComponentData(bulletInstance, new LocalTransform
                    {
                        Position = SystemAPI.GetComponent<LocalToWorld>(gun.ValueRO.FirePoint).Position,
                        Rotation = SystemAPI.GetComponent<LocalTransform>(gun.ValueRO.BulletPrefab).Rotation,
                        Scale = SystemAPI.GetComponent<LocalTransform>(gun.ValueRO.BulletPrefab).Scale,
                    });
                
                    state.EntityManager.SetComponentData(bulletInstance, new Bullet
                    {
                        velocity = localToWorld.ValueRO.Up * 20f,
                    });
                    gun.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + gun.ValueRO.ShootRate;
                }
                
            }
    }
}
