using LudumDare50.Client.Data;
using UnityEditor;
using UnityEngine;

namespace LudumDare50.Client.Game
{
    public interface IStatusEffectService
    {
        void TryApplyEffect(InventoryItemType inventoryItemType);
    }
}