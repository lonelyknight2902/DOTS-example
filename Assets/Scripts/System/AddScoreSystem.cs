using Component;
using Unity.Collections;
using Unity.Entities;

namespace System
{
    public partial struct AddScoreSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (score, entity) in SystemAPI.Query<RefRO<AddScore>>().WithEntityAccess())
            {
                ecb.SetComponent(entity, new Player
                {
                    score = state.EntityManager.GetComponentData<Player>(entity).score + score.ValueRO.score,
                });
                ecb.RemoveComponent<AddScore>(entity);
            }
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}