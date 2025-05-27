using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Bomberman.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private InputActionReference _startInput;

        private void OnEnable()
        {
            _startInput.action.performed += StartGame;
        }

        private void OnDisable()
        {
            _startInput.action.performed -= StartGame;
        }

        private void StartGame(InputAction.CallbackContext _)
        {
            Fader.Instance.OnFadeComplete += LoadGameScene;
            Fader.Instance.FadeOut(1);
        }

        private void LoadGameScene()
        {
            Fader.Instance.OnFadeComplete -= LoadGameScene;
            SceneManager.LoadScene("GameScene");
        }
    }
}