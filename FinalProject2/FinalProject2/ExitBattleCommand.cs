using FinalGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject2
{
    public class ExitBattleCommand : Command
    {
        public ExitBattleCommand() : base()
        {
            this.Name = "exit";
        }

        override
        public bool Execute(Player player)
        {
            Console.WriteLine("You fled the battle!");
            return true; // Exit the battle
        }
    }
}
