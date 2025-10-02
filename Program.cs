using TEXT_RPG.UI;

namespace TEXT_RPG
{
    enum PlayerJob { 전사, 마법사, 궁수 }
    enum ItemType { Weapon, Armor }
    enum SceneType { Start, Status, Inventory, InventoryManagement, InventorySort, Shop, ShopPurchase, ShopSell, Dungeon, DungeonClear }
    enum DungeonLevel { Easy = 1, Normal = 2, Hard = 3 }

    internal partial class Program
    {
        static void Main(string[] args)
        {
            ArgumentNullException.ThrowIfNull(args);
            UIHelper.SetInitDesign();

            // run
            while (true)
            {
                Console.Clear();
                UIHelper.WriteHeader();
                GameManager.Instance.ResetHeaderText();
                GameManager.Instance.CurrentScene.Show();
            }
        }
    }
}
