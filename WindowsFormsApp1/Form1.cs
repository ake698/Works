using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Thread chromeThread;
        private Work work;
        private List<string> keys;

        public Form1()
        {
            InitializeComponent();
            InitListView();
            Utils.LoadSetting();
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
            taskView.Columns.Add("网址", 220);
            taskView.Columns.Add("添加时间", 100);
            taskView.Columns.Add("完成数", 60);
            taskView.Columns.Add("是否完成", 100);
            for (int i = 0; i < keys.Count; i++)
            {
                var item = new ListViewItem($"NO.{i + 1}");
                item.SubItems.Add(keys[i]);
                item.SubItems.Add($"{DateTime.Now.ToString("HH:mm:ss")}");
                item.SubItems.Add("0");
                item.SubItems.Add("未完成");
                taskView.Items.Add(item);
            }
        }


        private void startButton_Click(object sender, EventArgs e)
        {
            InitListView();
            Utils.LoadSetting();
            Setting.Running = true;
            if (keys.Count < 1)
            {
                MessageBox.Show("请在设置中添加相应的任务列表！", "错误");
                return;
            }
            // 开始运行
            UpdateButtonAction(true);

            work = new Work(keys);
            work.UpdateButtonAction = UpdateButtonAsync;
            work.PrintLogAction = PrintLogAsync;
            work.TaskListViewAction = AddTaskListViewAsync;
            chromeThread = new Thread(new ThreadStart(work.Start));
            chromeThread.Start();


            
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

        private void close_button_Click(object sender, EventArgs e)
        {
            PrintLog("程序停止中...");
            Setting.Running = false;
            UpdateButtonAction(false);
            work.Dispose();
            chromeThread.Abort();
        }

        private void load_button_Click(object sender, EventArgs e)
        {
            logBox.Clear();
            taskView.Clear();
            InitListView();
        }
    }
}
