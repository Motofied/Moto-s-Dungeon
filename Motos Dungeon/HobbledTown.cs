using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Motos_Dungeon
{
    public class HobbledTown
    {
        public static void HobbledTownLoad(Player p)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Talk to the fellow who appears to be a blacksmith? Type: '1' ");
            Console.WriteLine();
            Console.WriteLine("Talk to the lady stirring a cauldron full of herbs? Type: '2'");
            Console.WriteLine();
            Console.WriteLine("Talk to the fellow who looks like he's been places? Type: '3' ");
            Console.WriteLine();
            Console.WriteLine("Talk to the suspicious individual who looks like he doesn't want to be seen? Type: '4'");
            Console.WriteLine();
            Console.WriteLine("Return to Crossroads? Type: '5'");
            string input = Console.ReadLine().ToLower();


            //Harold - Blacksmith
            if (input == "1")
            {

                Console.Clear();
                Program.Print("Hello traveler! My name is Harold, I'm a local blacksmith 'round these parts. What can I do for ya?");
                Thread.Sleep(1000);
                Console.WriteLine(" 1.) Could you upgrade my weapon for me? ");
                Console.WriteLine(" 2.) Can you tell me more about this place? ");
                Console.WriteLine(" 3.) No thank you, I'll be leaving");
                


                string input1 = Console.ReadLine().ToLower();

                if (input1 == "1")
                {
                    Program.Print("Sure thing, traveler, my price is 2500 coins");
                    Console.WriteLine("Purchase this? (Yes/No)");
                    string input2 = Console.ReadLine().ToLower();

                    if (input2 == "yes")
                    {
                        if (p.coins >= 2500)
                        {
                            Program.Print("Thanks for your business! I have now upgraded your sword by + 6");
                            p.weaponValue += 6;

                            p.coins -= 2500;
                            Console.ReadKey();
                        }
                        else
                        {
                            Program.Print("Sorry, you don't have enough coins... Come back whenever! ");
                        }
                    }
                    else if (input2 == "no")
                    {
                        Program.Print("No problemo, traveler, come back anytime!");
                        Console.ReadKey();
                    }
                    else
                    {
                        Program.Print("Please enter correct information");
                    }


                }
                else if (input1 == "2")
                {
                    Program.Print("Well shucks, I'm not the best at giving any sort of information... If you want that go talk to Jensen.");
                    Console.ReadKey();
                }
                else if (input1 == "3")
                    Program.Print("No problemo traveler! See you around");
                Console.ReadKey();

                // End of Harold
            }
            //Reya - Potion Brewer
            else if (input == "2")
            {
                Console.Clear();
                Program.Print("Hello there... My name is, Reya. It looks like you've seen some things in your lifetime.");
                Program.Print("Anything I can help you with?");
                Console.WriteLine();
                Console.WriteLine("1.) Do you have any potions to sell?");
                
                
                string inputR = Console.ReadLine().ToLower();

                while (true)
                {
                    if (inputR == "1" && Program.currentPlayer.reyaspotions > 0)
                    {
                       


                        Console.Clear();
                        Console.WriteLine("Looks like you are in luck, I have potions today!");
                        Console.WriteLine();
                        Console.WriteLine("Here's how many I have left: " + Program.currentPlayer.reyaspotions);
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.WriteLine("(H)ealing Potion: 75$   (M)ana Flask: 100$");
                        Console.WriteLine("(L)eave shop");
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.WriteLine("                Your stats: ");
                        Console.WriteLine("Coins: " +p.coins + "  Health: " +p.health);
                        Console.WriteLine("Healing Potions: " + p.potion + "  Mana Flasks: "+ p.manapotion);
                        Console.WriteLine("Mana: " +p.mana);

                        //Buying Handler
                        string inputRR = Console.ReadLine().ToLower();
                            if (inputRR == "h" && p.coins >= 75)
                            {
                            //Check for potions
                            if (p.reyaspotions == 0)
                            {
                                
                                Program.Print("Sorry, looks like I am out of potions right now... Check back later and I'll try to make more");
                                Console.ReadKey();
                                Console.Clear();
                                HobbledTownLoad(p);
                            }
                            p.reyaspotions--;
                                p.coins -= 75;
                                p.potion++;
                            }
                            else if (inputRR == "m" & p.coins >= 100)
                             {
                            //Check for potions
                            if (p.reyaspotions == 0)
                            {
                                Console.Clear();
                                Program.Print("Sorry, looks like I am out of potions right now... Check back later and I'll try to make more");
                                Console.ReadKey();
                                HobbledTownLoad(p);
                            }
                            p.reyaspotions--;
                                 p.coins -= 100;
                                 p.manapotion++;
                             }
                            else if (inputRR == "l")
                             {
                            Console.Clear();
                            Program.Print("Thanks for coming! Check back later for more potions!");
                            Console.ReadKey();
                            HobbledTownLoad(p);
                             }
                        //End of buying handler
                    }
                    else if (inputR == "1" && Program.currentPlayer.reyaspotions == 0)
                    {
                        Console.WriteLine("Looks like I have none right now, please check back later");
                        Console.ReadKey();
                        HobbledTownLoad(p);
                    }
                 //End of loop
                }
                
            }
            //End of Reya



            // Jensen - Traveler
            else if (input == "3")
            {
                Console.Clear();
                Program.Print("Hello, I am Jensen, Who are you and what do you want?");
                Console.WriteLine();
                Console.WriteLine("1.) I was wondering if you could answer some questions for me?");
                Console.WriteLine("2.) Nothing, goodbye.");
                Console.WriteLine("3.) Turn in quest.");
                string inputJ = Console.ReadLine();
                if (inputJ == "1" && Program.currentPlayer.jensenquestcomplete == false)
                {
                    Program.Print("Well... I could but I'd like to get something from you first.");
                    Program.Print("If you go on a bit of a kill quest for me then perhaps I could.");
                    Console.WriteLine();
                    Console.WriteLine("Do you accept this quest?  (Yes/No)");
                    string inputJJ = Console.ReadLine().ToLower();

                    if (inputJJ == "yes" && p.level >= 3)
                    {
                        Program.Print("Okay, here's what I will have you do...");
                        Quests.JensensQuest();
                        Shop.MainMenu(p);
                    }
                    else if (inputJJ == "yes" && p.jensenquestcomplete == true)
                    {
                        Program.Print("Seems you already completed this quest, no need to do it again.", 20);
                    }
                    else if (inputJJ == "yes" && p.level < 3)
                    {
                        Program.Print("Come back when you are at least level 3.");
                        Console.ReadKey();
                    }


                }
                else if (inputJ == "2")
                {
                    Program.Print("Hmph, Okay...");
                    Console.ReadKey();
                    HobbledTownLoad(p);
                }
                else if (inputJ == "3" && p.questgoal == true)
                {
                    Program.Print("Wow! You actually managed to kill that thing?", 20);
                    Program.Print("I half expected to hear the news later of another person killed by that damned golem..", 20);
                    Program.Print("Well for going through all that effort I did promise you a reward and whatever information you wanna know...");
                    Console.WriteLine();
                    int cc = Program.currentPlayer.GetSpecialCoins();
                    int xx = Program.currentPlayer.GetSpecialXP();
                    Program.Print("You get: " + cc + " coins, and: " + xx + " XP!");
                    Program.Print("Jensen gives you +5 potions!");
                    Program.currentPlayer.coins += cc;
                    Program.currentPlayer.xp += xx;
                    Program.currentPlayer.potion += 5;

                    if (Program.currentPlayer.CanLevelUp())
                        Program.currentPlayer.LevelUp();
                    Console.ReadKey();
                    Console.Clear();

                    Program.Print("So what did you want to ask me, huh?");
                    Console.WriteLine();
                    Console.WriteLine("You ask Jensen: Do you have any idea as to why I would be locked in a dungeon with someone guarding my cell?");
                    Console.ReadKey();
                    Program.Print("That's a hell of a question... I do not, but a friend of mine named Xeke who lives over in the capital might.", 20);
                    Program.Print("He's quite the knowledgable person when it comes to dungeons.", 20);
                    Program.currentPlayer.questactive = false;
                    Program.currentPlayer.jensenquestcomplete = true;
                    Console.ReadKey();
                    HobbledTownLoad(p);
                }
                else if (inputJ == "3" && p.questgoal == false)
                {
                    Program.Print("You don't have any quests to turn in.");
                    Console.ReadKey();
                }


            } //End of Jensen
            
            else if (input == "4")
            {
                Console.WriteLine("This does nothing currently.");
                Console.ReadKey();
            }
            else if (input == "5")
            {
                Shop.MainMenu(p);
            }   
            else
            {
                Console.WriteLine("Please enter a proper number");
            }
                    Console.Clear();
                    Console.WriteLine("Press any key to return to hobbled town");
                    Console.ReadKey();
                    HobbledTownLoad(p);
        }

    }
}
    

