using System;
using System.Collections;
using Bomberman.Utility;
using UnityEngine;

namespace Bomberman.Player
{
    public class PlayerDeathView : MonoBehaviour, IBombable
    {
        [SerializeField] private PlayerModel _playerModel;

        private void OnEnable()
        {
            _playerModel.OnIsAliveChanged += OnPlayerAliveChanged;
        }

        private void OnDisable()
        {
            _playerModel.OnIsAliveChanged -= OnPlayerAliveChanged;
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
            GameEvents.instance.OnPlayerDeath?.Invoke();
            GameEvents.instance.OnLevelReloadBegin?.Invoke();
            GameEvents.instance.OnLevelReloadEnd?.Invoke();
            _playerModel.IsAlive = true;
        }

        public void GetBombed()
        {
            Kill();
        }
    }
}