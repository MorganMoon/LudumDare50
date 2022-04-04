using Cerberus;
using LudumDare50.Client.Extensions;
using LudumDare50.Client.Infrastructure;
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

        [Inject]
        public MiniGameSelectWifiState(IScreenService screenService)
        {
            _screenService = screenService;
        }

        public override void OnEnter()
        {
            _screenService.AddToScreen<MiniGameSelectWifiViewModel, string[]>(new string[] { "poopoo", "MoonLAN", "cactusjuice", "ironthrone", "sendnudes", "Nacho WiFi", "Hooters Guest", "Wu Tang LAN" }.Shuffle());
        }

        public override void OnExit()
        {
            _screenService.RemoveFromScreen<MiniGameSelectWifiViewModel>();
        }
    }
}
