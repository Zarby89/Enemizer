using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public abstract class Boss
    {
        public BossType BossType { get; set; }
        public List<Func<Dungeon, RomData, byte[], bool>> Rules { get; set; } = new List<Func<Dungeon, RomData, byte[], bool>>();
        public byte[] BossPointer { get; internal set; }
        public byte BossGraphics { get; internal set; }

        public string Requirements { get; protected set; }
        protected string BossNode { get; set; }

        public Boss(BossType bossType)
        {
            BossType = bossType;
            BossPointer = null;
            BossGraphics = 0;
        }

        public bool CanBeUsed(Graph graph)
        {
            var res = graph.FindPath("cave-links-house", BossNode, true, null, Requirements);
            return res.Success;
        }

        public static Boss GetBossFromType(BossType boss)
        {
            switch (boss)
            {
                case BossType.Armos:
                    return new ArmosBoss();
                case BossType.Arrghus:
                    return new ArrghusBoss();
                case BossType.Blind:
                    return new BlindBoss();
                case BossType.Helmasaur:
                    return new HelmasaurBoss();
                case BossType.Kholdstare:
                    return new KholdstareBoss();
                case BossType.Lanmola:
                    return new LanmolaBoss();
                case BossType.Moldorm:
                    return new MoldormBoss();
                case BossType.Mothula:
                    return new MothulaBoss();
                case BossType.Trinexx:
                    return new TrinexxBoss();
                case BossType.Vitreous:
                    return new VitreousBoss();
                default:
                    throw new Exception("Unknown Boss Type Selected");
            }
        }

        public static Boss GetRandomBoss(Random rand, List<BossType> excludedBossTypes=null, Graph graph=null)
        {
            var bosses = Enum.GetValues(typeof(BossType)).Cast<BossType>()
                .Where(x => x != BossType.NoBoss)
                .Where(x => excludedBossTypes == null || excludedBossTypes.Contains(x) == false)
                .ToList();

            Boss boss = null;

            while (boss == null)
            {
                boss = GetBossFromType(bosses[rand.Next(bosses.Count)]);
                if(graph != null && !boss.CanBeUsed(graph))
                {
                    boss = null;
                }
            }
            return boss;
        }

        public static Boss GetRandomKillableBoss(Random rand)
        {
            // exclude bosses that require special weapons
            var bosses = Enum.GetValues(typeof(BossType)).Cast<BossType>()
                                .Where(x => x != BossType.NoBoss)
                                .Where(x => x != BossType.Trinexx
                                         && x != BossType.Kholdstare
                                         && x != BossType.Arrghus
                                         )
                                .ToList();
            var boss = bosses[rand.Next(bosses.Count)];

            return GetBossFromType(boss);
        }
    }
}
