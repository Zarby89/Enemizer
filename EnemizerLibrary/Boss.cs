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
        public List<Func<Dungeon, bool>> Rules { get; set; } = new List<Func<Dungeon, bool>>();

        public Boss(BossType bossType)
        {
            BossType = bossType;
            FillRules();
        }

        protected void FillRules()
        {

        }

        public bool CheckRules(Dungeon dungeon)
        {
            bool result = false;
            foreach (var rule in Rules)
            {
                result |= rule.Invoke(dungeon);
            }

            return result;
        }
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
            // TODO: check for hookshot

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
            // TODO: check for firerod (bombos?)

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
            // TODO: check for firerod and icerod

        }
    }

    public class EmptyBoss : Boss
    {
        public EmptyBoss() : base(BossType.NoBoss) { }
    }
}
