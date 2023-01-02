using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace oyun
{

    public partial class Form2 : Form
    {

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private Process proc;

        public Form2(IntPtr handle, string imagePath, Process process)
        {
            InitializeComponent();

            SetParent(this.Handle, handle);
            proc = process;
            this.Size = Screen.PrimaryScreen.Bounds.Size;
            pictureBox1.Dock = DockStyle.Fill;
            this.Location = new Point(0, 0);

            // Make the form topmost and give it focus
            this.TopMost = true;
            this.Focus();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Image.FromFile(imagePath);
        }
    }
}
