using TEXT_RPG.Data;

namespace TEXT_RPG.Scenes.Inventory
{
    internal class InventoryDefaultScene : InventorySceneBase
    {
        protected override string Title => "인벤토리";
        public override string[] Options => ["나가기", "장착 관리", "아이템 정렬"];

        protected override void HandleInput(int select)
        {
            switch (select)
            {
                case 0:
                    Manager.ChangeScene(SceneType.Start);
                    break;
                case 1:
                    Manager.ChangeScene(SceneType.InventoryManagement);
                    ToggleInventoryNumbered();
                    break;
                case 2:
                    Manager.ChangeScene(SceneType.InventorySort);
                    break;
                default:
                    break;
            }
        }
    }
}
