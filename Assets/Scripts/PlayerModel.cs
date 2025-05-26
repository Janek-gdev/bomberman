using System;
using Bomberman.Level;
using UnityEngine;

namespace Bomberman.Player
{
    [CreateAssetMenu(menuName = MenuName.Player + nameof(PlayerModel), fileName = nameof(PlayerModel))]
    public class PlayerModel : ResettableScriptableObject
    {
        public event Action<LayerMask> OnNonWalkableLayerMaskChanged;
        private LayerMask _nonWalkableLayerMask;
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
        [SerializeField] private bool _isAlive;
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
        
        [SerializeField] private float _baseMoveSpeed = 4f;
        private float _moveSpeed;

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

        [SerializeField] private float _gridCorrectionSpeed;
        public float GridCorrectionSpeed => _gridCorrectionSpeed;
        
        public event Action<WalkableTileModel> OnCurrentTileChanged;
        private WalkableTileModel _currentTile;
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
        
        [SerializeField] private int _baseMaxBombAllowance;
        private int _maxBombAllowance;

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