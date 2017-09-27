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

        private void EnemizerForm_Load(object sender, EventArgs e)
        {
            //panel1.BackColor = Color.FromArgb(255, 128, 192);
            this.Text = "Enemizer " + EnemizerLibrary.Version.CurrentVersion;

            LoadSpriteDropdown();

            if (LoadConfig())
            {
                UpdateUIFromConfig();

                if (config.CheckForUpdates)
                {
                    CheckForUpdates();
                }
            }
        }

        private void EnemizerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig();
        }

        private bool CheckForUpdates()
        {
            if (EnemizerLibrary.Version.CheckUpdate() == true)
            {
                var window = MessageBox.Show("There is a new version available, do you want to download the update?", "Update Available", MessageBoxButtons.YesNo);
                if (window == DialogResult.Yes)
                {
                    Help.ShowHelp(null, @"https://zarby89.github.io/Enimizer");
                    return true;
                }
            }
            return false;
        }

        private void checkForUpdatesButton_Click(object sender, EventArgs e)
        {
            if (!CheckForUpdates())
            {
                MessageBox.Show("No update available");
                //noupdate
            }
        }

        private void LoadSpriteDropdown()
        {
            linkSpriteCombobox.Items.Add(new files_names("Default", "Default"));
            linkSpriteCombobox.Items.Add(new files_names("Random", "Random"));
            linkSpriteCombobox.SelectedIndex = 0;

            foreach (string f in Directory.GetFiles("sprites\\"))
            {
                files_names item = new files_names(Path.GetFileNameWithoutExtension(f), f);
                linkSpriteCombobox.Items.Add(item);
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
            var configJson = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText("setting.cfg", configJson);
        }


        public void UpdateUIFromConfig()
        {
            // TODO: update anything else
            UpdateMainFormUIFromConfig();

            UpdateEnemiesTabUIFromConfig();

            UpdateBossesTabUIFromConfig();

            UpdatePalettesTabUIFromConfig();

            UpdateExtrasTabUIFromConfig();

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

        private void UpdateMainFormUIFromConfig()
        {
            checkForUpdatesCheckbox.Checked = config.CheckForUpdates;

            randomizeLinksPaletteCheckbox.Checked = config.OptionFlags.RandomizeLinkSpritePalette;

            generateSpoilerCheckbox.Checked = config.OptionFlags.GenerateSpoilers;
        }

        private void UpdateEnemiesTabUIFromConfig()
        {
            randomizeEnemiesCheckbox.Checked = config.OptionFlags.RandomizeEnemies;
            randomizationTypeTrackbar.Enabled = config.OptionFlags.RandomizeEnemies;
            chkRandomizeBushEnemyChance.Enabled = config.OptionFlags.RandomizeEnemies;

            randomizationTypeTrackbar.Value = (int)config.OptionFlags.RandomizeEnemiesType;
            randomizationTypeLabel.Text = ((RandomizeEnemiesType)randomizationTypeTrackbar.Value).ToString();

            chkRandomizeBushEnemyChance.Checked = config.OptionFlags.RandomizeBushEnemyChance;

            randomizeEnemiesHealthCheckbox.Checked = config.OptionFlags.RandomizeEnemyHealthRange;
            randomizeEnemiesHealthTrackbar.Enabled = config.OptionFlags.RandomizeEnemyHealthRange;

            randomizeEnemiesHealthTrackbar.Value = config.OptionFlags.RandomizeEnemyHealthRangeAmount / 2; // TODO: don't hardcode magic numbers
            healthLabel.Text = $"±{config.OptionFlags.RandomizeEnemyHealthRangeAmount}%";

            randomizeEnemiesDamageCheckbox.Checked = config.OptionFlags.RandomizeEnemyDamage;
            allowZeroDamageCheckbox.Enabled = config.OptionFlags.RandomizeEnemyDamage;

            allowZeroDamageCheckbox.Checked = config.OptionFlags.AllowEnemyZeroDamage;

            easyModeEscapeCheckbox.Checked = config.OptionFlags.EasyModeEscape;

            allowAbsorbableItemsCheckbox.Checked = config.OptionFlags.EnemiesAbsorbable;
            absorbableItemsChecklist.Enabled = config.OptionFlags.EnemiesAbsorbable;
            absorbableItemsSpawnrateTrackbar.Enabled = config.OptionFlags.EnemiesAbsorbable;

            absorbableItemsSpawnrateTrackbar.Value = config.OptionFlags.AbsorbableSpawnRate / 5; // TODO: don't hardcode magic numbers
            spawnrateLabel.Text = $"{config.OptionFlags.AbsorbableSpawnRate}%";

            LoadAbsorbableItemsChecklistFromConfig();
        }

        private void UpdateBossesTabUIFromConfig()
        {
            randomizeBossesCheckbox.Checked = config.OptionFlags.RandomizeBosses;
            bossRandomizationTypesTrackbar.Enabled = config.OptionFlags.RandomizeBosses;

            bossRandomizationTypesTrackbar.Value = (int)config.OptionFlags.RandomizeBossesType;
            typebossLabel.Text = config.OptionFlags.RandomizeBossesType.ToString();

            randomizeBossHealthCheckbox.Checked = config.OptionFlags.RandomizeBossHealth;
            bossHealthRandomizationTrackbar.Enabled = config.OptionFlags.RandomizeBossHealth;

            bossHealthRandomizationTrackbar.Value = (100 - config.OptionFlags.RandomizeBossHealthMinAmount) / 5; // TODO: don't hardcode magic numbers
            bosshealthLabel.Text = $"{config.OptionFlags.RandomizeBossHealthMinAmount}% - {config.OptionFlags.RandomizeBossHealthMaxAmount}%";

            randomizeBossDamageCheckbox.Checked = config.OptionFlags.RandomizeBossDamage;
            bossDamageRandomizationTrackbar.Enabled = config.OptionFlags.RandomizeBossDamage;

            bossDamageRandomizationTrackbar.Value = (100 - config.OptionFlags.RandomizeBossDamageMinAmount) / 5; // TODO: don't hardcode magic numbers
            bossdamageLabel.Text = $"{config.OptionFlags.RandomizeBossDamageMinAmount}% - {config.OptionFlags.RandomizeBossDamageMaxAmount}%";

            randomizeBossBehaviorCheckbox.Checked = config.OptionFlags.RandomizeBossBehavior;
        }

        private void UpdatePalettesTabUIFromConfig()
        {
            randomizeDungeonPalettesCheckbox.Checked = config.OptionFlags.RandomizeDungeonPalettes;
            setBlackoutModeCheckbox.Enabled = config.OptionFlags.RandomizeDungeonPalettes;

            setBlackoutModeCheckbox.Checked = config.OptionFlags.SetBlackoutMode;

            randomizeOverworldPalettesCheckbox.Checked = config.OptionFlags.RandomizeOverworldPalettes;

            randomizeSpritePalettesBasicCheckbox.Checked = config.OptionFlags.RandomizeSpritePalettes;
            randomizeSpritePalettesAdvancedCheckbox.Enabled = config.OptionFlags.RandomizeSpritePalettes;

            randomizeSpritePalettesAdvancedCheckbox.Checked = config.OptionFlags.SetAdvancedSpritePalettes;
        }

        private void UpdateExtrasTabUIFromConfig()
        {
            chkBootlegMagic.Checked = config.OptionFlags.BootlegMagic;
        }

        private void LoadAbsorbableItemsChecklistFromConfig()
        {
            for (int i = 0; i < absorbableItemsChecklist.Items.Count; i++)
            {
                var item = absorbableItemsChecklist.Items[i];
                bool isSet = false;
                try
                {

                    var type = EnumEx.GetValueFromDescription<AbsorbableTypes>(item.ToString());

                    if (config.OptionFlags.AbsorbableTypes.TryGetValue(type, out isSet))
                    {
                        absorbableItemsChecklist.SetItemChecked(i, isSet);
                    }
                }
                catch
                {
                    // probably a value that wasn't in our AbsorbableTypes enum
                    absorbableItemsChecklist.SetItemChecked(i, false);
                }
            }
        }


        /*
         * Main form
         */

        private void checkForUpdatesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.CheckForUpdates = checkForUpdatesCheckbox.Checked;
        }

        private void linkSpriteCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (linkSpriteCombobox.SelectedIndex > 1)
            {
                FileStream fs = new FileStream((linkSpriteCombobox.Items[linkSpriteCombobox.SelectedIndex] as files_names).file.ToString(), FileMode.Open, FileAccess.Read);
                var data = new byte[fs.Length];
                fs.Read(data, 0, (int)fs.Length);
                fs.Close();

                Sprite linkSprite = new Sprite();
                linkSpritePicturebox.Image = linkSprite.refreshEverything(linkSpritePicturebox.BackColor, linkSpritePicturebox.Image, data);
                linkSpritePicturebox.Refresh();
            }
        }

        private void randomizeLinksPaletteCheckbox_MouseClick(object sender, MouseEventArgs e)
        {
            if (randomizeLinksPaletteCheckbox.Checked == true)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to use random palette for link it can looks really really bad\nFor better result use link original sprite", "Confirmation", MessageBoxButtons.YesNo);
                randomizeLinksPaletteCheckbox.Checked = (dialogResult == DialogResult.Yes);
                config.OptionFlags.RandomizeLinkSpritePalette = randomizeLinksPaletteCheckbox.Checked;
            }
            else
            {
                config.OptionFlags.RandomizeLinkSpritePalette = randomizeLinksPaletteCheckbox.Checked;
            }
        }

        private void weaponSpriteCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TODO: wire this up
        }

        private void completeModificationCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TODO: wire this up
        }

        private void generateRomButton_Click(object sender, EventArgs e)
        {
            int seed = 0;
            if (String.IsNullOrEmpty(seedNumberTextbox.Text))
            {
                seed = rand.Next();
            }
            else
            {
                // TODO: add validation to the textbox so it can't be anything but a number
                if (!int.TryParse(seedNumberTextbox.Text, out seed))
                {
                    MessageBox.Show("Invalid Seed Number entered. Please enter an integer value.");
                    return;
                }
            }

            //try
            //{
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Randomizer Roms (*.sfc)|*.sfc|All Files (*.*)|*.*";
                ofd.Title = "Select a Randomizer Rom File";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
                    byte[] rom_data = new byte[fs.Length];
                    fs.Read(rom_data, 0, (int)fs.Length);
                    fs.Close();

                    SaveConfig();

                    var linkSpriteFilename = (linkSpriteCombobox.Items[linkSpriteCombobox.SelectedIndex] as files_names).file.ToString();
                    Randomization randomize = new Randomization();
                    RomData randomizedRom = randomize.MakeRandomization(seed, config.OptionFlags, rom_data, linkSpriteFilename);

                    string fileName = "Enemizer " + EnemizerLibrary.Version.CurrentVersion + " - " + Path.GetFileName(ofd.FileName);
                    fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                    randomizedRom.WriteRom(fs);
                    fs.Close();

                    MessageBox.Show("Enemizer " + EnemizerLibrary.Version.CurrentVersion + " - " + Path.GetFileName(ofd.FileName) + " Has been created in the enemizer folder !");

                }
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void generateSpoilerCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.GenerateSpoilers = generateSpoilerCheckbox.Checked;
        }

        /*
         * Enemies tab
         */
        private void randomizeEnemiesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeEnemies = randomizeEnemiesCheckbox.Checked;
            randomizationTypeTrackbar.Enabled = config.OptionFlags.RandomizeEnemies;
        }

        private void randomizationTypeTrackbar_ValueChanged(object sender, EventArgs e)
        {
            //typeLabel.Text = typeString[typeTrackbar.Value];
            config.OptionFlags.RandomizeEnemiesType = (RandomizeEnemiesType)randomizationTypeTrackbar.Value;
            randomizationTypeLabel.Text = ((RandomizeEnemiesType)randomizationTypeTrackbar.Value).ToString();
        }

        private void chkRandomizeBushEnemyChance_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeBushEnemyChance = chkRandomizeBushEnemyChance.Checked;
        }

        private void randomizeEnemiesHealthCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeEnemyHealthRange = randomizeEnemiesHealthCheckbox.Checked;
            randomizeEnemiesHealthTrackbar.Enabled = config.OptionFlags.RandomizeEnemyHealthRange;
        }

        private void randomizeEnemiesHealthTrackbar_ValueChanged(object sender, EventArgs e)
        {
            var healthMin = (2 * randomizeEnemiesHealthTrackbar.Value);
            healthLabel.Text = $"±{healthMin}%";
            config.OptionFlags.RandomizeEnemyHealthRangeAmount = healthMin;
        }

        private void randomizeEnemiesDamageCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeEnemyDamage = randomizeEnemiesDamageCheckbox.Checked;
            allowZeroDamageCheckbox.Enabled = config.OptionFlags.RandomizeEnemyDamage;
        }

        private void allowZeroDamageCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.AllowEnemyZeroDamage = allowZeroDamageCheckbox.Checked;
        }

        private void easyModeEscapeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.EasyModeEscape = easyModeEscapeCheckbox.Checked;
        }

        private void allowAbsorbableItemsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.EnemiesAbsorbable = allowAbsorbableItemsCheckbox.Checked;
            absorbableItemsChecklist.Enabled = config.OptionFlags.EnemiesAbsorbable;
            absorbableItemsSpawnrateTrackbar.Enabled = config.OptionFlags.EnemiesAbsorbable;
        }

        private void absorbableItemsSpawnrateTrackbar_ValueChanged(object sender, EventArgs e)
        {
            var spawnRate = 5 * absorbableItemsSpawnrateTrackbar.Value;
            spawnrateLabel.Text = spawnRate.ToString("D2") + "%";

            config.OptionFlags.AbsorbableSpawnRate = spawnRate;
        }

        private void absorbableItemsChecklist_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var selectedItem = absorbableItemsChecklist.Items[e.Index].ToString();
            var type = EnumEx.GetValueFromDescription<AbsorbableTypes>(selectedItem);
            var isSet = (e.NewValue == CheckState.Checked);
            config.OptionFlags.AbsorbableTypes[type] = isSet;
        }


        /*
         * Bosses tab
         */
        private void randomizeBossesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeBosses = randomizeBossesCheckbox.Checked;
            bossRandomizationTypesTrackbar.Enabled = config.OptionFlags.RandomizeBosses;
        }

        private void bossRandomizationTypesTrackbar_ValueChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeBossesType = (RandomizeBossesType)bossRandomizationTypesTrackbar.Value;
            typebossLabel.Text = ((RandomizeBossesType)bossRandomizationTypesTrackbar.Value).ToString();
        }

        private void randomizeBossHealthCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeBossHealth = randomizeBossHealthCheckbox.Checked;
            bossHealthRandomizationTrackbar.Enabled = config.OptionFlags.RandomizeBossHealth;
        }

        private void bossHealthRandomizationTrackbar_ValueChanged(object sender, EventArgs e)
        {
            var bosshealthMin = 100 - (5 * bossHealthRandomizationTrackbar.Value);
            var bosshealthMax = (100 + (10 * bossHealthRandomizationTrackbar.Value));
            bosshealthLabel.Text = bosshealthMin.ToString("D2") + "% - " + bosshealthMax.ToString("D2") + "%";

            config.OptionFlags.RandomizeBossHealthMinAmount = bosshealthMin;
            config.OptionFlags.RandomizeBossHealthMaxAmount = bosshealthMax;
        }

        private void randomizeBossDamageCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeBossDamage = randomizeBossDamageCheckbox.Checked;
            bossDamageRandomizationTrackbar.Enabled = config.OptionFlags.RandomizeBossDamage;
        }

        private void bossDamageRandomizationTrackbar_ValueChanged(object sender, EventArgs e)
        {
            var bossdamageMin = 100 - (5 * bossDamageRandomizationTrackbar.Value);
            var bossdamageMax = (100 + (5 * bossDamageRandomizationTrackbar.Value));
            bossdamageLabel.Text = bossdamageMin.ToString("D2") + "% - " + bossdamageMax.ToString("D2") + "%";

            config.OptionFlags.RandomizeBossDamageMinAmount = bossdamageMin;
            config.OptionFlags.RandomizeBossDamageMaxAmount = bossdamageMax;
        }

        private void randomizeBossBehaviorCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeBossBehavior = randomizeBossBehaviorCheckbox.Checked;
        }


        /*
         * Palettes tab
         */
        private void randomizeDungeonPalettesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeDungeonPalettes = randomizeDungeonPalettesCheckbox.Checked;
            setBlackoutModeCheckbox.Enabled = config.OptionFlags.RandomizeDungeonPalettes;
        }

        private void setBlackoutModeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.SetBlackoutMode = setBlackoutModeCheckbox.Checked;
        }

        private void randomizeOverworldPalettesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeOverworldPalettes = randomizeOverworldPalettesCheckbox.Checked;
        }

        private void randomizeSpritePalettesBasicCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeSpritePalettes = randomizeSpritePalettesBasicCheckbox.Checked;
            randomizeSpritePalettesAdvancedCheckbox.Enabled = config.OptionFlags.RandomizeSpritePalettes;
        }

        private void randomizeSpritePalettesAdvancedCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.SetAdvancedSpritePalettes = randomizeSpritePalettesAdvancedCheckbox.Checked;
        }


        /*
         * Extras tab
         */
        private void extraSettingsCheckedList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // TODO: add extra flags
        }


        // TODO: replace this with resource file and tooltips
        public string[] description = 
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

        private void chkBootlegMagic_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.BootlegMagic = chkBootlegMagic.Checked;
        }
    }



}
