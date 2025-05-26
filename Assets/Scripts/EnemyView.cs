using System;
using Bomberman.Collisions;
using Bomberman.Player;
using UnityEngine;

namespace Bomberman.Enemies
{
    public class EnemyView : MonoBehaviour, IBombable
    {
        [SerializeField] private PlayerCollisionDetector _playerCollisionDetector;
        public event Action<EnemyView> OnEnemyDestroyed;

        private void OnEnable()
        {
            _playerCollisionDetector.OnDetectedPlayerChanged += OnPlayerDetected;
        }
        
        private void OnDisable()
        {
            _playerCollisionDetector.OnDetectedPlayerChanged -= OnPlayerDetected;
        }

        private void OnPlayerDetected(GameObject obj)
        {
            if (_playerCollisionDetector.DetectedPlayer != null)
            {
                var playerDeath = _playerCollisionDetector.DetectedPlayer.GetComponent<PlayerDeathView>();
                playerDeath.Kill();
            }
        }
        
        public void GetBombed()
        {
            OnEnemyDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}