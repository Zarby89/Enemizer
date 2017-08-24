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
using EnemizerLibrary;
using Newtonsoft.Json;

namespace Enemizer
{
    public partial class EnemizerForm : Form
    {
        readonly string configFilename = "setting.cfg";
        EnemizerConfig config = new EnemizerConfig();
        OptionFlags optionFlags = new OptionFlags();
        Random rand = new Random();

        public EnemizerForm()
        {
            InitializeComponent();
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

        public byte[] load8x8(int pos = 0)
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

        public byte[,] load16x16(int pos = 0) //pos 0x40 = head facing down, pos 0x4C0 = body facing down
        {
            byte[,] temp_array = new byte[16, 16];
            byte[] topLeft = load8x8(pos);
            byte[] topRight = load8x8(pos+0x20);
            byte[] bottomLeft = load8x8(pos+0x200);
            byte[] bottomRight = load8x8(pos+0x200+0x20);

            //copy all the bytes at the correct position in the 2d array
            for(int x = 0;x<8;x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    temp_array[x, y] = topLeft[x + (y * 8)];
                    temp_array[x+8, y] = topRight[x + (y * 8)];
                    temp_array[x, y+8] = bottomLeft[x + (y * 8)];
                    temp_array[x+8, y+8] = bottomRight[x + (y * 8)];
                }
            }
            return temp_array;
        }

        Bitmap loadedblocks = new Bitmap(128, 32);
        public void updateGraphic(int pos)
        {
            linkSpritePicturebox.Image = new Bitmap(128, 48);
            loadedblocks = new Bitmap(128, 32);
            Graphics g = Graphics.FromImage(linkSpritePicturebox.Image);
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
                    loadedblocks.SetPixel(x, y+8, palette[dataX[x, y]]);
                    loadedblocks.SetPixel(x+16, y + 8, palette2[dataX[x, y]]);
                    loadedblocks.SetPixel(x+32, y + 8, palette3[dataX[x, y]]);
                }
            }

            dataX = load16x16(0x40);

