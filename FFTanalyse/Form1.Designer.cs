namespace FFTanalyse
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
            this.button1 = new System.Windows.Forms.Button();
            this.OutBox = new System.Windows.Forms.RichTextBox();
            this.FFTcount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.audioButton = new System.Windows.Forms.Button();
            this.audiolabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(356, 243);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "LOAD";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // OutBox
            // 
            this.OutBox.Location = new System.Drawing.Point(12, 13);
            this.OutBox.Name = "OutBox";
            this.OutBox.Size = new System.Drawing.Size(418, 224);
            this.OutBox.TabIndex = 3;
            this.OutBox.Text = "";
            // 
            // FFTcount
            // 
            this.FFTcount.Location = new System.Drawing.Point(69, 244);
            this.FFTcount.Name = "FFTcount";
            this.FFTcount.Size = new System.Drawing.Size(58, 21);
            this.FFTcount.TabIndex = 4;
            this.FFTcount.Text = "512";
            this.FFTcount.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 249);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "FFT点数：";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(278, 243);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "RESET";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // audioButton
            // 
            this.audioButton.Location = new System.Drawing.Point(199, 243);
            this.audioButton.Name = "audioButton";
            this.audioButton.Size = new System.Drawing.Size(75, 23);
            this.audioButton.TabIndex = 7;
            this.audioButton.Text = "Audio";
            this.audioButton.UseVisualStyleBackColor = true;
            this.audioButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // audiolabel
            // 
            this.audiolabel.AutoSize = true;
            this.audiolabel.Location = new System.Drawing.Point(134, 249);
            this.audiolabel.Name = "audiolabel";
            this.audiolabel.Size = new System.Drawing.Size(53, 12);
            this.audiolabel.TabIndex = 8;
            this.audiolabel.Text = "AudioOff";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 278);
            this.Controls.Add(this.audiolabel);
            this.Controls.Add(this.audioButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FFTcount);
            this.Controls.Add(this.OutBox);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox OutBox;
        private System.Windows.Forms.TextBox FFTcount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button audioButton;
        private System.Windows.Forms.Label audiolabel;
    }
}

