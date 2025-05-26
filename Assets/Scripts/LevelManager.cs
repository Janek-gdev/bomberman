using System.Collections.Generic;
using Bomberman.UI;
using Bomberman.Utility;
using UnityEngine;

namespace Bomberman.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelView _levelView;
        [SerializeField] private List<LevelModel> _levelModels;

        private int _currentLevelIndex;

        private void Awake()
        {
            _levelView.LoadLevel(_levelModels[_currentLevelIndex]);
        }

        private void OnEnable()
        {
            GameEvents.instance.OnLevelComplete += LoadNextLevel;
            GameEvents.instance.OnPlayerDeath += OnPlayerDeath;
        }

        private void OnDisable()
        {
            GameEvents.instance.OnLevelComplete -= LoadNextLevel;
            GameEvents.instance.OnPlayerDeath -= OnPlayerDeath;
        }

        private void OnPlayerDeath()
        {
            LoadFirstLevel();
        }

        private void LoadFirstLevel()
        {
            _currentLevelIndex = 0;
            BeginCurrentLevelLoadSequence();
        }

        [ContextMenu("Load next level")]
        private void LoadNextLevel()
        {
            _currentLevelIndex++;
            BeginCurrentLevelLoadSequence();
        }

        private void BeginCurrentLevelLoadSequence()
        {
            Fader.Instance.OnFadeComplete += LoadLevel;
            Fader.Instance.FadeOut();
            
        }

        private void LoadLevel()
        {
            Fader.Instance.OnFadeComplete -= LoadLevel;
            GameEvents.instance.OnLevelTeardown?.Invoke();
            _levelView.LoadLevel(_levelModels[_currentLevelIndex]);
            GameEvents.instance.OnLevelSetupComplete?.Invoke();
            Fader.Instance.FadeIn();
        }
    }
}