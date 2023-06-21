using Unity.Entities;

namespace Component
{
    public struct Collision: IComponentData
    {
        public Entity collidedEntity;
    }
}