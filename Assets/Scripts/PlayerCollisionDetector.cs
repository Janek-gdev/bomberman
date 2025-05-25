using System;
using UnityEngine;

namespace Bomberman.Collisions
{
    public class PlayerCollisionDetector : MonoBehaviour
    {
        [SerializeField] private float _detectionRange;
        [SerializeField] private LayerMask _playerLayer = LayerMask.NameToLayer("Player");

        private void Update()
        {
            throw new NotImplementedException();
        }
    }
}