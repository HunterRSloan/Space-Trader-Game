using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public class Game
    {
        private Player _player;
        private Parser _parser;
        private bool _playing;

        public Game()
        {
            _playing = false;
            _parser = new Parser(new CommandWords());
            _player = new Player("Player 1", Galaxy.Instance.Entrance);
        }

        /**
     *  Main play routine.  Loops until end of play.
     */
        public void Play()
        {

            // Enter the main command loop.  Here we repeatedly read commands and
            // execute them until the game is over.

            bool finished = false;
            while (!finished)
            {
                Console.Write("\n>");
                Command command = _parser.ParseCommand(Console.ReadLine());
                if (command == null)
                {
                    _player.ErrorMessage("I don't understand...");
                }
                else
                {
                    finished = command.Execute(_player);
                }
            }
        }


        public void Start()
        {
            _playing = true;
            _player.InfoMessage(Welcome());
        }

        public void End()
        {
            _playing = false;
            _player.InfoMessage(Goodbye());
        }

        public string Welcome()
        {
            return "Welcome to the Twelve Realms Game! This journey will set you one a quest for the lost keys to gain access to your richest dream! \n\nType 'help' if you need help." + _player.CurrentRoom.Description();
        }

        public string Goodbye()
        {
            return "\nThank you for playing, Goodbye. \n";
        }

    }
}

