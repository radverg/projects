namespace Labyrinth
{
    partial class ConnectionForm
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox_host_ip = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_host_port = new System.Windows.Forms.TextBox();
            this.button_connect_to_game = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_create_host_port = new System.Windows.Forms.TextBox();
            this.button_create_game = new System.Windows.Forms.Button();
            this.textBox_player_name = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_my_ip = new System.Windows.Forms.Label();
            this.tb_my_ip = new System.Windows.Forms.TextBox();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox_host_ip);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.textBox_host_port);
            this.groupBox3.Controls.Add(this.button_connect_to_game);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(31, 139);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(228, 119);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Connnect to server";
            // 
            // textBox_host_ip
            // 
            this.textBox_host_ip.Location = new System.Drawing.Point(111, 31);
            this.textBox_host_ip.Name = "textBox_host_ip";
            this.textBox_host_ip.Size = new System.Drawing.Size(111, 20);
            this.textBox_host_ip.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "IP address:";
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
            this.textBox_host_port.Size = new System.Drawing.Size(111, 20);
            this.textBox_host_port.TabIndex = 3;
            // 
            // button_connect_to_game
            // 
            this.button_connect_to_game.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button_connect_to_game.ForeColor = System.Drawing.Color.Black;
            this.button_connect_to_game.Location = new System.Drawing.Point(6, 90);
            this.button_connect_to_game.Name = "button_connect_to_game";
            this.button_connect_to_game.Size = new System.Drawing.Size(216, 23);
            this.button_connect_to_game.TabIndex = 4;
            this.button_connect_to_game.Text = "Connect";
            this.button_connect_to_game.UseVisualStyleBackColor = false;
            this.button_connect_to_game.Click += new System.EventHandler(this.button_connect_to_game_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBox_create_host_port);
            this.groupBox2.Controls.Add(this.button_create_game);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(31, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(228, 81);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Create server";
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
            // textBox_create_host_port
            // 
            this.textBox_create_host_port.Location = new System.Drawing.Point(111, 19);
            this.textBox_create_host_port.Name = "textBox_create_host_port";
            this.textBox_create_host_port.Size = new System.Drawing.Size(111, 20);
            this.textBox_create_host_port.TabIndex = 3;
            // 
            // button_create_game
            // 
            this.button_create_game.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button_create_game.ForeColor = System.Drawing.Color.Black;
            this.button_create_game.Location = new System.Drawing.Point(6, 45);
            this.button_create_game.Name = "button_create_game";
            this.button_create_game.Size = new System.Drawing.Size(216, 23);
            this.button_create_game.TabIndex = 4;
            this.button_create_game.Text = "Create";
            this.button_create_game.UseVisualStyleBackColor = false;
            this.button_create_game.Click += new System.EventHandler(this.button_create_game_Click);
            // 
            // textBox_player_name
            // 
            this.textBox_player_name.Location = new System.Drawing.Point(142, 12);
            this.textBox_player_name.MaxLength = 10;
            this.textBox_player_name.Name = "textBox_player_name";
            this.textBox_player_name.Size = new System.Drawing.Size(111, 20);
            this.textBox_player_name.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(34, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Player name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(34, 274);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Your IP address:";
            // 
            // label_my_ip
            // 
            this.label_my_ip.AutoSize = true;
            this.label_my_ip.ForeColor = System.Drawing.Color.White;
            this.label_my_ip.Location = new System.Drawing.Point(125, 274);
            this.label_my_ip.Name = "label_my_ip";
            this.label_my_ip.Size = new System.Drawing.Size(0, 13);
            this.label_my_ip.TabIndex = 9;
            // 
            // tb_my_ip
            // 
            this.tb_my_ip.Location = new System.Drawing.Point(125, 271);
            this.tb_my_ip.Name = "tb_my_ip";
            this.tb_my_ip.Size = new System.Drawing.Size(128, 20);
            this.tb_my_ip.TabIndex = 11;
            // 
            // ConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(285, 303);
            this.Controls.Add(this.tb_my_ip);
            this.Controls.Add(this.label_my_ip);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_player_name);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ConnectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConnectionForm_FormClosing);
            this.Load += new System.EventHandler(this.ConnectionForm_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox_host_ip;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_host_port;
        private System.Windows.Forms.Button button_connect_to_game;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_create_host_port;
        private System.Windows.Forms.Button button_create_game;
        private System.Windows.Forms.TextBox textBox_player_name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_my_ip;
        private System.Windows.Forms.TextBox tb_my_ip;

    }
}