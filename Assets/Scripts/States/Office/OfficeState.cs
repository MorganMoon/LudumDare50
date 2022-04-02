using Cerberus;
using LudumDare50.Client.Game;
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
        private readonly ISleepService _sleepService;

        [Inject]
        public OfficeState(IStateController<OfficeStateEvent> officeStateController, ISleepService sleepService)
        {
            _officeStateController = officeStateController;
            _sleepService = sleepService;
        }

        public override void OnEnter()
        {
            Debug.Log("Welcome to the office! Press 'Spacebar' to start a minigame!");
        }

        public void Tick()
        {
            if(Input.GetKey(KeyCode.Space))
            {
                if(_sleepService.TrySleep())
                {
                    Debug.Log("Zzzzz");
                }
                else
                {
                    Debug.Log("nosleep");
                }
                //_officeStateController.TriggerEvent(OfficeStateEvent.StartMiniGame);
            }
        }
    }
}
