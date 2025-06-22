using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    public class Item : IItem
    {
        private String _name;
        private float _weight;
        private float _sellValue;
        private float _buyValue;
        public String Name { get { return _name; } private set { _name = value; } }
        public float Weight { get { return _weight + (_decorator == null ? 0 : _decorator.Weight); } private set { _weight = value; } }
        public float SellValue { get { return _sellValue + (_decorator == null ? 0 : _decorator.SellValue); } private set { _sellValue = value; } }
        public float BuyValue { get { return _buyValue + (_decorator == null ? 0 : _decorator.BuyValue); } private set { _buyValue = value; } }
        public virtual string Description { get { return Name + ", " + Weight + ", sell value = " + SellValue + ", buy value = " + BuyValue; } }

        private IItem _decorator;
        public Item() : this("Nameless") { }
        public Item(String name) : this(name, 1) { }
        public Item(string name, float weight) : this(name, weight, 1) { }
        public Item(string name, float weight, float sellValue) : this(name, weight, sellValue, 1) { }
        public Item(string name, float weight, float sellValue, float buyValue)
        {
            Name = name;
            Weight = weight;
            SellValue = sellValue;
            BuyValue = buyValue;
            _decorator = null;
        }
        public void AddDecorator(IItem decorator)
        {
            if (_decorator == null)
            {
                _decorator = decorator;
            }
            else
            {
                _decorator.AddDecorator(decorator);
            }
        }
        public String LongName { get { return Name + (_decorator == null ? "" : ", " + _decorator.LongName); } }

        public bool IsContainer { get; set; }
        public IItem Clone()
        {
            // Call the constructor with existing properties to create a new instance
            return new Item(_name, _weight);
        }
    }
    public class ItemContainer : Item, IItemContainer
    {
        public Dictionary<String, IItem> _container;
        public List<Item> Items = new List<Item>();

        public ItemContainer(string name) : base(name)
        {
            _container = new Dictionary<String, IItem>();
        }
        public int MaxCapacity { get; private set; } // Define MaxCapacity property


        public void Insert(IItem item)
        {
            _container[item.Name] = item;
        }
        public IItem Remove(String itemName)
        {
            IItem itemToRemove = null;
            _container.TryGetValue(itemName, out itemToRemove);
            return itemToRemove; // Return null if the item is not found
        }
        public bool ContainsItem(string itemName)
        { 
            if (_container.ContainsKey(itemName))
            {
                    return true;
                
            }
            return false;
        }
       

        override
        public String Description
        {
            get 
            {
                string description = base.Description;
                description += "Contents\n";
                foreach (IItem item in _container.Values)
                {
                    description += "\t" + item.Description + "\n";
                }
                return description;
            }
        }

        public bool Any(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
        public bool TryGetValue(string itemName, out IItem item)
        {
            return _container.TryGetValue(itemName, out item);
        }
        
        public List<IItem> GetAllItems()
        {
            return _container.Values.ToList(); // Return a list of all items
        }

        public void Clear()
        {
            _container.Clear(); // Clear the dictionary of items
        }
    }
}
    
