using FinalGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject2
{
    public class LoadGameCommand : Command
    {
        public LoadGameCommand() : base()
        {
            this.Name = "load";
        }

        public override bool Execute(Player player)
        {
            GameStateManager.Instance.LoadGame();
            return false;
        }
    }

}
