using Cerberus;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.ViewModels.ClickABunch;

namespace LudumDare50.Client.States.MiniGame.ClickABunch
{
    public enum MiniGameClickABunchStateEvent
    {

    }

    public class MiniGameClickABunchState : State
    {
        private readonly IScreenService _screenService;

        public MiniGameClickABunchState(IScreenService screenService)
        {
            _screenService = screenService;
        }

        public override void OnEnter()
        {
            _screenService.AddToScreen<MiniGameClickABunchViewModel>();
        }

        public override void OnExit()
        {
            _screenService.RemoveFromScreen<MiniGameClickABunchViewModel>();
        }
    }
}
