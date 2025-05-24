using System;
using Bomberman.Player;
using UnityEngine;

namespace Bomberman.Level
{
    public class DestructibleTile : MonoBehaviour
    {
        public event Action<DestructibleTile> OnDestroyed;

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

        private void SpawnExit(DestructibleTile _)
        {
            //todo
        }

        private void SpawnPowerUp(DestructibleTile _)
        {
            //todo
        }
    }
}