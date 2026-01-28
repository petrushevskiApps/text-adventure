using System.Collections.Generic;
using Game.Components;

namespace Game.InventorySystem
{
    public class Inventory: IInventory
    {
        private readonly Dictionary<string, IItem> _items;

        public Inventory()
        {
            _items = new Dictionary<string, IItem>();
        }
        public bool StoreItem(IItem item)
        {
            return _items.TryAdd(item.Name, item);
        }

        public IItem GetItem(string itemKey)
        {
            return _items.TryGetValue(itemKey, out IItem value) ? value : null;
        }

        public bool ContainsItem(string key)
        {
            return _items.ContainsKey(key);
        }
    }
}