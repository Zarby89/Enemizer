using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Enemizer
{
    public class Sprite
    {
        Color[] palette = new Color[17];
        Color[] palette2 = new Color[17];
        Color[] palette3 = new Color[17];

        void load_palette()
        {
            for (int i = 0; i < 15; i++)
            {
                palette[i + 1] = getColor((short)((data[0x7000 + (i * 2) + 1] << 8) + (data[0x7000 + (i * 2)])));
            }
            for (int i = 0; i < 15; i++)
            {
                palette2[i + 1] = getColor((short)((data[0x7000 + 30 + (i * 2) + 1] << 8) + (data[0x7000 + 30 + (i * 2)])));
            }
            for (int i = 0; i < 15; i++)
            {
                palette3[i + 1] = getColor((short)((data[0x7000 + 60 + (i * 2) + 1] << 8) + (data[0x7000 + 60 + (i * 2)])));
            }
        }

        Color getColor(short c)
        {
            return Color.FromArgb(((c & 0x1F) * 8), ((c & 0x3E0) >> 5) * 8, ((c & 0x7C00) >> 10) * 8);
        }


        public Image refreshEverything(Color backgroundColor, Image picImage, byte[] spriteData)
        {
            data = spriteData;
            load_palette();
            //load4bpp(0);
            return updateGraphic(0, backgroundColor, picImage);
        }

        byte[,] imgdata = new byte[128, 32];
        byte[] data;
        int[] positions = new int[] { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };
        int hexOffset = 0x0;
        void load4bpp(int pos = 0)
        {

            for (int j = 0; j < 4; j++) //4 par y
            {
                for (int i = 0; i < 16; i++)
                {
                    int offset = (hexOffset + (pos * 0x800)) + ((j * 32) * 16) + (i * 32);
                    for (int x = 0; x < 8; x++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            byte tmpbyte = 0;

                            if ((data[offset + (x * 2)] & positions[y]) == positions[y])
                            {
                                tmpbyte += 1;
                            }
                            if ((data[offset + (x * 2) + 1] & positions[y]) == positions[y])
                            {
                                tmpbyte += 2;
                            }

                            if ((data[offset + 16 + (x * 2)] & positions[y]) == positions[y])
                            {
                                tmpbyte += 4;
                            }
                            if ((data[offset + 16 + (x * 2) + 1] & positions[y]) == positions[y])
                            {
                                tmpbyte += 8;
                            }

                            imgdata[y + (i * 8), x + (j * 8)] = tmpbyte;

                        }
                    }
                    // pos++;
                }
            }

        }

        byte[] load8x8(int pos = 0)
        {
            byte[] temp_array = new byte[64];
            int offset = ((pos)); //32 bytes to read per 8x8 tiles, will return an array of 64bytes
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    byte tmpbyte = 0;
                    //There's 4 bit per pixel, 2 at the start, 2 at the middle, for every pixels
                    //so we read all of them in order up to 32 byte
                    if ((data[offset + (x * 2)] & positions[y]) == positions[y]) { tmpbyte += 1; }
                    if ((data[offset + (x * 2) + 1] & positions[y]) == positions[y]) { tmpbyte += 2; }
                    if ((data[offset + 16 + (x * 2)] & positions[y]) == positions[y]) { tmpbyte += 4; }
                    if ((data[offset + 16 + (x * 2) + 1] & positions[y]) == positions[y]) { tmpbyte += 8; }
                    temp_array[y + (x * 8)] = tmpbyte;
                }
            }

            return temp_array;
        }

        byte[,] load16x16(int pos = 0) //pos 0x40 = head facing down, pos 0x4C0 = body facing down
        {
            byte[,] temp_array = new byte[16, 16];
            byte[] topLeft = load8x8(pos);
            byte[] topRight = load8x8(pos + 0x20);
            byte[] bottomLeft = load8x8(pos + 0x200);
            byte[] bottomRight = load8x8(pos + 0x200 + 0x20);

            //copy all the bytes at the correct position in the 2d array
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    temp_array[x, y] = topLeft[x + (y * 8)];
                    temp_array[x + 8, y] = topRight[x + (y * 8)];
                    temp_array[x, y + 8] = bottomLeft[x + (y * 8)];
                    temp_array[x + 8, y + 8] = bottomRight[x + (y * 8)];
                }
            }
            return temp_array;
        }

        Bitmap loadedblocks = new Bitmap(128, 32);
        Image updateGraphic(int pos, Color backgroundColor, Image picImage)
        {
            picImage = new Bitmap(128, 48);
            loadedblocks = new Bitmap(128, 32);
            Graphics g = Graphics.FromImage(picImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            /* for (int x = 48; x < 64; x++)
             {
                 for (int y = 16; y < 32; y++)
                 {
                     //Create body with palette1
                     loadedblocks.SetPixel(x - 48, y - 8, palette[imgdata[x, y]]);

                     loadedblocks.SetPixel(x - 32, y-8, palette2[imgdata[x, y]]);

                     loadedblocks.SetPixel(x - 16, y-8, palette3[imgdata[x, y]]);

                 }
             }


             for (int x = 16; x < 32; x++)
             {
                 for (int y = 0; y < 16; y++)
                 {
                     //Create Head with palette1
                     if (imgdata[x, y] != 0)
                     {
                         loadedblocks.SetPixel(x - 16, y, palette[imgdata[x, y]]);

                         loadedblocks.SetPixel(x, y, palette2[imgdata[x, y]]);

                         loadedblocks.SetPixel(x + 16, y, palette3[imgdata[x, y]]);
                     }
                 }
             }*/
            byte[,] dataX = load16x16(0x4C0);

            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    loadedblocks.SetPixel(x, y + 8, palette[dataX[x, y]]);
                    loadedblocks.SetPixel(x + 16, y + 8, palette2[dataX[x, y]]);
                    loadedblocks.SetPixel(x + 32, y + 8, palette3[dataX[x, y]]);
                }
            }

            dataX = load16x16(0x40);

            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    if (dataX[x, y] != 0)
                    {
                        loadedblocks.SetPixel(x, y, palette[dataX[x, y]]);
                        loadedblocks.SetPixel(x + 16, y, palette2[dataX[x, y]]);
                        loadedblocks.SetPixel(x + 32, y, palette3[dataX[x, y]]);
                    }
                }
            }



            g.Clear(backgroundColor);
            g.DrawImage(loadedblocks, new Rectangle(2, 0, 128, 48), new Rectangle(0, 0, 64, 24), GraphicsUnit.Pixel);
            //linkSpritePicturebox.Refresh();

            return picImage;
        }

    }
}
