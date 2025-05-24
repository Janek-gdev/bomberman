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
        private void Awake()
        {
            GenerateLevel();
        }

        private void GenerateLevel()
        {
            var freeCoords = LevelLayoutGeneratorModel.instance.FreeCoords;
            
            Assert.IsTrue(_levelModel.DestructibleTileCount < freeCoords.Count);

            var shuffledFreeCoords = GetShuffledFreeCoords(freeCoords);

            SpawnDestructibleTiles(shuffledFreeCoords);

            //todo could iterate backwards over shuffled free coords and remove from list, instead of passing starting index
            SpawnEnemies(shuffledFreeCoords, _levelModel.DestructibleTileCount);

            SpawnPowerUp();
            SpawnExit();
        }

        private void SpawnExit()
        {
            _levelModel.SpawnedDestructibleTiles[ExitIndex].IsExit = true;
        }

        private void SpawnPowerUp()
        {
            _levelModel.SpawnedDestructibleTiles[PowerUpIndex].PowerUp = _levelModel.AvailablePowerUp;
        }

        private void SpawnDestructibleTiles(List<Vector2> shuffledFreeCoords)
        {
            for (var i = 0; i < _levelModel.DestructibleTileCount; i++)
            {
                var destructibleTile = Instantiate(_levelModel.Prefabs.DestructiblePrefab, shuffledFreeCoords[i],
                    Quaternion.identity, transform);
                _levelModel.SpawnedDestructibleTiles.Add(destructibleTile);
                destructibleTile.OnDestroyed += OnTileDestroyed;
            }
        }

        private void SpawnEnemies(List<Vector2> shuffledFreeCoords, int freeTileStartingIndex)
        {
            var freeTileIndex = freeTileStartingIndex;
            foreach (var enemyCounter in _levelModel.Enemies)
            {
                for (int i = 0; i < freeTileIndex; i++)
                {
                    var spawnedEnemy = Instantiate(_levelModel.Prefabs.EnemyPrefabs.First(x => x._enemyModel == enemyCounter._enemyModel).Prefab, shuffledFreeCoords[i],
                        Quaternion.identity, transform);
                    _levelModel.SpawnedEnemies.Add(spawnedEnemy);
                }

                freeTileIndex += enemyCounter.Amount;
            }
        }

        private void OnTileDestroyed(DestructibleTile destroyedTile)
        {
            _levelModel.SpawnedDestructibleTiles.Remove(destroyedTile);
            destroyedTile.OnDestroyed -= OnTileDestroyed;
        }

        private static List<Vector2> GetShuffledFreeCoords(List<Vector2> freeCoords)
        {
            var shuffledFreeCoords = new List<Vector2>(freeCoords);
            for (int i = 0; i < shuffledFreeCoords.Count; i++)
            {
                var swapIndex = Random.Range(i, shuffledFreeCoords.Count);
                (shuffledFreeCoords[i], shuffledFreeCoords[swapIndex]) = (shuffledFreeCoords[swapIndex], shuffledFreeCoords[i]);
            }

            return shuffledFreeCoords;
        }
    }
}