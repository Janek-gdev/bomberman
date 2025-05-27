using System;
using UnityEngine;

namespace Bomberman.Player
{
    [CreateAssetMenu(menuName = ScriptableObjectMenuName.Player + nameof(LivesModel), fileName = nameof(LivesModel))]
    public class LivesModel : ResettableScriptableObject
    {
        [SerializeField] private int _startingLives = 2;
        public int StartingLives => _startingLives;
        
        public event Action<int> OnLivesLeftChanged;
        private int _livesLeft;
        public int LivesLeft
        {
            get => _livesLeft;
            set
            {
                if (value != _livesLeft)
                {
                    _livesLeft = value;
                    OnLivesLeftChanged?.Invoke(_livesLeft);
                }
            }
        }

        protected override void ResetAfterPlayInEditor()
        {
            base.ResetAfterPlayInEditor();
            LivesLeft = StartingLives;
        }
    }
}