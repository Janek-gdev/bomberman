using System;
using Bomberman.Player;
using UnityEngine;

namespace Bomberman.Level
{
    public class WalkableTileModel
    {
        public Action<WalkableTileModel> OnDestroyed;
        public Vector2 Position { get; private set; }


        public WalkableTileModel(Vector2 position)
        {
            Position = position;
        }

        public PowerUp PowerUp { get; set; }

        public bool IsExit { get; set; }
        public bool IsBlocked { get; set; }

        public void Reset()
        {
            IsExit = false;
            PowerUp = PowerUp.None;
            IsBlocked = false;
        }
    }
}