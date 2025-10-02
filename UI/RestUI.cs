namespace TEXT_RPG.UI
{
    internal class RestUI
    {
        public static void ShowRest(string title)
        {
            UIHelper.WriteTitle(title);
            Console.WriteLine($"500 G를 내면 체력을 회복할 수 있습니다. (보유 골드 : {GameManager.Instance.Player.Gold} G)");
            UIHelper.WriteOptions();
        }
    }
}
