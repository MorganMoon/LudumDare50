using Cerberus;
using LudumDare50.Client.Infrastructure;
using Zenject;

namespace LudumDare50.Client.States.Gameplay
{
    public enum GameplayStateEvent
    {

    }

    public class GameplayState : State
    {
        private readonly IScreenService _screenService;

        [Inject]
        public GameplayState(IScreenService screenService)
        {
            _screenService = screenService;
        }

        public override void OnEnter()
        {
            _screenService.ClearScreen();
        }
    }
}
