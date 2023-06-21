using Component;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;

namespace System
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(SimulationSystemGroup))]
    public partial struct BulletCollideSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Bullet>();
            state.RequireForUpdate<Enemy>();
            state.RequireForUpdate<SimulationSingleton>();
        }

        public partial struct JobCheckCollision : ITriggerEventsJob
        {
            public ComponentLookup<Enemy> enemyLookup;
            public ComponentLookup<Bullet> bulletLookup;
            public ComponentLookup<Damage> damageLookup;
            public EntityCommandBuffer ecb;

            private bool isEnemy(Entity e)
            {
                return enemyLookup.HasComponent(e);
            }

            private bool isBullet(Entity e)
            {
                return bulletLookup.HasComponent(e);
            }
            public void Execute(TriggerEvent triggerEvent)
            {
                var isEnemyA = isEnemy(triggerEvent.EntityA);
                var isEnemyB = isEnemy(triggerEvent.EntityB);

                var isBulletA = isBullet(triggerEvent.EntityA);
                var isBulletB = isBullet(triggerEvent.EntityB);

                var validA = (isEnemyA != isBulletA);
                if (!validA)
                {
                    return;
                }

                var validB = (isEnemyB != isBulletB);
                if (!validB)
                {
                    return;
                }
                
                var v = (isEnemyA == isBulletB) || (isBulletA == isEnemyB);
                if (!v)
                {
                    return;
                }
                
                var destroyableA = false;
                var destroyableB = false;
                if (enemyLookup.HasComponent(triggerEvent.EntityA))
                {
                    // ecb.AddComponent(triggerEvent.EntityA, new Hit
                    // {
                    //     damage = damageLookup.GetRefRO(triggerEvent.EntityB).ValueRO.damage
                    // });
                    if (enemyLookup.IsComponentEnabled(triggerEvent.EntityA))
                    {
                        ecb.SetComponentEnabled<Enemy>(triggerEvent.EntityA, false);
                        
                        
                        destroyableA = true;
                    }
                    //a is enemy
                }
                else if (bulletLookup.HasComponent(triggerEvent.EntityA))
                {
                    if (bulletLookup.IsComponentEnabled(triggerEvent.EntityA))
                    {
                        //a is bullet
                        ecb.SetComponentEnabled<Bullet>(triggerEvent.EntityA, false);
                        destroyableA = true;
                    }
                }

                if (enemyLookup.HasComponent(triggerEvent.EntityB))
                {
                    // ecb.AddComponent(triggerEvent.EntityA, new Hit
                    // {
                    //     damage = damageLookup.GetRefRO(triggerEvent.EntityA).ValueRO.damage
                    // });
                    if (enemyLookup.IsComponentEnabled(triggerEvent.EntityB))
                    {
                        ecb.SetComponentEnabled<Enemy>(triggerEvent.EntityB, false);
                        
                        destroyableB = true;
                    }
                    //b is enemy
                }
                else if (bulletLookup.HasComponent(triggerEvent.EntityB))
                {
                    if (bulletLookup.IsComponentEnabled(triggerEvent.EntityB))
                    {
                        ecb.SetComponentEnabled<Bullet>(triggerEvent.EntityB, false);
                        destroyableB = true;
                    }
                    //b is bullet
                }

                // if (destroyableA)
                // {
                //     ecb.AddComponent<Destroyed>(triggerEvent.EntityA);
                // }
                //
                // if (destroyableB)
                // {
                //     ecb.AddComponent<Destroyed>(triggerEvent.EntityB);
                // }

                if (destroyableA && destroyableB)
                {
                    
                }
            }
        }

        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
            state.Dependency = new JobCheckCollision
            {
                enemyLookup = SystemAPI.GetComponentLookup<Enemy>(),
                bulletLookup = SystemAPI.GetComponentLookup<Bullet>(),
                damageLookup = SystemAPI.GetComponentLookup<Damage>(),
                ecb = ecb
            }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
            state.Dependency.Complete();
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}