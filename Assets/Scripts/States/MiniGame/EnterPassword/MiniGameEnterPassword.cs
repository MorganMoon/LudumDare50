using Cerberus;
using UnityEngine;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.ViewModels.MiniGame.EnterPassword;
using Zenject;

namespace LudumDare50.Client.States.MiniGame.EnterPassword
{
    public enum MiniGameEnterPasswordStateEvent
    {

    }

    public class MiniGameEnterPasswordState : State, ITickable
    {
        private readonly IScreenService _screenService;

        public MiniGameEnterPasswordState(IScreenService screenService)
        {
            _screenService = screenService;
        }

        public override void OnEnter()
        {
            _screenService.AddToScreen<EnterPasswordViewModel>();
            Debug.Log("EnterPassword game started!");
        }

        public override void OnExit()
        {
            _screenService.RemoveFromScreen<EnterPasswordViewModel>();
            Debug.Log("EnterPassword game finished!");
        }

        public void Tick()
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("PRESSED ENTER!");
            }
        }
    }
}
