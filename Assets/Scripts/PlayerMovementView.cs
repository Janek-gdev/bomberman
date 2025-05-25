using System;
using Bomberman.Collisions;
using Bomberman.Level;
using Bomberman.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Bomberman.Player
{
    public class PlayerMovementView : MonoBehaviour
    {
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private Animator _animator;
        [SerializeField] private CollisionDetector _collisionDetector;
        
        [SerializeField] private InputActionReference _moveInput;

        protected void Update()
        {
            if (!_playerModel.IsAlive)
            {
                return;
            }
            var strongestMovementDirection = GetInputDirection(_moveInput.action.ReadValue<Vector2>());
            if (_collisionDetector.IsDirectionWalkable(strongestMovementDirection))
            {
                var movement = (Vector3) DirectionUtility.GetDirectionVector(strongestMovementDirection) *
                                      (_playerModel.MoveSpeed * Time.deltaTime);
                movement += GetGridCorrection(strongestMovementDirection);

                transform.position += movement;
            }

            _playerModel.CurrentTile = LevelLayoutGeneratorModel.instance.GetClosestFreeTile(transform);
        }

        private Direction GetInputDirection(Vector2 input)
        {
            if (input == Vector2.zero)
                return Direction.None;

            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                return input.x < 0 ? Direction.Left : Direction.Right;
            }
            else
            {
                return input.y < 0 ? Direction.Down : Direction.Up;
            }
        }

        private Vector3 GetGridCorrection(Direction strongestMovementDirection)
        {
            var movement = new Vector3();
            
            if (strongestMovementDirection is Direction.Left or Direction.Right)
            {
                var closestTile = LevelLayoutGeneratorModel.instance.GetClosestFreeTile(transform);
                movement.y += (closestTile.Position.y - transform.position.y) *
                              (_playerModel.GridCorrectionSpeed * Time.deltaTime);
            }
            else if (strongestMovementDirection is Direction.Up or Direction.Down)
            {
                var closestTile = LevelLayoutGeneratorModel.instance.GetClosestFreeTile(transform);
                movement.x += (closestTile.Position.x - transform.position.x) *
                              (_playerModel.GridCorrectionSpeed * Time.deltaTime) ;
            }

            return movement;
        }
    }
}