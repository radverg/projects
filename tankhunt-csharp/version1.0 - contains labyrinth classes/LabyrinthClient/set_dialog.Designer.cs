namespace LabyrinthClient
{
    partial class SetDialog
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1_full_screen = new System.Windows.Forms.CheckBox();
            this.numericUpDown1_width = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown2_height = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2_height)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(56, 92);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(137, 92);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Storno";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1_full_screen);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numericUpDown2_height);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDown1_width);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 73);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nastavení zobrazení";
            // 
            // checkBox1_full_screen
            // 
            this.checkBox1_full_screen.AutoSize = true;
            this.checkBox1_full_screen.Location = new System.Drawing.Point(9, 45);
            this.checkBox1_full_screen.Name = "checkBox1_full_screen";
            this.checkBox1_full_screen.Size = new System.Drawing.Size(122, 17);
            this.checkBox1_full_screen.TabIndex = 4;
            this.checkBox1_full_screen.Text = "Na celou obrazovku";
            this.checkBox1_full_screen.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1_width
            // 
            this.numericUpDown1_width.Location = new System.Drawing.Point(49, 19);
            this.numericUpDown1_width.Name = "numericUpDown1_width";
            this.numericUpDown1_width.Size = new System.Drawing.Size(48, 20);
            this.numericUpDown1_width.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Šířka:";
            // 
            // numericUpDown2_height
            // 
            this.numericUpDown2_height.Location = new System.Drawing.Point(147, 19);
            this.numericUpDown2_height.Name = "numericUpDown2_height";
            this.numericUpDown2_height.Size = new System.Drawing.Size(47, 20);
            this.numericUpDown2_height.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Výška:";
            // 
            // SetDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 127);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "SetDialog";
            this.Text = "Nastavení";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2_height)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.CheckBox checkBox1_full_screen;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUpDown2_height;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericUpDown1_width;
    }
}