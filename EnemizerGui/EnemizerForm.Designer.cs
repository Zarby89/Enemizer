namespace Enemizer
{
    partial class EnemizerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnemizerForm));
            this.generateRomButton = new System.Windows.Forms.Button();
            this.linkSpriteCombobox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.weaponSpriteCombobox = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.generateSpoilerCheckbox = new System.Windows.Forms.CheckBox();
            this.completeModificationCombobox = new System.Windows.Forms.ComboBox();
            this.checkForUpdatesButton = new System.Windows.Forms.Button();
            this.checkForUpdatesCheckbox = new System.Windows.Forms.CheckBox();
            this.linkSpritePicturebox = new System.Windows.Forms.PictureBox();
            this.randomizeLinksPaletteCheckbox = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkRandomizeBushEnemyChance = new System.Windows.Forms.CheckBox();
            this.allowZeroDamageCheckbox = new System.Windows.Forms.CheckBox();
            this.absorbableItemsChecklist = new System.Windows.Forms.CheckedListBox();
            this.spawnrateLabel = new System.Windows.Forms.Label();
            this.lblAbsorbSpawnRate = new System.Windows.Forms.Label();
            this.absorbableItemsSpawnrateTrackbar = new System.Windows.Forms.TrackBar();
            this.allowAbsorbableItemsCheckbox = new System.Windows.Forms.CheckBox();
            this.easyModeEscapeCheckbox = new System.Windows.Forms.CheckBox();
            this.randomizeEnemiesDamageCheckbox = new System.Windows.Forms.CheckBox();
            this.healthLabel = new System.Windows.Forms.Label();
            this.randomizeEnemiesHealthTrackbar = new System.Windows.Forms.TrackBar();
            this.randomizeEnemiesHealthCheckbox = new System.Windows.Forms.CheckBox();
            this.randomizationTypeLabel = new System.Windows.Forms.Label();
            this.lblTypeOfRandomization = new System.Windows.Forms.Label();
            this.randomizationTypeTrackbar = new System.Windows.Forms.TrackBar();
            this.randomizeEnemiesCheckbox = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.randomizeBossBehaviorCheckbox = new System.Windows.Forms.CheckBox();
            this.bossdamageLabel = new System.Windows.Forms.Label();
            this.bossDamageRandomizationTrackbar = new System.Windows.Forms.TrackBar();
            this.randomizeBossDamageCheckbox = new System.Windows.Forms.CheckBox();
            this.bosshealthLabel = new System.Windows.Forms.Label();
            this.bossHealthRandomizationTrackbar = new System.Windows.Forms.TrackBar();
            this.randomizeBossHealthCheckbox = new System.Windows.Forms.CheckBox();
            this.typebossLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.bossRandomizationTypesTrackbar = new System.Windows.Forms.TrackBar();
            this.randomizeBossesCheckbox = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.pukeModeCheckbox = new System.Windows.Forms.CheckBox();
            this.setBlackoutModeCheckbox = new System.Windows.Forms.CheckBox();
            this.randomizeSpritePalettesAdvancedCheckbox = new System.Windows.Forms.CheckBox();
            this.randomizeSpritePalettesBasicCheckbox = new System.Windows.Forms.CheckBox();
            this.randomizeOverworldPalettesCheckbox = new System.Windows.Forms.CheckBox();
            this.randomizeDungeonPalettesCheckbox = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.andyModeCheckbox = new System.Windows.Forms.CheckBox();
            this.customBossesCheckbox = new System.Windows.Forms.CheckBox();
            this.shufflePotContentsCheckbox = new System.Windows.Forms.CheckBox();
            this.shuffleMusicCheckBox = new System.Windows.Forms.CheckBox();
            this.debugModeCheckbox = new System.Windows.Forms.CheckBox();
            this.bootlegMagicCheckbox = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.seedNumberTextbox = new System.Windows.Forms.TextBox();
            this.alternateGfxCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.linkSpritePicturebox)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.absorbableItemsSpawnrateTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.randomizeEnemiesHealthTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.randomizationTypeTrackbar)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bossDamageRandomizationTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bossHealthRandomizationTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bossRandomizationTypesTrackbar)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // generateRomButton
            // 
            this.generateRomButton.Location = new System.Drawing.Point(486, 278);
            this.generateRomButton.Name = "generateRomButton";
            this.generateRomButton.Size = new System.Drawing.Size(122, 23);
            this.generateRomButton.TabIndex = 0;
            this.generateRomButton.Text = "Generate!";
            this.generateRomButton.UseVisualStyleBackColor = true;
            this.generateRomButton.Click += new System.EventHandler(this.generateRomButton_Click);
            // 
            // linkSpriteCombobox
            // 
            this.linkSpriteCombobox.FormattingEnabled = true;
            this.linkSpriteCombobox.Location = new System.Drawing.Point(487, 64);
            this.linkSpriteCombobox.Name = "linkSpriteCombobox";
            this.linkSpriteCombobox.Size = new System.Drawing.Size(128, 21);
            this.linkSpriteCombobox.TabIndex = 8;
            this.linkSpriteCombobox.SelectedIndexChanged += new System.EventHandler(this.linkSpriteCombobox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(484, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Link Gfx :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(484, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Weapon Gfx - WIP :";
            // 
            // weaponSpriteCombobox
            // 
            this.weaponSpriteCombobox.Enabled = false;
            this.weaponSpriteCombobox.FormattingEnabled = true;
            this.weaponSpriteCombobox.Items.AddRange(new object[] {
            "Normal Sword",
            "Mace"});
            this.weaponSpriteCombobox.Location = new System.Drawing.Point(487, 178);
            this.weaponSpriteCombobox.Name = "weaponSpriteCombobox";
            this.weaponSpriteCombobox.Size = new System.Drawing.Size(121, 21);
            this.weaponSpriteCombobox.TabIndex = 11;
            this.weaponSpriteCombobox.SelectedIndexChanged += new System.EventHandler(this.weaponSpriteCombobox_SelectedIndexChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(684, 19);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(68, 293);
            this.richTextBox1.TabIndex = 14;
            this.richTextBox1.Text = "";
            this.richTextBox1.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(484, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Complete Modification (Gfx)";
            // 
            // generateSpoilerCheckbox
            // 
            this.generateSpoilerCheckbox.AutoSize = true;
            this.generateSpoilerCheckbox.Location = new System.Drawing.Point(486, 307);
            this.generateSpoilerCheckbox.Name = "generateSpoilerCheckbox";
            this.generateSpoilerCheckbox.Size = new System.Drawing.Size(126, 17);
            this.generateSpoilerCheckbox.TabIndex = 16;
            this.generateSpoilerCheckbox.Text = "Generate Spoiler Log";
            this.generateSpoilerCheckbox.UseVisualStyleBackColor = true;
            this.generateSpoilerCheckbox.CheckedChanged += new System.EventHandler(this.generateSpoilerCheckbox_CheckedChanged);
            // 
            // completeModificationCombobox
            // 
            this.completeModificationCombobox.Enabled = false;
            this.completeModificationCombobox.FormattingEnabled = true;
            this.completeModificationCombobox.Items.AddRange(new object[] {
            "Normal Sword",
            "Mace"});
            this.completeModificationCombobox.Location = new System.Drawing.Point(487, 218);
            this.completeModificationCombobox.Name = "completeModificationCombobox";
            this.completeModificationCombobox.Size = new System.Drawing.Size(121, 21);
            this.completeModificationCombobox.TabIndex = 17;
            this.completeModificationCombobox.SelectedIndexChanged += new System.EventHandler(this.completeModificationCombobox_SelectedIndexChanged);
            // 
            // checkForUpdatesButton
            // 
            this.checkForUpdatesButton.Location = new System.Drawing.Point(486, 26);
            this.checkForUpdatesButton.Name = "checkForUpdatesButton";
            this.checkForUpdatesButton.Size = new System.Drawing.Size(129, 23);
            this.checkForUpdatesButton.TabIndex = 18;
            this.checkForUpdatesButton.Text = "Check Update";
            this.checkForUpdatesButton.UseVisualStyleBackColor = true;
            this.checkForUpdatesButton.Click += new System.EventHandler(this.checkForUpdatesButton_Click);
            // 
            // checkForUpdatesCheckbox
            // 
            this.checkForUpdatesCheckbox.AutoSize = true;
            this.checkForUpdatesCheckbox.Location = new System.Drawing.Point(487, 7);
            this.checkForUpdatesCheckbox.Name = "checkForUpdatesCheckbox";
            this.checkForUpdatesCheckbox.Size = new System.Drawing.Size(135, 17);
            this.checkForUpdatesCheckbox.TabIndex = 19;
            this.checkForUpdatesCheckbox.Text = "Check Update on Start";
            this.toolTip1.SetToolTip(this.checkForUpdatesCheckbox, "Automatically check if there is a new version of the enemizer");
            this.checkForUpdatesCheckbox.UseVisualStyleBackColor = true;
            this.checkForUpdatesCheckbox.CheckedChanged += new System.EventHandler(this.checkForUpdatesCheckbox_CheckedChanged);
            // 
            // linkSpritePicturebox
            // 
            this.linkSpritePicturebox.Location = new System.Drawing.Point(487, 88);
            this.linkSpritePicturebox.Name = "linkSpritePicturebox";
            this.linkSpritePicturebox.Size = new System.Drawing.Size(128, 48);
            this.linkSpritePicturebox.TabIndex = 20;
            this.linkSpritePicturebox.TabStop = false;
            // 
            // randomizeLinksPaletteCheckbox
            // 
            this.randomizeLinksPaletteCheckbox.AutoSize = true;
            this.randomizeLinksPaletteCheckbox.Location = new System.Drawing.Point(487, 142);
            this.randomizeLinksPaletteCheckbox.Name = "randomizeLinksPaletteCheckbox";
            this.randomizeLinksPaletteCheckbox.Size = new System.Drawing.Size(125, 17);
            this.randomizeLinksPaletteCheckbox.TabIndex = 21;
            this.randomizeLinksPaletteCheckbox.Text = "Random Link Palette";
            this.randomizeLinksPaletteCheckbox.UseVisualStyleBackColor = true;
            this.randomizeLinksPaletteCheckbox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.randomizeLinksPaletteCheckbox_MouseClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(466, 318);
            this.tabControl1.TabIndex = 22;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkRandomizeBushEnemyChance);
            this.tabPage1.Controls.Add(this.allowZeroDamageCheckbox);
            this.tabPage1.Controls.Add(this.absorbableItemsChecklist);
            this.tabPage1.Controls.Add(this.spawnrateLabel);
            this.tabPage1.Controls.Add(this.lblAbsorbSpawnRate);
            this.tabPage1.Controls.Add(this.absorbableItemsSpawnrateTrackbar);
            this.tabPage1.Controls.Add(this.allowAbsorbableItemsCheckbox);
            this.tabPage1.Controls.Add(this.easyModeEscapeCheckbox);
            this.tabPage1.Controls.Add(this.randomizeEnemiesDamageCheckbox);
            this.tabPage1.Controls.Add(this.healthLabel);
            this.tabPage1.Controls.Add(this.randomizeEnemiesHealthTrackbar);
            this.tabPage1.Controls.Add(this.randomizeEnemiesHealthCheckbox);
            this.tabPage1.Controls.Add(this.randomizationTypeLabel);
            this.tabPage1.Controls.Add(this.lblTypeOfRandomization);
            this.tabPage1.Controls.Add(this.randomizationTypeTrackbar);
            this.tabPage1.Controls.Add(this.randomizeEnemiesCheckbox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(458, 292);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Enemies";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkRandomizeBushEnemyChance
            // 
            this.chkRandomizeBushEnemyChance.AutoSize = true;
            this.chkRandomizeBushEnemyChance.Checked = true;
            this.chkRandomizeBushEnemyChance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRandomizeBushEnemyChance.Enabled = false;
            this.chkRandomizeBushEnemyChance.Location = new System.Drawing.Point(21, 80);
            this.chkRandomizeBushEnemyChance.Name = "chkRandomizeBushEnemyChance";
            this.chkRandomizeBushEnemyChance.Size = new System.Drawing.Size(213, 17);
            this.chkRandomizeBushEnemyChance.TabIndex = 17;
            this.chkRandomizeBushEnemyChance.Text = "Randomize Bush/Grass Enemy Chance";
            this.chkRandomizeBushEnemyChance.UseVisualStyleBackColor = true;
            this.chkRandomizeBushEnemyChance.CheckedChanged += new System.EventHandler(this.chkRandomizeBushEnemyChance_CheckedChanged);
            // 
            // allowZeroDamageCheckbox
            // 
            this.allowZeroDamageCheckbox.AutoSize = true;
            this.allowZeroDamageCheckbox.Enabled = false;
            this.allowZeroDamageCheckbox.Location = new System.Drawing.Point(21, 199);
            this.allowZeroDamageCheckbox.Name = "allowZeroDamageCheckbox";
            this.allowZeroDamageCheckbox.Size = new System.Drawing.Size(103, 17);
            this.allowZeroDamageCheckbox.TabIndex = 16;
            this.allowZeroDamageCheckbox.Text = "Allow 0 Damage";
            this.toolTip1.SetToolTip(this.allowZeroDamageCheckbox, "Allow the sprites to do bumper effect just push you back without any damage");
            this.allowZeroDamageCheckbox.UseVisualStyleBackColor = true;
            this.allowZeroDamageCheckbox.CheckedChanged += new System.EventHandler(this.allowZeroDamageCheckbox_CheckedChanged);
            // 
            // absorbableItemsChecklist
            // 
            this.absorbableItemsChecklist.Enabled = false;
            this.absorbableItemsChecklist.FormattingEnabled = true;
            this.absorbableItemsChecklist.Items.AddRange(new object[] {
            "Heart",
            "Green Rupee",
            "Blue Rupee",
            "Red Rupee",
            "Bomb (1)",
            "Bomb (4)",
            "Bomb (8)",
            "Small Magic",
            "Full Maigc",
            "Arrow (5)",
            "Arrow (10)",
            "Fairy",
            "Key",
            "Big Key(Test)"});
            this.absorbableItemsChecklist.Location = new System.Drawing.Point(265, 74);
            this.absorbableItemsChecklist.Name = "absorbableItemsChecklist";
            this.absorbableItemsChecklist.Size = new System.Drawing.Size(140, 214);
            this.absorbableItemsChecklist.TabIndex = 12;
            this.absorbableItemsChecklist.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.absorbableItemsChecklist_ItemCheck);
            // 
            // spawnrateLabel
            // 
            this.spawnrateLabel.AutoSize = true;
            this.spawnrateLabel.Location = new System.Drawing.Point(402, 46);
            this.spawnrateLabel.Name = "spawnrateLabel";
            this.spawnrateLabel.Size = new System.Drawing.Size(27, 13);
            this.spawnrateLabel.TabIndex = 15;
            this.spawnrateLabel.Text = "00%";
            // 
            // lblAbsorbSpawnRate
            // 
            this.lblAbsorbSpawnRate.AutoSize = true;
            this.lblAbsorbSpawnRate.Location = new System.Drawing.Point(253, 26);
            this.lblAbsorbSpawnRate.Name = "lblAbsorbSpawnRate";
            this.lblAbsorbSpawnRate.Size = new System.Drawing.Size(66, 13);
            this.lblAbsorbSpawnRate.TabIndex = 14;
            this.lblAbsorbSpawnRate.Text = "Spawn Rate";
            // 
            // absorbableItemsSpawnrateTrackbar
            // 
            this.absorbableItemsSpawnrateTrackbar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.absorbableItemsSpawnrateTrackbar.Enabled = false;
            this.absorbableItemsSpawnrateTrackbar.Location = new System.Drawing.Point(256, 42);
            this.absorbableItemsSpawnrateTrackbar.Maximum = 20;
            this.absorbableItemsSpawnrateTrackbar.Name = "absorbableItemsSpawnrateTrackbar";
            this.absorbableItemsSpawnrateTrackbar.Size = new System.Drawing.Size(140, 45);
            this.absorbableItemsSpawnrateTrackbar.TabIndex = 13;
            this.toolTip1.SetToolTip(this.absorbableItemsSpawnrateTrackbar, resources.GetString("absorbableItemsSpawnrateTrackbar.ToolTip"));
            this.absorbableItemsSpawnrateTrackbar.ValueChanged += new System.EventHandler(this.absorbableItemsSpawnrateTrackbar_ValueChanged);
            // 
            // allowAbsorbableItemsCheckbox
            // 
            this.allowAbsorbableItemsCheckbox.AutoSize = true;
            this.allowAbsorbableItemsCheckbox.Location = new System.Drawing.Point(240, 6);
            this.allowAbsorbableItemsCheckbox.Name = "allowAbsorbableItemsCheckbox";
            this.allowAbsorbableItemsCheckbox.Size = new System.Drawing.Size(140, 17);
            this.allowAbsorbableItemsCheckbox.TabIndex = 11;
            this.allowAbsorbableItemsCheckbox.Text = "Allow Absorbables Items";
            this.allowAbsorbableItemsCheckbox.UseVisualStyleBackColor = true;
            this.allowAbsorbableItemsCheckbox.CheckedChanged += new System.EventHandler(this.allowAbsorbableItemsCheckbox_CheckedChanged);
            // 
            // easyModeEscapeCheckbox
            // 
            this.easyModeEscapeCheckbox.AutoSize = true;
            this.easyModeEscapeCheckbox.Enabled = false;
            this.easyModeEscapeCheckbox.Location = new System.Drawing.Point(6, 271);
            this.easyModeEscapeCheckbox.Name = "easyModeEscapeCheckbox";
            this.easyModeEscapeCheckbox.Size = new System.Drawing.Size(148, 17);
            this.easyModeEscapeCheckbox.TabIndex = 10;
            this.easyModeEscapeCheckbox.Text = "Easy Mode Escape (WIP)";
            this.toolTip1.SetToolTip(this.easyModeEscapeCheckbox, "No \"Unfair\" enemies will spawn during the escape\r\nfrom link\'s house to sanctuary");
            this.easyModeEscapeCheckbox.UseVisualStyleBackColor = true;
            this.easyModeEscapeCheckbox.CheckedChanged += new System.EventHandler(this.easyModeEscapeCheckbox_CheckedChanged);
            // 
            // randomizeEnemiesDamageCheckbox
            // 
            this.randomizeEnemiesDamageCheckbox.AutoSize = true;
            this.randomizeEnemiesDamageCheckbox.Location = new System.Drawing.Point(6, 176);
            this.randomizeEnemiesDamageCheckbox.Name = "randomizeEnemiesDamageCheckbox";
            this.randomizeEnemiesDamageCheckbox.Size = new System.Drawing.Size(165, 17);
            this.randomizeEnemiesDamageCheckbox.TabIndex = 7;
            this.randomizeEnemiesDamageCheckbox.Text = "Randomize Enemies Damage";
            this.randomizeEnemiesDamageCheckbox.UseVisualStyleBackColor = true;
            this.randomizeEnemiesDamageCheckbox.CheckedChanged += new System.EventHandler(this.randomizeEnemiesDamageCheckbox_CheckedChanged);
            // 
            // healthLabel
            // 
            this.healthLabel.AutoSize = true;
            this.healthLabel.Location = new System.Drawing.Point(146, 140);
            this.healthLabel.Name = "healthLabel";
            this.healthLabel.Size = new System.Drawing.Size(30, 13);
            this.healthLabel.TabIndex = 6;
            this.healthLabel.Text = " ±0%";
            // 
            // randomizeEnemiesHealthTrackbar
            // 
            this.randomizeEnemiesHealthTrackbar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.randomizeEnemiesHealthTrackbar.Enabled = false;
            this.randomizeEnemiesHealthTrackbar.Location = new System.Drawing.Point(27, 138);
            this.randomizeEnemiesHealthTrackbar.Maximum = 20;
            this.randomizeEnemiesHealthTrackbar.Name = "randomizeEnemiesHealthTrackbar";
            this.randomizeEnemiesHealthTrackbar.Size = new System.Drawing.Size(113, 45);
            this.randomizeEnemiesHealthTrackbar.TabIndex = 5;
            this.toolTip1.SetToolTip(this.randomizeEnemiesHealthTrackbar, "Randomize the hp of the enemies by a minimum/maximum from their original values");
            this.randomizeEnemiesHealthTrackbar.ValueChanged += new System.EventHandler(this.randomizeEnemiesHealthTrackbar_ValueChanged);
            // 
            // randomizeEnemiesHealthCheckbox
            // 
            this.randomizeEnemiesHealthCheckbox.AutoSize = true;
            this.randomizeEnemiesHealthCheckbox.Location = new System.Drawing.Point(6, 116);
            this.randomizeEnemiesHealthCheckbox.Name = "randomizeEnemiesHealthCheckbox";
            this.randomizeEnemiesHealthCheckbox.Size = new System.Drawing.Size(191, 17);
            this.randomizeEnemiesHealthCheckbox.TabIndex = 4;
            this.randomizeEnemiesHealthCheckbox.Text = "Randomize Enemies Health Range";
            this.randomizeEnemiesHealthCheckbox.UseVisualStyleBackColor = true;
            this.randomizeEnemiesHealthCheckbox.CheckedChanged += new System.EventHandler(this.randomizeEnemiesHealthCheckbox_CheckedChanged);
            // 
            // randomizationTypeLabel
            // 
            this.randomizationTypeLabel.AutoSize = true;
            this.randomizationTypeLabel.Location = new System.Drawing.Point(146, 46);
            this.randomizationTypeLabel.Name = "randomizationTypeLabel";
            this.randomizationTypeLabel.Size = new System.Drawing.Size(37, 13);
            this.randomizationTypeLabel.TabIndex = 3;
            this.randomizationTypeLabel.Text = "Chaos";
            // 
            // lblTypeOfRandomization
            // 
            this.lblTypeOfRandomization.AutoSize = true;
            this.lblTypeOfRandomization.Location = new System.Drawing.Point(24, 30);
            this.lblTypeOfRandomization.Name = "lblTypeOfRandomization";
            this.lblTypeOfRandomization.Size = new System.Drawing.Size(116, 13);
            this.lblTypeOfRandomization.TabIndex = 2;
            this.lblTypeOfRandomization.Text = "Type of Randomization";
            // 
            // randomizationTypeTrackbar
            // 
            this.randomizationTypeTrackbar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.randomizationTypeTrackbar.Enabled = false;
            this.randomizationTypeTrackbar.LargeChange = 1;
            this.randomizationTypeTrackbar.Location = new System.Drawing.Point(27, 46);
            this.randomizationTypeTrackbar.Maximum = 4;
            this.randomizationTypeTrackbar.Name = "randomizationTypeTrackbar";
            this.randomizationTypeTrackbar.Size = new System.Drawing.Size(113, 45);
            this.randomizationTypeTrackbar.TabIndex = 1;
            this.toolTip1.SetToolTip(this.randomizationTypeTrackbar, resources.GetString("randomizationTypeTrackbar.ToolTip"));
            this.randomizationTypeTrackbar.Value = 3;
            this.randomizationTypeTrackbar.ValueChanged += new System.EventHandler(this.randomizationTypeTrackbar_ValueChanged);
            // 
            // randomizeEnemiesCheckbox
            // 
            this.randomizeEnemiesCheckbox.AutoSize = true;
            this.randomizeEnemiesCheckbox.Location = new System.Drawing.Point(6, 6);
            this.randomizeEnemiesCheckbox.Name = "randomizeEnemiesCheckbox";
            this.randomizeEnemiesCheckbox.Size = new System.Drawing.Size(152, 17);
            this.randomizeEnemiesCheckbox.TabIndex = 0;
            this.randomizeEnemiesCheckbox.Text = "Randomize Enemies (WIP)";
            this.randomizeEnemiesCheckbox.UseVisualStyleBackColor = true;
            this.randomizeEnemiesCheckbox.CheckedChanged += new System.EventHandler(this.randomizeEnemiesCheckbox_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.randomizeBossBehaviorCheckbox);
            this.tabPage2.Controls.Add(this.bossdamageLabel);
            this.tabPage2.Controls.Add(this.bossDamageRandomizationTrackbar);
            this.tabPage2.Controls.Add(this.randomizeBossDamageCheckbox);
            this.tabPage2.Controls.Add(this.bosshealthLabel);
            this.tabPage2.Controls.Add(this.bossHealthRandomizationTrackbar);
            this.tabPage2.Controls.Add(this.randomizeBossHealthCheckbox);
            this.tabPage2.Controls.Add(this.typebossLabel);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.bossRandomizationTypesTrackbar);
            this.tabPage2.Controls.Add(this.randomizeBossesCheckbox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(458, 292);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Bosses";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // randomizeBossBehaviorCheckbox
            // 
            this.randomizeBossBehaviorCheckbox.AutoSize = true;
            this.randomizeBossBehaviorCheckbox.Location = new System.Drawing.Point(6, 269);
            this.randomizeBossBehaviorCheckbox.Name = "randomizeBossBehaviorCheckbox";
            this.randomizeBossBehaviorCheckbox.Size = new System.Drawing.Size(180, 17);
            this.randomizeBossBehaviorCheckbox.TabIndex = 27;
            this.randomizeBossBehaviorCheckbox.Text = "Randomize Boss Behavior (WIP)";
            this.randomizeBossBehaviorCheckbox.UseVisualStyleBackColor = true;
            this.randomizeBossBehaviorCheckbox.CheckedChanged += new System.EventHandler(this.randomizeBossBehaviorCheckbox_CheckedChanged);
            // 
            // bossdamageLabel
            // 
            this.bossdamageLabel.AutoSize = true;
            this.bossdamageLabel.Location = new System.Drawing.Point(146, 154);
            this.bossdamageLabel.Name = "bossdamageLabel";
            this.bossdamageLabel.Size = new System.Drawing.Size(68, 13);
            this.bossdamageLabel.TabIndex = 26;
            this.bossdamageLabel.Text = "100% - 100%";
            // 
            // bossDamageRandomizationTrackbar
            // 
            this.bossDamageRandomizationTrackbar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bossDamageRandomizationTrackbar.Enabled = false;
            this.bossDamageRandomizationTrackbar.Location = new System.Drawing.Point(27, 148);
            this.bossDamageRandomizationTrackbar.Maximum = 20;
            this.bossDamageRandomizationTrackbar.Name = "bossDamageRandomizationTrackbar";
            this.bossDamageRandomizationTrackbar.Size = new System.Drawing.Size(113, 45);
            this.bossDamageRandomizationTrackbar.TabIndex = 25;
            this.toolTip1.SetToolTip(this.bossDamageRandomizationTrackbar, "Randomize the hp of the enemies by a maximum / minimum percentage");
            this.bossDamageRandomizationTrackbar.ValueChanged += new System.EventHandler(this.bossDamageRandomizationTrackbar_ValueChanged);
            // 
            // randomizeBossDamageCheckbox
            // 
            this.randomizeBossDamageCheckbox.AutoSize = true;
            this.randomizeBossDamageCheckbox.Location = new System.Drawing.Point(6, 125);
            this.randomizeBossDamageCheckbox.Name = "randomizeBossDamageCheckbox";
            this.randomizeBossDamageCheckbox.Size = new System.Drawing.Size(159, 17);
            this.randomizeBossDamageCheckbox.TabIndex = 24;
            this.randomizeBossDamageCheckbox.Text = "Randomize Bosses Damage";
            this.randomizeBossDamageCheckbox.UseVisualStyleBackColor = true;
            this.randomizeBossDamageCheckbox.CheckedChanged += new System.EventHandler(this.randomizeBossDamageCheckbox_CheckedChanged);
            // 
            // bosshealthLabel
            // 
            this.bosshealthLabel.AutoSize = true;
            this.bosshealthLabel.Location = new System.Drawing.Point(146, 99);
            this.bosshealthLabel.Name = "bosshealthLabel";
            this.bosshealthLabel.Size = new System.Drawing.Size(71, 13);
            this.bosshealthLabel.TabIndex = 23;
            this.bosshealthLabel.Text = " 100% - 100%";
            // 
            // bossHealthRandomizationTrackbar
            // 
            this.bossHealthRandomizationTrackbar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bossHealthRandomizationTrackbar.Enabled = false;
            this.bossHealthRandomizationTrackbar.Location = new System.Drawing.Point(27, 97);
            this.bossHealthRandomizationTrackbar.Maximum = 20;
            this.bossHealthRandomizationTrackbar.Name = "bossHealthRandomizationTrackbar";
            this.bossHealthRandomizationTrackbar.Size = new System.Drawing.Size(113, 45);
            this.bossHealthRandomizationTrackbar.TabIndex = 22;
            this.toolTip1.SetToolTip(this.bossHealthRandomizationTrackbar, "Randomize the hp of the enemies by a maximum / minimum percentage");
            this.bossHealthRandomizationTrackbar.ValueChanged += new System.EventHandler(this.bossHealthRandomizationTrackbar_ValueChanged);
            // 
            // randomizeBossHealthCheckbox
            // 
            this.randomizeBossHealthCheckbox.AutoSize = true;
            this.randomizeBossHealthCheckbox.Location = new System.Drawing.Point(6, 74);
            this.randomizeBossHealthCheckbox.Name = "randomizeBossHealthCheckbox";
            this.randomizeBossHealthCheckbox.Size = new System.Drawing.Size(150, 17);
            this.randomizeBossHealthCheckbox.TabIndex = 21;
            this.randomizeBossHealthCheckbox.Text = "Randomize Bosses Health";
            this.randomizeBossHealthCheckbox.UseVisualStyleBackColor = true;
            this.randomizeBossHealthCheckbox.CheckedChanged += new System.EventHandler(this.randomizeBossHealthCheckbox_CheckedChanged);
            // 
            // typebossLabel
            // 
            this.typebossLabel.AutoSize = true;
            this.typebossLabel.Location = new System.Drawing.Point(146, 46);
            this.typebossLabel.Name = "typebossLabel";
            this.typebossLabel.Size = new System.Drawing.Size(33, 13);
            this.typebossLabel.TabIndex = 20;
            this.typebossLabel.Text = "Basic";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Type of Randomization";
            // 
            // bossRandomizationTypesTrackbar
            // 
            this.bossRandomizationTypesTrackbar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bossRandomizationTypesTrackbar.Enabled = false;
            this.bossRandomizationTypesTrackbar.LargeChange = 1;
            this.bossRandomizationTypesTrackbar.Location = new System.Drawing.Point(27, 46);
            this.bossRandomizationTypesTrackbar.Maximum = 2;
            this.bossRandomizationTypesTrackbar.Name = "bossRandomizationTypesTrackbar";
            this.bossRandomizationTypesTrackbar.Size = new System.Drawing.Size(113, 45);
            this.bossRandomizationTypesTrackbar.TabIndex = 18;
            this.toolTip1.SetToolTip(this.bossRandomizationTypesTrackbar, resources.GetString("bossRandomizationTypesTrackbar.ToolTip"));
            this.bossRandomizationTypesTrackbar.ValueChanged += new System.EventHandler(this.bossRandomizationTypesTrackbar_ValueChanged);
            // 
            // randomizeBossesCheckbox
            // 
            this.randomizeBossesCheckbox.AutoSize = true;
            this.randomizeBossesCheckbox.Location = new System.Drawing.Point(6, 6);
            this.randomizeBossesCheckbox.Name = "randomizeBossesCheckbox";
            this.randomizeBossesCheckbox.Size = new System.Drawing.Size(116, 17);
            this.randomizeBossesCheckbox.TabIndex = 17;
            this.randomizeBossesCheckbox.Text = "Randomize Bosses";
            this.randomizeBossesCheckbox.UseVisualStyleBackColor = true;
            this.randomizeBossesCheckbox.CheckedChanged += new System.EventHandler(this.randomizeBossesCheckbox_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.pukeModeCheckbox);
            this.tabPage3.Controls.Add(this.setBlackoutModeCheckbox);
            this.tabPage3.Controls.Add(this.randomizeSpritePalettesAdvancedCheckbox);
            this.tabPage3.Controls.Add(this.randomizeSpritePalettesBasicCheckbox);
            this.tabPage3.Controls.Add(this.randomizeOverworldPalettesCheckbox);
            this.tabPage3.Controls.Add(this.randomizeDungeonPalettesCheckbox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(458, 292);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Palettes";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // pukeModeCheckbox
            // 
            this.pukeModeCheckbox.AutoSize = true;
            this.pukeModeCheckbox.Location = new System.Drawing.Point(3, 118);
            this.pukeModeCheckbox.Name = "pukeModeCheckbox";
            this.pukeModeCheckbox.Size = new System.Drawing.Size(144, 17);
            this.pukeModeCheckbox.TabIndex = 5;
            this.pukeModeCheckbox.Text = "Puke Mode (Indoor Only)";
            this.pukeModeCheckbox.UseVisualStyleBackColor = true;
            this.pukeModeCheckbox.CheckedChanged += new System.EventHandler(this.pukeModeCheckbox_CheckedChanged);
            // 
            // setBlackoutModeCheckbox
            // 
            this.setBlackoutModeCheckbox.AutoSize = true;
            this.setBlackoutModeCheckbox.Enabled = false;
            this.setBlackoutModeCheckbox.Location = new System.Drawing.Point(25, 27);
            this.setBlackoutModeCheckbox.Name = "setBlackoutModeCheckbox";
            this.setBlackoutModeCheckbox.Size = new System.Drawing.Size(214, 17);
            this.setBlackoutModeCheckbox.TabIndex = 4;
            this.setBlackoutModeCheckbox.Text = "Set all palette to black (Black out mode)";
            this.setBlackoutModeCheckbox.UseVisualStyleBackColor = true;
            this.setBlackoutModeCheckbox.CheckedChanged += new System.EventHandler(this.setBlackoutModeCheckbox_CheckedChanged);
            // 
            // randomizeSpritePalettesAdvancedCheckbox
            // 
            this.randomizeSpritePalettesAdvancedCheckbox.AutoSize = true;
            this.randomizeSpritePalettesAdvancedCheckbox.Enabled = false;
            this.randomizeSpritePalettesAdvancedCheckbox.Location = new System.Drawing.Point(25, 95);
            this.randomizeSpritePalettesAdvancedCheckbox.Name = "randomizeSpritePalettesAdvancedCheckbox";
            this.randomizeSpritePalettesAdvancedCheckbox.Size = new System.Drawing.Size(217, 17);
            this.randomizeSpritePalettesAdvancedCheckbox.TabIndex = 3;
            this.randomizeSpritePalettesAdvancedCheckbox.Text = "Advanced Sprite Palettes (Can be awful)";
            this.randomizeSpritePalettesAdvancedCheckbox.UseVisualStyleBackColor = true;
            this.randomizeSpritePalettesAdvancedCheckbox.CheckedChanged += new System.EventHandler(this.randomizeSpritePalettesAdvancedCheckbox_CheckedChanged);
            // 
            // randomizeSpritePalettesBasicCheckbox
            // 
            this.randomizeSpritePalettesBasicCheckbox.AutoSize = true;
            this.randomizeSpritePalettesBasicCheckbox.Location = new System.Drawing.Point(3, 72);
            this.randomizeSpritePalettesBasicCheckbox.Name = "randomizeSpritePalettesBasicCheckbox";
            this.randomizeSpritePalettesBasicCheckbox.Size = new System.Drawing.Size(185, 17);
            this.randomizeSpritePalettesBasicCheckbox.TabIndex = 2;
            this.randomizeSpritePalettesBasicCheckbox.Text = "Randomize Sprite Palettes (Basic)";
            this.randomizeSpritePalettesBasicCheckbox.UseVisualStyleBackColor = true;
            this.randomizeSpritePalettesBasicCheckbox.CheckedChanged += new System.EventHandler(this.randomizeSpritePalettesBasicCheckbox_CheckedChanged);
            // 
            // randomizeOverworldPalettesCheckbox
            // 
            this.randomizeOverworldPalettesCheckbox.AutoSize = true;
            this.randomizeOverworldPalettesCheckbox.Location = new System.Drawing.Point(3, 49);
            this.randomizeOverworldPalettesCheckbox.Name = "randomizeOverworldPalettesCheckbox";
            this.randomizeOverworldPalettesCheckbox.Size = new System.Drawing.Size(171, 17);
            this.randomizeOverworldPalettesCheckbox.TabIndex = 1;
            this.randomizeOverworldPalettesCheckbox.Text = "Randomize Overworld Palettes";
            this.randomizeOverworldPalettesCheckbox.UseVisualStyleBackColor = true;
            this.randomizeOverworldPalettesCheckbox.CheckedChanged += new System.EventHandler(this.randomizeOverworldPalettesCheckbox_CheckedChanged);
            // 
            // randomizeDungeonPalettesCheckbox
            // 
            this.randomizeDungeonPalettesCheckbox.AutoSize = true;
            this.randomizeDungeonPalettesCheckbox.Location = new System.Drawing.Point(3, 4);
            this.randomizeDungeonPalettesCheckbox.Name = "randomizeDungeonPalettesCheckbox";
            this.randomizeDungeonPalettesCheckbox.Size = new System.Drawing.Size(172, 17);
            this.randomizeDungeonPalettesCheckbox.TabIndex = 0;
            this.randomizeDungeonPalettesCheckbox.Text = "Randomize Dungeons Palettes";
            this.randomizeDungeonPalettesCheckbox.UseVisualStyleBackColor = true;
            this.randomizeDungeonPalettesCheckbox.CheckedChanged += new System.EventHandler(this.randomizeDungeonPalettesCheckbox_CheckedChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.alternateGfxCheckbox);
            this.tabPage4.Controls.Add(this.andyModeCheckbox);
            this.tabPage4.Controls.Add(this.customBossesCheckbox);
            this.tabPage4.Controls.Add(this.shufflePotContentsCheckbox);
            this.tabPage4.Controls.Add(this.shuffleMusicCheckBox);
            this.tabPage4.Controls.Add(this.debugModeCheckbox);
            this.tabPage4.Controls.Add(this.bootlegMagicCheckbox);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(458, 292);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Extra";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // andyModeCheckbox
            // 
            this.andyModeCheckbox.AutoSize = true;
            this.andyModeCheckbox.Location = new System.Drawing.Point(7, 30);
            this.andyModeCheckbox.Name = "andyModeCheckbox";
            this.andyModeCheckbox.Size = new System.Drawing.Size(80, 17);
            this.andyModeCheckbox.TabIndex = 12;
            this.andyModeCheckbox.Text = "Andy Mode";
            this.andyModeCheckbox.UseVisualStyleBackColor = true;
            this.andyModeCheckbox.CheckedChanged += new System.EventHandler(this.andyModeCheckbox_CheckedChanged);
            // 
            // customBossesCheckbox
            // 
            this.customBossesCheckbox.AutoSize = true;
            this.customBossesCheckbox.Enabled = false;
            this.customBossesCheckbox.Location = new System.Drawing.Point(318, 7);
            this.customBossesCheckbox.Name = "customBossesCheckbox";
            this.customBossesCheckbox.Size = new System.Drawing.Size(98, 17);
            this.customBossesCheckbox.TabIndex = 11;
            this.customBossesCheckbox.Text = "Custom Bosses";
            this.customBossesCheckbox.UseVisualStyleBackColor = true;
            this.customBossesCheckbox.CheckedChanged += new System.EventHandler(this.customBossesCheckbox_CheckedChanged);
            // 
            // shufflePotContentsCheckbox
            // 
            this.shufflePotContentsCheckbox.AutoSize = true;
            this.shufflePotContentsCheckbox.Location = new System.Drawing.Point(158, 7);
            this.shufflePotContentsCheckbox.Name = "shufflePotContentsCheckbox";
            this.shufflePotContentsCheckbox.Size = new System.Drawing.Size(123, 17);
            this.shufflePotContentsCheckbox.TabIndex = 10;
            this.shufflePotContentsCheckbox.Text = "Shuffle Pot Contents";
            this.shufflePotContentsCheckbox.UseVisualStyleBackColor = true;
            this.shufflePotContentsCheckbox.CheckedChanged += new System.EventHandler(this.shufflePotContentsCheckbox_CheckedChanged);
            // 
            // shuffleMusicCheckBox
            // 
            this.shuffleMusicCheckBox.AutoSize = true;
            this.shuffleMusicCheckBox.Location = new System.Drawing.Point(7, 148);
            this.shuffleMusicCheckBox.Name = "shuffleMusicCheckBox";
            this.shuffleMusicCheckBox.Size = new System.Drawing.Size(177, 17);
            this.shuffleMusicCheckBox.TabIndex = 9;
            this.shuffleMusicCheckBox.Text = "Shuffle Music (May crash game)";
            this.shuffleMusicCheckBox.UseVisualStyleBackColor = true;
            this.shuffleMusicCheckBox.CheckedChanged += new System.EventHandler(this.shuffleMusicCheckBox_CheckedChanged);
            // 
            // debugModeCheckbox
            // 
            this.debugModeCheckbox.AutoSize = true;
            this.debugModeCheckbox.Location = new System.Drawing.Point(7, 272);
            this.debugModeCheckbox.Name = "debugModeCheckbox";
            this.debugModeCheckbox.Size = new System.Drawing.Size(88, 17);
            this.debugModeCheckbox.TabIndex = 8;
            this.debugModeCheckbox.Text = "Debug Mode";
            this.debugModeCheckbox.UseVisualStyleBackColor = true;
            this.debugModeCheckbox.CheckedChanged += new System.EventHandler(this.debugModeCheckbox_CheckedChanged);
            // 
            // bootlegMagicCheckbox
            // 
            this.bootlegMagicCheckbox.AutoSize = true;
            this.bootlegMagicCheckbox.Checked = true;
            this.bootlegMagicCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bootlegMagicCheckbox.Location = new System.Drawing.Point(7, 7);
            this.bootlegMagicCheckbox.Name = "bootlegMagicCheckbox";
            this.bootlegMagicCheckbox.Size = new System.Drawing.Size(94, 17);
            this.bootlegMagicCheckbox.TabIndex = 7;
            this.bootlegMagicCheckbox.Text = "Bootleg Magic";
            this.bootlegMagicCheckbox.UseVisualStyleBackColor = true;
            this.bootlegMagicCheckbox.CheckedChanged += new System.EventHandler(this.chkBootlegMagic_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(484, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Seed Number";
            // 
            // seedNumberTextbox
            // 
            this.seedNumberTextbox.Location = new System.Drawing.Point(487, 254);
            this.seedNumberTextbox.Name = "seedNumberTextbox";
            this.seedNumberTextbox.Size = new System.Drawing.Size(121, 20);
            this.seedNumberTextbox.TabIndex = 24;
            // 
            // alternateGfxCheckbox
            // 
            this.alternateGfxCheckbox.AutoSize = true;
            this.alternateGfxCheckbox.Checked = true;
            this.alternateGfxCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.alternateGfxCheckbox.Location = new System.Drawing.Point(158, 30);
            this.alternateGfxCheckbox.Name = "alternateGfxCheckbox";
            this.alternateGfxCheckbox.Size = new System.Drawing.Size(87, 17);
            this.alternateGfxCheckbox.TabIndex = 13;
            this.alternateGfxCheckbox.Text = "Alternate Gfx";
            this.alternateGfxCheckbox.UseVisualStyleBackColor = true;
            this.alternateGfxCheckbox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // EnemizerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 331);
            this.Controls.Add(this.seedNumberTextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.randomizeLinksPaletteCheckbox);
            this.Controls.Add(this.linkSpritePicturebox);
            this.Controls.Add(this.checkForUpdatesCheckbox);
            this.Controls.Add(this.checkForUpdatesButton);
            this.Controls.Add(this.completeModificationCombobox);
            this.Controls.Add(this.generateSpoilerCheckbox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.weaponSpriteCombobox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.linkSpriteCombobox);
            this.Controls.Add(this.generateRomButton);
            this.Name = "EnemizerForm";
            this.Text = "Enemizer 6.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EnemizerForm_FormClosing);
            this.Load += new System.EventHandler(this.EnemizerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.linkSpritePicturebox)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.absorbableItemsSpawnrateTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.randomizeEnemiesHealthTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.randomizationTypeTrackbar)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bossDamageRandomizationTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bossHealthRandomizationTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bossRandomizationTypesTrackbar)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button generateRomButton;
        private System.Windows.Forms.ComboBox linkSpriteCombobox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox weaponSpriteCombobox;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.CheckBox generateSpoilerCheckbox;
        private System.Windows.Forms.ComboBox completeModificationCombobox;
        private System.Windows.Forms.Button checkForUpdatesButton;
        private System.Windows.Forms.CheckBox checkForUpdatesCheckbox;
        private System.Windows.Forms.PictureBox linkSpritePicturebox;
        public System.Windows.Forms.CheckBox randomizeLinksPaletteCheckbox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TrackBar randomizationTypeTrackbar;
        private System.Windows.Forms.CheckBox randomizeEnemiesCheckbox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label lblTypeOfRandomization;
        private System.Windows.Forms.Label randomizationTypeLabel;
        private System.Windows.Forms.Label healthLabel;
        private System.Windows.Forms.TrackBar randomizeEnemiesHealthTrackbar;
        private System.Windows.Forms.CheckBox randomizeEnemiesHealthCheckbox;
        private System.Windows.Forms.CheckBox easyModeEscapeCheckbox;
        private System.Windows.Forms.CheckBox randomizeEnemiesDamageCheckbox;
        private System.Windows.Forms.CheckBox allowAbsorbableItemsCheckbox;
        private System.Windows.Forms.Label spawnrateLabel;
        private System.Windows.Forms.Label lblAbsorbSpawnRate;
        private System.Windows.Forms.TrackBar absorbableItemsSpawnrateTrackbar;
        private System.Windows.Forms.CheckedListBox absorbableItemsChecklist;
        private System.Windows.Forms.CheckBox allowZeroDamageCheckbox;
        private System.Windows.Forms.Label bossdamageLabel;
        private System.Windows.Forms.TrackBar bossDamageRandomizationTrackbar;
        private System.Windows.Forms.CheckBox randomizeBossDamageCheckbox;
        private System.Windows.Forms.Label bosshealthLabel;
        private System.Windows.Forms.TrackBar bossHealthRandomizationTrackbar;
        private System.Windows.Forms.CheckBox randomizeBossHealthCheckbox;
        private System.Windows.Forms.Label typebossLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar bossRandomizationTypesTrackbar;
        private System.Windows.Forms.CheckBox randomizeBossesCheckbox;
        private System.Windows.Forms.CheckBox randomizeBossBehaviorCheckbox;
        private System.Windows.Forms.CheckBox setBlackoutModeCheckbox;
        private System.Windows.Forms.CheckBox randomizeSpritePalettesAdvancedCheckbox;
        private System.Windows.Forms.CheckBox randomizeSpritePalettesBasicCheckbox;
        private System.Windows.Forms.CheckBox randomizeOverworldPalettesCheckbox;
        private System.Windows.Forms.CheckBox randomizeDungeonPalettesCheckbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox seedNumberTextbox;
        private System.Windows.Forms.CheckBox chkRandomizeBushEnemyChance;
        private System.Windows.Forms.CheckBox bootlegMagicCheckbox;
        private System.Windows.Forms.CheckBox debugModeCheckbox;
        private System.Windows.Forms.CheckBox customBossesCheckbox;
        private System.Windows.Forms.CheckBox shufflePotContentsCheckbox;
        private System.Windows.Forms.CheckBox shuffleMusicCheckBox;
        private System.Windows.Forms.CheckBox pukeModeCheckbox;
        private System.Windows.Forms.CheckBox andyModeCheckbox;
        private System.Windows.Forms.CheckBox alternateGfxCheckbox;
    }
}