            for (int x = 0;x<16;x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    if (dataX[x, y] != 0)
                    {
                        loadedblocks.SetPixel(x, y, palette[dataX[x, y]]);
                        loadedblocks.SetPixel(x+16, y, palette2[dataX[x, y]]);
                        loadedblocks.SetPixel(x+32, y, palette3[dataX[x, y]]);
                    }
                }
            }



            g.Clear(linkSpritePicturebox.BackColor);
            g.DrawImage(loadedblocks,new Rectangle(2,0,128,48), new Rectangle(0, 0, 64, 24), GraphicsUnit.Pixel);
            linkSpritePicturebox.Refresh();
        }


        private void EnemizerForm_Load(object sender, EventArgs e)
        {
            //panel1.BackColor = Color.FromArgb(255, 128, 192);
            this.Text = "Enemizer " + EnemizerLibrary.Version.CurrentVersion;
            linkSpriteCombobox.Items.Add(new files_names("Default", "Default"));
            linkSpriteCombobox.Items.Add(new files_names("Random", "Random"));
            linkSpriteCombobox.SelectedIndex = 0;

            foreach (string f in Directory.GetFiles("sprites\\"))
            {
                files_names item = new files_names(Path.GetFileNameWithoutExtension(f), f);
                linkSpriteCombobox.Items.Add(item);
            }

            if (LoadConfig())
            {
                update_flags();
                checkForUpdatesCheckbox.Checked = config.CheckForUpdates;

                if (config.CheckForUpdates)
                {
                    if (EnemizerLibrary.Version.CheckUpdate() == true)
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

        private bool LoadConfig()
        {
            try
            {
                if (File.Exists(configFilename))
                {
                    config = JsonConvert.DeserializeObject<EnemizerConfig>(File.ReadAllText(configFilename));
                    if(config != null)
                    {
                        randomizeBossesCheckbox.Checked = config.OptionFlags.RandomizeBosses;

                        return true;
                    }
                }
            }
            catch
            {
                // invalid file
                MessageBox.Show("Invalid setting file. Loading defaults.");
            }

            config = new EnemizerConfig();
            return false;
        }

        private void SaveConfig()
        {
            var configJson = JsonConvert.SerializeObject(config);
            File.WriteAllText("setting.cfg", configJson);
        }


        public void update_flags()
        {
            // TODO: update optionFlags

            //int flagsText = 0;
            //for (int i = 0; i < extraSettingsCheckedList.Items.Count-1; i++)
            //{
            //    extraSettingsCheckedList.SetItemCheckState(i, CheckState.Unchecked);
            //    if ((flagsText & flags_setter[i + 1]) == flags_setter[i + 1])
            //    {
            //        extraSettingsCheckedList.SetItemCheckState(i, CheckState.Checked);

            //    }
            //}
            //flags = flagsText;
        }


        /*
         * Main form
         */

        private void checkForUpdatesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.CheckForUpdates = checkForUpdatesCheckbox.Checked;
            SaveConfig();
        }

        private void checkForUpdatesButton_Click(object sender, EventArgs e)
        {
            if (EnemizerLibrary.Version.CheckUpdate() == true)
            {
                //update
                var window = MessageBox.Show("There is a new version available, do you want to download the update?", "Update Available", MessageBoxButtons.YesNo);
                if (window == DialogResult.Yes)
                {
                    // TODO: is this really what we want to do?
                    Help.ShowHelp(null, @"https://zarby89.github.io/Enimizer/");
                }
            }
            else
            {
                MessageBox.Show("No update available");
                //noupdate
            }
        }

        private void linkSpriteCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (linkSpriteCombobox.SelectedIndex > 1)
            {
                FileStream fs = new FileStream((linkSpriteCombobox.Items[linkSpriteCombobox.SelectedIndex] as files_names).file.ToString(), FileMode.Open, FileAccess.Read);
                data = new byte[fs.Length];
                fs.Read(data, 0, (int)fs.Length);
                load_palette();
                //load4bpp();
                //load8x8();
                refreshEverything();
                fs.Close();
            }
        }

        private void randomizeLinksPaletteCheckbox_MouseClick(object sender, MouseEventArgs e)
        {
            if (randomizeLinksPaletteCheckbox.Checked == true)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to use random palette for link it can looks really really bad\nFor better result use link original sprite", "Confirmation", MessageBoxButtons.YesNo);
                randomizeLinksPaletteCheckbox.Checked = (dialogResult == DialogResult.Yes);
            }
        }

        private void weaponSpriteCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void completeModificationCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void generateRomButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Randomizer Roms (*.sfc)|*.sfc|All Files (*.*)|*.*";
            ofd.Title = "Select a Randomizer Rom File";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
                byte[] rom_data = new byte[fs.Length];
                fs.Read(rom_data, 0, (int)fs.Length);
                fs.Close();

                /*int seed = 0;
                if (textBox1.Text == "")
                {
                    seed = rand.Next();
                }
                else
                {
                    seed = int.Parse(textBox1.Text);
                }*/

                SaveConfig();

                Randomization randomize = new Randomization(rand.Next(), config.OptionFlags, rom_data, ofd.FileName, (linkSpriteCombobox.Items[linkSpriteCombobox.SelectedIndex] as files_names).file.ToString(), generateSpoilerCheckbox.Checked, randomizeLinksPaletteCheckbox.Checked);
            }
        }

        private void generateSpoilerCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.GenerateSpoilers = generateSpoilerCheckbox.Checked;
            SaveConfig();
        }

        /*
         * Enemies tab
         */
        private void randomizeEnemiesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeEnemies = randomizeEnemiesCheckbox.Checked;
            randomizationTypeTrackbar.Enabled = config.OptionFlags.RandomizeEnemies;
            SaveConfig();
        }

        private void randomizationTypeTrackbar_ValueChanged(object sender, EventArgs e)
        {
            //typeLabel.Text = typeString[typeTrackbar.Value];
            config.OptionFlags.RandomizeEnemiesType = (RandomizeEnemiesType)randomizationTypeTrackbar.Value;
            typeLabel.Text = ((RandomizeEnemiesType)randomizationTypeTrackbar.Value).ToString();
            SaveConfig();
        }

        private void randomizeEnemiesHealthCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeEnemyHealthRange = randomizeEnemiesHealthCheckbox.Checked;
            randomizeEnemiesHealthTrackbar.Enabled = config.OptionFlags.RandomizeEnemyHealthRange;
            SaveConfig();
        }

        private void randomizeEnemiesHealthTrackbar_ValueChanged(object sender, EventArgs e)
        {
            var healthMin = (2 * randomizeEnemiesHealthTrackbar.Value);
            healthLabel.Text = $"±{healthMin}%";
            config.OptionFlags.RandomizeEnemyHealthRangeAmount = healthMin;
            SaveConfig();
        }

        private void randomizeEnemiesDamageCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeEnemyDamage = randomizeEnemiesDamageCheckbox.Checked;
            allowZeroDamageCheckbox.Enabled = config.OptionFlags.RandomizeEnemyDamage;
            SaveConfig();
        }

        private void allowZeroDamageCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.AllowEnemyZeroDamage = allowZeroDamageCheckbox.Checked;
            SaveConfig();
        }

        private void easyModeEscapeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.EasyModeEscape = easyModeEscapeCheckbox.Checked;
            SaveConfig();
        }

        private void allowAbsorbableItemsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.EnemiesAbsorbable = allowAbsorbableItemsCheckbox.Checked;
            absorbableItemsChecklist.Enabled = config.OptionFlags.EnemiesAbsorbable;
            absorbableItemsSpawnrateTrackbar.Enabled = config.OptionFlags.EnemiesAbsorbable;
            SaveConfig();
        }

        private void absorbableItemsSpawnrateTrackbar_ValueChanged(object sender, EventArgs e)
        {
            var spawnRate = 5 * absorbableItemsSpawnrateTrackbar.Value;
            spawnrateLabel.Text = spawnRate.ToString("D2") + "%";

            config.OptionFlags.AbsorbableSpawnRate = spawnRate;
            SaveConfig();
        }

        private void absorbableItemsChecklist_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var selectedItem = absorbableItemsChecklist.Items[e.Index].ToString();
            var type = EnumEx.GetValueFromDescription<AbsorbableTypes>(selectedItem);
            var isSet = (e.NewValue == CheckState.Checked);
            config.OptionFlags.AbsorbableTypes[type] = isSet;
            SaveConfig();
        }


        /*
         * Bosses tab
         */
        private void randomizeBossesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeBosses = randomizeBossesCheckbox.Checked;
            bossRandomizationTypesTrackbar.Enabled = config.OptionFlags.RandomizeBosses;
            SaveConfig();
        }

        private void bossRandomizationTypesTrackbar_ValueChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeBossesType = (RandomizeBossesType)bossRandomizationTypesTrackbar.Value;
            typebossLabel.Text = ((RandomizeBossesType)bossRandomizationTypesTrackbar.Value).ToString();
            SaveConfig();
        }

        private void randomizeBossHealthCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeBossHealth = randomizeBossHealthCheckbox.Checked;
            bossHealthRandomizationTrackbar.Enabled = config.OptionFlags.RandomizeBossHealth;
            SaveConfig();
        }

        private void bossHealthRandomizationTrackbar_ValueChanged(object sender, EventArgs e)
        {
            var bosshealthMin = 100 - (5 * bossHealthRandomizationTrackbar.Value);
            var bosshealthMax = (100 + (10 * bossHealthRandomizationTrackbar.Value));
            bosshealthLabel.Text = bosshealthMin.ToString("D2") + "% - " + bosshealthMax.ToString("D2") + "%";

            config.OptionFlags.RandomizeBossHealthMinAmount = bosshealthMin;
            config.OptionFlags.RandomizeBossHealthMaxAmount = bosshealthMax;
            SaveConfig();
        }

        private void randomizeBossDamageCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeBossDamage = randomizeBossDamageCheckbox.Checked;
            bossDamageRandomizationTrackbar.Enabled = config.OptionFlags.RandomizeBossDamage;
            SaveConfig();
        }

        private void bossDamageRandomizationTrackbar_ValueChanged(object sender, EventArgs e)
        {
            var bossdamageMin = 100 - (5 * bossDamageRandomizationTrackbar.Value);
            var bossdamageMax = (100 + (5 * bossDamageRandomizationTrackbar.Value));
            bossdamageLabel.Text = bossdamageMin.ToString("D2") + "% - " + bossdamageMax.ToString("D2") + "%";

            config.OptionFlags.RandomizeBossDamageMinAmount = bossdamageMin;
            config.OptionFlags.RandomizeBossDamageMaxAmount = bossdamageMax;
            SaveConfig();
        }

        private void randomizeBossBehaviorCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeBossBehavior = randomizeBossBehaviorCheckbox.Checked;
            SaveConfig();
        }


        /*
         * Palettes tab
         */
        private void randomizeDungeonPalettesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeDungeonPalettes = randomizeDungeonPalettesCheckbox.Checked;
            setBlackoutModeCheckbox.Enabled = config.OptionFlags.RandomizeDungeonPalettes;
            SaveConfig();
        }

        private void setBlackoutModeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.SetBlackoutMode = setBlackoutModeCheckbox.Checked;
            SaveConfig();
        }

        private void randomizeOverworldPalettesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeOverworldPalettes = randomizeOverworldPalettesCheckbox.Checked;
            SaveConfig();
        }

        private void randomizeSpritePalettesBasicCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeSpritePalettes = randomizeSpritePalettesBasicCheckbox.Checked;
            randomizeSpritePalettesAdvancedCheckbox.Enabled = config.OptionFlags.RandomizeSpritePalettes;
            SaveConfig();
        }

        private void randomizeSpritePalettesAdvancedCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.SetAdvancedSpritePalettes = randomizeSpritePalettesAdvancedCheckbox.Checked;
            SaveConfig();
        }


        /*
         * Extras tab
         */
        private void extraSettingsCheckedList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // TODO: add extra flags
            //SaveConfig();
        }


        public string[] description = new string[16]
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
            "Allow Pots to be shuffled\nwithin one room",
            "Everytime you transition damage and hp of enemies change"
        };
        // "Randomize All bosses, no unique\nbosses every bosses can be anywhere\nyou can have trinexx everywhere\nthis box overwrite shuffle bosses",

        private void extraSettingsCheckedList_SelectedIndexChanged(object sender, EventArgs e)
        {
            descriptionLabel.Text = description[extraSettingsCheckedList.SelectedIndex];
        }

    }



}
