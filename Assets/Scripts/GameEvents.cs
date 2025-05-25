using System;
using UnityEditor;
using UnityEngine;

namespace Bomberman.Utility
{
    [CreateAssetMenu(menuName = MenuName.Utility + nameof(GameEvents), fileName = nameof(GameEvents))]
    public class GameEvents : ScriptableSingleton<GameEvents>
    {
        public Action OnLevelReloadBegin;
        public Action OnLevelReloadEnd;
        public Action OnPlayerDeath;
    }
}