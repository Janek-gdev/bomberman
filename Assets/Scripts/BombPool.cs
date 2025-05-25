using Bomberman.Bombing;
using Bomberman.Player;
using UnityEngine;

namespace Bomberman.Pooling
{
    [CreateAssetMenu(menuName = MenuName.Pooling + nameof(BombPool), fileName = nameof(BombPool))]
    public class BombPool : Pool<BombView>
    {
        
    }
}