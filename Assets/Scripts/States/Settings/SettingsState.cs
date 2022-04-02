using Cerberus;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.ViewModels.Settings;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.States.Settings
{
    public enum SettingsStateEvent
    {
        GoBack
    }

    public class SettingsState : State
    {
        private IScreenService _screenService;

        [Inject]
        public SettingsState(IScreenService screenService)
        {
            _screenService = screenService;
        }

        public override void OnEnter()
        {
            //_screenService.ClearScreen();
            _screenService.SetActiveScreen<SettingsViewModel, SettingsViewModel.PrepareData>(new SettingsViewModel.PrepareData()
            {
                Resolutions = Screen.resolutions,
                SelectedResolution = new Resolution() { width = Screen.width, height = Screen.height, refreshRate = Screen.currentResolution.refreshRate },
                IsFullScreen = Screen.fullScreen
            });
        }
    }
}
