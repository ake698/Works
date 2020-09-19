using System.Windows.Forms;

namespace GoogleChrome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.close_button = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.button_group = new System.Windows.Forms.GroupBox();
            this.setting_button = new System.Windows.Forms.Button();
            this.load_button = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.taskView = new System.Windows.Forms.ListView();
            this.button_group.SuspendLayout();
            this.SuspendLayout();
            // 
            // close_button
            // 
            this.close_button.Enabled = false;
            this.close_button.Location = new System.Drawing.Point(114, 62);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(75, 23);
            this.close_button.TabIndex = 2;
            this.close_button.Text = "停止";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(6, 20);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "开始";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // button_group
            // 
            this.button_group.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_group.Controls.Add(this.setting_button);
            this.button_group.Controls.Add(this.load_button);
            this.button_group.Controls.Add(this.startButton);
            this.button_group.Controls.Add(this.close_button);
            this.button_group.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_group.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_group.Location = new System.Drawing.Point(3, 5);
            this.button_group.Name = "button_group";
            this.button_group.Size = new System.Drawing.Size(195, 100);
            this.button_group.TabIndex = 5;
            this.button_group.TabStop = false;
            // 
            // setting_button
            // 
            this.setting_button.Location = new System.Drawing.Point(6, 62);
            this.setting_button.Name = "setting_button";
            this.setting_button.Size = new System.Drawing.Size(75, 23);
            this.setting_button.TabIndex = 3;
            this.setting_button.Text = "设置";
            this.setting_button.UseVisualStyleBackColor = true;
            this.setting_button.Click += new System.EventHandler(this.setting_button_Click);
            // 
            // load_button
            // 
            this.load_button.Location = new System.Drawing.Point(114, 20);
            this.load_button.Name = "load_button";
            this.load_button.Size = new System.Drawing.Size(75, 23);
            this.load_button.TabIndex = 1;
            this.load_button.Text = "重载任务";
            this.load_button.UseVisualStyleBackColor = true;
            this.load_button.Click += new System.EventHandler(this.load_button_Click);
            // 
            // logBox
            // 
            this.logBox.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.logBox.Location = new System.Drawing.Point(3, 111);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(195, 230);
            this.logBox.TabIndex = 6;
            this.logBox.Text = "";
            // 
            // taskView
            // 
            this.taskView.FullRowSelect = true;
            this.taskView.GridLines = true;
            this.taskView.HideSelection = false;
            this.taskView.Location = new System.Drawing.Point(204, 12);
            this.taskView.Name = "taskView";
            this.taskView.Size = new System.Drawing.Size(515, 329);
            this.taskView.TabIndex = 5;
            this.taskView.UseCompatibleStateImageBehavior = false;
            this.taskView.View = System.Windows.Forms.View.Details;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 353);
            this.Controls.Add(this.taskView);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.button_group);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "点击精灵";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.button_group.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.GroupBox button_group;
        private System.Windows.Forms.Button load_button;
        private System.Windows.Forms.RichTextBox logBox;
        private System.Windows.Forms.ListView taskView;
        private Button setting_button;
    }
}

