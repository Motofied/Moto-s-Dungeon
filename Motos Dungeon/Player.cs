using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motos_Dungeon
{
    [Serializable]
    public class Player
    {



        public string name;
        public int id;
        public int coins = 30000;
        public int level = 1;
        public int xp = 0;
        public int health = 10;
        public int damage = 1;
        public int armorValue = 0;
        public int potion = 5;
        public int weaponValue = 1;
        public int spelldamage = 5;
        public int mana = 10;
        public int manapotion = 2;
        public int reyaspotions = 8;
        public bool questactive = false;
        public bool questgoal = false;
        public bool jensenquestcomplete = false;
        



        public int mods = 0;
        public enum PlayerClass {Cleric, Ninja, Brute, Mage, Berserker};
        public PlayerClass currentClass = PlayerClass.Brute;


        public int GetHealth()
        {
            int upper = (2 * mods + 5);
            int lower = (mods + 2);
            return Program.rand.Next(lower, upper);
        }
        public int GetPower()
        {
            int upper = (2 * mods + 2);
            int lower = (mods + 1);
            return Program.rand.Next(lower, upper);
        }
        public int GetCoins()
        {
            int upper = (25 * mods + 70);
            int lower = (15 * mods + 25);
            return Program.rand.Next(lower, upper);
        }
        public int GetXP()
        {
            int upper = (20 * mods + 100);
            int lower = (15 * mods + 70);
            return Program.rand.Next(lower, upper);
        }
        public int GetSpecialCoins()
        {
            int upper = (70 * mods + 200);
            int lower = (60 * mods + 100);
            return Program.rand.Next(lower, upper);
        }
        public int GetSpecialXP()
        {
            int upper = (50 * mods + 280);
            int lower = (40 * mods + 180);
            return Program.rand.Next(lower, upper);
        }
        public int GetLevelUpValue()
        {
            return 100 * level + 400;
        }
        public bool CanLevelUp()
        {
            if (xp >= GetLevelUpValue())
                return true;
            else
                return false;
        }
        public void LevelUp()
        {
           while (CanLevelUp())
            {
                xp -= GetLevelUpValue();
                level++;
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Program.Print("Congratulations, you are now level: " +level+"!");
            Console.ResetColor();
        }
    }
}
