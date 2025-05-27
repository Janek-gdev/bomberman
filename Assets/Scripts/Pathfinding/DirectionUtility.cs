using UnityEngine;

namespace Bomberman.Utility
{
    public static class DirectionUtility
    {
        public static Vector2 GetDirectionVector(Direction direction)
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
        
        public static Quaternion GetRotationFromDirection(Direction direction)
        {
            return direction switch
            {
                Direction.Up => Quaternion.Euler(0f, 0f, 0f),
                Direction.Right => Quaternion.Euler(0f, 0f, -90f),
                Direction.Down => Quaternion.Euler(0f, 0f, 180f),
                Direction.Left => Quaternion.Euler(0f, 0f, 90f),
                _ => Quaternion.identity
            };
        }
    }
}