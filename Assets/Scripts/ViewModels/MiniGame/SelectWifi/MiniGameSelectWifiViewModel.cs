using Cerberus;
using LudumDare50.Client.States.MiniGame;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.ViewModels.SelectWifi
{
    public class MiniGameSelectWifiViewModel : ViewModel<string[]>
    {
        private readonly IStateController<MiniGameStateEvent> _miniGameStateController;

        private string[] _wifiNames;
        public string[] WifiNames
        {
            get => _wifiNames;
            set
            {
                if(_wifiNames == value)
                {
                    return;
                }
                _wifiNames = value;
                OnPropertyChanged();
            }
        }

        private string _expectedWifiName;
        public string ExpectedWifiName
        {
            get => _expectedWifiName;
            set
            {
                if(_expectedWifiName == value)
                {
                    return;
                }
                _expectedWifiName = value;
                OnPropertyChanged();
            }
        }
        
        [Inject]
        public MiniGameSelectWifiViewModel(IStateController<MiniGameStateEvent> miniGameStateController)
        {
            _miniGameStateController = miniGameStateController;
        }

        public override void Prepare(string[] parameter)
        {
            WifiNames = parameter;
            ExpectedWifiName = parameter[Random.Range(0, parameter.Length)];
        }

        public void OnExpectedWifiButtonPressed()
        {
            _miniGameStateController.TriggerEvent(MiniGameStateEvent.Success);
        }
    }
}