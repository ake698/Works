using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace GoogleChrome
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
            click_limit_input.Text = Setting.ClickLimit.ToString();
            
            search_staytime_input1.Text = Setting.SearchStayMin.ToString();
            search_staytime_input2.Text = Setting.SearchStayMax.ToString();
            ad_click_input1.Text = Setting.AdClickMin.ToString();
            ad_click_input2.Text = Setting.AdClickMax.ToString();
            ad_staytime_input1.Text = Setting.AdStayMin.ToString();
            ad_staytime_input2.Text = Setting.AdStayMax.ToString();

            snap_staytime_input1.Text = Setting.SnapStayMin.ToString();
            snap_staytime_input2.Text = Setting.SnapStayMax.ToString();
            snap_click_input1.Text = Setting.SnapClickMin.ToString();
            snap_click_input2.Text = Setting.SnapClickMax.ToString();

            // 为更新统计数量
            Utils.LoadKeys();
            key_count_label.Text = Setting.KeyCount.ToString();
            gloable_count_input.Text = Setting.GlobalCount.ToString();
            random_radio.Checked = !Setting.Normal;
            normal_radio.Checked = Setting.Normal;

            search_from_input.Text = Setting.SearchFrom;
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
            if (string.IsNullOrEmpty(adsl_input.Text) || string.IsNullOrEmpty(adsl_user_input.Text) || string.IsNullOrEmpty(adsl_password_input.Text))
            {
                MessageBox.Show("ADSL信息不能为空", "错误");
                return;
            }

            Setting.CheckRepeatIP = grep_ip_checkbox.Checked;
            Setting.IPCheckDays = GetIntSettingValue(grep_ip_input);


            Setting.ADSL = adsl_input.Text;
            Setting.ADSLUser = adsl_user_input.Text;
            Setting.ADSLPassword = adsl_password_input.Text;

            Setting.SiteStayMin = GetIntSettingValue(site_stay_input1);
            Setting.SiteStayMax = GetIntSettingValue(site_stay_input2, Setting.SiteStayMin);
            Setting.ClickLimit = GetIntSettingValue(click_limit_input);
            Setting.SearchStayMin = GetIntSettingValue(search_staytime_input1);
            Setting.SearchStayMax = GetIntSettingValue(search_staytime_input2, Setting.SearchStayMin);

            Setting.AdClickMin = GetIntSettingValue(ad_click_input1);
            Setting.AdClickMax = GetIntSettingValue(ad_click_input2, Setting.AdClickMin);
            Setting.AdStayMin = GetIntSettingValue(ad_staytime_input1);
            Setting.AdStayMax = GetIntSettingValue(ad_staytime_input2, Setting.AdStayMin);

            Setting.SnapStayMin = GetIntSettingValue(snap_staytime_input1);
            Setting.SnapStayMax = GetIntSettingValue(snap_staytime_input2, Setting.SnapStayMin);
            Setting.SnapClickMin = GetIntSettingValue(snap_click_input1);
            Setting.SnapClickMax = GetIntSettingValue(snap_click_input2, Setting.SnapClickMin);

            Setting.Normal = normal_radio.Checked;
            Setting.SearchFrom = search_from_input.Text;
            Setting.GlobalCount = GetIntSettingValue(gloable_count_input);

            if (random_radio.Checked) Setting.Normal = false;

            Utils.SaveSetting();
            MessageBox.Show("保存成功！","保存设置");
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
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

    }
}
