using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public class Galaxy
    {
        private static Galaxy _instance = null;
        public static Galaxy Instance 
        { 
            get 
            { 
                if(_instance == null)
                {
                    _instance = new Galaxy();
                }
                return _instance; 
            } 
        }

        private Room _entrance;
        public Room Entrance { get { return _entrance; } }
        public Room _exit;
        private Stack<Room> _targetRooms;
        private Player _player;
        private Galaxy()
        {
            _targetRooms = new Stack<Room>();
            _entrance = CreateWorld();

            NotificationCenter.Instance.AddObserver("PlayerDidEnterRoom", PlayerEnteredRoom);
            NotificationCenter.Instance.AddObserver("PlayerWillLeaveRoom", PlayerWillLeaveRoom);
            NotificationCenter.Instance.AddObserver("VictoryConditionMet", EndGame);
        }
        public void PlayerEnteredRoom(Notification notification)
        {
            Player player = (Player)notification.Object;
        }
            
            
        
        //Console.WriteLine("the player entered a room.")

        public void PlayerWillLeaveRoom(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player != null)
            {
                //player.WarningMessage("The player will leave ")
            }
        }
        //Output after receiving Notification that win conditions have been met
        public void EndGame(Notification notification)
        {
            int currency = (int)notification.UserInfo["Currency"];
            bool defeatedPirateKing = (bool)notification.UserInfo["DefeatedPirateKing"];

            Console.WriteLine("You Completed the Game!");
            Console.WriteLine($"Final Stats: Currency - {currency}, Defeated Pirate King - {defeatedPirateKing}");

            // Stop the game or transition to a post-game screen
            Environment.Exit(0); // Placeholder for ending the game
        }

        //Creation of Game World
        public Room CreateWorld()
        {
            Room DockingBay = new Room("Your spaceship lands on a massive orbital station known as Nexus Prime. The docking bay is bustling with activity, from traders hauling cargo to engineers fixing starships. The intercom announces, 'Welcome to Nexus Prime, the gateway to adventure.' You’ve heard rumors of two keys hidden across the galaxy, said to unlock the Pirate King's lair. Your journey begins here.");
            Room TheMarketHub = new Room("You step into the market hub, where vendors shout about their wares. The air smells of exotic spices and fuel vapors. Holographic advertisements flicker above, enticing you to buy rare goods. Among the crowd, a shady figure gestures for you to approach. What will you do?");
            Room Room1 = new Room("You dock at a derelict freighter adrift in space. The corridors are dim, lit only by flickering emergency lights. Cargo crates are strewn everywhere. As you explore, you find a crate marked 'Confidential.' It seems locked, but who knows what treasures might be inside?");
            Room Room2 = new Room("The airlock hisses as you step into an abandoned mining station orbiting a dying star. Rust covers the walls, and the sound of machinery echoes faintly. In one corner, you spot a terminal still active, displaying cryptic messages. Could it hold a clue?");
            Room Room3 = new Room("You land on a barren moon covered in fine gray dust. The horizon glows with the light of a distant nebula. Scanning the area, your sensors detect an anomaly beneath the surface. As you dig, you uncover a shimmering key-like artifact. What will you do?");
            Room Room4 = new Room("You enter a starship graveyard, where the hulks of destroyed vessels drift in silence. You navigate carefully through the debris field, your scanners picking up faint energy signatures. Among the wreckage, something glints—a piece of advanced technology or perhaps a trap?");
            Room Room5 = new Room("You arrive at a rogue planet, a dark world untethered to any star. The surface is icy and desolate, but your sensors detect a hidden facility. Inside, you find an intricate maze of corridors and locked doors. The facility hums faintly with power. What will you do?");
            Room Room6 = new Room("Your ship touches down on a lush jungle planet with towering, bioluminescent trees. Strange creatures chirp and click as you explore. Hidden among the foliage is a ruined temple-like structure, its entrance blocked by dense vines. How will you proceed?");
            Room Room7 = new Room("You descend into the planet's underworld through a vast chasm. The descent is treacherous, but you manage to land on a rocky ledge. The cavern glows with crystalline formations, and an ancient alien console hums quietly in the corner. It looks operational. What will you do?");
            Room Room8 = new Room("You enter an asteroid base concealed within a hollowed-out asteroid. The walls are lined with reflective metals, giving the impression of infinite space. In the center of the room lies a storage crate surrounded by defense turrets. A key-like device is locked inside. What’s your next move?");
            Room Room9 = new Room("You approach a heavily fortified pirate outpost orbiting a volatile gas giant. The station is crawling with pirate ships, but your scanners detect a secret docking port. Once inside, you find the outpost’s control room filled with stolen treasures and schematics. What will you do?");
            Room Room10 = new Room("You stand before the Pirate King’s stronghold, a massive station bristling with weapons and surrounded by a fleet of loyal ships. Inside, the main chamber is dark and ominous, with holographic skulls projecting eerie lights. The Pirate King awaits, his throne gleaming with trophies from past conquests. This is your final challenge. Will you prevail?");

            //Adding Items to Rooms
            Item item = new Item("Basic Laser Gun", 1.3f);
            Item decorator = new Item("Enhanced Laser", 0.4f);
            item.AddDecorator(decorator);
            decorator = new Item("Piercing Buff", 0.7f);
            item.AddDecorator(decorator);
            Room1.Drop(item);

            item = new Item("MedKit", 0.3f);
            Room10.Drop(item);

            item = new Item("armor", 3.1f);
            decorator = new Item("ornate gold", 0.1f);
            item.AddDecorator(decorator);
            Room7.Drop(item);

            item = new Item("moon key", 0.2f);
            Room3.Drop(item);


            item = new Item("gold and riches", 4.0f);
            Room10.Drop(item);

            ItemContainer chest = new ItemContainer("chest");
            Item spoon = new Item("spoon", 0.3f);
            chest.Insert(spoon);
            Item key = new Item("Asteroid key", 0.2f);
            chest.Insert(key);
            Room8.Drop(chest);

            //Connecting Rooms with Doors
            //outside.SetExit("west", boulevard);
            //boulevard.SetExit("east", outside);
            Door door = Door.ConnectRooms("east", "west", DockingBay, TheMarketHub);
            door = Door.ConnectRooms("east", "west", TheMarketHub, Room2);
            door = Door.ConnectRooms("north", "south", TheMarketHub, Room1);
            door = Door.ConnectRooms("south", "north", TheMarketHub, Room4);
            door = Door.ConnectRooms("east", "west", Room2, Room3);
            door = Door.ConnectRooms("south", "north", Room4, Room5);
            door = Door.ConnectRooms("south", "north", Room5, Room6);
            door = Door.ConnectRooms("north", "south", Room7, Room6);
            door = Door.ConnectRooms("east", "west", Room6, Room8);
            door = Door.ConnectRooms("east", "west", Room5, Room9);
            door = Door.ConnectRooms("north", "south", Room9, Room10);
           
            
            //Adding Locks to Doors
            RegularLock aLock = new RegularLock();
            Door Lockeddoor = Door.ConnectRoomsWithKey("east", "west", Room9, Room10, "Asteroid key", "Victory Door");
            aLock = new RegularLock();
            Lockeddoor.TheLock = aLock;
            Lockeddoor.Close();
            Lockeddoor.Lock();
            
            
            Door Lockeddoor3 = Door.ConnectRoomsWithKey("south", "north", Room9, Room8, "moon key", "PirateOutpost");
            aLock = new RegularLock();
            Lockeddoor3.TheLock = aLock;
            Lockeddoor3.Close();
            Lockeddoor3.Lock();
            Console.WriteLine("\n You open the door and see another hallway in front of you! You look to your left and see another door, opening it you realize it leads back to the hub you were at earlier! You brush the dirt off as you look around");
            
            
            //TrapRooms and Similar
            TrapRoom tr = new TrapRoom("Kabloom");
            Room1.Delegate = tr;

            
            EchoRoom er = new EchoRoom(5);
            Room3.Delegate = er;

            DeathRoom dr = new DeathRoom();
            Room4.Delegate = dr;

            _exit = Room10;
            _targetRooms.Push(Room3);
            _targetRooms.Push(Room7);
            _targetRooms.Push(Room10);
            _targetRooms.Push(Room10);

            return DockingBay;
        }

    }
}
