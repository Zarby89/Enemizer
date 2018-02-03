using System;
using System.Text;

namespace EnemizerLibrary
{
    public class ManualBossRandomizer : BossRandomizer
    {
        public ManualBossRandomizer(Random rand, OptionFlags optionFlags, StringBuilder spoilerFile, Graph graph) : base(rand, optionFlags, spoilerFile, graph)
        {
        }

        protected override void GenerateRandomizedBosses()
        {
            foreach (var dungeon in this.DungeonPool)
            {
                switch (dungeon.DungeonType)
                {
                    case DungeonType.EasternPalace:
                        dungeon.SelectedBoss = GetBossFromOptionString(optionFlags.ManualBosses.EasternPalace);
                        break;
                    case DungeonType.DesertPalace:
                        dungeon.SelectedBoss = GetBossFromOptionString(optionFlags.ManualBosses.DesertPalace);
                        break;
                    case DungeonType.TowerOfHera:
                        dungeon.SelectedBoss = GetBossFromOptionString(optionFlags.ManualBosses.TowerOfHera);
                        break;
                    case DungeonType.PalaceOfDarkness:
                        dungeon.SelectedBoss = GetBossFromOptionString(optionFlags.ManualBosses.PalaceOfDarkness);
                        break;
                    case DungeonType.SwampPalace:
                        dungeon.SelectedBoss = GetBossFromOptionString(optionFlags.ManualBosses.SwampPalace);
                        break;
                    case DungeonType.SkullWoods:
                        dungeon.SelectedBoss = GetBossFromOptionString(optionFlags.ManualBosses.SkullWoods);
                        break;
                    case DungeonType.ThievesTown:
                        dungeon.SelectedBoss = GetBossFromOptionString(optionFlags.ManualBosses.ThievesTown);
                        break;
                    case DungeonType.IcePalace:
                        dungeon.SelectedBoss = GetBossFromOptionString(optionFlags.ManualBosses.IcePalace);
                        break;
                    case DungeonType.MiseryMire:
                        dungeon.SelectedBoss = GetBossFromOptionString(optionFlags.ManualBosses.MiseryMire);
                        break;
                    case DungeonType.TurtleRock:
                        dungeon.SelectedBoss = GetBossFromOptionString(optionFlags.ManualBosses.TurtleRock);
                        break;
                    case DungeonType.GanonsTower1:
                        dungeon.SelectedBoss = GetBossFromOptionString(optionFlags.ManualBosses.GanonsTower1);
                        break;
                    case DungeonType.GanonsTower2:
                        dungeon.SelectedBoss = GetBossFromOptionString(optionFlags.ManualBosses.GanonsTower2);
                        break;
                    case DungeonType.GanonsTower3:
                        dungeon.SelectedBoss = GetBossFromOptionString(optionFlags.ManualBosses.GanonsTower3);
                        break;
                }
            }
        }

        Boss GetBossFromOptionString(string boss)
        {
            switch(boss)
            {
                case "Armos":
                    return new ArmosBoss();
                case "Lanmola":
                    return new LanmolaBoss();
                case "Moldorm":
                    return new MoldormBoss();
                case "Helmasaur":
                    return new HelmasaurBoss();
                case "Arrghus":
                    return new ArrghusBoss();
                case "Mothula":
                    return new MothulaBoss();
                case "Blind":
                    return new BlindBoss();
                case "Kholdstare":
                    return new KholdstareBoss();
                case "Vitreous":
                    return new VitreousBoss();
                case "Trinexx":
                    return new TrinexxBoss();
                default:
                    return new EmptyBoss();
            }
        }

    }
}