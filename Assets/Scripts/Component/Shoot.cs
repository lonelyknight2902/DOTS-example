using Unity.Entities;

namespace Component
{
    public struct Shoot : IComponentData
    {
        public Entity BulletPrefab;
        public Entity FirePoint;
        public float NextSpawnTime;
        public float ShootRate;
    }
}