using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bomberman.Level
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private LevelModel _levelModel;
        private const int PowerUpIndex = 0;
        private const int ExitIndex = 1;
        private void OnEnable()
        {
            SpawnPlayer();
            GenerateLevel();
        }
        
        private void SpawnPlayer()
        {
            Instantiate(_levelModel.Prefabs.PlayerRig, Vector2.zero, Quaternion.identity);
        }

        private void GenerateLevel()
        {
            var walkableTiles = LevelLayoutGeneratorModel.instance.WalkableTiles;
            
            Assert.IsTrue(_levelModel.DestructibleTileCount < walkableTiles.Count);

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
            //Remove top left corner so we can spawn the player there
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
                _levelModel.SpawnedDestructibleTiles.Add(destructibleTile);
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
                    _levelModel.SpawnedEnemies.Add(spawnedEnemy);
                }

                freeTileIndex += enemyCounter.Amount;
            }
        }

        private void SpawnExit()
        {
            _levelModel.SpawnedDestructibleTiles[ExitIndex].TileModel.IsExit = true;
            Debug.Log($"Exit spawned on {_levelModel.SpawnedDestructibleTiles[ExitIndex].name}");
        }

        private void SpawnPowerUp()
        {
            _levelModel.SpawnedDestructibleTiles[PowerUpIndex].TileModel.PowerUp = _levelModel.AvailablePowerUp;
            Debug.Log($"Power up spawned on {_levelModel.SpawnedDestructibleTiles[PowerUpIndex].name}");
        }
    }
}