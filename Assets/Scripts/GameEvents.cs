using System;
using UnityEditor;

namespace Bomberman.Player
{
    public class GameEvents : ScriptableSingleton<GameEvents>
    {
        public event Action OnLevelReload;
    }
}