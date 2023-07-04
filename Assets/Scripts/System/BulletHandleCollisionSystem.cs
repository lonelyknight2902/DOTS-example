using Component;
using CortexDeveloper.ECSMessages.Service;
// using Config;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace System
{
    [UpdateAfter(typeof(BulletCollideSystem))]
    public partial struct BulletHandleCollisionSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<DamageEvent>();
            // state.RequireForUpdate<ConfigComponent>();
        }

        public void OnUpdate(ref SystemState state)
        {
            // var bulletConfig = SystemAPI.GetSingleton<ConfigComponent>();
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (damageEvent, entity) in SystemAPI.Query<RefRO<DamageEvent>>().WithEntityAccess())
            {
                var damage = state.EntityManager.GetComponentData<Damage>(damageEvent.ValueRO.DamageEntity).damage;
                ecb.AddComponent(damageEvent.ValueRO.TargetEntity, new Hit
                {
                    damage = damage,
                });
                
                ecb.AddComponent<Destroyed>(damageEvent.ValueRO.DamageEntity);
                // ecb.RemoveComponent<DamageEvent>(entity);
                ecb.AddComponent<Destroyed>(entity);
            }
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}