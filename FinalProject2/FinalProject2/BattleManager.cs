using FinalGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject2
{
    public class BattleManager
    {
        private Player _player;
        private Enemy _enemy;

        public BattleManager(Player player, Enemy enemy)
        {
            _player = player;
            _enemy = enemy;
        }

        public void StartBattle()
        {
            Console.WriteLine($"Battle begins! You are fighting {_enemy.Name}.");

            while (_player.Health > 0 && _enemy.Health > 0)
            {
                // Player's turn
                Console.WriteLine("Choose an action: attack, defend, flee");
                string action = Console.ReadLine().ToLower();

                switch (action)
                {
                    case "attack":
                        _enemy.TakeDamage(_player.AttackPower);
                        Console.WriteLine($"You attacked {_enemy.Name} for {_player.AttackPower} damage.");
                        break;
                    case "defend":
                        _player.Defend();
                        Console.WriteLine("You brace yourself for the enemy's attack.");
                        break;
                    case "flee":
                        Console.WriteLine("You fled the battle!");
                        return; // Exit battle
                    default:
                        Console.WriteLine("Invalid action.");
                        continue;
                }

                // Check if enemy is defeated
                if (_enemy.Health <= 0)
                {
                    Console.WriteLine($"You defeated {_enemy.Name}!");
                    return;
                }

                // Enemy's turn
                _enemy.ExecuteStrategy(_player);

                // Check if player is defeated
                if (_player.Health <= 0)
                {
                    Console.WriteLine("You have been defeated!");
                    return;
                }
            }
        }
    }

}
