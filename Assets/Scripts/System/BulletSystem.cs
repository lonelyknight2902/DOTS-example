using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct BulletSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        
    }

    public void OnDestroy(ref SystemState state)
    {
        
    }

    public void OnUpdate(ref SystemState state)
    {
        // var deltaTime = SystemAPI.Time.DeltaTime;
        // new BulletJob
        // {
        //     deltaTime = deltaTime
        // }.ScheduleParallel();
    }
}
//
// public partial struct BulletJob : IJobEntity
// {
//     public float deltaTime;
//     void Execute(ref LocalTransform transform, in Bullet bullet)
//     {
//         transform.Position += bullet.velocity * deltaTime;
//     }
// }