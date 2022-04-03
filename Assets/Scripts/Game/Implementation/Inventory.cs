using System;
using System.Collections.Generic;
using LudumDare50.Client.Data;

namespace LudumDare50.Client.Game.Implementation
{
    public class Inventory : IInventory
    {
        public event Action OnInventoryUpdated;

        private readonly Dictionary<InventoryItemType, int> _inventoryCounts = new Dictionary<InventoryItemType, int>();

        public InventoryItem GetItem(InventoryItemType inventoryItemType)
        {
            _inventoryCounts.TryGetValue(inventoryItemType, out var count);
            return new InventoryItem(inventoryItemType, count);
        }

        public void SetItem(InventoryItem inventoryItem)
        {
            _inventoryCounts[inventoryItem.Type] = inventoryItem.Count;

            OnInventoryUpdated?.Invoke();
        }

        public bool CanAfford(InventoryItem inventoryItem)
        {
            var currentAmount = GetItem(inventoryItem.Type);

            var temp = currentAmount.Count >= inventoryItem.Count;
            return temp;
        }
    }
}