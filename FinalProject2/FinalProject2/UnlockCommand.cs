using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public class UnlockCommand : Command
    { 
            public UnlockCommand() : base()
            {
                this.Name = "unlock";
            }
            override
            public bool Execute(Player player)
            {
                
                if (this.HasSecondWord())
            {
                player.Unlock(this.SecondWord);
            }
                else
                {
                    player.WarningMessage("\nUnlock What?");
                }
                return false;
            }
        }
}
