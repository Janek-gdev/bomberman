using Bomberman.Enemies;
using UnityEngine;

namespace Bomberman.Pooling
{
    [CreateAssetMenu(menuName = ScriptableObjectMenuName.Pooling + nameof(EnemyPool), fileName = nameof(EnemyPool))]
    public class EnemyPool : Pool<EnemyView>
    {
        
    }
}