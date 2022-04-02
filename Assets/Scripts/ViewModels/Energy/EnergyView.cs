using TMPro;
using UnityEngine;

namespace LudumDare50.Client.ViewModels.Energy
{
    public class EnergyView : View<EnergyViewModel>
    {
        [SerializeField]
        private TMP_Text _energyText;

        protected override void SetBindings()
        {
            _energyText.text = "0/0";
            Bind<float>((maxEnergy) =>
            {
                var splitText = _energyText.text.Split('/');
                _energyText.text = $"{splitText[0]}/{maxEnergy}";
            }, nameof(ViewModel.MaxEnergy));
            Bind<float>((currentEnergy) =>
            {
                var splitText = _energyText.text.Split('/');
                _energyText.text = $"{currentEnergy.ToString("N1")}/{splitText[1]}";
            }, nameof(ViewModel.CurrentEnergy));
        }
    }
}
