namespace DMEmu
{
    partial class Form4
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.keyboardCheck1 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.effectCheck1 = new System.Windows.Forms.CheckBox();
            this.effectCheck2 = new System.Windows.Forms.CheckBox();
            this.effectCheck3 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.equCheck1 = new System.Windows.Forms.CheckBox();
            this.equCheck2 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.styleCheck1 = new System.Windows.Forms.CheckBox();
            this.styleCheck2 = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.guideCheck1 = new System.Windows.Forms.CheckBox();
            this.guideCheck4 = new System.Windows.Forms.CheckBox();
            this.guideCheck2 = new System.Windows.Forms.CheckBox();
            this.guideCheck3 = new System.Windows.Forms.CheckBox();
            this.button11 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Teal;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(533, 25);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(470, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(26, 22);
            this.button2.TabIndex = 2;
            this.button2.Text = "-";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(502, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 22);
            this.button1.TabIndex = 1;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Teal;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "DMEmu - In-game settings editor";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label1_MouseMove);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(9, 269);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(71, 27);
            this.button3.TabIndex = 8;
            this.button3.Text = "Close";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(201, 276);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(295, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Note: You need restart the game to make settings take effect";
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::DMEmu.Properties.Resources.Keyboard;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.button10);
            this.panel2.Controls.Add(this.button9);
            this.panel2.Controls.Add(this.button8);
            this.panel2.Controls.Add(this.button7);
            this.panel2.Controls.Add(this.button6);
            this.panel2.Controls.Add(this.button5);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Location = new System.Drawing.Point(206, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(304, 199);
            this.panel2.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(207, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Keyboard";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(14, 160);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(30, 26);
            this.button4.TabIndex = 0;
            this.button4.Text = "S";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(52, 160);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(30, 26);
            this.button5.TabIndex = 1;
            this.button5.Text = "D";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(91, 160);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(30, 26);
            this.button6.TabIndex = 2;
            this.button6.Text = "F";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(132, 160);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(39, 26);
            this.button7.TabIndex = 3;
            this.button7.Text = "SPC";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(182, 160);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(30, 26);
            this.button8.TabIndex = 4;
            this.button8.Text = "J";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(220, 159);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(30, 26);
            this.button9.TabIndex = 5;
            this.button9.Text = "K";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(259, 159);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(30, 26);
            this.button10.TabIndex = 6;
            this.button10.Text = "L";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // keyboardCheck1
            // 
            this.keyboardCheck1.AutoSize = true;
            this.keyboardCheck1.ForeColor = System.Drawing.SystemColors.Control;
            this.keyboardCheck1.Location = new System.Drawing.Point(282, 36);
            this.keyboardCheck1.Name = "keyboardCheck1";
            this.keyboardCheck1.Size = new System.Drawing.Size(90, 17);
            this.keyboardCheck1.TabIndex = 12;
            this.keyboardCheck1.Text = "Test the keys";
            this.keyboardCheck1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(12, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Effect";
            // 
            // effectCheck1
            // 
            this.effectCheck1.AutoSize = true;
            this.effectCheck1.ForeColor = System.Drawing.SystemColors.Control;
            this.effectCheck1.Location = new System.Drawing.Point(15, 56);
            this.effectCheck1.Name = "effectCheck1";
            this.effectCheck1.Size = new System.Drawing.Size(82, 17);
            this.effectCheck1.TabIndex = 14;
            this.effectCheck1.Text = "Best - 2.5D ";
            this.effectCheck1.UseVisualStyleBackColor = true;
            // 
            // effectCheck2
            // 
            this.effectCheck2.AutoSize = true;
            this.effectCheck2.ForeColor = System.Drawing.SystemColors.Control;
            this.effectCheck2.Location = new System.Drawing.Point(112, 56);
            this.effectCheck2.Name = "effectCheck2";
            this.effectCheck2.Size = new System.Drawing.Size(52, 17);
            this.effectCheck2.TabIndex = 15;
            this.effectCheck2.Text = "Good";
            this.effectCheck2.UseVisualStyleBackColor = true;
            // 
            // effectCheck3
            // 
            this.effectCheck3.AutoSize = true;
            this.effectCheck3.ForeColor = System.Drawing.SystemColors.Control;
            this.effectCheck3.Location = new System.Drawing.Point(15, 79);
            this.effectCheck3.Name = "effectCheck3";
            this.effectCheck3.Size = new System.Drawing.Size(149, 17);
            this.effectCheck3.TabIndex = 16;
            this.effectCheck3.Text = "Do not use any 2.D Effect";
            this.effectCheck3.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(12, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 17);
            this.label5.TabIndex = 17;
            this.label5.Text = "Equalizer (F9)";
            // 
            // equCheck1
            // 
            this.equCheck1.AutoSize = true;
            this.equCheck1.ForeColor = System.Drawing.SystemColors.Control;
            this.equCheck1.Location = new System.Drawing.Point(15, 119);
            this.equCheck1.Name = "equCheck1";
            this.equCheck1.Size = new System.Drawing.Size(65, 17);
            this.equCheck1.TabIndex = 18;
            this.equCheck1.Text = "Enabled";
            this.equCheck1.UseVisualStyleBackColor = true;
            // 
            // equCheck2
            // 
            this.equCheck2.AutoSize = true;
            this.equCheck2.ForeColor = System.Drawing.SystemColors.Control;
            this.equCheck2.Location = new System.Drawing.Point(112, 119);
            this.equCheck2.Name = "equCheck2";
            this.equCheck2.Size = new System.Drawing.Size(67, 17);
            this.equCheck2.TabIndex = 19;
            this.equCheck2.Text = "Disabled";
            this.equCheck2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(12, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 17);
            this.label6.TabIndex = 20;
            this.label6.Text = "Note Style (F5)";
            // 
            // styleCheck1
            // 
            this.styleCheck1.AutoSize = true;
            this.styleCheck1.ForeColor = System.Drawing.SystemColors.Control;
            this.styleCheck1.Location = new System.Drawing.Point(15, 159);
            this.styleCheck1.Name = "styleCheck1";
            this.styleCheck1.Size = new System.Drawing.Size(60, 17);
            this.styleCheck1.TabIndex = 21;
            this.styleCheck1.Text = "Square";
            this.styleCheck1.UseVisualStyleBackColor = true;
            // 
            // styleCheck2
            // 
            this.styleCheck2.AutoSize = true;
            this.styleCheck2.ForeColor = System.Drawing.SystemColors.Control;
            this.styleCheck2.Location = new System.Drawing.Point(112, 159);
            this.styleCheck2.Name = "styleCheck2";
            this.styleCheck2.Size = new System.Drawing.Size(52, 17);
            this.styleCheck2.TabIndex = 22;
            this.styleCheck2.Text = "Circle";
            this.styleCheck2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.Control;
            this.label7.Location = new System.Drawing.Point(12, 179);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(148, 17);
            this.label7.TabIndex = 23;
            this.label7.Text = "Note Guide Line (F6)";
            // 
            // guideCheck1
            // 
            this.guideCheck1.AutoSize = true;
            this.guideCheck1.ForeColor = System.Drawing.SystemColors.Control;
            this.guideCheck1.Location = new System.Drawing.Point(15, 199);
            this.guideCheck1.Name = "guideCheck1";
            this.guideCheck1.Size = new System.Drawing.Size(50, 17);
            this.guideCheck1.TabIndex = 24;
            this.guideCheck1.Text = "Long";
            this.guideCheck1.UseVisualStyleBackColor = true;
            // 
            // guideCheck4
            // 
            this.guideCheck4.AutoSize = true;
            this.guideCheck4.ForeColor = System.Drawing.SystemColors.Control;
            this.guideCheck4.Location = new System.Drawing.Point(112, 225);
            this.guideCheck4.Name = "guideCheck4";
            this.guideCheck4.Size = new System.Drawing.Size(67, 17);
            this.guideCheck4.TabIndex = 25;
            this.guideCheck4.Text = "Disabled";
            this.guideCheck4.UseVisualStyleBackColor = true;
            // 
            // guideCheck2
            // 
            this.guideCheck2.AutoSize = true;
            this.guideCheck2.ForeColor = System.Drawing.SystemColors.Control;
            this.guideCheck2.Location = new System.Drawing.Point(112, 199);
            this.guideCheck2.Name = "guideCheck2";
            this.guideCheck2.Size = new System.Drawing.Size(57, 17);
            this.guideCheck2.TabIndex = 25;
            this.guideCheck2.Text = "Middle";
            this.guideCheck2.UseVisualStyleBackColor = true;
            // 
            // guideCheck3
            // 
            this.guideCheck3.AutoSize = true;
            this.guideCheck3.ForeColor = System.Drawing.SystemColors.Control;
            this.guideCheck3.Location = new System.Drawing.Point(15, 225);
            this.guideCheck3.Name = "guideCheck3";
            this.guideCheck3.Size = new System.Drawing.Size(51, 17);
            this.guideCheck3.TabIndex = 26;
            this.guideCheck3.Text = "Short";
            this.guideCheck3.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(86, 269);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(109, 27);
            this.button11.TabIndex = 27;
            this.button11.Text = "Volume settings";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(533, 308);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.guideCheck3);
            this.Controls.Add(this.guideCheck2);
            this.Controls.Add(this.guideCheck4);
            this.Controls.Add(this.guideCheck1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.styleCheck2);
            this.Controls.Add(this.styleCheck1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.equCheck2);
            this.Controls.Add(this.equCheck1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.effectCheck3);
            this.Controls.Add(this.effectCheck2);
            this.Controls.Add(this.effectCheck1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.keyboardCheck1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form4";
            this.Text = "DMEmu.Info";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox keyboardCheck1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox effectCheck1;
        private System.Windows.Forms.CheckBox effectCheck2;
        private System.Windows.Forms.CheckBox effectCheck3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox equCheck1;
        private System.Windows.Forms.CheckBox equCheck2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox styleCheck1;
        private System.Windows.Forms.CheckBox styleCheck2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox guideCheck1;
        private System.Windows.Forms.CheckBox guideCheck4;
        private System.Windows.Forms.CheckBox guideCheck2;
        private System.Windows.Forms.CheckBox guideCheck3;
        private System.Windows.Forms.Button button11;
    }
}