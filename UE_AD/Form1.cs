using System;
using System.Threading;
using System.Windows.Forms;

namespace UE_AD
{
    public partial class Form1 : Form
    {
        private Work work;
        private Thread chromeThread;

        public Form1()
        {
            InitializeComponent();
        }

        private void start_Button_Click(object sender, EventArgs e)
        {
            SaveSetting();
            //UpdateButtonAction(true);
            //work = new Work();
            //work.UpdateButtonAction = UpdateButtonAsync;
            //work.PrintLogAction = PrintLogAsync;
            //chromeThread = new Thread(new ThreadStart(work.Start));
            //chromeThread.Start();

            var work2 = new WapWork();
            work2.UpdateButtonAction = UpdateButtonAsync;
            work2.PrintLogAction = PrintLogAsync;
            new Thread(new ThreadStart(work2.Start)).Start();
        }

        private void stop_Button_Click(object sender, EventArgs e)
        {
            PrintLog("程序停止中...");
            UpdateButtonAction(false);
            work.Dispose();
            chromeThread.Abort();
        }

        private void SaveSetting()
        {
            Setting.ADSL = adsl_input.Text;
            Setting.ADSLUser = adsl_user_input.Text;
            Setting.ADSLPassword = adsl_password_input.Text;
            Utils.SaveSetting();
        }

        private void LoadSetting()
        {
            Utils.LoadSetting();
            adsl_input.Text = Setting.ADSL;
            adsl_user_input.Text = Setting.ADSLUser;
            adsl_password_input.Text = Setting.ADSLPassword;
        }

        private void logBox_TextChanged(object sender, EventArgs e)
        {
            logBox.ScrollToCaret();
        }
    }
}
