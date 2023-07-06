using Component;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace System
{
    public partial struct StartGameSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<StartGameCommand>();
        }

        public void OnUpdate(ref SystemState state)
        {
            Entity gameStatus = SystemAPI.GetSingletonEntity<GameStatus>();
            bool status = SystemAPI.GetSingleton<GameStatus>().inPlay;
            // bool status = state.EntityManager.GetComponentData<GameStatus>(gameStatus).inPlay;
            // EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            new StartGameCommandListenerJob
            {
                gameStatus = gameStatus,
                status = status,
                ecb = ecb
                
            }.Schedule();
            state.Enabled = false;
        }
    }

    public partial struct StartGameCommandListenerJob : IJobEntity
    {
        public Entity gameStatus;
        public bool status;
        public EntityCommandBuffer ecb;
        public void Execute(in StartGameCommand command)
        {
            if (!status)
            {
                ecb.SetComponent(gameStatus, new GameStatus
                {
                    inPlay = true
                });
            }
        }
    }
}