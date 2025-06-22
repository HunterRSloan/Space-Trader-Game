using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using FinalGame;
using Newtonsoft.Json;
using System.Xml;



namespace FinalProject2
{
    public class GameStateManager
    {
        private static GameStateManager _instance;
        public static GameStateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameStateManager();
                }
                return _instance;
            }
        }
        public Room StartingRoom { get; private set; }
        public Player _player { get; set; }
        public List<Room> Rooms { get; set; } = new List<Room>();
        private string saveFilePath = "savegame.json";
        private GameState _gameState;
        private GameStateManager()
        {
            // Initialize the game state with default values or load from a saved state
            // For demonstration purposes, I'll create a default player and room
            StartingRoom = new Room("Default Room");
            _player = new Player("Default Player", StartingRoom);
            // Create the initial game state with the default player and room
            _gameState = new GameState(_player, Rooms); ; // Pass the player and room here
        }
        public void InitializeGame(Player player, Room startingRoom)
        {
            _player = player;
            StartingRoom = startingRoom;
        }

        public void SaveGame()
        {
            try
            {
                var gameState = new
                {
                    Player = new
                    {
                        CurrentRoom = GameStateManager.Instance._player.CurrentRoom.Tag,
                        Health = _player.Health,
                        Inventory = _player.GetInventory().Select(item => new { item.Name, item.Weight }).ToList()
                    },
                    Rooms = Rooms.Select(room => new
                    {
                        room.Tag,
                        Items = room.GetItems().Select(item => new { item.Name, item.Weight }).ToList()
                    }).ToList()
                };

                string json = JsonConvert.SerializeObject(gameState, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(saveFilePath, json);

                Console.WriteLine("Game saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving game: {ex.Message}");
            }
        }

        public void LoadGame()
        {
            try
            {
                if (!File.Exists(saveFilePath))
                {
                    Console.WriteLine("No saved game found.");
                    return;
                }

                string json = File.ReadAllText(saveFilePath);
                var gameState = JsonConvert.DeserializeObject<GameState>(json);

                // Restore player state
                GameStateManager.Instance._player.Health = gameState.Player.Health;
                GameStateManager.Instance._player.CurrentRoom = Rooms.First(room => room.Tag == gameState.Player.CurrentRoom);

                foreach (var item in gameState.Player.Inventory)
                {
                    GameStateManager.Instance._player.AddItem(new Item(item.Name, item.Weight));
                }

                // Restore room states
                foreach (var roomState in gameState.Rooms)
                {
                    var room = Rooms.First(r => r.Tag == roomState.Tag);
                    room.ClearItems();
                    foreach (var item in roomState.Items)
                    {
                        room.AddItem(new Item(item.Name, item.Weight));
                    }
                }

                Console.WriteLine("Game loaded successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading game: {ex.Message}");
            }
        }
    }
    public class GameState
    {
        public PlayerState Player { get; set; }
        public List<RoomState> Rooms { get; set; }
        public GameState(Player player, List<Room> rooms)
        {
            Room room1 = rooms[0];
            Player player1 = player;
        }
    }

    public class PlayerState
    {
        public string CurrentRoom { get; set; }
        public int Health { get; set; }
        public List<ItemState> Inventory { get; set; }
    }

    public class RoomState
    {
        public string Tag { get; set; }
        public List<ItemState> Items { get; set; }
    }

    public class ItemState
    {
        public string Name { get; set; }
        public float Weight { get; set; }
    }



}

