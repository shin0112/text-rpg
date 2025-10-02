using TEXT_RPG.Core;
using TEXT_RPG.Core.DTO;
using TEXT_RPG.Data;
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

            int defGap = dungeonInfo.RequiredDef - player.DefensePower;

            // 결과 저장 변수
            int hpLost = 0, rewardGold = 0, rewardExp = 0;

            // 방어력 계산
            if (defGap > 0) // 방어력 미달
            {
                if (random.Next(0, 100) < 40)
                {
                    hpLost = player.Hp / 2;
                }
            }
            else // 방어력 충족
            {
                hpLost += random.Next(20 + defGap, 36 + defGap);

                // 공격력 계산
                int attackBonus = random.Next(player.AttackPower, player.AttackPower * 2 + 1);
                rewardGold = (int)(dungeonInfo.RewardGold * (1 + attackBonus * 0.01));
                rewardExp = (int)(dungeonInfo.RewardExp * (1 + attackBonus * 0.01));
            }

            // after
            int afterHp = Math.Max(0, player.Hp - hpLost);
            int afterGold = player.Gold + rewardGold;
            int afterExp = player.Exp + rewardExp;

            // 보상 지정
            Manager.LastDungeonResult = new DungeonResultDto(
                level,
                player.Hp,
                player.Gold,
                player.Exp,
                afterHp,
                afterGold,
                afterExp
            );

            // 플레이어 정보 갱신
            player.AddHp(-hpLost);
            player.AddGold(rewardGold);
            player.AddExp(rewardExp);
        }
    }
}
