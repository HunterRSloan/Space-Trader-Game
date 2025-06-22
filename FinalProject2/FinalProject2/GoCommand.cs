using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public class GoCommand : Command
    {

        public GoCommand() : base()
        {
            this.Name = "go";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                string direction = this.SecondWord;
                Room nextRoom = player.CurrentRoom.GetRoomInDirection(direction);

                if (nextRoom != null)
                {
                    player.CurrentRoom = nextRoom;
                    player.InfoMessage($"You move {direction} into {nextRoom.Description()}.");
                }
                else
                {
                    player.WarningMessage($"You cannot go {direction} from here.");
                }
            }
            else
            {
                player.WarningMessage("Go where?");
            }
            return false;
        }

    }
}
