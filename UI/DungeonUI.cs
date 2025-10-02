using TEXT_RPG.Core.DTO;
using TEXT_RPG.Data;
using TEXT_RPG.Manager;
using TEXT_RPG.Repository;
using TEXT_RPG.Scenes;

namespace TEXT_RPG.UI
{
    internal class DungeonUI
    {
        public static void WriteOptions()
        {
            Scene scene = GameManager.Instance.CurrentScene;
            for (int i = 1; i < scene.Options.Length; i++)
            {
                UIHelper.WriteOption($"{i}. {UIHelper.GetPaddedString(scene.Options[i], 15)} " +
                    $"| 방어력 {DungeonRepository.Dungeons[(DungeonLevel)i].RequiredDef} 이상 권장");
            }

            UIHelper.WriteExitOption();
        }

        public static void ShowClearInfo(string title)
        {
            DungeonResultDto dto = GameManager.Instance.LastDungeonResult!;

            UIHelper.WriteTitle(title);
            if (dto.IsSuccess)
            {
                Console.WriteLine("축하합니다!!");
                Console.WriteLine($"{dto.Level} 던전을 클리어 했습니다.\n");
            }
            else
            {
                Console.WriteLine("아앗...");
                Console.WriteLine($"{dto.Level} 던전을 클리어하지 못했습니다.\n");
            }

            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"체력 {dto.BeforeHp} -> {dto.AfterHp}");
            Console.WriteLine($"Gold {dto.BeforeGold} G -> {dto.AfterGold} G");
        }
    }
}
