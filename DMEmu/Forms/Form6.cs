using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DMEmu
{
    public partial class Form6 : Form
    {
        public Point mouseLocation;
        public Form6()
        {
            InitializeComponent();
            (new DropShadow()).ApplyShadows(this);
            this.CenterToScreen();
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

        private int[] speedOffsets =
        {
            533478,
            533487,
            533496,
            533505,
            533514,
            533523,
            533532,
            533541,
            533550,
            533559
        };

        private void PatchSpeed()
        {
            try {
                IEnumerable<TextBox> textboxes = Utils.GetChildControls<TextBox>(this);

                int count = 0;
                foreach (TextBox tb in textboxes) {
                    bool isInt = int.TryParse(tb.Text, out int toRef);

                    if (isInt) {
                        int value = int.Parse(tb.Text);
                        if (Convert.ToSingle(value) > 0) {
                            this.ChangeSpeed(value, this.speedOffsets[count]);
                            count++;
                        }
                    } else {
                        MessageBox msg = new MessageBox();
                        msg.Show(string.Format("Textbox's Value {0} is not numberic type!", tb.Name), "Speed Patch Error");
                        break;
                    }
                }
            } catch (Exception err) {
                MessageBox msg = new MessageBox();
                msg.Show(err.Message, "Speed Patch Error");
            }
        }

        private void ChangeSpeed(float speed, long fileOffset)
        {
            try {
                Stream file = File.Open(Application.StartupPath + @"\OTwo.exe", FileMode.Open);
                BinaryReader br = new BinaryReader(file);
                br.BaseStream.Position = fileOffset;
                byte[] byteArray = BitConverter.GetBytes(speed);
                foreach (byte byteB in byteArray) {
                    if (byteB.ToString() == string.Empty) {
                        br.BaseStream.WriteByte(byteB);
                    }
                    else break;
                }
                br.Close();
            } catch (Exception err) {
                MessageBox msg = new MessageBox();
                msg.Show(err.Message, "Speed Patch Error");
            }
        }
    }
}
