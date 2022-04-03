using Cerberus;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.States.Startup
{
    public enum StartupStateEvent
    {
        PlayGame
    }

    public enum StartupStateSubState
    {
        MainMenu,
        SettingsMenu,
        CreditsMenu
    }

    public class StartupState : State
    {
        private readonly Transform _startupArea;

        [Inject]
        public StartupState([Inject(Id = "StartupArea")]Transform startupArea)
        {
            _startupArea = startupArea;
        }

        public override void OnEnter()
        {
            _startupArea.gameObject.SetActive(true);
        }

        public override void OnExit()
        {
            _startupArea.gameObject.SetActive(false);
        }
    }
}