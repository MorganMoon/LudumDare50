﻿using Cerberus;
using LudumDare50.Client.Data;
using LudumDare50.Client.Game;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.ViewModels.Energy;
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
        private readonly IStateController<GameplayStateEvent> _gameplayStateController;
        private readonly IGameTime _gameTime;
        private readonly Transform _gameplayArea;

        [Inject]
        public GameplayState(IScreenService screenService, ISleepService sleepService,
            IStateController<GameplayStateEvent> gameplayStateController, IGameTime gameTime,
            [Inject(Id = "GameplayArea")]Transform gameplayArea)
        {
            _screenService = screenService;
            _sleepService = sleepService;
            _gameplayStateController = gameplayStateController;
            _gameTime = gameTime;
            _gameplayArea = gameplayArea;
        }

        public override void OnEnter()
        {
            _gameplayArea.gameObject.SetActive(true);
            _sleepService.Start();
            _gameTime.Start();
            _screenService.SetActiveScreen<EnergyViewModel, Energy>(_sleepService.Energy);
        }

        public override void OnExit()
        {
            _sleepService.Stop();
            _gameTime.Stop();
            _gameplayArea.gameObject.SetActive(false);
        }

        public void Tick()
        {
            if(_screenService.TryGetViewModel<EnergyViewModel>(out var energyViewModel))
            {
                energyViewModel.CurrentEnergy = _sleepService.Energy.Current;
            }

            if(_sleepService.Energy.Current <= 0)
            {
                _gameplayStateController.TriggerEvent(GameplayStateEvent.GameOver);
            }
        }
    }
}
