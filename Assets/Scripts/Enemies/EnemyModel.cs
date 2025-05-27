using UnityEngine;

namespace Bomberman.Enemies
{
    [CreateAssetMenu(menuName = MenuName.Enemies + nameof(EnemyModel), fileName = nameof(EnemyModel))]
    public class EnemyModel : ScriptableObject
    {
        [SerializeField] private float _moveSpeed = 1f;
        public float MoveSpeed => _moveSpeed;
    }
}