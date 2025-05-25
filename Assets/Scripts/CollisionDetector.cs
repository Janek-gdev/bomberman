using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bomberman.Collisions
{
    public class CollisionDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask _nonWalkableLayerMask;
        [SerializeField] private float _raycastDistance = 1f;
        private readonly RaycastHit2D[] _raycastResults = new RaycastHit2D[10];

        public bool IsDirectionWalkable(Direction direction)
        {
            if (direction == Direction.None)
            {
                return false;
            }
            return IsDirectionWalkable(GetDirectionVector(direction));
        }

        public bool IsDirectionWalkable(Vector2 direction)
        {
            var hits = Physics2D.RaycastNonAlloc(transform.position, direction, _raycastResults, _raycastDistance, _nonWalkableLayerMask);
            Debug.Log(_raycastResults[0]);
            return hits == 0;
        }

        public Vector2 GetDirectionVector(Direction direction)
        {
            return direction switch
            {
                Direction.Left => Vector2.left,
                Direction.Right => Vector2.right,
                Direction.Up => Vector2.up,
                Direction.Down => Vector2.down,
                _ => Vector2.zero
            };
        }
    }
}