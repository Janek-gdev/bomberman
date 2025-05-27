using System;
using Bomberman.Level;
using Bomberman.Player;
using UnityEngine;

namespace Bomberman.Collisions
{
    public class PlayerTileDetector : MonoBehaviour
    {
        public event Action<bool> OnPlayerIsOnTileChanged;
        private bool _playerIsOnTile;
        public bool PlayerIsOnTile
        {
            get => _playerIsOnTile;
            set
            {
                if (value != _playerIsOnTile)
                {
                    _playerIsOnTile = value;
                    OnPlayerIsOnTileChanged?.Invoke(_playerIsOnTile);
                }
            }
        }
        [SerializeField] private PlayerModel _playerModel;
        private WalkableTileModel _tile;

        private void Awake()
        {
            _tile = LevelLayoutGeneratorModel.instance.GetClosestFreeTile(transform);
        }

        private void OnEnable()
        {
            OnPlayerTileChanged(_playerModel.CurrentTile);
            _playerModel.OnCurrentTileChanged += OnPlayerTileChanged;
        }

        private void OnDisable()
        {
            _playerModel.OnCurrentTileChanged -= OnPlayerTileChanged;
        }

        private void OnPlayerTileChanged(WalkableTileModel _)
        {
            PlayerIsOnTile = _playerModel.CurrentTile == _tile;
        }
    }
}