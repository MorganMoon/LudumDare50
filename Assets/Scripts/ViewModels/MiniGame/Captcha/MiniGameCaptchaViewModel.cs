using Cerberus;
using LudumDare50.Client.States.MiniGame;
using Zenject;

namespace LudumDare50.Client.ViewModels.Captcha
{
    public class MiniGameCaptchaViewModel : ViewModel
    {
        private readonly IStateController<MiniGameStateEvent> _miniGameStateController;

        [Inject]
        public MiniGameCaptchaViewModel(IStateController<MiniGameStateEvent> miniGameStateController)
        {
            _miniGameStateController = miniGameStateController;
        }

        public void OnNotARobotButtonPressed()
        {
            _miniGameStateController.TriggerEvent(MiniGameStateEvent.Success);
        }
    }
}
