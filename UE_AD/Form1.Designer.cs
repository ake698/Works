namespace UE_AD
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.start_Button = new System.Windows.Forms.Button();
            this.action_box = new System.Windows.Forms.GroupBox();
            this.stop_Button = new System.Windows.Forms.Button();
            this.adsl_password_input = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.adsl_user_input = new System.Windows.Forms.TextBox();
            this.adsl_input = new System.Windows.Forms.TextBox();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.action_box.SuspendLayout();
            this.SuspendLayout();
            // 
            // start_Button
            // 
            this.start_Button.Location = new System.Drawing.Point(6, 21);
            this.start_Button.Name = "start_Button";
            this.start_Button.Size = new System.Drawing.Size(124, 71);
            this.start_Button.TabIndex = 0;
            this.start_Button.Text = "开始";
            this.start_Button.UseVisualStyleBackColor = true;
            this.start_Button.Click += new System.EventHandler(this.start_Button_Click);
            // 
            // action_box
            // 
            this.action_box.Controls.Add(this.stop_Button);
            this.action_box.Controls.Add(this.adsl_password_input);
            this.action_box.Controls.Add(this.label3);
            this.action_box.Controls.Add(this.label2);
            this.action_box.Controls.Add(this.label1);
            this.action_box.Controls.Add(this.adsl_user_input);
            this.action_box.Controls.Add(this.adsl_input);
            this.action_box.Controls.Add(this.start_Button);
            this.action_box.Location = new System.Drawing.Point(12, 12);
            this.action_box.Name = "action_box";
            this.action_box.Size = new System.Drawing.Size(462, 110);
            this.action_box.TabIndex = 1;
            this.action_box.TabStop = false;
            this.action_box.Text = "操作";
            // 
            // stop_Button
            // 
            this.stop_Button.Location = new System.Drawing.Point(136, 21);
            this.stop_Button.Name = "stop_Button";
            this.stop_Button.Size = new System.Drawing.Size(131, 71);
            this.stop_Button.TabIndex = 7;
            this.stop_Button.Text = "停止";
            this.stop_Button.UseVisualStyleBackColor = true;
            this.stop_Button.Click += new System.EventHandler(this.stop_Button_Click);
            // 
            // adsl_password_input
            // 
            this.adsl_password_input.Location = new System.Drawing.Point(345, 77);
            this.adsl_password_input.Name = "adsl_password_input";
            this.adsl_password_input.Size = new System.Drawing.Size(111, 21);
            this.adsl_password_input.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(273, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "ADSL名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(273, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "ADSL密码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(273, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "ADSL账号：";
            // 
            // adsl_user_input
            // 
            this.adsl_user_input.Location = new System.Drawing.Point(345, 47);
            this.adsl_user_input.Name = "adsl_user_input";
            this.adsl_user_input.Size = new System.Drawing.Size(111, 21);
            this.adsl_user_input.TabIndex = 2;
            // 
            // adsl_input
            // 
            this.adsl_input.Location = new System.Drawing.Point(345, 17);
            this.adsl_input.Name = "adsl_input";
            this.adsl_input.Size = new System.Drawing.Size(111, 21);
            this.adsl_input.TabIndex = 1;
            this.adsl_input.Text = "adsl";
            // 
            // logBox
            // 
            this.logBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.logBox.Location = new System.Drawing.Point(12, 128);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(462, 183);
            this.logBox.TabIndex = 8;
            this.logBox.Text = "";
            this.logBox.TextChanged += new System.EventHandler(this.logBox_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 323);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.action_box);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.action_box.ResumeLayout(false);
            this.action_box.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button start_Button;
        private System.Windows.Forms.GroupBox action_box;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox adsl_user_input;
        private System.Windows.Forms.TextBox adsl_input;
        private System.Windows.Forms.RichTextBox logBox;
        private System.Windows.Forms.TextBox adsl_password_input;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button stop_Button;
    }
}

