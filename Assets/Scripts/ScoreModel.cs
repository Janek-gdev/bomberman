using System;
using UnityEngine;

namespace Bomberman.Scoring
{
    [CreateAssetMenu(menuName = MenuName.Scoring + nameof(ScoreModel), fileName = nameof(ScoreModel))]
    public class ScoreModel : ResettableScriptableSingleton<ScoreModel>
    {
        [SerializeField] private int _scorePerSecondLeft = 10;
        public int ScorePerSecondLeft => _scorePerSecondLeft;
        
        public event Action<int> OnScoreChanged;
        private int _score;
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

        public bool CanScore { get; set; } = true;

        protected override void ResetAfterPlayInEditor()
        {
            base.ResetAfterPlayInEditor();
            Score = 0;
            CanScore = true;
        }
    }
}