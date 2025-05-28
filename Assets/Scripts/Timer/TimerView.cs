using System.Collections;
using Bomberman.Utility;
using TMPro;
using UnityEngine;

namespace Bomberman.Timing
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TimerModel _timerModel;
        [SerializeField] private TMP_Text _timerText;
        private Coroutine _timer;
        private WaitForSeconds _secondsTimer;

        private void Awake()
        {
            _timerModel.TimeLeftOnCurrentLevel = _timerModel.TimePerLevel;
        }

        private void OnEnable()
        {
            UpdateTimerUI();
            _secondsTimer = new WaitForSeconds(1);
            GameEvent.OnLevelSetupComplete += StartTimer;
            GameEvent.OnLevelTeardown += StopTimer;
            _timerModel.OnTimeLeftOnCurrentLevelChanged += UpdateTimerUI;
            _timerModel.OnTimerPausedChanged += ToggleTimer;
        }

        private void OnDisable()
        {
            GameEvent.OnLevelSetupComplete -= StartTimer;
            GameEvent.OnLevelTeardown -= StopTimer;
            _timerModel.OnTimeLeftOnCurrentLevelChanged -= UpdateTimerUI;
            _timerModel.OnTimerPausedChanged -= ToggleTimer;
        }

        private void ToggleTimer(bool _)
        {
            if (_timerModel.TimerPaused)
            {
                StopTimer();
            }
            else
            {
                RestartTimer();
            }
        }

        private void UpdateTimerUI(int _) => UpdateTimerUI();
        private void UpdateTimerUI()
        {
            _timerText.text = _timerModel.TimeLeftOnCurrentLevel.ToString();
        }

        private void StopTimer()
        {
            if (_timer != null)
            {
                StopCoroutine(_timer);
            }

            _timer = null;
        }

        private void StartTimer()
        {
            _timerModel.TimeLeftOnCurrentLevel = _timerModel.TimePerLevel;
            RestartTimer();
        }

        private void RestartTimer()
        {
            if (_timer != null)
            {
                StopTimer();
            }
            
            _timer = StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            while (true)
            {
                yield return _secondsTimer;
                _timerModel.TimeLeftOnCurrentLevel--;
            }
        }
    }
}