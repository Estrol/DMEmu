using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
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
            (new DropShadow()).ApplyShadows(this);
            this.CenterToScreen();
            Console.SetOut(new MultiTextWriter(new RichTextBoxWritter(this), Console.Out));
            Console.WriteLine("DMJam Singleplayer Server pre-release1");
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            session = new Session(this, 15010);
            Thread t = new Thread(session.Start);
            t.IsBackground = true;
            t.Start();
        }

        public async void WriteToForm(string text)
        {
            if (this.textBox1.InvokeRequired)
            {
                this.textBox1.Invoke(new MethodInvoker(delegate
                {
                    this.OnText(text);
                }));
            }
            else await this.OnText(text);
        }

        public Task<bool> OnText(string text)
        {
            this.textBox1.AppendText(text);
            return Task.FromResult(true);
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
            foreach (Form f in Application.OpenForms.Cast<Form>().ToArray())
            {
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
            }
            else
            {
                form2 = new Form2(this);
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
            if (!File.Exists(Application.StartupPath + @"\OTwo.exe")) {
                MessageBox msg = new MessageBox();
                msg.Show("Cannot find OTwo.exe.\n\nPlease check if OTwo.exe is present in game folder.", "Error");
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form5 info = new Form5();
            info.ShowDialog();
        }
    }

    public class MultiTextWriter : TextWriter
    {
        private IEnumerable<TextWriter> writers;
        public MultiTextWriter(IEnumerable<TextWriter> writers)
        {
            this.writers = writers.ToList();
        }

        public MultiTextWriter(params TextWriter[] writers)
        {
            this.writers = writers;
        }

        public override void Write(char value)
        {
            foreach (var writer in writers)
                writer.Write(value);
        }

        public override void Write(string value)
        {
            foreach (var writer in writers)
                writer.Write(value);
        }

        public override void Flush()
        {
            foreach (var writer in writers)
                writer.Flush();
        }

        public override void Close()
        {
            foreach (var writer in writers)
                writer.Close();
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }
#pragma warning disable CS4014
    /// <summary>
    /// Warning: this thing is a mess please send PR about better one.
    /// </summary>
    public class RichTextBoxWritter : TextWriter
    {
        public Form1 main_form;

        public RichTextBoxWritter(Form1 main)
        {
            this.main_form = main;
        }

        public override void Write(char value)
        {

            this.WriteL(value.ToString());
        }

        public override void Write(string value)
        {
            this.WriteL(value);
        }

        private Task<bool> WriteL(string val)
        {
            this.main_form.WriteToForm(val);

            return Task.FromResult(true);
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }
#pragma warning restore CS4014 
}
