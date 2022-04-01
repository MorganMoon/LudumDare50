using Cerberus;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.ViewModels.MainMenu;
using Zenject;

namespace LudumDare50.Client.States.MainMenu
{
    public enum MainMenuStateEvent
    {
        Credits,
        Settings
    }

    public class MainMenuState : State
    {
        private IScreenService _screenService;

        [Inject]
        public MainMenuState(IScreenService screenService)
        {
            _screenService = screenService;
        }

        public override void OnEnter()
        {
            _screenService.SetActiveScreen<MainMenuViewModel>();
        }
    }
}