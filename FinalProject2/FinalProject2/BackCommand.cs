using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public class BackCommand : Command
    {
        public BackCommand() : base()
        {
            this.Name = "Back";
        }
        override
        public bool Execute(Player player)
        {
           if (this.HasSecondWord())
            {
                player.OutputMessage("\nI cannot back " + this.SecondWord + "");
            }
            else
            {
                player.Back();
            }
            return false;
        }
        }
    }

