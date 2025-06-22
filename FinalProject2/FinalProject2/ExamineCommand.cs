using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public class ExamineCommand : Command
    {
        public ExamineCommand() : base()
        {
            this.Name = "examine";
        }
        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Examine(this.SecondWord);
            }
            else
            {
                player.WarningMessage("\nExamine What?");
            }
            return false;
        }
    }

}
