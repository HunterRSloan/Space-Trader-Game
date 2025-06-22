using FinalGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject2
{
    public class EnterTradeCommand : Command
    {
        public EnterTradeCommand() : base()
        {
            this.Name = "EnterTrade";
        }
        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                // Assuming the second word specifies the target of the battle
                string target = this.SecondWord;

                // Trigger the battle logic with the specified target
                if (player.EnterTrade(target))
                {
                    player.InfoMessage($"You have entered a trade with {target}!");
                    return false; // Game continues
                }
                else
                {
                    player.WarningMessage($"There is no one named {target} to trade with!");
                    return false; // Game continues
                }
            }
            else
            {
                player.WarningMessage("Enter a trade with whom?");
                return false; // Game continues
            }
        }
    }
}
