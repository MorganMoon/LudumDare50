using Cerberus;
using LudumDare50.Client.Infrastructure;
using Zenject;

namespace LudumDare50.Client.States.Settings
{
    public enum SettingsStateEvent
    {
        GoBack
    }

    public class SettingsState : State
    {
        private IScreenService _screenService;

        [Inject]
        public SettingsState(IScreenService screenService)
        {
            _screenService = screenService;
        }

        public override void OnEnter()
        {
            _screenService.ClearScreen();
        }
    }
}
