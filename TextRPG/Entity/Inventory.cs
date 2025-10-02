using TextRPG.Enum;
using TextRPG.Item;

namespace TextRPG.Entity
{
    internal class Inventory
    {
        public List<ItemBase> Items { get; private set; } = new List<ItemBase>();

        public int EquipItemCount { get; private set; }
        public int ConsumeItemCount { get; private set; }

        // 장비 슬롯
        private readonly Dictionary<EquipSlot, EquipItem> equippedItems = new Dictionary<EquipSlot, EquipItem>();

        private readonly Character character;

        public Inventory(Character character)
        {
            this.character = character;

            foreach (EquipSlot slot in System.Enum.GetValues(typeof(EquipSlot)))
            {
                equippedItems.Add(slot, null);
            }
        }

        // 인벤토리에 아이템 추가
        public void AddItem(ItemBase item)
        {
            if (item is EquipItem)
                EquipItemCount++;
            if (item is ConsumeItem)
                ConsumeItemCount++;
            Items.Add(item);
        }

        // 인벤토리에서 아이템 제거
        private void RemoveItem(ItemBase item)
        {
            Items.Remove(item);
        }

        public void EquipItem(EquipItem item)
        {
            // 슬롯에 아이템을 장착 중이면 해제
            if (equippedItems[item.Slot] != null)
            {
                EquipItem equipedItem = equippedItems[item.Slot];
                character.UnequipItem(equipedItem);
                Console.WriteLine("착용 해제");
            }

            // 같은 아이템이면 해제
            if (equippedItems[item.Slot] == item)
            {
                equippedItems[item.Slot] = null;
                Console.WriteLine("착용 해제만");
            }
            // 다른 아이템이면 변경
            else
            {
                equippedItems[item.Slot] = item;
                character.EquipItem(item);
                Console.WriteLine("착용");
            }
        }

        public void ConsumeItem(ConsumeItem item)
        {
            item.Use(character);
            RemoveItem(item);
        }

        public IEnumerable<EquipItem> GetEquipments()
        {
            // LINQ의 OfType<T>() 메서드를 사용하여 Item 목록에서 Equipment 타입만 추출합니다.
            return Items.OfType<EquipItem>();
        }

        public IEnumerable<ConsumeItem> GetConsumes()
        {
            // LINQ의 OfType<T>() 메서드를 사용하여 Item 목록에서 ConsumeItem 타입만 추출합니다.
            return Items.OfType<ConsumeItem>();
        }
    }
}
