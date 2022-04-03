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

        [Inject]
        public GameIntroductionState(IScreenService screenService, TiredReasonSettings tiredReasonSettings)
        {
            _screenService = screenService;
            _tiredReasonSettings = tiredReasonSettings;
        }

        public override void OnEnter()
        {
            var randomReason = _tiredReasonSettings.TiredReasons[Random.Range(0, _tiredReasonSettings.TiredReasons.Length)];
            _screenService.SetActiveScreen<GameIntroductionViewModel, string>(randomReason);
        }
    }
}