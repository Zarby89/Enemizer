namespace MoveHeaderASM
{
    partial class Form1
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.glitched_checkbox = new System.Windows.Forms.CheckBox();
            this.zerohp_checkbox = new System.Windows.Forms.CheckBox();
            this.palette_e_checkbox = new System.Windows.Forms.CheckBox();
            this.bosses_checkbox = new System.Windows.Forms.CheckBox();
            this.anyboss_checkbox = new System.Windows.Forms.CheckBox();
            this.damage_checkbox = new System.Windows.Forms.CheckBox();
            this.hp_checkbox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.extra_checkbox = new System.Windows.Forms.CheckBox();
            this.layout_checkbox = new System.Windows.Forms.CheckBox();
            this.randsprites_checkbox = new System.Windows.Forms.CheckBox();
            this.overworld_checkbox = new System.Windows.Forms.CheckBox();
            this.absorbable_checkbox = new System.Windows.Forms.CheckBox();
            this.gfx_checkbox = new System.Windows.Forms.CheckBox();
            this.floors_checkbox = new System.Windows.Forms.CheckBox();
            this.palettes_checkbox = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 378);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(561, 146);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(513, 262);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Generate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Seed : ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(281, 20);
            this.textBox1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(296, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Flags : (case-sensitive)";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(299, 25);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(71, 20);
            this.textBox2.TabIndex = 5;
            this.textBox2.Text = "00";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.glitched_checkbox);
            this.groupBox1.Controls.Add(this.zerohp_checkbox);
            this.groupBox1.Controls.Add(this.palette_e_checkbox);
            this.groupBox1.Controls.Add(this.bosses_checkbox);
            this.groupBox1.Controls.Add(this.anyboss_checkbox);
            this.groupBox1.Controls.Add(this.damage_checkbox);
            this.groupBox1.Controls.Add(this.hp_checkbox);
            this.groupBox1.Location = new System.Drawing.Point(12, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(281, 205);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Monsters";
            // 
            // glitched_checkbox
            // 
            this.glitched_checkbox.AutoSize = true;
            this.glitched_checkbox.Location = new System.Drawing.Point(6, 157);
            this.glitched_checkbox.Name = "glitched_checkbox";
            this.glitched_checkbox.Size = new System.Drawing.Size(130, 17);
            this.glitched_checkbox.TabIndex = 6;
            this.glitched_checkbox.Text = "Use Glitched Enemies";
            this.glitched_checkbox.UseVisualStyleBackColor = true;
            this.glitched_checkbox.CheckedChanged += new System.EventHandler(this.all_checkbox_CheckedChanged);
            // 
            // zerohp_checkbox
            // 
            this.zerohp_checkbox.AutoSize = true;
            this.zerohp_checkbox.Location = new System.Drawing.Point(6, 134);
            this.zerohp_checkbox.Name = "zerohp_checkbox";
            this.zerohp_checkbox.Size = new System.Drawing.Size(124, 17);
            this.zerohp_checkbox.TabIndex = 5;
            this.zerohp_checkbox.Text = "Set Enemies HP to 0";
            this.zerohp_checkbox.UseVisualStyleBackColor = true;
            this.zerohp_checkbox.CheckedChanged += new System.EventHandler(this.all_checkbox_CheckedChanged);
            // 
            // palette_e_checkbox
            // 
            this.palette_e_checkbox.AutoSize = true;
            this.palette_e_checkbox.Checked = true;
            this.palette_e_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.palette_e_checkbox.Location = new System.Drawing.Point(6, 65);
            this.palette_e_checkbox.Name = "palette_e_checkbox";
            this.palette_e_checkbox.Size = new System.Drawing.Size(163, 17);
            this.palette_e_checkbox.TabIndex = 4;
            this.palette_e_checkbox.Text = "Randomize Enemies Palettes";
            this.palette_e_checkbox.UseVisualStyleBackColor = true;
            this.palette_e_checkbox.CheckedChanged += new System.EventHandler(this.all_checkbox_CheckedChanged);
            // 
            // bosses_checkbox
            // 
            this.bosses_checkbox.AutoSize = true;
            this.bosses_checkbox.Enabled = false;
            this.bosses_checkbox.Location = new System.Drawing.Point(6, 111);
            this.bosses_checkbox.Name = "bosses_checkbox";
            this.bosses_checkbox.Size = new System.Drawing.Size(269, 17);
            this.bosses_checkbox.TabIndex = 3;
            this.bosses_checkbox.Text = "Shuffle Bosses - place unique bosses in boss rooms";
            this.bosses_checkbox.UseVisualStyleBackColor = true;
            this.bosses_checkbox.CheckedChanged += new System.EventHandler(this.all_checkbox_CheckedChanged);
            // 
            // anyboss_checkbox
            // 
            this.anyboss_checkbox.AutoSize = true;
            this.anyboss_checkbox.Enabled = false;
            this.anyboss_checkbox.Location = new System.Drawing.Point(6, 88);
            this.anyboss_checkbox.Name = "anyboss_checkbox";
            this.anyboss_checkbox.Size = new System.Drawing.Size(263, 17);
            this.anyboss_checkbox.TabIndex = 2;
            this.anyboss_checkbox.Text = "Randomize Bosses - put any bosses in boss rooms";
            this.anyboss_checkbox.UseVisualStyleBackColor = true;
            this.anyboss_checkbox.CheckedChanged += new System.EventHandler(this.all_checkbox_CheckedChanged);
            // 
            // damage_checkbox
            // 
            this.damage_checkbox.AutoSize = true;
            this.damage_checkbox.Checked = true;
            this.damage_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.damage_checkbox.Location = new System.Drawing.Point(6, 42);
            this.damage_checkbox.Name = "damage_checkbox";
            this.damage_checkbox.Size = new System.Drawing.Size(165, 17);
            this.damage_checkbox.TabIndex = 1;
            this.damage_checkbox.Text = "Randomize Enemies Damage";
            this.damage_checkbox.UseVisualStyleBackColor = true;
            this.damage_checkbox.CheckedChanged += new System.EventHandler(this.all_checkbox_CheckedChanged);
            // 
            // hp_checkbox
            // 
            this.hp_checkbox.AutoSize = true;
            this.hp_checkbox.Checked = true;
            this.hp_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hp_checkbox.Location = new System.Drawing.Point(6, 19);
            this.hp_checkbox.Name = "hp_checkbox";
            this.hp_checkbox.Size = new System.Drawing.Size(140, 17);
            this.hp_checkbox.TabIndex = 0;
            this.hp_checkbox.Text = "Randomize Enemies HP";
            this.hp_checkbox.UseVisualStyleBackColor = true;
            this.hp_checkbox.CheckedChanged += new System.EventHandler(this.all_checkbox_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.extra_checkbox);
            this.groupBox2.Controls.Add(this.layout_checkbox);
            this.groupBox2.Controls.Add(this.randsprites_checkbox);
            this.groupBox2.Controls.Add(this.overworld_checkbox);
            this.groupBox2.Controls.Add(this.absorbable_checkbox);
            this.groupBox2.Controls.Add(this.gfx_checkbox);
            this.groupBox2.Controls.Add(this.floors_checkbox);
            this.groupBox2.Controls.Add(this.palettes_checkbox);
            this.groupBox2.Location = new System.Drawing.Point(299, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 205);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Global Settings";
            // 
            // extra_checkbox
            // 
            this.extra_checkbox.AutoSize = true;
            this.extra_checkbox.Enabled = false;
            this.extra_checkbox.Location = new System.Drawing.Point(6, 180);
            this.extra_checkbox.Name = "extra_checkbox";
            this.extra_checkbox.Size = new System.Drawing.Size(158, 17);
            this.extra_checkbox.TabIndex = 8;
            this.extra_checkbox.Text = "Add / Remove Extra Sprites";
            this.extra_checkbox.UseVisualStyleBackColor = true;
            this.extra_checkbox.CheckedChanged += new System.EventHandler(this.all_checkbox_CheckedChanged);
            // 
            // layout_checkbox
            // 
            this.layout_checkbox.AutoSize = true;
            this.layout_checkbox.Enabled = false;
            this.layout_checkbox.Location = new System.Drawing.Point(6, 157);
            this.layout_checkbox.Name = "layout_checkbox";
            this.layout_checkbox.Size = new System.Drawing.Size(166, 17);
            this.layout_checkbox.TabIndex = 7;
            this.layout_checkbox.Text = "Randomize Dungeons Layout";
            this.layout_checkbox.UseVisualStyleBackColor = true;
            this.layout_checkbox.CheckedChanged += new System.EventHandler(this.all_checkbox_CheckedChanged);
            // 
            // randsprites_checkbox
            // 
            this.randsprites_checkbox.AutoSize = true;
            this.randsprites_checkbox.Checked = true;
            this.randsprites_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.randsprites_checkbox.Enabled = false;
            this.randsprites_checkbox.Location = new System.Drawing.Point(6, 134);
            this.randsprites_checkbox.Name = "randsprites_checkbox";
            this.randsprites_checkbox.Size = new System.Drawing.Size(166, 17);
            this.randsprites_checkbox.TabIndex = 6;
            this.randsprites_checkbox.Text = "Randomize Dungeons Sprites";
            this.randsprites_checkbox.UseVisualStyleBackColor = true;
            this.randsprites_checkbox.CheckedChanged += new System.EventHandler(this.all_checkbox_CheckedChanged);
            // 
            // overworld_checkbox
            // 
            this.overworld_checkbox.AutoSize = true;
            this.overworld_checkbox.Enabled = false;
            this.overworld_checkbox.Location = new System.Drawing.Point(6, 111);
            this.overworld_checkbox.Name = "overworld_checkbox";
            this.overworld_checkbox.Size = new System.Drawing.Size(165, 17);
            this.overworld_checkbox.TabIndex = 5;
            this.overworld_checkbox.Text = "Randomize Overworld Sprites";
            this.overworld_checkbox.UseVisualStyleBackColor = true;
            this.overworld_checkbox.CheckedChanged += new System.EventHandler(this.all_checkbox_CheckedChanged);
            // 
            // absorbable_checkbox
            // 
            this.absorbable_checkbox.AutoSize = true;
            this.absorbable_checkbox.Checked = true;
            this.absorbable_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.absorbable_checkbox.Location = new System.Drawing.Point(6, 88);
            this.absorbable_checkbox.Name = "absorbable_checkbox";
            this.absorbable_checkbox.Size = new System.Drawing.Size(224, 17);
            this.absorbable_checkbox.TabIndex = 3;
            this.absorbable_checkbox.Text = "Put Absorbables items in the enemies pool";
            this.absorbable_checkbox.UseVisualStyleBackColor = true;
            this.absorbable_checkbox.CheckedChanged += new System.EventHandler(this.all_checkbox_CheckedChanged);
            // 
            // gfx_checkbox
            // 
            this.gfx_checkbox.AutoSize = true;
            this.gfx_checkbox.Enabled = false;
            this.gfx_checkbox.Location = new System.Drawing.Point(6, 65);
            this.gfx_checkbox.Name = "gfx_checkbox";
            this.gfx_checkbox.Size = new System.Drawing.Size(277, 17);
            this.gfx_checkbox.TabIndex = 2;
            this.gfx_checkbox.Text = "Randomize Dungeons Gfx - can looks heavy glitched";
            this.gfx_checkbox.UseVisualStyleBackColor = true;
            this.gfx_checkbox.CheckedChanged += new System.EventHandler(this.all_checkbox_CheckedChanged);
            // 
            // floors_checkbox
            // 
            this.floors_checkbox.AutoSize = true;
            this.floors_checkbox.Enabled = false;
            this.floors_checkbox.Location = new System.Drawing.Point(6, 42);
            this.floors_checkbox.Name = "floors_checkbox";
            this.floors_checkbox.Size = new System.Drawing.Size(181, 17);
            this.floors_checkbox.TabIndex = 1;
            this.floors_checkbox.Text = "Randomize Dungeons Floors Gfx";
            this.floors_checkbox.UseVisualStyleBackColor = true;
            this.floors_checkbox.CheckedChanged += new System.EventHandler(this.all_checkbox_CheckedChanged);
            // 
            // palettes_checkbox
            // 
            this.palettes_checkbox.AutoSize = true;
            this.palettes_checkbox.Checked = true;
            this.palettes_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.palettes_checkbox.Location = new System.Drawing.Point(6, 19);
            this.palettes_checkbox.Name = "palettes_checkbox";
            this.palettes_checkbox.Size = new System.Drawing.Size(172, 17);
            this.palettes_checkbox.TabIndex = 0;
            this.palettes_checkbox.Text = "Randomize Dungeons Palettes";
            this.palettes_checkbox.UseVisualStyleBackColor = true;
            this.palettes_checkbox.CheckedChanged += new System.EventHandler(this.all_checkbox_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(12, 262);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Set All On";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(407, 262);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "Patch ROM";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(404, 288);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 78);
            this.label3.TabIndex = 10;
            this.label3.Text = "The ROM must be patched before\r\ngenerating a seed ! to select a ROM\r\npress Patch " +
    "ROM Button wait at least\r\n2 seconds after you selected your \r\nROM DO NOT PATCH T" +
    "HE SAME\r\nROM TWICE";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(117, 330);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(99, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "Report Bug";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 330);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(99, 23);
            this.button5.TabIndex = 12;
            this.button5.Text = "Check Update";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            this.openFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog2_FileOk);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 370);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "Enemizer Beta 2.4";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox palette_e_checkbox;
        private System.Windows.Forms.CheckBox bosses_checkbox;
        private System.Windows.Forms.CheckBox anyboss_checkbox;
        private System.Windows.Forms.CheckBox damage_checkbox;
        private System.Windows.Forms.CheckBox hp_checkbox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox randsprites_checkbox;
        private System.Windows.Forms.CheckBox overworld_checkbox;
        private System.Windows.Forms.CheckBox absorbable_checkbox;
        private System.Windows.Forms.CheckBox gfx_checkbox;
        private System.Windows.Forms.CheckBox floors_checkbox;
        private System.Windows.Forms.CheckBox palettes_checkbox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox layout_checkbox;
        private System.Windows.Forms.CheckBox zerohp_checkbox;
        private System.Windows.Forms.CheckBox glitched_checkbox;
        private System.Windows.Forms.CheckBox extra_checkbox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
    }
}

