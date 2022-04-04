using LudumDare50.Client.ViewModels;
using LudumDare50.Client.ViewModels.Captcha;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare50.Client.ViewModels.Captcha
{
    public class MiniGameCaptchaView : View<MiniGameCaptchaViewModel>
    {
        [SerializeField]
        private List<Toggle> _toggleList;

        protected override void SetBindings()
        {
            Bind<bool[]>((something) =>
            {
                
            }, nameof(ViewModel.NeededSelection));
            Bind<bool[]>((somethingElse) =>
            {

            }, nameof(ViewModel.PlayerSelections));
        }

        public void OnNotARobotButtonPressed()
        {
            ViewModel.OnNotARobotButtonPressed();
        }

    }
}
