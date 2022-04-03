using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare50.Client.ViewModels
{
    public class MiniGameView : View<MiniGameViewModel>
    {
        void Start()
        {
            
        }

        void Update()
        {
            
        }

        public void OnExitButtonPressed()
        {
            ViewModel.OnExitButtonPressed();
        }
    }
}

