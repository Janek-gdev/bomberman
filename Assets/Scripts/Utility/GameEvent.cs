using System;
using UnityEditor;
using UnityEngine;

namespace Bomberman.Utility
{
    public static class GameEvent
    {
        public static Action OnLevelTeardown;
        public static Action OnPlayerDeath;
        public static Action OnLevelComplete;
        public static Action OnLevelSetupComplete;
    }
}