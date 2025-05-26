using Bomberman.Collisions;
using Bomberman.Utility;
using UnityEngine;

namespace Bomberman.Level
{
    public class ExitDoor : MonoBehaviour
    {
        [SerializeField] private PlayerTileDetector _playerTileDetector;
        private LevelModel _levelModel;
        private void OnEnable()
        {
            _playerTileDetector.OnPlayerIsOnTileChanged += OnPlayerEntersTile;
        }

        private void OnDisable()
        {
            _playerTileDetector.OnPlayerIsOnTileChanged -= OnPlayerEntersTile;
        }

        public void Initialize(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        private void OnPlayerEntersTile(bool obj)
        {
            if (_levelModel.SpawnedEnemies.Count == 0)
            {
                GameEvents.instance.OnLevelComplete?.Invoke();
            }
        }
    }
}