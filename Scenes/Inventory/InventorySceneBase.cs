using TEXT_RPG.UI;

namespace TEXT_RPG.Scenes.Inventory
{
    internal abstract class InventorySceneBase : Scene
    {
        protected void ShowInventoryTitle()
        {
            PlayerUI.ShowInventorytitle(Title);
        }
    }
}
