using Cerberus;
using LudumDare50.Client.States.MiniGame;
using System;
using Zenject;

namespace LudumDare50.Client.ViewModels.Captcha
{
    public class MiniGameCaptchaViewModel : ViewModel
    {
        private readonly IStateController<MiniGameStateEvent> _miniGameStateController;

        private bool[] _neededSelections;
        public bool[] NeededSelection
        {
            get => _neededSelections;
            set
            {
                if (_neededSelections == value)
                {
                    return;
                }
                _neededSelections = value;
                OnPropertyChanged();
            }
        }

        private bool[] _playerSelections;
        public bool[] PlayerSelections
        {
            get => _playerSelections;
            set
            {
                if (_playerSelections == value)
                {
                    return;
                }
                _playerSelections = value;
                OnPropertyChanged();
            }
        }

        [Inject]
        public MiniGameCaptchaViewModel(IStateController<MiniGameStateEvent> miniGameStateController)
        {
            _miniGameStateController = miniGameStateController;
        }

        public void Startup()
        {
            //Randomize which buttons need to be selected
            var random = new Random();
            random.Next(PlayerSelections.Length);
        }

        public void OnNotARobotButtonPressed()
        {
            _miniGameStateController.TriggerEvent(MiniGameStateEvent.Success);
        }
    }
}
