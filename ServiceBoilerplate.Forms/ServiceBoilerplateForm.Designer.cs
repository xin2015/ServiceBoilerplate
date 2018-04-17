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
            this.SuspendLayout();
            // 
            // CodeComboBox
            // 
            this.CodeComboBox.FormattingEnabled = true;
            this.CodeComboBox.Location = new System.Drawing.Point(84, 66);
            this.CodeComboBox.Name = "CodeComboBox";
            this.CodeComboBox.Size = new System.Drawing.Size(188, 20);
            this.CodeComboBox.TabIndex = 18;
            this.CodeComboBox.SelectedIndexChanged += new System.EventHandler(this.CodeComboBox_SelectedIndexChanged);
            // 
            // CodeLabel
            // 
            this.CodeLabel.AutoSize = true;
            this.CodeLabel.Location = new System.Drawing.Point(12, 69);
            this.CodeLabel.Name = "CodeLabel";
            this.CodeLabel.Size = new System.Drawing.Size(41, 12);
            this.CodeLabel.TabIndex = 17;
            this.CodeLabel.Text = "Code：";
            this.CodeLabel.Click += new System.EventHandler(this.CodeLabel_Click);
            // 
            // EndTimeTextBox
            // 
            this.EndTimeTextBox.Location = new System.Drawing.Point(84, 39);
            this.EndTimeTextBox.Name = "EndTimeTextBox";
            this.EndTimeTextBox.Size = new System.Drawing.Size(188, 21);
            this.EndTimeTextBox.TabIndex = 16;
            this.EndTimeTextBox.TextChanged += new System.EventHandler(this.EndTimeTextBox_TextChanged);
            // 
            // EndTimeLabel
            // 
            this.EndTimeLabel.AutoSize = true;
            this.EndTimeLabel.Location = new System.Drawing.Point(12, 42);
            this.EndTimeLabel.Name = "EndTimeLabel";
            this.EndTimeLabel.Size = new System.Drawing.Size(65, 12);
            this.EndTimeLabel.TabIndex = 14;
            this.EndTimeLabel.Text = "结束时间：";
            this.EndTimeLabel.Click += new System.EventHandler(this.EndTimeLabel_Click);
            // 
            // BeginTimeTextBox
            // 
            this.BeginTimeTextBox.Location = new System.Drawing.Point(84, 12);
            this.BeginTimeTextBox.Name = "BeginTimeTextBox";
            this.BeginTimeTextBox.Size = new System.Drawing.Size(188, 21);
            this.BeginTimeTextBox.TabIndex = 12;
            this.BeginTimeTextBox.TextChanged += new System.EventHandler(this.BeginTimeTextBox_TextChanged);
            // 
            // BeginTimeLabel
            // 
            this.BeginTimeLabel.AutoSize = true;
            this.BeginTimeLabel.Location = new System.Drawing.Point(12, 15);
            this.BeginTimeLabel.Name = "BeginTimeLabel";
            this.BeginTimeLabel.Size = new System.Drawing.Size(65, 12);
            this.BeginTimeLabel.TabIndex = 11;
            this.BeginTimeLabel.Text = "开始时间：";
            this.BeginTimeLabel.Click += new System.EventHandler(this.BeginTimeLabel_Click);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 353);
            this.Controls.Add(this.CodeComboBox);
            this.Controls.Add(this.CodeLabel);
            this.Controls.Add(this.EndTimeTextBox);
            this.Controls.Add(this.EndTimeLabel);
            this.Controls.Add(this.BeginTimeTextBox);
            this.Controls.Add(this.BeginTimeLabel);
            this.Controls.Add(this.ResultTextBox);
            this.Controls.Add(this.ResultLabel);
            this.Name = "Form1";
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
    }
}

