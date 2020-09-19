using System;
using System.Windows.Forms;

namespace GoogleChrome
{
    public partial class Form1
    {
        public Action<bool> UpdateButtonAction;
        public Action<string> PrintLogAction;
        public Action<int> FinishTaskViewAction;
        public Action<int, int, int> UpdateTaskViewCountAction;

        private void UpdateTaskCountView(int index, int ad, int snap)
        {
            taskView.Items[index].SubItems[3].Text = ad.ToString();
            taskView.Items[index].SubItems[4].Text = snap.ToString();
        }

        private void UpdateTaskCountViewAsync(int index, int ad, int snap) => this.Invoke(UpdateTaskViewCountAction, index, ad, snap);


        private void FinishTaskView(int index)
        {
            taskView.Items[index].SubItems[4].Text = "已完成";
        }

        private void FinishTaskViewAsync(int index) => this.Invoke(FinishTaskViewAction, index);


        private void PrintLog(string log)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            logBox.AppendText($"{time}: {log}");
            logBox.AppendText(Environment.NewLine);
        }

        private void PrintLogAsync(string log) => Invoke(PrintLogAction, log);


        private void UpdateButton(Button button, bool open, string text = null)
        {
            button.Enabled = open;
            if(!string.IsNullOrEmpty(text)) button.Text = text;
        }

        private void UpdateButtonWhenStartAndStop(bool running)
        {
            if (running)
            {
                UpdateButton(startButton, false);
                UpdateButton(setting_button, false);
                UpdateButton(load_button, false);
                UpdateButton(close_button, true);
            }
            else
            {
                UpdateButton(startButton, true);
                UpdateButton(setting_button, true);
                UpdateButton(load_button, true);
                UpdateButton(close_button, false);
            }
        }

        private void UpdateButtonAsync(bool running) => Invoke(UpdateButtonAction, running);


        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateButtonAction = UpdateButtonWhenStartAndStop;
            PrintLogAction = PrintLog;
            FinishTaskViewAction = FinishTaskView;
            UpdateTaskViewCountAction = UpdateTaskCountView;
        }
    }
}
