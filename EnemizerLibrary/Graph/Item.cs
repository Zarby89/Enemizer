using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Item(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Item;

            if(item == null)
            {
                return false;
            }

            return this.Id == item.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }

    public class SpecialItem : Item
    {
        public SpecialItem(string id, string name)
            :base(id, name)
        {

        }
    }

    public class ConsumableItem : Item
    {
        public int FoundCount { get; private set; } = 0;
        public int UsedCount { get; private set; } = 0;

        public bool Usable { get { return FoundCount > UsedCount; } }
        public ConsumableItem(string id, string name)
            :base(id, name)
        {

        }

        public void IncreaseCount()
        {
            this.FoundCount++;
        }

        public void Consume()
        {
            this.UsedCount++;
        }
    }

    public class ProgressiveItem : Item
    {
        public int Level { get; private set; }
        public ProgressiveItem(string id, string name)
            :base(id, name)
        {

        }

        public void IncreaseLevel()
        {
            this.Level++;
        }
    }
}
