using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class BossRandomizer
    {
        public List<Boss> PossibleBossesPool { get; set; } = new List<Boss>();

        public List<Dungeon> DungeonPool { get; set; } = new List<Dungeon>();

        public BossRandomizer()
        {
            FillDungeonPool();
            FillBossPool();
        }

        void FillDungeonPool()
        {
            DungeonPool.Add(new EasternPalaceDungeon());
            DungeonPool.Add(new DesertPalaceDungeon());
            DungeonPool.Add(new TowerOfHeraDungeon(1));
            DungeonPool.Add(new PalaceOfDarknessDungeon());
            DungeonPool.Add(new SwampPalaceDungeon());
            DungeonPool.Add(new SkullWoodsDungeon());
            DungeonPool.Add(new ThievesTownDungeon());
            DungeonPool.Add(new IcePalaceDungeon());
            DungeonPool.Add(new MiseryMireDungeon());
            DungeonPool.Add(new TurtleRockDungeon());
            DungeonPool.Add(new GT1Dungeon());
            DungeonPool.Add(new GT2Dungeon());
            DungeonPool.Add(new GT3Dungeon(2));
        }

        protected void FillBossPool()
        {
            FillBasePool();
            FillGTPool();
        }

        protected void FillBasePool()
        {
            PossibleBossesPool.Add(new ArmosBoss());
            PossibleBossesPool.Add(new LanmolaBoss());
            PossibleBossesPool.Add(new MoldormBoss());
            PossibleBossesPool.Add(new HelmasaurBoss());
            PossibleBossesPool.Add(new ArrghusBoss());
            PossibleBossesPool.Add(new MothulaBoss());
            PossibleBossesPool.Add(new BlindBoss());
            PossibleBossesPool.Add(new KholdstareBoss());
            PossibleBossesPool.Add(new VitreousBoss());
            PossibleBossesPool.Add(new TrinexxBoss());
        }

        protected void FillGTPool()
        {
            PossibleBossesPool.Add(new ArmosBoss()); // GT1
            PossibleBossesPool.Add(new LanmolaBoss()); // GT2
            PossibleBossesPool.Add(new MoldormBoss()); // GT3
        }
    }
}
