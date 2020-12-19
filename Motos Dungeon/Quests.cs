using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motos_Dungeon
{
    public class Quests
    {
        public static void JensensQuest()
        {
            Program.currentPlayer.questactive = true;
            Program.Print("So you decided you wanna take this kill quest huh?");
            Program.Print("I do warn you... It'll be hard, I hope you are ready.");
            Program.Print("Basically there is a golem blocking the path forward to the capital");
            Program.Print("I've been trying to get there for awhile but I lack the skills to defeat it... If you can defeat that golem");
            Program.Print("and come back to me in one piece, I'll tell you anything I know.. plus on the bright side you can also enter the capital afterwards.");
            Console.ReadKey();
            Encounters.GolemEncounter();
            Console.Clear();
            Program.Print("After defeating the golem, you feel a sense of relief and make your way back to the crossroads to finish off this kill quest.");
            Program.currentPlayer.questgoal = true;
            Console.ReadKey();
            



           
        }



    }
}
