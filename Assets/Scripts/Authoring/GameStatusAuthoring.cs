using Component;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class GameStatusAuthoring : MonoBehaviour
    {
        public class GameStatusBaker : Baker<GameStatusAuthoring>
        {
            public override void Bake(GameStatusAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new GameStatus
                {
                    inPlay = false,
                });
            }
        }
    }
}