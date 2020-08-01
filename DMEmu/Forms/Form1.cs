using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMEmu
{
    public partial class Form1 : Form
    {
        public Point mouseLocation;
        public Form2 form2 = null;
        public Session session = null;

        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            session = new Session(this, 15010);
            Thread t = new Thread(session.Start);
            t.IsBackground = true;
            t.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Control control in this.panel1.Controls)
            {
                control.PreviewKeyDown += new PreviewKeyDownEventHandler(this.control_PreviewKeydown);
            }
        }

        public void control_PreviewKeydown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.IsInputKey = true;
            }
        }

        public void UpdateLoadingText(string text)
        {
            label4.Invoke((Action)delegate
            {
                label4.Text = text;
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms.Cast<Form>().ToArray()) { 
                if (f.Name != "Form1")
                {
                    f.Close();
                }
            }
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 info = new Form3();
            info.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (CheckForm("Form2"))
            {
                form2.Focus();
            } else
            {
                form2 = new Form2();
                form2.Show();
            }
        }

        private bool CheckForm(string name)
        {
            FormCollection fc = Application.OpenForms;
            foreach (Form f in fc)
            {
                if (f.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
