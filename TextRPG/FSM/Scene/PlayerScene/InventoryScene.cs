using TextRPG.Entity;

namespace TextRPG.FSM.Scene
{
    internal class InventoryScene : SceneBase
    {
        private Inventory inventory;

        public InventoryScene(SceneController controller) : base(controller)
        {
        }

        protected override void SetSceneTitle()
        {
            Console.Title = "인벤토리";
        }

        protected override void View()
        {
            Console.WriteLine("[인벤토리]\n");
            //foreach(Item item in inventory)
            //    item.DisplayInfo(SceneType.Inventory);
            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("2. 아이템 정렬\n");
        }

        protected override void Control()
        {
            Console.Write("원하시는 행동을 입력해주세요. ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    return; // Enter() (메인 메뉴)로 돌아갑니다.
                case "1":
                    // 장착 관리 로직
                    controller.ChangeSceneState(controller.EquipmentScene);
                    break;
                case "2":
                    // 인벤토리 정렬 로직
                    // 아이템 이름이 긴 순으로 정렬
                    //inventory.Sort((itemA, itemB) => itemB.Name.Length.CompareTo(itemA.Name.Length));
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Sleep();
                    break;
            }
        }
    }
}
