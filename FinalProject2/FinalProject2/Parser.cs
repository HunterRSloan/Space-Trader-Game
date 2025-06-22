using FinalProject2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public class Parser
    {
        private Stack<CommandWords> _commands;
        ParserState _state;

        public Parser() : this(new CommandWords()) { }

        // Designated Constructor
        public Parser(CommandWords newCommands)
        {
            _commands = new Stack<CommandWords>();
            Push(newCommands);
            _state = new NormalParserState();
            _state.Enter(this);
            NotificationCenter.Instance.AddObserver("PlayerDidEnterBattle", HandleInput);
            NotificationCenter.Instance.AddObserver("PlayerDidEnterTrade", HandleInput);
            NotificationCenter.Instance.AddObserver("PlayerDidExitBattle", HandleInput);
            NotificationCenter.Instance.AddObserver("PlayerDidExitBattle", HandleInput);
        }
        public void HandleInput(Notification notification)
        {
            ParserState potentialState = _state.HandleInput(this, notification.Name);
            if (potentialState != _state) 
            {
                Player player = (Player)notification.Object;
                player.InfoMessage("Exiting " + _state.Name);
                _state.Exit(this);
                _state = potentialState;
                player.InfoMessage("Entering " + _state.Name);
                _state.Enter(this);
            }
        }
        public void Push(CommandWords _commandsToPush)
        {
            _commands.Push(_commandsToPush);
        }
        public CommandWords Pop()
        {
            return _commands.Pop();
        }

        public Command ParseCommand(string commandString)
        {
            Command command = null;
            string[] words = commandString.Split(' '); // Split into at most two parts
            if (words.Length > 0)
            {
                string word1 = words[0];
                command = _commands.Get(words[0]); // Get the primary command
                if (command != null && words.Length > 1)
                {
                    command.SecondWord = words[1]; // Capture the remainder as the second part
                }
                else
                {
                    command.SecondWord = null;
                }
            }
            else
            {
                Console.WriteLine("No command recognized!");
            }
            return command;
        }


        public string Description()
        {
            foreach (var command in _commands)
            {
                Console.WriteLine(command.Description());
            }
            return Description();
        }
    }
}

