using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.ViewModels.SelectWifi
{
    public class MiniGameSelectWifiViewEntry : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _wifiNameText;
        [SerializeField]
        private GameObject _lockIcon;
        [SerializeField]
        private GameObject _greenCheckIcon;
        [SerializeField]
        private GameObject _redXIcon;
        private MiniGameSelectWifiViewModel _miniGameSelectWifiViewModel;

        private Coroutine _showWrongCoroutine = null;

        private bool _isExpected;
        public bool IsExpected
        {
            get => _isExpected;
            set
            {
                _isExpected = value;
                _lockIcon.SetActive(!IsExpected);
            }
        }

        private string _wifiName;
        public string WifiName
        {
            get => _wifiName;
            set
            {
                if(_wifiName == value)
                {
                    return;
                }
                _wifiName = value;
                _wifiNameText.text = value;
            }
        }

        [Inject]
        public void Inject(string wifiName, bool isExpected, MiniGameSelectWifiViewModel miniGameSelectWifiViewModel)
        {
            WifiName = wifiName;
            IsExpected = isExpected;
            _miniGameSelectWifiViewModel = miniGameSelectWifiViewModel;
        }

        public void OnConnectButtonPressed()
        {
            if(IsExpected)
            {
                _miniGameSelectWifiViewModel.OnExpectedWifiButtonPressed();
            }
            else
            {
                if(_showWrongCoroutine != null)
                {
                    StopCoroutine(_showWrongCoroutine);
                }
                _showWrongCoroutine = StartCoroutine(ShowWrong());
            }
        }

        private IEnumerator ShowWrong()
        {
            _greenCheckIcon.SetActive(false);
            _redXIcon.SetActive(true);
            yield return new WaitForSeconds(3f);
            _redXIcon.SetActive(false);
            _greenCheckIcon.SetActive(true);
        }

        public class Factory : PlaceholderFactory<string, bool, MiniGameSelectWifiViewModel, MiniGameSelectWifiViewEntry>
        {

        }
    }
}
