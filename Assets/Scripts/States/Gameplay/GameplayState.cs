using Cerberus;
using LudumDare50.Client.Data;
using LudumDare50.Client.Game;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.ViewModels.Energy;
using LudumDare50.Client.ViewModels.SupervisorAwareness;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.States.Gameplay
{
    public enum GameplayStateEvent
    {
        GameOver
    }

    public enum GameplayStateSubState
    {
        Office,
        MiniGame
    }

    public class GameplayState : State, ITickable
    {
        private readonly IScreenService _screenService;
        private readonly ISleepService _sleepService;
        private readonly ITaskService _taskService;
        private readonly IStateController<GameplayStateEvent> _gameplayStateController;
        private readonly IGameTime _gameTime;
        private readonly ZenjectSceneLoader _zenjectSceneLoader;

        [Inject]
        public GameplayState(IScreenService screenService, ISleepService sleepService, ITaskService taskService,
            IStateController<GameplayStateEvent> gameplayStateController, IGameTime gameTime,
            ZenjectSceneLoader zenjectSceneLoader)
        {
            _screenService = screenService;
            _sleepService = sleepService;
            _taskService = taskService;
            _gameplayStateController = gameplayStateController;
            _gameTime = gameTime;
            _zenjectSceneLoader = zenjectSceneLoader;
        }

        public override void OnEnter()
        {
            _zenjectSceneLoader.LoadScene("GameplayScene", UnityEngine.SceneManagement.LoadSceneMode.Additive);
            _sleepService.Start();
            _gameTime.Start();
            _taskService.Start();
            _screenService.SetActiveScreen()
                .With<EnergyViewModel, Energy>(_sleepService.Energy)
                .With<MoneyViewModel>()
                .With<SupervisorAwarenessViewModel, SupervisorAwareness>(_taskService.SupervisorAwareness)
                .Build();
        }

        public override void OnExit()
        {
            _sleepService.Stop();
            _gameTime.Stop();
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("GameplayScene");
        }

        public void Tick()
        {
            if(_screenService.TryGetViewModel<EnergyViewModel>(out var energyViewModel))
            {
                energyViewModel.CurrentEnergy = _sleepService.Energy.Current;
                energyViewModel.CanSleep = !_sleepService.IsCaught;
            }

            if (_screenService.TryGetViewModel<SupervisorAwarenessViewModel>(out var supervisorViewModel))
            {
                supervisorViewModel.CurrentFailures = _taskService.SupervisorAwareness.Current;
            }

            if (_sleepService.Energy.Current <= 0 || _taskService.SupervisorAwareness.Current >= _taskService.SupervisorAwareness.Max)
            {
                _gameplayStateController.TriggerEvent(GameplayStateEvent.GameOver);
            }
        }
    }
}
