using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoClick
{
    public partial class AutoClickProj : Form
    {

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const int LEFTUP = 0x0004;
        private const int LEFTDOWN = 0x0002;
        public int intervals = 5;
        public bool Click = false;
        public int parsedValue;

        public AutoClickProj()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Thread AC = new Thread(AutoClick);

            backgroundWorker1.RunWorkerAsync();

            AC.Start();
        }
        private void AutoClick()
        {
            while(true)
            {
                if (Click == true)
                {
                    mouse_event(dwFlags: LEFTUP, dx: 0, dy: 0, cButtons: 0, dwExtraInfo: 0);
                    Thread.Sleep(1);
                    mouse_event(dwFlags: LEFTDOWN, dx: 0, dy: 0, cButtons: 0, dwExtraInfo: 0);
                    Thread.Sleep(intervals);
                }
                Thread.Sleep(2);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {
                if (chckbxEnabled.Checked)
                {
                    if (GetAsyncKeyState(Keys.Down) < 0) // if key down is pressed = AutoClick Off
                    {
                        Click = false;
                    } else if (GetAsyncKeyState(Keys.Up) < 0) //if key up is pressed = AutoClick On
                    {
                        Click = true;
                    }
                    Thread.Sleep(1);
                }
                Thread.Sleep(1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out parsedValue))
            {
                MessageBox.Show("Error in interval: please put any avaliable value (this script just accept int/real numbers");
                return;
            } else
            {
                intervals = int.Parse(textBox1.Text);
                MessageBox.Show("New interval successfully added. You can use it again now!");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void AutoClickProj_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName("AutoClick")) // if I close the program = kill the background process
                {
                    proc.Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex); // if I close but the process not die = exception message
            }
        }
    }
}
