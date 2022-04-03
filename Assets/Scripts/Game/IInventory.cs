using LudumDare50.Client.Data;
using System;

namespace LudumDare50.Client.Game
{
    public interface IInventory
    {
        event Action OnInventoryUpdated;

        InventoryItem GetItem(InventoryItemType inventoryItemType);
        void SetItem(InventoryItem inventoryItem);
        bool CanAfford(InventoryItem inventoryItem);
    }
}