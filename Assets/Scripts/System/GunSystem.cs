using System.Collections;
using System.Collections.Generic;
using Component;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct GunSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Shoot>();
    }

    public void OnDestroy(ref SystemState state)
    {
        
    }

    public void OnUpdate(ref SystemState state)
    {
        // bool isPressedSpace;
        // if (Input.GetKey(KeyCode.Space))
        // {
        //     isPressedSpace = true;
        // }
        // else
        // {
        //     isPressedSpace = false;
        // }
        var isPressedSpace = Input.GetKey(KeyCode.Space);
        foreach (var (localToWorld, gun) in SystemAPI.Query<RefRW<LocalToWorld>, RefRW<Shoot>>())
        {
            
            // Debug.Log(gun.ValueRO.NextSpawnTime);
            if (!isPressedSpace)
            {
                gun.ValueRW.NextSpawnTime = 0;
            }
            else
            {
                if (gun.ValueRO.NextSpawnTime <= 0)
                {
                    var bulletInstance = state.EntityManager.Instantiate(gun.ValueRO.BulletPrefab);
                    state.EntityManager.SetComponentData(bulletInstance, new LocalTransform
                    {
                        Position = SystemAPI.GetComponent<LocalToWorld>(gun.ValueRO.FirePoint).Position,
                        Rotation = SystemAPI.GetComponent<LocalTransform>(gun.ValueRO.BulletPrefab).Rotation,
                        Scale = SystemAPI.GetComponent<LocalTransform>(gun.ValueRO.BulletPrefab).Scale,
                    });
                
                    state.EntityManager.SetComponentData(bulletInstance, new Move()
                    {
                        speed = 20f,
                        direction = localToWorld.ValueRO.Up,
                    });
                    gun.ValueRW.NextSpawnTime = gun.ValueRO.ShootRate;
                }
                else
                {
                    gun.ValueRW.NextSpawnTime -= SystemAPI.Time.DeltaTime;
                }
            }
            
                
        }
        // foreach (var (localToWorld, gun) in SystemAPI.Query<RefRW<LocalToWorld>, RefRW<Gun>>())
        // {
        //     if (gun.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
        //     {
        //         var bulletInstance = state.EntityManager.Instantiate(gun.ValueRO.BulletPrefab);
        //         state.EntityManager.SetComponentData(bulletInstance, new LocalTransform
        //         {
        //             Position = SystemAPI.GetComponent<LocalToWorld>(gun.ValueRO.FirePoint).Position,
        //             Rotation = SystemAPI.GetComponent<LocalTransform>(gun.ValueRO.BulletPrefab).Rotation,
        //             Scale = SystemAPI.GetComponent<LocalTransform>(gun.ValueRO.BulletPrefab).Scale,
        //         });
        //     
        //         state.EntityManager.SetComponentData(bulletInstance, new Bullet
        //         {
        //             velocity = localToWorld.ValueRO.Up * 20f,
        //         });
        //         gun.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + gun.ValueRO.ShootRate;
        //     }
        //     
        // }
    }
}
