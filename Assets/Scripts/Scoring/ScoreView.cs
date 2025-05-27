using TMPro;
using UnityEngine;

namespace Bomberman.Scoring
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private ScoreModel _scoreModel;
        [SerializeField] private TMP_Text _scoreText;

        private void OnEnable()
        {
            UpdateScoreUI();
            _scoreModel.OnScoreChanged += UpdateScoreUI;
        }

        private void OnDisable()
        {
            _scoreModel.OnScoreChanged -= UpdateScoreUI;
        }

        private void UpdateScoreUI(int obj) => UpdateScoreUI();
        private void UpdateScoreUI()
        {
            _scoreText.text = _scoreModel.Score.ToString();
        }
    }
}