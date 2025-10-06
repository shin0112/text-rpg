using Newtonsoft.Json;
using TEXT_RPG.Config;
using TEXT_RPG.Core.DTO;

namespace TEXT_RPG.Data
{
    internal class ShopRepository
    {
        private static readonly string FilePath = PathConfig.SHOP_FILE_PATH;

        public void Save(ShopDto shopDto)
        {
            string shopData = JsonConvert.SerializeObject(shopDto);

            File.WriteAllText(FilePath, shopData);
        }

        public ShopDto? Get()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    Console.WriteLine("파일 존재하지 않음");
                    return null;
                }

                string shopData = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<ShopDto>(shopData)!;
            }
            catch (JsonException e)
            {
                Console.WriteLine($"JSON 역직렬화 중 오류 발생: {e.Message}");
            }
            catch (IOException e)
            {
                Console.WriteLine($"파일 입출력 중 오류 발생: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"예기치 못한 오류 발생: {e.Message}");
            }

            return null;
        }
    }
}
