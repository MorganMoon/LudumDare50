using LudumDare50.Client.ViewModels;
using LudumDare50.Client.ViewModels.SupervisorAwareness;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare50.Client.ViewModels.SupervisorAwareness
{
    public class SupervisorAwarenessView : View<SupervisorAwarenessViewModel>
    {
        [SerializeField]
        private TMP_Text _awarenessText;

        [SerializeField]
        private Slider _awarenessSlider;

        [SerializeField]
        private Image _fillImage;

        protected override void SetBindings()
        {
            _awarenessText.text = "0/0";
            Bind<int>((maxFailures) =>
            {
                var splitText = _awarenessText.text.Split('/');
                _awarenessText.text = $"{splitText[0]}/{maxFailures}";
            }, nameof(ViewModel.MaxFailures));
            Bind<int>((currentFailures) =>
            {
                var splitText = _awarenessText.text.Split('/');
                _awarenessText.text = $"{currentFailures}/{splitText[1]}";

                float percent = currentFailures / float.Parse(splitText[1]);
                _awarenessSlider.value = percent;

                if (currentFailures >= int.Parse(splitText[1]) - 1)
                {
                    _fillImage.color = Color.red;
                }
                else
                {
                    _fillImage.color = Color.blue;
                }
            }, nameof(ViewModel.CurrentFailures));
        }
    }
}
