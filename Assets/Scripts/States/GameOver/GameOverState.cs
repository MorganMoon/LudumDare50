using Cerberus;
using LudumDare50.Client.Game;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.ViewModels.GameOver;
using Zenject;

namespace LudumDare50.Client.States.GameOver
{
    public enum GameOverStateEvent
    {
        Continue
    }

    public class GameOverState : State
    {
        private readonly IScreenService _screenService;
        private readonly IGameTime _gameTime;

        [Inject]
        public GameOverState(IScreenService screenService, IGameTime gameTime)
        {
            _screenService = screenService;
            _gameTime = gameTime;
        }

        public override void OnEnter()
        {
            _screenService.SetActiveScreen<GameOverViewModel, GameOverViewModel.PrepareData>(new GameOverViewModel.PrepareData()
            {
                TotalTime = _gameTime.EndTime - _gameTime.StartTime,
            });
        }
    }
}