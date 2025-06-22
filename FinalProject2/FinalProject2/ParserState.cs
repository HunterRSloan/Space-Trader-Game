using FinalGame;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject2
{
    public abstract class ParserState
    {
        public string Name { set; get; }
        public abstract ParserState HandleInput(Parser parser, string input);
        public abstract void Enter(Parser parser);
        public virtual void Exit(Parser parser)
        {
            parser.Pop();
        }
    }

    public class NormalParserState : ParserState
    {
        public NormalParserState()
        {
            Name = "Normal Parser State";
        }
        override
        public ParserState HandleInput(Parser parser, string input)
        {
            ParserState stateToReturn = this;
            switch(input)
            {
                case "PlayerDidEnterBattle":
                    stateToReturn = new BattleParserState();
                    break;
                case "PlayerDidEnterTrade":
                    stateToReturn = new TradeParserState();
                    break;
                default:
                    break;

            }
            return stateToReturn;
        }
        override
        public void Enter(Parser parser)
        {

        }
        override
        public void Exit(Parser parser)
        {

        }
    }
    public class BattleParserState : ParserState
    {
        private CommandWords _battleCommands;
        public BattleParserState()
        {
            Name = "Battle Parser State";
            Command[] _commands = { new ExitBattleCommand() };
            _battleCommands = new CommandWords("Battle Commands", _commands);
        }
        override 
        public ParserState HandleInput(Parser parser, string input)
        {
            ParserState stateToReturn = this;
            switch (input)
            {
                case "PlayerDidExitBattle":
                    stateToReturn = new NormalParserState();
                    break;
                default:
                    break;
            }
            return stateToReturn;
        }
        override
        public void Enter(Parser parser)
        {
            parser.Push(_battleCommands);
        }
        override
        public void Exit(Parser parser)
        {
            parser.Pop();
        }
    }

    public class TradeParserState : ParserState
    {
        CommandWords _tradeCommands;

        public TradeParserState()
        {
            Name = "Trade Parser State";
            Command[] _commands = { new ExitTradeCommand() };
            _tradeCommands = new CommandWords("Trade Commands", _commands);
        }
        override
        public ParserState HandleInput(Parser parser, string input)
        {
            ParserState stateToReturn = this;
            switch (input)
            {
                case "PlayerDidExitTrade":
                    stateToReturn = new NormalParserState();
                    break;
                default:
                    break;
            }
            return stateToReturn;
        }
        override
        public void Enter(Parser parser)
        {
            parser.Push(_tradeCommands);
        }
        override
        public void Exit(Parser parser)
        {
            parser.Pop();
        }
        
    }
}
