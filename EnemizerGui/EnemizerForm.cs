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
        //OptionFlags optionFlags = new OptionFlags();
        Random rand = new Random();

        public EnemizerForm()
        {
            InitializeComponent();
        }

        private void EnemizerForm_Load(object sender, EventArgs e)
        {
            //panel1.BackColor = Color.FromArgb(255, 128, 192);
            this.Text = "Enemizer " + EnemizerLibrary.Version.CurrentVersion;

            LoadAbsorbablesListBox();

            LoadSpriteDropdown();
            LoadSwordDropdown();
            LoadShieldDropdown();
            LoadBeeMizerDropdown();

            LoadDebugEnemyListBox();
            LoadDebugBossListBox();

            LoadLanguageDropdown();

            tabControl1.TabPages.Remove(tabDebug); // remove it for now

            LoadConfig();
            UpdateUIFromConfig();
            CheckForUpdates();
        }

        private void LoadLanguageDropdown()
        {
            uiLanguageCombobox.Items.Clear();
            uiLanguageCombobox.Items.Add("English");
            uiLanguageCombobox.SelectedIndex = 0;
        }

        private void ChangeUILangauge()
        {

        }

        private void EnemizerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig();
        }

        private bool CheckForUpdates()
        {
            if (config.CheckForUpdates && EnemizerLibrary.Version.CheckUpdate() == true)
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
                MessageBox.Show("No update available", "Enemizer");
                //noupdate
            }
        }

        private void LoadSpriteDropdown()
        {
            linkSpriteCombobox.Items.Add(new files_names("Unchanged", "Unchanged"));
            linkSpriteCombobox.Items.Add(new files_names("Random", "Random"));
            linkSpriteCombobox.SelectedIndex = 0;

            foreach (string f in Directory.GetFiles("sprites\\"))
            {
                files_names item = new files_names(Path.GetFileNameWithoutExtension(f), f);
                linkSpriteCombobox.Items.Add(item);
            }
        }

        void LoadSwordDropdown()
        {
            swordGraphicsCombobox.Items.Clear();
            //foreach (var e in Enum.GetValues(typeof(SwordTypes)))
            //{
            //    swordGraphicsCombobox.Items.Add(((SwordTypes)e).GetDescription());
            //}

            foreach (string f in Directory.GetFiles("sword_gfx\\"))
            {
                files_names item = new files_names(Path.GetFileNameWithoutExtension(f), f);
                swordGraphicsCombobox.Items.Add(item);
            }
        }

        void LoadShieldDropdown()
        {
            shieldSpriteCombobox.Items.Clear();
            //foreach(var e in Enum.GetValues(typeof(ShieldTypes)))
            //{
            //    shieldSpriteCombobox.Items.Add(((ShieldTypes)e).GetDescription());
            //}

            foreach (string f in Directory.GetFiles("shield_gfx\\"))
            {
                files_names item = new files_names(Path.GetFileNameWithoutExtension(f), f);
                shieldSpriteCombobox.Items.Add(item);
            }
        }

        void LoadBeeMizerDropdown()
        {
            beeLevelComboBox.Items.Clear();
            foreach (var e in Enum.GetValues(typeof(BeeLevel)))
            {
                beeLevelComboBox.Items.Add(((BeeLevel)e).GetDescription());
            }
        }

        void LoadAbsorbablesListBox()
        {
            absorbableItemsChecklist.Items.Clear();
            foreach(var e in Enum.GetValues(typeof(AbsorbableTypes)))
            {
                absorbableItemsChecklist.Items.Add(((AbsorbableTypes)e).GetDescription());
            }
        }

        void LoadDebugEnemyListBox()
        {
            debugForceEnemyComboBox.Items.Clear();
            debugForceEnemyComboBox.DataSource = EnemizerLibrary.SpriteConstants.spriteNames
                    .Where(x => EnemizerLibrary.SpriteConstants.DebugUsableSprites.Contains(x.Key))
                    .OrderBy(x => x.Value)
                    .ToList();
            debugForceEnemyComboBox.ValueMember = "Key";
            debugForceEnemyComboBox.DisplayMember = "Value";
        }

        void LoadDebugBossListBox()
        {
            var bossTypes = new Dictionary<int, string>();
            bossTypes.Add((int)BossType.Armos, "Armos");
            bossTypes.Add((int)BossType.Lanmola, "Lanmola");
            bossTypes.Add((int)BossType.Moldorm, "Moldorm");
            bossTypes.Add((int)BossType.Helmasaur, "Helmasaur King");
            bossTypes.Add((int)BossType.Arrghus, "Arrghus");
            bossTypes.Add((int)BossType.Mothula, "Mothula");
            bossTypes.Add((int)BossType.Blind, "Blind");
            bossTypes.Add((int)BossType.Kholdstare, "Kholdstare");
            bossTypes.Add((int)BossType.Vitreous, "Vitreous");
            bossTypes.Add((int)BossType.Trinexx, "Trinexx");

            debugForceBossComboBox.Items.Clear();
            debugForceBossComboBox.DataSource = bossTypes.ToList();
            debugForceBossComboBox.ValueMember = "Key";
            debugForceBossComboBox.DisplayMember = "Value";
        }

        private bool LoadConfig()
        {
            var configFullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Enemizer", configFilename);

            try
            {
                if (File.Exists(configFullPath))
                {
                    config = JsonConvert.DeserializeObject<EnemizerConfig>(File.ReadAllText(configFullPath));
                    if(config != null)
                    {
                        return true;
                    }
                }
            }
            catch
            {
                // invalid file
                MessageBox.Show("Invalid setting file. Loading defaults.", "Enemizer");
            }

            config = new EnemizerConfig();
            return false;
        }

        private void SaveConfig()
        {
            var configJson = JsonConvert.SerializeObject(config, Formatting.Indented);
            // make sure the folder exists and create it
            var configPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Enemizer");
            Directory.CreateDirectory(configPath);

            // write the config file
            var configFullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Enemizer", configFilename);
            File.WriteAllText(configFullPath, configJson);
        }


        public void UpdateUIFromConfig()
        {
            // TODO: update anything else
            UpdateMainFormUIFromConfig();

            UpdateEnemiesTabUIFromConfig();

            UpdateBossesTabUIFromConfig();

            UpdatePalettesTabUIFromConfig();

            UpdateExtrasTabUIFromConfig();

            UpdateGraphicsTabUIFromConfig();

            UpdateDebugTabUIFromConfig();
        }

        private void UpdateMainFormUIFromConfig()
        {
            checkForUpdatesCheckbox.Checked = config.CheckForUpdates;

            randomizeLinksPaletteCheckbox.Checked = config.OptionFlags.RandomizeLinkSpritePalette;

            generateSpoilerCheckbox.Checked = config.OptionFlags.GenerateSpoilers;

            bulkSeedsCheckbox.Checked = config.BulkSeeds;
            numberOfBulkSeeds.Enabled = config.BulkSeeds;
            seedNumberTextbox.Enabled = !config.BulkSeeds;
            numberOfBulkSeeds.Value = config.NumberOfBulkSeeds;
        }

        private void UpdateEnemiesTabUIFromConfig()
        {
            randomizeEnemiesCheckbox.Checked = config.OptionFlags.RandomizeEnemies;
            //randomizationTypeTrackbar.Enabled = config.OptionFlags.RandomizeEnemies;
            randomizationTypeLabel.Enabled = randomizationTypeTrackbar.Enabled;
            lblTypeOfRandomization.Enabled = randomizationTypeTrackbar.Enabled;
            chkRandomizeBushEnemyChance.Enabled = config.OptionFlags.RandomizeEnemies;

            randomizationTypeTrackbar.Value = (int)config.OptionFlags.RandomizeEnemiesType;
            randomizationTypeLabel.Text = ((RandomizeEnemiesType)randomizationTypeTrackbar.Value).ToString();

            chkRandomizeBushEnemyChance.Checked = config.OptionFlags.RandomizeBushEnemyChance;

            randomizeEnemiesHealthCheckbox.Checked = config.OptionFlags.RandomizeEnemyHealthRange;
            randomizeEnemiesHealthTrackbar.Enabled = config.OptionFlags.RandomizeEnemyHealthRange;

            if((int)config.OptionFlags.RandomizeEnemyHealthType > randomizeEnemiesHealthTrackbar.Maximum)
            {
                config.OptionFlags.RandomizeEnemyHealthType = RandomizeEnemyHPType.Easy;
            }
            randomizeEnemiesHealthTrackbar.Value = (int)config.OptionFlags.RandomizeEnemyHealthType;
            healthLabel.Text = ((RandomizeEnemyHPType)randomizeEnemiesHealthTrackbar.Value).ToString();

            randomizeEnemiesDamageCheckbox.Checked = config.OptionFlags.RandomizeEnemyDamage;
            allowZeroDamageCheckbox.Enabled = config.OptionFlags.RandomizeEnemyDamage;
            shuffleEnemyDamageGroupsCheckbox.Enabled = config.OptionFlags.RandomizeEnemyDamage;
            enemyDamageChaosModeCheckbox.Enabled = config.OptionFlags.RandomizeEnemyDamage && shuffleEnemyDamageGroupsCheckbox.Checked;

            allowZeroDamageCheckbox.Checked = config.OptionFlags.AllowEnemyZeroDamage;

            shuffleEnemyDamageGroupsCheckbox.Checked = config.OptionFlags.ShuffleEnemyDamageGroups;

            enemyDamageChaosModeCheckbox.Checked = config.OptionFlags.EnemyDamageChaosMode;

            easyModeEscapeCheckbox.Checked = config.OptionFlags.EasyModeEscape;

            allowAbsorbableItemsCheckbox.Checked = config.OptionFlags.EnemiesAbsorbable;
            // TODO: hook these up in the randomizer
            absorbableItemsChecklist.Enabled = config.OptionFlags.EnemiesAbsorbable; 
            absorbableItemsSpawnrateTrackbar.Enabled = config.OptionFlags.EnemiesAbsorbable;
            lblAbsorbSpawnRate.Enabled = absorbableItemsSpawnrateTrackbar.Enabled;
            spawnrateLabel.Enabled = absorbableItemsSpawnrateTrackbar.Enabled;

            absorbableItemsSpawnrateTrackbar.Value = config.OptionFlags.AbsorbableSpawnRate / 5; // TODO: don't hardcode magic numbers
            spawnrateLabel.Text = $"{config.OptionFlags.AbsorbableSpawnRate}%";

            LoadAbsorbableItemsChecklistFromConfig();

            ohkoCheckBox.Checked = config.OptionFlags.OHKO;

            randomizeEnemiesDamageCheckbox.Enabled = !config.OptionFlags.OHKO;
            allowZeroDamageCheckbox.Enabled = !config.OptionFlags.OHKO;
            shuffleEnemyDamageGroupsCheckbox.Enabled = config.OptionFlags.RandomizeEnemyDamage && !config.OptionFlags.OHKO;
            enemyDamageChaosModeCheckbox.Enabled = config.OptionFlags.RandomizeEnemyDamage && shuffleEnemyDamageGroupsCheckbox.Checked && !config.OptionFlags.OHKO;
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

            pukeModeCheckbox.Checked = config.OptionFlags.PukeMode;
            randomizeDungeonPalettesCheckbox.Enabled = !pukeModeCheckbox.Checked;
            setBlackoutModeCheckbox.Enabled = !pukeModeCheckbox.Checked;
            //randomizeOverworldPalettesCheckbox.Enabled = !pukeModeCheckbox.Checked;
            //randomizeSpritePalettesBasicCheckbox.Enabled = !pukeModeCheckbox.Checked;
            //randomizeSpritePalettesAdvancedCheckbox.Enabled = !pukeModeCheckbox.Checked;
        }

        private void UpdateExtrasTabUIFromConfig()
        {
            bootlegMagicCheckbox.Checked = config.OptionFlags.BootlegMagic;
            debugModeCheckbox.Checked = config.OptionFlags.DebugMode;
            shuffleMusicCheckBox.Checked = config.OptionFlags.ShuffleMusic;
            shufflePotContentsCheckbox.Checked = config.OptionFlags.RandomizePots;
            customBossesCheckbox.Checked = config.OptionFlags.CustomBosses;
            andyModeCheckbox.Checked = config.OptionFlags.AndyMode;
            heartBeepSpeedTrackbar.Value = (int)config.OptionFlags.HeartBeepSpeed;
            SetHeartBeepSpeedText(config.OptionFlags.HeartBeepSpeed);
            alternateGfxCheckbox.Checked = config.OptionFlags.AlternateGfx;
            pukeModeCheckbox.Checked = config.OptionFlags.PukeMode;
            grayscaleModeCheckbox.Checked = config.OptionFlags.GrayscaleMode;
            negativeModeCheckbox.Checked = config.OptionFlags.NegativeMode;

            beeMizerCheckBox.Checked = config.OptionFlags.BeeMizer;
            beeLevelComboBox.Enabled = config.OptionFlags.BeeMizer;
            beeLevelComboBox.SelectedIndex = (int)config.OptionFlags.BeesLevel;

            randomizeTileTrapPatternCheckBox.Checked = config.OptionFlags.RandomizeTileTrapPattern;
            randomizeTileTrapFloorTile.Checked = config.OptionFlags.RandomizeTileTrapFloorTile;

            allowKillableThiefCheckBox.Checked = config.OptionFlags.AllowKillableThief;
        }

        private void UpdateGraphicsTabUIFromConfig()
        {
            
            var swords = swordGraphicsCombobox.Items.Cast<files_names>();
            if(false == swords.Any(x => x.file == config.OptionFlags.SwordGraphics))
            {
                config.OptionFlags.SwordGraphics = "sword_gfx\\normal.gfx";
            }
            swordGraphicsCombobox.SelectedIndex = swordGraphicsCombobox.Items.IndexOf(swords.FirstOrDefault(x => x.file == config.OptionFlags.SwordGraphics));

            var shields = shieldSpriteCombobox.Items.Cast<files_names>();
            if (false == shields.Any(x => x.file == config.OptionFlags.ShieldGraphics))
            {
                config.OptionFlags.ShieldGraphics = "shield_gfx\\normal.gfx";
            }
            shieldSpriteCombobox.SelectedIndex = shieldSpriteCombobox.Items.IndexOf(shields.FirstOrDefault(x => x.file == config.OptionFlags.ShieldGraphics));
        }

        private void UpdateDebugTabUIFromConfig()
        {
            debugForceEnemyCheckBox.Checked = config.OptionFlags.DebugForceEnemy;
            debugForceEnemyComboBox.Enabled = config.OptionFlags.DebugForceEnemy;
            //var forcedEnemy = ((List<KeyValuePair<int, string>>)(debugForceEnemyComboBox.DataSource)).FirstOrDefault(x => x.Key == config.OptionFlags.DebugForceEnemyId);
            debugForceEnemyComboBox.SelectedValue = config.OptionFlags.DebugForceEnemyId;

            debugForceBossCheckBox.Checked = config.OptionFlags.DebugForceBoss;
            debugForceBossComboBox.Enabled = config.OptionFlags.DebugForceBoss;
            //var forcedBoss = ((List<KeyValuePair<int, string>>)(debugForceBossComboBox.DataSource)).FirstOrDefault(x => x.Key == (int)config.OptionFlags.DebugForceBossId);
            debugForceBossComboBox.SelectedValue = (int)config.OptionFlags.DebugForceBossId;

            debugDisableShutterDoorsCheckBox.Checked = config.OptionFlags.DebugOpenShutterDoors;
            debugSetEnemyDamageZero.Checked = config.OptionFlags.DebugForceEnemyDamageZero;
            debugShowRoomIdInRupeeCounterCheckBox.Checked = config.OptionFlags.DebugShowRoomIdInRupeeCounter;
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

        private void generateRomButton_Click(object sender, EventArgs e)
        {
            var oldCursor = this.Cursor;
#if !DEBUG
            try
            {
#endif
            int seed = 0;

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
                RomData romData = new RomData(rom_data);
                if (romData.IsEnemizerRom)
                {
                    if (DialogResult.No == MessageBox.Show("Enemizer rom detected: this will cause Enemizer to try to reset the rom and rerun the same settings that are embedded in the rom. This feature exists for debugging purposes only. If you think this is a mistake, please double check the input file you selected. Do you wish to continue?", "Enemizer rom detected", MessageBoxButtons.YesNo))
                    {
                        // user picked no
                        return;
                    }
                }

                using (var fbd = new FolderBrowserDialog())
                {
                    fbd.Description = "Select Enemizer Destination Folder";
                    if (!String.IsNullOrEmpty(config.DefaultFolder) && Directory.Exists(config.DefaultFolder))
                    {
                        fbd.SelectedPath = config.DefaultFolder;
                    }
                    else
                    {
                        fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    }

                    var result = fbd.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.DefaultFolder = fbd.SelectedPath;

                        this.Cursor = Cursors.WaitCursor;

                        if (this.bulkSeedsCheckbox.Checked)
                        {
                            int totalSeeds = 0;
                            var sb = new StringBuilder();
                            if (Int32.TryParse(this.numberOfBulkSeeds.Text, out totalSeeds))
                            {
                                sb.AppendLine($"{totalSeeds} seeds created:");
                                for (int i = 0; i < totalSeeds; i++)
                                {
                                    seed = rand.Next(0, 999999999);

                                    var fileName = GenerateSeed(seed, rom_data, linkSpriteFilename, ofd.FileName, fbd.SelectedPath);
                                    sb.Append(fileName + (i == totalSeeds ? "" : ", "));
                                }
                                MessageBox.Show(sb.ToString(), "Bulk Enemizer Roms Created");
                            }
                            else
                            {
                                MessageBox.Show("Invalid bulk seed number");
                            }
                        }
                        else
                        {

                            if (String.IsNullOrEmpty(seedNumberTextbox.Text))
                            {
                                seed = rand.Next(0, 999999999);
                            }
                            else
                            {
                                // TODO: add validation to the textbox so it can't be anything but a number
                                if (!int.TryParse(seedNumberTextbox.Text, out seed))
                                {
                                    MessageBox.Show("Invalid Seed Number entered. Please enter an integer value.", "Enemizer");
                                    return;
                                }
                                if (seed < 0)
                                {
                                    MessageBox.Show("Please enter a positive Seed Number.", "Enemizer");
                                    return;
                                }
                            }

                            var fileName = GenerateSeed(seed, rom_data, linkSpriteFilename, ofd.FileName, fbd.SelectedPath);

                            MessageBox.Show($"{fileName} has been created!", "Enemizer Rom Created");
                        }
                    }
                }
            }
#if !DEBUG
            }
            finally
            {
                this.Cursor = oldCursor;
            }
