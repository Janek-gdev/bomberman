using System;
using Bomberman.Bombing;
using Bomberman.Collisions;
using Bomberman.Player;
using Bomberman.Utility;
using UnityEngine;

namespace Bomberman.Enemies
{
    public class EnemyView : MonoBehaviour, IBombable
    {
        [SerializeField] private EnemyBrain _enemyBrain;
        [SerializeField] private PlayerCollisionDetector _playerCollisionDetector;
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationEventListener _animationEventListener;
        private static readonly int Explode = Animator.StringToHash("Explode");
        public event Action<EnemyView> OnEnemyDestroyed;

        private void OnEnable()
        {
            _playerCollisionDetector.OnDetectedPlayerChanged += OnPlayerDetected;
            _animationEventListener.OnAnimationEventFired += OnAnimationComplete;
        }
        
        private void OnDisable()
        {
            _playerCollisionDetector.OnDetectedPlayerChanged -= OnPlayerDetected;
            _animationEventListener.OnAnimationEventFired -= OnAnimationComplete;
        }

        public void GetBombed()
        {
            _playerCollisionDetector.OnDetectedPlayerChanged -= OnPlayerDetected;
            _animator.SetTrigger(Explode);
            _enemyBrain.enabled = false;
        }

        private void OnAnimationComplete()
        {
            OnEnemyDestroyed?.Invoke(this);
            Destroy(gameObject);
        }

        private void OnPlayerDetected(GameObject obj)
        {
            if (_playerCollisionDetector.DetectedPlayer != null)
            {
                var playerDeath = _playerCollisionDetector.DetectedPlayer.GetComponent<PlayerDeathView>();
                playerDeath.Kill();
            }
        }
    }
}