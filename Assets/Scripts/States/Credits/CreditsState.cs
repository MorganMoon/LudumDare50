using Cerberus;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.ViewModels.Credits;

namespace LudumDare50.Client.States.Credits
{
    public enum CreditsStateEvent
    {
        GoBack
    }

    public class CreditsState : State
    {
        private readonly IScreenService _screenService;

        public CreditsState(IScreenService screenService)
        {
            _screenService = screenService;
        }

        public override void OnEnter()
        {
            _screenService.SetActiveScreen<CreditsViewModel>();
        }
    }
}
