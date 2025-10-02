using TextRPG.Entity;
using TextRPG.FSM;

namespace TextRPG
{
    public record DungeonInfo(
        string Name, 
        int RequiredArmor, 
        int BaseGoldReward, 
        int BaseExpReward,
        int BaseHpLossMin, // 기본 체력 감소 범위 최소
        int BaseHpLossMax  // 기본 체력 감소 범위 최대
    );

    internal class Program
    {
        static Character character;
        static List<Item> inventory;
        static List<Item> shopItem;
        static bool isPlay = true;
        static readonly Random random = new Random();

        static List<DungeonInfo> dungeons;

        static void InitializeGame()
        {
            // 인벤토리 초기화 (List로 선언하여 유연성 확보)
            inventory = new List<Item>
            {
                new Item(ItemType.Armor, "천 갑옷", 3, "질긴 천으로 만들어져 튼튼한 갑옷입니다.", 500),
                new Item(ItemType.Armor, "무쇠 갑옷", 5, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1000),
                new Item(ItemType.Weapon, "낡은 검", 2, "쉽게 볼 수 있는 낡은 검 입니다.", 500),
                new Item(ItemType.Weapon, "연습용 창", 3, "검보다는 그래도 창이 다루기 쉽죠.", 1000),
            };

            shopItem = new List<Item>
            {
                new Item(ItemType.Armor, "낡은 갑옷", 3, "누군가가 많이 입은 낡은 갑옷입니다.", 1000),
                new Item(ItemType.Armor, "수련자 갑옷", 5, "수련에 도움을 주는 갑옷입니다.", 1500),
                new Item(ItemType.Armor, "고급 갑옷", 7, "고급스러운 디자인의 갑옷입니다.", 1500),
                new Item(ItemType.Weapon, "낡은 도끼", 3, "누군가가 많이 사용한 낡은 도끼입니다.", 1000),
                new Item(ItemType.Weapon, "고급 도끼", 5, "고급스러운 디자인의 도끼입니다.", 1500),
                new Item(ItemType.Weapon, "스파르타 도끼", 7, "스파르타의 전사들이 사용한 도끼입니다.", 1500),
            };

            dungeons = new List<DungeonInfo>
            {
                // 쉬운 던전: 인덱스 0
                new DungeonInfo("쉬운 던전", 5, 1000, 50, 20, 35),
                // 일반 던전: 인덱스 1
                new DungeonInfo("일반 던전", 11, 1700, 100, 20, 35),
                // 어려운 던전: 인덱스 2
                new DungeonInfo("어려운 던전", 17, 2500, 200, 20, 35),
            };
        }

        static void Enter()
        {
            Console.Clear();
            PrintLine();
            Console.WriteLine("\n스파르타 마을에 오신 여러분 환영합니다.\r\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 랜덤 모험(스테미나 10 소요)");
            Console.WriteLine("4. 마을 순찰(스테미나 5 소요)");
            Console.WriteLine("5. 훈련하기(스테미나 15 소요)");
            Console.WriteLine("6. 상점");
            Console.WriteLine("7. 던전 입장");
            Console.WriteLine("8. 휴식하기\n");
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
                case "3":
                    RandomAdventure();
                    break;
                case "4":
                    PatrolVillage();
                    break;
                case "5":
                    Training();
                    break;
                case "6":
                    ShowShop();
                    break;
                case "7":
                    ShowDungeon();
                    break;
                case "8":
                    ShowRest();
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
            Console.WriteLine("[인벤토리]\n");
            foreach (Item item in inventory)
                item.DisplayInfo(SceneType.Inventory);
            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("2. 아이템 정렬\n");

            Console.Write("원하시는 행동을 입력해주세요. ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    return; // Enter() (메인 메뉴)로 돌아갑니다.
                case "1":
                    // 장착 관리 로직
                    EquipManagement();
                    break;
                case "2":
                    // 인벤토리 정렬 로직
                    // 아이템 이름이 긴 순으로 정렬
                    inventory.Sort((itemA, itemB) => itemB.Name.Length.CompareTo(itemA.Name.Length));
                    ShowInventory();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500); // 0.5초 대기 후 메뉴 재표시
                    ShowInventory(); // 잘못 입력하면 인벤토리 화면을 다시 표시
                    break;
            }
        }

