using Newtonsoft.Json;
using TEXT_RPG.Data;
using TEXT_RPG.Manager;

namespace TEXT_RPG.Core
{
    internal class Player
    {
        public int Level { get; private set; }
        public string Name { get; private set; }
        public PlayerJob Job { get; private set; }
        public float AttackPower { get; private set; }
        public float DefensePower { get; private set; }
        public int Hp { get; private set; }
        public int Stamina { get; private set; }
        public int Exp { get; private set; }
        public int Gold { get; private set; }
        public Dictionary<ItemType, Item?> Equipped = [];
        public List<Item> Items { get; }

        public bool SpendStamina(int stamina)
        {
            if (Stamina >= stamina)
            {
                Stamina -= stamina;
                return true;
            }
            else
            {
                GameManager.Instance.HeaderText = "스테미나가 부족합니다.";
                return false;
            }
        }

        internal void AddItem(Item item) => Items.Add(item);
        internal void RemoveItem(Item item) => Items.Remove(item);

        public void AddGold(int gold) => Gold += gold;
        public void AddExp(int exp)
        {
            Exp += exp;
            if (Exp > LevelRepository.RequiredExp[Level])
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Exp -= LevelRepository.RequiredExp[Level];
            Level++;
            AttackPower += 0.5f;
            DefensePower += 1f;
        }

        public void AddHp(int hp)
        {
            Hp += hp;
            if (Hp > 100)
            {
                Hp = 100;
            }
        }

        public void AddStamina(int stamina)
        {
            Stamina += stamina;
            if (Stamina > 100)
            {
                Stamina = 100;
            }
        }

        public void SortItems(byte num)
        {
            switch (num)
            {
                case 1: // 이름 길이 순
                    Items.Sort((item, another) => another.Name.Length.CompareTo(item.Name.Length));
                    break;
                case 2: // 장착순
                    Items.Sort((item, another) => another.IsEquipped.CompareTo(item.IsEquipped));
                    break;
                case 3: // 공격력
                    Items.Sort((item, another) =>
                    {
                        int ret = item.Type.CompareTo(another.Type);
                        return ret != 0 ? ret : another.Value.CompareTo(item.Value);
                    });
                    break;
                case 4: // 방어력
                    Items.Sort((item, another) =>
                    {
                        int ret = another.Type.CompareTo(item.Type);
                        return ret != 0 ? ret : another.Value.CompareTo(item.Value);
                    });
                    break;
                default:
                    break;
            }
        }

        public Player(string name, PlayerJob job)
        {
            Level = 1;
            Name = name;
            Job = job;
            AttackPower = 10;
            DefensePower = 5;
            Hp = 100;
            Stamina = 20;
            Exp = 0;
            Gold = 1500;
            foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
            {
                Equipped[type] = null;
            }

            // STEP 5
            Items =
            [
                new Item("연습용 창", ItemType.Weapon, 3, "검보다는 그래도 창이 다루기 쉽죠."),
                    new Item("무쇠갑옷", ItemType.Armor, 5, "무쇠로 만들어져 튼튼한 갑옷입니다."),
                    new Item("낡은 검", ItemType.Weapon, 2, "쉽게 볼 수 있는 낡은 검 입니다."),
                ];
        }

        [JsonConstructor]
        public Player(int level, string name, PlayerJob job, float attackPower, float defensePower,
              int hp, int stamina, int exp, int gold,
              Dictionary<ItemType, Item?> equipped, List<Item> items)
        {
            Level = level;
            Name = name;
            Job = job;
            AttackPower = attackPower;
            DefensePower = defensePower;
            Hp = hp;
            Stamina = stamina;
            Exp = exp;
            Gold = gold;
            Equipped = equipped ?? [];
            Items = items ?? [];
        }

        // todo: item 전체가 아니라 equipped에서 계산하도록 로직 수정
        public (int attackPower, int defensePower) CalculatePlusPower()
        {
            int attackPower = GetItemValue(ItemType.Weapon);
            int defensePower = GetItemValue(ItemType.Armor);
            return (attackPower, defensePower);
        }

        private int GetItemValue(ItemType type)
        {
            return Equipped[type] == null ? 0 : Equipped[type]!.Value;
        }

        public void ToggleEquip(Item item)
        {
            ItemType type = item.Type;

            // 착용 중인 장비 해제 
            if (Equipped[type] == item)
            {
                item.ToggleEquip();
                Equipped[type] = null;
                return;
            }

            // 다른 장비 해제
            if (Equipped[type] != null)
            {
                Equipped[type]!.ToggleEquip();
            }

            // 새 장비 장착
            Equipped[type] = item;
            item.ToggleEquip();
        }
    }
}
