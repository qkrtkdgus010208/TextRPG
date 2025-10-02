using TextRPG.Entity;
using TextRPG.FSM;
using TextRPG.Interface;

namespace TextRPG
{
    internal class GameManager
    {
        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        // 게임의 모든 데이터는 GameManager가 소유
        public Character Character { get; private set; }
        public List<Item> Inventory { get; private set; }
        public List<Item> ShopItems { get; private set; }
        public List<DungeonInfo> Dungeons { get; private set; }
        public Random Random { get; } = new Random();
        public bool IsGameOver { get; set; }

        public void InitializeCharacter(Character character)
        {
            Character = character;
        }

        private void InitializeInventory()
        {
            // ... (기존 초기화 로직)
        }
        private void InitializeShopItems()
        {
            // ... (기존 초기화 로직)
        }
        private void InitializeDungeons()
        {
            // ... (기존 초기화 로직)
        }
    }
}
