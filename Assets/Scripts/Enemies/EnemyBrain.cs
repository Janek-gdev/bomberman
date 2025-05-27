using Bomberman.Collisions;
using Bomberman.Player;
using UnityEngine;

namespace Bomberman.Enemies
{
    /// <summary>
    /// Used for controlling enemy movements
    /// </summary>
    public abstract class EnemyBrain : MonoBehaviour
    {
        protected class CollisionDirectionData
        {
            public bool TileIsClear;
            public Direction Direction;

            public CollisionDirectionData(bool tileIsClear, Direction direction)
            {
                TileIsClear = tileIsClear;
                Direction = direction;
            }
        }
        [SerializeField] protected EnemyModel _enemyModel;
        [SerializeField] protected DirectionalCollisionDetector _directionalCollisionDetector;
        [SerializeField] protected TileMovable _tileMovable;
        protected readonly CollisionDirectionData[] _directionDataCollection = new CollisionDirectionData[4];
        protected virtual void Awake()
        {
            _directionDataCollection[0] = new CollisionDirectionData(false, Direction.Left);
            _directionDataCollection[1] = new CollisionDirectionData(false, Direction.Right);
            _directionDataCollection[2] = new CollisionDirectionData(false, Direction.Up);
            _directionDataCollection[3] = new CollisionDirectionData(false, Direction.Down);
            _tileMovable.MoveSpeed = _enemyModel.MoveSpeed;
        }
    }
}