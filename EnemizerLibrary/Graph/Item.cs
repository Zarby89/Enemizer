using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class Item
    {
        public int Id { get; set; }
        public string LogicalId { get; set; }
        public string Name { get; set; }

        public Item(int id, string logicalId, string name)
        {
            this.Id = id;
            this.LogicalId = logicalId;
            this.Name = name;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Item;

            if(item == null)
            {
                return false;
            }

            return this.LogicalId == item.LogicalId;
        }

        public override int GetHashCode()
        {
            return this.LogicalId.GetHashCode();
        }
    }

    public class SpecialItem : Item
    {
        public SpecialItem(int id, string logicalId, string name)
            :base(id, logicalId, name)
        {

        }
    }

    public class BottleItem : Item
    {
        public BottleItem(int id, string logicalId, string name)
            : base(id, logicalId, name)
        {

        }
    }

    public class ConsumableItem : Item
    {
        public int FoundCount { get; private set; } = 0;
        public int UsedCount { get; private set; } = 0;

        public bool Usable { get { return FoundCount > UsedCount; } }
        public ConsumableItem(int id, string logicalId, string name)
            :base(id, logicalId, name)
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
        public ProgressiveItem(int id, string logicalId, string name)
            :base(id, logicalId, name)
        {

        }

        public void IncreaseLevel()
        {
            this.Level++;
        }
    }
}
