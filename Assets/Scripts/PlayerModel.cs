using UnityEngine;

namespace Bomberman.Player
{
    [CreateAssetMenu(menuName = MenuName.Player + nameof(PlayerModel), fileName = nameof(PlayerModel))]
    public class PlayerModel : ScriptableObject
    {
        [SerializeField] private float _moveSpeed = 1;

        public float MoveSpeed => _moveSpeed;
    }
}