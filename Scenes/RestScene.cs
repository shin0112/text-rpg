using TEXT_RPG.Data;
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
                if (Manager.Player.Gold < 500)
                {
                    Manager.HeaderText = "Gold가 부족합니다.";
                }
                else
                {
                    Manager.Player.AddGold(-500);
                    Manager.Player.AddStamina(20);
                    Manager.Player.AddHp(100);
                    Manager.HeaderText = "휴식을 완료했습니다.";
                }
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
