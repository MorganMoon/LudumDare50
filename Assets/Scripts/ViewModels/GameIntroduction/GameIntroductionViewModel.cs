using Cerberus;
using LudumDare50.Client.States.GameIntroduction;
using Zenject;

namespace LudumDare50.Client.ViewModels.GameIntroduction
{
    public class GameIntroductionViewModel : ViewModel<string>
    {
        private readonly IStateController<GameIntroductionStateEvent> _gameIntroductionStateEvent;

        private string _tiredReason;
        public string TiredReason
        {
            get => _tiredReason;
            set
            {
                if(_tiredReason == value)
                {
                    return;
                }
                _tiredReason = value;
                OnPropertyChanged();
            }
        }

        [Inject]
        public GameIntroductionViewModel(IStateController<GameIntroductionStateEvent> gameIntroductionStateController)
        {
            _gameIntroductionStateEvent = gameIntroductionStateController;
        }

        public void OnContinueButtonPressed()
        {
            _gameIntroductionStateEvent.TriggerEvent(GameIntroductionStateEvent.Continue);
        }

        public override void Prepare(string parameter)
        {
            TiredReason = parameter;
        }
    }
}