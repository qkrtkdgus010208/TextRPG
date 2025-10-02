using TextRPG.Entity;
using TextRPG.Item;

namespace TextRPG.FSM.Scene
{
    internal class InventoryScene : SceneBase
    {
        private Inventory inventory;

        public InventoryScene(SceneController controller) : base(controller)
        {
        }

        protected override void SetScene()
        {
            Console.Title = "인벤토리";
            inventory = GameManager.Instance.Character.Inventory;

            // 테스트용 아이템 추가
            inventory.AddItem(new EquipItem("전사 무기1", "전사 무기1...", 1000, Enum.ItemType.Weapon, Enum.JobType.Warrior, Enum.EquipSlot.Weapon, 0, 0, 10, 5, 0));
            inventory.AddItem(new EquipItem("전사 무기2", "전사 무기2...", 1000, Enum.ItemType.Weapon, Enum.JobType.Warrior, Enum.EquipSlot.Weapon, 0, 0, 20, 10, 0));
            inventory.AddItem(new EquipItem("궁수 무기1", "궁수 무기1...", 1000, Enum.ItemType.Weapon, Enum.JobType.Archer, Enum.EquipSlot.Weapon, 0, 0, 10, 5, 0));
            inventory.AddItem(new EquipItem("궁수 무기2", "궁수 무기2...", 1000, Enum.ItemType.Weapon, Enum.JobType.Archer, Enum.EquipSlot.Weapon, 0, 0, 20, 10, 0));
            inventory.AddItem(new EquipItem("법사 무기1", "법사 무기1...", 1000, Enum.ItemType.Weapon, Enum.JobType.Mage, Enum.EquipSlot.Weapon, 0, 0, 10, 5, 0));
            inventory.AddItem(new EquipItem("법사 무기2", "법사 무기2...", 1000, Enum.ItemType.Weapon, Enum.JobType.Mage, Enum.EquipSlot.Weapon, 0, 0, 20, 10, 0));

            inventory.AddItem(new EquipItem("전사 방어구1", "전사 방어구1...", 1000, Enum.ItemType.Armor, Enum.JobType.Warrior, Enum.EquipSlot.Armor, 50, 10, 0, 0, 5));
            inventory.AddItem(new EquipItem("전사 방어구2", "전사 방어구2...", 1000, Enum.ItemType.Armor, Enum.JobType.Warrior, Enum.EquipSlot.Armor, 100, 20, 0, 0, 10));
            inventory.AddItem(new EquipItem("궁수 방어구1", "궁수 방어구1...", 1000, Enum.ItemType.Armor, Enum.JobType.Archer, Enum.EquipSlot.Armor, 50, 10, 0, 0, 5));
            inventory.AddItem(new EquipItem("궁수 방어구2", "궁수 방어구2...", 1000, Enum.ItemType.Armor, Enum.JobType.Archer, Enum.EquipSlot.Armor, 100, 20, 0, 0, 10));
            inventory.AddItem(new EquipItem("법사 방어구1", "법사 방어구1...", 1000, Enum.ItemType.Armor, Enum.JobType.Mage, Enum.EquipSlot.Armor, 50, 10, 0, 0, 5));
            inventory.AddItem(new EquipItem("법사 방어구2", "법사 방어구2...", 1000, Enum.ItemType.Armor, Enum.JobType.Mage, Enum.EquipSlot.Armor, 100, 20, 0, 0, 10));

            inventory.AddItem(new ConsumeItem("소비 아이템1", "Hp를 50 회복시켜줍니다.", 1000, 50, 0));
            inventory.AddItem(new ConsumeItem("소비 아이템2", "Hp를 100 회복시켜줍니다.", 2000, 100, 0));
            inventory.AddItem(new ConsumeItem("소비 아이템3", "Mp를 50 회복시켜줍니다.", 1000, 0, 50));
            inventory.AddItem(new ConsumeItem("소비 아이템4", "Mp를 100 회복시켜줍니다.", 2000, 0, 100));
            inventory.AddItem(new ConsumeItem("소비 아이템5", "Hp, Mp를 모두 50 회복시켜줍니다.", 2000, 50, 50));
            inventory.AddItem(new ConsumeItem("소비 아이템6", "Hp, Mp를 모두 100 회복시켜줍니다.", 4000, 100, 100));
        }

        protected override void View()
        {
            Console.WriteLine("[인벤토리]\n");

            Console.WriteLine("인벤 - 장비 아이템\n");
            foreach (EquipItem item in inventory.GetEquipments())
            {
                string isEquipped = item.IsEquipped ? " [E]" : "";
                Console.WriteLine($"{isEquipped}{item.DisplayInfo()}");
            }
            Console.WriteLine();

            Console.WriteLine("인벤 - 소비 아이템\n");
            foreach (ItemBase item in inventory.GetConsumes())
                Console.WriteLine(item.DisplayInfo());
            Console.WriteLine();

            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장비 아이템 관리");
            Console.WriteLine("2. 소비 아이템 관리");
            Console.WriteLine("3. 아이템 정렬\n");
        }

        protected override void Control()
        {
            Console.Write("원하시는 행동을 입력해주세요. ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    controller.ChangeSceneState(controller.VillageScene);
                    break;
                case "1":
                    controller.ChangeSceneState(controller.EquipmentScene);
                    break;
                case "2":
                    controller.ChangeSceneState(controller.ConsumptionScene);
                    break;
                case "3":
                    // 인벤토리 정렬 로직
                    // 아이템 이름이 긴 순으로 정렬
                    inventory.Items.Sort((itemA, itemB) => itemB.Name.Length.CompareTo(itemA.Name.Length));
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Sleep();
                    break;
            }
        }
    }
}
