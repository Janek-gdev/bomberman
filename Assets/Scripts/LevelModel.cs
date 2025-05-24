using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace Bomberman.Level
{
    public class EnemyType : ScriptableObject
    {
        
    }

    public class EnemyView : MonoBehaviour
    {
        
    }
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
                    var spawnedEnemy = Instantiate(_levelModel.Prefabs.EnemyPrefabs.First(x => x.EnemyType == enemyCounter.EnemyType).Prefab, shuffledFreeCoords[i],
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
    
    public class LevelModel : ResettableScriptableObject
    {
        [Serializable]
        public class EnemyCounter
        {
            public EnemyType EnemyType;
            public int Amount;
        }
        
        [SerializeField] private List<EnemyCounter> _enemies;
        [SerializeField] private int _destructibleTileCount;
        [SerializeField] private PowerUp _availablePowerUp;
        [SerializeField] private LevelPrefabsModel _levelPrefabsModel;

        //todo move these to own models?
        public List<EnemyView> SpawnedEnemies { get; set; }
        public List<DestructibleTile> SpawnedDestructibleTiles { get; set; }

        public List<EnemyCounter> Enemies => _enemies;
        public int DestructibleTileCount => _destructibleTileCount;
        public PowerUp AvailablePowerUp => _availablePowerUp;
        public LevelPrefabsModel Prefabs => _levelPrefabsModel;
    }

    public class LevelPrefabsModel : ScriptableObject
    {
        [Serializable]
        public class EnemyPrefab
        {
            public EnemyType EnemyType;
            public EnemyView Prefab;
        }
        
        [SerializeField] private DestructibleTile _destructiblePrefab;
        [SerializeField] private List<EnemyPrefab> _enemyPrefabs;
        [SerializeField] private GameObject _exitDoor;

        public GameObject ExitDoor => _exitDoor;
        public List<EnemyPrefab> EnemyPrefabs => _enemyPrefabs;
        public DestructibleTile DestructiblePrefab => _destructiblePrefab;
    }

    public class DestructibleTile : MonoBehaviour
    {
        public event Action<DestructibleTile> OnDestroyed;

        private PowerUp _powerUp;
        public PowerUp PowerUp
        {
            get => _powerUp;
            set
            {
                _powerUp = value;
                if (_powerUp != PowerUp.None)
                {
                    OnDestroyed += SpawnPowerUp;
                }
                else
                {
                    OnDestroyed -= SpawnPowerUp;
                }
            }
        }
        
        private bool _isExit;
        public bool IsExit
        {
            get => _isExit;
            set
            {
                _isExit = value;
                if (_isExit)
                {
                    OnDestroyed += SpawnExit;
                }
                else
                {
                    OnDestroyed -= SpawnExit;
                }
            }
        }

        private void SpawnExit(DestructibleTile _)
        {
            //todo
        }

        private void SpawnPowerUp(DestructibleTile _)
        {
            //todo
        }
    }

    public enum PowerUp 
    {
        None, BombUp, RangeUp, SpeedUp,WallPass, BombPass, Fireproof
    }

    public class TimerModel : ResettableScriptableSingleton<TimerModel>
    {
        //todo
    }
}