using System;
using System.Collections.Generic;
using Bomberman.Bombing;
using Bomberman.PowerUps;
using UnityEngine;

namespace Bomberman.Player
{
    /// <summary>
    /// Holds the players current power ups, the affect of the power ups, and how they're applied to <see cref="PlayerModel"/>
    /// </summary>
    [CreateAssetMenu(menuName = ScriptableObjectMenuName.Player + nameof(PlayerPowerUpModel), fileName = nameof(PlayerPowerUpModel))]
    public class PlayerPowerUpModel : ResettableScriptableObject
    {
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private BombModel _playerBombModel;
        [SerializeField] private float _moveSpeedIncrementPerPowerUp = 1f;
        [SerializeField] private int _bombLayer;
        [SerializeField] private int _destructibleLayer;
        [SerializeField] private int _playerLayer;

        private List<PowerUp> _powerUps;
        public IReadOnlyCollection<PowerUp> PowerUps => _powerUps.AsReadOnly();

        [ContextMenu("Give Bomb Up")] public void GiveBombUp() => Add(PowerUp.BombUp);
        [ContextMenu("Give Range Up")] public void GiveRangeUp() => Add(PowerUp.RangeUp);
        [ContextMenu("Give Speed Up")] public void GiveSpeedUp() => Add(PowerUp.SpeedUp);
        [ContextMenu("Give Wall Pass")] public void GiveWallPass() => Add(PowerUp.WallPass);
        [ContextMenu("Give Bomb Pass")] public void GiveBombPass() => Add(PowerUp.BombPass);
        [ContextMenu("Give Fireproof")] public void GiveFireproof() => Add(PowerUp.Fireproof);

        public void Add(PowerUp powerUp)
        {
            _powerUps.Add(powerUp);
            switch (powerUp)
            {
                case PowerUp.None:
                    break;
                case PowerUp.BombUp:
                    _playerModel.MaxBombAllowance++;
                    break;
                case PowerUp.RangeUp:
                    _playerBombModel.ExplosionRange++;
                    break;
                case PowerUp.SpeedUp:
                    _playerModel.MoveSpeed += _moveSpeedIncrementPerPowerUp;
                    break;
                case PowerUp.WallPass:
                    _playerModel.NonWalkableLayerMask &= ~(1 << _destructibleLayer);
                    break;
                case PowerUp.BombPass:
                    _playerModel.NonWalkableLayerMask &= ~(1 << _bombLayer);
                    break;
                case PowerUp.Fireproof:
                    _playerBombModel.BombableLayerMask &= ~(1 << _playerLayer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(powerUp), powerUp, null);
            }
        }

        protected override void ResetAfterPlayInEditor()
        {
            base.ResetAfterPlayInEditor();
            _powerUps = new List<PowerUp>();
        }

        public void RemoveAllPowerUps()
        {
            foreach (var powerUp in _powerUps)
            {
                RemovePowerUp(powerUp);
            }

            _powerUps = new List<PowerUp>();
        }

        private void RemovePowerUp(PowerUp powerUp)
        {
            switch (powerUp)
            {
                case PowerUp.None:
                    break;
                case PowerUp.BombUp:
                    _playerModel.MaxBombAllowance--;
                    break;
                case PowerUp.RangeUp:
                    _playerBombModel.ExplosionRange--;
                    break;
                case PowerUp.SpeedUp:
                    _playerModel.MoveSpeed -= _moveSpeedIncrementPerPowerUp;
                    break;
                case PowerUp.WallPass:
                    _playerModel.NonWalkableLayerMask |= (1 << _destructibleLayer);
                    break;
                case PowerUp.BombPass:
                    _playerModel.NonWalkableLayerMask |= (1 << _bombLayer);
                    break;
                case PowerUp.Fireproof:
                    _playerBombModel.BombableLayerMask |= (1 << _playerLayer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(powerUp), powerUp, null);
            }
        }
    }
}