namespace LabyrinthClient
{
    partial class NewLevelDialog
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
            this.button1_ok = new System.Windows.Forms.Button();
            this.button2_cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDown1_width = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown2_height = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2_height)).BeginInit();
            this.SuspendLayout();
            // 
            // button1_ok
            // 
            this.button1_ok.Location = new System.Drawing.Point(90, 148);
            this.button1_ok.Name = "button1_ok";
            this.button1_ok.Size = new System.Drawing.Size(72, 22);
            this.button1_ok.TabIndex = 0;
            this.button1_ok.Text = "OK";
            this.button1_ok.UseVisualStyleBackColor = true;
            this.button1_ok.Click += new System.EventHandler(this.button1_ok_Click);
            // 
            // button2_cancel
            // 
            this.button2_cancel.Location = new System.Drawing.Point(168, 148);
            this.button2_cancel.Name = "button2_cancel";
            this.button2_cancel.Size = new System.Drawing.Size(70, 22);
            this.button2_cancel.TabIndex = 1;
            this.button2_cancel.Text = "Storno";
            this.button2_cancel.UseVisualStyleBackColor = true;
            this.button2_cancel.Click += new System.EventHandler(this.button2_cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Název nového levelu:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDown2_height);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDown1_width);
            this.groupBox1.Location = new System.Drawing.Point(12, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 82);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rozměry";
            // 
            // numericUpDown1_width
            // 
            this.numericUpDown1_width.Location = new System.Drawing.Point(114, 19);
            this.numericUpDown1_width.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown1_width.Name = "numericUpDown1_width";
            this.numericUpDown1_width.Size = new System.Drawing.Size(106, 20);
            this.numericUpDown1_width.TabIndex = 4;
            this.numericUpDown1_width.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Šiřka:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Výška:";
            // 
            // numericUpDown2_height
            // 
            this.numericUpDown2_height.Location = new System.Drawing.Point(114, 45);
            this.numericUpDown2_height.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown2_height.Name = "numericUpDown2_height";
            this.numericUpDown2_height.Size = new System.Drawing.Size(106, 20);
            this.numericUpDown2_height.TabIndex = 6;
            this.numericUpDown2_height.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(126, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(112, 20);
            this.textBox1.TabIndex = 4;
            // 
            // NewLevelDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 182);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2_cancel);
            this.Controls.Add(this.button1_ok);
            this.Name = "NewLevelDialog";
            this.Text = "Nový level";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2_height)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1_ok;
        private System.Windows.Forms.Button button2_cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericUpDown2_height;
        public System.Windows.Forms.NumericUpDown numericUpDown1_width;
        public System.Windows.Forms.TextBox textBox1;
    }
}