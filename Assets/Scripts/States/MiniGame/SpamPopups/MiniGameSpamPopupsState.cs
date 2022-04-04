using Cerberus;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.Settings;
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
        private readonly MiniGameSpamPopupsSettings _miniGameSpamPopupsSettings;

        [Inject]
        public MiniGameSpamPopupsState(IScreenService screenService, MiniGameSpamPopupsSettings miniGameSpamPopupsSettings)
        {
            _screenService = screenService;
            _miniGameSpamPopupsSettings = miniGameSpamPopupsSettings;
        }

        public override void OnEnter()
        {
            _screenService.AddToScreen<MiniGameSpamPopupsViewModel, int>(_miniGameSpamPopupsSettings.AmountOfPopups);
        }

        public override void OnExit()
        {
            _screenService.RemoveFromScreen<MiniGameSpamPopupsViewModel>();
        }
    }
}
