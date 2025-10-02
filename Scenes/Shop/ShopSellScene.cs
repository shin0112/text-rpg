using TEXT_RPG.Core;

namespace TEXT_RPG.Scenes.Shop
{
    internal class ShopSellScene : ShopSceneBase
    {
        protected override string Title => "상점 - 아이템 판매";
        public override string[] Options => ["나가기"];
        public override byte SelectionCount => (byte)(base.SelectionCount + Manager.Player.Items.Count);

        protected override void HandleInput(int select)
        {
            if (select == 0)
            {
                Manager.ChangeScene(SceneType.Shop);
                ToggleInventoryNumbered();
            }
            else if (0 < select && select <= Manager.Player.Items.Count)
            {
                Item item = Manager.Player.Items[select - 1];

                if (item.IsEquipped == true) // 장착 해제
                {
                    item.ToggleEquip();
                }

                Manager.Player.UpdateGold((int)(item.Price * 0.85));
                Manager.Player.RemoveItem(item);
            }
        }
    }
}
