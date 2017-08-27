using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public enum BossType
    {
        Kholdstare = 0,
        Moldorm = 1,
        Mothula = 2,
        Vitreous = 3,
        Helmasaur = 4,
        Armos = 5,
        Lanmola = 6,
        Blind = 7,
        Arrghus = 8,
        Trixnexx = 9,
        NoBoss = 255
    }
    public class Boss
    {
        public BossType BossType { get; set; }
        public List<Func<Dungeon, RomData, byte[], bool>> Rules { get; set; } = new List<Func<Dungeon, RomData, byte[], bool>>();

        public Boss(BossType bossType)
        {
            BossType = bossType;
            FillRules();
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

    public class ArmosBoss : Boss
    {
        public ArmosBoss() : base(BossType.Armos) { }
    }

    public class LanmolaBoss : Boss
    {
        public LanmolaBoss() : base(BossType.Lanmola) { }
    }

    public class MoldormBoss : Boss
    {
        public MoldormBoss() : base(BossType.Moldorm) { }
    }

    public class HelmasaurBoss : Boss
    {
        public HelmasaurBoss() : base(BossType.Helmasaur) { }
    }

    public class ArrghusBoss : Boss
    {
        public ArrghusBoss() : base(BossType.Arrghus) { }

        protected new void FillRules()
        {
            Rules.Add(this.CheckGTowerAndPedestalForItems);
            Rules.Add(this.CheckShabadooHasItem);
            Rules.Add(this.CheckFatFairyHasItem);
            Rules.Add(this.CheckBossDropHasImportantItem);
        }

        public override bool CheckRules(Dungeon dungeon, RomData romData)
        {
            return base.CheckRules(dungeon, romData, ItemConstants.Hookshot);
        }
    }

    public class MothulaBoss : Boss
    {
        public MothulaBoss() : base(BossType.Mothula) { }
    }

    public class BlindBoss : Boss
    {
        public BlindBoss() : base(BossType.Blind) { }
    }

    public class KholdstareBoss : Boss
    {
        public KholdstareBoss() : base(BossType.Kholdstare) { }

        protected new void FillRules()
        {
            Rules.Add(this.CheckGTowerAndPedestalForItems);
            Rules.Add(this.CheckShabadooHasItem);
            Rules.Add(this.CheckFatFairyHasItem);
            Rules.Add(this.CheckBossDropHasImportantItem);
        }

        public override bool CheckRules(Dungeon dungeon, RomData romData)
        {
            return base.CheckRules(dungeon, romData, ItemConstants.FireRod);
        }
    }

    public class VitreousBoss : Boss
    {
        public VitreousBoss() : base(BossType.Vitreous) { }
    }

    public class TrinexxBoss : Boss
    {
        public TrinexxBoss() : base(BossType.Trixnexx) { }

        protected new void FillRules()
        {
            Rules.Add(this.CheckGTowerAndPedestalForItems);
            Rules.Add(this.CheckShabadooHasItem);
            Rules.Add(this.CheckFatFairyHasItem);
            Rules.Add(this.CheckBossDropHasImportantItem);
        }

        public override bool CheckRules(Dungeon dungeon, RomData romData)
        {
            return base.CheckRules(dungeon, romData, ItemConstants.FireRod, ItemConstants.IceRod);
        }
    }

    public class EmptyBoss : Boss
    {
        public EmptyBoss() : base(BossType.NoBoss) { }
    }
}
