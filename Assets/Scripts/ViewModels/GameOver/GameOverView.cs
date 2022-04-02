using System;
using TMPro;
using UnityEngine;

namespace LudumDare50.Client.ViewModels.GameOver
{
    public class GameOverView : View<GameOverViewModel>
    {
        [SerializeField]
        private TMP_Text _scoreText;

        protected override void SetBindings()
        {
            _scoreText.text = "Score: 0";
            Bind<TimeSpan>((totalTime) => _scoreText.text = $"Score: {(int)Math.Floor(totalTime.TotalSeconds)}" , nameof(ViewModel.TotalTime));
        }

        public void OnContinueButtonPressed()
        {
            ViewModel.OnContinueButtonPressed();
        }
    }
}