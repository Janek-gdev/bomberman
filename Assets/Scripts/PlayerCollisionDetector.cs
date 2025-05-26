using System;
using Bomberman.Player;
using UnityEngine;

namespace Bomberman.Collisions
{
    public class PlayerCollisionDetector : MonoBehaviour
    {
        [SerializeField] private float _detectionRange;
        [SerializeField] private LayerMask _playerLayer;
        private readonly RaycastHit2D[] _raycastResults = new RaycastHit2D[10];
        
        public event Action<GameObject> OnDetectedPlayerChanged;
        private GameObject _detectedPlayer;
        public GameObject DetectedPlayer
        {
            get => _detectedPlayer;
            set
            {
                if (value != _detectedPlayer)
                {
                    _detectedPlayer = value;
                    OnDetectedPlayerChanged?.Invoke(_detectedPlayer);
                }
            }
        }

        public bool ShouldDetectPlayer { get; set; } = true;
        private void Update()
        {
            if (ShouldDetectPlayer)
            {
                var hits = Physics2D.CircleCastNonAlloc(transform.position, _detectionRange, Vector2.zero, _raycastResults, 0, _playerLayer);
                if (hits == 0)
                {
                    DetectedPlayer = null;
                }
                for (int i = 0; i < hits; i++)
                {
                    var playerMovementView = _raycastResults[i].transform.GetComponent<PlayerDeathView>();
                    if (playerMovementView != null)
                    {
                        DetectedPlayer = playerMovementView.gameObject;
                    }
                }
            }
        }
    }
}