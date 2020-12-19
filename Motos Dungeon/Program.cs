using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Media;

namespace Motos_Dungeon
{
    public class Program
    {
        public static Random rand = new Random();
        public static Player currentPlayer = new Player();
        public static bool mainLoop = true;

        static void Main(string[] args)
        {
            if (!Directory.Exists("saves"))
            {
                Directory.CreateDirectory("saves");
            }
            currentPlayer = Load(out bool newP);
            if (newP)
                Encounters.FirstEncounter();
            while (mainLoop)
            {
                Encounters.RandomEncounter();
            }

        }


        static Player NewStart(int i)
        {
            Console.Clear();
            Player p = new Player();
            Print("Moto's Dungeon", 100);
            Print("Choose a name for your character:",50);
            p.name = Console.ReadLine();
            Console.Clear();
            Print(p.name + ", Please choose a class to start your adventure!",25);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Print("Cleric: Grants additional buff to healing items.",20);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Print("Ninja: Grants 100% chance to run from a fight & less damage taken from traps.", 20);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Print("Brute: Grants small buff to basic attack.", 20);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Print("Mage: Grants buff to Mana Flasks and small buff towards spells.", 20);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Print("Berserker: Grants a decent increase to physical damage but decreases healing & spell damage",20);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");


            bool flag = false;
            while(flag == false)
            {
                flag = true;
                string input = Console.ReadLine().ToLower();
                if (input.ToLower() == "cleric")
                    p.currentClass = Player.PlayerClass.Cleric;
                else if (input.ToLower() == "ninja")
                    p.currentClass = Player.PlayerClass.Ninja;
                else if (input.ToLower() == "brute")
                    p.currentClass = Player.PlayerClass.Brute;
                else if (input.ToLower() == "mage")
                    p.currentClass = Player.PlayerClass.Mage;
                else if (input.ToLower() == "berserker")
                    p.currentClass = Player.PlayerClass.Berserker;
                else
                {
                    Print("Please choose a valid class.");
                    flag = false;
                }
                
            }
            Print("You have chosen the " + p.currentClass + " class, you can now embark on your adventure!");
            Console.ReadKey();
            p.id = i;
            Console.Clear();
            Print("You awake in a cold, stone, dark room. You are feeling dizzy, and are having trouble remembering",22);
            Print("anything of your past. ",22);
            if (p.name == "")
                Print("You can't even remember your own name...",22);
            else
                Print("All you remember is your name is " + p.name);
            Console.ReadKey();
            Console.Clear();
            Print("You grope around in the darkness until you find a door handle. You feel some resistance as", 22);
            Print("you turn the handle, but the rusty lock breaks effortlessly. You see your captor", 22);
            Print("standing with his back to you outside the door.", 22);
            return p;

        }

        public static void Quit()
        {
            Save();
            Environment.Exit(0);
        }
        public static void Save()
        {
            BinaryFormatter binForm = new BinaryFormatter();
            string path = "saves/" + currentPlayer.id.ToString() + ".level";
            FileStream file = File.Open(path, FileMode.OpenOrCreate);
            binForm.Serialize(file, currentPlayer);
            file.Close();
        }
        public static Player Load(out bool newP)
        {
            newP = false;

            Console.Clear();
            Console.WriteLine("Choose your save file:");
            string[] paths = Directory.GetFiles("saves");
            List<Player> players = new List<Player>();
            int idCount = 0;

            BinaryFormatter binForm = new BinaryFormatter();
            foreach (string p in paths)
            {
                FileStream file = File.Open(p, FileMode.Open);
                Player player = (Player)binForm.Deserialize(file);
                file.Close();
                players.Add(player);
            }
            idCount = players.Count;

            while (true)
            {
                Console.Clear();
                Print("Choose your character:", 30);
                Print("Please input player name or id.(id:# or playername.) Additionally, 'create' will start a new save.", 30);

                foreach (Player p in players)
                {
                    Console.WriteLine(p.id + ": " + p.name);
                }
                string[] data = Console.ReadLine().Split(':');

                try
                {
                    if (data[0] == "id")
                    {
                        if (int.TryParse(data[1], out int id))
                        {
                            foreach (Player player in players)
                            {
                                if (player.id == id)
                                {
                                    return player;
                                }
                            }
                            Console.WriteLine("There is no player with that ID.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("You entered incorrect information. Press any key to try again.");
                            Console.ReadKey();
                        }
                    }
                    else if (data[0] == "create")
                    {
                        Player newPlayer = NewStart(idCount);
                        newP = true;
                        return newPlayer;

                    }



                    else
                    {
                        foreach (Player player in players)
                        {
                            if (player.name == data[0])
                            {
                                return player;
                            }
                        }
                        Console.WriteLine("There is no player with that name.");
                        Console.ReadKey();
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("You entered incorrect information. Press any key to try again.");
                    Console.ReadKey();
                }
            }



        }
        public static void Print(string text, int speed = 30)
        {

            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(speed);
               
              
            }
           
            Console.WriteLine();
        }

        public static void ProgressBar(string fillerChar, string backgroundChar, decimal value, int size)
        {
            int dif = (int)(value * size);
            for(int i = 0; i < size; i++)
            {
                if (i < dif)
                    Console.Write(fillerChar);
                else
                    Console.Write(backgroundChar);
            }
        }


    }
}