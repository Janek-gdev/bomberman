using UnityEngine;

namespace Bomberman.Player
{
    public abstract class Movable : MonoBehaviour
    {
        protected Vector2 _targetPosition;
        protected bool _isStationary;
        protected abstract float MoveSpeed { get; }
        
        protected virtual void Update()
        {
            transform.position =
                Vector2.MoveTowards(transform.position, _targetPosition, Time.deltaTime * MoveSpeed);

            _isStationary = IsAtDestination();
        }

        protected bool IsAtDestination()
        {
            return (transform.position - (Vector3)_targetPosition).sqrMagnitude < 0.0001f;
        }
    }
}