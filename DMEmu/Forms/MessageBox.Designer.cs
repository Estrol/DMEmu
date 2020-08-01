namespace DMEmu
{
    partial class MessageBox
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
            this.resButton1 = new System.Windows.Forms.Button();
            this.resButton2 = new System.Windows.Forms.Button();
            this.resButton3 = new System.Windows.Forms.Button();
            this.msgPanel = new System.Windows.Forms.Panel();
            this.imagePanel = new System.Windows.Forms.Panel();
            this.labelText = new System.Windows.Forms.Label();
            this.msgPanel2 = new System.Windows.Forms.Panel();
            this.labelText2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.msgPanel.SuspendLayout();
            this.msgPanel2.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(386, 25);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(325, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(26, 22);
            this.button2.TabIndex = 2;
            this.button2.Text = "-";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(357, 1);
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
            this.label1.Size = new System.Drawing.Size(85, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "DMEmu - TBA";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label1_MouseMove);
            // 
            // resButton1
            // 
            this.resButton1.Location = new System.Drawing.Point(291, 150);
            this.resButton1.Name = "resButton1";
            this.resButton1.Size = new System.Drawing.Size(83, 27);
            this.resButton1.TabIndex = 8;
            this.resButton1.Text = "OK/Cancel";
            this.resButton1.UseVisualStyleBackColor = true;
            this.resButton1.Click += new System.EventHandler(this.button3_Click);
            // 
            // resButton2
            // 
            this.resButton2.Location = new System.Drawing.Point(208, 150);
            this.resButton2.Name = "resButton2";
            this.resButton2.Size = new System.Drawing.Size(77, 27);
            this.resButton2.TabIndex = 9;
            this.resButton2.Text = "OK/Retry";
            this.resButton2.UseVisualStyleBackColor = true;
            // 
            // resButton3
            // 
            this.resButton3.Location = new System.Drawing.Point(125, 150);
            this.resButton3.Name = "resButton3";
            this.resButton3.Size = new System.Drawing.Size(77, 27);
            this.resButton3.TabIndex = 10;
            this.resButton3.Text = "Abort";
            this.resButton3.UseVisualStyleBackColor = true;
            // 
            // msgPanel
            // 
            this.msgPanel.Controls.Add(this.labelText);
            this.msgPanel.Location = new System.Drawing.Point(83, 34);
            this.msgPanel.Name = "msgPanel";
            this.msgPanel.Size = new System.Drawing.Size(290, 105);
            this.msgPanel.TabIndex = 11;
            // 
            // imagePanel
            // 
            this.imagePanel.Location = new System.Drawing.Point(12, 61);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(65, 59);
            this.imagePanel.TabIndex = 12;
            // 
            // labelText
            // 
            this.labelText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelText.ForeColor = System.Drawing.SystemColors.Control;
            this.labelText.Location = new System.Drawing.Point(0, 0);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(290, 105);
            this.labelText.TabIndex = 0;
            this.labelText.Text = "label2";
            this.labelText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // msgPanel2
            // 
            this.msgPanel2.Controls.Add(this.labelText2);
            this.msgPanel2.Location = new System.Drawing.Point(12, 34);
            this.msgPanel2.Name = "msgPanel2";
            this.msgPanel2.Size = new System.Drawing.Size(361, 104);
            this.msgPanel2.TabIndex = 13;
            // 
            // labelText2
            // 
            this.labelText2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelText2.ForeColor = System.Drawing.SystemColors.Control;
            this.labelText2.Location = new System.Drawing.Point(0, 0);
            this.labelText2.Name = "labelText2";
            this.labelText2.Size = new System.Drawing.Size(361, 104);
            this.labelText2.TabIndex = 1;
            this.labelText2.Text = "label3";
            this.labelText2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(386, 193);
            this.Controls.Add(this.msgPanel2);
            this.Controls.Add(this.imagePanel);
            this.Controls.Add(this.msgPanel);
            this.Controls.Add(this.resButton3);
            this.Controls.Add(this.resButton2);
            this.Controls.Add(this.resButton1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MessageBox";
            this.Text = "DMEmu.Error";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.msgPanel.ResumeLayout(false);
            this.msgPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button resButton1;
        private System.Windows.Forms.Button resButton2;
        private System.Windows.Forms.Button resButton3;
        private System.Windows.Forms.Panel msgPanel;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Panel imagePanel;
        private System.Windows.Forms.Panel msgPanel2;
        private System.Windows.Forms.Label labelText2;
    }
}