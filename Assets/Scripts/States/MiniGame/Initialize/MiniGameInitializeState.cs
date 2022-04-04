using Cerberus;
using LudumDare50.Client.Data;
using System;
using Zenject;

namespace LudumDare50.Client.States.MiniGame.Initialize
{
    public enum MiniGameInitializeStateEvent
    {
        Exit,
        PlayClickABunch,
        PlaySpamPopups,
        PlayCaptcha
    }

    public class MiniGameInitializeState : State
    {
        private readonly IStateController<MiniGameInitializeStateEvent> _miniGameInitializeStateController;

        [Inject]
        public MiniGameInitializeState(IStateController<MiniGameInitializeStateEvent> miniGameInitializeStateController)
        {
            _miniGameInitializeStateController = miniGameInitializeStateController;
        }

        public override void OnEnter()
        {
            //var randomMiniGame = (MiniGameType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(MiniGameType)).Length);
            var randomMiniGame = MiniGameType.Captcha;
            //Play some animation bringing up the minigame
            _miniGameInitializeStateController.TriggerEvent(MiniGameTypeToStateEvent(randomMiniGame));
        }

        private MiniGameInitializeStateEvent MiniGameTypeToStateEvent(MiniGameType miniGameType)
        {
            return (MiniGameInitializeStateEvent)(miniGameType + 1);
        }
    }
}
