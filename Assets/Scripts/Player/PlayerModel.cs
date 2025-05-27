using System;
using Bomberman.Level;
using UnityEngine;

namespace Bomberman.Player
{
    /// <summary>
    /// Handles the base and modified data for the player, works closely with <see cref="PlayerPowerUpModel"/>
    /// </summary>
    [CreateAssetMenu(menuName = ScriptableObjectMenuName.Player + nameof(PlayerModel), fileName = nameof(PlayerModel))]
    public class PlayerModel : ResettableScriptableObject
    {
        [SerializeField] private int _baseMaxBombAllowance;
        [SerializeField] private float _baseMoveSpeed = 4f;
        [SerializeField] private float _gridCorrectionSpeed;
        
        private int _maxBombAllowance;
        private bool _isAlive = true;
        private LayerMask _nonWalkableLayerMask;
        private float _moveSpeed;
        private WalkableTileModel _currentTile;

        public float GridCorrectionSpeed => _gridCorrectionSpeed;

        public float MoveSpeed
        {
            get
            {
                if (_moveSpeed == 0)
                {
                    _moveSpeed = _baseMoveSpeed;
                }
                return _moveSpeed;
            }
            set => _moveSpeed = value;
        }
        public event Action<LayerMask> OnNonWalkableLayerMaskChanged;
        public LayerMask NonWalkableLayerMask
        {
            get => _nonWalkableLayerMask;
            set
            {
                if (value != _nonWalkableLayerMask)
                {
                    _nonWalkableLayerMask = value;
                    OnNonWalkableLayerMaskChanged?.Invoke(_nonWalkableLayerMask);
                }
            }
        }
        
        public event Action<bool> OnIsAliveChanged;
        public bool IsAlive
        {
            get => _isAlive;
            set
            {
                if (value != _isAlive)
                {
                    _isAlive = value;
                    OnIsAliveChanged?.Invoke(_isAlive);
                }
            }
        }
        
        public event Action<WalkableTileModel> OnCurrentTileChanged;
        public WalkableTileModel CurrentTile
        {
            get => _currentTile;
            set
            {
                if (value != _currentTile)
                {
                    Debug.Log($"Tile changed {value}");
                    _currentTile = value;
                    OnCurrentTileChanged?.Invoke(_currentTile);
                }
            }
        }

        public int MaxBombAllowance
        {
            get
            {
                if (_maxBombAllowance == 0)
                {
                    _maxBombAllowance = _baseMaxBombAllowance;
                }
                return _maxBombAllowance;
            }
            set => _maxBombAllowance = value;
        }

        protected override void ResetAfterPlayInEditor()
        {
            base.ResetAfterPlayInEditor();
            MaxBombAllowance = _baseMaxBombAllowance;
            _currentTile = null;
            IsAlive = true;
            _moveSpeed = _baseMoveSpeed;
        }
        
    }
}