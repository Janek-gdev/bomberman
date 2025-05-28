using Bomberman.Collisions;
using Bomberman.Utility;
using UnityEngine;

namespace Bomberman.Level
{
    public class ExitDoor : MonoBehaviour
    {
        [SerializeField] private PlayerTileDetector _playerTileDetector;
        private LevelModel _levelModel;
        private WalkableTileModel _walkableTileModel;
        private void OnEnable()
        {
            _playerTileDetector.OnPlayerIsOnTileChanged += OnPlayerEntersTile;
        }

        private void OnDisable()
        {
            _playerTileDetector.OnPlayerIsOnTileChanged -= OnPlayerEntersTile;
        }

        public void Initialize(LevelModel levelModel, WalkableTileModel walkableTileModel)
        {
            _levelModel = levelModel;
            _walkableTileModel = walkableTileModel;
            _walkableTileModel.IsBlocked = true;
        }

        private void OnPlayerEntersTile(bool obj)
        {
            if (_levelModel.SpawnedEnemies.Count == 0)
            {
                GameEvent.OnLevelComplete?.Invoke();
                _walkableTileModel.IsBlocked = false;
            }
        }
    }
}