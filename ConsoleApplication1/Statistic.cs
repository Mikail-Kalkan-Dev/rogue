using System;

namespace ConsoleApplication1
{

    public class Statistic
    {
        private const char Sprite = 'C';
        public int hp = 10;
        public int maxHp = 100;
        public int xp = 0;
        public int targetXp = 0;
        public int lvl = 0;
        public int attack = 5;
        public int defense = 5;
        
        public Statistic()
        {
        }

        public Statistic(int hp, int maxHp, int xp, int targetXp,int lvl, int attack, int defense)
        {
            this.hp = hp;
            this.maxHp = maxHp;
            this.xp = xp;
            this.lvl = lvl;
            this.targetXp = targetXp;
            this.attack = attack;
            this.defense = defense;
        }

        public void AddExperience(int experience)
        {
            xp += experience;
            CheckLevelUp();
        }

        public void setTargetExperience()
        {
            targetXp = (int)Math.Round(4 * Math.Pow(lvl, 3) / 5);
        }

        public void CheckLevelUp()
        {
            while (xp >= targetXp)
            {
                xp -= targetXp;
                lvl++;
                StatistiqueGrowth();
                setTargetExperience();
                ResetHp();
                //Growth Stats
                //Set nex Target Xp
            }
        }
        
        


        
        /*----------------------------------------------------------------------
        DICE SYSTEM & VALUES FOR LIFE & STATISTICS
        ------------------------------------------------------------------------
        Fast_Stat = 4D2 --- Medium_Stat = 2D2 --- Slow_Stat = 1D2
        Fast_Health = 9D6 --- Medium_Health = 5D6 --- Slow_Health = 3D4

        Fast_Stat     :     Max 8 Min 4         Fast_Health     :     Max 54 Min 9
        Medium_Stat   :     Max 4 Min 2         Medium_Health   :     Max 30 Min 5
        Slow_Stat     :     Max 2 Min 1         Slow_Health     :     Max 12 Min 3
        ----------------------------------------------------------------------*/
        private int rollDice(int number, int faces)
        {
            var value = 0;
            var rng = new Random();
            for (int i = 0; i < number; i++)
            {
                value += rng.Next(1, faces + 1);
            }
            return value;
        }
        
        public void StatistiqueGrowth()
        {
            maxHp += rollDice(5, 6);
            attack += rollDice(2, 2);
            defense += rollDice(2, 2);
        }

        public void ResetHp()
        {
            hp = maxHp;
        }
        
        public void IncreaseHp(int value)
        {
            hp = Math.Min(hp + value , maxHp);
        }
        
        public void DecreaseHp(int value)
        {
            hp = Math.Max(hp - value , 0);
        }

        public int Damage(int targetDefense)
        {
            return Math.Max(attack - targetDefense, 1);
        }
        
    }
    
    
}