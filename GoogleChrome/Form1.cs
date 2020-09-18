using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace GoogleChrome
{
    public partial class Form1 : Form
    {
        private Thread chromeThread;
        private Work work;
        private List<string> keys;
        public Form1()
        {
            InitializeComponent();
            // 初始化任务
            InitListView();
        }

        private void InitListView()
        {
            taskView.Clear();
            keys = Utils.LoadKeys();
            taskView.View = View.Details;
            taskView.GridLines = true;
            taskView.LabelEdit = false;
            taskView.FullRowSelect = true;
            taskView.Columns.Add("编号", 50);
            taskView.Columns.Add("关键词", 150);
            taskView.Columns.Add("广告数量", 100);
            taskView.Columns.Add("快照数量", 100);
            taskView.Columns.Add("是否完成", 150);
            for (int i = 0; i < keys.Count; i++)
            {
                var item = new ListViewItem($"NO.{i + 1}");
                item.SubItems.Add(keys[i]);
                item.SubItems.Add($"{Setting.AdClickMin}-{Setting.AdClickMax}");
                item.SubItems.Add($"{Setting.SnapClickMin}-{Setting.SnapClickMax}");
                item.SubItems.Add("未完成");
                taskView.Items.Add(item);
            }
        }



        private void StartButton_Click(object sender, EventArgs e)
        {
            InitListView();
            Setting.Running = true;
            if (keys.Count < 1)
            {
                MessageBox.Show("请在设置中添加相应的关键字！", "错误");
                return;
            }
            // 开始运行
            UpdateButtonAction(true);

            work = new Work(keys);
            work.UpdateButtonAction = UpdateButtonAsync;
            work.PrintLogAction = PrintLogAsync;
            work.FinishTaskViewAction = FinishTaskViewAsync;
            work.UpdateTaskViewCountAction = UpdateTaskCountViewAsync;
            chromeThread = new Thread(new ThreadStart(work.Start));
            //chromeThread = new Thread(new ThreadStart(work.ChangeIP));
            chromeThread.Start();
        }


        private void load_button_Click(object sender, EventArgs e)
        {
            InitListView();
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            chromeThread.Abort();
            Setting.Running = false;
            UpdateButtonAction(false);
        }

        private void setting_button_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            new SettingForm().ShowDialog();
            this.TopMost = true;
        }

        
    }
}
