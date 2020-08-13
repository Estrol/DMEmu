using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMEmu
{
    public partial class MessageBox : Form
    {
        public Point mouseLocation;
        public MessageBox()
        {
            InitializeComponent();
            (new DropShadow()).ApplyShadows(this);
            this.CenterToScreen();
            this.msgPanel.Hide();
            this.msgPanel2.Hide();
            this.imagePanel.Hide();
            this.resButton1.Hide();
            this.resButton2.Hide();
            this.resButton3.Hide();
        }

        public int Show(string msg)
        {
            this.label1.Text = string.Format("DMEmu - {0}", "Info");
            this.labelText2.Text = msg;
            this.resButton1.Text = "Ok";
            this.resButton1.Show();
            this.msgPanel2.Show();

            this.ShowDialog();
            return 0;
        }

        public int Show(string msg, string title)
        {
            this.label1.Text = string.Format("DMEmu - {0}", title);
            this.labelText2.Text = msg;
            this.resButton1.Text = "Ok";
            this.resButton1.Show();
            this.msgPanel2.Show();

            this.ShowDialog();
            return 0;
        }

        public int Show(string msg, string title, string type)
        {
            switch (type)
            {
                case "OK":
                    {
                        this.resButton1.Text = "Ok";
                        this.resButton1.Show();
                        break;
                    }

                case "RETRYABORT":
                    {
                        this.resButton1.Text = "Abort";
                        this.resButton1.Show();
                        this.resButton2.Text = "Retry";
                        this.resButton2.Show();
                        break;
                    }

                case "RETRY":
                    {
                        break;
                    }

                case "RETRYABORTCANCEL":
                    {
                        this.resButton1.Text = "Cancel";
                        this.resButton1.Show();
                        this.resButton2.Text = "Abort";
                        this.resButton2.Show();
                        this.resButton3.Text = "Retry";
                        this.resButton3.Show();
                        break;
                    }

                case "OKCANCEL":
                    {
                        this.resButton1.Text = "Cancel";
                        this.resButton1.Show();
                        this.resButton2.Text = "Ok";
                        this.resButton2.Show();
                        break;
                    }

                default:
                    {
                        throw new ArgumentException(string.Format("Argument type {0} is invalid", type));
                    }
            }

            this.label1.Text = string.Format("DMEmu - {0}", title);

            this.ShowDialog();
            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
