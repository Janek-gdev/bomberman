using System;
using UnityEditor;
using UnityEngine;

namespace Bomberman.Utility
{
    [CreateAssetMenu(menuName = MenuName.Utility + nameof(GameEvents), fileName = nameof(GameEvents))]
    public class GameEvents : ScriptableSingleton<GameEvents>
    {
        public Action OnLevelTeardown;
        public Action OnPlayerDeath;
        public Action OnLevelComplete;
        public Action OnLevelSetupComplete;
    }
}