using System.Collections;
using Bomberman.Bombing;
using Bomberman.Utility;
using UnityEngine;

namespace Bomberman.Player
{
    /// <summary>
    /// Handles triggering the player death flow
    /// </summary>
    public class PlayerDeathView : MonoBehaviour, IBombable
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private PlayerPowerUpModel _playerPowerUpModel;
        private static readonly int IsDead = Animator.StringToHash("IsDead");

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

        private void OnPlayerAliveChanged(bool _)
        {
            if (!_playerModel.IsAlive)
            {
                StartCoroutine(PlayerDeath());
            }
            else
            {
                _animator.speed = 1f;
                _animator.SetBool(IsDead, false);
            }
        }

        private void SetPlayerAlive()
        {
            _playerModel.IsAlive = true;
        }

        public void Kill()
        {
            _playerModel.IsAlive = false;
        }

        private IEnumerator PlayerDeath()
        {
            _animator.speed = 1f;
            _animator.SetBool(IsDead, true);
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