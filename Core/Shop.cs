using TEXT_RPG.Core.DTO;
using TEXT_RPG.Data;
using TEXT_RPG.Manager;

namespace TEXT_RPG.Core
{
    internal class Shop
    {
        public List<ShopEntry> ShopEntries { get; private set; } = new();

        public void InitializeDefaultItems()
        {
            // 상점 아이템 리스트
            var items = new List<Item> {
                    new("수련자 갑옷", ItemType.Armor, 5, "수련에 도움을 주는 갑옷입니다.", 1000),
                    new("무쇠갑옷", ItemType.Armor, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000),
                    new("스파르타의 갑옷", ItemType.Armor, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500),
                    new("낡은 검", ItemType.Weapon, 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600),
                    new("청동 도끼", ItemType.Weapon, 5, "어디선가 사용됐던거 같은 도끼입니다.", 1500),
                    new("스파르타의 창", ItemType.Weapon, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2300)
                };

            ShopEntries.Clear();
            foreach (Item item in items)
            {
                ShopEntries.Add(new ShopEntry(item));
            }
        }

        public void PurchaseItem(ShopEntry shopEntry, Player player)
        {
            if (shopEntry.IsPurchased) // 이미 구매
            {
                GameManager.Instance.HeaderText = "이미 구매한 아이템입니다.";
            }
            else if (shopEntry.Item.Price <= player.Gold) // 보유금액 충족
            {
                player.AddGold(-shopEntry.Item.Price);
                player.AddItem(shopEntry.Item);
                shopEntry.TogglePurchased();
                GameManager.Instance.HeaderText = "구매를 완료했습니다.";
            }
            else // 보유 금액 미달
            {
                GameManager.Instance.HeaderText = "Gold가 부족합니다.";
            }
        }

        public ShopDto ToDto()
        {
            return new ShopDto { ShopEntries = [.. ShopEntries.Select(i => i.ToDto())] };
        }
    }
}
