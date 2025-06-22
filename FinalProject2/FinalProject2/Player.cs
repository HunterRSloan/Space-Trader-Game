using FinalProject2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FinalGame
{
    public class Player
    {
        private Room _currentRoom = null;
        Stack<Room> VisitedRooms = new Stack<Room>();
        public int Currency { get; private set; }
        public bool DefeatedPirateKing { get; private set; }

        public Room CurrentRoom { get { return _currentRoom; } set { _currentRoom = value; } }
        public ItemContainer _inventory;
        public float CurrentWeight => _inventory.Sum(item => item.Weight);
        private string _name;
        private string _lockName;
        public float CarryCapacity { get; private set; } = 10.0f;
        bool CanMove = true;

        private ItemContainer _inventoryBackup;
        private Room _currentRoomBackup;
        private float _currentWeightBackup;
        public string Name { get; set; }
        public int Health { get; set; } = 100;
        public int AttackPower { get; set; } = 10;
        public Room StartingRoom { get; set; }

        public Player(string name, Room room)
        {
            _currentRoom = room;
            IsDead = false;
        }

        public void Walkto(string direction)
        {
            if (CanMove == true)
            {
                Door door = this.CurrentRoom.GetExit(direction);
                if (door != null)
                {
                    if (door.IsOpen)
                    {
                        Room nextRoom = door.RoomOnTheOtherSide(CurrentRoom);
                        Notification notification = new Notification("PlayerWillLeaveRoom");
                        NotificationCenter.Instance.PostNotification(notification);
                        CurrentRoom = nextRoom;
                        notification = new Notification("PlayerDidEnterRoom", this);
                        NotificationCenter.Instance.PostNotification(notification);
                        NormalMessage("\n" + this.CurrentRoom.Description());
                        VisitedRooms.Push(CurrentRoom);
                    }
                    else
                    {
                        ErrorMessage("\nThe door on " + direction + " is closed.");
                    }
                }
                else
                {
                    ErrorMessage("\nThere is no door on " + direction);
                }
            }
        }
        public void GameWon()
        {
            Console.WriteLine("Congratulations! You've won the game!");
            CanMove = false;
            DisplayVictoryMessage();
            DisplayPlayerStats();
            Console.WriteLine("Exit? (Type 'Yes')");
            string response = Console.ReadLine();
            if (response.ToLower() == "yes")
            {
                Environment.Exit(0);
            }
        }

        // Method to display a victory message
        private void DisplayVictoryMessage()
        {
            Console.WriteLine("You have successfully reached the winning room!");
            Console.WriteLine("Congratulations!");
        }

        // Method to display player's final stats or inventory
        private void DisplayPlayerStats()
        {
            Console.WriteLine("Final Player Inventory:");
            NormalMessage(_inventory.Description);
        }
        public void Open(string direction)
        {
            Door door = this.CurrentRoom.GetExit(direction);
            if(door != null)
            {
                door.Open();
                if(door.IsOpen)
                {
                    InfoMessage("\nThe door on " + direction + " is open.");
                }
                else if (door.IsLocked)
                {
                    ErrorMessage("\nThe door on " + direction + " is still closed.");
                }
            }
            else
            {
                ErrorMessage("\nThere is no door on " + direction);
            }
        }

        public void Say(string word)
        {
            Notification notification = new Notification("PlayerWillSayAWord");
            Dictionary<string, object> userInfo = new Dictionary<string, object>();
            userInfo["word"] = word;
            notification.UserInfo = userInfo;
            NotificationCenter.Instance.PostNotification(notification);
            NormalMessage(word);
            notification = new Notification("PlayerDidSayAWord", this);
            notification.UserInfo = userInfo;
            NotificationCenter.Instance.PostNotification(notification);
        }
        public void Examine(string itemName)
        {
            IItem item = CurrentRoom.Pickup(itemName);
            if (item != null)
            {
                InfoMessage(item.Description);
                CurrentRoom.Drop(item);
            }
            else
            {
                ErrorMessage("There is no item named " + itemName + " in the room.");
            }
        }
        public void Insert(Item item)
        {
                _inventory.Insert(item);
                Console.WriteLine("Added " + item.Name + " to inventory.");
        }
        public void Give(string containerName, Item item)
        {
            _inventory.Insert(item);
            Console.WriteLine("Added " + item.Name + " to inventory");
        }
        public IItem Take(String itemName)
        {
            return _inventory.Remove(itemName);
        }
        public void Pickup(String itemName)
        {
            {
                Item item = CurrentRoom.GetItem(itemName);

                if (item == null)
                {
                    Console.WriteLine($"The item '{itemName}' is not in this room.");
                    return;
                }

                if (item.Weight + CurrentWeight > CarryCapacity)
                {
                    Console.WriteLine($"You cannot carry '{itemName}'. It exceeds your carry capacity.");
                    return;
                }

                _inventory.Insert(item);
                CurrentRoom.RemoveItem(itemName);
                Console.WriteLine($"You picked up '{itemName}'. Current weight: {CurrentWeight}/{CarryCapacity}.");
            }
        }

            public void Drop(String itemName)
        {
            Item item = _inventory.TryGetValue(itemName, out item);

            if (item == null)
            {
                Console.WriteLine($"You don't have an item named '{itemName}'.");
                return;
            }

            _inventory.Remove(item.Name);
            CurrentRoom.AddItem(item);
            Console.WriteLine($"You dropped '{itemName}'. Current weight: {CurrentWeight}/{CarryCapacity}.");
        }
        public void Inventory()
        {
            NormalMessage(_inventory.Description);
            
        }

        public void AddItem(Item item)
        {
            _inventory.Insert(item);
        }
        public void RemoveItem(Item item)
        {
            _inventory.Remove(item.Name);
        }
        public ItemContainer GetInventory()
        {
            return _inventory;
        }
        public void Back()
        {
            if (VisitedRooms.Count > 1)
            {
                VisitedRooms.Pop();
                Room previousRoom = VisitedRooms.Peek();
                this.InfoMessage("\n" + this._currentRoom.Description());
            }
            else
            {
                this.OutputMessage("\n\nThere is no where to return to");
            }
        }
        public void DoorName(string name, string lockName)
        {
            _name = name;
            _lockName = lockName;
        }
        public void Unlock(string doorName)
        {
            Door door;
            if (doorName == "Closet")
            {
                door = this.CurrentRoom.GetExit("south");
                // Check if the player has the "Pantry Key" in their inventory
                _inventory.ContainsItem("pantry key");
                door.Unlock();
                InfoMessage("\nThe Walk-In Closet door is open.");
            }
            else if (doorName == "Victory")
            {
                door = this.CurrentRoom.GetExit("east");
                // Check if the player has the "Cliff Key" in their inventory
                _inventory.ContainsItem("cliff key");
                door.Unlock();
                InfoMessage("\nThe Victory door is open. Enter to claim your prize!");
            }
            else
            {
                Console.WriteLine("This door is not one that needs to be unlocked");
            }
        }
        public void EnterTrade(string target)
        {
            Notification notification = new Notification("PlayerDidEnterTrade", this);
            NotificationCenter.Instance.PostNotification(notification);
            WarningMessage("Entering Trade with " + target + "!");
        }
        public void ExitTrade()
        {
            Notification notification = new Notification("PlayerDidExitTrade", this);
            NotificationCenter.Instance.PostNotification(notification);
            InfoMessage("Exiting Trade");
        }
        public void EnterBattle(string target)
        {
            Notification notification = new Notification("PlayerDidEnterBattle", this);
            NotificationCenter.Instance.PostNotification(notification);
            WarningMessage("Entering Battle with " + target + "!");
        }
        public void ExitBattle()
        {
            Notification notification = new Notification("PlayerDidExitBattle", this);
            NotificationCenter.Instance.PostNotification(notification);
            InfoMessage("Exiting Battle");
        }
        public bool IsDead { get; private set; }
        public void Die()
        {
            Console.WriteLine("You have died. All items are dropped in this room.");

            // Create a notification for the player's death
            Notification deathNotification = new Notification(
                "PlayerDied",
                this,
                new Dictionary<string, object>
                {
                { "DroppedItems", _inventory.Description },
                { "DeathRoom", CurrentRoom }
                }
            );

            NotificationCenter.Instance.PostNotification(deathNotification);

            // Drop items in the current room
            CurrentRoom.Drop(_inventory);
            _inventory.Clear();

            // Respawn the player
            Revive();
        }
        public void Revive()
        {
            Console.WriteLine("You have revived at the starting location.");

            // Create a notification for the player's respawn
            Notification respawnNotification = new Notification(
                "PlayerRevived",
                this,
                null
            );

            NotificationCenter.Instance.PostNotification(respawnNotification);

            // Move player to the starting room
            CurrentRoom = GameStateManager.Instance.StartingRoom;
        }
        
        public void DropAllItems()
        {
            foreach (IItem item in _inventory.GetAllItems())
            {
                CurrentRoom.Drop(item); // Drop each item in the current room
            }
            _inventory.Clear(); // Clear the inventory after dropping all items
        }
        //Details condition for Winning Game
        public void EndGame()
        {
            
            if (Currency >= 1000000)
            {
                InfoMessage("Congratulations! You have become the ultimate trader with over $1 million in earnings!");
            }
            else if (DefeatedPirateKing)
            {
                InfoMessage("Congratulations! You have defeated the Pirate King and secured your place in history!");
            }
            else
            {
                InfoMessage("Game Over. Better luck next time.");
            }
            Notification endgameNotification = new Notification(
                "EndGame",
                this,
                new Dictionary<string, object>
                {
                    {"Currency", Currency },
                    {"DefeatedPirateKing", DefeatedPirateKing }
                }
            );
            Galaxy.Instance.EndGame(endgameNotification);
        }
        public void AddCurrency(int amount)
        {
            Currency += amount;
            Console.WriteLine($"You earned {amount} credits. Total: {Currency}");
        }
        public void DefeatPirateKing()
        {
            DefeatedPirateKing = true;
            Console.WriteLine("You defeated the Pirate King!");
        }
        public void TakeDamage(int damage)
        {
            Health -= damage;
            Console.WriteLine($"You took {damage} damage. Health remaining: {Health}.");
        }

        public void Defend()
        {
            // Reduce damage for next enemy attack
            Health += 5; // Example mechanic
        }


        //Delegate Messages
        public void OutputMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ColoredMessage(string message, ConsoleColor newColor)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = newColor;
            OutputMessage(message);
            Console.ForegroundColor = oldColor;
        }

        public void NormalMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.White);
        }

        public void InfoMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Blue);
        }

        public void WarningMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.DarkYellow);
        }

        public void ErrorMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Red);
        }
    }

}
