using FinalProject2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public interface IRoomDelegate
    {
        Room ContainingRoom { set; get; }
        String OnTag(String fromRoom);
        Door OnGetExit(Door fromRoom);
        String OnGetExits(String fromRoom);
    }
    public interface ICloseable
    {
        bool IsOpen { get; }
        bool IsClosed { get; }
        bool Open();
        bool Close();
    }
    public interface IKeyed
    {
        IItem Insert(IItem key);
        IItem Remove();
        Boolean MayUnlock { get; }
        Boolean MayLock { get; }
        Boolean Validate {  get; }
    }

    public interface ILockable : ICloseable
    {
        bool IsLocked { get; }
        bool IsUnlocked { get; }
        bool Lock();
        bool Unlock();
        bool MayUnlock {  get; }
        bool MayLock { get; }
        bool MayOpen { get; }
        bool MayClose { get; }
        bool OnOpen();
        bool OnClose();
    }
    public interface IItem
    {
        string Name { get; }
        float Weight { get; }
        String LongName { get; }
        float SellValue { get; }
        float BuyValue { get; }
        string Description { get; }
        void AddDecorator(IItem decorator);
        bool IsContainer { get; set; }
        IItem Clone();
    }
    public interface IItemContainer : IItem
    {
        bool Any(Func<object, bool> value);
        bool ContainsItem(string itemName);
        void Insert(IItem item);
        IItem Remove(string itemName);
        bool TryGetValue(string itemName, out IItem item);
    }
    public interface ICombatStrategy
    {
        void Execute(Player player, Enemy enemy);
    }
}
