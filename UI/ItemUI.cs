using TEXT_RPG.Core;
using TEXT_RPG.Data;

namespace TEXT_RPG.UI
{
    internal class ItemUI
    {
        public static void ShowItemInfo(Item item)
        {
            string prefix = item.IsEquipped ? "[E]" : "";
            string displayName = prefix + item.Name;

            string statType = item.Type switch
            {
                ItemType.Weapon => "공격력 +",
                ItemType.Armor => "방어력 +",
                _ => ""
            };
            string displayStat = statType + item.Value;

            string paddedName = UIHelper.GetPaddedString(displayName, 20);
            string paddedStat = UIHelper.GetPaddedString(displayStat, 10);
            string paddedDescription = UIHelper.GetPaddedString(item.Description, 50);

            Console.Write($"{paddedName} | {paddedStat} | {paddedDescription}");
        }
    }
}
