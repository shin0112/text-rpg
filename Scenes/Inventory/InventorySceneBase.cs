using TEXT_RPG.UI;

namespace TEXT_RPG.Scenes.Inventory
{
    internal abstract class InventorySceneBase : Scene
    {
        protected void ShowInventoryTitle()
        {
            PlayerUI.ShowInventoryHeader(Title);
        }

        public override void Show()
        {
            ShowInventoryTitle();
            PlayerUI.ShowInventory(Manager.Player.Items, Manager.InventoryNumbered);
            UIHelper.WriteOptions();
            int select = Manager.SelectAct();
            HandleInput(select);
        }

        public void ToggleInventoryNumbered() => Manager.InventoryNumbered = !Manager.InventoryNumbered;
    }
}
