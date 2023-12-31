using System.Collections;
using System.Collections.Generic;
using Component;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial struct CollisionSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
    }

    public void OnUpdate(ref SystemState state)
    {
        // EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        // foreach (var (enemy, hp, enemyEntity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<Hp>>().WithAll<Enemy>().WithNone<Destroyed>().WithEntityAccess())
        // {
        //     foreach (var (bullet, damage, bulletEntity) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<Damage>>().WithAll<Bullet>().WithNone<Destroyed>().WithEntityAccess()) 
        //     {
        //         if (math.distancesq(enemy.ValueRO.Position, bullet.ValueRO.Position) < 0.6f)
        //         {
        //             hp.ValueRW.hitpoint -= damage.ValueRO.damage;
        //             ecb.AddComponent<Destroyed>(bulletEntity);
        //         }
        //     }
        // }
        // ecb.Playback(state.EntityManager);
        // ecb.Dispose();
    }
}
