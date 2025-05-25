using UnityEngine;

namespace Bomberman.Player
{
    public class Movable : MonoBehaviour
    {
        public Vector2 TargetPosition { get; set; }
        public bool IsStationary { get; private set; }
        public float MoveSpeed { get; set; }
        
        private void Update()
        {
            transform.position =
                Vector2.MoveTowards(transform.position, TargetPosition, Time.deltaTime * MoveSpeed);

            IsStationary = IsAtDestination();
        }

        public bool IsAtDestination()
        {
            return (transform.position - (Vector3)TargetPosition).sqrMagnitude < 0.0001f;
        }
    }
}