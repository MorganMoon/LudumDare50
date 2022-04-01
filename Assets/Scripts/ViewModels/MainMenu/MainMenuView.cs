namespace LudumDare50.Client.ViewModels.MainMenu
{
    public class MainMenuView : View<MainMenuViewModel>
    {
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