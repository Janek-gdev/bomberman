using System;
using UnityEngine;

namespace Bomberman.Timing
{
    [CreateAssetMenu(menuName = ScriptableObjectMenuName.UI + nameof(TimerModel), fileName = nameof(TimerModel))]
    public class TimerModel : ResettableScriptableObject
    {
        [SerializeField] private int _timePerLevel;
        public int TimePerLevel => _timePerLevel;
        private int _timeLeftOnCurrentLevel;
        private bool _timerPaused;

        public event Action<bool> OnTimerPausedChanged;
        public bool TimerPaused
        {
            get => _timerPaused;
            set
            {
                if (value != _timerPaused)
                {
                    _timerPaused = value;
                    OnTimerPausedChanged?.Invoke(_timerPaused);
                }
            }
        }
        
        public event Action<int> OnTimeLeftOnCurrentLevelChanged;
        public int TimeLeftOnCurrentLevel
        {
            get => _timeLeftOnCurrentLevel;
            set
            {
                if (value != _timeLeftOnCurrentLevel)
                {
                    _timeLeftOnCurrentLevel = value;
                    OnTimeLeftOnCurrentLevelChanged?.Invoke(_timeLeftOnCurrentLevel);
                }
            }
        }

        protected override void ResetAfterPlayInEditor()
        {
            base.ResetAfterPlayInEditor();
            TimeLeftOnCurrentLevel = 0;
            _timerPaused = false;
        }
    }
}