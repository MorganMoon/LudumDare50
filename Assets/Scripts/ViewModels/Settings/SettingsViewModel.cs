using UnityEngine;
using static LudumDare50.Client.ViewModels.Settings.SettingsViewModel;

namespace LudumDare50.Client.ViewModels.Settings
{
    public class SettingsViewModel : ViewModel<PrepareData>
    {
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

        public void OnResolutionSelected(int index)
        {

        }

        public void OnApplyButtonPressed()
        {

        }

        public void OnBackButtonPressed()
        {

        }

        public override void Prepare(PrepareData parameter)
        {
            ChangesMade = false;
            ResolutionEntries = parameter.Resolutions;
        }

        public class PrepareData
        {
            public Resolution[] Resolutions { get; set; }
            public Resolution SelectedResolution { get; set; }
        }
    }
}