using Cerberus;
using LudumDare50.Client.Data;
using LudumDare50.Client.Game;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.ViewModels.Energy;
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

    public class GameplayState : State, ITickable
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
            _sleepService.Start();
            _screenService.SetActiveScreen<EnergyViewModel, Energy>(_sleepService.Energy);
        }

        public override void OnExit()
        {
            _sleepService.Stop();
        }

        public void Tick()
        {
            if(_screenService.TryGetViewModel<EnergyViewModel>(out var energyViewModel))
            {
                energyViewModel.CurrentEnergy = _sleepService.Energy.Current;
            }
        }
    }
}
