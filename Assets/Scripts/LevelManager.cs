using System.Collections.Generic;
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
            LoadCurrentLevel();
        }

        [ContextMenu("Load next level")]
        private void LoadNextLevel()
        {
            _currentLevelIndex++;
            LoadCurrentLevel();
        }

        private void LoadCurrentLevel()
        {
            GameEvents.instance.OnLevelTeardown?.Invoke();
            _levelView.LoadLevel(_levelModels[_currentLevelIndex]);
            GameEvents.instance.OnLevelSetupComplete?.Invoke();
        }
    }
}