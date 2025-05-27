using System;
using System.Collections;
using Bomberman.Utility;
using UnityEngine;

namespace Bomberman.Player
{
    public class PlayerDeathView : MonoBehaviour, IBombable
    {
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private PlayerPowerUpModel _playerPowerUpModel;

        private void OnEnable()
        {
            _playerModel.OnIsAliveChanged += OnPlayerAliveChanged;
            GameEvents.instance.OnLevelSetupComplete += SetPlayerAlive;
        }

        private void OnDisable()
        {
            _playerModel.OnIsAliveChanged -= OnPlayerAliveChanged;
            GameEvents.instance.OnLevelSetupComplete -= SetPlayerAlive;
        }

        private void SetPlayerAlive()
        {
            _playerModel.IsAlive = true;
        }

        private void OnPlayerAliveChanged(bool _)
        {
            if (!_playerModel.IsAlive)
            {
                StartCoroutine(PlayerDeath());
            }
        }

        public void Kill()
        {
            _playerModel.IsAlive = false;
        }

        private IEnumerator PlayerDeath()
        {
            //todo animation event
            yield return new WaitForSeconds(0.3f);
            _playerPowerUpModel.RemoveAllPowerUps();
            GameEvents.instance.OnPlayerDeath?.Invoke();
        }

        public void GetBombed()
        {
            Kill();
        }
    }
}