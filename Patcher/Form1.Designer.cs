namespace Patcher
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
            this.patchButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // patchButton
            // 
            this.patchButton.Location = new System.Drawing.Point(12, 12);
            this.patchButton.Name = "patchButton";
            this.patchButton.Size = new System.Drawing.Size(444, 237);
            this.patchButton.TabIndex = 0;
            this.patchButton.Text = "Apply Patch";
            this.patchButton.UseVisualStyleBackColor = true;
            this.patchButton.Click += new System.EventHandler(this.patchButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 261);
            this.Controls.Add(this.patchButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "Patcher";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button patchButton;
    }
}

