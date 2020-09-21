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
            taskView.Columns.Add("编号", 40);
            taskView.Columns.Add("网址", 200);
            taskView.Columns.Add("关键词", 90);
            taskView.Columns.Add("广告", 50);
            taskView.Columns.Add("目标", 50);
            taskView.Columns.Add("添加时间", 100);
            //for (int i = 0; i < keys.Count; i++)
            //{
            //    var item = new ListViewItem($"NO.{i + 1}");
            //    item.SubItems.Add(Setting.SearchFrom);
            //    item.SubItems.Add(keys[i]);
            //    item.SubItems.Add($"{Setting.AdClickMin}-{Setting.AdClickMax}");
            //    item.SubItems.Add($"{Setting.GlobalCount}");
            //    item.SubItems.Add("未完成");
            //    taskView.Items.Add(item);
            //}
            //PrintLog($"数据加载完成...本次搜索网址为{Setting.SearchFrom}");
        }



        private void StartButton_Click(object sender, EventArgs e)
        {
            Utils.LoadADSL();
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
            work.AddTaskListViewAction = AddTaskListViewAsync;
            chromeThread = new Thread(new ThreadStart(work.Start));
            //chromeThread = new Thread(new ThreadStart(work.ChangeIP));
            chromeThread.Start();
        }


        private void load_button_Click(object sender, EventArgs e)
        {
            //InitListView();
            logBox.Clear();
            taskView.Clear();
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            PrintLog("程序停止中...");
            Setting.Running = false;
            UpdateButtonAction(false);
            work.Dispose();
            chromeThread.Abort();


        }

        private void setting_button_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            new SettingForm().ShowDialog();
            this.TopMost = true;
        }

        private void logBox_TextChanged(object sender, EventArgs e)
        {
            logBox.ScrollToCaret();
        }
    }
}
