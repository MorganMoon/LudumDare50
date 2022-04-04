using UnityEngine;

namespace LudumDare50.Client.ViewModels.SelectWifi
{
    public class MiniGameSelectWifiViewModel : ViewModel<string[]>
    {
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

        public void OnExpectedWifiButtonPressed()
        {

        }

        public override void Prepare(string[] parameter)
        {
            WifiNames = parameter;
            ExpectedWifiName = parameter[Random.Range(0, parameter.Length)];
        }
    }
}