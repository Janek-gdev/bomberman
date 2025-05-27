using UnityEngine;

namespace Bomberman.Scoring
{
    public class Scorable : MonoBehaviour
    {
        [SerializeField] private int _score;

        //todo on destroy isn't ideal since it calls on level de-load and we need to have a bool to check we can add score, an event fired from another component would be better
        private void OnDestroy()
        {
            ScoreModel.instance.Score += _score;
        }
    }
}