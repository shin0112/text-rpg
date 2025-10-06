namespace TEXT_RPG.Config
{
    internal static class PathConfig
    {
        public const string FILE_PATH = "../";
        public static readonly string PLAYER_FILE_PATH = Path.Combine(FILE_PATH, "player.json");
        public static readonly string SHOP_FILE_PATH = Path.Combine(FILE_PATH, "shop.json");
    }
}
