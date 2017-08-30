using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class Boss
    {
        public BossType BossType { get; set; }
        public List<Func<Dungeon, RomData, byte[], bool>> Rules { get; set; } = new List<Func<Dungeon, RomData, byte[], bool>>();
        public byte[] BossPointer { get; internal set; }
        public byte BossGraphics { get; internal set; }

        public Boss(BossType bossType)
        {
            BossType = bossType;
            FillRules();
            BossPointer = null;
            BossGraphics = 0;
        }

        protected void FillRules()
        {

        }

        public virtual bool CheckRules(Dungeon dungeon, RomData romData)
        {
            return CheckRules(dungeon, romData, null);
        }

        protected bool CheckRules(Dungeon dungeon, RomData romData, params byte[] items)
        {
            bool result = false;
            foreach (var rule in Rules)
            {
                result |= rule.Invoke(dungeon, romData, items);
            }

            return result;
        }

        protected Func<Dungeon, RomData, byte[], bool> CheckShabadooHasItem = (Dungeon dungeon, RomData romData, byte[] items) =>
        {
            if (dungeon.DungeonCrystalAddress == null || dungeon.DungeonCrystalTypeAddress == null)
            {
                // Probably GT
                return false;
            }

            if (romData[(int)dungeon.DungeonCrystalTypeAddress] == ItemConstants.CrystalTypePendant
                && romData[(int)dungeon.DungeonCrystalAddress] == ItemConstants.CrystalGreenPendant
                && ItemConstants.ImportantItems.Contains(romData[ItemConstants.SahasrahlaItemAddress]))
            {
                return true;
            }

            return false;
        };

        protected Func<Dungeon, RomData, byte[], bool> CheckFatFairyHasItem = (Dungeon dungeon, RomData romData, byte[] items) =>
        {
            if (dungeon.DungeonCrystalAddress == null || dungeon.DungeonCrystalTypeAddress == null)
            {
                // Probably GT
                return false;
            }

            if (romData[(int)dungeon.DungeonCrystalTypeAddress] == ItemConstants.CrystalTypeCrystal
                && (romData[(int)dungeon.DungeonCrystalAddress] == ItemConstants.Crystal5
                    || romData[(int)dungeon.DungeonCrystalAddress] == ItemConstants.Crystal6)
                && (ItemConstants.ImportantItems.Contains(romData[ItemConstants.FatFairyItem1Address])
                    || ItemConstants.ImportantItems.Contains(romData[ItemConstants.FatFairyItem2Address])))
            {
                return true;
            }

            return false;
        };

        protected Func<Dungeon, RomData, byte[], bool> CheckBossDropHasImportantItem = (Dungeon dungeon, RomData romData, byte[] items) =>
        {
            if (dungeon.BossDropItemAddress == null)
            {
                // Probably GT
                return false;
            }

            if (ItemConstants.ImportantItems.Contains(romData[(int)dungeon.BossDropItemAddress]))
            {
                return true;
            }

            return false;
        };

        protected Func<Dungeon, RomData, byte[], bool> CheckGTowerAndPedestalForItems = (Dungeon dungeon, RomData romData, byte[] items) =>
        {
            foreach (var item in items)
            {
                if (scan_gtower(romData, item))
                {
                    return true;
                }
                if (romData[ItemConstants.MasterSwordPedestalAddress] == item)
                {
                    return true;
                }
            }
            return false;
        };

        // TODO: Fix this function
        protected static bool scan_gtower(RomData romData, byte item) //0x08 = ice rod, 0x07 = fire rod
        {
            if (romData[0xEAB8] == item) { return true; }
            if (romData[0xEABB] == item) { return true; }
            if (romData[0xEABE] == item) { return true; }
            if (romData[0xEAC1] == item) { return true; }
            if (romData[0xEAD3] == item) { return true; }
            if (romData[0xEAD6] == item) { return true; }
            if (romData[0xEAD9] == item) { return true; }
            if (romData[0xEADC] == item) { return true; }
            if (romData[0xEAC4] == item) { return true; }
            if (romData[0xEAC7] == item) { return true; }
            if (romData[0xEACA] == item) { return true; }
            if (romData[0xEACD] == item) { return true; }
            if (romData[0xEADF] == item) { return true; }
            if (romData[0xEAE2] == item) { return true; }
            if (romData[0xEAE5] == item) { return true; }
            if (romData[0xEAE8] == item) { return true; }
            if (romData[0xEAEB] == item) { return true; }
            if (romData[0xEAEE] == item) { return true; }
            if (romData[0xEAD0] == item) { return true; }
            if (romData[0xEAFD] == item) { return true; }
            if (romData[0xEB00] == item) { return true; }
            if (romData[0xEB03] == item) { return true; }
            if (romData[0xEB06] == item) { return true; }
            if (romData[0xEAF4] == item) { return true; }
            if (romData[0xEAF7] == item) { return true; }
            if (romData[0xEAF1] == item) { return true; }
            if (romData[0x180161] == item) { return true; }

            return false;
        }

    }
}
