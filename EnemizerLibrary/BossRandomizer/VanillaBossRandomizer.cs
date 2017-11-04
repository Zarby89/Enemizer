using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class VanillaBossResetter
    {
        List<Dungeon> DungeonPool { get; set; } = new List<Dungeon>();
        List<Boss> bosses = new List<Boss>();

        void FillDungeonPool()
        {
            DungeonPool.Add(new EasternPalaceDungeon());
            DungeonPool.Add(new DesertPalaceDungeon());
            DungeonPool.Add(new TowerOfHeraDungeon());
            DungeonPool.Add(new PalaceOfDarknessDungeon());
            DungeonPool.Add(new SwampPalaceDungeon());
            DungeonPool.Add(new SkullWoodsDungeon());
            DungeonPool.Add(new ThievesTownDungeon());
            DungeonPool.Add(new IcePalaceDungeon());
            DungeonPool.Add(new MiseryMireDungeon());
            DungeonPool.Add(new TurtleRockDungeon());
            DungeonPool.Add(new GT1Dungeon());
            DungeonPool.Add(new GT2Dungeon());
            DungeonPool.Add(new GT3Dungeon());
        }

        protected void FillBossPool()
        {
            bosses.Add(new ArmosBoss());
            bosses.Add(new LanmolaBoss());
            bosses.Add(new MoldormBoss());
            bosses.Add(new HelmasaurBoss());
            bosses.Add(new ArrghusBoss());
            bosses.Add(new MothulaBoss());
            bosses.Add(new BlindBoss());
            bosses.Add(new KholdstareBoss());
            bosses.Add(new VitreousBoss());
            bosses.Add(new TrinexxBoss());
            bosses.Add(new GTArmosBoss()); // GT1
            bosses.Add(new GTLanmolaBoss()); // GT2
            bosses.Add(new GTMoldormBoss()); // GT3
        }

        public void ResetRom(RomData romData)
        {
            FillDungeonPool();
            FillBossPool();

            ResetBosses();

            WriteRom(romData);
        }

        void ResetBosses()
        {
            for(int i = 0; i < bosses.Count; i++)
            {
                DungeonPool[i].SelectedBoss = bosses[i];
            }
        }

        private void WriteRom(RomData romData)
        {
            foreach (var dungeon in DungeonPool)
            {
                romData[dungeon.DungeonRoomSpritePointerAddress] = dungeon.SelectedBoss.BossPointer[0];
                romData[dungeon.DungeonRoomSpritePointerAddress + 1] = dungeon.SelectedBoss.BossPointer[1];

                romData[AddressConstants.dungeonHeaderBaseAddress + ((dungeon.BossRoomId * 14) + 3)] = dungeon.SelectedBoss.BossGraphics;
            }
        }
    }
}
