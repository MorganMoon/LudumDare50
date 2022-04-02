using Cerberus;
using LudumDare50.Client.Game;
using LudumDare50.Client.Infrastructure;
using Zenject;

namespace LudumDare50.Client.States.Gameplay
{
    public enum GameplayStateEvent
    {

    }

    public enum GameplayStateSubState
    {
        Office,
        MiniGame
    }

    public class GameplayState : State
    {
        private readonly IScreenService _screenService;
        private readonly ISleepService _sleepService;

        [Inject]
        public GameplayState(IScreenService screenService, ISleepService sleepService)
        {
            _screenService = screenService;
            _sleepService = sleepService;
        }

        public override void OnEnter()
        {
            _screenService.ClearScreen();
            _sleepService.Start();
        }

        public override void OnExit()
        {
            _sleepService.Stop();
        }
    }
}
