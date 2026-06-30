namespace HID
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCurrentApp = new System.Windows.Forms.Label();
            this.lblModeTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCurrentApp
            // 
            this.lblCurrentApp.AutoSize = true;
            this.lblCurrentApp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCurrentApp.Font = new System.Drawing.Font("Noto Serif TC", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblCurrentApp.Location = new System.Drawing.Point(0, 358);
            this.lblCurrentApp.Name = "lblCurrentApp";
            this.lblCurrentApp.Size = new System.Drawing.Size(239, 92);
            this.lblCurrentApp.TabIndex = 0;
            this.lblCurrentApp.Text = "label1";
            // 
            // lblModeTitle
            // 
            this.lblModeTitle.AutoSize = true;
            this.lblModeTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblModeTitle.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModeTitle.Location = new System.Drawing.Point(0, 0);
            this.lblModeTitle.Name = "lblModeTitle";
            this.lblModeTitle.Size = new System.Drawing.Size(65, 24);
            this.lblModeTitle.TabIndex = 1;
            this.lblModeTitle.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblModeTitle);
            this.Controls.Add(this.lblCurrentApp);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCurrentApp;
        private System.Windows.Forms.Label lblModeTitle;
    }
}

