using Cerberus;
using LudumDare50.Client.States.MiniGame;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.ViewModels.MiniGame.EnterPassword
{
    public class EnterPasswordViewModel : ViewModel
    {
        private readonly IStateController<MiniGameStateEvent> _miniGameStateController;

        public EnterPasswordViewModel(IStateController<MiniGameStateEvent> miniGameStateController)
        {
            _miniGameStateController = miniGameStateController;
        }
        
        public void OnSubmit(string submittedText)
        {
            Debug.Log($"SUBMITTED TEXT: {submittedText}");
            _miniGameStateController.TriggerEvent(MiniGameStateEvent.Success);
        }
    }
}
    
