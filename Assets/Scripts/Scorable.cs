using UnityEngine;

namespace Bomberman.Scoring
{
    public class Scorable : MonoBehaviour
    {
        [SerializeField] private int _score;

        private void OnDestroy()
        {
            ScoreModel.instance.Score += _score;
        }
    }
}