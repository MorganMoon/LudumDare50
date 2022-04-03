using Cerberus;
using LudumDare50.Client.Game;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.ViewModels.GameOver;
using UnityEngine;
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
        private readonly Transform _startupArea;

        [Inject]
        public GameOverState(IScreenService screenService, IGameTime gameTime, [Inject(Id = "StartupArea")] Transform startupArea)
        {
            _screenService = screenService;
            _gameTime = gameTime;
            _startupArea = startupArea;
        }

        public override void OnEnter()
        {
            _startupArea.gameObject.SetActive(true);
            _screenService.SetActiveScreen<GameOverViewModel, GameOverViewModel.PrepareData>(new GameOverViewModel.PrepareData()
            {
                TotalTime = _gameTime.EndTime - _gameTime.StartTime,
            });
        }

        public override void OnExit()
        {
            _startupArea.gameObject.SetActive(false);
        }
    }
}