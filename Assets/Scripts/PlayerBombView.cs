using System.Collections.Generic;
using Bomberman.Bombing;
using Bomberman.Player;
using Bomberman.Pooling;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Bomberman.Player
{
    public class PlayerBombView : MonoBehaviour
    {
        [SerializeField] private InputActionReference _layBomb;
        [SerializeField] private BombPool _bombPool;
        [SerializeField] private BombModel _bombModel;
        [SerializeField] private PlayerModel _playerModel;

        private List<BombView> _laidBombs = new();
        
        private void OnEnable()
        {
            _layBomb.action.performed += OnLayBombPerformed;
            GameEvents.instance.OnLevelReload += RemoveBombs;
        }

        private void OnDisable()
        {
            _layBomb.action.performed -= OnLayBombPerformed;
        }

        private void RemoveBombs()
        {
            foreach (var laidBomb in _laidBombs)
            {
                Destroy(laidBomb);
            }

            _laidBombs = new List<BombView>();
        }

        private void OnLayBombPerformed(InputAction.CallbackContext obj)
        {
            if (_laidBombs.Count >= _playerModel.MaxMaxBombAllowance || _playerModel.CurrentTile.IsBlocked || !_playerModel.CanBeControlled)
            {
                return;
            }

            var bomb = _bombPool.Spawn(_playerModel.CurrentTile.Position, Quaternion.identity);
            bomb.Initialize(_playerModel.CurrentTile, _bombModel);
            bomb.OnBombExploded += OnBombExploded;
            _laidBombs.Add(bomb);
            
        }

        private void OnBombExploded(BombView explodedBomb)
        {
            _laidBombs.Remove(explodedBomb);
        }
    }
}