using TextRPG.Enum;

namespace TextRPG.Entity
{

    internal class Character
    {
        public string Name { get; private set; }
        public int MaxHp { get; private set; }
        public int Hp { get; private set; }
        public int MaxMp { get; private set; }
        public int Mp { get; private set; }
        public int Attack { get; private set; }
        public int SkillAttack { get; private set; }
        public int Armor { get; private set; }
        public JobType Job { get; private set; }

        public int Level { get; private set; }
        public int Gold { get; private set; }
        public int MaxExp { get; private set; }
        public int Exp { get; private set; }
        public int Stamina { get; private set; }

        public int BonusAttack { get; private set; }
        public int BonusArmor { get; private set; }

        public Character(string name, int maxHp, int maxMp, int attack, int skillAttack, int armor, JobType job)
        {
            Name = name;
            MaxHp = maxHp;
            Hp = MaxHp;
            MaxMp = maxMp;
            Mp = MaxMp;
            Attack = attack;
            SkillAttack = skillAttack;
            Armor = armor;
            Job = job;

            Level = 1;
            Gold = 5000;
            MaxExp = 100;
            Stamina = 100;
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

        public int AddHp(int hp)
        {
            Hp = Math.Min(100, Hp + hp);
            return Hp;
        }
        public int TakeHp(int hp)
        {
            Hp -= hp;
            if (Hp < 0)
                Hp = 0;
            return hp;
        }

        public int AddStamina(int stamina)
        {
            Stamina = Math.Min(20, Stamina + stamina);
            return Stamina;
        }

        public bool TakeStamina(int stamina)
        {
            if (Stamina >= stamina)
            {
                Stamina -= stamina;
                return true;
            }
            return false;
        }

        public int AddGold(int gold)
        {
            Gold += gold;
            return Gold;
        }

        public bool TakeGold(int gold)
        {
            if (Gold >= gold)
            {
                Gold -= gold;
                return true;
            }
            return false;
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
                    MaxMp += 4;
                    Attack += 3;
                    SkillAttack += 1;
                    Armor += 3;
                    break;
                case JobType.Archer:
                    MaxHp += 16;
                    MaxMp += 8;
                    Attack += 2;
                    SkillAttack += 2;
                    Armor += 2;
                    break;
                case JobType.Mage:
                    MaxHp += 12;
                    MaxMp += 12;
                    Attack += 1;
                    SkillAttack += 3;
                    Armor += 1;
                    break;
            }

            Level++;
            MaxExp = (int)(MaxExp * 1.2f);
            Hp = MaxHp;

            Console.WriteLine($"레벨 업!");
            Console.WriteLine($"레벨이 {Level}로 상승했습니다.");
            Console.WriteLine($"Hp가 회복됩니다.\n");
        }
    }
}
