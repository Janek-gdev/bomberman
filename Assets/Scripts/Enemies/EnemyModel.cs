using UnityEngine;

namespace Bomberman.Enemies
{
    /// <summary>
    /// A model representing a template of an enemy 
    /// </summary>
    [CreateAssetMenu(menuName = ScriptableObjectMenuName.Enemies + nameof(EnemyModel), fileName = nameof(EnemyModel))]
    public class EnemyModel : ScriptableObject
    {
        [SerializeField] private float _moveSpeed = 1f;
        public float MoveSpeed => _moveSpeed;

        [SerializeField] private EnemyView _enemyPrefab;
        public EnemyView EnemyPrefab => _enemyPrefab;
    }
}