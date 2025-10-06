using Newtonsoft.Json;
using TEXT_RPG.Core;
using TEXT_RPG.Core.DTO;
using TEXT_RPG.Data;

namespace TEXT_RPG.Manager
{
    internal class DataManager
    {
        private static DataManager _instance = new();
        public static DataManager Instance = _instance;

        private static readonly GameManager manager = GameManager.Instance;
        private readonly PlayerRepository playerRepository = new();

        public void SaveData()
        {
            playerRepository.Save(manager.Player.ToDto());
            string shopData = JsonConvert.SerializeObject(manager.Shop);

            File.WriteAllText(FILE_PATH + SHOP_FILE_NAME, shopData);

            manager.HeaderText = "저장 완료";
        }

        public void LoadData()
        {
            // 플레이어
            PlayerDto? playerDto = playerRepository.Get();
            if (playerDto == null)
            {
                manager.Player = new Player("아무개", PlayerJob.전사);
            }
            else
            {
                manager.Player = Player.FromDto(playerDto);
            }
        }
    }
}
