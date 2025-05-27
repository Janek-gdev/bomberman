using System.Linq;
using Bomberman.Utility;
using UnityEngine;

namespace Bomberman.Enemies
{
    public class WanderingEnemyBrain : EnemyBrain
    {
        private void LateUpdate()
        {
            if (_tileMovable.IsAtDestination())
            {
                foreach (var directionData in _directionDataCollection)
                {
                    directionData.IsValid = _collisionDetector.IsDirectionWalkable(directionData.Direction);
                }

                var validDirections = _directionDataCollection.Where(x => x.IsValid).ToList();
                if (validDirections.Count == 0)
                {
                    return;
                }
                var randomDirection = validDirections[Random.Range(0, validDirections.Count)];
                _tileMovable.TargetPosition = transform.position + (Vector3)DirectionUtility.GetDirectionVector(randomDirection.Direction);
            }
        }
    }
}