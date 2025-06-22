using FinalProject2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public class Room
    {
        private Dictionary<string, Door> _exits;
        private string _tag;
        private IRoomDelegate _delegate;
        private ItemContainer _itemsBackup;
        private Dictionary<string, Door> _exitsBackup;
        private Dictionary<string, Item> _items = new Dictionary<string, Item>();
        public IRoomDelegate Delegate
        {
            set
            {
                if (value != null && value.ContainingRoom != null)
                {
                    value.ContainingRoom.Delegate = null;
                }
                _delegate = value;
                if (_delegate != null)
                {
                    _delegate.ContainingRoom = this;
                }
            }
        }

        public string Tag { get { return _delegate == null ? _tag : _delegate.OnTag(_tag); } set { _tag = value; } }

        public Room() : this("No Tag") { }

        // Designated Constructor
        public Room(string tag)
        {
            _delegate = null;
            _exits = new Dictionary<string, Door>();
            this.Tag = tag;
        }

        public void SetExit(string exitName, Door door)
        {
            _exits[exitName] = door;
        }

        public Door GetExit(string exitName)
        {
            Door door = null;
            _exits.TryGetValue(exitName, out door);
            return _delegate == null ? door : _delegate.OnGetExit(door);
        }

        public string GetExits()
        {
            string exitNames = "Exits: ";
            Dictionary<string, Door>.KeyCollection keys = _exits.Keys;
            foreach (string exitName in keys)
            {
                exitNames += " " + exitName;
            }

            return _delegate == null ? exitNames : _delegate.OnGetExits(exitNames);
        }
        public void Insert(IItem item)
        {
            _items.Insert(item);
        }
        public void Drop(IItem item)
        {
            _items.Insert(item);  // Assuming "_items" is the container name for the room
            Console.WriteLine("Item dropped in room: " + item.Description);
        }
        public IItem Pickup(string itemName)
        {
            IItem item = null;
            if (_items.TryGetValue(itemName, out item))
            {
                _items.Remove(itemName);
                return item;
            }
            return null;
        }
        public string Description()
        {
            return "You are " + this.Tag + ".\n *** " + this.GetExits();
        }
        public void ClearItems()
        {
            _items.Clear();
        }

        // Method to restore the state of the room to the backed-up state
      
        public void OnPlayerDidEnterRoom(Player player)
        {
            Console.WriteLine($"You've entered {GetType().Name}.");
            Console.WriteLine(Description());

            // Add additional actions when a player enters a room if needed
        }
        public void AddItems(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                _items[item.Name] = item;
            }
        }
        public void AddItem(Item item)
        {
            _items[item.Name] = item;
        }
        public void RemoveItem(string itemName)
        {
            _items.Remove(itemName);
        }
        public Item GetItem(string itemName)
        {
            _items.TryGetValue(itemName, out Item item);
            return item;
        }
        public IEnumerable<Item> GetItems()
        {
            return _items.Values;
        }
        public Room GetRoomInDirection(string direction)
        {
            if (_exits.TryGetValue(direction, out Door door) && !door.IsLocked)
            {
                return door.RoomOnTheOtherSide(this);
            }
            return null;
        }

    }
    public class TrapRoom : IRoomDelegate
    {
        private bool _active;
        private String _password;
        private Room _containingRoom;

        public Room ContainingRoom
        {
            set
            {
                _containingRoom = value;
            }
            get
            {
                return _containingRoom;
            }
        }
        public TrapRoom(String password)
        {

            _active = true;
            _password = password;
            NotificationCenter.Instance.AddObserver("PlayerDidSayAWord", OnPlayerDidSayAWord);

        }
        public String OnTag(String fromRoom)
        {
            return _active ? "in a TRAP ROOM" : fromRoom;
        }
        public Door OnGetExit(Door fromRoom)
        {
            return _active ? null : fromRoom;
        }
        public String OnGetExits(String fromRoom)
        {
            return _active ? "Muahahahaha \n" : "" + fromRoom;
        }
        public void OnPlayerDidSayAWord(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player != null)
            {
                if (player.CurrentRoom == ContainingRoom)
                {
                    String word = (String)notification.UserInfo["word"];
                    if (word.Equals(_password))
                    {
                        _active = false;
                        player.InfoMessage("The trap has been disabled");
                        player.InfoMessage(player.CurrentRoom.Description());
                    }
                    else
                    {
                        player.ErrorMessage("Ah, Ah, Ah, you didn't say the magic word");
                    }
                }
            }
        }

    }
    public class EchoRoom : IRoomDelegate
    {
        private int _times;
        private Room _containingRoom;
        public Room ContainingRoom
        {
            set
            {
                _containingRoom = value;
            }
            get
            {
                return _containingRoom;
            }

        }
        public EchoRoom(int times)
        {
            _times = times;
            NotificationCenter.Instance.AddObserver("PlayerDidSayAWord", OnPlayerDidSayAWord);
        }
        public String OnTag(String fromRoom)
        {
            return "*" + fromRoom;
        }
        public Door OnGetExit(Door fromRoom)
        {
            return fromRoom;
        }
        public String OnGetExits(String fromRoom)
        {
            return fromRoom;
        }
        public void OnPlayerDidSayAWord(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player != null)
            {
                if (player.CurrentRoom == ContainingRoom)
                {
                    String word = (String)notification.UserInfo["word"];
                    String echo = "";
                    for (int i = 0; i < _times; i++)
                    {
                        echo += word + " ";
                    }
                    player.NormalMessage(echo);
                }
            }
        }
    }
    public class DeathRoom : IRoomDelegate
    {
        private Room _containingRoom;
        private bool _playerDied;
        private bool _isActive;
        private bool _activatedOnce;
        public Room ContainingRoom
        {
            set { _containingRoom = value; }
            get { return _containingRoom; }
        }

        public DeathRoom()
        {
            _playerDied = false;
            _isActive = true;
            _activatedOnce = false;
            NotificationCenter.Instance.AddObserver("PlayerDidEnterRoom", OnPlayerDidEnterRoom);
        }

        public void OnPlayerDidEnterRoom(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player != null && player.CurrentRoom == ContainingRoom)
            {
                if (_isActive)
                {
                    if (!_activatedOnce)
                    {
                        // Perform actions for the first activation (drop items, etc.)
                        player.OutputMessage("You entered the death room. Your items have been dropped.");
                        player.DropAllItems();
                        _activatedOnce = true;
                    }
                    else
                    {
                        // Send player back to the last checkpoint
                        GameStateManager.Instance.LoadGame(); // Load the saved game state from the last checkpoint
                        _isActive = false; // Deactivate the death logic for subsequent visits
                    }
                }
            }
        }

        private void MoveItemsToDeathRoom(Player player)
        {
            if (player.IsDead) // Assuming you have a property 'IsDead' in the Player class
            {
                foreach (var item in player._inventory.GetAllItems())
                {
                    player.CurrentRoom.Insert(item);
                }
                player._inventory.Clear();
            }
        }
        public void SetPlayerDeathStatus(bool status)
        {
            _playerDied = status;
        }
        public string OnTag(string fromRoom)
        {
            return "You've entered the Death Room. Game over.";
        }

        public Door OnGetExit(Door fromRoom)
        {
            return null; // As this is a Death Room, it doesn't lead anywhere
        }

        public string OnGetExits(string fromRoom)
        {
            return "There are no exits. You're trapped in the Death Room.";
        }

    }
}






