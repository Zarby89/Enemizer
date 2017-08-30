using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public partial class Randomization
    {
        public void Randomize_Dungeons_Sprite(bool absorbable)
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
                    if (WallMasterRoom.Contains(room)) //if the current room is switch room
                    {
                        sprite_group = 39;
                        Console.WriteLine("Required Wall Master");
                    }

                    if (canonRoom.Contains(room))//if the current room is an Moving wall canon room
                    {
                        if (random_sprite_group[sprite_group][0] != 47) { continue; }
                        Console.WriteLine("Required Canon1");
                    }

                    if (ShadowRoom.Contains(room)) //if the current room is an Shadow room
                    {
                        if (random_sprite_group[sprite_group][1] != 32) { continue; }
                        Console.WriteLine("Shadow");
                    }

                    if (IcemanRoom.Contains(room))
                    {
                        if (random_sprite_group[sprite_group][2] != 38) { continue; }
                        Console.WriteLine("Required Iceman");
                    }

                    if (WaterRoom.Contains(room))
                    {
                        if (random_sprite_group[sprite_group][2] != 34) { continue; }
                        Console.WriteLine("Required Water");
                    }

                    if (canonRoom2.Contains(room)) //if the current room is a canon room
                    {
                        if (random_sprite_group[sprite_group][2] != 46) { continue; }
                        Console.WriteLine("Required Canon2");
                    }

                    if (TonguesRoom.Contains(room)) //if the current room is tongue room
                    {
                        if (random_sprite_group[sprite_group][3] != 83) { continue; }
                        Console.WriteLine("Required Tongues");
                    }

                    if (SwitchesRoom.Contains(room)) //if the current room is switch room
                    {
                        if (random_sprite_group[sprite_group][3] != 82) { continue; }
                        Console.WriteLine("Required Switches");
                    }

                    if (bumperandcrystalRoom.Contains(room)) //if the current room is bumper/crystal/laser eye room
                    {
                        if (random_sprite_group[sprite_group][3] == 82 || random_sprite_group[sprite_group][3] == 83)
                        {

                        }
                        else
                        { continue; }
                        Console.WriteLine("Required BumperCrystalEyes");
                    }

                    if (room == 85) //uncle
                    {
                        sprite_group = 13; //force sprite_group to be uncle 13
                    }

                    if (room == 127)
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
                        foreach (int shutterRoom in NeedKillable_doors)
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
                                sprites.Add(absorbable_sprites[rand.Next(absorbable_sprites.Length)]); //add all the absorbable sprites
                            }
                        }
                        int c = sprites.Count;
                        //LAG REDUCTION CODE !!!
                        if (room == 203) //add same amount of green rupee in the pool as the number of sprites
                        {
                            for(int i = 0;i < c;i++)
                            {
                                sprites.Add(0xD9);
                            }
                        }
                        if (room == 204) //add same amount of green rupee in the pool as the number of sprites
                        {
                            for (int i = 0; i < c; i++)
                            {
                                sprites.Add(0xD9);
                            }
                        }
                        if (room == 220) //add same amount of green rupee in the pool as the number of sprites
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


                            if (noStatueRoom.Contains(room))
                            {
                                if (selected_sprite == 0x1C)
                                {
                                    continue;
                                }
                            }

                            if (room == 63 | room == 206)
                            {
                                if (selected_sprite == 0x86)
                                {
                                    continue;
                                }
                            }

                            if (room == 291)
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


                            for (int j = 0; j < key_sprite.Length; j++)
                            {
                                //Check if the sprite address we are modifying is a key drop sprite then it need to be a killable sprite
                                if (roomSprites.RoomSprites[room][i] == key_sprite[j])
                                {
                                    byte protection_try = 0;
                                    while (true)
                                    {
                                        protection_try++;
                                        selected_sprite = sprites[rand.Next(sprites.Count)]; //generate a new sprite to get a killable sprite
                                        if (NonKillable.Contains(selected_sprite)) // if the selected sprite we have is invincible then restart
                                        {
                                            if (room == 107 || room == 109 || room == 93 || room == 27 || room == 11 || room == 123 || room == 125)
                                            {
                                                if (bowSprites.Contains(selected_sprite))
                                                {
                                                    break;
                                                }
                                                if (hammerSprites.Contains(selected_sprite))
                                                {
                                                    break;
                                                }
                                            }
                                            if (room == 75 || room == 216 || room == 217 || room == 218)
                                            {
                                                if (bowSprites.Contains(selected_sprite))
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

                            if (room == 151)
                            {
                                ROM_DATA[roomSprites.RoomSprites[room][i] - 2] = 0x15;
                                ROM_DATA[roomSprites.RoomSprites[room][i] - 1] = 0x07;
                            }

                            //Modify the sprite in the ROM / also set all overlord sprites on normal sprites to prevent any crashes
                            ROM_DATA[roomSprites.RoomSprites[room][i]] = selected_sprite;
                            ROM_DATA[roomSprites.RoomSprites[room][i] - 1] = (byte)(ROM_DATA[roomSprites.RoomSprites[room][i] - 1] & 0x1F);//change overlord into normal sprite
                                                                                                                     //extra_roll = 0;
                        }
                    }
                    
                    ROM_DATA[0x120090 + ((room * 14) + 3)] = sprite_group; //set the room header sprite gfx

                    break; //if everything is fine in that room then go to the next one
                }
                
            }
            //remove key in skull wood to prevent a softlock
            ROM_DATA[0x04DD74] = 0x16;
            ROM_DATA[0x04DD75] = 0x05;
            ROM_DATA[0x04DD76] = 0xE4;

            //remove all sprite in the room before boss room in mire can cause problem with different boss in the room
            //NOT NEEDED ANYMORE
            //ROM_DATA[0x04E591] = 0xFF;
        }

        public List<byte> remove_unkillable_sprite(int room,List<byte> sprites)
        {
            bool no_change = false;
            for (int i = 0; i < sprites.Count; i++)
            {
                no_change = false;
                if (room == 107 || room == 109 || room == 93 || room == 27 || room == 11 || room == 123 || room == 125)
                {

                    if (bowSprites.Contains(sprites[i]))
                    {
                        no_change = true;
                    }
                    if (hammerSprites.Contains(sprites[i]))
                    {
                        no_change = true;
                    }
                }
                if (room == 75 || room == 216 || room == 217 || room == 218)
                {
                    if (bowSprites.Contains(sprites[i]))
                    {
                        no_change = true;
                    }
                }
                if (no_change == false)
                {
                    if (NonKillable_shutter.Contains(sprites[i]))
                    {
                        sprites.RemoveAt(i);
                        continue;
                    }
                }
                
            }
            return sprites;

        }

    }
}
