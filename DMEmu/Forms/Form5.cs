using DMEmu.Data;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DMEmu
{
    public partial class Form5 : Form
    {
        public Point mouseLocation;
        public IniFile ini;
        public Form5()
        {
            InitializeComponent();
            (new DropShadow()).ApplyShadows(this);
            this.CenterToScreen();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            PlayerData data = new PlayerData(Application.StartupPath + @"\Spt\D007.Spt");

            ingameTextbox.Text = data.InGameName;
            numInstrument.Value = data.Instrument;
            numHair.Value = data.Hair;
            numAccessories.Value = data.Accessories;
            numGloves.Value = data.Gloves;
            numNecklaces.Value = data.Necklaces;
            numShirts.Value = data.Shirt;
            numPants.Value = data.Pants;
            numGlasses.Value = data.Glasses;
            numEarrings.Value = data.Earrings;
            numClothestAccessories.Value = data.ClothesAccessories;
            numShoes.Value = data.Shoes;
            numFaces.Value = data.Faces;
            numWings.Value = data.Wings;
            numInstruAccess.Value = data.InstrumentAccessories;
            numPets.Value = data.Pets;
            numHairAccesst.Value = data.HairAccessories;

            if (data.Gender == 0x01) {
                FemaleCheckbox.Checked = true;
            } else {
                MaleCheckbox.Checked = true;
            }

            numLevel.Value = data.Level;
        }

        private int GetValue(byte[] src, int offset1)
        {
            return BitConverter.ToInt16(src, offset1);
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
