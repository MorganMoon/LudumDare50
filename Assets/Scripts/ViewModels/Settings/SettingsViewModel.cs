using Cerberus;
using LudumDare50.Client.States.Settings;
using UnityEngine;
using Zenject;
using static LudumDare50.Client.ViewModels.Settings.SettingsViewModel;

namespace LudumDare50.Client.ViewModels.Settings
{
    public class SettingsViewModel : ViewModel<PrepareData>
    {
        private readonly IStateController<SettingsStateEvent> _settingsStateController;

        private Resolution _selectedResolution;
        public Resolution SelectedResolution
        {
            get => _selectedResolution;
            set
            {
                if(_selectedResolution.Equals(value))
                {
                    return;
                }

                _selectedResolution = value;
                OnPropertyChanged();
            }
        }

        private Resolution[] _resolutionEntries;
        public Resolution[] ResolutionEntries
        {
            get => _resolutionEntries;
            set
            {
                if(_resolutionEntries == value)
                {
                    return;
                }
                _resolutionEntries = value;
                OnPropertyChanged();
            }
        }

        private bool _isFullScreen;
        public bool IsFullScreen
        {
            get => _isFullScreen;
            set
            {
                if(_isFullScreen == value)
                {
                    return;
                }
                _isFullScreen = value;
                OnPropertyChanged();
                ChangesMade = true;
            }
        }

        private bool _changesMade = false;
        public bool ChangesMade
        {
            get => _changesMade;
            set
            {
                if(_changesMade == value)
                {
                    return;
                }
                _changesMade = value;
                OnPropertyChanged();
            }
        }

        [Inject]
        public SettingsViewModel(IStateController<SettingsStateEvent> settingsStateController)
        {
            _settingsStateController = settingsStateController;
        }

        public void OnResolutionSelected(int index)
        {
            SelectedResolution = ResolutionEntries[index];
            ChangesMade = true;
        }

        public void OnApplyButtonPressed()
        {
            if(!ChangesMade)
            {
                return;
            }

            Screen.SetResolution(SelectedResolution.width, SelectedResolution.height, IsFullScreen, SelectedResolution.refreshRate);

            ChangesMade = false;
        }

        public void OnBackButtonPressed()
        {
            _settingsStateController.TriggerEvent(SettingsStateEvent.GoBack);
        }

        public override void Prepare(PrepareData parameter)
        {
            ChangesMade = false;
            ResolutionEntries = parameter.Resolutions;
            _isFullScreen = parameter.IsFullScreen;
        }

        public class PrepareData
        {
            public Resolution[] Resolutions { get; set; }
            public Resolution SelectedResolution { get; set; }
            public bool IsFullScreen { get; set; }
        }
    }
}