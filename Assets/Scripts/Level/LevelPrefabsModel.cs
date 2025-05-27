using System;
using System.Collections.Generic;
using Bomberman.Enemies;
using Bomberman.Player;
using Bomberman.Pooling;
using Bomberman.PowerUps;
using UnityEngine;

namespace Bomberman.Level
{
    /// <summary>
    /// A wrapper class for all the prefabs used within a level
    /// Separate from <see cref="LevelModel"/> to allow easy re-use of prefabs, while still allowing reskinning/logic changing
    /// </summary>
    [CreateAssetMenu(menuName = ScriptableObjectMenuName.LevelLayout + nameof(LevelPrefabsModel), fileName = nameof(LevelPrefabsModel))]
    public class LevelPrefabsModel : ScriptableObject
    {

        [Serializable]
        public class PowerUpPrefab
        {
            public PowerUpPickup PowerUpPickup;
            public PowerUp PowerUp;
        }
        
        [SerializeField] private DestructibleTilePool _destructiblePool;
        [SerializeField] private ExitDoor _exitDoor;
        [SerializeField] private GameObject _playerRig;
        [SerializeField] private GameObject _blockerTile;
        [SerializeField] private GameObject _walkableTile;
        [SerializeField] private List<PowerUpPrefab> _powerUpPrefabs;
        public GameObject BlockerTile => _blockerTile;
        public GameObject WalkableTile => _walkableTile;
        

        public GameObject PlayerRig => _playerRig;
        public ExitDoor ExitDoor => _exitDoor;
        public DestructibleTilePool DestructibleTilePool => _destructiblePool;
        public List<PowerUpPrefab> PowerUpPrefabs => _powerUpPrefabs;
    }
}