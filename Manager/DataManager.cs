using Newtonsoft.Json;
using TEXT_RPG.Core;

namespace TEXT_RPG.Manager
{
    internal class DataManager
    {
        private static DataManager _instance = new();
        public static DataManager Instance = _instance;

        private static readonly string FILE_PATH = "../";
        private static readonly string USER_FILE_NAME = "user.json";
        private static readonly string SHOP_FILE_NAME = "shop.json";

        private static GameManager manager = GameManager.Instance;

        public void SaveData()
        {
            string userData = JsonConvert.SerializeObject(manager.Player);
            string shopData = JsonConvert.SerializeObject(manager.Shop);

            File.WriteAllText(FILE_PATH + USER_FILE_NAME, userData);
            File.WriteAllText(FILE_PATH + SHOP_FILE_NAME, shopData);

            manager.HeaderText = "저장 완료";
        }

        public void LoadData()
        {
            try
            {
                if (File.Exists(FILE_PATH + USER_FILE_NAME))
                {
                    string userData = File.ReadAllText(FILE_PATH + USER_FILE_NAME);
                    manager.Player = JsonConvert.DeserializeObject<Player>(userData)!;
                }
                else
                {
                    manager.Player = new("아무개", Data.PlayerJob.전사);
                }

                if (File.Exists(FILE_PATH + SHOP_FILE_NAME))
                {
                    string shopData = File.ReadAllText(FILE_PATH + SHOP_FILE_NAME);
                    manager.Shop = JsonConvert.DeserializeObject<Shop>(shopData) ?? new();
                }
                else
                {
                    manager.Shop = new();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"데이터 로드 중 오류 발생: {ex.Message}");
                manager.Player = new("아무개", Data.PlayerJob.전사);
                manager.Shop = new();
            }
        }
    }
}
