using System;
using System.Collections.Generic;
using Bomberman.Enemies;
using Bomberman.Player;
using Bomberman.Pooling;
using UnityEngine;

namespace Bomberman.Level
{
    [CreateAssetMenu(menuName = MenuName.LevelLayout + nameof(LevelPrefabsModel), fileName = nameof(LevelPrefabsModel))]
    public class LevelPrefabsModel : ScriptableObject
    {
        [Serializable]
        public class EnemyPrefab
        {
            public EnemyModel EnemyModel;
            public EnemyPool Pool;
        }

        [Serializable]
        public class PowerUpPrefab
        {
            public PowerUpPickup PowerUpPickup;
            public PowerUp PowerUp;
        }
        
        [SerializeField] private DestructibleTilePool _destructiblePool;
        [SerializeField] private List<EnemyPrefab> _enemyPrefabs;
        [SerializeField] private ExitDoor _exitDoor;
        [SerializeField] private GameObject _playerRig;
        [SerializeField] private GameObject _blockerTile;
        [SerializeField] private GameObject _walkableTile;
        [SerializeField] private List<PowerUpPrefab> _powerUpPrefabs;
        public GameObject BlockerTile => _blockerTile;
        public GameObject WalkableTile => _walkableTile;
        

        public GameObject PlayerRig => _playerRig;
        public ExitDoor ExitDoor => _exitDoor;
        public List<EnemyPrefab> EnemyPrefabs => _enemyPrefabs;
        public DestructibleTilePool DestructibleTilePool => _destructiblePool;
        public List<PowerUpPrefab> PowerUpPrefabs => _powerUpPrefabs;
    }
}