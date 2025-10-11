﻿using TextRPG.Enum;
using TextRPG.Item;

namespace TextRPG.Entity
{

    internal class Character
    {
        // 생성자 주입
        public string Name { get; private set; }
        public int MaxHp { get; private set; }
        public int Hp { get; private set; }
        public int MaxMp { get; private set; }
        public int Mp { get; private set; }
        public int Attack { get; private set; }
        public int SkillAttack { get; private set; }
        public int Armor { get; private set; }
        public int MagicResistance { get; private set; }
        public JobType Job { get; private set; }

        // 초기 세팅값
        public int Level { get; private set; }
        public int Gold { get; private set; }
        public int MaxExp { get; private set; }
        public int Exp { get; private set; }
        public int Stamina { get; private set; }

        // 아이템으로 얻은 추가 능력치
        public int BonusMaxHp { get; private set; }
        public int BonusMaxMp { get; private set; }
        public int BonusAttack { get; private set; }
        public int BonusSkillAttack { get; private set; }
        public int BonusArmor { get; private set; }
        public int BonusMagicResistance { get; private set; }

        // 인벤토리
        public Inventory Inventory { get; private set; }

        public Character(string name, int maxHp, int maxMp, int attack, int skillAttack, int armor, int magicResistance, JobType job)
        {
            Name = name;
            MaxHp = maxHp;
            Hp = MaxHp;
            MaxMp = maxMp;
            Mp = MaxMp;
            Attack = attack;
            SkillAttack = skillAttack;
            Armor = armor;
            MagicResistance = magicResistance;
            Job = job;

            Level = 1;
            Gold = 5000;
            MaxExp = 100;
            Exp = 0;
            Stamina = 100;

            Inventory = new Inventory(this);
        }

        public void EquipItem(EquipItem item)
        {
            item.IsEquipped = true;

            BonusMaxHp += item.BonusMaxHp;
            BonusMaxMp += item.BonusMaxMp;
            BonusAttack += item.BonusAttack;
            BonusSkillAttack += item.BonusSkillAttack;
            BonusArmor += item.BonusArmor;
            BonusMagicResistance += item.BonusMagicResistance;

            MaxHp += item.BonusMaxHp;
            Hp += item.BonusMaxHp;
            MaxMp += item.BonusMaxMp;
            Mp += item.BonusMaxMp;
            Attack += item.BonusAttack;
            SkillAttack += item.BonusSkillAttack;
            Armor += item.BonusArmor;
            MagicResistance -= item.BonusMagicResistance;
        }

        public void UnequipItem(EquipItem item)
        {
            item.IsEquipped = false;

            BonusMaxHp -= item.BonusMaxHp;
            BonusMaxMp -= item.BonusMaxMp;
            BonusAttack -= item.BonusAttack;
            BonusSkillAttack -= item.BonusSkillAttack;
            BonusArmor -= item.BonusArmor;
            BonusMagicResistance += item.BonusMagicResistance;

            MaxHp -= item.BonusMaxHp;
            Hp -= item.BonusMaxHp;
            MaxMp -= item.BonusMaxMp;
            Mp -= item.BonusMaxMp;
            Attack -= item.BonusAttack;
            SkillAttack -= item.BonusSkillAttack;
            Armor -= item.BonusArmor;
            MagicResistance -= item.BonusMagicResistance;
        }

        public int AddHp(int hp)
        {
            Hp = Math.Min(MaxHp, Hp + hp);
            return Hp;
        }

        public bool TakeHp(int hp)
        {
            if (Hp >= hp)
            {
                Hp -= hp;
                return true;
            }
            return false;
        }

        public int AddMp(int mp)
        {
            Mp = Math.Min(MaxMp, Hp + mp);
            return Mp;
        }

        public bool TakeMp(int mp)
        {
            if (Mp >= mp)
            {
                Mp -= mp;
                return true;
            }
            return false;
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
            Hp = MaxHp;
            Mp = MaxMp;
            MaxExp = (int)(MaxExp * 1.2f);

            Console.WriteLine($"레벨 업!");
            Console.WriteLine($"레벨이 {Level}로 상승했습니다.");
            Console.WriteLine($"Hp가 회복됩니다.\n");
        }

        public string DisplayInfo()
        {
            string s = string.Empty;
            s += $"Lv. {Level}\n";
            s += $"직업: {Job}\n";
            s += $"HP: {Hp} / {MaxHp} {(BonusMaxHp != 0 ? $"(+{BonusMaxHp})" : "")}\n";
            s += $"MP: {Mp} / {MaxMp} {(BonusMaxMp != 0 ? $"(+{BonusMaxMp})" : "")}\n";
            s += $"공격력: {Attack} {(BonusAttack != 0 ? $"(+{BonusAttack})" : "")}\n";
            s += $"주문력: {SkillAttack} {(BonusAttack != 0 ? $"(+{BonusAttack})" : "")}\n";
            s += $"방어력: {Armor + BonusArmor} {(BonusArmor != 0 ? $"(+{BonusArmor})" : "")}\n";
            s += $"마법저항력: {MagicResistance + BonusMagicResistance} {(BonusMagicResistance != 0 ? $"(+{BonusMagicResistance})" : "")}\n";
            s += $"Gold: {Gold} G\n";
            s += $"Exp: {Exp} / {MaxExp}\n";
            s += $"스테미나: {Stamina}\n";
            return s;
        }
    }
}
