using FinalProject2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public class CommandWords
    {
        private Dictionary<string, Command> _commands;
        public string Name { set; get; }
        private static Command[] _commandArray = { new GoCommand(), new QuitCommand(), new SayCommand(), new OpenCommand(), new ExamineCommand(), new PickupCommand(), new DropCommand(), new InventoryCommand(), new UnlockCommand(), new BackCommand(), new EnterBattleCommand(), new ExitBattleCommand(), new EnterTradeCommand(), new ExitTradeCommand(), new SaveGameCommand(), new LoadGameCommand() };

        public CommandWords() : this("Normal Commands", _commandArray) { }

        // Designated Constructor
        public CommandWords(string name, Command[] commandList)
        {
            Name = name;
            _commands = new Dictionary<string, Command>();
            foreach (Command command in commandList)
            {
                _commands[command.Name] = command;
            }
            Command help = new HelpCommand(this);
            _commands[help.Name] = help;
        }

        public Command Get(string word)
        {
            Command command = null;
            _commands.TryGetValue(word, out command);
            return command;
        }

        public string Description()
        {
            string commandNames = Name + ": ";
            Dictionary<string, Command>.KeyCollection keys = _commands.Keys;
            foreach (string commandName in keys)
            {
                commandNames += " " + commandName;
            }
            return commandNames;
        }
    }
}
