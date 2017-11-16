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
        public int BossSpriteId { get; internal set; }

        string requirements;
        public string Requirements
        {
            get { return requirements; }
            protected set
            {
                requirements = value;
                RequirementList = Requirement.MakeRequirementListFromString(requirements);
            }
        }
        public List<Requirement> RequirementList { get; protected set; }

        protected string BossNode { get; set; }

        public byte[] BossSpriteArray { get; protected set; }

        public Boss(BossType bossType)
        {
            BossType = bossType;
            BossPointer = null;
            BossGraphics = 0;
        }

        public bool CanBeUsed(Dungeon dungeon, Graph graph)
        {
            var res = graph.FindPath("cave-links-house", dungeon.LogicalBossRoomId, true, null, Requirements);


            return res.Success || MeetsRequirements(res.ItemsObtained);
        }

        bool MeetsRequirements(List<Item> items)
        {
            if (RequirementList == null || RequirementList.Count == 0)
            {
                return true;
            }

            var needConsume = new List<ConsumableItem>();

            int swordCount = items.Where(x => x.LogicalId == "Progressive Sword").Count();
            int gloveCount = items.Where(x => x.LogicalId == "Progressive Gloves").Count();

            foreach (var r in RequirementList)
            {
                int count = 0;
                foreach (var i in r.Requirements)
                {
                    if (i is ConsumableItem)
                    {
                        var c = i as ConsumableItem;
                        if (items.Contains(c) && items.Any(x => x == c && ((ConsumableItem)x).Usable))
                        {
                            count++;
                            needConsume.Add(c);
                        }
                    }
                    else if (items.Contains(i))
                    {
                        count++;
                    }
                    else if (i is BottleItem && items.Any(x => x is BottleItem))
                    {
                        count++;
                    }
                    else if (i is ProgressiveItem)
                    {
                        var split = i.LogicalId.Split(' ');
                        if (split.Length > 1)
                        {
                            int level = (int)char.GetNumericValue(split[0][1]);

                            switch (split[1])
                            {
                                case "Sword":
                                    if (swordCount >= level)
                                    {
                                        count++;
                                    }
                                    break;
                                case "Gloves":
                                    if (gloveCount >= level)
                                    {
                                        count++;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            throw new Exception("Boss.MeetsRequirements - Invalid progressive item");
                        }
                    }
                }
                if (count == r.Requirements.Count)
                {
                    return true;
                }
            }
            return false;
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

        public static Boss GetRandomBoss(Random rand, Dungeon dungeon, List<BossType> excludedBossTypes=null, Graph graph=null)
        {
            var bosses = Enum.GetValues(typeof(BossType)).Cast<BossType>()
                .Where(x => x != BossType.NoBoss)
                .Where(x => excludedBossTypes == null || excludedBossTypes.Contains(x) == false)
                .ToList();

            Boss boss = null;

            while (boss == null)
            {
                boss = GetBossFromType(bosses[rand.Next(bosses.Count)]);
                if(graph != null && !boss.CanBeUsed(dungeon, graph))
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
