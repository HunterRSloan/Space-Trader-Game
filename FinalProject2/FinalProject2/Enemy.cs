using FinalGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject2
{
    public class Enemy
    {
        public string Name { get; set; }
        public int Health { get; set; } = 50;
        public int AttackPower { get; set; } = 8;

        private ICombatStrategy _strategy;

        public Enemy(string name, ICombatStrategy strategy)
        {
            Name = name;
            _strategy = strategy;
        }

        public void ExecuteStrategy(Player player)
        {
            _strategy.Execute(player, this);
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            Console.WriteLine($"{Name} took {damage} damage. Health remaining: {Health}.");
        }
    }
}
