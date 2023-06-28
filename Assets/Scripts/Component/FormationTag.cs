using Unity.Entities;

namespace Component
{
    public struct SquareFormation : IComponentData
    {
        public int side;
    }

    public struct DiamondFormation : IComponentData
    {
        public int radius;
    }

    public struct HorizontalFormation : IComponentData
    {
        public int length;
    }
}