using TEXT_RPG.UI;

namespace TEXT_RPG.Scenes.Dungeon
{
    internal abstract class DungeonSceneBase : Scene
    {
        public override void Show()
        {
            UIHelper.WriteTitle(Title);
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            UIHelper.WriteOptions();
            int select = Manager.SelectAct();
            HandleInput(select);
        }
    }
}
