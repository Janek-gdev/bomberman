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

        private PowerUp _powerUp;


        public PowerUp PowerUp
        {
            get => _powerUp;
            set
            {
                _powerUp = value;
                if (_powerUp != PowerUp.None)
                {
                    OnDestroyed += SpawnPowerUp;
                }
                else
                {
                    OnDestroyed -= SpawnPowerUp;
                }
            }
        }

        private bool _isExit;
        public bool IsExit
        {
            get => _isExit;
            set
            {
                _isExit = value;
                if (_isExit)
                {
                    OnDestroyed += SpawnExit;
                }
                else
                {
                    OnDestroyed -= SpawnExit;
                }
            }
        }

        public bool IsBlocked { get; set; }

        private void SpawnExit(WalkableTileModel _)
        {
            //todo
        }

        private void SpawnPowerUp(WalkableTileModel _)
        {
            //todo
        }


        public void Reset()
        {
            _isExit = false;
            _powerUp = PowerUp.None;
            IsBlocked = false;
        }
    }
}