namespace HID
{
    partial class HID_Form
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
            this.cmbMonitors = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.labBrightnessValue = new System.Windows.Forms.Label();
            this.labContrastValue = new System.Windows.Forms.Label();
            this.BrightnessGroup = new System.Windows.Forms.GroupBox();
            this.BrightnesshScrollBar = new System.Windows.Forms.HScrollBar();
            this.trackBrightness = new System.Windows.Forms.TrackBar();
            this.ContrasthScrollBar = new System.Windows.Forms.HScrollBar();
            this.ContrastGroup = new System.Windows.Forms.GroupBox();
            this.trackContrast = new System.Windows.Forms.TrackBar();
            this.SetValueGroup = new System.Windows.Forms.GroupBox();
            this.Contrast_SetValue_lab = new System.Windows.Forms.Label();
            this.Brightness_SetValue_lab = new System.Windows.Forms.Label();
            this.Contrast_SetValue_txtBox = new System.Windows.Forms.TextBox();
            this.Brightness_SetValue_txtBox = new System.Windows.Forms.TextBox();
            this.NigthModeBTN = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.BrightnessGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBrightness)).BeginInit();
            this.ContrastGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackContrast)).BeginInit();
            this.SetValueGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbMonitors
            // 
            this.cmbMonitors.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.cmbMonitors.Font = new System.Drawing.Font("Noto Serif JP Medium", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMonitors.FormattingEnabled = true;
            this.cmbMonitors.Location = new System.Drawing.Point(134, 25);
            this.cmbMonitors.Name = "cmbMonitors";
            this.cmbMonitors.Size = new System.Drawing.Size(240, 38);
            this.cmbMonitors.TabIndex = 0;
            this.cmbMonitors.SelectedIndexChanged += new System.EventHandler(this.cmbMonitors_SelectedIndexChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Noto Serif JP Medium", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(20, 81);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(87, 35);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "label1";
            // 
            // labBrightnessValue
            // 
            this.labBrightnessValue.AutoSize = true;
            this.labBrightnessValue.Font = new System.Drawing.Font("Noto Serif JP Medium", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labBrightnessValue.Location = new System.Drawing.Point(7, 18);
            this.labBrightnessValue.Name = "labBrightnessValue";
            this.labBrightnessValue.Size = new System.Drawing.Size(124, 30);
            this.labBrightnessValue.TabIndex = 5;
            this.labBrightnessValue.Tag = "Brightness";
            this.labBrightnessValue.Text = "Brightness";
            // 
            // labContrastValue
            // 
            this.labContrastValue.AutoSize = true;
            this.labContrastValue.Font = new System.Drawing.Font("Noto Serif JP Medium", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labContrastValue.Location = new System.Drawing.Point(7, 18);
            this.labContrastValue.Name = "labContrastValue";
            this.labContrastValue.Size = new System.Drawing.Size(103, 30);
            this.labContrastValue.TabIndex = 5;
            this.labContrastValue.Tag = "Contrast";
            this.labContrastValue.Text = "Contrast";
            // 
            // BrightnessGroup
            // 
            this.BrightnessGroup.Controls.Add(this.BrightnesshScrollBar);
            this.BrightnessGroup.Controls.Add(this.labBrightnessValue);
            this.BrightnessGroup.Controls.Add(this.trackBrightness);
            this.BrightnessGroup.Font = new System.Drawing.Font("Noto Serif JP Medium", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BrightnessGroup.Location = new System.Drawing.Point(12, 143);
            this.BrightnessGroup.Name = "BrightnessGroup";
            this.BrightnessGroup.Size = new System.Drawing.Size(511, 132);
            this.BrightnessGroup.TabIndex = 6;
            this.BrightnessGroup.TabStop = false;
            this.BrightnessGroup.Tag = "Brightness";
            this.BrightnessGroup.Text = "亮度";
            // 
            // BrightnesshScrollBar
            // 
            this.BrightnesshScrollBar.Cursor = System.Windows.Forms.Cursors.NoMoveHoriz;
            this.BrightnesshScrollBar.LargeChange = 5;
            this.BrightnesshScrollBar.Location = new System.Drawing.Point(3, 103);
            this.BrightnesshScrollBar.Name = "BrightnesshScrollBar";
            this.BrightnesshScrollBar.Size = new System.Drawing.Size(426, 26);
            this.BrightnesshScrollBar.TabIndex = 6;
            this.BrightnesshScrollBar.Tag = "Brightness";
            this.BrightnesshScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.BrightnesshScrollBar_Scroll);
            // 
            // trackBrightness
            // 
            this.trackBrightness.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.trackBrightness.Location = new System.Drawing.Point(7, 51);
            this.trackBrightness.Name = "trackBrightness";
            this.trackBrightness.Size = new System.Drawing.Size(422, 45);
            this.trackBrightness.TabIndex = 3;
            this.trackBrightness.Tag = "Brightness";
            this.trackBrightness.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBarBrightness_MouseUp);
            // 
            // ContrasthScrollBar
            // 
            this.ContrasthScrollBar.Cursor = System.Windows.Forms.Cursors.NoMoveHoriz;
            this.ContrasthScrollBar.LargeChange = 5;
            this.ContrasthScrollBar.Location = new System.Drawing.Point(3, 103);
            this.ContrasthScrollBar.Name = "ContrasthScrollBar";
            this.ContrasthScrollBar.Size = new System.Drawing.Size(426, 26);
            this.ContrasthScrollBar.TabIndex = 7;
            this.ContrasthScrollBar.Tag = "Contrast";
            this.ContrasthScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ContrasthScrollBar_Scroll);
            // 
            // ContrastGroup
            // 
            this.ContrastGroup.Controls.Add(this.ContrasthScrollBar);
            this.ContrastGroup.Controls.Add(this.labContrastValue);
            this.ContrastGroup.Controls.Add(this.trackContrast);
            this.ContrastGroup.Font = new System.Drawing.Font("Noto Serif JP Medium", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ContrastGroup.Location = new System.Drawing.Point(12, 306);
            this.ContrastGroup.Name = "ContrastGroup";
            this.ContrastGroup.Size = new System.Drawing.Size(511, 132);
            this.ContrastGroup.TabIndex = 7;
            this.ContrastGroup.TabStop = false;
            this.ContrastGroup.Tag = "Contrast";
            this.ContrastGroup.Text = "對比";
            // 
            // trackContrast
            // 
            this.trackContrast.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.trackContrast.Location = new System.Drawing.Point(7, 51);
            this.trackContrast.Name = "trackContrast";
            this.trackContrast.Size = new System.Drawing.Size(422, 45);
            this.trackContrast.TabIndex = 3;
            this.trackContrast.Tag = "Contrast";
            this.trackContrast.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBarContrast_MouseUp);
            // 
            // SetValueGroup
            // 
            this.SetValueGroup.Controls.Add(this.Contrast_SetValue_lab);
            this.SetValueGroup.Controls.Add(this.Brightness_SetValue_lab);
            this.SetValueGroup.Controls.Add(this.Contrast_SetValue_txtBox);
            this.SetValueGroup.Controls.Add(this.Brightness_SetValue_txtBox);
            this.SetValueGroup.Controls.Add(this.NigthModeBTN);
            this.SetValueGroup.Font = new System.Drawing.Font("Noto Serif JP Medium", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SetValueGroup.Location = new System.Drawing.Point(553, 15);
            this.SetValueGroup.Name = "SetValueGroup";
            this.SetValueGroup.Size = new System.Drawing.Size(232, 406);
            this.SetValueGroup.TabIndex = 8;
            this.SetValueGroup.TabStop = false;
            this.SetValueGroup.Text = "預設參數";
            // 
            // Contrast_SetValue_lab
            // 
            this.Contrast_SetValue_lab.AutoSize = true;
            this.Contrast_SetValue_lab.Font = new System.Drawing.Font("Noto Serif JP Medium", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Contrast_SetValue_lab.Location = new System.Drawing.Point(40, 144);
            this.Contrast_SetValue_lab.Name = "Contrast_SetValue_lab";
            this.Contrast_SetValue_lab.Size = new System.Drawing.Size(159, 21);
            this.Contrast_SetValue_lab.TabIndex = 4;
            this.Contrast_SetValue_lab.Text = "Contrast_SetValue";
            // 
            // Brightness_SetValue_lab
            // 
            this.Brightness_SetValue_lab.AutoSize = true;
            this.Brightness_SetValue_lab.Font = new System.Drawing.Font("Noto Serif JP Medium", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Brightness_SetValue_lab.Location = new System.Drawing.Point(40, 66);
            this.Brightness_SetValue_lab.Name = "Brightness_SetValue_lab";
            this.Brightness_SetValue_lab.Size = new System.Drawing.Size(177, 21);
            this.Brightness_SetValue_lab.TabIndex = 3;
            this.Brightness_SetValue_lab.Text = "Brightness_SetValue";
            // 
            // Contrast_SetValue_txtBox
            // 
            this.Contrast_SetValue_txtBox.Font = new System.Drawing.Font("Noto Serif JP Medium", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Contrast_SetValue_txtBox.Location = new System.Drawing.Point(44, 168);
            this.Contrast_SetValue_txtBox.Name = "Contrast_SetValue_txtBox";
            this.Contrast_SetValue_txtBox.Size = new System.Drawing.Size(141, 38);
            this.Contrast_SetValue_txtBox.TabIndex = 2;
            // 
            // Brightness_SetValue_txtBox
            // 
            this.Brightness_SetValue_txtBox.Font = new System.Drawing.Font("Noto Serif JP Medium", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Brightness_SetValue_txtBox.Location = new System.Drawing.Point(41, 90);
            this.Brightness_SetValue_txtBox.Name = "Brightness_SetValue_txtBox";
            this.Brightness_SetValue_txtBox.Size = new System.Drawing.Size(141, 38);
            this.Brightness_SetValue_txtBox.TabIndex = 1;
            // 
            // NigthModeBTN
            // 
            this.NigthModeBTN.Location = new System.Drawing.Point(44, 341);
            this.NigthModeBTN.Name = "NigthModeBTN";
            this.NigthModeBTN.Size = new System.Drawing.Size(142, 59);
            this.NigthModeBTN.TabIndex = 0;
            this.NigthModeBTN.Text = "夜晚";
            this.NigthModeBTN.UseVisualStyleBackColor = true;
            this.NigthModeBTN.Click += new System.EventHandler(this.button1_Click);
            // 
            // HID_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 459);
            this.Controls.Add(this.SetValueGroup);
            this.Controls.Add(this.ContrastGroup);
            this.Controls.Add(this.BrightnessGroup);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbMonitors);
            this.Name = "HID_Form";
            this.Text = "HID";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HID__Form_FormClosing);
            this.Load += new System.EventHandler(this.HID_Form_Load);
            this.BrightnessGroup.ResumeLayout(false);
            this.BrightnessGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBrightness)).EndInit();
            this.ContrastGroup.ResumeLayout(false);
            this.ContrastGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackContrast)).EndInit();
            this.SetValueGroup.ResumeLayout(false);
            this.SetValueGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbMonitors;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TrackBar trackBrightness;
        private System.Windows.Forms.Label labBrightnessValue;
        private System.Windows.Forms.GroupBox BrightnessGroup;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox ContrastGroup;
        private System.Windows.Forms.Label labContrastValue;
        private System.Windows.Forms.TrackBar trackContrast;
        private System.Windows.Forms.GroupBox SetValueGroup;
        private System.Windows.Forms.Button NigthModeBTN;
        private System.Windows.Forms.Label Contrast_SetValue_lab;
        private System.Windows.Forms.Label Brightness_SetValue_lab;
        private System.Windows.Forms.TextBox Contrast_SetValue_txtBox;
        private System.Windows.Forms.TextBox Brightness_SetValue_txtBox;
        private System.Windows.Forms.HScrollBar BrightnesshScrollBar;
        private System.Windows.Forms.HScrollBar ContrasthScrollBar;
    }
}