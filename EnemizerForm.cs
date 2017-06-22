using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Enemizer
{
    public partial class EnemizerForm : Form
    {
        public EnemizerForm()
        {
            InitializeComponent();
        }

        public class files_names
        {
            public string name = "";
            public string file = "";
            public files_names(string name, string file)
            {
                this.name = name;
                this.file = file;
            }
            public override string ToString()
            {
                return name;
            }

        }
        Color[] palette = new Color[17];
        Color[] palette2 = new Color[17];
        Color[] palette3 = new Color[17];

        public Color getColor(short c)
        {
            return Color.FromArgb(((c & 0x1F) * 8), ((c & 0x3E0) >> 5) * 8, ((c & 0x7C00) >> 10) * 8);
        }

        public void load_palette()
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


        public void refreshEverything()
        {
            for (int i = 0; i < 1; i++)
            {
                load4bpp(i);
                updateGraphic(i);
            }
        }
        byte[,] imgdata = new byte[128, 32];
        byte[] data;
        int[] positions = new int[] { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };
        int hexOffset = 0x0;
        public void load4bpp(int pos = 0)
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

        Bitmap loadedblocks = new Bitmap(128, 32);
        public void updateGraphic(int pos)
        {
            pictureBox1.Image = new Bitmap(128, 48);
            loadedblocks = new Bitmap(128, 32);
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            for (int x = 48; x < 64; x++)
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
            }
            g.Clear(pictureBox1.BackColor);
            g.DrawImage(loadedblocks,new Rectangle(2,0,128,48), new Rectangle(0, 0, 64, 24), GraphicsUnit.Pixel);
            pictureBox1.Refresh();
        }


        private void EnemizerForm_Load(object sender, EventArgs e)
        {
            //panel1.BackColor = Color.FromArgb(255, 128, 192);
            
            comboBox1.Items.Add(new files_names("Default", "Default"));
            comboBox1.SelectedIndex = 0;

            foreach (string f in Directory.GetFiles("sprites\\"))
            {
                files_names item = new files_names(Path.GetFileNameWithoutExtension(f), f);
                comboBox1.Items.Add(item);
            }
            if (File.Exists("setting.cfg"))
            {
                bool checkb = false;
                BinaryReader fw = new BinaryReader(new FileStream("setting.cfg", FileMode.Open, FileAccess.Read));
                checkb = fw.ReadBoolean();
                flags = fw.ReadInt32();
                textBox2.Text = flags.ToString();
                fw.Close();
                update_flags();
                checkBox2.Checked = checkb;

                if (checkb)
                {
                    if (Version.CheckUpdate() == true)
                    {
                        var window = MessageBox.Show("There is a new version available, do you want to download the update?", "Update Available", MessageBoxButtons.YesNo);
                        if (window == DialogResult.Yes)
                        {
                            Help.ShowHelp(null, @"https://zarby89.github.io/Enimizer");
                        }
                    }
                }

            }


        }
        Random rand = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
            byte[] rom_data = new byte[fs.Length];
            fs.Read(rom_data, 0, (int)fs.Length);
            fs.Close();
            int seed = 0;
            if (textBox1.Text == "")
            {
                seed = rand.Next();
            }
            else
            {
                seed = int.Parse(textBox1.Text);
            }
            BinaryWriter fw = new BinaryWriter(new FileStream("setting.cfg", FileMode.OpenOrCreate, FileAccess.Write));
            fw.Write((bool)checkBox2.Checked);
            fw.Write((int)flags);
            fw.Close();
            Randomization randomize = new Randomization(seed, flags, rom_data, openFileDialog1.FileName,(comboBox1.Items[comboBox1.SelectedIndex] as files_names).file.ToString(),checkBox1.Checked);
        }
        int flags = 0;
        int[] flags_setter = new int[16] { 0x00, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x100, 0x200, 0x400,0x800,0x1000,0x2000,0x4000 };
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }

        private void checkedListBox1_MouseUp(object sender, MouseEventArgs e)
        {
            flags = 0;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemCheckState(i) == CheckState.Checked)
                {
                    flags += flags_setter[i + 1];
                }
            }

            textBox2.Text = flags.ToString();
        }

        public string[] description = new string[15]
        {
            "Randomize enemies inside\ndungeons / houses / caves",
            "Randomize enemies on the\noverworld",
            "Randomize color palettes\nof dungeons",
            "Randomize color palettes\nof enemies",
            "Randomize color palettes\nof overworld screens\n(can be weird)",
            "Randomize Enemy HP\nup to a max value of -10/+10\nexample a popo can't have\nmore than 11 hp",
            "Randomize Enemy Damage\nGroup sprite could do ganon damage\nor bumper damage(0)",
            "Set all Enemy HP to 0",
            "Shuffle All Bosses\n (no repeat bosses)",
            "Put Absorbable items like keys,\nfairy,rupees,bombs,arrows in\nthe Enemy spawn pool",
            "Randomize All Bosses\n (allow duplicate bosses)",
            "Set all palettes pitch black\nexcept sprites, remove dark rooms\n",
            "Shuffle all background music",
            "Allow Custom Bosses\nto replace one of the original boss\nCurrently not working",
            "Allow Pots to be shuffled\nwithin one room"
        };
        // "Randomize All bosses, no unique\nbosses every bosses can be anywhere\nyou can have trinexx everywhere\nthis box overwrite shuffle bosses",

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            descriptionLabel.Text = description[checkedListBox1.SelectedIndex];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flags = 0;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (i != 7 && i != 10 && i != 13 && i != 11)
                {
                    checkedListBox1.SetItemCheckState(i, CheckState.Checked);
                    flags += flags_setter[i + 1];
                }
            }

            textBox2.Text = flags.ToString();
        }



        byte[] r_data;

        private void button3_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("zeldapalettetest.sfc", FileMode.Open, FileAccess.Read);
            r_data = new byte[fs.Length];
            fs.Read(r_data, 0, (int)fs.Length);
            fs.Close();

            /* setColor(0x0DD744, Color.Red,2);
             setColor(0x0DD746, Color.Red,4);
             setColor(0x0DD748, Color.Red,6);
             setColor(0x0DD74A, Color.Red,8);
             setColor(0x0DD74C, Color.Red,10);
             */
            //randomize_wall(0);
            //randomize_floors(0);
            // = Color.FromArgb(255, 255, 255);

            byte[] changing = new byte[] //0 = no change, 1 = yes
    {
            0,1,1,1,0,1,1,0,0,1,1,1,0,1,1,
            0,1,1,1,0,1,1,0,0,1,1,1,0,1,1,
            0,1,1,1,0,1,1,0,0,0,0,0,0,0,0,
            0,1,1,1,0,1,1,0,0,1,1,1,0,1,1
    };

            byte[] aux_changing = new byte[] //0 = no change, 1 = yes
            {
        1, 1, 1, 1, 1, 1, 1,0, 1, 1, 1, 0, 1, 1,1, 1, 1, 1, 0, 1, 1,
        1, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,
        0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,
        0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,1, 1, 1, 1, 1, 1, 1,
        0, 1, 1, 1, 1, 1, 1,0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1,
        0, 0, 0, 0, 1, 1, 1,0, 0, 1, 1, 0, 1, 1,0, 1, 1, 1, 1, 1, 1,
        0, 1, 1, 1, 1, 1, 1,0, 0, 1, 1, 1, 1, 1,1, 1, 1, 1, 1, 1, 1,
        1, 1, 1, 1, 1, 1, 1,1, 1, 1, 1, 1, 1, 1,0, 1, 1, 1, 0, 1, 1,
        0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1,
        0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,1, 1, 1, 1, 0, 1, 1,
        0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,
        0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,
        0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,
        0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,1, 1, 1, 1, 0, 0, 1,
        0, 1, 1, 1, 0, 1, 1,0, 1, 1, 1, 0, 1, 1,1, 1, 1, 1, 0, 1, 1,
        0, 1, 1, 1, 0, 1, 1,1, 1, 1, 1, 1, 1, 1
            };


             byte[] palette_l = new byte[]
             {
                
             };

            
                int pos = 0x0DD290;
                richTextBox1.AppendText("Color c = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));\n");
                for (int i =0;i<palette_l.Length;i++)
                {

                    if (palette_l[i] == 0)
                    {
                        pos += 2;
                    }
                    else
                    {
                        richTextBox1.AppendText("c = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));\n");
                        for (int j = 0; j < palette_l[i]; j++)
                        {
                            richTextBox1.AppendText("setColor(0x" + pos.ToString("X6") + ",c," + ((palette_l[i] * 2) - (j * 2)).ToString() + ");\n");
                            pos += 2;
                        }
                    }
                }

 


            //0x0DD734 * 5 == wall1
            //0x0DD770 * 5 == wall2
            //0x0DD744 * 5 == wall3
            fs = new FileStream("zeldapalettetest.sfc", FileMode.OpenOrCreate, FileAccess.Write);
                        fs.Write(r_data, 0, r_data.Length);
                        fs.Close();
                    }

                    public void setColor(int address, Color col, byte shade = 0)
                    {

                        byte r = col.R;
                        byte g = col.G;
                        byte b = col.B;
                        if ((r / 8) - shade >= 0)
                        {
                            r = (byte)((r / 8) - shade);
                        }
                        else
                        {
                            r = 0;
                        }
                        if ((g / 8) - shade >= 0)
                        {
                            g = (byte)((g / 8) - shade);
                        }
                        else
                        {
                            g = 0;
                        }
                        if ((b / 8) - shade >= 0)
                        {
                            b = (byte)((b / 8) - shade);
                        }
                        else
                        {
                            b = 0;
                        }
                        short s = (short)(((b) << 10) | ((g) << 5) | ((r) << 0));

                        /* byte[] bb = BitConverter.GetBytes(s);
                         colorBytes[c * 2] = bb[0];
                         colorBytes[(c * 2) + 1] = bb[1];*/
            //Console.WriteLine("RED : " + (s & 0x1F));
            //Console.WriteLine("GREEN : " + ((s & 0x3E0)>>5) );
            //Console.WriteLine("BLUE : " + ((s & 0x7C00 )>>10));
            r_data[address] = (byte)(s & 0x00FF);
            r_data[address + 1] = (byte)((s >> 8) & 0x00FF);


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            update_flags();
        }

        public void update_flags()
        {
            int flagsText = 0;
            Int32.TryParse(textBox2.Text, out flagsText);

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
                if ((flagsText & flags_setter[i + 1]) == flags_setter[i + 1])
                {
                    checkedListBox1.SetItemCheckState(i, CheckState.Checked);

                }
            }
            flags = flagsText;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Version.CheckUpdate() == true)
            {
                //update
                    var window = MessageBox.Show("There is a new version available, do you want to download the update?", "Update Available", MessageBoxButtons.YesNo);
                    if (window == DialogResult.Yes)
                    {
                        Help.ShowHelp(null, @"https://zarby89.github.io/Enimizer/");
                    }
            }
            else
            {
                MessageBox.Show("No update available");
                //noupdate
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            BinaryWriter fw = new BinaryWriter(new FileStream("setting.cfg", FileMode.OpenOrCreate, FileAccess.Write));
            fw.Write((bool)checkBox2.Checked);
            fw.Write((int)flags);
            fw.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != 0)
            {
                FileStream fs = new FileStream((comboBox1.Items[comboBox1.SelectedIndex] as files_names).file.ToString(), FileMode.Open, FileAccess.Read);
                data = new byte[fs.Length];
                fs.Read(data, 0, (int)fs.Length);
                load_palette();
                load4bpp();
                refreshEverything();
                fs.Close();
            }
        }
    }



}
