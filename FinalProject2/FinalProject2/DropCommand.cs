using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public  class DropCommand : Command
    {
        public DropCommand() : base()
        {
            this.Name = "drop";
        }
        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Drop(this.SecondWord);
            }
            else
            {
                player.WarningMessage("\nDrop What?");
            }
            return false;
        }
    }
}

