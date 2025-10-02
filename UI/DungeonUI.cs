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
    }
}
