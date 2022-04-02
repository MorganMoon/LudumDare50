using Cerberus;
using LudumDare50.Client.Game;

namespace LudumDare50.Client.States.MiniGame
{
    public enum MiniGameStateEvent
    {
        Success
    }

    public enum MiniGameStateSubState
    {
        Initialize,
        ClickABunchMiniGame
    }

    public class MiniGameState : State
    {
        private readonly ITaskService _taskService;

        public MiniGameState(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public override void OnExit()
        {
            _taskService.Reset();
        }
    }
}
