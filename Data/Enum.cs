namespace TEXT_RPG.Data
{
    internal enum PlayerJob { 전사, 마법사, 궁수 }
    internal enum ItemType { Weapon, Armor }
    internal enum SceneType
    {
        Start, Status, Inventory, InventoryManagement,
        InventorySort, Shop, ShopPurchase, ShopSell,
        Dungeon, DungeonClear, Rest
    }
    internal enum DungeonLevel { 쉬운 = 1, 일반 = 2, 어려운 = 3 }
}
