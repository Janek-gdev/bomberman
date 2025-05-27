using System.Collections;
using System.Collections.Generic;
using Bomberman.Player;
using Bomberman.Scoring;
using Bomberman.Timing;
using Bomberman.UI;
using Bomberman.Utility;
using UnityEngine;

namespace Bomberman.Level
{
    /// <summary>
    /// Handles the transition between levels and level resetting
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LivesModel _livesModel;
        [SerializeField] private TimerModel _timerModel;
        [SerializeField] private float _timeToDisplayLevelTextFor = 1f;
        [SerializeField] private LevelView _levelView;
        [SerializeField] private List<LevelModel> _levelModels;
        private int _currentLevelIndex;

        private void Awake()
        {
            StartCoroutine(FadeOutIntoLevel());
        }

        private void OnEnable()
        {
            GameEvents.instance.OnLevelComplete += LoadNextLevel;
            GameEvents.instance.OnPlayerDeath += FailPlayer;
            _timerModel.OnTimeLeftOnCurrentLevelChanged += CheckForTimeout;
        }

        private void OnDisable()
        {
            GameEvents.instance.OnLevelComplete -= LoadNextLevel;
            GameEvents.instance.OnPlayerDeath -= FailPlayer;
            _timerModel.OnTimeLeftOnCurrentLevelChanged -= CheckForTimeout;
        }

        private void CheckForTimeout(int _)
        {
            if (_timerModel.TimeLeftOnCurrentLevel <= 0)
            {
                FailPlayer();
            }
        }

        private void FailPlayer()
        {
            _livesModel.LivesLeft--;
            if (_livesModel.LivesLeft >= 0 && _currentLevelIndex != 0)
            {
                BeginCurrentLevelLoadSequence();
            }
            else
            {
                LoadFirstLevel();
            }
        }

        private void LoadFirstLevel()
        {
            _currentLevelIndex = 0;
            ScoreModel.instance.Score = 0;
            _livesModel.LivesLeft = _livesModel.StartingLives;
            BeginCurrentLevelLoadSequence();
        }

        [ContextMenu("Load next level")]
        private void LoadNextLevel()
        {
            _currentLevelIndex++;
            _livesModel.LivesLeft++;
            ScoreModel.instance.Score += _timerModel.TimeLeftOnCurrentLevel * ScoreModel.instance.ScorePerSecondLeft;
            BeginCurrentLevelLoadSequence();
        }

        private void BeginCurrentLevelLoadSequence()
        {
            ScoreModel.instance.CanScore = false;
            Fader.Instance.OnFadeComplete += LoadLevelFromFade;
            Fader.Instance.FadeOut();
            
        }

        private void LoadLevelFromFade()
        {
            Fader.Instance.OnFadeComplete -= LoadLevelFromFade;
            GameEvents.instance.OnLevelTeardown?.Invoke();
            StartCoroutine(FadeOutIntoLevel());
        }

        private IEnumerator FadeOutIntoLevel()
        {
            _levelView.LoadLevel(_levelModels[_currentLevelIndex]);
            Fader.Instance.DisplayText(_levelModels[_currentLevelIndex].LevelName);
            yield return new WaitForSeconds(_timeToDisplayLevelTextFor);
            GameEvents.instance.OnLevelSetupComplete?.Invoke();
            ScoreModel.instance.CanScore = true;
            Fader.Instance.FadeIn();
        }
    }
}