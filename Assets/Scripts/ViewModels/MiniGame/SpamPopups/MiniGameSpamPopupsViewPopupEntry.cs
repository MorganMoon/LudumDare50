using UnityEngine;
using Zenject;

namespace LudumDare50.Client.ViewModels.ClickABunch
{
    public class MiniGameSpamPopupsViewPopupEntry : MonoBehaviour
    {
        public int PopupIndex { get; private set; }
        private MiniGameSpamPopupsView _view;

        [Inject]
        public void Inject(int popupIndex, MiniGameSpamPopupsView miniGameSpamPopupsView)
        {
            PopupIndex = popupIndex;
            _view = miniGameSpamPopupsView;
        }

        public void OnCloseButtonPressed()
        {
            _view.OnClosePopupButtonPressed(PopupIndex);
        }

        public class Factory : PlaceholderFactory<int, MiniGameSpamPopupsView, MiniGameSpamPopupsViewPopupEntry>
        {

        }
    }
}
