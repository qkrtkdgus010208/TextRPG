using System.Reflection.Emit;
using TextRPG.Entity;

namespace TextRPG.FSM.Scene.PlayerScene
{
    internal class StatusScene : SceneBase
    {
        public StatusScene(SceneController controller) : base(controller)
        {
        }

        protected override void SetSceneTitle()
        {
            Console.Title = "상태창";
        }

        protected override void View()
        {
            Console.WriteLine("[상태창]");
            Console.WriteLine("캐릭터의 능력치를 확인할 수 있습니다.");
            ViewStatus();
        }

        protected override void Control()
        {
        }

        private void ViewStatus()
        {
            Character character = GameManager.Instance.Character;

            Console.WriteLine($"Lv. {character.Level}");
            Console.WriteLine($"직업: {character.Job}");
            Console.WriteLine($"공격력: {character.Attack + character.BonusAttack} {(character.BonusAttack != 0 ? $"(+{character.BonusAttack})" : "")}");
            Console.WriteLine($"주문력: {character.SkillAttack + character.BonusAttack} {(character.BonusAttack != 0 ? $"(+{character.BonusAttack})" : "")}");
            Console.WriteLine($"방어력: {character.Armor + character.BonusArmor} {(character.BonusArmor != 0 ? $"(+{character.BonusArmor})" : "")}");
            Console.WriteLine($"HP: {character.Hp} / {character.MaxHp}");
            Console.WriteLine($"MP: {character.Mp} / {character.MaxMp}");
            Console.WriteLine($"Gold: {character.Gold} G");
            Console.WriteLine($"Exp: {character.Exp} / {character.MaxExp}");
            Console.WriteLine($"스테미나: {character.Stamina}");

            ReturnToVillage();
        }
    }
}
