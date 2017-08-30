using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class DungeonSpriteRandomizer
    {
        byte[][] random_sprite_group = new byte[60][];

        Random rand;
        RomData romData;

        public DungeonSpriteRandomizer(RomData romData, Random rand)
        {
            this.romData = romData;
            this.rand = rand;
        }
        public void RandomizeDungeonSprites(bool absorbable, byte[][] subset_gfx_sprites)
        {
            this.create_sprite_group();
            this.patch_sprite_group();

            this.Randomize_Dungeons_Sprite(absorbable, subset_gfx_sprites);
        }

        public void Randomize_Dungeons_Sprite(bool absorbable, byte[][] subset_gfx_sprites)
        {
            var roomSprites = new RoomSpriteCollection();

            foreach (int room in roomSprites.randomized_rooms)
            {

                while (true)
                {

                    // Select one of the 60 sprites group avoid the ones that are empty because they contain npcs/bosses
                    byte sprite_group = (byte)rand.Next(60);
                    if (random_sprite_group[sprite_group].Length == 0) { continue; } //restart

                    //The group we selected is not empty - check if the room is part of a special room

                    /*NOTE Some room have multiple required sprites like shadows, crystal switches might need to force a
                     * specific group for these rooms*/

                    Console.WriteLine("Start Generation " + room.ToString());
                    //tongue switch/crystal subset3 on 83
                    //switch/crystal subset3 on  82
                    if (RoomIdConstants.WallMasterRoom.Contains(room)) //if the current room is switch room
                    {
                        sprite_group = 39;
                        Console.WriteLine("Required Wall Master");
                    }

                    if (RoomIdConstants.canonRoom.Contains(room))//if the current room is an Moving wall canon room
                    {
                        if (random_sprite_group[sprite_group][0] != 47) { continue; }
                        Console.WriteLine("Required Canon1");
                    }

                    if (RoomIdConstants.ShadowRoom.Contains(room)) //if the current room is an Shadow room
                    {
                        if (random_sprite_group[sprite_group][1] != 32) { continue; }
                        Console.WriteLine("Shadow");
                    }

                    if (RoomIdConstants.IcemanRoom.Contains(room))
                    {
                        if (random_sprite_group[sprite_group][2] != 38) { continue; }
                        Console.WriteLine("Required Iceman");
                    }

                    if (RoomIdConstants.WaterRoom.Contains(room))
                    {
                        if (random_sprite_group[sprite_group][2] != 34) { continue; }
                        Console.WriteLine("Required Water");
                    }

                    if (RoomIdConstants.canonRoom2.Contains(room)) //if the current room is a canon room
                    {
                        if (random_sprite_group[sprite_group][2] != 46) { continue; }
                        Console.WriteLine("Required Canon2");
                    }

                    if (RoomIdConstants.TonguesRoom.Contains(room)) //if the current room is tongue room
                    {
                        if (random_sprite_group[sprite_group][3] != 83) { continue; }
                        Console.WriteLine("Required Tongues");
                    }

                    if (RoomIdConstants.SwitchesRoom.Contains(room)) //if the current room is switch room
                    {
                        if (random_sprite_group[sprite_group][3] != 82) { continue; }
                        Console.WriteLine("Required Switches");
                    }

                    if (RoomIdConstants.bumperandcrystalRoom.Contains(room)) //if the current room is bumper/crystal/laser eye room
                    {
                        if (random_sprite_group[sprite_group][3] == 82 || random_sprite_group[sprite_group][3] == 83)
                        {

                        }
                        else
                        { continue; }
                        Console.WriteLine("Required BumperCrystalEyes");
                    }

                    if (room == RoomIdConstants.R85_CastleSecretEntrance_UncleDeathRoom) //uncle
                    {
                        sprite_group = 13; //force sprite_group to be uncle 13
                    }

                    if (room == RoomIdConstants.R127_IcePalace_BigSpikeTrapsRoom)
                    {
                        if (random_sprite_group[sprite_group][0] != 31)
                        {
                            continue;
                        }
                    }


                    if (roomSprites.RoomSprites[room].Length != 0)
                    {
                        //we finally have a sprite_group that contain good subset for that room
                        List<byte> sprites = new List<byte>(); //create a new list of sprite that can possibly be in the room


                        bool need_killable_sprite = false;
                        //check all the sprites addresses of that room we are in check if that room is a "shutter door" room
                        foreach (int shutterRoom in RoomIdConstants.NeedKillable_doors)
                        {
                            if (shutterRoom == room) //if we are in a shutterdoor room then
                            {
                                need_killable_sprite = true;
                            }
                        }

                        foreach (byte s in subset_gfx_sprites[random_sprite_group[sprite_group][0]]) //add all subset0 sprites of the selected group
                        {
                            sprites.Add(s);
                        }
                        foreach (byte s in subset_gfx_sprites[random_sprite_group[sprite_group][1]]) //add all subset1 sprites of the selected group
                        {
                            sprites.Add(s);
                        }
                        foreach (byte s in subset_gfx_sprites[random_sprite_group[sprite_group][2]]) //add all subset2 sprites of the selected group
                        {
                            sprites.Add(s);
                        }
                        foreach (byte s in subset_gfx_sprites[random_sprite_group[sprite_group][3]]) //add all subset3 sprites of the selected group
                        {
                            sprites.Add(s);
                        }
                        if (need_killable_sprite)
                        {
                            sprites = remove_unkillable_sprite(room, sprites);
                        }

                        //our sprites list should contain at least 1 sprite at this point else then restart
                        if (sprites.Count <= 0) { continue; }
                        int real_sprites = sprites.Count;
                        if (absorbable == true)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                //pick 3 sprite
                                sprites.Add(SpriteConstants.absorbable_sprites[rand.Next(SpriteConstants.absorbable_sprites.Length)]); //add all the absorbable sprites
                            }
                        }
                        int c = sprites.Count;
                        //LAG REDUCTION CODE !!!
                        if (room == RoomIdConstants.R203_ThievesTown_NorthWestEntranceRoom) //add same amount of green rupee in the pool as the number of sprites
                        {
                            for (int i = 0; i < c; i++)
                            {
                                sprites.Add(0xD9);
                            }
                        }
                        if (room == RoomIdConstants.R204_ThievesTown_NorthEastEntranceRoom) //add same amount of green rupee in the pool as the number of sprites
                        {
                            for (int i = 0; i < c; i++)
                            {
                                sprites.Add(0xD9);
                            }
                        }
                        if (room == RoomIdConstants.R220_ThievesTown_SouthEastEntranceRoom) //add same amount of green rupee in the pool as the number of sprites
                        {
                            for (int i = 0; i < c; i++)
                            {
                                sprites.Add(0xD9);
                            }
                        }

                        //for each sprites address in the room we are currently modifying
                        for (int i = 0; i < roomSprites.RoomSprites[room].Length; i++)
                        {
                            byte selected_sprite = sprites[rand.Next(real_sprites)];
                            //Select a new sprite from the sprites list we will put at that address
                            if (absorbable == true)
                            {
                                selected_sprite = sprites[rand.Next(real_sprites + (rand.Next(3)))];
                            }


                            if (RoomIdConstants.noStatueRoom.Contains(room))
                            {
                                if (selected_sprite == 0x1C)
                                {
                                    continue;
                                }
                            }

                            if (room == RoomIdConstants.R63_IcePalace_MapChestRoom | room == RoomIdConstants.R206_IcePalace_HoletoKholdstareRoom)
                            {
                                if (selected_sprite == 0x86)
                                {
                                    continue;
                                }
                            }

                            if (room == RoomIdConstants.R291_MiniMoldormCave)
                            {
                                if (selected_sprite == 0xE4)
                                {
                                    continue;
                                }
                            }


                            if (roomSprites.RoomSprites[room][i] == 0x04DE29)
                            {
                                if (selected_sprite == 0x7D | selected_sprite == 0x8A | selected_sprite == 0x61 |
                                     selected_sprite == 0x15)
                                {
                                    continue;
                                }
                            }


                            for (int j = 0; j < SpriteConstants.key_sprite.Length; j++)
                            {
                                //Check if the sprite address we are modifying is a key drop sprite then it need to be a killable sprite
                                if (roomSprites.RoomSprites[room][i] == SpriteConstants.key_sprite[j])
                                {
                                    byte protection_try = 0;
                                    while (true)
                                    {
                                        protection_try++;
                                        selected_sprite = sprites[rand.Next(sprites.Count)]; //generate a new sprite to get a killable sprite
                                        if (SpriteConstants.NonKillable.Contains(selected_sprite)) // if the selected sprite we have is invincible then restart
                                        {
                                            if (room == RoomIdConstants.R107_GanonsTower_MimicsRooms
                                                || room == RoomIdConstants.R109_GanonsTower_Gauntlet4_5
                                                || room == RoomIdConstants.R93_GanonsTower_Gauntlet1_2_3
                                                || room == RoomIdConstants.R27_PalaceofDarkness_Mimics_MovingWallRoom
                                                || room == RoomIdConstants.R11_PalaceofDarkness_TurtleRoom
                                                || room == RoomIdConstants.R123_GanonsTower
                                                || room == RoomIdConstants.R125_GanonsTower_Winder_WarpMazeRoom)
                                            {
                                                if (SpriteConstants.bowSprites.Contains(selected_sprite))
                                                {
                                                    break;
                                                }
                                                if (SpriteConstants.hammerSprites.Contains(selected_sprite))
                                                {
                                                    break;
                                                }
                                            }
                                            if (room == RoomIdConstants.R75_PalaceofDarkness_Warps_SouthMimicsRoom
                                                || room == RoomIdConstants.R216_EasternPalace_PreArmosKnightsRoom
                                                || room == RoomIdConstants.R217_EasternPalace_CanonballRoom
                                                || room == RoomIdConstants.R218_EasternPalace)
                                            {
                                                if (SpriteConstants.bowSprites.Contains(selected_sprite))
                                                {
                                                    break;
                                                }
                                            }
                                            if (protection_try >= 200)
                                            {
                                                break;
                                            }
                                            continue; //add a protection timer if after 200 try then it might be possible the selected group do not have killable sprite then restart from the begining
                                        }
                                        break;
                                    }
                                    if (protection_try >= 200) { break; }//reset from the start

                                }
                            }

                            if (room == RoomIdConstants.R151_MiseryMire_TorchPuzzle_MovingWallRoom)
                            {
                                romData[roomSprites.RoomSprites[room][i] - 2] = 0x15;
                                romData[roomSprites.RoomSprites[room][i] - 1] = 0x07;
                            }

                            //Modify the sprite in the ROM / also set all overlord sprites on normal sprites to prevent any crashes
                            romData[roomSprites.RoomSprites[room][i]] = selected_sprite;
                            romData[roomSprites.RoomSprites[room][i] - 1] = (byte)(romData[roomSprites.RoomSprites[room][i] - 1] & 0x1F);//change overlord into normal sprite
                                                                                                                                         //extra_roll = 0;
                        }
                    }

                    romData[0x120090 + ((room * 14) + 3)] = sprite_group; //set the room header sprite gfx

                    break; //if everything is fine in that room then go to the next one
                }

            }
            //remove key in skull wood to prevent a softlock
            romData[0x04DD74] = 0x16;
            romData[0x04DD75] = 0x05;
            romData[0x04DD76] = 0xE4;

            //remove all sprite in the room before boss room in mire can cause problem with different boss in the room
            //NOT NEEDED ANYMORE
            //ROM_DATA[0x04E591] = 0xFF;
        }

        public List<byte> remove_unkillable_sprite(int room, List<byte> sprites)
        {
            bool no_change = false;
            for (int i = 0; i < sprites.Count; i++)
            {
                no_change = false;
                if (room == RoomIdConstants.R107_GanonsTower_MimicsRooms
                    || room == RoomIdConstants.R109_GanonsTower_Gauntlet4_5
                    || room == RoomIdConstants.R93_GanonsTower_Gauntlet1_2_3
                    || room == RoomIdConstants.R27_PalaceofDarkness_Mimics_MovingWallRoom
                    || room == RoomIdConstants.R11_PalaceofDarkness_TurtleRoom
                    || room == RoomIdConstants.R123_GanonsTower
                    || room == RoomIdConstants.R125_GanonsTower_Winder_WarpMazeRoom)
                {

                    if (SpriteConstants.bowSprites.Contains(sprites[i]))
                    {
                        no_change = true;
                    }
                    if (SpriteConstants.hammerSprites.Contains(sprites[i]))
                    {
                        no_change = true;
                    }
                }
                if (room == RoomIdConstants.R75_PalaceofDarkness_Warps_SouthMimicsRoom
                    || room == RoomIdConstants.R216_EasternPalace_PreArmosKnightsRoom
                    || room == RoomIdConstants.R217_EasternPalace_CanonballRoom
                    || room == RoomIdConstants.R218_EasternPalace)
                {
                    if (SpriteConstants.bowSprites.Contains(sprites[i]))
                    {
                        no_change = true;
                    }
                }
                if (no_change == false)
                {
                    if (SpriteConstants.NonKillable_shutter.Contains(sprites[i]))
                    {
                        sprites.RemoveAt(i);
                        continue;
                    }
                }

            }
            return sprites;

        }


        public void create_sprite_group()
        {
            // id + 0x40 (64) = HM block id
            //Creations of the guards group :
            random_sprite_group[0] = new byte[] { }; //Do not randomize that group (Ending thing?)
            random_sprite_group[1] = new byte[] { 70, get_guard_subset_1(), 19, SpriteConstants.sprite_subset_3[rand.Next(SpriteConstants.sprite_subset_3.Length)] };
            random_sprite_group[2] = new byte[] { 70, get_guard_subset_1(), 19, 83 }; //tongue switch group
            random_sprite_group[3] = new byte[] { 72, get_guard_subset_1(), 19, SpriteConstants.sprite_subset_3[rand.Next(SpriteConstants.sprite_subset_3.Length)] };
            random_sprite_group[4] = new byte[] { 72, get_guard_subset_1(), 19, 82 }; //switch group
            random_sprite_group[5] = new byte[] { }; //Do not randomize that group (Npcs, Items, some others thing)
            random_sprite_group[6] = new byte[] { }; //Do not randomize that group (Sanctuary Mantle, Priest)
            random_sprite_group[7] = new byte[] { };//Do not randomize that group (Npcs, Arghus)
            random_sprite_group[8] = fully_randomize_that_group(); //Force Group 8 for Iceman subset2 on 38
            random_sprite_group[8][2] = 38;
            random_sprite_group[9] = new byte[] { };//Do not randomize that group (Armos Knight)
            random_sprite_group[10] = fully_randomize_that_group(); //Force Group 10 for Watersprites subset2 on 34
            random_sprite_group[10][2] = 34;
            random_sprite_group[11] = new byte[] { };//Do not randomize that group (Lanmolas)
            random_sprite_group[12] = new byte[] { }; //Do not randomize that group (Moldorm)
            random_sprite_group[13] = fully_randomize_that_group(); //(Link's House)/Sewer restore uncle (81)
            random_sprite_group[13][0] = 81;
            random_sprite_group[14] = new byte[] { }; //Do not randomize that group (Npcs)
            random_sprite_group[15] = new byte[] { };//Do not randomize that group (Npcs)
            random_sprite_group[16] = new byte[] { }; //Do not randomize that group (Minigame npcs, witch)
            random_sprite_group[17] = fully_randomize_that_group();//Force Group 17 for Shadow(Zoro) Subset 1 on 32
            random_sprite_group[17][1] = 32;
            random_sprite_group[18] = new byte[] { };//Do not randomize that group (Vitreous?,Agahnim)
            random_sprite_group[19] = fully_randomize_that_group();//Force group 19 for Wallmaster Subset2 on 35
            random_sprite_group[19][2] = 35;
            random_sprite_group[20] = new byte[] { }; //Do not randomize that group (Bosses)
            random_sprite_group[21] = new byte[] { }; //Do not randomize that group (Bosses)
            random_sprite_group[22] = new byte[] { };//Do not randomize that group (Bosses)
            random_sprite_group[23] = new byte[] { }; //Do not randomize that group (Bosses)
            random_sprite_group[24] = new byte[] { }; //Do not randomize that group (Bosses)
            random_sprite_group[25] = fully_randomize_that_group();
            random_sprite_group[26] = new byte[] { };//Do not randomize that group (Bosses)
            random_sprite_group[27] = fully_randomize_that_group();//Force group 27 for movingwallcanon subset0 on 47
            random_sprite_group[27][0] = 47;
            random_sprite_group[28] = fully_randomize_that_group();//Force group 27 for canon rooms subset2 on 46
            random_sprite_group[28][2] = 46;
            random_sprite_group[29] = fully_randomize_that_group();
            random_sprite_group[30] = fully_randomize_that_group();
            random_sprite_group[31] = fully_randomize_that_group();
            random_sprite_group[32] = new byte[] { }; //Do not randomize that group (Bosses)
            random_sprite_group[33] = fully_randomize_that_group();
            random_sprite_group[34] = new byte[] { }; //Do not randomize that group (Ganon)
            random_sprite_group[35] = new byte[] { }; //Do not randomize that group (Lanmolas)
            random_sprite_group[36] = fully_randomize_that_group();
            random_sprite_group[37] = fully_randomize_that_group();
            random_sprite_group[38] = fully_randomize_that_group();
            random_sprite_group[39] = fully_randomize_that_group();
            random_sprite_group[39][2] = 35;
            random_sprite_group[39][3] = 82;




            //room 88 require bumper, switch
            //room 104 require bumper, wall master
            random_sprite_group[40] = new byte[] { }; //Do not randomize that group (Npcs)
            for (int i = 41; i < 60; i++)
            {
                random_sprite_group[i] = fully_randomize_that_group(); //group from 105 to 124 are empty
            }

            random_sprite_group[29][0] = 47;
            random_sprite_group[29][2] = 46;
            random_sprite_group[30][0] = 14;
            random_sprite_group[31][1] = 32;
            random_sprite_group[31][2] = 28;
            random_sprite_group[38][2] = 38;
            random_sprite_group[38][3] = 82;
            random_sprite_group[33][2] = 34;
            random_sprite_group[36][3] = 83;
            random_sprite_group[37][3] = 82;
            random_sprite_group[41][2] = 35;

            for (int i = 0; i < SpriteConstants.statis_sprites.Length; i++)
            {
                romData[0x6B44C + SpriteConstants.statis_sprites[i]] = (byte)(romData[0x6B44C + SpriteConstants.statis_sprites[i]] | 0x40);


            }



        }

        public void patch_sprite_group()
        {
            for (int i = 0; i < 60; i++)
            {
                if (random_sprite_group[i].Length != 0)
                {
                    romData[0x05C97 + (i * 4)] = random_sprite_group[i][0];
                    romData[0x05C97 + (i * 4) + 1] = random_sprite_group[i][1];
                    romData[0x05C97 + (i * 4) + 2] = random_sprite_group[i][2];
                    romData[0x05C97 + (i * 4) + 3] = random_sprite_group[i][3];
                }
            }
        }

        public byte get_guard_subset_1()
        {
            int i = rand.Next(2);
            if (i == 0) { i = 73; }
            if (i == 1) { i = 13; }
            if (i == 2) { i = 13; }
            return (byte)i;
        }

        public byte[] fully_randomize_that_group()
        {
            return new byte[]
            {
                SpriteConstants.sprite_subset_0[rand.Next(SpriteConstants.sprite_subset_0.Length)],
                SpriteConstants.sprite_subset_1[rand.Next(SpriteConstants.sprite_subset_1.Length)],
                SpriteConstants.sprite_subset_2[rand.Next(SpriteConstants.sprite_subset_2.Length)],
                SpriteConstants.sprite_subset_3[rand.Next(SpriteConstants.sprite_subset_3.Length)]
            };
        }

    }
}
