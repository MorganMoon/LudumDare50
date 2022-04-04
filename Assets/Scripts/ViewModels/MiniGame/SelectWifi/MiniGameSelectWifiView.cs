using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.ViewModels.SelectWifi
{
    public class MiniGameSelectWifiView : View<MiniGameSelectWifiViewModel>
    {
        [Inject]
        private MiniGameSelectWifiViewEntry.Factory _entryFactory;
        [SerializeField]
        private TMP_Text _wifiNameNoteText;
        [SerializeField]
        private Transform _entryParent;

        private readonly List<MiniGameSelectWifiViewEntry> _entries = new List<MiniGameSelectWifiViewEntry>();

        protected override void SetBindings()
        {
            Bind<string[]>((wifiNames) =>
            {
                foreach(var entry in _entries)
                {
                    Destroy(entry);
                }
                _entries.Clear();
                foreach(var wifiName in wifiNames)
                {
                    var entry = _entryFactory.Create(wifiName, wifiName == ViewModel.ExpectedWifiName, ViewModel);
                    entry.transform.SetParent(_entryParent, false);
                    _entries.Add(entry);
                }
            }, nameof(ViewModel.WifiNames));
            Bind<string>((expectedWifiName) =>
            {
                _wifiNameNoteText.text = expectedWifiName;
                foreach (var entry in _entries)
                {
                    entry.IsExpected = entry.WifiName == expectedWifiName;
                }
            }, nameof(ViewModel.ExpectedWifiName));
        }
    }
}
