using System;
using System.Collections.Generic;
using System.Linq;
using Bomberman.Enemies;
using Bomberman.Player;
using Bomberman.Utility;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace Bomberman.Level
{
    public class LevelView : MonoBehaviour
    {
        private LevelModel _levelModel;
        private const int PowerUpIndex = 0;
        private const int ExitIndex = 1;
        private const int PlayerStartingTilesAllocation = 3;
        private GameObject _player;
        private List<GameObject> _additionalSpawnedObjects = new();

        private void OnEnable()
        {
            GameEvents.instance.OnLevelTeardown += TearDownLevel;
        }

        private void OnDisable()
        {
            GameEvents.instance.OnLevelTeardown -= TearDownLevel;
        }

        public void LoadLevel(LevelModel levelModel)
        {
            Debug.Log($"Loading level {levelModel.name}");
            _levelModel = levelModel;
            GenerateLevel();
            if (_player == null)
            {
                SpawnPlayer();
            }
            MovePlayerToStart();
        }

        private void GenerateLevel()
        {
            var walkableTiles = LevelLayoutGeneratorModel.instance.WalkableTiles;
            
            Assert.IsTrue(_levelModel.DestructibleTileCount < walkableTiles.Count - PlayerStartingTilesAllocation);

            var shuffledFreeCoords = GetShuffledTiles(walkableTiles);

            SpawnDestructibleTiles(shuffledFreeCoords);

            //todo could iterate backwards over shuffled free coords and remove from list, instead of passing starting index
            SpawnEnemies(shuffledFreeCoords, _levelModel.DestructibleTileCount);

            SpawnPowerUp();
            SpawnExit();
        }

        private static List<WalkableTileModel> GetShuffledTiles(List<WalkableTileModel> walkableTiles)
        {
            var shuffledTiles = new List<WalkableTileModel>(walkableTiles.Where(x => x != null));
            //Remove bottom right corner so we can spawn the player there
            shuffledTiles.RemoveAll(tile =>
                (tile.Position.x == 0 && tile.Position.y == 0) ||
                (tile.Position.x == 1 && tile.Position.y == 0) ||
                (tile.Position.x == 0 && tile.Position.y == 1));
            
            for (int i = 0; i < shuffledTiles.Count; i++)
            {
                var swapIndex = Random.Range(i, shuffledTiles.Count);
                (shuffledTiles[i], shuffledTiles[swapIndex]) = (shuffledTiles[swapIndex], shuffledTiles[i]);
            }
            return shuffledTiles;
        }

        private void SpawnDestructibleTiles(List<WalkableTileModel> shuffledTiles)
        {
            for (var i = 0; i < _levelModel.DestructibleTileCount; i++)
            {
                var destructibleTile = _levelModel.Prefabs.DestructibleTilePool.Spawn(shuffledTiles[i].Position,
                    Quaternion.identity, transform);
                destructibleTile.name = $"Destructible tile {shuffledTiles[i].Position.x}/{shuffledTiles[i].Position.y}";
                destructibleTile.TileModel = shuffledTiles[i];
                destructibleTile.TileModel.IsBlocked = true;
                destructibleTile.OnTileDestroyed += HandleTileDestruction;
                _levelModel.SpawnedDestructibleTiles.Add(destructibleTile);
            }
        }

        private void HandleTileDestruction(DestructibleTileView destroyedTile)
        {
            destroyedTile.OnTileDestroyed -= HandleTileDestruction;
            _levelModel.SpawnedDestructibleTiles.Remove(destroyedTile);
            if (destroyedTile.TileModel.IsExit)
            {
                Debug.Log("Exit found!");
                var exitDoor = Instantiate(_levelModel.Prefabs.ExitDoor, destroyedTile.TileModel.Position,
                    Quaternion.identity, transform);
                exitDoor.Initialize(_levelModel, destroyedTile.TileModel);
                _additionalSpawnedObjects.Add(exitDoor.gameObject);
            }
            else if (destroyedTile.TileModel.PowerUp != PowerUp.None)
            {
                Debug.Log("Power up found!");
                var powerUpPickup = Instantiate(_levelModel.Prefabs.PowerUpPrefabs.First(x => x.PowerUp == destroyedTile.TileModel.PowerUp).PowerUpPickup, 
                    destroyedTile.TileModel.Position,
                    Quaternion.identity, transform);
                powerUpPickup.Initialize(destroyedTile.TileModel.PowerUp);
                _additionalSpawnedObjects.Add(powerUpPickup.gameObject);
            }
        }

        private void SpawnEnemies(List<WalkableTileModel> shuffledTiles, int freeTileStartingIndex)
        {
            var freeTileIndex = freeTileStartingIndex;
            foreach (var enemyCounter in _levelModel.Enemies)
            {
                for (int i = freeTileIndex; i < freeTileIndex + enemyCounter.Amount; i++)
                {
                    var spawnedEnemy = _levelModel.Prefabs.EnemyPrefabs.First(x => x.EnemyModel == enemyCounter._enemyModel).Pool.Spawn(shuffledTiles[i].Position,
                        Quaternion.identity, transform);
                    spawnedEnemy.OnEnemyDestroyed += HandleEnemyDestruction;
                    _levelModel.SpawnedEnemies.Add(spawnedEnemy);
                }

                freeTileIndex += enemyCounter.Amount;
            }
        }

        private void HandleEnemyDestruction(EnemyView destroyedEnemy)
        {
            destroyedEnemy.OnEnemyDestroyed -= HandleEnemyDestruction;
            _levelModel.SpawnedEnemies.Remove(destroyedEnemy);
        }

        private void SpawnPlayer()
        {
            var playerRig = Instantiate(_levelModel.Prefabs.PlayerRig, Vector2.zero, Quaternion.identity);
            _player = playerRig.GetComponentInChildren<PlayerMovementView>().gameObject;
        }

        private void SpawnPowerUp()
        {
            _levelModel.SpawnedDestructibleTiles[PowerUpIndex].TileModel.PowerUp = _levelModel.AvailablePowerUp;
            Debug.Log($"Power up spawned on {_levelModel.SpawnedDestructibleTiles[PowerUpIndex].name}");
        }

        private void SpawnExit()
        {
            _levelModel.SpawnedDestructibleTiles[ExitIndex].TileModel.IsExit = true;
            Debug.Log($"Exit spawned on {_levelModel.SpawnedDestructibleTiles[ExitIndex].name}");
        }

        private void MovePlayerToStart()
        {
            _player.transform.position = Vector2.zero;
        }

        private void TearDownLevel()
        {
            foreach (var destructibleTileView in _levelModel.SpawnedDestructibleTiles)
            {
                destructibleTileView.TileModel.Reset();
                Destroy(destructibleTileView.gameObject);
            }

            foreach (var spawnedEnemy in _levelModel.SpawnedEnemies)
            {
                Destroy(spawnedEnemy.gameObject);
            }

            foreach (var additionalSpawnedObject in _additionalSpawnedObjects)
            {
                Destroy(additionalSpawnedObject);
            }

            _levelModel.SpawnedDestructibleTiles = new List<DestructibleTileView>();
            _levelModel.SpawnedEnemies = new List<EnemyView>();
            _additionalSpawnedObjects = new List<GameObject>();
        }
    }
}