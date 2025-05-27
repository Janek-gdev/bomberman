using System;
using System.Collections.Generic;
using Bomberman.Enemies;
using Bomberman.Player;
using Bomberman.PowerUps;
using UnityEngine;

namespace Bomberman.Level
{
    /// <summary>
    /// Represents a playable level within the game
    /// </summary>
    [CreateAssetMenu(menuName = ScriptableObjectMenuName.LevelLayout + nameof(LevelModel), fileName = nameof(LevelModel))]
    public class LevelModel : ResettableScriptableObject
    {
        [Serializable]
        public class EnemyCounter
        {
            public EnemyModel EnemyModel;
            public int Amount;
        }

        [SerializeField] private string _levelName;
        [SerializeField] private List<EnemyCounter> _enemies;
        [SerializeField, Min(2)] private int _destructibleTileCount;
        [SerializeField] private PowerUp _availablePowerUp;
        [SerializeField] private LevelPrefabsModel _levelPrefabsModel;

        //todo move these to own models?
        public List<EnemyView> SpawnedEnemies { get; set; }
        public List<DestructibleTileView> SpawnedDestructibleTiles { get; set; }

        public List<EnemyCounter> Enemies => _enemies;
        public int DestructibleTileCount => _destructibleTileCount;
        public PowerUp AvailablePowerUp => _availablePowerUp;
        public LevelPrefabsModel Prefabs => _levelPrefabsModel;
        public string LevelName => _levelName;

        protected override void ResetAfterPlayInEditor()
        {
            base.ResetAfterPlayInEditor();
            SpawnedEnemies = new List<EnemyView>();
            SpawnedDestructibleTiles = new List<DestructibleTileView>();
        }
    }
}