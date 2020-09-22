namespace WindowsFormsApp1
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.adsl_setting = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.ad_staytime_input2 = new System.Windows.Forms.TextBox();
            this.ad_staytime_input1 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.site_stay_input2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.site_stay_input1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.adsl_password_input = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.adsl_user_input = new System.Windows.Forms.TextBox();
            this.adsl_input = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ip_gourp = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grep_ip_input = new System.Windows.Forms.TextBox();
            this.grep_ip_checkbox = new System.Windows.Forms.CheckBox();
            this.task_setting = new System.Windows.Forms.GroupBox();
            this.key_count_label = new System.Windows.Forms.Label();
            this.splite_label1 = new System.Windows.Forms.Label();
            this.count_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.edit_button = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.task_input = new System.Windows.Forms.TextBox();
            this.adsl_setting.SuspendLayout();
            this.ip_gourp.SuspendLayout();
            this.task_setting.SuspendLayout();
            this.SuspendLayout();
            // 
            // adsl_setting
            // 
            this.adsl_setting.Controls.Add(this.task_input);
            this.adsl_setting.Controls.Add(this.label7);
            this.adsl_setting.Controls.Add(this.label13);
            this.adsl_setting.Controls.Add(this.ad_staytime_input2);
            this.adsl_setting.Controls.Add(this.ad_staytime_input1);
            this.adsl_setting.Controls.Add(this.label14);
            this.adsl_setting.Controls.Add(this.label11);
            this.adsl_setting.Controls.Add(this.site_stay_input2);
            this.adsl_setting.Controls.Add(this.label4);
            this.adsl_setting.Controls.Add(this.site_stay_input1);
            this.adsl_setting.Controls.Add(this.label6);
            this.adsl_setting.Controls.Add(this.adsl_password_input);
            this.adsl_setting.Controls.Add(this.label5);
            this.adsl_setting.Controls.Add(this.adsl_user_input);
            this.adsl_setting.Controls.Add(this.adsl_input);
            this.adsl_setting.Controls.Add(this.label3);
            this.adsl_setting.Location = new System.Drawing.Point(208, 26);
            this.adsl_setting.Name = "adsl_setting";
            this.adsl_setting.Size = new System.Drawing.Size(225, 214);
            this.adsl_setting.TabIndex = 2;
            this.adsl_setting.TabStop = false;
            this.adsl_setting.Text = "参数设置";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(144, 154);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 12);
            this.label13.TabIndex = 34;
            this.label13.Text = "--";
            // 
            // ad_staytime_input2
            // 
            this.ad_staytime_input2.Location = new System.Drawing.Point(168, 149);
            this.ad_staytime_input2.Name = "ad_staytime_input2";
            this.ad_staytime_input2.Size = new System.Drawing.Size(44, 21);
            this.ad_staytime_input2.TabIndex = 33;
            this.ad_staytime_input2.Text = "5";
            this.ad_staytime_input2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ad_staytime_input1
            // 
            this.ad_staytime_input1.Location = new System.Drawing.Point(98, 149);
            this.ad_staytime_input1.Name = "ad_staytime_input1";
            this.ad_staytime_input1.Size = new System.Drawing.Size(40, 21);
            this.ad_staytime_input1.TabIndex = 32;
            this.ad_staytime_input1.Text = "3";
            this.ad_staytime_input1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 150);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 12);
            this.label14.TabIndex = 31;
            this.label14.Text = "广告页停留:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(144, 119);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 23;
            this.label11.Text = "--";
            // 
            // site_stay_input2
            // 
            this.site_stay_input2.Location = new System.Drawing.Point(168, 115);
            this.site_stay_input2.Name = "site_stay_input2";
            this.site_stay_input2.Size = new System.Drawing.Size(44, 21);
            this.site_stay_input2.TabIndex = 22;
            this.site_stay_input2.Text = "5";
            this.site_stay_input2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "ADSL账号:";
            // 
            // site_stay_input1
            // 
            this.site_stay_input1.Location = new System.Drawing.Point(98, 115);
            this.site_stay_input1.Name = "site_stay_input1";
            this.site_stay_input1.Size = new System.Drawing.Size(40, 21);
            this.site_stay_input1.TabIndex = 13;
            this.site_stay_input1.Text = "3";
            this.site_stay_input1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "网站停留:";
            // 
            // adsl_password_input
            // 
            this.adsl_password_input.Location = new System.Drawing.Point(98, 85);
            this.adsl_password_input.Name = "adsl_password_input";
            this.adsl_password_input.Size = new System.Drawing.Size(114, 21);
            this.adsl_password_input.TabIndex = 11;
            this.adsl_password_input.Text = "294085";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "ADSL密码:";
            // 
            // adsl_user_input
            // 
            this.adsl_user_input.Location = new System.Drawing.Point(98, 55);
            this.adsl_user_input.Name = "adsl_user_input";
            this.adsl_user_input.Size = new System.Drawing.Size(114, 21);
            this.adsl_user_input.TabIndex = 9;
            this.adsl_user_input.Text = "hh27ad113";
            // 
            // adsl_input
            // 
            this.adsl_input.Location = new System.Drawing.Point(98, 25);
            this.adsl_input.Name = "adsl_input";
            this.adsl_input.Size = new System.Drawing.Size(114, 21);
            this.adsl_input.TabIndex = 7;
            this.adsl_input.Text = "adsl";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "ADSL名:";
            // 
            // ip_gourp
            // 
            this.ip_gourp.Controls.Add(this.label19);
            this.ip_gourp.Controls.Add(this.label2);
            this.ip_gourp.Controls.Add(this.grep_ip_input);
            this.ip_gourp.Controls.Add(this.grep_ip_checkbox);
            this.ip_gourp.Location = new System.Drawing.Point(9, 145);
            this.ip_gourp.Name = "ip_gourp";
            this.ip_gourp.Size = new System.Drawing.Size(160, 93);
            this.ip_gourp.TabIndex = 9;
            this.ip_gourp.TabStop = false;
            this.ip_gourp.Text = "IP设置";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(77, 60);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 12);
            this.label19.TabIndex = 10;
            this.label19.Text = "天内的IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "过滤";
            // 
            // grep_ip_input
            // 
            this.grep_ip_input.Location = new System.Drawing.Point(40, 55);
            this.grep_ip_input.Name = "grep_ip_input";
            this.grep_ip_input.Size = new System.Drawing.Size(31, 21);
            this.grep_ip_input.TabIndex = 8;
            this.grep_ip_input.Text = "1";
            this.grep_ip_input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // grep_ip_checkbox
            // 
            this.grep_ip_checkbox.AutoSize = true;
            this.grep_ip_checkbox.Checked = true;
            this.grep_ip_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.grep_ip_checkbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grep_ip_checkbox.Location = new System.Drawing.Point(7, 20);
            this.grep_ip_checkbox.Name = "grep_ip_checkbox";
            this.grep_ip_checkbox.Size = new System.Drawing.Size(105, 16);
            this.grep_ip_checkbox.TabIndex = 7;
            this.grep_ip_checkbox.Text = "启动IP重复过滤";
            this.grep_ip_checkbox.UseVisualStyleBackColor = true;
            // 
            // task_setting
            // 
            this.task_setting.Controls.Add(this.key_count_label);
            this.task_setting.Controls.Add(this.splite_label1);
            this.task_setting.Controls.Add(this.count_button);
            this.task_setting.Controls.Add(this.label1);
            this.task_setting.Controls.Add(this.edit_button);
            this.task_setting.Location = new System.Drawing.Point(9, 26);
            this.task_setting.Name = "task_setting";
            this.task_setting.Size = new System.Drawing.Size(163, 113);
            this.task_setting.TabIndex = 10;
            this.task_setting.TabStop = false;
            this.task_setting.Text = "任务列表";
            // 
            // key_count_label
            // 
            this.key_count_label.AutoSize = true;
            this.key_count_label.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.key_count_label.Location = new System.Drawing.Point(132, 31);
            this.key_count_label.Name = "key_count_label";
            this.key_count_label.Size = new System.Drawing.Size(14, 14);
            this.key_count_label.TabIndex = 6;
            this.key_count_label.Text = "1";
            // 
            // splite_label1
            // 
            this.splite_label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.splite_label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splite_label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.splite_label1.Font = new System.Drawing.Font("宋体", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splite_label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.splite_label1.Location = new System.Drawing.Point(0, 101);
            this.splite_label1.Name = "splite_label1";
            this.splite_label1.Size = new System.Drawing.Size(160, 1);
            this.splite_label1.TabIndex = 3;
            this.splite_label1.Text = "\"\"";
            this.splite_label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // count_button
            // 
            this.count_button.Location = new System.Drawing.Point(7, 60);
            this.count_button.Name = "count_button";
            this.count_button.Size = new System.Drawing.Size(150, 31);
            this.count_button.TabIndex = 2;
            this.count_button.Text = "统计数量";
            this.count_button.UseVisualStyleBackColor = true;
            this.count_button.Click += new System.EventHandler(this.count_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(88, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "数量：";
            // 
            // edit_button
            // 
            this.edit_button.Location = new System.Drawing.Point(7, 20);
            this.edit_button.Name = "edit_button";
            this.edit_button.Size = new System.Drawing.Size(75, 34);
            this.edit_button.TabIndex = 0;
            this.edit_button.Text = "编辑列表";
            this.edit_button.UseVisualStyleBackColor = true;
            this.edit_button.Click += new System.EventHandler(this.edit_button_Click);
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(242, 246);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(176, 30);
            this.save_button.TabIndex = 12;
            this.save_button.Text = "保存";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(9, 244);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(176, 32);
            this.cancel_button.TabIndex = 11;
            this.cancel_button.Text = "关闭";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 35;
            this.label7.Text = "任务量:";
            // 
            // task_input
            // 
            this.task_input.Location = new System.Drawing.Point(98, 179);
            this.task_input.Name = "task_input";
            this.task_input.Size = new System.Drawing.Size(112, 21);
            this.task_input.TabIndex = 36;
            this.task_input.Text = "5";
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 288);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.task_setting);
            this.Controls.Add(this.ip_gourp);
            this.Controls.Add(this.adsl_setting);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingForm";
            this.Text = "设置";
            this.adsl_setting.ResumeLayout(false);
            this.adsl_setting.PerformLayout();
            this.ip_gourp.ResumeLayout(false);
            this.ip_gourp.PerformLayout();
            this.task_setting.ResumeLayout(false);
            this.task_setting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox adsl_setting;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox ad_staytime_input2;
        private System.Windows.Forms.TextBox ad_staytime_input1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox site_stay_input2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox site_stay_input1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox adsl_password_input;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox adsl_user_input;
        private System.Windows.Forms.TextBox adsl_input;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox ip_gourp;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox grep_ip_input;
        private System.Windows.Forms.CheckBox grep_ip_checkbox;
        private System.Windows.Forms.GroupBox task_setting;
        private System.Windows.Forms.Label key_count_label;
        private System.Windows.Forms.Label splite_label1;
        private System.Windows.Forms.Button count_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button edit_button;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.TextBox task_input;
        private System.Windows.Forms.Label label7;
    }
}