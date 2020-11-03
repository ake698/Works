using System;
using System.Windows.Forms;

namespace UE_AD
{
    public partial class Form1
    {
        public Action<bool> UpdateButtonAction;
        public Action<string> PrintLogAction;


        private void PrintLog(string log)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            logBox.AppendText($"{time}: {log}");
            logBox.AppendText(Environment.NewLine);
            logBox.ScrollToCaret();
        }

        private void PrintLogAsync(string log) => this.Invoke(PrintLogAction, log);


        private void UpdateButton(Button button, bool open, string text = null)
        {
            button.Enabled = open;
            if (!string.IsNullOrEmpty(text)) button.Text = text;
        }

        private void UpdateButtonWhenStartAndStop(bool running)
        {
                UpdateButton(start_Button, !running);
                UpdateButton(stop_Button, running);
        }

        private void UpdateButtonAsync(bool running) => Invoke(UpdateButtonAction, running);


        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateButtonAction = UpdateButtonWhenStartAndStop;
            PrintLogAction = PrintLog;
            LoadSetting();
        }
    }
}
