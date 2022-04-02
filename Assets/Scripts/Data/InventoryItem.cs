namespace LudumDare50.Client.Data
{
    public enum InventoryItemType
    {
        Money
    }

    public struct InventoryItem
    {
        public InventoryItemType Type { get; }
        public int Count { get; }

        public InventoryItem(InventoryItemType type, int count)
        {
            Type = type;
            Count = count;
        }
    }
}