using UnityEngine;

namespace Bomberman.Level
{
    public class LevelLayoutGeneratorView : MonoBehaviour
    {
        [SerializeField] private LevelLayoutGeneratorModel _levelLayoutGeneratorModel;
        [SerializeField] private GameObject _blockerPrefab;
        [SerializeField] private GameObject _emptySpacePrefab;

        private void Awake()
        {
            GenerateBaseLevel();
        }

        private void GenerateBaseLevel()
        {
            foreach (var blockedCoord in _levelLayoutGeneratorModel.BlockedCoords)
            {
                Instantiate(_blockerPrefab, blockedCoord, Quaternion.identity, transform);
            }
            
            foreach (var freeCoord in _levelLayoutGeneratorModel.FreeCoords)
            {
                Instantiate(_emptySpacePrefab, freeCoord, Quaternion.identity, transform);
            }
        }
    }
}