#endif
            this.Cursor = oldCursor;

        }

        string GenerateSeed(int seed, byte[] rom_data, string linkSpriteFilename, string inputFilename, string outputPath)
        {

#if !DEBUG
            try // make sure we don't crash in release build
            {
#endif

            RomData romData = new RomData(rom_data);
            Randomization randomize = new Randomization();
            RomData randomizedRom = randomize.MakeRandomization(seed, config.OptionFlags, romData, linkSpriteFilename);

            string fileNameNoExtension = Path.Combine(outputPath, $"Enemizer {EnemizerLibrary.Version.CurrentVersion} - {Path.GetFileNameWithoutExtension(inputFilename)} (EN{randomizedRom.EnemizerSeed})");

            if (config.OptionFlags.GenerateSpoilers)
            {
                File.WriteAllText($"{fileNameNoExtension}.txt", randomizedRom.Spoiler.ToString());
            }
            string fileName = $"{fileNameNoExtension}.sfc";
            var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            randomizedRom.WriteRom(fs);
            fs.Close();

            return fileName;

#if !DEBUG
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Enemizer");
            }

            return "Failed";
#endif
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
            //randomizationTypeTrackbar.Enabled = config.OptionFlags.RandomizeEnemies;
            randomizationTypeLabel.Enabled = randomizationTypeTrackbar.Enabled;
            lblTypeOfRandomization.Enabled = randomizationTypeTrackbar.Enabled;
            chkRandomizeBushEnemyChance.Enabled = config.OptionFlags.RandomizeEnemies;
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
            //var healthMin = (randomizeEnemiesHealthTrackbar.Value);
            //healthLabel.Text = $"±{healthMin}";
            //config.OptionFlags.RandomizeEnemyHealthType = healthMin;
            config.OptionFlags.RandomizeEnemyHealthType = (RandomizeEnemyHPType)randomizeEnemiesHealthTrackbar.Value;
            healthLabel.Text = ((RandomizeEnemyHPType)randomizeEnemiesHealthTrackbar.Value).ToString();
        }

        private void randomizeEnemiesDamageCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeEnemyDamage = randomizeEnemiesDamageCheckbox.Checked;
            allowZeroDamageCheckbox.Enabled = config.OptionFlags.RandomizeEnemyDamage;
            shuffleEnemyDamageGroupsCheckbox.Enabled = config.OptionFlags.RandomizeEnemyDamage;
            enemyDamageChaosModeCheckbox.Enabled = config.OptionFlags.RandomizeEnemyDamage && shuffleEnemyDamageGroupsCheckbox.Checked;
        }

        private void allowZeroDamageCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.AllowEnemyZeroDamage = allowZeroDamageCheckbox.Checked;
        }

        private void shuffleEnemyDamageGroupsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.ShuffleEnemyDamageGroups = shuffleEnemyDamageGroupsCheckbox.Checked;
            enemyDamageChaosModeCheckbox.Enabled = config.OptionFlags.RandomizeEnemyDamage && shuffleEnemyDamageGroupsCheckbox.Checked;
        }

        private void enemyDamageChaosModeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.EnemyDamageChaosMode = enemyDamageChaosModeCheckbox.Checked;
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
            lblAbsorbSpawnRate.Enabled = absorbableItemsSpawnrateTrackbar.Enabled;
            spawnrateLabel.Enabled = absorbableItemsSpawnrateTrackbar.Enabled;
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

        private void pukeModeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.PukeMode = pukeModeCheckbox.Checked;
            randomizeDungeonPalettesCheckbox.Enabled = !pukeModeCheckbox.Checked;
            setBlackoutModeCheckbox.Enabled = !pukeModeCheckbox.Checked;
            //randomizeOverworldPalettesCheckbox.Enabled = !pukeModeCheckbox.Checked;
            //randomizeSpritePalettesBasicCheckbox.Enabled = !pukeModeCheckbox.Checked;
            //randomizeSpritePalettesAdvancedCheckbox.Enabled = !pukeModeCheckbox.Checked;
        }

        /*
         * Extras tab
         */

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


        private void chkBootlegMagic_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.BootlegMagic = bootlegMagicCheckbox.Checked;
        }

        private void debugModeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.DebugMode = debugModeCheckbox.Checked;
            if (config.OptionFlags.DebugMode)
            {
                tabControl1.TabPages.Add(tabDebug);
            }
            else
            {
                tabControl1.TabPages.Remove(tabDebug);
            }
        }

        private void shuffleMusicCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.ShuffleMusic = shuffleMusicCheckBox.Checked;
        }

        private void shufflePotContentsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizePots = shufflePotContentsCheckbox.Checked;
        }

        private void customBossesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.CustomBosses = customBossesCheckbox.Checked;
        }

        private void andyModeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.AndyMode = andyModeCheckbox.Checked;
        }
        private void heartBeepSpeedTrackbar_Scroll(object sender, EventArgs e)
        {
            var beepSpeed = (HeartBeepSpeed)heartBeepSpeedTrackbar.Value;
            config.OptionFlags.HeartBeepSpeed = beepSpeed;
            SetHeartBeepSpeedText(beepSpeed);
        }

        void SetHeartBeepSpeedText(HeartBeepSpeed heartBeepSpeed)
        {
            heartBeepSpeedLabel.Text = heartBeepSpeed.ToString();
        }

        private void alternateGfxCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.AlternateGfx = alternateGfxCheckbox.Checked;
        }

        private void grayscaleModeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.GrayscaleMode = grayscaleModeCheckbox.Checked;
        }

        private void negativeModeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.NegativeMode = negativeModeCheckbox.Checked;
        }

        /*
         * Graphics tab
         */

        private void swordGraphicsCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            config.OptionFlags.SwordGraphics = (swordGraphicsCombobox.SelectedItem as files_names).file.ToString();
        }

        private void shieldSpriteCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            config.OptionFlags.ShieldGraphics = (shieldSpriteCombobox.SelectedItem as files_names).file.ToString();
        }

        private void completeModificationCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TODO: wire this up
        }

        private void bulkSeedsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this.numberOfBulkSeeds.Enabled = bulkSeedsCheckbox.Checked;
            this.seedNumberTextbox.Enabled = !bulkSeedsCheckbox.Checked;
        }

        private void beeMizerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.BeeMizer = beeMizerCheckBox.Checked;
            beeLevelComboBox.Enabled = config.OptionFlags.BeeMizer;
        }

        private void beeLevelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            config.OptionFlags.BeesLevel = (BeeLevel)beeLevelComboBox.SelectedIndex;
        }

        private void debugForceEnemyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.DebugForceEnemy = debugForceEnemyCheckBox.Checked;
            debugForceEnemyComboBox.Enabled = config.OptionFlags.DebugForceEnemy;
        }

        private void debugForceEnemyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            config.OptionFlags.DebugForceEnemyId = ((KeyValuePair<int, string>)(debugForceEnemyComboBox.SelectedItem)).Key;
        }

        private void debugForceBossCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.DebugForceBoss = debugForceBossCheckBox.Checked;
            debugForceBossComboBox.Enabled = config.OptionFlags.DebugForceBoss;
        }

        private void debugForceBossComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            config.OptionFlags.DebugForceBossId = (BossType)((KeyValuePair<int, string>)(debugForceBossComboBox.SelectedItem)).Key;
        }

        private void debugDisableShutterDoorsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.DebugOpenShutterDoors = debugDisableShutterDoorsCheckBox.Checked;
        }

        private void debugSetEnemyDamageZero_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.DebugForceEnemyDamageZero = debugSetEnemyDamageZero.Checked;
        }

        private void debugShowRoomIdInRupeeCounterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.DebugShowRoomIdInRupeeCounter = debugShowRoomIdInRupeeCounterCheckBox.Checked;
        }

        private void ohkoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.OHKO = ohkoCheckBox.Checked;

            randomizeEnemiesDamageCheckbox.Enabled = !config.OptionFlags.OHKO;
            allowZeroDamageCheckbox.Enabled = !config.OptionFlags.OHKO;
            shuffleEnemyDamageGroupsCheckbox.Enabled = config.OptionFlags.RandomizeEnemyDamage && !config.OptionFlags.OHKO;
            enemyDamageChaosModeCheckbox.Enabled = config.OptionFlags.RandomizeEnemyDamage && shuffleEnemyDamageGroupsCheckbox.Checked && !config.OptionFlags.OHKO;
        }

        private void randomizeTileTrapPatternCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeTileTrapPattern = randomizeTileTrapPatternCheckBox.Checked;
        }

        private void randomizeTileTrapFloorTile_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.RandomizeTileTrapFloorTile = randomizeTileTrapFloorTile.Checked;
        }

        private void allowKillableThiefCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            config.OptionFlags.AllowKillableThief = allowKillableThiefCheckBox.Checked;
        }
    }



}
