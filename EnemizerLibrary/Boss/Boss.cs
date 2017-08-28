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
                //if (scan_gtower(item))
                //{
                //    return true;
                //}
                if (romData[ItemConstants.MasterSwordPedestalAddress] == item)
                {
                    return true;
                }
            }
            return false;
        };
    }
}
