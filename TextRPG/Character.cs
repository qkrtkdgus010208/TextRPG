using System.Reflection.Emit;

namespace TextRPG
{
    public enum JobType { Warrior, Mage, Archer }

    internal class Character
    {
        public int Level { get; private set; }
        public string Name { get; private set; }
        public JobType Job { get; private set; }
        public int Attack { get; private set; }
        public int Armor { get; private set; }
        public int MaxHp { get; private set; }
        public int Hp { get; private set; }
        public int Gold { get; private set; }
        public int MaxExp { get; private set; }
        public int Exp { get; private set; }
        public int Stamina { get; private set; }

        public int BonusAttack { get; private set; }
        public int BonusArmor { get; private set; }

        public Character()
        {
            Level = 1;
            Name = string.Empty;
            Job = JobType.Warrior;
            Attack = 10;
            Armor = 5;
            MaxHp = 100;
            Hp = MaxHp;
            Gold = 10000;
            MaxExp = 50;
            Stamina = 20;
        }

        public Character(JobType newJob, int newAttack, int newArmor, int newHp, int newGold)
        {
            Level = 1;
            Name = string.Empty;
            Job = newJob;
            Attack = newAttack;
            Armor = newArmor;
            MaxHp = newHp;
            Hp = MaxHp;
            Gold = newGold;
            MaxExp = 50;
            Stamina = 20;
        }

        public void SetName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name)) // 빈칸 입력도 방지
            {
                Name = name;
            }
        }

        public void UpdateStats(List<Item> inventory)
        {
            // Bonus 능력치 초기화
            BonusAttack = 0;
            BonusArmor = 0;

            // 인벤토리를 순회하며 장착된 아이템의 효과를 합산합니다.
            foreach (var item in inventory)
            {
                if (item.IsEquipped)
                {
                    if (item.Type == ItemType.Weapon)
                    {
                        BonusAttack += item.EffectValue;
                    }
                    else if (item.Type == ItemType.Armor)
                    {
                        BonusArmor += item.EffectValue;
                    }
                }
            }
        }

        public bool Adventure()
        {
            if (Stamina >= 10)
            {
                Stamina -= 10;
                return true;
            }

            return false;
        }

        public bool Patrol()
        {
            if (Stamina >= 5)
            {
                Stamina -= 5;
                return true;
            }

            return false;
        }

        public bool Train()
        {
            if (Stamina >= 15)
            {
                Stamina -= 15;
                return true;
            }

            return false;
        }

        public int AddGold(int gold)
        {
            Gold += gold;
            return Gold;
        }

        public int AddExp(int exp)
        {
            Exp += exp;
            CheckExp();
            return Exp;
        }

        private void CheckExp()
        {
            while (Exp >= MaxExp)
            {
                AddExp(-MaxExp);
                LevelUp();
            }
        }

        private void LevelUp()
        {
            switch (Job)
            {
                case JobType.Warrior:
                    MaxHp += 20;
                    Attack += 3;
                    Armor += 2;
                    break;
                case JobType.Mage:
                    MaxHp += 20;
                    Attack += 3;
                    Armor += 2;
                    break;
                case JobType.Archer:
                    MaxHp += 20;
                    Attack += 3;
                    Armor += 2;
                    break;
            }

            Level++;
            MaxExp = (int)(MaxExp * 1.2f);
            Hp = MaxHp;

            Console.WriteLine($"레벨 업!");
            Console.WriteLine($"레벨이 {Level}로 상승했습니다.");
            Console.WriteLine($"Hp가 회복됩니다.\n");
        }

        public int AddHp(int hp)
        {
            Hp = Math.Min(100, Hp + hp);
            return Hp;
        }

        public int AddStamina(int stamina)
        {
            Stamina = Math.Min(20, Stamina + stamina);
            return Stamina;
        }

        public int TakeGold(int gold)
        {
            if (Gold >= gold)
                Gold -= gold;
            return Gold;
        }

        public int TakeHp(int hp)
        {
            Hp -= hp;
            if (Hp < 0)
                Hp = 0;
            return hp;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Lv. {Level}");
            Console.WriteLine($"직업: {Job}");
            Console.WriteLine($"공격력: {Attack + BonusAttack} {(BonusAttack != 0 ? $"(+{BonusAttack})" : "")}");
            Console.WriteLine($"방어력: {Armor + BonusArmor} {(BonusArmor != 0 ? $"(+{BonusArmor})" : "")}");
            Console.WriteLine($"체력: {Hp}");
            Console.WriteLine($"Gold: {Gold} G");
            Console.WriteLine($"Exp: {Exp}");
            Console.WriteLine($"스테미나: {Stamina}");
        }
    }
}
