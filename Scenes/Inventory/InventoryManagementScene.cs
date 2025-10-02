using TEXT_RPG.Data;

namespace TEXT_RPG.Scenes.Inventory
{
    internal class InventoryManagementScene : InventorySceneBase
    {
        protected override string Title => "인벤토리 - 장착 관리";
        public override string[] Options => ["나가기"];
        public override byte SelectionCount => (byte)(Options.Length + Manager.Player.Items.Count);

        protected override void HandleInput(int select)
        {
            if (select == 0)
            {
                Manager.ChangeScene(SceneType.Inventory);
                ToggleInventoryNumbered();
            }
            else if (0 < select && select <= Manager.Player.Items.Count)
            {
                Manager.Player.ToggleEquip(Manager.Player.Items[select - 1]);
            }
        }
    }
}
