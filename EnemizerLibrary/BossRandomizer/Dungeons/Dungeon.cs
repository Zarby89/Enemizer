using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public abstract class Dungeon
    {
        public string Name { get; set; }
        public int Priority { get; set; } = 255;
        public int? DungeonCrystalTypeAddress { get; set; }
        public int? DungeonCrystalAddress { get; set; }
        public BossType BossType { get; set; } // TODO: need?
        public Boss SelectedBoss { get; set; }
        public int BossRoomId { get; set; }
        public int BossAddress { get; set; }
        public int? BossDropItemAddress { get; set; }

        public List<BossType> DisallowedBosses { get; protected set; } = new List<BossType>();

        public DungeonType DungeonType { get; protected set; } = DungeonType.NotSet;

        protected Dungeon(int priority)
        {
            Priority = priority;
        }
    }

}
