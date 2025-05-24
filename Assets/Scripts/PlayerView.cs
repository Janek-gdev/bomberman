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


        private Vector2 _targetPosition;
        private void OnEnable()
        {
            _moveInput.action.performed += OnMovePressed;
        }

        private void OnDisable()
        {
            _moveInput.action.performed -= OnMovePressed;

        }

        private void OnMovePressed(InputAction.CallbackContext obj)
        {
            
        }
        
        private Vector2 GetStrongestAxis(Vector2 input)
        {
            if (input == Vector2.zero)
                return Vector2.zero;
            
            return Mathf.Abs(input.x) > Mathf.Abs(input.y)
                ? new Vector2(Mathf.Sign(input.x), 0f)
                : new Vector2(0f, Mathf.Sign(input.y));
        }

        private void Update()
        {
            transform.position =
                Vector2.MoveTowards(transform.position, _targetPosition, Time.deltaTime * _playerModel.MoveSpeed);

            // if can walk
            {
                var strongestMovement = GetStrongestAxis(_moveInput.action.ReadValue<Vector2>());
                _targetPosition += strongestMovement;
            }
            if ((transform.position - (Vector3)_targetPosition).sqrMagnitude < 0.0001f)
            {
                //reached destination
            }
        }

        private void OnUpPressed(InputAction.CallbackContext obj)
        {
            _targetPosition = new Vector2(_targetPosition.x, _targetPosition.y + 1);
        }
        private void OnDownPressed(InputAction.CallbackContext obj)
        {
        }

        private void OnLeftPressed(InputAction.CallbackContext obj)
        {
        }

        private void OnRightPressed(InputAction.CallbackContext obj)
        {
        }
    }
}