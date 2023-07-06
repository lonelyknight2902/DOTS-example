using Unity.Entities;

namespace Component
{
    public struct GameStatus : IComponentData
    {
        public bool inPlay;
    }
}