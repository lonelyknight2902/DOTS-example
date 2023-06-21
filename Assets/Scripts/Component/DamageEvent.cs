using Unity.Entities;

namespace Component
{
    public struct DamageEvent : IComponentData
    {
        public Entity TargetEntity;
        public Entity DamageEntity;
    }
}