using System;
using TextRPG.Entity;

namespace TextRPG.FSM.Scene.Village
{
    internal class RandomAdventureScene : SceneBase
    {
        public RandomAdventureScene(SceneController controller) : base(controller)
        {
        }

        protected override void SetSceneTitle()
        {
            Console.Title = "랜덤 모험";
        }

        protected override void View()
        {
            Character character = GameManager.Instance.Character;

            Console.WriteLine("[랜덤 모험]\n");

            if (character.TakeStamina(10))
            {
                int rand = Random.Next(1, 101);

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

            Console.WriteLine($"\n현재 스테미나: {character.Stamina}");

            ReturnToVillage();
        }

        protected override void Control()
        {
        }
    }
}
