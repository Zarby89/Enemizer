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
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.linkPaletteCheckbox = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.allowzerodamageCheckbox = new System.Windows.Forms.CheckBox();
            this.absorbableChecklist = new System.Windows.Forms.CheckedListBox();
            this.spawnrateLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.spawnrateTrackbar = new System.Windows.Forms.TrackBar();
            this.enemiesAbsorbableCheckbox = new System.Windows.Forms.CheckBox();
            this.easyModeEscapeCheckbox = new System.Windows.Forms.CheckBox();
            this.enemiesDamageCheckbox = new System.Windows.Forms.CheckBox();
            this.healthLabel = new System.Windows.Forms.Label();
            this.enemiesHealthTrackbar = new System.Windows.Forms.TrackBar();
            this.enemiesHealthCheckbox = new System.Windows.Forms.CheckBox();
            this.typeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.typeTrackbar = new System.Windows.Forms.TrackBar();
            this.enemiesCheckbox = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.bossBehaviorCheckbox = new System.Windows.Forms.CheckBox();
            this.bossdamageLabel = new System.Windows.Forms.Label();
            this.bossdamageTrackbar = new System.Windows.Forms.TrackBar();
            this.bossdamageCheckbox = new System.Windows.Forms.CheckBox();
            this.bosshealthLabel = new System.Windows.Forms.Label();
            this.bosshealthTrackbar = new System.Windows.Forms.TrackBar();
            this.bosshealthCheckbox = new System.Windows.Forms.CheckBox();
            this.typebossLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.bosstypesTrackbar = new System.Windows.Forms.TrackBar();
            this.bossesCheckbox = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.blackoutPaletteCheckbox = new System.Windows.Forms.CheckBox();
            this.spriteadvancedPaletteCheckbox = new System.Windows.Forms.CheckBox();
            this.spritebasicPaletteCheckbox = new System.Windows.Forms.CheckBox();
            this.overworldPaletteCheckbox = new System.Windows.Forms.CheckBox();
            this.dungeonPaletteCheckbox = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spawnrateTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemiesHealthTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.typeTrackbar)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bossdamageTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bosshealthTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bosstypesTrackbar)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(486, 251);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Generate!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "sfc";
            this.openFileDialog1.FileName = "VT Randomized Rom";
            this.openFileDialog1.Filter = "VT Randomized Rom|*.sfc";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Dungeon Enemies",
            "Overworld Enemies",
            "Dungeon Palettes",
            "Enemy Palettes",
            "Overworld Palettes",
            "Enemy Hit Points",
            "Enemy DMG Output",
            "Enemy HP to 0",
            "Shuffle Bosses",
            "Absorbables in Pool",
            "Boss Madness",
            "Blackout Mode",
            "Shuffle Music",
            "Custom Bosses (WIP)",
            "Shuffle Pot Contents"});
            this.checkedListBox1.Location = new System.Drawing.Point(110, 37);
            this.checkedListBox1.MultiColumn = true;
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(263, 184);
            this.checkedListBox1.TabIndex = 5;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            this.checkedListBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.checkedListBox1_MouseUp);
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(527, 390);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.descriptionLabel.TabIndex = 6;
            this.descriptionLabel.Text = "Descritpion";
            this.descriptionLabel.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(533, 359);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Check All";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(487, 67);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(128, 21);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(484, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Link Gfx :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(484, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Weapon Gfx - WIP :";
            // 
            // comboBox2
            // 
            this.comboBox2.Enabled = false;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Normal Sword",
            "Mace"});
            this.comboBox2.Location = new System.Drawing.Point(487, 184);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 11;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(532, 406);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(532, 306);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(76, 20);
            this.panel1.TabIndex = 13;
            this.panel1.Visible = false;
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
            this.label5.Location = new System.Drawing.Point(484, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Complete Modification (Gfx)";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(486, 280);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(126, 17);
            this.checkBox1.TabIndex = 16;
            this.checkBox1.Text = "Generate Spoiler Log";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox3
            // 
            this.comboBox3.Enabled = false;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "Normal Sword",
            "Mace"});
            this.comboBox3.Location = new System.Drawing.Point(487, 224);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(121, 21);
            this.comboBox3.TabIndex = 17;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(486, 28);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(129, 23);
            this.button4.TabIndex = 18;
            this.button4.Text = "Check Update";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(487, 8);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(135, 17);
            this.checkBox2.TabIndex = 19;
            this.checkBox2.Text = "Check Update on Start";
            this.toolTip1.SetToolTip(this.checkBox2, "Automatically check if there is a new version of the enemizer");
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(487, 94);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 48);
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // linkPaletteCheckbox
            // 
            this.linkPaletteCheckbox.AutoSize = true;
            this.linkPaletteCheckbox.Location = new System.Drawing.Point(487, 148);
            this.linkPaletteCheckbox.Name = "linkPaletteCheckbox";
            this.linkPaletteCheckbox.Size = new System.Drawing.Size(125, 17);
            this.linkPaletteCheckbox.TabIndex = 21;
            this.linkPaletteCheckbox.Text = "Random Link Palette";
            this.linkPaletteCheckbox.UseVisualStyleBackColor = true;
            this.linkPaletteCheckbox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.linkPaletteCheckbox_MouseClick);
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
            this.tabPage1.Controls.Add(this.allowzerodamageCheckbox);
            this.tabPage1.Controls.Add(this.absorbableChecklist);
            this.tabPage1.Controls.Add(this.spawnrateLabel);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.spawnrateTrackbar);
            this.tabPage1.Controls.Add(this.enemiesAbsorbableCheckbox);
            this.tabPage1.Controls.Add(this.easyModeEscapeCheckbox);
            this.tabPage1.Controls.Add(this.enemiesDamageCheckbox);
            this.tabPage1.Controls.Add(this.healthLabel);
            this.tabPage1.Controls.Add(this.enemiesHealthTrackbar);
            this.tabPage1.Controls.Add(this.enemiesHealthCheckbox);
            this.tabPage1.Controls.Add(this.typeLabel);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.typeTrackbar);
            this.tabPage1.Controls.Add(this.enemiesCheckbox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(458, 292);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Enemies";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // allowzerodamageCheckbox
            // 
            this.allowzerodamageCheckbox.AutoSize = true;
            this.allowzerodamageCheckbox.Enabled = false;
            this.allowzerodamageCheckbox.Location = new System.Drawing.Point(21, 157);
            this.allowzerodamageCheckbox.Name = "allowzerodamageCheckbox";
            this.allowzerodamageCheckbox.Size = new System.Drawing.Size(103, 17);
            this.allowzerodamageCheckbox.TabIndex = 16;
            this.allowzerodamageCheckbox.Text = "Allow 0 Damage";
            this.toolTip1.SetToolTip(this.allowzerodamageCheckbox, "Allow the sprites to do bumper effect just push you back without any damage");
            this.allowzerodamageCheckbox.UseVisualStyleBackColor = true;
            // 
            // absorbableChecklist
            // 
            this.absorbableChecklist.Enabled = false;
            this.absorbableChecklist.FormattingEnabled = true;
            this.absorbableChecklist.Items.AddRange(new object[] {
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
            this.absorbableChecklist.Location = new System.Drawing.Point(265, 74);
            this.absorbableChecklist.Name = "absorbableChecklist";
            this.absorbableChecklist.Size = new System.Drawing.Size(140, 214);
            this.absorbableChecklist.TabIndex = 12;
            this.absorbableChecklist.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.absorbableChecklist_ItemCheck);
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
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(253, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Spawn Rate";
            // 
            // spawnrateTrackbar
            // 
            this.spawnrateTrackbar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.spawnrateTrackbar.Enabled = false;
            this.spawnrateTrackbar.Location = new System.Drawing.Point(256, 42);
            this.spawnrateTrackbar.Maximum = 20;
            this.spawnrateTrackbar.Name = "spawnrateTrackbar";
            this.spawnrateTrackbar.Size = new System.Drawing.Size(140, 45);
            this.spawnrateTrackbar.TabIndex = 13;
            this.toolTip1.SetToolTip(this.spawnrateTrackbar, resources.GetString("spawnrateTrackbar.ToolTip"));
            this.spawnrateTrackbar.Scroll += new System.EventHandler(this.spawnrateTrackbar_Scroll);
            // 
            // enemiesAbsorbableCheckbox
            // 
            this.enemiesAbsorbableCheckbox.AutoSize = true;
            this.enemiesAbsorbableCheckbox.Location = new System.Drawing.Point(240, 6);
            this.enemiesAbsorbableCheckbox.Name = "enemiesAbsorbableCheckbox";
            this.enemiesAbsorbableCheckbox.Size = new System.Drawing.Size(140, 17);
            this.enemiesAbsorbableCheckbox.TabIndex = 11;
            this.enemiesAbsorbableCheckbox.Text = "Allow Absorbables Items";
            this.enemiesAbsorbableCheckbox.UseVisualStyleBackColor = true;
            this.enemiesAbsorbableCheckbox.CheckedChanged += new System.EventHandler(this.checkboxes_CheckedChanged);
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
            // 
            // enemiesDamageCheckbox
            // 
            this.enemiesDamageCheckbox.AutoSize = true;
            this.enemiesDamageCheckbox.Location = new System.Drawing.Point(6, 134);
            this.enemiesDamageCheckbox.Name = "enemiesDamageCheckbox";
            this.enemiesDamageCheckbox.Size = new System.Drawing.Size(165, 17);
            this.enemiesDamageCheckbox.TabIndex = 7;
            this.enemiesDamageCheckbox.Text = "Randomize Enemies Damage";
            this.enemiesDamageCheckbox.UseVisualStyleBackColor = true;
            this.enemiesDamageCheckbox.CheckedChanged += new System.EventHandler(this.checkboxes_CheckedChanged);
            // 
            // healthLabel
            // 
            this.healthLabel.AutoSize = true;
            this.healthLabel.Location = new System.Drawing.Point(146, 99);
            this.healthLabel.Name = "healthLabel";
            this.healthLabel.Size = new System.Drawing.Size(59, 13);
            this.healthLabel.TabIndex = 6;
            this.healthLabel.Text = " 00% - 00%";
            // 
            // enemiesHealthTrackbar
            // 
            this.enemiesHealthTrackbar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.enemiesHealthTrackbar.Enabled = false;
            this.enemiesHealthTrackbar.Location = new System.Drawing.Point(27, 97);
            this.enemiesHealthTrackbar.Maximum = 20;
            this.enemiesHealthTrackbar.Name = "enemiesHealthTrackbar";
            this.enemiesHealthTrackbar.Size = new System.Drawing.Size(113, 45);
            this.enemiesHealthTrackbar.TabIndex = 5;
            this.toolTip1.SetToolTip(this.enemiesHealthTrackbar, "Randomize the hp of the enemies by a minimum/maximum from their original values");
            this.enemiesHealthTrackbar.Scroll += new System.EventHandler(this.enemiesHealthTrackbar_Scroll);
            // 
            // enemiesHealthCheckbox
            // 
            this.enemiesHealthCheckbox.AutoSize = true;
            this.enemiesHealthCheckbox.Location = new System.Drawing.Point(6, 74);
            this.enemiesHealthCheckbox.Name = "enemiesHealthCheckbox";
            this.enemiesHealthCheckbox.Size = new System.Drawing.Size(191, 17);
            this.enemiesHealthCheckbox.TabIndex = 4;
            this.enemiesHealthCheckbox.Text = "Randomize Enemies Health Range";
            this.enemiesHealthCheckbox.UseVisualStyleBackColor = true;
            this.enemiesHealthCheckbox.CheckedChanged += new System.EventHandler(this.checkboxes_CheckedChanged);
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(146, 46);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(33, 13);
            this.typeLabel.TabIndex = 3;
            this.typeLabel.Text = "Basic";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Type of Randomization";
            // 
            // typeTrackbar
            // 
            this.typeTrackbar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.typeTrackbar.Enabled = false;
            this.typeTrackbar.Location = new System.Drawing.Point(27, 46);
            this.typeTrackbar.Maximum = 4;
            this.typeTrackbar.Name = "typeTrackbar";
            this.typeTrackbar.Size = new System.Drawing.Size(113, 45);
            this.typeTrackbar.TabIndex = 1;
            this.toolTip1.SetToolTip(this.typeTrackbar, resources.GetString("typeTrackbar.ToolTip"));
            this.typeTrackbar.Scroll += new System.EventHandler(this.typeTrackbar_Scroll);
            // 
            // enemiesCheckbox
            // 
            this.enemiesCheckbox.AutoSize = true;
            this.enemiesCheckbox.Location = new System.Drawing.Point(6, 6);
            this.enemiesCheckbox.Name = "enemiesCheckbox";
            this.enemiesCheckbox.Size = new System.Drawing.Size(226, 17);
            this.enemiesCheckbox.TabIndex = 0;
            this.enemiesCheckbox.Text = "Randomize Enemies (WIP) - default Chaos";
            this.enemiesCheckbox.UseVisualStyleBackColor = true;
            this.enemiesCheckbox.CheckedChanged += new System.EventHandler(this.checkboxes_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.bossBehaviorCheckbox);
            this.tabPage2.Controls.Add(this.bossdamageLabel);
            this.tabPage2.Controls.Add(this.bossdamageTrackbar);
            this.tabPage2.Controls.Add(this.bossdamageCheckbox);
            this.tabPage2.Controls.Add(this.bosshealthLabel);
            this.tabPage2.Controls.Add(this.bosshealthTrackbar);
            this.tabPage2.Controls.Add(this.bosshealthCheckbox);
            this.tabPage2.Controls.Add(this.typebossLabel);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.bosstypesTrackbar);
            this.tabPage2.Controls.Add(this.bossesCheckbox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(458, 292);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Bosses";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // bossBehaviorCheckbox
            // 
            this.bossBehaviorCheckbox.AutoSize = true;
            this.bossBehaviorCheckbox.Location = new System.Drawing.Point(6, 269);
            this.bossBehaviorCheckbox.Name = "bossBehaviorCheckbox";
            this.bossBehaviorCheckbox.Size = new System.Drawing.Size(180, 17);
            this.bossBehaviorCheckbox.TabIndex = 27;
            this.bossBehaviorCheckbox.Text = "Randomize Boss Behavior (WIP)";
            this.bossBehaviorCheckbox.UseVisualStyleBackColor = true;
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
            // bossdamageTrackbar
            // 
            this.bossdamageTrackbar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bossdamageTrackbar.Enabled = false;
            this.bossdamageTrackbar.Location = new System.Drawing.Point(27, 148);
            this.bossdamageTrackbar.Maximum = 20;
            this.bossdamageTrackbar.Name = "bossdamageTrackbar";
            this.bossdamageTrackbar.Size = new System.Drawing.Size(113, 45);
            this.bossdamageTrackbar.TabIndex = 25;
            this.toolTip1.SetToolTip(this.bossdamageTrackbar, "Randomize the hp of the enemies by a maximum / minimum percentage");
            this.bossdamageTrackbar.Scroll += new System.EventHandler(this.bossdamageTrackbar_Scroll);
            // 
            // bossdamageCheckbox
            // 
            this.bossdamageCheckbox.AutoSize = true;
            this.bossdamageCheckbox.Location = new System.Drawing.Point(6, 125);
            this.bossdamageCheckbox.Name = "bossdamageCheckbox";
            this.bossdamageCheckbox.Size = new System.Drawing.Size(159, 17);
            this.bossdamageCheckbox.TabIndex = 24;
            this.bossdamageCheckbox.Text = "Randomize Bosses Damage";
            this.bossdamageCheckbox.UseVisualStyleBackColor = true;
            this.bossdamageCheckbox.CheckedChanged += new System.EventHandler(this.checkboxes_CheckedChanged);
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
            // bosshealthTrackbar
            // 
            this.bosshealthTrackbar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bosshealthTrackbar.Enabled = false;
            this.bosshealthTrackbar.Location = new System.Drawing.Point(27, 97);
            this.bosshealthTrackbar.Maximum = 20;
            this.bosshealthTrackbar.Name = "bosshealthTrackbar";
            this.bosshealthTrackbar.Size = new System.Drawing.Size(113, 45);
            this.bosshealthTrackbar.TabIndex = 22;
            this.toolTip1.SetToolTip(this.bosshealthTrackbar, "Randomize the hp of the enemies by a maximum / minimum percentage");
            this.bosshealthTrackbar.Scroll += new System.EventHandler(this.bosshealthTrackbar_Scroll);
            // 
            // bosshealthCheckbox
            // 
            this.bosshealthCheckbox.AutoSize = true;
            this.bosshealthCheckbox.Location = new System.Drawing.Point(6, 74);
            this.bosshealthCheckbox.Name = "bosshealthCheckbox";
            this.bosshealthCheckbox.Size = new System.Drawing.Size(150, 17);
            this.bosshealthCheckbox.TabIndex = 21;
            this.bosshealthCheckbox.Text = "Randomize Bosses Health";
            this.bosshealthCheckbox.UseVisualStyleBackColor = true;
            this.bosshealthCheckbox.CheckedChanged += new System.EventHandler(this.checkboxes_CheckedChanged);
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
            // bosstypesTrackbar
            // 
            this.bosstypesTrackbar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bosstypesTrackbar.Enabled = false;
            this.bosstypesTrackbar.Location = new System.Drawing.Point(27, 46);
            this.bosstypesTrackbar.Maximum = 2;
            this.bosstypesTrackbar.Name = "bosstypesTrackbar";
            this.bosstypesTrackbar.Size = new System.Drawing.Size(113, 45);
            this.bosstypesTrackbar.TabIndex = 18;
            this.toolTip1.SetToolTip(this.bosstypesTrackbar, resources.GetString("bosstypesTrackbar.ToolTip"));
            this.bosstypesTrackbar.Scroll += new System.EventHandler(this.bosstypesTrackbar_Scroll);
            // 
            // bossesCheckbox
            // 
            this.bossesCheckbox.AutoSize = true;
            this.bossesCheckbox.Location = new System.Drawing.Point(6, 6);
            this.bossesCheckbox.Name = "bossesCheckbox";
            this.bossesCheckbox.Size = new System.Drawing.Size(116, 17);
            this.bossesCheckbox.TabIndex = 17;
            this.bossesCheckbox.Text = "Randomize Bosses";
            this.bossesCheckbox.UseVisualStyleBackColor = true;
            this.bossesCheckbox.CheckedChanged += new System.EventHandler(this.checkboxes_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.blackoutPaletteCheckbox);
            this.tabPage3.Controls.Add(this.spriteadvancedPaletteCheckbox);
            this.tabPage3.Controls.Add(this.spritebasicPaletteCheckbox);
            this.tabPage3.Controls.Add(this.overworldPaletteCheckbox);
            this.tabPage3.Controls.Add(this.dungeonPaletteCheckbox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(458, 292);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Palettes";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // blackoutPaletteCheckbox
            // 
            this.blackoutPaletteCheckbox.AutoSize = true;
            this.blackoutPaletteCheckbox.Enabled = false;
            this.blackoutPaletteCheckbox.Location = new System.Drawing.Point(25, 27);
            this.blackoutPaletteCheckbox.Name = "blackoutPaletteCheckbox";
            this.blackoutPaletteCheckbox.Size = new System.Drawing.Size(214, 17);
            this.blackoutPaletteCheckbox.TabIndex = 4;
            this.blackoutPaletteCheckbox.Text = "Set all palette to black (Black out mode)";
            this.blackoutPaletteCheckbox.UseVisualStyleBackColor = true;
            // 
            // spriteadvancedPaletteCheckbox
            // 
            this.spriteadvancedPaletteCheckbox.AutoSize = true;
            this.spriteadvancedPaletteCheckbox.Enabled = false;
            this.spriteadvancedPaletteCheckbox.Location = new System.Drawing.Point(25, 95);
            this.spriteadvancedPaletteCheckbox.Name = "spriteadvancedPaletteCheckbox";
            this.spriteadvancedPaletteCheckbox.Size = new System.Drawing.Size(217, 17);
            this.spriteadvancedPaletteCheckbox.TabIndex = 3;
            this.spriteadvancedPaletteCheckbox.Text = "Advanced Sprite Palettes (Can be awful)";
            this.spriteadvancedPaletteCheckbox.UseVisualStyleBackColor = true;
            // 
            // spritebasicPaletteCheckbox
            // 
            this.spritebasicPaletteCheckbox.AutoSize = true;
            this.spritebasicPaletteCheckbox.Location = new System.Drawing.Point(3, 72);
            this.spritebasicPaletteCheckbox.Name = "spritebasicPaletteCheckbox";
            this.spritebasicPaletteCheckbox.Size = new System.Drawing.Size(185, 17);
            this.spritebasicPaletteCheckbox.TabIndex = 2;
            this.spritebasicPaletteCheckbox.Text = "Randomize Sprite Palettes (Basic)";
            this.spritebasicPaletteCheckbox.UseVisualStyleBackColor = true;
            // 
            // overworldPaletteCheckbox
            // 
            this.overworldPaletteCheckbox.AutoSize = true;
            this.overworldPaletteCheckbox.Location = new System.Drawing.Point(3, 49);
            this.overworldPaletteCheckbox.Name = "overworldPaletteCheckbox";
            this.overworldPaletteCheckbox.Size = new System.Drawing.Size(171, 17);
            this.overworldPaletteCheckbox.TabIndex = 1;
            this.overworldPaletteCheckbox.Text = "Randomize Overworld Palettes";
            this.overworldPaletteCheckbox.UseVisualStyleBackColor = true;
            // 
            // dungeonPaletteCheckbox
            // 
            this.dungeonPaletteCheckbox.AutoSize = true;
            this.dungeonPaletteCheckbox.Location = new System.Drawing.Point(3, 4);
            this.dungeonPaletteCheckbox.Name = "dungeonPaletteCheckbox";
            this.dungeonPaletteCheckbox.Size = new System.Drawing.Size(172, 17);
            this.dungeonPaletteCheckbox.TabIndex = 0;
            this.dungeonPaletteCheckbox.Text = "Randomize Dungeons Palettes";
            this.dungeonPaletteCheckbox.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.checkedListBox1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(458, 292);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Extra";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // EnemizerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 331);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.linkPaletteCheckbox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.button1);
            this.Name = "EnemizerForm";
            this.Text = "Enemizer 6.0";
            this.Load += new System.EventHandler(this.EnemizerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spawnrateTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemiesHealthTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.typeTrackbar)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bossdamageTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bosshealthTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bosstypesTrackbar)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.CheckBox linkPaletteCheckbox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TrackBar typeTrackbar;
        private System.Windows.Forms.CheckBox enemiesCheckbox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.Label healthLabel;
        private System.Windows.Forms.TrackBar enemiesHealthTrackbar;
        private System.Windows.Forms.CheckBox enemiesHealthCheckbox;
        private System.Windows.Forms.CheckBox easyModeEscapeCheckbox;
        private System.Windows.Forms.CheckBox enemiesDamageCheckbox;
        private System.Windows.Forms.CheckBox enemiesAbsorbableCheckbox;
        private System.Windows.Forms.Label spawnrateLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar spawnrateTrackbar;
        private System.Windows.Forms.CheckedListBox absorbableChecklist;
        private System.Windows.Forms.CheckBox allowzerodamageCheckbox;
        private System.Windows.Forms.Label bossdamageLabel;
        private System.Windows.Forms.TrackBar bossdamageTrackbar;
        private System.Windows.Forms.CheckBox bossdamageCheckbox;
        private System.Windows.Forms.Label bosshealthLabel;
        private System.Windows.Forms.TrackBar bosshealthTrackbar;
        private System.Windows.Forms.CheckBox bosshealthCheckbox;
        private System.Windows.Forms.Label typebossLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar bosstypesTrackbar;
        private System.Windows.Forms.CheckBox bossesCheckbox;
        private System.Windows.Forms.CheckBox bossBehaviorCheckbox;
        private System.Windows.Forms.CheckBox blackoutPaletteCheckbox;
        private System.Windows.Forms.CheckBox spriteadvancedPaletteCheckbox;
        private System.Windows.Forms.CheckBox spritebasicPaletteCheckbox;
        private System.Windows.Forms.CheckBox overworldPaletteCheckbox;
        private System.Windows.Forms.CheckBox dungeonPaletteCheckbox;
    }
}

