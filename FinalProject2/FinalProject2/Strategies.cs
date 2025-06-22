using FinalGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject2
{
    //Battle System Strategies!
    public class AggressiveStrategy : ICombatStrategy
    {
        public void Execute(Player player, Enemy enemy)
        {
            int damage = enemy.AttackPower;
            player.TakeDamage(damage);
            Console.WriteLine($"{enemy.Name} attacked you aggressively for {damage} damage.");
        }
    }

    public class DefensiveStrategy : ICombatStrategy
    {
        public void Execute(Player player, Enemy enemy)
        {
            int reducedDamage = Math.Max(0, player.AttackPower - 5);
            enemy.TakeDamage(reducedDamage);
            Console.WriteLine($"{enemy.Name} defended and took only {reducedDamage} damage.");
        }
    }

    public class RandomStrategy : ICombatStrategy
    {
        private static Random _random = new Random();

        public void Execute(Player player, Enemy enemy)
        {
            if (_random.Next(2) == 0)
            {
                int damage = enemy.AttackPower;
                player.TakeDamage(damage);
                Console.WriteLine($"{enemy.Name} attacked you randomly for {damage} damage.");
            }
            else
            {
                Console.WriteLine($"{enemy.Name} hesitated and did nothing.");
            }
        }
    }
}
