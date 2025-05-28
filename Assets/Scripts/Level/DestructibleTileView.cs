using System;
using Bomberman.Bombing;
using Bomberman.Player;
using Bomberman.Utility;
using UnityEngine;

namespace Bomberman.Level
{
    public class DestructibleTileView : MonoBehaviour, IBombable
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationEventListener _animationEventListener;
        public event Action<DestructibleTileView> OnTileDestroyed; 
        public WalkableTileModel TileModel;
        private static readonly int Explode = Animator.StringToHash("Explode");

        private void OnEnable()
        {
            _animationEventListener.OnAnimationEventFired += OnAnimationComplete;
        }

        private void OnDisable()
        {
            _animationEventListener.OnAnimationEventFired -= OnAnimationComplete;
        }

        public void GetBombed()
        {
            _animator.SetTrigger(Explode);
        }

        public void OnAnimationComplete()
        {
            TileModel.IsBlocked = false;
            OnTileDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}