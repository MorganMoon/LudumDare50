using Cerberus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LudumDare50.Client.StateHandler;
using LudumDare50.Client.States.MiniGame;
using Zenject;

namespace LudumDare50.Client.ViewModels
{
    public class MiniGameViewModel : ViewModel
    {
        private readonly IStateController<MiniGameStateEvent> _miniGameStateController;

        [Inject]
        public MiniGameViewModel(IStateController<MiniGameStateEvent> miniGameStateController)
        {
            _miniGameStateController = miniGameStateController;
        }

        public void OnExitButtonPressed()
        {
            _miniGameStateController.TriggerEvent(MiniGameStateEvent.Failure);
        }
    }
}
    
