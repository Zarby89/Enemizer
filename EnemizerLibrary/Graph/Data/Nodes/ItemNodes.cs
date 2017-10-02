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
                if(GameItems.Items.ContainsKey(raw.Value.ItemName))
                {
                    Nodes.Add(raw.Key, new ItemLocation(raw.Value.LogicalId, raw.Value.LocationName, GameItems.Items[raw.Value.ItemName]));
                }
                else if(raw.Value.ItemName.ToLower() == "key")
                {
                    Nodes.Add(raw.Key, new ItemLocation(raw.Value.LogicalId, raw.Value.LocationName, GetDungeonKey(raw.Value)));
                }
                else if (raw.Value.ItemName.ToLower() == "big key")
                {
                    Nodes.Add(raw.Key, new ItemLocation(raw.Value.LogicalId, raw.Value.LocationName, GetDungeonBigKey(raw.Value)));
                }
                else
                {
                    throw new Exception($"FillItemNodes - Unknown item name found '{raw.Value.ItemName}'");
                }
            }
        }

        static Item GetDungeonKey(RawItemLocation raw)
        {
            if(raw.ItemName.ToLower() != "key")
            {
                throw new Exception("GetDungeonKey - Not a key!");
            }
            var dungeon = Dungeons.GetDungeonFromRoom(raw.RoomId);
            var dungeonKey = Dungeons.DungeonKeys[dungeon];
            return GameItems.Items[dungeonKey];
        }

        static Item GetDungeonBigKey(RawItemLocation raw)
        {
            if (raw.ItemName.ToLower() != "big key")
            {
                throw new Exception("GetDungeonBigKey - Not a big key!");
            }
            var dungeon = Dungeons.GetDungeonFromRoom(raw.RoomId);
            var dungeonKey = Dungeons.DungeonBigKeys[dungeon];
            return GameItems.Items[dungeonKey];
        }
    }
}