        static void EquipManagement()
        {
            Console.Clear();
            Console.WriteLine("[인벤토리 - 장착 관리]\n");

            // 아이템 목록 앞에 숫자를 표시합니다.
            for (int i = 0; i < inventory.Count; i++)
            {
                // 인덱스는 1부터 시작하도록 (i + 1) 사용
                inventory[i].DisplayInfo(SceneType.InventoryManagement, index: i + 1);
            }

            Console.WriteLine("\n0. 나가기\n");

            Console.Write("장착/해제할 아이템 번호를 입력해주세요. ");
            string input = Console.ReadLine();

            if (input == "0")
            {
                return; // 처음으로 돌아가기
            }

            if (int.TryParse(input, out int choice) && choice > 0 && choice <= inventory.Count)
            {
                // 유효한 인덱스 선택 (배열 인덱스는 0부터 시작하므로 -1)
                Item selectedItem = inventory[choice - 1];

                // 이미 장착된 아이템이라면 -> 해제
                if (selectedItem.IsEquipped)
                {
                    selectedItem.IsEquipped = false;
                    Console.WriteLine($"{selectedItem.Name}의 장착을 해제했습니다. [E] 제거.");
                }
                // 장착되지 않은 아이템이라면 -> 타입 체크 후 교체 장착
                else if (selectedItem.Type == ItemType.Weapon || selectedItem.Type == ItemType.Armor)
                {
                    // 동일 타입 기존 장비 해제 로직
                    ItemType typeToUnequip = selectedItem.Type;

                    foreach (var item in inventory)
                    {
                        // 같은 타입이고, 현재 장착 중이며, 현재 선택된 아이템이 아닌 경우
                        if (item.Type == typeToUnequip && item.IsEquipped && item != selectedItem)
                        {
                            item.IsEquipped = false;
                            Console.WriteLine($"[알림] {item.Name}의 장착을 해제했습니다. (동일 타입)");
                            break; // 동일 타입은 하나만 장착 가능하므로, 찾으면 즉시 멈춥니다.
                        }
                    }

                    // 새 아이템 장착
                    selectedItem.IsEquipped = true;
                    Console.WriteLine($"{selectedItem.Name}을(를) 장착했습니다. [E] 추가.");
                }
                else
                {
                    // 장착 불가능한 타입 (Potion, Etc)을 선택한 경우
                    Console.WriteLine($"[알림] {selectedItem.Name}은(는) 장착 가능한 아이템이 아닙니다.");
                }

                // 장착 상태가 변경되었으므로 능력치 업데이트
                character.UpdateStats(inventory);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            Thread.Sleep(1000); // 사용자 확인 시간
            EquipManagement(); // 장착 관리 화면 다시 표시
        }

        private static void RandomAdventure()
        {
            Console.Clear();
            Console.WriteLine("[랜덤 모험]\n");

            if (character.TakeStamina(5))
            {
                int rand = random.Next(1, 101);

                if (rand > 50)
                {
                    character.AddGold(500);
                    Console.WriteLine("몬스터 조우! 골드 500 획득");
                    Console.WriteLine($"보유 골드: {character.Gold}");
                }
                else
                {
                    Console.WriteLine("아무 일도 일어나지 않았다...");
                }
            }
            else
            {
                Console.WriteLine("스태미나가 부족합니다.");
            }

            Console.WriteLine($"\n스테미나: {character.Stamina}");

            Console.WriteLine("\n아무 키나 눌러 메인 메뉴로 돌아가세요...");
            Console.ReadKey(true);
        }

        private static void PatrolVillage()
        {
            Console.Clear();
            Console.WriteLine("[마을 순찰]\n");

            if (character.TakeStamina(5))
            {
                int rand = random.Next(1, 101);

                if (rand > 90)
                {
                    character.TakeGold(500);
                    Console.WriteLine("마을 아이들이 모여있다. 간식을 사줘볼까?");
                    Console.WriteLine($"보유 골드: {character.Gold}");
                }
                else if (rand > 80)
                {
                    character.AddGold(2000);
                    Console.WriteLine("촌장님을 만나서 심부름을 했다.");
                    Console.WriteLine($"보유 골드: {character.Gold}");
                }
                else if (rand > 60)
                {
                    character.AddGold(1000);
                    Console.WriteLine("길 읽은 사람을 안내해주었다.");
                    Console.WriteLine($"보유 골드: {character.Gold}");
                }
                else if (rand > 30)
                {
                    character.AddGold(500);
                    Console.WriteLine("마을 주민과 인사를 나눴다. 선물을 받았다.");
                    Console.WriteLine($"보유 골드: {character.Gold}");
                }
                else
                {
                    Console.WriteLine("아무 일도 일어나지 않았다");
                }
            }
            else
            {
                Console.WriteLine("스태미나가 부족합니다.");
            }

            Console.WriteLine($"\n스테미나: {character.Stamina}");

            Console.WriteLine("\n아무 키나 눌러 메인 메뉴로 돌아가세요...");
            Console.ReadKey(true);
        }

        private static void Training()
        {
            Console.Clear();
            Console.WriteLine("[훈련하기]\n");

            if (character.TakeStamina(5))
            {
                int rand = random.Next(1, 101);

                if (rand > 85)
                {
                    character.AddExp(60);
                    Console.WriteLine("훈련이 잘 되었습니다!");
                    Console.WriteLine($"Lv.: {character.Level}");
                    Console.WriteLine($"Exp: {character.Exp}");
                }
                else if (rand > 25)
                {
                    character.AddExp(40);
                    Console.WriteLine("오늘하루 열심히 훈련했습니다.");
                    Console.WriteLine($"Lv.: {character.Level}");
                    Console.WriteLine($"Exp: {character.Exp}");
                }
                else
                {
                    character.AddExp(30);
                    Console.WriteLine("하기 싫다... 훈련이...");
                    Console.WriteLine($"Lv.: {character.Level}");
                    Console.WriteLine($"Exp: {character.Exp}");
                }
            }
            else
            {
                Console.WriteLine("스태미나가 부족합니다.");
            }

            Console.WriteLine($"\n스테미나: {character.Stamina}");

            Console.WriteLine("\n아무 키나 눌러 메인 메뉴로 돌아가세요...");
            Console.ReadKey(true);
        }

        private static void ShowShop()
        {
            Console.Clear();
            Console.WriteLine("[상점]\n");

            Console.WriteLine($"보유 골드: {character.Gold}\n");

            foreach (Item item in shopItem)
                item.DisplayInfo(SceneType.Shop);
            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매\n");

            Console.Write("원하시는 행동을 입력해주세요. ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    return; // Enter() (메인 메뉴)로 돌아갑니다.

                case "1":
                    // 아이템 구매 로직
                    ShopBuyManagement();
                    break;

                case "2":
                    // 아이템 판매 로직
                    ShopSellManagement();
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500); // 0.5초 대기 후 메뉴 재표시
                    ShowShop(); // 잘못 입력하면 상점 화면을 다시 표시
                    break;
            }
        }

