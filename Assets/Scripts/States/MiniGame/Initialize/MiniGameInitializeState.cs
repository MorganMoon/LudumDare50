using Cerberus;
using LudumDare50.Client.Data;
using System;

namespace LudumDare50.Client.States.MiniGame.Initialize
{
    public enum MiniGameInitializeStateEvent
    {
        Exit,
        PlayClickABunch
    }

    public class MiniGameInitializeState : State
    {
        public override void OnEnter()
        {
            var randomMiniGame = (MiniGameType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(MiniGameType)).Length);
            //Play some animation bringing up the minigame
        }
    }
}
