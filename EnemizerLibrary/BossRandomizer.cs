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

        Random rand;
        public BossRandomizer(Random rand)
        {
            this.rand = rand;

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

        public void GenerateRandomizedBosses(RomData romData)
        {
            foreach(var dungeon in this.DungeonPool.OrderBy(x => x.Priority))
            {
                var possibleBosses = this.PossibleBossesPool.Where(x => dungeon.DisallowedBosses.Contains(x.BossType) == false);
                if(possibleBosses.Count() == 0)
                {
                    throw new Exception($"Couldn't find any possible bosses not disallowed for dungeon: {dungeon.Name}");
                }

                possibleBosses = possibleBosses.Where(x => x.CheckRules(dungeon, romData) == false);
                if (possibleBosses.Count() == 0)
                {
                    throw new Exception($"Couldn't find any possible bosses meeting item checks for dungeon: {dungeon.Name}");
                }

                Boss boss = possibleBosses.ElementAt(rand.Next(possibleBosses.Count()));

                dungeon.SelectedBoss = boss;

                this.PossibleBossesPool.Remove(boss);
            }
        }
    }
}
