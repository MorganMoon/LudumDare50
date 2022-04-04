using Cerberus;
using LudumDare50.Client.Extensions;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.Settings;
using LudumDare50.Client.ViewModels.SelectWifi;
using Zenject;

namespace LudumDare50.Client.States.MiniGame.SelectWifi
{
    public enum MiniGameSelectWifiStateEvent
    {

    }

    public class MiniGameSelectWifiState : State
    {
        private readonly IScreenService _screenService;
        private readonly MiniGameSelectWifiSettings _miniGameSelectWifiSettings;

        [Inject]
        public MiniGameSelectWifiState(IScreenService screenService, MiniGameSelectWifiSettings miniGameSelectWifiSettings)
        {
            _screenService = screenService;
            _miniGameSelectWifiSettings = miniGameSelectWifiSettings;
        }

        public override void OnEnter()
        {
            _screenService.AddToScreen<MiniGameSelectWifiViewModel, string[]>(_miniGameSelectWifiSettings.WifiNames.Shuffle());
        }

        public override void OnExit()
        {
            _screenService.RemoveFromScreen<MiniGameSelectWifiViewModel>();
        }
    }
}
