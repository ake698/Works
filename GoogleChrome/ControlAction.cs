using System;
using System.Windows.Forms;

namespace GoogleChrome
{
    public partial class Form1
    {
        public Action<bool> UpdateButtonAction;
        public Action<string> PrintLogAction;
        public Action<int> FinishTaskViewAction;
        public Action<int, int, string> AddTaskListViewAction;


        private void AddTaskListView(int index, int ad, string key)
        {
            var item = new ListViewItem($"NO.{index}");
            item.SubItems.Add(Setting.SearchFrom);
            item.SubItems.Add(key);
            item.SubItems.Add(ad.ToString());
            item.SubItems.Add($"{Setting.GlobalCount}");
            item.SubItems.Add($"{DateTime.Now.ToString("HH:mm:ss")}");
            taskView.Items.Add(item);
        }

        private void AddTaskListViewAsync(int index, int ad, string key) => this.Invoke(AddTaskListViewAction, index, ad, key);




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
            AddTaskListViewAction = AddTaskListView;
        }
    }
}
