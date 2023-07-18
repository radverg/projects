namespace LabyrinthClient
{
    partial class BasicForm
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
            this.listBoxSet = new System.Windows.Forms.ListBox();
            this.buttonRun = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBoxLevel = new System.Windows.Forms.ListBox();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sadaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nováToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.přejmenovatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.odstranitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.konmataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nováToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.upravitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.odstranitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.posunoutNahoruToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.posunoutDolůToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nápovědaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nastaveníToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.buttonMulti = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxSet
            // 
            this.listBoxSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listBoxSet.FormattingEnabled = true;
            this.listBoxSet.ItemHeight = 20;
            this.listBoxSet.Location = new System.Drawing.Point(7, 29);
            this.listBoxSet.Name = "listBoxSet";
            this.listBoxSet.Size = new System.Drawing.Size(192, 184);
            this.listBoxSet.TabIndex = 0;
            this.listBoxSet.SelectedIndexChanged += new System.EventHandler(this.listBoxSet_SelectedIndexChanged);
            // 
            // buttonRun
            // 
            this.buttonRun.BackColor = System.Drawing.Color.LightCoral;
            this.buttonRun.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonRun.ForeColor = System.Drawing.Color.Snow;
            this.buttonRun.Location = new System.Drawing.Point(232, 279);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(199, 27);
            this.buttonRun.TabIndex = 1;
            this.buttonRun.Text = "Spusit hru";
            this.buttonRun.UseVisualStyleBackColor = false;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Brown;
            this.groupBox1.Controls.Add(this.listBoxSet);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 219);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sada";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Brown;
            this.groupBox2.Controls.Add(this.listBoxLevel);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(232, 43);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(205, 219);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Komnata";
            // 
            // listBoxLevel
            // 
            this.listBoxLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listBoxLevel.FormattingEnabled = true;
            this.listBoxLevel.ItemHeight = 20;
            this.listBoxLevel.Location = new System.Drawing.Point(7, 30);
            this.listBoxLevel.Name = "listBoxLevel";
            this.listBoxLevel.Size = new System.Drawing.Size(192, 184);
            this.listBoxLevel.TabIndex = 0;
            this.listBoxLevel.SelectedIndexChanged += new System.EventHandler(this.listBoxLevel_SelectedIndexChanged);
            // 
            // buttonEdit
            // 
            this.buttonEdit.BackColor = System.Drawing.Color.LightCoral;
            this.buttonEdit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonEdit.ForeColor = System.Drawing.Color.Snow;
            this.buttonEdit.Location = new System.Drawing.Point(232, 312);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(199, 27);
            this.buttonEdit.TabIndex = 4;
            this.buttonEdit.Text = "Editovat";
            this.buttonEdit.UseVisualStyleBackColor = false;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Bisque;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.menuStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sadaToolStripMenuItem,
            this.konmataToolStripMenuItem,
            this.nápovědaToolStripMenuItem,
            this.nastaveníToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.nápovědaToolStripMenuItem;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(10, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(453, 25);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sadaToolStripMenuItem
            // 
            this.sadaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nováToolStripMenuItem,
            this.přejmenovatToolStripMenuItem,
            this.odstranitToolStripMenuItem});
            this.sadaToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.sadaToolStripMenuItem.Name = "sadaToolStripMenuItem";
            this.sadaToolStripMenuItem.Size = new System.Drawing.Size(49, 21);
            this.sadaToolStripMenuItem.Text = "Sada";
            // 
            // nováToolStripMenuItem
            // 
            this.nováToolStripMenuItem.Name = "nováToolStripMenuItem";
            this.nováToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.nováToolStripMenuItem.Text = "Nová";
            this.nováToolStripMenuItem.Click += new System.EventHandler(this.nováToolStripMenuItem_Click);
            // 
            // přejmenovatToolStripMenuItem
            // 
            this.přejmenovatToolStripMenuItem.Name = "přejmenovatToolStripMenuItem";
            this.přejmenovatToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.přejmenovatToolStripMenuItem.Text = "Přejmenovat";
            this.přejmenovatToolStripMenuItem.Click += new System.EventHandler(this.přejmenovatToolStripMenuItem_Click);
            // 
            // odstranitToolStripMenuItem
            // 
            this.odstranitToolStripMenuItem.Name = "odstranitToolStripMenuItem";
            this.odstranitToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.odstranitToolStripMenuItem.Text = "Odstranit";
            this.odstranitToolStripMenuItem.Click += new System.EventHandler(this.odstranitToolStripMenuItem_Click);
            // 
            // konmataToolStripMenuItem
            // 
            this.konmataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nováToolStripMenuItem1,
            this.upravitToolStripMenuItem1,
            this.odstranitToolStripMenuItem1,
            this.posunoutNahoruToolStripMenuItem,
            this.posunoutDolůToolStripMenuItem});
            this.konmataToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.konmataToolStripMenuItem.Name = "konmataToolStripMenuItem";
            this.konmataToolStripMenuItem.Size = new System.Drawing.Size(72, 21);
            this.konmataToolStripMenuItem.Text = "Komnata";
            // 
            // nováToolStripMenuItem1
            // 
            this.nováToolStripMenuItem1.Name = "nováToolStripMenuItem1";
            this.nováToolStripMenuItem1.Size = new System.Drawing.Size(175, 22);
            this.nováToolStripMenuItem1.Text = "Nová";
            this.nováToolStripMenuItem1.Click += new System.EventHandler(this.nováToolStripMenuItem1_Click);
            // 
            // upravitToolStripMenuItem1
            // 
            this.upravitToolStripMenuItem1.Name = "upravitToolStripMenuItem1";
            this.upravitToolStripMenuItem1.Size = new System.Drawing.Size(175, 22);
            this.upravitToolStripMenuItem1.Text = "Upravit";
            // 
            // odstranitToolStripMenuItem1
            // 
            this.odstranitToolStripMenuItem1.Name = "odstranitToolStripMenuItem1";
            this.odstranitToolStripMenuItem1.Size = new System.Drawing.Size(175, 22);
            this.odstranitToolStripMenuItem1.Text = "Odstranit";
            // 
            // posunoutNahoruToolStripMenuItem
            // 
            this.posunoutNahoruToolStripMenuItem.Name = "posunoutNahoruToolStripMenuItem";
            this.posunoutNahoruToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.posunoutNahoruToolStripMenuItem.Text = "Posunout nahoru";
            this.posunoutNahoruToolStripMenuItem.Click += new System.EventHandler(this.posunoutNahoruToolStripMenuItem_Click);
            // 
            // posunoutDolůToolStripMenuItem
            // 
            this.posunoutDolůToolStripMenuItem.Name = "posunoutDolůToolStripMenuItem";
            this.posunoutDolůToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.posunoutDolůToolStripMenuItem.Text = "Posunout dolů";
            this.posunoutDolůToolStripMenuItem.Click += new System.EventHandler(this.posunoutDolůToolStripMenuItem_Click);
            // 
            // nápovědaToolStripMenuItem
            // 
            this.nápovědaToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.nápovědaToolStripMenuItem.Name = "nápovědaToolStripMenuItem";
            this.nápovědaToolStripMenuItem.Size = new System.Drawing.Size(81, 21);
            this.nápovědaToolStripMenuItem.Text = "Nápověda";
            // 
            // nastaveníToolStripMenuItem
            // 
            this.nastaveníToolStripMenuItem.Name = "nastaveníToolStripMenuItem";
            this.nastaveníToolStripMenuItem.Size = new System.Drawing.Size(77, 21);
            this.nastaveníToolStripMenuItem.Text = "Nastavení";
            this.nastaveníToolStripMenuItem.Click += new System.EventHandler(this.nastaveníToolStripMenuItem_Click);
            // 
            // gameOpenFileDialog
            // 
            this.gameOpenFileDialog.FileName = "openFileDialog1";
            // 
            // buttonMulti
            // 
            this.buttonMulti.BackColor = System.Drawing.Color.LightCoral;
            this.buttonMulti.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonMulti.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMulti.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonMulti.ForeColor = System.Drawing.Color.Snow;
            this.buttonMulti.Location = new System.Drawing.Point(12, 312);
            this.buttonMulti.Name = "buttonMulti";
            this.buttonMulti.Size = new System.Drawing.Size(199, 27);
            this.buttonMulti.TabIndex = 9;
            this.buttonMulti.Text = "Multiplayer";
            this.buttonMulti.UseVisualStyleBackColor = false;
            this.buttonMulti.Click += new System.EventHandler(this.buttonMulti_Click);
            // 
            // BasicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Brown;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(453, 359);
            this.Controls.Add(this.buttonMulti);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BasicForm";
            this.Text = "Labyrint";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BasicForm_FormClosing);
            this.Load += new System.EventHandler(this.BasicForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sadaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nováToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem přejmenovatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem odstranitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konmataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nováToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem upravitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem odstranitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem nápovědaToolStripMenuItem;
        public System.Windows.Forms.ListBox listBoxSet;
        public System.Windows.Forms.ListBox listBoxLevel;
        private System.Windows.Forms.ToolStripMenuItem nastaveníToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem posunoutNahoruToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem posunoutDolůToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog gameOpenFileDialog;
        private System.Windows.Forms.Button buttonMulti;
    }
}

