using TEXT_RPG.Core.DTO;
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
            DungeonResultDto dto = Manager.LastDungeonResult!;

            UIHelper.WriteTitle(Title);
            Console.WriteLine("축하합니다!!");
            Console.WriteLine($"{dto.Level} 던전을 클리어 하였습니다.");

            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"체력 {dto.BeforeHp} -> {dto.AfterHp}");
            Console.WriteLine($"Gold {dto.BeforeGold} G -> {dto.AfterGold} G");

            UIHelper.WriteOptions();
            int select = Manager.SelectAct();
            HandleInput(select);
        }
    }
}
