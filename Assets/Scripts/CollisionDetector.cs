using System;
using UnityEngine;

namespace Bomberman.Collisions
{
    public class CollisionDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask _walkableLayerMask;
        private readonly RaycastHit2D[] _results = new RaycastHit2D[10];

        public bool IsDirectionWalkable(Direction direction)
        {
            return IsDirectionWalkable(GetDirectionVector(direction));
        }

        public bool IsDirectionWalkable(Vector2 direction)
        {
            var hits = Physics2D.RaycastNonAlloc(transform.position, direction, _results, 1, _walkableLayerMask);
            return hits == 0;
        }

        private Vector2 GetDirectionVector(Direction direction)
        {
            return direction switch
            {
                Direction.Left => Vector2.left,
                Direction.Right => Vector2.right,
                Direction.Up => Vector2.up,
                Direction.Down => Vector2.down,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
    }
}