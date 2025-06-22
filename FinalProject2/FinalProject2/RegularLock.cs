using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public class RegularLock : ILockable
    {
        private bool _locked;
        public bool IsOpen { get { return true; } }
        public bool IsClosed { get { return false; } }
        public RegularLock()
        {
            _locked = false;
        }
        public bool Open()
        {
            return true;
        }
        public bool Close()
        {
            return true;
        }
        public bool IsLocked { get { return _locked; } }
        public bool IsUnlocked { get { return !_locked; } }

        public bool MayUnlock => throw new NotImplementedException();

        public bool MayLock => throw new NotImplementedException();

        public bool MayOpen => throw new NotImplementedException();

        public bool MayClose => throw new NotImplementedException();

        public bool Lock()
        {
            _locked = true;
            return true;
        }
        public bool Unlock()
        {
          _locked= false;
           return true;
        }
        public bool OnOpen()
        {
            return !_locked;
        }
        public bool OnClose()
        {
            return true;
        }
    }
}
