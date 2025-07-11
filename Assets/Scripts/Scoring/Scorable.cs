﻿using UnityEngine;

namespace Bomberman.Scoring
{
    /// <summary>
    /// A destructible object that gives the player score when destroyed
    /// </summary>
    public class Scorable : MonoBehaviour
    {
        [SerializeField] private int _score;
        [SerializeField] private ScoreModel _scoreModel;
        //todo on destroy isn't ideal since it calls on level de-load and we need to have a bool to check we can add score, an event fired from another component would be better
        private void OnDestroy()
        {
            _scoreModel.Score += _score;
        }
    }
}