using TMPro;
using UnityEngine;

namespace LudumDare50.Client.ViewModels.GameIntroduction
{
    public class GameIntroductionView : View<GameIntroductionViewModel>
    {
        [SerializeField]
        private TMP_Text _tiredReasonText;

        protected override void SetBindings()
        {
            Bind<string>((tiredReason) => _tiredReasonText.text = tiredReason, nameof(ViewModel.TiredReason));
        }

        public void OnContinueButtonPressed()
        {
            ViewModel.OnContinueButtonPressed();
        }
    }
}