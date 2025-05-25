using System;
using System.Collections.Generic;
using Bomberman.Utility;
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
            return IsDirectionWalkable(DirectionUtility.GetDirectionVector(direction));
        }

        public bool IsDirectionWalkable(Vector2 direction)
        {
            var hits = Physics2D.RaycastNonAlloc(transform.position, direction, _raycastResults, _raycastDistance, _nonWalkableLayerMask);
            return hits == 0;
        }

        
    }
}