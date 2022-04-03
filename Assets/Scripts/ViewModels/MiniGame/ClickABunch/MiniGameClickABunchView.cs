using UnityEngine;
using UnityEngine.UI;

namespace LudumDare50.Client.ViewModels.ClickABunch
{
    public class MiniGameClickABunchView : View<MiniGameClickABunchViewModel>
    {
        [SerializeField]
        private Slider _downloadSlider;

        protected override void SetBindings()
        {
            _downloadSlider.value = 0;
            Bind<int>((clickCount) =>
            {
                var percent = clickCount / (float)ViewModel.NeededClickCount;
                _downloadSlider.value = percent;
            }, nameof(ViewModel.ClickCount));
        }

        public void OnClickButtonPressed()
        {
            ViewModel.OnClickButtonPressed();
        }
    }
}
