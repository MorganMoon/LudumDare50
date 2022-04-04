using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.ViewModels.ClickABunch
{
    public class MiniGameSpamPopupsView : View<MiniGameSpamPopupsViewModel>
    {
        [Inject]
        private MiniGameSpamPopupsViewPopupEntry.Factory _entryFactory;
        [SerializeField]
        private Transform _entryParent;

        private readonly Dictionary<int, MiniGameSpamPopupsViewPopupEntry> _popupEntries = new Dictionary<int, MiniGameSpamPopupsViewPopupEntry>();

        protected override void SetBindings()
        {
            Bind<bool[]>(UpdateEntries, nameof(ViewModel.ActivePopups));
        }

        public void OnClosePopupButtonPressed(int popupIndex)
        {
            ViewModel.OnPopupClosedButtonPressed(popupIndex);
            UpdateEntries(ViewModel.ActivePopups);
        }

        private void UpdateEntries(bool[] popups)
        {
            for(int index = 0; index < popups.Length; index++)
            {
                var popup = popups[index];
                if(_popupEntries.ContainsKey(index))
                {
                    if(!popup)
                    {
                        Destroy(_popupEntries[index].gameObject);
                        _popupEntries.Remove(index);
                    }
                }
                else if(popup)
                {
                    var entry = _entryFactory.Create(index, this);
                    entry.transform.SetParent(_entryParent, false);
                    entry.transform.localPosition = new Vector3(Random.Range(-392f, 295f), Random.Range(-230f, 230f), 0);
                    _popupEntries[index] = entry;
                }
            }
        }
    }
}
