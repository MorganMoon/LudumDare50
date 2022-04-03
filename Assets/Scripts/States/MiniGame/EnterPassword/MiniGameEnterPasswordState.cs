using Cerberus;
using UnityEngine;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.ViewModels.MiniGame.EnterPassword;
using Zenject;
using LudumDare50.Client.Settings;

namespace LudumDare50.Client.States.MiniGame.EnterPassword
{
    public enum MiniGameEnterPasswordStateEvent
    {

    }

    public class MiniGameEnterPasswordState : State
    {
        private readonly IScreenService _screenService;
        private readonly EnterPasswordSettings _enterPasswordSettings;

        [Inject]
        public MiniGameEnterPasswordState(IScreenService screenService, EnterPasswordSettings enterPasswordSettings)
        {
            _screenService = screenService;
            _enterPasswordSettings = enterPasswordSettings;
        }

        public override void OnEnter()
        {
            int randomIndex = Random.Range(0, _enterPasswordSettings.passwords.Length);
            string password = _enterPasswordSettings.passwords[randomIndex];
            _screenService.AddToScreen<EnterPasswordViewModel, string>(password);
        }

        public override void OnExit()
        {
            _screenService.RemoveFromScreen<EnterPasswordViewModel>();
        }
    }
}
