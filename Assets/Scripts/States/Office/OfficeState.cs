using Cerberus;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.States.OfficeState
{
    public enum OfficeStateEvent
    {
        StartMiniGame
    }

    public class OfficeState : State, ITickable
    {
        private readonly IStateController<OfficeStateEvent> _officeStateController;

        [Inject]
        public OfficeState(IStateController<OfficeStateEvent> officeStateController)
        {
            _officeStateController = officeStateController;
        }

        public override void OnEnter()
        {
            Debug.Log("Welcome to the office! Press 'Spacebar' to start a minigame!");
        }

        public void Tick()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _officeStateController.TriggerEvent(OfficeStateEvent.StartMiniGame);
            }
        }
    }
}
