using Cerberus;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.Settings;
using LudumDare50.Client.ViewModels.GameIntroduction;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.States.GameIntroduction
{
    public enum GameIntroductionStateEvent
    {
        Continue
    }

    public class GameIntroductionState : State
    {
        private readonly IScreenService _screenService;
        private readonly TiredReasonSettings _tiredReasonSettings;
        private readonly Transform _startupArea;

        [Inject]
        public GameIntroductionState(IScreenService screenService, TiredReasonSettings tiredReasonSettings, [Inject(Id = "StartupArea")] Transform startupArea)
        {
            _screenService = screenService;
            _tiredReasonSettings = tiredReasonSettings;
            _startupArea = startupArea;
        }

        public override void OnEnter()
        {
            _startupArea.gameObject.SetActive(true);
            var randomReason = _tiredReasonSettings.TiredReasons[Random.Range(0, _tiredReasonSettings.TiredReasons.Length)];
            _screenService.SetActiveScreen<GameIntroductionViewModel, string>(randomReason);
        }

        public override void OnExit()
        {
            _startupArea.gameObject.SetActive(false);
        }
    }
}