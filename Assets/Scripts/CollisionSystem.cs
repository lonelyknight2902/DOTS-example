using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

// [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
// [UpdateBefore(typeof(PhysicsSimulationGroup))] // We are updating before `PhysicsSimulationGroup` - this means that we will get the events of the previous frame
// public partial struct GetNumCollisionEventsSystem : ISystem
// {
//     [BurstCompile]
//     public partial struct CountNumCollisionEvents : ICollisionEventsJob
//     {
//         public NativeReference<int> NumCollisionEvents;
//         public void Execute(CollisionEvent collisionEvent)
//         {
//             NumCollisionEvents.Value++;
//         }
//     }
//
//     [BurstCompile]
//     public void OnUpdate(ref SystemState state)
//     {
//         NativeReference<int> numCollisionEvents = new NativeReference<int>(0, Allocator.TempJob);
//
//         state.Dependency = new CountNumCollisionEvents
//         {
//             NumCollisionEvents = numCollisionEvents
//         }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>());
//
//         // ...
//     }
// }
public partial struct CollisionSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
    }

    public void OnUpdate(ref SystemState state)
    { 
        // var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        // var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
        // var simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
        //
        // // EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
        // JobHandle trigger = new CollisionEvent
        // {
        //     BulletGroup = state.GetComponentLookup<Bullet>(),
        //     EnemyGroup = state.GetComponentLookup<Enemy>(),
        //     CommandBuffer = ecb
        // }.Schedule(simulationSingleton, state.Dependency);
        //
        // trigger.Complete();
        // ecb.Playback(state.EntityManager);
        // ecb.Dispose();
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        foreach (var (enemy, hp, enemyEntity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<HP>>().WithAll<Enemy>().WithAll<Alive>().WithEntityAccess())
        {
            foreach (var (bullet, damage, bulletEntity) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<Damage>>().WithAll<Bullet>().WithEntityAccess()) 
            {
                if (math.distancesq(enemy.ValueRO.Position, bullet.ValueRO.Position) < 0.6f)
                {
                    hp.ValueRW.hitpoint -= damage.ValueRO.damage;
                    ecb.DestroyEntity(bulletEntity);
                }
            }
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}

// public struct CollisionEvent : ICollisionEventsJob
// {
//     public ComponentLookup<Bullet> BulletGroup;
//     public ComponentLookup<Enemy> EnemyGroup;
//     public EntityCommandBuffer CommandBuffer;
//     bool isBullet(Entity e)
//     {
//         return BulletGroup.HasComponent(e);
//     }
//
//     bool isEnemy(Entity e)
//     {
//         return EnemyGroup.HasComponent(e);
//     }
//     
//     public void Execute(Unity.Physics.CollisionEvent collisionEvent)
//     {
//         Entity A = collisionEvent.EntityA;
//         Entity B = collisionEvent.EntityB;
//         Entity bulletEntity = Entity.Null;
//         Entity EnemyEntity = Entity.Null;
//         if (isBullet(A) && isEnemy(B))
//         {
//             bulletEntity = A;
//             EnemyEntity = B;
//         }
//         else
//         {
//             bulletEntity = B;
//             EnemyEntity = A;
//         }
//         
//         if(bulletEntity != Entity.Null && EnemyEntity != Entity.Null)
//         {            
//             CommandBuffer.AddComponent<Dead>(EnemyEntity);
//             CommandBuffer.DestroyEntity(bulletEntity);
//         }
//     }
// }