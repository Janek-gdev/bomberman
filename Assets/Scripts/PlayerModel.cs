using System;
using Bomberman.Level;
using UnityEngine;

namespace Bomberman.Player
{
    [CreateAssetMenu(menuName = MenuName.Player + nameof(PlayerModel), fileName = nameof(PlayerModel))]
    public class PlayerModel : ResettableScriptableObject
    {
        public bool CanBeControlled { get; set; }
        
        [SerializeField] private float _moveSpeed = 1;

        public float MoveSpeed => _moveSpeed;

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

        public int MaxMaxBombAllowance
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
            MaxMaxBombAllowance = _baseMaxBombAllowance;
            _currentTile = null;
            CanBeControlled = true;
        }
        
    }
}