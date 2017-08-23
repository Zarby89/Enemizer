using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public partial class Randomization
    {
        public void Randomize_Overworld_Sprite(bool absorbable)
        {
            int index = 0;

            ROM_DATA[0x04CF4F] = 0x10; //move bird from tree


            for (int i = 0; i < 208; i++)
            {

                if (overworld_sprites[i] != null)
                {

                    if (overworld_sprites[i].Length != 0) //if there's sprite in that map
                    {
                        while (true)
                        {
                            Console.WriteLine("Overworld??? " + i.ToString());
                            // Select one of the 63 sprites group avoid the ones that are empty because they contain npcs/bosses
                            byte sprite_group = (byte)rand.Next(43);
                            if (random_sprite_group_ow[sprite_group].Length == 0) { continue; } //restart

                            if (i == 0x02) { sprite_group = 7; }
                            if (i == 0x03) { sprite_group = 16; }
                            if (i == 0x0F) { sprite_group = 4; }
                            if (i == 0x14) { sprite_group = 3; }
                            if (i == 0x1B) { sprite_group = 1; }
                            if (i == 0x22) { sprite_group = 6; }
                            if (i == 0x28) { sprite_group = 6; }
                            if (i == 0x30) { sprite_group = 9; }
                            if (i == 0x3A) { sprite_group = 10; }
                            if (i == 0x4F) { sprite_group = 22; }
                            if (i == 0x62) { sprite_group = 21; }
                            if (i == 0x68) { sprite_group = 27; }
                            if (i == 0x69) { sprite_group = 21; }

                            //we finally have a sprite_group that contain good subset for that room
                            List<byte> sprites = new List<byte>(); //create a new list of sprite that can possibly be in the room
                            sprites.Clear();




                            foreach (byte s in subset_gfx_sprites[random_sprite_group_ow[sprite_group][0]]) //add all subset0 sprites of the selected group
                            {
                                sprites.Add(s);
                            }
                            foreach (byte s in subset_gfx_sprites[random_sprite_group_ow[sprite_group][1]]) //add all subset1 sprites of the selected group
                            {
                                sprites.Add(s);
                            }
                            foreach (byte s in subset_gfx_sprites[random_sprite_group_ow[sprite_group][2]]) //add all subset2 sprites of the selected group
                            {
                                sprites.Add(s);
                            }
                            foreach (byte s in subset_gfx_sprites[random_sprite_group_ow[sprite_group][3]]) //add all subset3 sprites of the selected group
                            {
                                sprites.Add(s);
                            }

                            if (sprite_group == 27)
                            {
                                sprites.Remove(0x8E);
                            }



                            //our sprites list should contain at least 1 sprite at this point else then restart
                            if (sprites.Count <= 0) { continue; }

                            if (absorbable == true)
                            {
                                for (int j = 0; j < 4; j++)
                                {
                                    sprites.Add(absorbable_sprites[rand.Next(absorbable_sprites.Length)]); //add all the absorbable sprites
                                }
                            }

                            //for each sprites address in the room we are currently modifying
                            for (int j = 0; j < overworld_sprites[i].Length; j++)
                            {
                                byte selected_sprite = sprites[rand.Next(sprites.Count)];
                                //NEED TO ADD CONDITION FOR WATER/REMOVED SPRITES !
                                ROM_DATA[overworld_sprites[i][j]] = selected_sprite;
                                //ROM_DATA[map[i] - 1] = (byte)(ROM_DATA[room_sprites[room][i] - 1] & 0x1F);//change overlord into normal sprite

                            }

                            if (i < 0x40)
                            {
                                //0x07AC1
                                ROM_DATA[0x07A81 + i] = sprite_group; //set the room header sprite gfx

                            }
                            else if (i >= 0x40 && i <= 0x80)
                            {
                                ROM_DATA[0x07B01 + (i - 0x40)] = sprite_group; //set the room header sprite gfx

                            }
                            else if (i > 0x80)
                            {
                                ROM_DATA[0x07AC1 + (i - 0x90)] = sprite_group;//set the room header sprite gfx
                            }
                            break; //if everything is fine in that room then go to the next one
                        }
                    }
                }
            }
        }

    }
}
