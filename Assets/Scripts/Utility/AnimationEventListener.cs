using System;
using UnityEngine;

namespace Bomberman.Utility
{
    public class AnimationEventListener : MonoBehaviour
    {
        public event Action OnAnimationEventFired;
        public void FireAnimationEvent()
        {
            Debug.Log("Animation event");
            OnAnimationEventFired?.Invoke();
        }
    }
}