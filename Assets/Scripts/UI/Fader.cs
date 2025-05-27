using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Bomberman.UI
{
    /// <summary>
    /// Fades the screen in and out 
    /// </summary>
    public class Fader : MonoBehaviour
    {
        public static Fader Instance { get; private set; }
    
        [SerializeField] CanvasGroup _canvasGroup;
        [SerializeField] float _defaultDuration = 1f;
        [SerializeField] private TMP_Text _text;
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

        public void DisplayText(string text)
        {
            _text.text = text;
        }

        public void FadeIn(float duration = -1f) => StartCoroutine(Fade(1f, 0f, duration));
        public void FadeOut(float duration = -1f) => StartCoroutine(Fade(0f, 1f, duration));

        private IEnumerator Fade(float from, float to, float duration)
        {
            _text.text = "";
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