using Newtonsoft.Json;
using TEXT_RPG.Core.DTO;
using TEXT_RPG.Manager;

namespace TEXT_RPG.Data
{
    internal class PlayerRepository
    {
        private static readonly string FilePath = DataManager.FILE_PATH + "player.json";

        public void Save(PlayerDto playerDto)
        {
            string userData = JsonConvert.SerializeObject(playerDto);

            File.WriteAllText(FilePath, userData);
        }

        public PlayerDto? Get()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string userData = File.ReadAllText(FilePath);
                    return JsonConvert.DeserializeObject<PlayerDto>(userData)!;
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"데이터 로드 중 오류 발생: {e.Message}");
            }

            return null;
        }
    }
}
