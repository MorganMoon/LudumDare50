using LudumDare50.Client.Data;

namespace LudumDare50.Client.Game
{
    public interface IInventory
    {
        InventoryItem GetItem(InventoryItemType inventoryItemType);
        void SetItem(InventoryItem inventoryItem);
        bool CanAfford(InventoryItem inventoryItem);
    }
}