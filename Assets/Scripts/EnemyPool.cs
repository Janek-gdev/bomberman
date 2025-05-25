using Bomberman.Enemies;
using UnityEngine;

namespace Bomberman.Pooling
{
    [CreateAssetMenu(menuName = MenuName.Pooling + nameof(EnemyPool), fileName = nameof(EnemyPool))]
    public class EnemyPool : Pool<EnemyView>
    {
        
    }
}