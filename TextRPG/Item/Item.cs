namespace TextRPG
{
    public enum ItemType { Weapon, Armor, Potion, Etc }

    public enum SceneType { Inventory, InventoryManagement, Shop, ShopManagement }

    internal class Item
    {
        public ItemType Type { get; private set; }
        public string Name { get; private set; }
        public int EffectValue { get; private set; }
        public string Description { get; private set; }
        public bool IsEquipped { get; set; }
        public int Price { get; private set; }
        public bool IsBuy {  get; private set; }

        public Item(ItemType type, string name, int value, string description, int price)
        {
            Type = type;
            Name = name;
            EffectValue = value;
            Description = description;
            Price = price;
        }

        public void DisplayInfo(SceneType sceneType, int index = 0)
        {
            string equipStatus = IsEquipped ? "[E]" : "";
            string effectText;
            string priceText = IsBuy ? "구매완료" : $"{Price} G";

            switch (Type)
            {
                case ItemType.Weapon:
                    effectText = $"공격력 +{EffectValue}";
                    break;

                case ItemType.Armor:
                    effectText = $"방어력 +{EffectValue}";
                    break;

                default:
                    effectText = string.Empty;
                    break;
            }

            switch (sceneType)
            {
                case SceneType.Inventory:
                    // 인벤토리에서 장착 상태만 출력합니다.
                    Console.Write($"- {equipStatus} {Name} | {effectText} | {Description}");
                    break;

                case SceneType.InventoryManagement:
                    // 장착 관리에서 인덱스 및 장착 상태를 출력합니다.
                    Console.Write($"- {index.ToString()}. {equipStatus} {Name} | {effectText} | {Description}");
                    break;

                case SceneType.Shop:
                    // 상점에서 
                    Console.Write($"- {equipStatus} {Name} | {effectText} | {Description} | {priceText}");
                    break;

                case SceneType.ShopManagement:
                    // 아이템 구매에서 
                    Console.Write($"- {index.ToString()}. {equipStatus} {Name} | {effectText} | {Description} | {priceText}");
                    break;

            }

            Console.WriteLine();
        }
    }
}
