using System;
using Bomberman.Collisions;
using Bomberman.Level;
using Bomberman.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

namespace Bomberman.Player
{
    /// <summary>
    /// Converts input to directional changes.
    /// Handles collision detection and allows a slight wrapping around corners (grid correction) of the player
    /// </summary>
    public class PlayerMovementView : MonoBehaviour
    {
        [SerializeField] private LevelLayoutGeneratorModel _levelLayoutGeneratorModel;
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private DirectionalCollisionDetector _directionalCollisionDetector;
        
        [SerializeField] private InputActionReference _moveInput;
        private static readonly int RunLeft = Animator.StringToHash("RunLeft");
        private static readonly int RunRight = Animator.StringToHash("RunRight");
        private static readonly int RunUp = Animator.StringToHash("RunUp");
        private static readonly int RunDown = Animator.StringToHash("RunDown");
        private static readonly int X = Animator.StringToHash("X");
        private static readonly int Y = Animator.StringToHash("Y");

        private void OnEnable()
        {
            _playerModel.NonWalkableLayerMask = _directionalCollisionDetector.NonWalkableLayerMask;
            _playerModel.OnNonWalkableLayerMaskChanged += UpdateNonWalkableLayerMask;
        }

        private void OnDisable()
        {
            _playerModel.OnNonWalkableLayerMaskChanged -= UpdateNonWalkableLayerMask;
        }

        private void UpdateNonWalkableLayerMask(LayerMask _)
        {
            _directionalCollisionDetector.NonWalkableLayerMask = _playerModel.NonWalkableLayerMask;
        }

        protected void Update()
        {
            if (!_playerModel.IsAlive)
            {
                return;
            }
            var strongestMovementDirection = GetInputDirection(_moveInput.action.ReadValue<Vector2>());
            SetAnimation(strongestMovementDirection);
            if (_directionalCollisionDetector.IsDirectionWalkable(strongestMovementDirection))
            {
                var movement = (Vector3) DirectionUtility.GetDirectionVector(strongestMovementDirection) *
                                      (_playerModel.MoveSpeed * Time.deltaTime);
                movement += GetGridCorrection(strongestMovementDirection);

                transform.position += movement;
            }

            _playerModel.CurrentTile = _levelLayoutGeneratorModel.GetClosestFreeTile(transform);
        }

        private void SetAnimation(Direction strongestMovementDirection)
        {
            _animator.speed = strongestMovementDirection == Direction.None ? 0 : 1;
            _spriteRenderer.flipX = strongestMovementDirection == Direction.Left;
            switch (strongestMovementDirection)
            {
                case Direction.Left:
                    _animator.SetFloat(X, 1f);
                    _animator.SetFloat(Y, 0f);
                    break;
                case Direction.Right:
                    _animator.SetFloat(X, -1f);
                    _animator.SetFloat(Y, 0f);
                    break;
                case Direction.Up:
                    _animator.SetFloat(X, 0f);
                    _animator.SetFloat(Y, 1f);
                    break;
                case Direction.Down:
                    _animator.SetFloat(X, 0f);
                    _animator.SetFloat(Y, -1f);
                    break;
                case Direction.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(strongestMovementDirection), strongestMovementDirection, null);
            }
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
                var closestTile = _levelLayoutGeneratorModel.GetClosestFreeTile(transform);
                movement.y += (closestTile.Position.y - transform.position.y) *
                              (_playerModel.GridCorrectionSpeed * Time.deltaTime);
            }
            else if (strongestMovementDirection is Direction.Up or Direction.Down)
            {
                var closestTile = _levelLayoutGeneratorModel.GetClosestFreeTile(transform);
                movement.x += (closestTile.Position.x - transform.position.x) *
                              (_playerModel.GridCorrectionSpeed * Time.deltaTime) ;
            }

            return movement;
        }
    }
}