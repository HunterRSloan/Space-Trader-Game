using FinalGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject2
{
    public class SaveGameCommand : Command
    {
        public SaveGameCommand() : base()
        {
            this.Name = "save";
        }

        public override bool Execute(Player player)
        {
            GameStateManager.Instance.SaveGame();
            return false;
        }
    }

}
