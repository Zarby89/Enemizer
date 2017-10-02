using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary.Data
{
    public class ItemLocations
    {
        RawItemLocationCollection _rawItemLocations;

        public ItemLocations(RawItemLocationCollection rawItemLocations)
        {
            this._rawItemLocations = rawItemLocations;

            Nodes = new Dictionary<string, ItemLocation>();
            FillItemNodes();
        }

        public Dictionary<string, ItemLocation> Nodes { get; }

        private void FillItemNodes()
        {
            foreach(var raw in _rawItemLocations.RawItemLocations)
            {
                Item item = GameItems.Items.Values.Where(x => x.Id == raw.Value.ItemId).FirstOrDefault();
                if(item == null && GameItems.Items.Values.Any(x => x.LogicalId == raw.Value.ItemName)) // fake items
                {
                    item = GameItems.Items.Values.Where(x => x.LogicalId == raw.Value.ItemName).FirstOrDefault();
                }

                if (item == null)
                {
                    throw new Exception($"FillItemNodes - Unknown item name found '{raw.Value.ItemName}'");
                }

                if (item.Id == GameItems.Key)
                {
                    item = GetDungeonKey(raw.Value);
                }
                if(item.Id == GameItems.Big_Key)
                {
                    item = GetDungeonBigKey(raw.Value);
                }

                Nodes.Add(raw.Key, new ItemLocation(raw.Value.LogicalId, raw.Value.LocationName, item));
            }
        }

        static Item GetDungeonKey(RawItemLocation raw)
        {
            if(raw.ItemId != GameItems.Key)
            {
                throw new Exception("GetDungeonKey - Not a key!");
            }
            var dungeon = Dungeons.GetDungeonFromRoom(raw.RoomId);
            var dungeonKey = Dungeons.DungeonKeys[dungeon];
            var item = GameItems.Items.Values.Where(x => x.LogicalId == dungeonKey).FirstOrDefault();
            if(item == null)
            {
                throw new Exception($"GetDungeonKey - invalid key item {dungeonKey}");
            }
            return item;
        }

        static Item GetDungeonBigKey(RawItemLocation raw)
        {
            if (raw.ItemId != GameItems.Big_Key)
            {
                throw new Exception("GetDungeonBigKey - Not a big key!");
            }
            var dungeon = Dungeons.GetDungeonFromRoom(raw.RoomId);
            var dungeonKey = Dungeons.DungeonBigKeys[dungeon];
            var item = GameItems.Items.Values.Where(x => x.LogicalId == dungeonKey).FirstOrDefault();
            if (item == null)
            {
                throw new Exception($"GetDungeonBigKey - invalid big key item {dungeonKey}");
            }
            return item;
        }
    }
}
