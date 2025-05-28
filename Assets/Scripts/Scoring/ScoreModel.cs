using System;
using UnityEngine;

namespace Bomberman.Scoring
{
    [CreateAssetMenu(menuName = ScriptableObjectMenuName.Scoring + nameof(ScoreModel), fileName = nameof(ScoreModel))]
    public class ScoreModel : ResettableScriptableObject
    {
        [SerializeField] private int _scorePerSecondLeft = 10;
        private int _score;
        public int ScorePerSecondLeft => _scorePerSecondLeft;
        public bool CanScore { get; set; } = true;

        public event Action<int> OnScoreChanged;
        public int Score
        {
            get => _score;
            set
            {
                if (CanScore && value != _score)
                {
                    _score = value;
                    OnScoreChanged?.Invoke(_score);
                }
            }
        }


        protected override void ResetAfterPlayInEditor()
        {
            base.ResetAfterPlayInEditor();
            Score = 0;
            CanScore = true;
        }
    }
}