using TEXT_RPG.UI;

namespace TEXT_RPG.Scenes
{
    internal class RestScene : Scene
    {
        protected override string Title => "휴식하기";
        public override string[] Options => ["나가기", "휴식하기"];

        protected override void HandleInput(int select)
        {
            if (select == 0)
            {
                Manager.ChangeScene(SceneType.Start);
            }
            else
            {
                Manager.Player.AddGold(-500);
            }
        }

        public override void Show()
        {
            RestUI.ShowRest(Title);
            int select = Manager.SelectAct();
            HandleInput(select);
        }
    }
}
