using System;
using Bomberman.Player;
using TMPro;
using UnityEngine;

namespace Bomberman.Player
{
    public class LivesView : MonoBehaviour
    {
        [SerializeField] private LivesModel _livesModel;
        [SerializeField] private TMP_Text _livesText;

        private void Awake()
        {
            _livesModel.LivesLeft = _livesModel.StartingLives;
        }

        private void OnEnable()
        {
            UpdateLivesUI();
            _livesModel.OnLivesLeftChanged += UpdateLivesUI;
        }

        private void OnDisable()
        {
            _livesModel.OnLivesLeftChanged -= UpdateLivesUI;
        }

        private void UpdateLivesUI(int obj) => UpdateLivesUI();
        private void UpdateLivesUI()
        {
            _livesText.text = _livesModel.LivesLeft.ToString();
        }
    }
}