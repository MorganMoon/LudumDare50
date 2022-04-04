using Cerberus;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.ViewModels.Captcha;

namespace LudumDare50.Client.States.MiniGame.Captcha
{
    public enum MiniGameCaptchaStateEvent
    {

    }

    public class MiniGameCaptchaState : State
    {
        private readonly IScreenService _screenService;

        public MiniGameCaptchaState(IScreenService screenService)
        {
            _screenService = screenService;
        }

        public override void OnEnter()
        {
            var captchaVM = _screenService.AddToScreen<MiniGameCaptchaViewModel>();
            captchaVM.Startup();
        }

        public override void OnExit()
        {
            _screenService.RemoveFromScreen<MiniGameCaptchaViewModel>();
        }
    }
}
