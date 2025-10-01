namespace TEXT_RPG.Scenes.Shop
{
    internal class ShopPurchaseScene : Scene
    {
        protected override string Title => "상점";
        public override string[] Options => ["나가기"];

        protected override void HandleInput(int select)
        {
            throw new NotImplementedException();
        }

        public override void Show()
        {
            int select;
            UI.PlayerUI.ShowPlayerInfo();
            UI.UIHelper.WriteOptions();
            select = Manager.SelectAct(SceneType.Status);
            if (select == 0)
            {
                Console.Clear();
                return;
            }
        }
    }
}
