using TMPro;
using UnityEngine;

namespace LudumDare50.Client.ViewModels.MainMenu
{
    public class MainMenuView : View<MainMenuViewModel>
    {
        [SerializeField]
        private TMP_Text _title;

        protected override void SetBindings()
        {
            Bind<string>((value) => _title.text = value, nameof(ViewModel.GameName));
        }

        public void OnStartGameButtonPressed()
        {
            ViewModel.OnStartGameButtonPressed();
        }

        public void OnSettingsButtonPressed()
        {
            ViewModel.OnSettingsButtonPressed();
        }

        public void OnCreditsButtonPressed()
        {
            ViewModel.OnCreditsButtonPressed();
        }
    }
}