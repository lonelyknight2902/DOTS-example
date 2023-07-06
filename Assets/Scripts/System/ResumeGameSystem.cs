using Component;
using Unity.Entities;

namespace System
{
    public partial struct ResumeGameSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ResumeGameCommand>();       
        }
        
        public void OnUpdate(ref SystemState state)
        {
            Entity gameStatus = SystemAPI.GetSingletonEntity<GameStatus>();
            bool status = SystemAPI.GetSingleton<GameStatus>().inPlay;
            // Debug.Log("Game paused");
            if (!status)
            {
                state.EntityManager.SetComponentData(gameStatus, new GameStatus
                {
                    inPlay = true
                });
            }
        }
    }
}