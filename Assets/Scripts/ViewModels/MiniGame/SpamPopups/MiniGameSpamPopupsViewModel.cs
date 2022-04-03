using Cerberus;
using LudumDare50.Client.States.MiniGame;
using Zenject;

namespace LudumDare50.Client.ViewModels.ClickABunch
{
    public class MiniGameSpamPopupsViewModel : ViewModel<int>
    {
        private readonly IStateController<MiniGameStateEvent> _miniGameStateController;

        private bool[] _activePopups;
        public bool[] ActivePopups
        {
            get => _activePopups;
            set
            {
                if(_activePopups == value)
                {
                    return;
                }
                _activePopups = value;
                OnPropertyChanged();
            }
        }

        [Inject]
        public MiniGameSpamPopupsViewModel(IStateController<MiniGameStateEvent> miniGameStateController)
        {
            _miniGameStateController = miniGameStateController;
        }

        public override void Prepare(int parameter)
        {
            var activePopups = new bool[parameter];
            for(int i = 0; i < parameter; i++)
            {
                activePopups[i] = true;
            }
            ActivePopups = activePopups;
        }

        public void OnPopupClosedButtonPressed(int popupIndex)
        {
            ActivePopups[popupIndex] = false;

            foreach(var popup in ActivePopups)
            {
                if(popup)
                {
                    return;
                }
            }
            _miniGameStateController.TriggerEvent(MiniGameStateEvent.Success);
        }
    }
}