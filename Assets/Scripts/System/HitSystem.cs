using Component;
using Unity.Collections;
using Unity.Entities;

namespace System
{
    public partial struct HitSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (damage,hp, entity) in SystemAPI.Query<RefRO<Hit>, RefRW<Hp>>().WithEntityAccess())
            {
                hp.ValueRW.hitpoint -= damage.ValueRO.damage;
                ecb.RemoveComponent<Hit>(entity);
            }
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}