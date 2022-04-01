using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare50.Client.ViewModels.Settings
{
    public class SettingsView : View<SettingsViewModel>
    {
        [SerializeField]
        private TMP_Dropdown _resolutionDropdown;
        [SerializeField]
        private Button _applyButton;

        protected override void SetBindings()
        {
            Bind<Resolution[]>((resolutions) =>
            {
                _resolutionDropdown.options.Clear();
                _resolutionDropdown.options = resolutions.Select(r => new TMP_Dropdown.OptionData() { text = r.ToString() }).ToList();
            }, nameof(ViewModel.ResolutionEntries));
            Bind<Resolution>((resolution) =>
            {
                var selectedIndex = _resolutionDropdown.options.FindIndex(od => od.text == resolution.ToString());
                if(selectedIndex != -1)
                {
                    _resolutionDropdown.SetValueWithoutNotify(selectedIndex);
                }
            }, nameof(ViewModel.SelectedResolution));
            Bind<bool>((changesMade) => _applyButton.interactable = changesMade, nameof(ViewModel.ChangesMade));
        }

        public void OnApplyButtonPressed()
        {
            ViewModel.OnApplyButtonPressed();
        }

        public void OnBackButtonPressed()
        {
            ViewModel.OnBackButtonPressed();
        }

        public void OnResolutionSelected(int index)
        {
            ViewModel.OnResolutionSelected(index);
        }
    }
}
