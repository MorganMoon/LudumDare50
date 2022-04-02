using Cerberus;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.States.MiniGame.ClickABunch
{
    public enum MiniGameClickABunchStateEvent
    {

    }

    public class MiniGameClickABunchState : State, ITickable
    {
        private readonly IStateController<MiniGameStateEvent> _miniGameStateController;

        private int _clickCount = 0;

        public MiniGameClickABunchState(IStateController<MiniGameStateEvent> miniGameStateController)
        {
            _miniGameStateController = miniGameStateController;
        }

        public override void OnEnter()
        {
            Debug.Log("ClickABunch game started!");
        }

        public override void OnExit()
        {
            Debug.Log("ClickABunch game finished!");
        }

        public void Tick()
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                _clickCount++;
            }

            if(_clickCount >= 10)
            {
                _miniGameStateController.TriggerEvent(MiniGameStateEvent.Success);
            }
        }
    }
}
