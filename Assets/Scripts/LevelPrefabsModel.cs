using System;
using System.Collections.Generic;
using Bomberman.Enemies;
using UnityEngine;

namespace Bomberman.Level
{
    [CreateAssetMenu(menuName = MenuName.LevelLayout + nameof(LevelPrefabsModel), fileName = nameof(LevelPrefabsModel))]
    public class LevelPrefabsModel : ScriptableObject
    {
        [Serializable]
        public class EnemyPrefab
        {
            public EnemyModel _enemyModel;
            public EnemyView Prefab;
        }
        
        [SerializeField] private DestructibleTile _destructiblePrefab;
        [SerializeField] private List<EnemyPrefab> _enemyPrefabs;
        [SerializeField] private GameObject _exitDoor;

        public GameObject ExitDoor => _exitDoor;
        public List<EnemyPrefab> EnemyPrefabs => _enemyPrefabs;
        public DestructibleTile DestructiblePrefab => _destructiblePrefab;
    }
}