using UnityEngine;

namespace Bomberman.Level
{
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
            
            foreach (var freeCoord in _levelLayoutGeneratorModel.FreeCoords)
            {
                Instantiate(_levelPrefabsModel.WalkableTile, freeCoord, Quaternion.identity, transform);
            }
        }
    }
}