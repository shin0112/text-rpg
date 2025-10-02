using TEXT_RPG.Core;
using TEXT_RPG.Core.DTO;
using TEXT_RPG.Repository;

namespace TEXT_RPG.Scenes.Dungeon
{
    internal class DungeonEnterScene : DungeonSceneBase
    {
        protected override string Title => "던전 입장";
        public override string[] Options => ["나가기", "쉬운 던전", "일반 던전", "어려운 던전"];

        protected override void HandleInput(int select)
        {
            switch (select)
            {
                case 0:
                    Manager.ChangeScene(SceneType.Start);
                    break;
                default:
                    EnterDungeon((DungeonLevel)select);
                    Manager.ChangeScene(SceneType.DungeonClear);
                    break;
            }
        }

        private void EnterDungeon(DungeonLevel level)
        {
            DungeonInfo dungeonInfo = DungeonRepository.Dungeons[level];
            Player player = Manager.Player;
            Random random = new();

            int defCost = dungeonInfo.RequiredDef - player.DefensePower;

            // 결과 저장 변수
            int hpCost = 0, rewardGold = 0, rewardExp = 0;

            // 방어력 계산
            if (defCost < 0) // 방어력 미달
            {
                if (random.Next(0, 100) < 40)
                {
                    hpCost = player.Hp / 2;
                }
                Manager.LastDungeonResult = new DungeonResultDto(hpCost, rewardGold, rewardExp);
                return;
            }
            else // 방어력 충족
            {
                hpCost += random.Next(20 + defCost, 36 + defCost);
            }

            // 공격력 계산
            int plusReward = random.Next(player.AttackPower, player.AttackPower * 2 + 1);
            rewardGold *= (1 + plusReward / 100);
            rewardExp *= (1 + plusReward / 100);

            // 보상 지정
            Manager.LastDungeonResult = new DungeonResultDto(hpCost, rewardGold, rewardExp);
        }
    }
}
