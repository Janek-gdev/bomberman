using Bomberman.Collisions;
using Bomberman.Player;
using UnityEngine;

namespace Bomberman.Enemies
{
    public abstract class EnemyBrain : MonoBehaviour
    {
        protected class DirectionData
        {
            public bool IsValid;
            public Direction Direction;

            public DirectionData(bool isValid, Direction direction)
            {
                IsValid = isValid;
                Direction = direction;
            }
        }
        [SerializeField] protected EnemyModel _enemyModel;
        [SerializeField] protected CollisionDetector _collisionDetector;
        [SerializeField] protected TileMovable _tileMovable;
        protected readonly DirectionData[] _directionDataCollection = new DirectionData[4];
        protected virtual void Awake()
        {
            _directionDataCollection[0] = new DirectionData(false, Direction.Left);
            _directionDataCollection[1] = new DirectionData(false, Direction.Right);
            _directionDataCollection[2] = new DirectionData(false, Direction.Up);
            _directionDataCollection[3] = new DirectionData(false, Direction.Down);
            _tileMovable.MoveSpeed = _enemyModel.MoveSpeed;
        }
    }
}