using System.Linq;
using UnityEngine;

namespace Bomberman.Level
{
    /// <summary>
    /// Spawns the blockers/free tiles into the world 
    /// </summary>
    public class LevelLayoutGeneratorView : MonoBehaviour
    {
        [SerializeField] private LevelLayoutGeneratorModel _levelLayoutGeneratorModel;
        [SerializeField] private LevelPrefabsModel _levelPrefabsModel;

        private void Awake()
        {
            GenerateBaseLevel();
        }

        private void GenerateBaseLevel()
        {
            foreach (var blockedCoord in _levelLayoutGeneratorModel.BlockedCoords)
            {
                Instantiate(_levelPrefabsModel.BlockerTile, blockedCoord, Quaternion.identity, transform);
            }

            foreach (var freeCoord in _levelLayoutGeneratorModel.WalkableTiles.Where(freeCoord => freeCoord != null))
            {
                Instantiate(_levelPrefabsModel.WalkableTile, freeCoord.Position, Quaternion.identity, transform);
            }
        }
    }
}