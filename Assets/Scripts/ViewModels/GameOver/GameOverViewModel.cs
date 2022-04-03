using System;
using Cerberus;
using LudumDare50.Client.States.GameOver;
using Zenject;
using static LudumDare50.Client.ViewModels.GameOver.GameOverViewModel;

namespace LudumDare50.Client.ViewModels.GameOver
{
    public class GameOverViewModel : ViewModel<PrepareData>
    {
        private readonly IStateController<GameOverStateEvent> _gameOverStateController;

        private TimeSpan _totalTime;
        public TimeSpan TotalTime
        {
            get => _totalTime;
            set
            {
                if(_totalTime == value)
                {
                    return;
                }
                _totalTime = value;
                OnPropertyChanged();
            }
        }

        [Inject]
        public GameOverViewModel(IStateController<GameOverStateEvent> gameOverStateEvent)
        {
            _gameOverStateController = gameOverStateEvent;
        }

        public override void Prepare(PrepareData parameter)
        {
            TotalTime = parameter.TotalTime;
        }

        public void OnContinueButtonPressed()
        {
            _gameOverStateController.TriggerEvent(GameOverStateEvent.Continue);
        }

        public class PrepareData
        {
            public TimeSpan TotalTime { get; set; }
        }
    }
}