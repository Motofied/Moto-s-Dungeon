using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motos_Dungeon
{
    public class Shop
    {
        //Loading everything
        public static void LoadShop(Player p)
        {
            MainMenu(p);
           // RunShop(p);
        }

        public static void MainMenu(Player p)
        {
            Console.Clear();

            Program.Print("You see in the distance a crossroad with multiple pathways to take...", 20);
            Program.Print("Eventually getting to the crossroad you notice the signs and they say: ");
            Console.WriteLine();
            Console.WriteLine("(Type the name of the location you want to go to)");
            Console.WriteLine("======================");
            Console.WriteLine("Go to: (H)obbled Village");
            Console.WriteLine("Go to: (G)eneral Shop");
            Console.WriteLine("Go to: (F)ishing hole");
            Console.WriteLine("Go to: (B)asic Dungeon");
            Console.WriteLine("Go to: (C)apital");
            Console.WriteLine("Quit game; Saves and exits. (Type 'quit')");

            //Locations

            
            while (true)
            { 
                string input = Console.ReadLine().ToLower();
            if (input == "hobbled village" || input == "h")
            {
                Console.Clear();
                Program.Print("You take the path leading towards the Hobbled Village");
                Program.Print("On your way there you see a bunch of farmland and farm animals. You wonder to yourself what they are");
                Program.Print("or who lives there. Is it possible anybody there has any knowledge of why you lost your memories or why you were locked in a dungeon?");
                Program.Print("You enter the small town and see a friendly billboard to greet you saying 'Welcome to Hobbled Town' ", 20);
                Program.Print("As you are walking around town a bit you see a variety of different people and wonder to yourself, \n should I go talk to them?", 20);
                    Console.ReadKey();
                HobbledTown.HobbledTownLoad(p);

                //Send to town
            }
            else if (input == "general shop" || input == "g")
            {
                Program.Print("You take the path for the General Shop");
                Program.Print("Walking down the path you notice the shop isn't too far out of sight.", 20);
                Console.ReadKey();
                RunShop(p);
            }
            else if (input == "fishing hole" || input == "f")
            {
                // Do things
            }

            //Capital

            else if (input == "c" || input == "capital")
            {
                   if (Program.currentPlayer.jensenquestcomplete == false)
                    {
                        Program.Print("The pathway seems blocked off, I better not try to go down here.");
                        Console.ReadKey();
                    }
                   else if (Program.currentPlayer.jensenquestcomplete == true)
                    {
                        Capital.CapitalLoad();
                    }
                   else
                    {
                        Program.Print("Something might've went wrong!");
                        Console.ReadKey();
                    }
            }

            else if (input == "quit")
            {
                Program.Quit();
            }
            else if (input == "basic dungeon" || input == "b")
            {
                Program.Print("You take the path leading towards the dungeon");
                Program.Print("As you are are walking you wonder why the land is barren and lifelss towards the dungeons. ");
                Console.ReadKey();
                    bool encounterloop = true;
                    while (encounterloop)
                        {
                        Encounters.RandomEncounter();
                        }
            }
            else
                    Console.WriteLine("Please enter correct information");

        }
            
        }
        //Shops main function
        public static void RunShop(Player p)

        {
            int potionP;
            int armorP;
            int difP;
            int weaponP;
            int manapotionP;
            int spelldamageP;


            while (true)
            {
                potionP = 20 + 10 * p.potion;
                armorP = 100 * (p.armorValue + 1);
                weaponP = 100 * p.weaponValue;
                difP = 300 + 100 * p.mods;
                manapotionP = 30 + 15 * p.manapotion;
                spelldamageP = 150 * p.spelldamage;
                //Abomination of texts aka the shop
                Console.Clear();
                Console.WriteLine("*          Shop:         *");
                Console.WriteLine("==========================");
                Console.WriteLine("(W)eapon :             $" + weaponP);
                Console.WriteLine("(A)rmor :              $" + armorP);
                Console.WriteLine("(P)otions :            $" + potionP);
                Console.WriteLine("(M)ana Flasks :        $" + manapotionP);
                Console.WriteLine("(S)pell Damage :       $" + spelldamageP);
                Console.WriteLine("(D)ifficulty Mod :     $" + difP);
                Console.WriteLine("==========================");
                Console.WriteLine("(E)xit shop");
                Console.WriteLine("(Q)uit game // Saves and quits.");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(p.name + "'s Stats: ");
                Console.WriteLine("==========================");
                Console.WriteLine("Current Health: " + p.health);
                Console.WriteLine("Coins:      " + p.coins);
                Console.WriteLine("Weapon Strength: " + p.weaponValue);
                Console.WriteLine("Armor Rating: " + p.armorValue);
                Console.WriteLine("Spell Damage: " + p.spelldamage);
                Console.WriteLine("Potions: " + p.potion);
                Console.WriteLine("Mana Flaks: " + p.manapotion);
                Console.WriteLine("Mana: " + p.mana);
                Console.WriteLine("Difficulty Modifier: " + p.mods);

                Console.WriteLine("XP: ");
                Console.Write("[");
                Program.ProgressBar("+", " ", ((decimal)p.xp / (decimal)p.GetLevelUpValue()), 25);
                Console.WriteLine("]");

                Console.WriteLine("Level: " + p.level);
                Console.WriteLine("==========================");

                //Wait for input
                string input = Console.ReadLine().ToLower();
                if (input == "p" || input == "potions")
                {
                    TryBuy("potions", potionP, p);
                }
                else if (input == "s" || input == "spell damage")
                {
                    TryBuy("spelldamage", spelldamageP, p);
                }
                else if (input == "m" || input == "mana flask")
                {
                    TryBuy("manapotion", manapotionP, p);
                }
                else if (input == "w" || input == "weapon")
                {
                    TryBuy("weapon", weaponP, p);
                }
                else if (input == "a" || input == "armor")
                {
                    TryBuy("armor", armorP, p);
                }
                else if (input == "d" || input == "difficulty mod")
                {
                    TryBuy("dif", difP, p);
                }
                else if (input == "q" || input == "quit")
                {
                    Program.Quit();
                }
                else if (input == "e" || input == "exit")
                    MainMenu(p);

            }
        }
        static void TryBuy(string item, int cost, Player p)
        {
            if (p.coins >= cost)
            {
                if (item == "potions")
                    p.potion++;
                else if (item == "weapon")
                    p.weaponValue++;
                else if (item == "armor")
                    p.armorValue++;
                else if (item == "dif")
                    p.mods++;
                else if (item == "manapotion")
                    p.manapotion++;
                else if (item == "spelldamage")
                    p.spelldamage += 2;
                p.coins -= cost;
            }
            else
            {
                Console.WriteLine("You do not have enough coins to purchase this.");
                Console.ReadKey();
            }
        }

    }
}