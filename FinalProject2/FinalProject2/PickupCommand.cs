using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public class PickupCommand : Command
    {
        public PickupCommand() : base()
        {
            this.Name = "pickup";
        }

        public override bool Execute(Player player)
        {
            if (!this.HasSecondWord())
            {
                player.WarningMessage("Pickup what?");
                return false;
            }

            string itemName = this.SecondWord;
            Item item = player.CurrentRoom.GetItem(itemName);

            if (item == null)
            {
                player.WarningMessage("That item is not here!");
            }
            else if (item.Weight + player.CurrentWeight > player.CarryCapacity)
            {
                player.WarningMessage("You cannot carry that item. It’s too heavy!");
            }
            else
            {
                player.CurrentRoom.RemoveItem(itemName);
                player.AddItem(item);
                player.InfoMessage($"You picked up {itemName}.");
            }
            return false;
        }
    }

}

