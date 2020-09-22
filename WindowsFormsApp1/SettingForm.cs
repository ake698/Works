using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
            InitSettingUI();
        }

        private void InitSettingUI()
        {
            Utils.LoadSetting();
            Utils.AddReadme();

            grep_ip_input.Text = Setting.IPCheckDays.ToString();
            grep_ip_checkbox.Checked = Setting.CheckRepeatIP;

            adsl_input.Text = Setting.ADSL;
            adsl_user_input.Text = Setting.ADSLUser;
            adsl_password_input.Text = Setting.ADSLPassword;

            site_stay_input1.Text = Setting.SiteStayMin.ToString();
            site_stay_input2.Text = Setting.SiteStayMax.ToString();

            ad_staytime_input1.Text = Setting.AdStayMin.ToString();
            ad_staytime_input2.Text = Setting.AdStayMax.ToString();

            task_input.Text = Setting.TaskInput.ToString();

            // 为更新统计数量
            Utils.LoadKeys();
            key_count_label.Text = Setting.KeyCount.ToString();
        }

        private void edit_button_Click(object sender, EventArgs e)
        {
            Utils.FileHanler(Setting.KeyFileName);
            Utils.ExecuteCommand($"notepad {Setting.KeyPath}");
        }

        private void count_button_Click(object sender, EventArgs e)
        {
            Utils.LoadKeys();
            key_count_label.Text = Setting.KeyCount.ToString();
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int GetIntSettingValue(TextBox textBox, int minValue = 0)
        {
            int result = -1;
            var value = textBox.Text;
            int.TryParse(value, out result);
            if (result < minValue) result = minValue + 1;
            return result;
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            Setting.CheckRepeatIP = grep_ip_checkbox.Checked;
            Setting.IPCheckDays = GetIntSettingValue(grep_ip_input);

            Setting.ADSL = adsl_input.Text;
            Setting.ADSLUser = adsl_user_input.Text;
            Setting.ADSLPassword = adsl_password_input.Text;

            Setting.SiteStayMin = GetIntSettingValue(site_stay_input1);
            Setting.SiteStayMax = GetIntSettingValue(site_stay_input2, Setting.SiteStayMin);
            Setting.AdStayMin = GetIntSettingValue(ad_staytime_input1);
            Setting.AdStayMax = GetIntSettingValue(ad_staytime_input2, Setting.AdStayMin);
            Setting.TaskInput = GetIntSettingValue(task_input);

            Utils.SaveSetting();
            MessageBox.Show("保存成功！", "保存设置");
        }
    }
}
