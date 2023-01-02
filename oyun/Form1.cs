using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace oyun
{
    public partial class Form1 : Form
    {

        private string gamename = "";
        private string uygName = "";
        private string type = "site aç";
        private string imagePath = "";
        private int closeIndex = 0;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.Z.GetHashCode());
            type = radioButton1.Text;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Unregister the global hotkey when the form is closing
            UnregisterHotKey(this.Handle, 1);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0312 && m.WParam.ToInt32() == 1)
            {
                // The hotkey has been pressed
                // You can add your code here to handle the input

                string processName = gamename.Replace(".exe", "");
                Process process = Process.GetProcessesByName(processName)[0];
                if (closeIndex == 0)
                {
                    IntPtr handle = process.MainWindowHandle;
                    Form2 form2 = new Form2(handle, imagePath, process);
                    form2.Show();
                }
                else
                {
                    if (type == radioButton1.Text)
                    {
                        Process.Start(textBox2.Text, textBox1.Text);
                    }
                    else
                    {
                        Process.Start(uygName);
                    }
                    process.Kill();
                    Thread.Sleep(1031);
                    Application.Exit();
                }
                closeIndex++;
            }
        }

        public enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                // The radio button was just checked, so you can do something with it here
                type = radioButton.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Oyunlar (*.exe)|*.exe|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // The user selected a file, so you can do something with it here
                gamename = openFileDialog1.SafeFileName;
                label1.Text= Path.GetFileName(gamename);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Oyunlar (*.exe)|*.exe|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // The user selected a file, so you can do something with it here
                label3.Text = openFileDialog1.SafeFileName;
                uygName = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)//kaydet
        {

            var today = DateTime.Now;
            string aa = today.ToString();
            string aa2 = aa.Replace(" ", "_");
            string aa3 = aa2.Replace(":", ".");

            string fileName = aa3 + "flovTürkSave.txt";
            string pathName = Path.Combine(Environment.CurrentDirectory, fileName);

            string[] lines = new string[]
            {
            gamename,
            uygName,
            textBox1.Text,
            type,
            imagePath,
            textBox2.Text
            };

            using (StreamWriter sw = File.CreateText(pathName)) ;
            File.WriteAllLines(pathName, lines);
        }

        private void button4_Click(object sender, EventArgs e)//kayıt aç
        {
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;

                // Read the contents of the file into a string
                string[] lines = File.ReadLines(filePath).ToArray();
                gamename = lines[0];
                uygName = lines[1];
                textBox1.Text = lines[2];
                type = lines[3];
                imagePath = lines[4];
                textBox2.Text = lines[5];
                if (type == radioButton1.Text) 
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                }
                else
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                }
                label3.Text = uygName;
                label1.Text = Path.GetFileName(gamename);
                pictureBox1.Image = Image.FromFile(imagePath);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                imagePath = openFileDialog1.FileName;
            }
        }
    }
}
