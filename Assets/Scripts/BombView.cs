using System;
using Bomberman.Level;
using UnityEngine;

namespace Bomberman.Bombing
{
    public class BombView : MonoBehaviour
    {
        public event Action<BombView> OnBombExploded;
        [SerializeField] private Animator _animator;

        public WalkableTileModel Tile { get; private set; }
        public void Initialize(WalkableTileModel tileModel)
        {
            Tile = tileModel;
            Tile.IsBlocked = true;
        }

        private void Explode()
        {
            Tile.IsBlocked = false;
        }
    }
}