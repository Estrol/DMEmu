using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DMEmu
{
    public partial class Form2 : Form
    {
        public Point mouseLocation;
        public string playerName;
        public Form1 mainForm;
        public int serverPort;
        public int webPort;

        public Form2(Form1 mainForm)
        {
            InitializeComponent();
            this.CenterToScreen();
            this.mainForm = mainForm;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            byte[] fileBuffer = File.ReadAllBytes(Application.StartupPath + @"\Spt\LauncherSettings.Spt");

            uint webPort = BitConverter.ToUInt16(fileBuffer, 21);
            uint gamePort = BitConverter.ToUInt16(fileBuffer, 27);

            Console.WriteLine("Port: {0}, web: {1}", gamePort, webPort);

            this.textBox2.Text = gamePort.ToString();
            this.textBox3.Text = webPort.ToString();

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

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 settingsDialog = new Form4();
            settingsDialog.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.mainForm.session.LoadData();
            byte[] MusicBuffer = File.ReadAllBytes(Application.StartupPath + @"\Spt\MusicList.Spt");
        }


        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
