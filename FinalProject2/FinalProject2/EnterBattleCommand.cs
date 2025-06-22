using FinalGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject2
{
    public class EnterBattleCommand : Command
    {
        public EnterBattleCommand() : base()
        {
            this.Name = "EnterBattle";
        }
        override
        public bool Execute(Player player)
        {
            Enemy enemy = new Enemy("Space Pirate", new RandomStrategy());
            BattleManager battleManager = new BattleManager(player, enemy);
            battleManager.StartBattle();
            return false;
        }
    }
}
