using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FinalGame
{
    public class Door : ILockable
    {
        private Room _room1;
        private Room _room2;
        public string _key;
        private bool _open;
        private string _name;
        private string _lockName;
        private ILockable _lock;
        public ILockable TheLock { set { _lock = value; } get { return _lock; } }
        public bool IsOpen { get { return _open; } }
        public bool IsClosed { get { return !_open; } }
        public bool IsLocked { get { return _lock == null ? false : _lock.IsLocked; } set { } }
        public bool IsUnlocked { get { return _lock == null ? true : _lock.IsUnlocked; } }

        public static string Name { get; private set; }

        public bool MayUnlock => throw new NotImplementedException();

        public bool MayLock => throw new NotImplementedException();

        public bool MayOpen => throw new NotImplementedException();

        public bool MayClose => throw new NotImplementedException();

        public Door(Room room1, Room room2)
        {
            _lock = null;
            _open = true;
            _room1 = room1;
            _room2 = room2;
        }

        public Room RoomOnTheOtherSide(Room from)
        {
            if (from == _room1)
            {
                return _room2;
            }
            else
            {
                return _room1;
            }
        }
        public bool Open()
        {
            _open = _lock == null ? true : _lock.OnOpen();
            return _open;
        }
        public bool Close()
        {
            _open = _lock == null ? false : !_lock.OnClose();
            return true;
        }
        
        public bool Lock()
        {
            if (_lock != null)
            {
                _lock.Lock();
                return true;
            }
            return false;
        }
        public void Unlock(string key)
        {
            if (_key.Equals(key))
            {
                IsLocked = false;
                Console.WriteLine("The door is now unlocked!");
            }
            else
            {
                Console.WriteLine("The key doesn't fit.");
            }
        }
        public bool OnOpen()
        {
            return _lock == null ? true : _lock.OnOpen();
        }
        public bool OnClose()
        {
            return _lock == null ? true : _lock.OnClose();
        }
        
        public static Door ConnectRooms(String label1, string label2, Room room1, Room room2)
        {
            Door door = new Door(room1, room2);
            room1.SetExit(label1, door);
            room2.SetExit(label2, door);
            return door;
        }
        public static Door ConnectRoomsWithKey(String label1, string label2, Room room1, Room room2, string itemName, string name)
        {
            Name = name;
            Door door = new Door(room1, room2);
            room1.SetExit(label1, door);
            room2.SetExit(label2, door);
            return door;
        }

        public bool Unlock()
        {
            throw new NotImplementedException();
        }
    }
}
