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
        [SerializeField] private PlayerModel _playerModel;
        private readonly List<BombView> _laidBombs = new();
        private void OnEnable()
        {
            _layBomb.action.performed += OnLayBombPerformed;
        }

        private void OnDisable()
        {
            _layBomb.action.performed -= OnLayBombPerformed;
        }

        private void OnLayBombPerformed(InputAction.CallbackContext obj)
        {
            if (_laidBombs.Count >= _playerModel.MaxMaxBombAllowance || _playerModel.CurrentTile.IsBlocked)
            {
                return;
            }

            var bomb = _bombPool.Spawn(_playerModel.CurrentTile.Position, Quaternion.identity);
            bomb.Initialize(_playerModel.CurrentTile);
            _laidBombs.Add(bomb);
            
        }
    }
}