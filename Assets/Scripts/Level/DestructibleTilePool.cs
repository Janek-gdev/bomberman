using Bomberman.Level;
using UnityEngine;

namespace Bomberman.Pooling
{
    [CreateAssetMenu(menuName = MenuName.Pooling + nameof(DestructibleTilePool), fileName = nameof(DestructibleTilePool))]
    public class DestructibleTilePool : Pool<DestructibleTileView>
    {
        
    }
}