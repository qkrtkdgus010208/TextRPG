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
        public int Hp { get; private set; }
        public int Gold { get; private set; }
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
            Hp = 100;
            Gold = 10000;
            Exp = 0;
            Stamina = 20;
        }

        public Character(JobType newJob, int newAttack, int newArmor, int newHp, int newGold)
        {
            Level = 1;
            Name = string.Empty;
            Job = newJob;
            Attack = newAttack;
            Armor = newArmor;
            Hp = newHp;
            Gold = newGold;
            Exp = 0;
            Stamina = 20;
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
            return Exp;
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
