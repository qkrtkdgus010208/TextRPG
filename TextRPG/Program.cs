namespace TextRPG
{
    internal class Program
    {
        static Character character;
        static List<Item> inventory;
        static bool isPlay = true;

        static void InitializeGame()
        {
            // 캐릭터 초기화a
            character = new Character();

            // 인벤토리 초기화 (List로 선언하여 유연성 확보)
            inventory = new List<Item>
            {
                new Item(ItemType.Armor, "무쇠 갑옷", 5, "무쇠로 만들어져 튼튼한 갑옷입니다."),
                new Item(ItemType.Weapon, "낡은 검", 2, "쉽게 볼 수 있는 낡은 검 입니다."),
                new Item(ItemType.Weapon, "연습용 창", 3, "검보다는 그래도 창이 다루기 쉽죠.")
            };
        }

        static void Enter()
        {
            PrintLine();
            Console.WriteLine("\n스파르타 마을에 오신 여러분 환영합니다.\r\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리\n");
            PrintLine();

            Console.Write("원하시는 행동을 입력해주세요. ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ShowStatus();
                    break;
                case "2":
                    ShowInventory();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500); // 0.5초 대기 후 메뉴 재표시
                    break;
            }
        }

        static void ShowStatus()
        {
            Console.Clear();
            Console.WriteLine("[상태 보기]\n");
            character.DisplayInfo();
            Console.WriteLine("\n0. 나가기\n");

            Console.Write("원하시는 행동을 입력해주세요. ");
            string input = Console.ReadLine();

            if (input == "0")
            {
                return; // Enter() 메서드를 종료하고, Main 루프가 다시 Enter()를 호출합니다.
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(500); // 0.5초 대기 후 메뉴 재표시
                ShowStatus(); // 잘못 입력하면 상태 보기 화면을 다시 표시
            }
        }

        static void ShowInventory()
        {
            Console.Clear();
            Console.WriteLine("[아이템 목록]\n");
            foreach (Item item in inventory)
                item.DisplayInfo();
            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("1. 장착 관리\n");

            Console.Write("원하시는 행동을 입력해주세요. ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    return; // Enter() (메인 메뉴)로 돌아갑니다.
                case "1":
                    // 장착 관리 로직
                    ShowEquipManagement();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    ShowInventory(); // 잘못 입력하면 인벤토리 화면을 다시 표시
                    break;
            }
        }

        static void ShowEquipManagement()
        {
            Console.Clear();
            Console.WriteLine("[장착 관리]\n");

            // 아이템 목록 앞에 숫자를 표시합니다.
            for (int i = 0; i < inventory.Count; i++)
            {
                // 인덱스는 1부터 시작하도록 (i + 1) 사용
                inventory[i].DisplayInfo(showIndex: true, index: i + 1);
            }

            Console.WriteLine("\n0. 나가기\n");

            Console.Write("장착/해제할 아이템 번호를 입력해주세요. ");
            string input = Console.ReadLine();

            if (input == "0")
            {
                return; // 인벤토리 화면으로 복귀
            }

            if (int.TryParse(input, out int choice) && choice > 0 && choice <= inventory.Count)
            {
                // 유효한 인덱스 선택 (배열 인덱스는 0부터 시작하므로 -1)
                Item selectedItem = inventory[choice - 1];

                // 장착/해제 로직
                selectedItem.IsEquipped = !selectedItem.IsEquipped;

                // 장착 상태가 변경되었으므로 능력치 업데이트
                character.UpdateStats(inventory);

                if (selectedItem.IsEquipped)
                    Console.WriteLine($"{selectedItem.Name}을(를) 장착했습니다. [E] 추가.");
                else
                    Console.WriteLine($"{selectedItem.Name}의 장착을 해제했습니다. [E] 제거.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            Thread.Sleep(1000); // 사용자 확인 시간
            ShowEquipManagement(); // 장착 관리 화면 다시 표시
        }

        private static void PrintLine()
        {
            Console.WriteLine("------------------------------------------------------------");
        }

        static void Main(string[] args)
        {
            // 게임 데이터 초기화
            InitializeGame();

            // 메인 게임 루프
            while (isPlay)
            {
                Enter();
            }
        }
    }
}
