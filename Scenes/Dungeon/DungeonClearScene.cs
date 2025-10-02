
using TEXT_RPG.Data;
using TEXT_RPG.UI;

namespace TEXT_RPG.Scenes.Dungeon
{
    internal class DungeonClearScene : DungeonSceneBase
    {
        protected override string Title => "던전 클리어";
        public override string[] Options => ["나가기"];

        protected override void HandleInput(int select)
        {
            if (select == 0)
            {
                Manager.ChangeScene(SceneType.Start);
            }
        }

        public override void Show()
        {
            DungeonUI.ShowClearInfo(Title);
            UIHelper.WriteOptions();
            int select = Manager.SelectAct();
            HandleInput(select);
        }
    }
}