        private static void ShopBuyManagement()
        {
            Console.Clear();
            Console.WriteLine("[상점 - 아이템 구매]\n");

            Console.WriteLine($"보유 골드: {character.Gold}\n");

            // 아이템 목록 앞에 숫자를 표시합니다.
            for (int i = 0; i < shopItem.Count; i++)
            {
                // 인덱스는 1부터 시작하도록 (i + 1) 사용
                shopItem[i].DisplayInfo(SceneType.ShopManagement, index: i + 1);
            }

            Console.WriteLine("\n0. 나가기\n");

            Console.Write("구매할 아이템 번호를 입력해주세요. ");
            string input = Console.ReadLine();

            if (input == "0")
            {
                return; // 처음으로 돌아가기
            }

            if (int.TryParse(input, out int choice) && choice > 0 && choice <= shopItem.Count)
            {
                // 유효한 인덱스 선택 (배열 인덱스는 0부터 시작하므로 -1)
                Item selectedItem = shopItem[choice - 1];

                // 이미 구매한 아이템
                if (selectedItem.IsBuy)
                {
                    Console.WriteLine($"이미 구매한 아이템입니다.");
                }
                // 구매하지 않은 아이템이라면
                else
                {
                    // 돈이 있으면 인벤토리에 추가, 상점에서 삭제
                    if (character.Gold >= selectedItem.Price)
                    {
                        inventory.Add(selectedItem);
                        shopItem.Remove(selectedItem);

                        Console.WriteLine($"{selectedItem.Name}을(를) 구매하였습니다.");
                    }
                    // 돈이 없으면 경고문
                    else
                    {
                        Console.WriteLine("Gold 가 부족합니다.");
                    }
                }

                // 상태가 변경되었으므로 능력치 업데이트
                character.UpdateStats(inventory);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            Thread.Sleep(1000); // 사용자 확인 시간
            ShopBuyManagement(); // 장착 관리 화면 다시 표시
        }

        private static void ShopSellManagement()
        {
            Console.Clear();
            Console.WriteLine("[상점 - 아이템 판매]\n");

            Console.WriteLine($"보유 골드: {character.Gold}\n");

            // 아이템 목록 앞에 숫자를 표시합니다.
            for (int i = 0; i < inventory.Count; i++)
            {
                // 인덱스는 1부터 시작하도록 (i + 1) 사용
                inventory[i].DisplayInfo(SceneType.InventoryManagement, index: i + 1);
            }

            Console.WriteLine("\n0. 나가기\n");

            Console.Write("판매할 아이템 번호를 입력해주세요. ");
            string input = Console.ReadLine();

            if (input == "0")
            {
                return; // 처음으로 돌아가기
            }

            if (int.TryParse(input, out int choice) && choice > 0 && choice <= inventory.Count)
            {
                // 유효한 인덱스 선택 (배열 인덱스는 0부터 시작하므로 -1)
                Item selectedItem = inventory[choice - 1];

                inventory.Remove(selectedItem);

                int sellPrice = (int)(selectedItem.Price * 0.85);
                character.AddGold(sellPrice);

                // 장착 중인 아이템
                if (selectedItem.IsEquipped)
                {
                    Console.WriteLine("장착 중인 아이템을 판매하였습니다.");
                    Console.WriteLine("아이템이 자동으로 해제되었습니다.");
                }
                Console.WriteLine($"아이템을 판매하여 {sellPrice} G를 획득하였습니다.");

                // 상태가 변경되었으므로 능력치 업데이트
                character.UpdateStats(inventory);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            Thread.Sleep(1000); // 사용자 확인 시간
            ShopSellManagement(); // 장착 관리 화면 다시 표시
        }

        private static void ShowDungeon()
        {
            Console.Clear();
            Console.WriteLine("[던전 입장]\n");

            Console.WriteLine("0. 나가기");
            for (int i = 0; i < dungeons.Count; i++)
            {
                var dungeon = dungeons[i];
                Console.WriteLine($"{i + 1}. {dungeon.Name} | 방어력 {dungeon.RequiredArmor} 이상 권장");
            }
            Console.WriteLine();

            Console.Write("원하시는 행동을 입력해주세요. ");
            string input = Console.ReadLine();

            if (input == "0")
            {
                return; // 메인 메뉴로 복귀
            }

            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= dungeons.Count)
            {
                // 던전 선택 후 던전 실행 로직 호출
                StartDungeon(choice - 1);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(500);
                ShowDungeon(); // 잘못 입력하면 던전 화면을 다시 표시
            }
        }

        private static void StartDungeon(int dungeonIndex)
        {
            DungeonInfo dungeon = dungeons[dungeonIndex];

            Console.Clear();
            Console.WriteLine($"[ {dungeon.Name} ]에 입장합니다.\n");


            // 던전 성공/실패 확률 판단 (권장 방어력 기준)
            bool success = true;
            int defenseDiff = character.Armor - dungeon.RequiredArmor; // 내 방어력 - 권장 방어력
            int baseFailChance = 40; // 기본 실패 확률 (권장 방어력보다 낮을 때)

            // 권장 방어력보다 낮은 경우 (실패 확률 40%)
            if (character.Armor < dungeon.RequiredArmor)
            {
                int failChance = baseFailChance;
                if (random.Next(1, 101) <= failChance)
                {
                    success = false;
                }
            }

            // 결과 처리 및 능력치 변경
            if (success)
            {
                // 성공 처리
                // 체력 소모량 계산 (기본 20~35 랜덤 + 방어력 차이 반영)
                int hpLossRangeMin = dungeon.BaseHpLossMin - defenseDiff;
                int hpLossRangeMax = dungeon.BaseHpLossMax - defenseDiff;

                // 체력 소모량 계산
                int actualHpLoss = random.Next(
                    Math.Max(0, hpLossRangeMin),
                    Math.Max(1, hpLossRangeMax + 1)
                );

                // 보상 계산 (기본 보상 + 공격력 기반 추가 보상)
                // 공격력 ~ 공격력 * 2 % 추가 보상 계산
                int bonusPercentMin = character.Attack;
                int bonusPercentMax = character.Attack * 2;

                // 실제 추가 보상 % (예: 공격력 10이면 10~20% 사이)
                int bonusPercent = random.Next(bonusPercentMin, bonusPercentMax + 1);

                float bonusMultiplier = 1f + (bonusPercent / 100.0f);

                // 최종 보상 계산
                int finalGold = (int)(dungeon.BaseGoldReward * bonusMultiplier);
                int finalExp = (int)(dungeon.BaseExpReward * bonusMultiplier);

                // 능력치 반영
                character.AddGold(finalGold);
                character.TakeHp(actualHpLoss);
                character.AddExp(finalExp);

                // 출력
                Console.WriteLine("던전 클리어!");
                Console.WriteLine($"체력 {actualHpLoss} 감소. (남은 체력: {character.Hp})");
                Console.WriteLine($"골드 {finalGold} 획득. (추가 보상 {bonusPercent}%)");
                Console.WriteLine($"경험치 {finalExp} 획득. (추가 보상 {bonusPercent}%)\n");
            }
            else
            {
                // 실패 처리
                // 체력 감소: 절반
                int hpLoss = character.Hp / 2;
                character.TakeHp(hpLoss);

                Console.WriteLine("던전 실패...");
                Console.WriteLine($"체력 {hpLoss} 감소. (남은 체력: {character.Hp})");
                Console.WriteLine("보상 없음.\n");
            }

            Console.WriteLine("\n아무 키나 눌러 메인 메뉴로 돌아가세요...");
            Console.ReadKey(true);
        }

        private static void ShowRest()
        {
            int cost = 500;

            Console.Clear();
            Console.WriteLine("[휴식하기]\n");

            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 휴식하기\n");

            Console.Write("원하시는 행동을 입력해주세요. ");
            string input = Console.ReadLine();

            if (input == "0")
            {
                return; // Enter() (메인 메뉴)로 복귀
            }

            if (input == "1")
            {
                if (character.Gold >= cost)
                {
                    character.TakeGold(500);
                    character.AddHp(100);
                    character.AddStamina(20);

                    Console.WriteLine("\n휴식을 완료했습니다. 체력과 스태미나가 완전히 회복되었습니다.");
                    Console.WriteLine($"(소모 골드: {cost} G, 남은 골드: {character.Gold} G)");
                }
                else
                {
                    Console.WriteLine("\nGold 가 부족합니다.");
                    Console.WriteLine($"(필요: {cost} G, 현재: {character.Gold} G)");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(500);
                ShowRest();
            }

            Console.WriteLine("\n아무 키나 눌러 메인 메뉴로 돌아가세요...");
            Console.ReadKey(true);
        }

        private static void PrintLine()
        {
            Console.WriteLine("------------------------------------------------------------");
        }

        static void Main(string[] args)
        {
            SceneController sceneController = new SceneController();
            sceneController.Start();

            // 게임 데이터 초기화
            InitializeGame();

            // 메인 게임 루프
            while (!GameManager.Instance.IsGameOver)
            {
                sceneController.Update();
            }

            Console.WriteLine("\n게임이 종료되었습니다.");
            Console.WriteLine("플레이해주셔서 감사합니다!");
        }
    }
}
