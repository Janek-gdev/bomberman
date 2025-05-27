using Bomberman.Bombing;
using UnityEngine;

namespace Bomberman.Pooling
{
    [CreateAssetMenu(menuName = ScriptableObjectMenuName.Pooling + nameof(BombPool), fileName = nameof(BombPool))]
    public class BombPool : Pool<BombView>
    {
        
    }
}