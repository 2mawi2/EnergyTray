namespace EnergyTray.UI
{
    partial class SelectIconsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SelectIconButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxMultipleDisplays = new System.Windows.Forms.CheckBox();
            this.checkBoxPluggedIn = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Found Engergy Plans:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(18, 68);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(144, 24);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(279, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(42, 42);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // SelectIconButton
            // 
            this.SelectIconButton.Location = new System.Drawing.Point(229, 88);
            this.SelectIconButton.Name = "SelectIconButton";
            this.SelectIconButton.Size = new System.Drawing.Size(92, 24);
            this.SelectIconButton.TabIndex = 4;
            this.SelectIconButton.Text = "Select Icon";
            this.SelectIconButton.UseVisualStyleBackColor = true;
            this.SelectIconButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "selected icon:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(354, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(154, 21);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Auto Mode Enabled";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.checkBoxPluggedIn);
            this.panel1.Controls.Add(this.checkBoxMultipleDisplays);
            this.panel1.Location = new System.Drawing.Point(354, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(206, 78);
            this.panel1.TabIndex = 7;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // checkBoxMultipleDisplays
            // 
            this.checkBoxMultipleDisplays.AutoSize = true;
            this.checkBoxMultipleDisplays.Location = new System.Drawing.Point(13, 22);
            this.checkBoxMultipleDisplays.Name = "checkBoxMultipleDisplays";
            this.checkBoxMultipleDisplays.Size = new System.Drawing.Size(135, 21);
            this.checkBoxMultipleDisplays.TabIndex = 0;
            this.checkBoxMultipleDisplays.Text = "Multiple Displays";
            this.checkBoxMultipleDisplays.UseVisualStyleBackColor = true;
            this.checkBoxMultipleDisplays.CheckedChanged += new System.EventHandler(this.checkBoxMultipleDisplays_CheckedChanged);
            // 
            // checkBoxPluggedIn
            // 
            this.checkBoxPluggedIn.AutoSize = true;
            this.checkBoxPluggedIn.Location = new System.Drawing.Point(13, 48);
            this.checkBoxPluggedIn.Name = "checkBoxPluggedIn";
            this.checkBoxPluggedIn.Size = new System.Drawing.Size(97, 21);
            this.checkBoxPluggedIn.TabIndex = 1;
            this.checkBoxPluggedIn.Text = "Plugged in";
            this.checkBoxPluggedIn.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.checkBoxPluggedIn.UseVisualStyleBackColor = true;
            this.checkBoxPluggedIn.CheckedChanged += new System.EventHandler(this.checkBoxPluggedIn_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Conditions:";
            // 
            // SelectIconsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 124);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SelectIconButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SelectIconsForm";
            this.Text = "Select Icons";
            this.Load += new System.EventHandler(this.SelectIconsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button SelectIconButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxPluggedIn;
        private System.Windows.Forms.CheckBox checkBoxMultipleDisplays;
    }
}

