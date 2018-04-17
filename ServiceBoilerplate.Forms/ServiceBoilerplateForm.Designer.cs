namespace ServiceBoilerplate.Forms
{
    partial class ServiceBoilerplateForm
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
            this.CodeComboBox = new System.Windows.Forms.ComboBox();
            this.CodeLabel = new System.Windows.Forms.Label();
            this.EndTimeTextBox = new System.Windows.Forms.TextBox();
            this.EndTimeLabel = new System.Windows.Forms.Label();
            this.BeginTimeTextBox = new System.Windows.Forms.TextBox();
            this.BeginTimeLabel = new System.Windows.Forms.Label();
            this.ResultTextBox = new System.Windows.Forms.TextBox();
            this.ResultLabel = new System.Windows.Forms.Label();
            this.SyncButton = new System.Windows.Forms.Button();
            this.CoverButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CodeComboBox
            // 
            this.CodeComboBox.FormattingEnabled = true;
            this.CodeComboBox.Location = new System.Drawing.Point(84, 66);
            this.CodeComboBox.Name = "CodeComboBox";
            this.CodeComboBox.Size = new System.Drawing.Size(188, 20);
            this.CodeComboBox.TabIndex = 18;
            // 
            // CodeLabel
            // 
            this.CodeLabel.AutoSize = true;
            this.CodeLabel.Location = new System.Drawing.Point(12, 69);
            this.CodeLabel.Name = "CodeLabel";
            this.CodeLabel.Size = new System.Drawing.Size(41, 12);
            this.CodeLabel.TabIndex = 17;
            this.CodeLabel.Text = "Code：";
            // 
            // EndTimeTextBox
            // 
            this.EndTimeTextBox.Location = new System.Drawing.Point(84, 39);
            this.EndTimeTextBox.Name = "EndTimeTextBox";
            this.EndTimeTextBox.Size = new System.Drawing.Size(188, 21);
            this.EndTimeTextBox.TabIndex = 16;
            // 
            // EndTimeLabel
            // 
            this.EndTimeLabel.AutoSize = true;
            this.EndTimeLabel.Location = new System.Drawing.Point(12, 42);
            this.EndTimeLabel.Name = "EndTimeLabel";
            this.EndTimeLabel.Size = new System.Drawing.Size(65, 12);
            this.EndTimeLabel.TabIndex = 14;
            this.EndTimeLabel.Text = "结束时间：";
            // 
            // BeginTimeTextBox
            // 
            this.BeginTimeTextBox.Location = new System.Drawing.Point(84, 12);
            this.BeginTimeTextBox.Name = "BeginTimeTextBox";
            this.BeginTimeTextBox.Size = new System.Drawing.Size(188, 21);
            this.BeginTimeTextBox.TabIndex = 12;
            // 
            // BeginTimeLabel
            // 
            this.BeginTimeLabel.AutoSize = true;
            this.BeginTimeLabel.Location = new System.Drawing.Point(12, 15);
            this.BeginTimeLabel.Name = "BeginTimeLabel";
            this.BeginTimeLabel.Size = new System.Drawing.Size(65, 12);
            this.BeginTimeLabel.TabIndex = 11;
            this.BeginTimeLabel.Text = "开始时间：";
            // 
            // ResultTextBox
            // 
            this.ResultTextBox.Location = new System.Drawing.Point(59, 320);
            this.ResultTextBox.Name = "ResultTextBox";
            this.ResultTextBox.Size = new System.Drawing.Size(213, 21);
            this.ResultTextBox.TabIndex = 15;
            // 
            // ResultLabel
            // 
            this.ResultLabel.AutoSize = true;
            this.ResultLabel.Location = new System.Drawing.Point(12, 323);
            this.ResultLabel.Name = "ResultLabel";
            this.ResultLabel.Size = new System.Drawing.Size(41, 12);
            this.ResultLabel.TabIndex = 13;
            this.ResultLabel.Text = "结果：";
            // 
            // SyncButton
            // 
            this.SyncButton.Location = new System.Drawing.Point(71, 126);
            this.SyncButton.Name = "SyncButton";
            this.SyncButton.Size = new System.Drawing.Size(143, 23);
            this.SyncButton.TabIndex = 19;
            this.SyncButton.Text = "Sync";
            this.SyncButton.UseVisualStyleBackColor = true;
            this.SyncButton.Click += new System.EventHandler(this.SyncButton_Click);
            // 
            // CoverButton
            // 
            this.CoverButton.Location = new System.Drawing.Point(71, 155);
            this.CoverButton.Name = "CoverButton";
            this.CoverButton.Size = new System.Drawing.Size(143, 23);
            this.CoverButton.TabIndex = 20;
            this.CoverButton.Text = "Cover";
            this.CoverButton.UseVisualStyleBackColor = true;
            this.CoverButton.Click += new System.EventHandler(this.CoverButton_Click);
            // 
            // ServiceBoilerplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 353);
            this.Controls.Add(this.CoverButton);
            this.Controls.Add(this.SyncButton);
            this.Controls.Add(this.CodeComboBox);
            this.Controls.Add(this.CodeLabel);
            this.Controls.Add(this.EndTimeTextBox);
            this.Controls.Add(this.EndTimeLabel);
            this.Controls.Add(this.BeginTimeTextBox);
            this.Controls.Add(this.BeginTimeLabel);
            this.Controls.Add(this.ResultTextBox);
            this.Controls.Add(this.ResultLabel);
            this.Name = "ServiceBoilerplateForm";
            this.Text = "ServiceBoilerplate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CodeComboBox;
        private System.Windows.Forms.Label CodeLabel;
        private System.Windows.Forms.TextBox EndTimeTextBox;
        private System.Windows.Forms.Label EndTimeLabel;
        private System.Windows.Forms.TextBox BeginTimeTextBox;
        private System.Windows.Forms.Label BeginTimeLabel;
        private System.Windows.Forms.TextBox ResultTextBox;
        private System.Windows.Forms.Label ResultLabel;
        private System.Windows.Forms.Button SyncButton;
        private System.Windows.Forms.Button CoverButton;
    }
}

