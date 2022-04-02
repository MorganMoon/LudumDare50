using Cerberus;
using LudumDare50.Client.States.MainMenu;
using LudumDare50.Client.States.Startup;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.ViewModels.MainMenu
{
    public class MainMenuViewModel : ViewModel
    {
        private readonly IStateController<StartupStateEvent> _startupStateController;
        private readonly IStateController<MainMenuStateEvent> _mainMenuStateController;

        private string _gameName;
        public string GameName
        {
            get => _gameName;
            set
            {
                if(_gameName == value)
                {
                    return;
                }

                _gameName = value;
                OnPropertyChanged();
            }
        }

        [Inject]
        public MainMenuViewModel(IStateController<StartupStateEvent> startupStateController, IStateController<MainMenuStateEvent> mainMenuStateController)
        {
            _startupStateController = startupStateController;
            _mainMenuStateController = mainMenuStateController;
        }

        public void OnStartGameButtonPressed()
        {
            _startupStateController.TriggerEvent(StartupStateEvent.PlayGame);
        }

        public void OnSettingsButtonPressed()
        {
            _mainMenuStateController.TriggerEvent(MainMenuStateEvent.Settings);
        }

        public void OnCreditsButtonPressed()
        {
            _mainMenuStateController.TriggerEvent(MainMenuStateEvent.Credits);
        }

        public void OnQuitButtonPressed()
        {
            Application.Quit();
        }
    }
}