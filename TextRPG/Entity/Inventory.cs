using TextRPG.Enum;
using TextRPG.Item;

namespace TextRPG.Entity
{
    internal class Inventory
    {
        public List<ItemBase> Items { get; set; } = new List<ItemBase>();

        public int EquipItemCount { get; set; }
        public int ConsumeItemCount { get; set; }

        // 장비 슬롯
        public Dictionary<EquipSlot, EquipItem> equippedItems = new Dictionary<EquipSlot, EquipItem>();

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
            if (item is EquipItem equipItem)
            {
                EquipItemCount++;
                equipItem.IsBuy = true;
            }
            if (item is ConsumeItem)
                ConsumeItemCount++;
            Items.Add(item);
        }

        // 아이템 판매
        public void SellItem(ItemBase item)
        {
            if (item is EquipItem equipItem && equipItem.IsEquipped)
            {
                EquipItem(equipItem);
                equipItem.IsBuy = false;
            }
            RemoveItem(item);
        }

        // 인벤토리에서 아이템 제거
        private void RemoveItem(ItemBase item)
        {
            if (item is EquipItem)
                EquipItemCount--;
            else
                ConsumeItemCount--;
            Items.Remove(item);
        }

        public void EquipItem(EquipItem item)
        {
            // 슬롯에 아이템을 장착 중이면 해제
            if (equippedItems[item.equipSlot] != null)
            {
                EquipItem equipedItem = equippedItems[item.equipSlot];
                character.UnequipItem(equipedItem);
            }

            // 같은 아이템이면 해제
            if (equippedItems[item.equipSlot] == item)
            {
                equippedItems[item.equipSlot] = null;
            }
            // 다른 아이템이면 변경
            else
            {
                equippedItems[item.equipSlot] = item;
                character.EquipItem(item);
            }
        }

        public void ConsumeItem(ConsumeItem item)
        {
            item.Use(character);
            RemoveItem(item);
        }
    }
}
