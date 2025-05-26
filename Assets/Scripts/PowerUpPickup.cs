using Bomberman.Collisions;
using Bomberman.Player;
using UnityEngine;

namespace Bomberman.Level
{
    public class PowerUpPickup : MonoBehaviour
    {
        [SerializeField] private PlayerPowerUpModel _playerPowerUp;
        [SerializeField] private PlayerTileDetector _playerTileDetector;
        private PowerUp _powerUp;
        private void OnEnable()
        {
            _playerTileDetector.OnPlayerIsOnTileChanged += OnPlayerEntersTile;
        }

        private void OnDisable()
        {
            _playerTileDetector.OnPlayerIsOnTileChanged -= OnPlayerEntersTile;
        }

        public void Initialize(PowerUp powerUp)
        {
            _powerUp = powerUp;
        }

        private void OnPlayerEntersTile(bool obj)
        {
            _playerPowerUp.Add(_powerUp);
            Destroy(gameObject);
        }
    }
}