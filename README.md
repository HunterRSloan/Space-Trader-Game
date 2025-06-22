# 🚀 FinalProject2 - Space Trader RPG

A C# console-based text RPG where you play as a space merchant traveling between planets, engaging in trade, battling enemies, and uncovering secrets of the galaxy. Your goal: either amass enough wealth or defeat the dreaded Pirate King.

## 🎮 Features

- 🌌 Interplanetary travel and room-based navigation
- 💬 Command-based player interaction (e.g., `go`, `pickup`, `save`, `say`)
- ⚔️ Battle system with enemies and combat strategies
- 🛍️ Trade system and inventory management
- 🧠 Game state managed by modular command classes and event notifications
- 🧩 Save/load functionality for persistent gameplay

## 🛠️ How to Run

1. Clone this repository:
   ```bash
   git clone https://github.com/yourusername/FinalProject2.git
   cd FinalProject2
   
2. Open the solution file in Visual Studio:
  ```bash
   FinalProject2.sln
  ```

3. Build and run the project:
  Press Ctrl + F5 or click "Start Without Debugging"

## 📁 Project Structure

- Program.cs: Main entry point
- Game.cs: Core game loop
- Player.cs, Room.cs, Item.cs: Core entities
- Command.cs + Subcommands: Handle player input
- BattleManager.cs: Combat logic
- Galaxy.cs: Room and planet layout

## ✨ Future Enhancements

- Add AI behavior to the Pirate King using Finite State Machines or Reinforcement Learning
- Dialogue system with branching narratives
- Procedurally generated planets or enemies

## 📄 License

- This project is open-source under the MIT License.
