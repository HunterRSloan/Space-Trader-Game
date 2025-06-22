using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public class QuitCommand : Command
    {

        public QuitCommand() : base()
        {
            this.Name = "quit";
        }

        override
        public bool Execute(Player player)
        {
            bool answer = true;
            if (this.HasSecondWord())
            {
                player.WarningMessage("\nI cannot quit " + this.SecondWord);
                answer = false;
            }
            return answer;
        }
    }
}
