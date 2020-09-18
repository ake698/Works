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
            adsl_input.Text = Setting.ADSL;
            connect_staytime_input.Text = Setting.ConnectStay.ToString();
            disconnect_staytime_input.Text = Setting.DisConnectStay.ToString();
            search_staytime_input1.Text = Setting.SearchStayMin.ToString();
            search_staytime_input2.Text = Setting.SearchStayMax.ToString();
            ad_click_input1.Text = Setting.AdClickMin.ToString();
            ad_click_input2.Text = Setting.AdClickMax.ToString();
            ad_staytime_input1.Text = Setting.AdStayMin.ToString();
            ad_staytime_input2.Text = Setting.AdStayMax.ToString();
            snap_staytime_input1.Text = Setting.SnapStayMin.ToString();
            snap_staytime_input2.Text = Setting.SnapStayMax.ToString();
        }


        private int GetIntSettingValue(TextBox textBox, int minValue = 0)
        {
            int result = -1;
            var value = textBox.Text;
            int.TryParse(value, out result);
            if (result < minValue) result = minValue + 2;
            return result;
        }


        private void save_button_Click(object sender, EventArgs e)
        {
            Setting.ADSL = adsl_input.Text;
            Setting.ConnectStay = GetIntSettingValue(connect_staytime_input);
            Setting.DisConnectStay = GetIntSettingValue(disconnect_staytime_input);
            Setting.SearchStayMin = GetIntSettingValue(search_staytime_input1);
            Setting.SearchStayMax = GetIntSettingValue(search_staytime_input2, Setting.SearchStayMin);
            Setting.AdClickMin = GetIntSettingValue(ad_click_input1);
            Setting.AdClickMax = GetIntSettingValue(ad_click_input2, Setting.AdClickMin);
            Setting.AdStayMin = GetIntSettingValue(ad_staytime_input1);
            Setting.AdStayMax = GetIntSettingValue(ad_staytime_input2, Setting.AdStayMin);
            Setting.SnapStayMin = GetIntSettingValue(snap_staytime_input1);
            Setting.SnapStayMax = GetIntSettingValue(snap_staytime_input2, Setting.SnapStayMin);

            if (random_radio.Checked) Setting.Normal = false;

            MessageBox.Show("保存成功！","保存设置");
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        


        private void edit_button_Click(object sender, EventArgs e)
        {
            Utils.FileHanler();
            Utils.Execute($"notepad {Setting.KeyPath}");
        }
    }
}
