using System.Collections.Generic;
using Bomberman.Bombing;
using Bomberman.Pooling;
using Bomberman.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Bomberman.Player
{
    /// <summary>
    /// Handles the spawning of bombs for the player and passing the relevant data to them
    /// </summary>
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
            GameEvents.instance.OnLevelTeardown += RemoveBombs;
        }

        private void OnDisable()
        {
            _layBomb.action.performed -= OnLayBombPerformed;
            GameEvents.instance.OnLevelTeardown -= RemoveBombs;
        }

        private void RemoveBombs()
        {
            foreach (var laidBomb in _laidBombs)
            {
                Destroy(laidBomb.gameObject);
            }

            _laidBombs = new List<BombView>();
        }

        private void OnLayBombPerformed(InputAction.CallbackContext obj)
        {
            if (_laidBombs.Count >= _playerModel.MaxBombAllowance || _playerModel.CurrentTile.IsBlocked || !_playerModel.IsAlive)
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