using TEXT_RPG.UI;

namespace TEXT_RPG.Scenes.Shop
{
    internal abstract class ShopSceneBase : Scene
    {
        public override void Show()
        {
            ShopUI.ShowShop(Title);
            UIHelper.WriteOptions();
            int select = Manager.SelectAct();
            HandleInput(select);
        }

        public void ToggleShopNumbered() => Manager.ShopNumbered = !Manager.ShopNumbered;
    }
}
