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
            ViewModel.Toggles = _toggleList;
        }

        public void OnNotARobotButtonPressed()
        {
            var playerSelectionList = new List<bool>();
            foreach(var toggle in _toggleList)
            {
                playerSelectionList.Add(toggle.isOn);
            }

            ViewModel.OnNotARobotButtonPressed(playerSelectionList);
        }

    }
}
