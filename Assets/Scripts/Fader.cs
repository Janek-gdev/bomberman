using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bomberman.UI
{
    public class Fader : MonoBehaviour
    {
        public static Fader Instance { get; private set; }

        [SerializeField] CanvasGroup _canvasGroup;
        [SerializeField] float _defaultDuration = 1f;

        public event Action OnFadeComplete;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void FadeIn(float duration = -1f) => StartCoroutine(Fade(1f, 0f, duration));
        public void FadeOut(float duration = -1f) => StartCoroutine(Fade(0f, 1f, duration));

        private IEnumerator Fade(float from, float to, float duration)
        {
            if (_canvasGroup == null) yield break;

            _canvasGroup.alpha = from;
            _canvasGroup.blocksRaycasts = true;

            var time = 0f;
            var total = duration > 0 ? duration : _defaultDuration;

            while (time < total)
            {
                time += Time.unscaledDeltaTime;
                _canvasGroup.alpha = Mathf.Lerp(from, to, time / total);
                yield return null;
            }

            _canvasGroup.alpha = to;
            _canvasGroup.blocksRaycasts = to > 0;
            OnFadeComplete?.Invoke();
        }
    }
}