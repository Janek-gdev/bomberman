using System;
using Bomberman.Level;
using Bomberman.Player;
using UnityEngine;

namespace Bomberman.Collisions
{
    /// <summary>
    /// Detects if the player is on the same tile is this gameobject
    /// </summary>
    public class PlayerTileDetector : MonoBehaviour
    {
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private LevelLayoutGeneratorModel _levelLayoutGeneratorModel;

        private WalkableTileModel _tile;
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

        private void Awake()
        {
            _tile = _levelLayoutGeneratorModel.GetClosestFreeTile(transform);
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