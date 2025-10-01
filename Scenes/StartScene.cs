using TEXT_RPG.Core;
using TEXT_RPG.UI;

namespace TEXT_RPG.Scenes
{
    internal class StartScene : Scene
    {
        protected override string Title => "";
        public override string[] Options => ["", "상태 보기", "인벤토리", "랜덤 모험", "마을 순찰하기", "훈련하기", "상점"];

        public override void Show()
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
            UIHelper.WriteOptions();

            int select = Manager.SelectAct(SceneType.Start);

            HandleInput(select);
        }

        protected override void HandleInput(int select)
        {
            switch (select)
            {
                case 1:
                    Manager.ChangeScene(SceneType.Status);
                    break;
                case 2:
                    Manager.ChangeScene(SceneType.Inventory);
                    break;
                case 3:
                    SelectRandomAdventure();
                    break;
                case 4:
                    SelectPatrolTown();
                    break;
                case 5:
                    SelectTraining();
                    break;
                case 6:
                    Manager.ChangeScene(SceneType.Shop);
                    break;
                default:
                    UIHelper.WarnBadInput();
                    break;
            }
        }

        private void SelectTraining()
        {
            Player player = Manager.Player;

            int encounterProb = new Random().Next(100);
            if (player.SpendStamina(15))
            {
                if (encounterProb < 15)
                {
                    Console.WriteLine("훈련이 잘 되었습니다!");
                    player.UpdateExp(60);
                }
                else if (encounterProb < 60)
                {
                    Console.WriteLine("오늘하루 열심히 훈련했습니다.");
                    player.UpdateExp(40);
                }
                else
                {
                    Console.WriteLine("하기 싫다... 훈련이...");
                    player.UpdateExp(30);
                }
            }
        }

        private void SelectPatrolTown()
        {
            Player player = Manager.Player;

            int encounterProb = new Random().Next(100);
            if (player.SpendStamina(5))
            {
                if (encounterProb < 10)
                {
                    Console.WriteLine("마을 아이들이 모여있다. 간식을 사줘볼까?");
                    player.UpdateGold(-500);
                }
                else if (encounterProb < 20)
                {
                    Console.WriteLine("촌장님을 만나서 심부름을 했다.");
                    player.UpdateGold(2000);
                }
                else if (encounterProb < 40)
                {
                    Console.WriteLine("길 읽은 사람을 안내해주었다.");
                    player.UpdateGold(1000);
                }
                else if (encounterProb < 70)
                {
                    Console.WriteLine("마을 주민과 인사를 나눴다. 선물을 받았다.");
                    player.UpdateGold(500);
                }
                else
                {
                    Console.WriteLine("아무 일도 일어나지 않았다");
                }
            }
        }

        private void SelectRandomAdventure()
        {
            Player player = Manager.Player;

            int encounterProb = new Random().Next(2);
            if (player.SpendStamina(10))
            {
                switch (encounterProb)
                {
                    case 0:
                        Console.WriteLine("몬스터 조우! 골드 500 획득");
                        player.UpdateGold(500);
                        break;
                    case 1:
                        Console.WriteLine("아무 일도 일어나지 않았다");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
