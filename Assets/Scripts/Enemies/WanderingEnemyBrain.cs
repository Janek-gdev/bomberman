using System.Linq;
using Bomberman.Utility;
using UnityEngine;

namespace Bomberman.Enemies
{
    /// <summary>
    /// Wanders without direction around the level
    /// Used in sync with <see cref="Collisions.DirectionalCollisionDetector"/> to determine wanderable tiles
    /// </summary>
    public class WanderingEnemyBrain : EnemyBrain
    {
        private void LateUpdate()
        {
            if (_tileMovable.IsAtDestination())
            {
                foreach (var directionData in _directionDataCollection)
                {
                    directionData.TileIsClear = _directionalCollisionDetector.IsDirectionWalkable(directionData.Direction);
                }

                var validDirections = _directionDataCollection.Where(x => x.TileIsClear).ToList();
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