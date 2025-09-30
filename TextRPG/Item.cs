namespace TextRPG
{
    public enum ItemType { Weapon, Armor, Potion, Etc }

    internal class Item
    {
        public ItemType Type { get; private set; }
        public string Name { get; private set; }
        public int EffectValue { get; private set; }
        public string Description { get; private set; }
        public bool IsEquipped { get; set; }

        public Item(ItemType type, string name, int value, string description)
        {
            Type = type;
            Name = name;
            EffectValue = value;
            Description = description;
        }

        public void DisplayInfo(bool showIndex = false, int index = 0)
        {
            string equipStatus = IsEquipped ? "[E]" : "";
            string effectText = Type == ItemType.Armor ? $"방어력 +{EffectValue}" : $"공격력 +{EffectValue}";

            if (showIndex)
            {
                // 인덱스 및 장착 상태를 출력합니다.
                Console.Write($"- {index.ToString()}. {equipStatus} {Name}\t | \t{effectText}\t | \t{Description}");
            }
            else
            {
                // 일반 목록에서 장착 상태만 출력합니다.
                Console.Write($"- {equipStatus} {Name}\t | \t{effectText}\t | \t{Description}");
            }
            Console.WriteLine();
        }
    }
}
