using Cerberus;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.ViewModels.ClickABunch;
using Zenject;

namespace LudumDare50.Client.States.MiniGame.SpamPopups
{
    public enum MiniGameSpamPopupsStateEvent
    {

    }

    public class MiniGameSpamPopupsState : State
    {
        private readonly IScreenService _screenService;

        [Inject]
        public MiniGameSpamPopupsState(IScreenService screenService)
        {
            _screenService = screenService;
        }

        public override void OnEnter()
        {
            _screenService.AddToScreen<MiniGameSpamPopupsViewModel, int>(8);
        }

        public override void OnExit()
        {
            _screenService.RemoveFromScreen<MiniGameSpamPopupsViewModel>();
        }
    }
}
