namespace TEXT_RPG.Core
{
    internal class Player
    {
        public int Level { get; private set; }
        public string Name { get; private set; }
        public PlayerJob Job { get; private set; }
        public int AttackPower { get; private set; }
        public int DefensePower { get; private set; }
        public int Hp { get; private set; }
        public int Stamina { get; private set; }
        public int Exp { get; private set; }
        public int Gold { get; private set; }
        private Dictionary<ItemType, Item?> equipped = [];
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

        internal void GetItem(Item item) => Items.Add(item);
        internal void RemoveItem(Item item) => Items.Remove(item);

        public void UpdateGold(int gold) => Gold += gold;
        public void UpdateExp(int exp) => Exp += exp;
        public void UpdateHp(int hp) => Hp += hp;

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

        // STEP 2
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
                equipped[type] = null;
            }

            // STEP 5
            Items =
            [
                new Item("연습용 창", ItemType.Weapon, 3, "검보다는 그래도 창이 다루기 쉽죠."),
                    new Item("무쇠갑옷", ItemType.Armor, 5, "무쇠로 만들어져 튼튼한 갑옷입니다."),
                    new Item("낡은 검", ItemType.Weapon, 2, "쉽게 볼 수 있는 낡은 검 입니다."),
                ];
        }

        // todo: item 전체가 아니라 equipped에서 계산하도록 로직 수정
        public (int attackPower, int defensePower) CalculatePlusPower()
        {
            int attackPower = 0, defensePower = 0;
            foreach (Item item in Items)
            {
                if (!item.IsEquipped) continue;

                if (item.Type == ItemType.Weapon) { attackPower += item.Value; }
                else if (item.Type == ItemType.Armor) { defensePower += item.Value; }
            }
            return (attackPower, defensePower);
        }

        public void ToggleEquip(Item item)
        {
            ItemType type = item.Type;

            // 착용 중인 장비 해제 
            if (equipped[type] == item)
            {
                item.ToggleEquip();
                equipped[type] = null;
                return;
            }

            // 다른 장비 해제
            if (equipped[type] != null)
            {
                equipped[type]!.ToggleEquip();
            }

            // 새 장비 장착
            equipped[type] = item;
            item.ToggleEquip();
        }
    }
}
