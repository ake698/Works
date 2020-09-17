using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleChrome
{
    public partial class Form1 : Form
    {
        private Thread chromeThread;
        private Work work;
        public Form1()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            work = new Work(new string[] { "我","教师"});
            chromeThread = new Thread(new ThreadStart(work.Start));
            chromeThread.Start();
        }


        private void stop_button_Click(object sender, EventArgs e)
        {
            chromeThread.Abort();
            work.Dispose();
            MessageBox.Show("ddd");
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            chromeThread.Abort();
            work.Dispose();
            MessageBox.Show("停止成功");
        }
    }
}
