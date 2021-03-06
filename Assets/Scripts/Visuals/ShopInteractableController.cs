using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LudumDare50.Client.Data;
using LudumDare50.Client.Game;
using Zenject;
using LudumDare50.Client.Settings;

namespace LudumDare50.Client.Visuals
{
    public class ShopInteractableController : MonoBehaviour
    {
        [SerializeField]
        private int _costItemCount;

        [SerializeField]
        private InventoryItemType _purchaseableItemType;

        [SerializeField]
        private Animator _dialogBubbleAnimator;

        [Inject]
        private IInventory _inventory;

        [Inject]
        private IStatusEffectService _statusEffectService;

        [Inject]
        private SoundsSettings _soundsSettings;

        public void TryPurchaseItem()
        {
            if (_inventory.CanAfford(new InventoryItem(InventoryItemType.Money, _costItemCount)))
            {
                var curMoney = _inventory.GetItem(InventoryItemType.Money);
                var updatedMoney = curMoney.Count - _costItemCount;
                _inventory.SetItem(new InventoryItem(InventoryItemType.Money, updatedMoney));
                _statusEffectService.TryApplyEffect(_purchaseableItemType);
                AudioSource.PlayClipAtPoint(_soundsSettings.coffeeDrinking, Vector3.zero, _soundsSettings.coffeeDrinkingVolume);
            }
            else
            {
                _dialogBubbleAnimator.SetTrigger("StartBubble");
            }
        }
    }
}
