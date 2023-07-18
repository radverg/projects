namespace LabyrinthClient
{
    partial class MultiplayerForm
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_create_host_port = new System.Windows.Forms.TextBox();
            this.button_create_game = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox_host_ip = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_host_port = new System.Windows.Forms.TextBox();
            this.button_connect_to_game = new System.Windows.Forms.Button();
            this.button_start = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(7, 19);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(208, 173);
            this.listBox1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(236, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 206);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lobby";
            // 
            // textBox_create_host_port
            // 
            this.textBox_create_host_port.Location = new System.Drawing.Point(111, 19);
            this.textBox_create_host_port.Name = "textBox_create_host_port";
            this.textBox_create_host_port.Size = new System.Drawing.Size(75, 20);
            this.textBox_create_host_port.TabIndex = 3;
            // 
            // button_create_game
            // 
            this.button_create_game.ForeColor = System.Drawing.Color.Black;
            this.button_create_game.Location = new System.Drawing.Point(6, 52);
            this.button_create_game.Name = "button_create_game";
            this.button_create_game.Size = new System.Drawing.Size(184, 23);
            this.button_create_game.TabIndex = 4;
            this.button_create_game.Text = "Vytvořit";
            this.button_create_game.UseVisualStyleBackColor = true;
            this.button_create_game.Click += new System.EventHandler(this.button_create_game_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBox_create_host_port);
            this.groupBox2.Controls.Add(this.button_create_game);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(196, 81);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Vytvořit server";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Port:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox_host_ip);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.textBox_host_port);
            this.groupBox3.Controls.Add(this.button_connect_to_game);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(12, 99);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(196, 119);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Připojit se na server";
            // 
            // textBox_host_ip
            // 
            this.textBox_host_ip.Location = new System.Drawing.Point(111, 31);
            this.textBox_host_ip.Name = "textBox_host_ip";
            this.textBox_host_ip.Size = new System.Drawing.Size(75, 20);
            this.textBox_host_ip.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "IP adresa:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port:";
            // 
            // textBox_host_port
            // 
            this.textBox_host_port.Location = new System.Drawing.Point(111, 57);
            this.textBox_host_port.Name = "textBox_host_port";
            this.textBox_host_port.Size = new System.Drawing.Size(75, 20);
            this.textBox_host_port.TabIndex = 3;
            // 
            // button_connect_to_game
            // 
            this.button_connect_to_game.ForeColor = System.Drawing.Color.Black;
            this.button_connect_to_game.Location = new System.Drawing.Point(6, 90);
            this.button_connect_to_game.Name = "button_connect_to_game";
            this.button_connect_to_game.Size = new System.Drawing.Size(184, 23);
            this.button_connect_to_game.TabIndex = 4;
            this.button_connect_to_game.Text = "Připojit";
            this.button_connect_to_game.UseVisualStyleBackColor = true;
            this.button_connect_to_game.Click += new System.EventHandler(this.button_connect_to_game_Click);
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(336, 226);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(126, 23);
            this.button_start.TabIndex = 8;
            this.button_start.Text = "Odstartovat";
            this.button_start.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(236, 226);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Check stream";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MultiplayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Brown;
            this.ClientSize = new System.Drawing.Size(471, 258);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MultiplayerForm";
            this.Text = "Multiplayer";
            this.Load += new System.EventHandler(this.MultiplayerForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_create_host_port;
        private System.Windows.Forms.Button button_create_game;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox_host_ip;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_host_port;
        private System.Windows.Forms.Button button_connect_to_game;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
    }
}