using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public class SayCommand : Command
    {
        public SayCommand() : base()
        {
            this.Name = "Say";
        }
        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Say(this.SecondWord);
            }
            else
            {
                player.WarningMessage("\nSay What?");
            }
            return false;
        }
    }
}

