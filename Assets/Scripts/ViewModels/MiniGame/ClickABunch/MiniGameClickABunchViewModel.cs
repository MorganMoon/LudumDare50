using Cerberus;
using LudumDare50.Client.States.MiniGame;
using Zenject;

namespace LudumDare50.Client.ViewModels.ClickABunch
{
    public class MiniGameClickABunchViewModel : ViewModel
    {
        private readonly IStateController<MiniGameStateEvent> _miniGameStateController;

        private int _clickCount;
        public int ClickCount
        {
            get => _clickCount;
            set
            {
                if(_clickCount == value)
                {
                    return;
                }
                _clickCount = value;
                OnPropertyChanged();
            }
        }

        public int NeededClickCount => 15;

        [Inject]
        public MiniGameClickABunchViewModel(IStateController<MiniGameStateEvent> miniGameStateController)
        {
            _miniGameStateController = miniGameStateController;
        }

        public void OnClickButtonPressed()
        {
            ClickCount += 1;

            if(ClickCount >= NeededClickCount)
            {
                _miniGameStateController.TriggerEvent(MiniGameStateEvent.Success);
            }
        }
    }
}