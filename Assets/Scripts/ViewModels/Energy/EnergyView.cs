using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare50.Client.ViewModels.Energy
{
    public class EnergyView : View<EnergyViewModel>
    {
        [SerializeField]
        private TMP_Text _energyText;

        [SerializeField]
        private Slider _energySlider;

        [SerializeField]
        private Image _fillImage;

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

                var percent = currentEnergy / float.Parse(splitText[1]);
                _energySlider.value = percent;

                if(percent > 0.5)
                {
                    _fillImage.color = Color.Lerp(Color.green, Color.yellow, 1 - ((percent - 0.5f) * 2));
                }
                else
                {
                    _fillImage.color = Color.Lerp(Color.yellow, Color.red, 1 - (percent * 2));

                }
            }, nameof(ViewModel.CurrentEnergy));
        }
    }
}
