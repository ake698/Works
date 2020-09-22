﻿using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1
    {
        public Action<bool> UpdateButtonAction;
        public Action<string> PrintLogAction;
        public Action<int> FinishTaskViewAction;
        public Action<int> TaskListViewAction;


        private void TaskListView(int index)
        {
            taskView.Items[index].SubItems[3].Text = "已完成";
        }

        private void AddTaskListViewAsync(int index) => this.Invoke(TaskListViewAction, index);




        private void PrintLog(string log)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            logBox.AppendText($"{time}: {log}");
            logBox.AppendText(Environment.NewLine);
        }

        private void PrintLogAsync(string log) => this.Invoke(PrintLogAction, log);


        private void UpdateButton(Button button, bool open, string text = null)
        {
            button.Enabled = open;
            if (!string.IsNullOrEmpty(text)) button.Text = text;
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
            TaskListViewAction = TaskListView;
        }
    }
}
