using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class Bees
    {
        RomData romData;
        OptionFlags optionFlags;
        Random rand;

        public Bees(RomData romData, OptionFlags optionFlags, Random rand)
        {
            this.romData = romData;
            this.optionFlags = optionFlags;
            this.rand = rand;
        }

        public void BeeMeUp()
        {
            int levelRand = 4;

            switch(optionFlags.BeesLevel)
            {
                case BeeLevel.Level1:
                    levelRand = 4;
                    break;
                case BeeLevel.Level2:
                    levelRand = 2;
                    break;
                case BeeLevel.Level3:
                    levelRand = 3;
                    break;
                case BeeLevel.Level4:
                    levelRand = 1;
                    break;
            }

            foreach(var a in ChestAddresses)
            {
                if(UselessItems.Contains(romData[a]))
                {
                    var r = rand.Next(levelRand);

                    if(r == 0)
                    {
                        romData[a] = ItemConstants.Trap1;
                    }
                    else if(optionFlags.BeesLevel == BeeLevel.Level3 && r == 1)
                    {
                        romData[a] = ItemConstants.Bee_NoBottle;
                    }
                }
                else if ((optionFlags.BeesLevel == BeeLevel.Level3 || optionFlags.BeesLevel == BeeLevel.Level4) 
                    && UselessItemsHardMode.Contains(romData[a]))
                {
                    var r = rand.Next(levelRand);

                    if (r == 0)
                    {
                        romData[a] = ItemConstants.Trap1;
                    }
                    else if (optionFlags.BeesLevel == BeeLevel.Level3 && r == 1)
                    {
                        romData[a] = ItemConstants.Bee_NoBottle;
                    }
                }
            }

            UpdateRomHook();
        }

        void UpdateRomHook()
        {
            var spawnBeesAddress = XkasSymbols.Instance.Symbols["Spawn_Bees"];
            var snesAddress = Utilities.PCAddressToSnesByteArray(spawnBeesAddress);

            // z3randomizer:
            // org $308060 ; PC 0x180060
            // ProgrammableItemLogicJump_1:
            // JSL.l $000000 : RTL
            romData[0x180061] = snesAddress[2];
            romData[0x180061 + 1] = snesAddress[1];
            romData[0x180061 + 2] = snesAddress[0];
        }

        readonly int[] ChestAddresses =
        {
            0x180000, 0x180001, 0x180002, 0x180003, 0x180004, 0x180005, 0x180006, 0x180010, 0x180011,
            0x180012, 0x180013, 0x180014, 0x180015, 0x180016, 0x180017, 0x18002A, 0x180140, 0x180141,
            0x180142, 0x180143, 0x180144, 0x180145, 0x180146, 0x180147, 0x180148, 0x180149, 0x18014A,
            0x180152, 0x289B0, 0x2DF45, 0x2EB18, 0x2F1FC, 0x330C7, 0x339CF, 0x33D68, 0x33E7D, 0xE96E,
            0xE971, 0xE974, 0xE977, 0xE97A, 0xE97D, 0xE980, 0xE983, 0xE986, 0xE989, 0xE98C, 0xE98F,
            0xE992, 0xE995, 0xE998, 0xE99B, 0xE99E, 0xE9A1, 0xE9A4, 0xE9AA, 0xE9AD, 0xE9B3, 0xE9B6,
            0xE9B9, 0xE9BC, 0xE9BF, 0xE9C2, 0xE9C5, 0xE9C8, 0xE9CB, 0xE9CE, 0xE9D4, 0xE9DA, 0xE9DD,
            0xE9E0, 0xE9E3, 0xE9E6, 0xE9E9, 0xE9EC, 0xE9EF, 0xE9F2, 0xE9F5, 0xE9F8, 0xE9FB, 0xE9FE,
            0xEA01, 0xEA04, 0xEA07, 0xEA0A, 0xEA0D, 0xEA10, 0xEA13, 0xEA16, 0xEA19, 0xEA1C, 0xEA1F,
            0xEA22, 0xEA25, 0xEA28, 0xEA2B, 0xEA2E, 0xEA31, 0xEA34, 0xEA37, 0xEA3A, 0xEA3D, 0xEA40,
            0xEA43, 0xEA49, 0xEA4C, 0xEA4F, 0xEA52, 0xEA52, 0xEA55, 0xEA58, 0xEA5B, 0xEA5E, 0xEA61,
            0xEA64, 0xEA67, 0xEA6A, 0xEA6D, 0xEA73, 0xEA76, 0xEA79, 0xEA7C, 0xEA7F, 0xEA82, 0xEA85,
            0xEA88, 0xEA8B, 0xEA8E, 0xEA91, 0xEA94, 0xEA97, 0xEA9A, 0xEA9D, 0xEAA0, 0xEAA3, 0xEAA6,
            0xEAA9, 0xEAAC, 0xEAAF, 0xEAB2, 0xEAB5, 0xEAB8, 0xEABB, 0xEABE, 0xEAC1, 0xEAC4, 0xEAC7,
            0xEACA, 0xEACD, 0xEAD0, 0xEAD3, 0xEAD6, 0xEAD9, 0xEADC, 0xEADF, 0xEAE2, 0xEAE5, 0xEAE8,
            0xEAEB, 0xEAEE, 0xEAF1, 0xEAF4, 0xEAF7, 0xEAFD, 0xEB00, 0xEB03, 0xEB06, 0xEB09, 0xEB0C,
            0xEB0F, 0xEB12, 0xEB15, 0xEB18, 0xEB1B, 0xEB1E, 0xEB21, 0xEB24, 0xEB27, 0xEB2A, 0xEB2D,
            0xEB30, 0xEB33, 0xEB36, 0xEB39, 0xEB3C, 0xEB3F, 0xEB42, 0xEB45, 0xEB48, 0xEB4B, 0xEB4E,
            0xEB51, 0xEB54, 0xEB57, 0xEB5A, 0xEB5D, 0xEB60, 0xEB63, 0xEDA8, 0xEE185, 0xEE1C3, 0xF69FA,
        };

        readonly byte[] UselessItems =
        {
            ItemConstants.Arrow_1,
            ItemConstants.Rupee_1,
            ItemConstants.Bombs_3,
            ItemConstants.Rupee_5,
            ItemConstants.Arrow_10,
            ItemConstants.Bomb_10,
            ItemConstants.Rupee_20,
            ItemConstants.Rupee_20_2,
            ItemConstants.Rupee_50,
            ItemConstants.Rupee_100,
            ItemConstants.Rupee_300,
            ItemConstants.Bomb,
            ItemConstants.Compass,
            ItemConstants.Map,
            ItemConstants.MaxBombs_5,
            ItemConstants.MaxBombs_10,
            ItemConstants.MaxArrows_5,
            ItemConstants.MaxArrows_10,
        };

        readonly byte[] UselessItemsHardMode =
        {
            ItemConstants.BugNet,
            ItemConstants.BlueBoomerang,
            ItemConstants.SanctuaryHeart,
            ItemConstants.BossHeart,
            ItemConstants.FireShield,
            ItemConstants.PieceOfHeart,
            ItemConstants.RedBoomerang,
            ItemConstants.QuarterMagic,
            ItemConstants.HalfMagic,
            ItemConstants.Maps,
            ItemConstants.Compasses,
            ItemConstants.ProgressiveShield,
            ItemConstants.ProgressiveArmor
        };
    }

}
