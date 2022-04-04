using Cerberus;
using LudumDare50.Client.States.Credits;
using Zenject;

namespace LudumDare50.Client.ViewModels.Credits
{
    public class CreditsViewModel : ViewModel
    {
        private readonly IStateController<CreditsStateEvent> _creditsStateController;

        [Inject]
        public CreditsViewModel(IStateController<CreditsStateEvent> creditsStateController)
        {
            _creditsStateController = creditsStateController;
        }

        public void OnBackButtonPressed()
        {
            _creditsStateController.TriggerEvent(CreditsStateEvent.GoBack);
        }
    }
}