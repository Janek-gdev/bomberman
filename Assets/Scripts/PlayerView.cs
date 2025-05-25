using System;
using Bomberman.Collisions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Bomberman.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private Animator _animator;
        [SerializeField] private CollisionDetector _collisionDetector;
        
        [SerializeField] private InputActionReference _moveInput;
        
        [SerializeField] private InputActionReference _layBomb;

        private Vector2 GetStrongestAxis(Vector2 input)
        {
            if (input == Vector2.zero)
                return Vector2.zero;
            
            return Mathf.Abs(input.x) > Mathf.Abs(input.y)
                ? new Vector2(Mathf.Sign(input.x), 0f)
                : new Vector2(0f, Mathf.Sign(input.y));
        }

        protected void Update()
        {
            var strongestMovement = GetStrongestAxis(_moveInput.action.ReadValue<Vector2>());
            transform.position += (Vector3)strongestMovement * (_playerModel.MoveSpeed * Time.deltaTime);
        }
    }
}