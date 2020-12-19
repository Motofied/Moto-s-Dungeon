using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Motos_Dungeon
{
    public class Encounters
    {
        static Random rand = new Random();
        // Encounter Generic




        // Encounters
        public static void FirstEncounter()
        {
            Program.Print("You throw open the door and grab a rusty metal sword while charging toward your captor",30);
            Program.Print("He turns...", 200);
            Console.ReadKey();
            Combat(false, "Torturer", 1, 4);
        }
        public static void BasicFightEncounter()
        {
            Console.Clear();
            Console.WriteLine("As you wander, you notice there is a silhouette of what appears to be an enemy...");
            Console.ReadKey();
            Combat(true, "", 0, 0);
        }

        // Unique Encounters

        public static void GiantEncounter()
        {
            Console.Clear();
            Program.Print("You hear loud stomping in the distance and the ground is shaking...");
            Program.Print("As you approach you notice the sillhoute of a large giant");
            Console.ReadKey();
            Combat(false, "Lesser Giant", 3, 12);
        }

        public static void WizardEncounter()
        {
            Console.Clear();
            Program.Print("You open the door slowly, opening into a dark and mysterious room. You see what appears to be a man in a robe.",30);
            Program.Print("He turns around and you notice his glowing eyes.",30);
            Console.ReadKey();
            Combat(false, "Dark Wizard", 4, 2);
        }
        public static void GolemEncounter()
        {
            Console.Clear();
            Program.Print("As you make your way back to the crossroad to take the path towards the capital you are contemplating what you got yourself into.",20);
            Program.Print("Upon arriving to the crossroads, you went through the closed off gate towards the capital...", 20);
            Program.Print("After what feels like walking forever you hear the roar of the golem and see it over the horizon.", 20);
            Combat(false, "Primordial Golem", 5, 100);
        }

        // Puzzles
        public static void PuzzleOneEncounter()
        {
            Console.Clear();
            Program.Print("You are walking down a hall. You see that the floor is covered in runes.");
            List<char> chars = new char[]{ '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }.ToList();
            List<int> positions = new List<int>();
            char c = chars[rand.Next(0, 10)];
            chars.Remove(c);
            for (int y = 0; y < 4; y++)
            {
                int pos = rand.Next(0, 4);
                positions.Add(pos);
                for (int x = 0; x < 4; x++)
                {
                    if(x == pos)
                        Console.Write(c);
                    else
                         Console.Write(chars[rand.Next(0, 8)]);
                }
                Console.Write("\n");
            }
            Program.Print("Choose your path wisely... (Type the position of the rune you want to stand on, not the number; top to bottom & left to right.)");
            for (int i = 0; i < 4; i++)
            {
                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out int input) && input < 5 && input > 0)
                    {
                        if (positions[i] == input - 1)
                            break;
                        else
                        {
                            
                            if (Program.currentPlayer.currentClass == Player.PlayerClass.Ninja)
                            {
                                Console.WriteLine("Darts fly out of the wall at you. You only take 1 damage due to your ninja like skills.");
                                Program.currentPlayer.health -= 1;
                            }
                            else
                            Console.WriteLine("Darts fly out of the wall and you get hit. You take 2 damage");
                            Program.currentPlayer.health -= 2;
                            if (Program.currentPlayer.health <= 0)
                            {
                                //Death
                                Console.WriteLine("You start to feel sick. The poison darts from the trap slowly killed you");
                                Console.ReadKey();
                                System.Environment.Exit(0);
                            }
                            break;
                        }

                    }


                    else
                        Console.WriteLine("Invalid input: Integers only. (1-4)");


                 }
                }
                Program.Print("You have successfully passed through this trap, congratulations!");
                 int cc = Program.currentPlayer.GetCoins();
                 int xx = Program.currentPlayer.GetXP();
                 Program.Print("You get: " + cc + " coins, and: " + xx + " XP!");
                 Program.currentPlayer.coins += cc;
                 Program.currentPlayer.xp += xx;

                 if (Program.currentPlayer.CanLevelUp())
                 Program.currentPlayer.LevelUp();

            Console.ReadKey();
            
        }

        // Encounter Generator
        public static void RandomEncounter()
        {
            switch (rand.Next(0, 4))
            {
                case 0:
                    BasicFightEncounter();
                    break;
                case 1:
                    WizardEncounter();
                    break;
                case 2:
                    PuzzleOneEncounter();
                    break;
                case 3:
                    GiantEncounter();
                    break;
            }
        }
        public static void Combat(bool random, string name, int power, int health)
        {
            string n = "";
            int p = 0;
            int h = 0;

            if (random)
            {
                n = GetName();
                p = Program.currentPlayer.GetPower();
                h = Program.currentPlayer.GetHealth();
            }
            else
            {
                n = name;
                p = power;
                h = health;

            }
            while (h > 0)
            {
                Console.Clear();
                Console.WriteLine(n);
                Console.WriteLine(" Power: " + p + "  Health: " + h);
                Console.WriteLine("==============================");
                Console.WriteLine("|   (A)ttack (D)efend        |");
                Console.WriteLine("|   (R)un    (H)eal          |");
                Console.WriteLine("|   (F)ireball (M)ana flask  |");
                Console.WriteLine("==============================");
                Console.WriteLine($"{Program.currentPlayer.name}'s stats:");
                Console.WriteLine(" Potions: " + Program.currentPlayer.potion + "   Health: " + Program.currentPlayer.health);
                Console.WriteLine("Mana: " + Program.currentPlayer.mana + "   Mana Potions: " + Program.currentPlayer.manapotion);
                string input = Console.ReadLine();
                if (input.ToLower() == "a" || input.ToLower() == "attack")
                {
                    //Attack
                    Console.WriteLine("With haste you run forth, slicing your sword at the " + n + ". As you run past he strikes you back.");
                    int damage = p - Program.currentPlayer.armorValue;
                    if (damage < 0)
                        damage = 0;
                    int attack = rand.Next(0, Program.currentPlayer.weaponValue) + rand.Next(1, 4) + ((Program.currentPlayer.currentClass == Player.PlayerClass.Brute) ? 2 : 0);
                    if (Program.currentPlayer.currentClass == Player.PlayerClass.Berserker)
                        attack += 4;
                    Console.WriteLine("You lose " + damage + " health, and deal " + attack + " damage");
                    Program.currentPlayer.health -= damage;
                    h -= attack;
                }
                //Fireball
                else if (input.ToLower() == "f" || input.ToLower() == "fireball")
                {
                    if (Program.currentPlayer.mana == 0)
                    {
                        Console.WriteLine("You try to summon a fireball but you don't have enough mana to do so.");
                        int damage2 = p - Program.currentPlayer.armorValue;
                        if (damage2 < 0)
                            damage2 = 0;

                        Console.WriteLine("The " + n + " strikes you with a mighty blow, dealing " + damage2 + " damage");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("You feel a strong power engulf you as you raise your hands towards the " + n + " then you unleash a devastating fireball");
                        int damage = p - Program.currentPlayer.armorValue;
                        if (damage < 0)
                            damage = 0;
                        int spell = rand.Next(0, Program.currentPlayer.spelldamage) + rand.Next(3, 9) + ((Program.currentPlayer.currentClass == Player.PlayerClass.Mage) ? 3 : 0);
                        if (Program.currentPlayer.currentClass == Player.PlayerClass.Berserker)
                            spell -= 2;
                        Console.WriteLine("You lose " + damage + " health, and deal " + spell + " damage");
                        Program.currentPlayer.mana -= 5;
                        Program.currentPlayer.health -= damage;
                        h -= spell;
                        Console.ReadKey();
                    }

                }





                else if (input.ToLower() == "d" || input.ToLower() == "defend")
                {
                    //Defend
                    Console.WriteLine("As the " + n + " prepares to strike, you ready your sword in a defensive stance.");
                    int damage = (p / 4) - Program.currentPlayer.armorValue;
                    if (damage < 0)
                        damage = 0;
                    int attack = rand.Next(0, Program.currentPlayer.weaponValue) / 2;

                    Console.WriteLine("You lose " + damage + " health, and deal " + attack + " damage");
                    Program.currentPlayer.health -= damage;
                    h -= attack;
                }
                else if (input.ToLower() == "r" || input.ToLower() == "run")
                {
                    //Run
                    if (Program.currentPlayer.currentClass != Player.PlayerClass.Ninja && rand.Next(0, 2) == 0)
                    {
                        Console.WriteLine("As you sprint away from the " + n + ", its strike catches you in the back, sending you falling to the ground.");
                        int damage = p - Program.currentPlayer.armorValue;
                        if (damage < 0)
                            damage = 0;
                        Program.currentPlayer.health -= damage;
                        Console.WriteLine("You lose " + damage + " health and are unable to escape.");
                        

                    }
                    else if (Program.currentPlayer.questactive == true)
                    {
                        Console.WriteLine("You can not escape from this type of encounter!");
                       
                        
                    }
                    else
                    {
                        Console.WriteLine("You run as fast as you can and run away from the " + n + " and you successfully escape!");
                        Console.ReadKey();
                        Shop.LoadShop(Program.currentPlayer);
                    }
                }

                //Mana flask
                else if (input.ToLower() == "m" || input.ToLower() == "mana flask")
                {
                    if (Program.currentPlayer.manapotion == 0)
                    {
                        Console.WriteLine("You are rustling through your bag desperately looking for a potion but wind up finding nothing.");
                        int damage = p - Program.currentPlayer.armorValue;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine("The " + n + " strikes you with a mighty blow, dealing " + damage + " damage");
                        
                    }
                    else
                    {
                        Console.WriteLine("You reach into your bag and grab a blue mana flask and drink it, you feel its power engulf you. ");
                        int manaV = 5;               
                            Console.WriteLine("You gain: " + manaV + " Mana!");
                        Program.currentPlayer.mana += manaV;
                        Program.currentPlayer.manapotion -= 1;
                        Console.WriteLine("After you drank your flask, the " +n+ " attacked you." );
                        int damage = (p / 2) - Program.currentPlayer.armorValue;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine("You lose " + damage + " health");
                        

                    }
                }
                else if (input.ToLower() == "h" || input.ToLower() == "heal")
                {
                    //Heal
                    if (Program.currentPlayer.potion == 0)
                    {
                        Console.WriteLine("You rustle through your bag quickly trying to find a potion, all that you can find is empty flasks.");
                        int damage = p - Program.currentPlayer.armorValue;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine("The " + n + " strikes you with a mighty blow, dealing " + damage + " damage");
                    }
                    else
                    {
                        Console.WriteLine("You reach into your bag and pull out a Red Flask. You take a swig and feel refreshed.");
                        int potionV = 5 + ((Program.currentPlayer.currentClass==Player.PlayerClass.Cleric)?+3:0);
                        if (Program.currentPlayer.currentClass == Player.PlayerClass.Berserker)
                            potionV--;
                            potionV--;

                        Console.WriteLine("You gain " + potionV + " health.");
                        Program.currentPlayer.health += potionV;
                        Program.currentPlayer.potion -= 1;
                        Console.WriteLine("After you drank your potion, the " + n + " struck you.");
                        int damage = (p / 2) - Program.currentPlayer.armorValue;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine("You lose " + damage + " health");

                    }
                    Console.ReadKey();
                }
                if (Program.currentPlayer.health <= 0)
                {
                    //Death
                    Console.WriteLine("As the " + n + "stands above you, he strikes you down.\n You have been slain by the " + n);
                    Console.ReadKey();
                    System.Environment.Exit(0);
                }
                Console.ReadKey();
            }
            int c = Program.currentPlayer.GetCoins();
            int x = Program.currentPlayer.GetXP();
            Console.WriteLine("You have defeated the " + n + ", it's corpse dissolves into " + c + " coins! ");
            Console.WriteLine("You have gained: " + x + "XP!");
            Program.currentPlayer.coins += c;
            Program.currentPlayer.xp += x;

            if (Program.currentPlayer.CanLevelUp())
                Program.currentPlayer.LevelUp();
         
            Console.ReadKey();
        }

        public static string GetName()

        {
            
            switch (rand.Next(0, 5))
            {
                case 0:
                    return "Skeleton";

                case 1:
                    return "Zombie";

                case 2:
                    return "Lesser Demon";
                case 3:
                    return "Cultist";
                case 4:
                    return "Undead hound";

            }
            return "Rogue";
        }

    }
}