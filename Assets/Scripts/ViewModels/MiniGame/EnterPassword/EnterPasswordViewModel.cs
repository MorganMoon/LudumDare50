using Cerberus;
using LudumDare50.Client.Settings;
using LudumDare50.Client.States.MiniGame;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.ViewModels.MiniGame.EnterPassword
{
    public class EnterPasswordViewModel : ViewModel<string>
    {
        private readonly IStateController<MiniGameStateEvent> _miniGameStateController;
        
        public string Password { get; set; }

        [Inject]
        public EnterPasswordViewModel(IStateController<MiniGameStateEvent> miniGameStateController)
        {
            _miniGameStateController = miniGameStateController;
        }

        public override void Prepare(string password)
        {
            Password = password;
        }
        
        public void OnCorrectSubmission()
        {
            _miniGameStateController.TriggerEvent(MiniGameStateEvent.Success);
        }
    }
}
    
