using UnityEngine;

namespace Bomberman.Player
{
    /// <summary>
    /// Handles moving between tiles smoothly
    /// </summary>
    public class TileMovable : MonoBehaviour
    {
        public Vector2 TargetPosition { get; set; } 

        public float MoveSpeed { get; set; }

        private void Awake()
        {
            TargetPosition = transform.position;
        }

        private void Update()
        {
            transform.position =
                Vector2.MoveTowards(transform.position, TargetPosition, Time.deltaTime * MoveSpeed);
        }

        public bool IsAtDestination()
        {
            return (transform.position - (Vector3)TargetPosition).sqrMagnitude < 0.0001f;
        }
    }
}