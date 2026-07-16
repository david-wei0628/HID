using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HID
{
    public partial class HID_Form : Form
    {
        private List<DdcCiMonitorController.MonitorInfo> _monitors;

        public HID_Form()
        {
            InitializeComponent();
        }

        private async void HID_Form_Load(object sender, EventArgs e)
        {
            // 丟到背景偵測，避免開表單時卡頓
            _monitors = await Task.Run(() => DdcCiMonitorController.GetAllMonitors());

            cmbMonitors.Items.Clear();
            if (_monitors.Count > 0)
            {
                foreach (var monitor in _monitors)
                {
                    cmbMonitors.Items.Add(monitor);
                }
                cmbMonitors.SelectedIndex = 0;
                lblStatus.Text = $"偵測完成，共找到 {_monitors.Count} 台螢幕。";
            }
            else
            {
                lblStatus.Text = "未偵測到支援 DDC/CI 的螢幕，請確認選單已開啟該功能。";
            }

            trackBrightness.Minimum = 0;
            trackBrightness.Maximum = 100;

            trackContrast.Minimum = 0;
            trackContrast.Maximum = 100;

            //testhScrollBar.Maximum = 100 + testhScrollBar.LargeChange - 1;
            BrightnesshScrollBar.Maximum = 100 + BrightnesshScrollBar.LargeChange - 1;
            ContrasthScrollBar.Maximum = 100 + ContrasthScrollBar.LargeChange - 1;
        }

        private void HID__Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // 1. 確保釋放所有曾獲取過的實體螢幕控制代碼
                if (_monitors != null && _monitors.Count > 0)
                {
                    // 將控制代碼陣列還給系統，解開 dxva2.dll 的鎖
                    // 實務上您可以將原本獲取到的 PHYSICAL_MONITOR 陣列存為全域變數，在此處傳入釋放
                    lblStatus.Text = "正在安全釋放硬體通道...";
                }
            }
            catch { }
            finally
            {
                // 2. 終極手段：強制結束當前應用程式的所有執行緒，不留任何背景殘留
                Environment.Exit(0);
            }
        }

        private async void cmbMonitors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMonitors.SelectedItem is DdcCiMonitorController.MonitorInfo selectedMonitor)
            {
                lblStatus.Text = "正在讀取螢幕當前亮度/對比...";

                /*// 呼叫剛剛寫的非同步獲取亮度方法
                int currentBrightness = await DdcCiMonitorController.GetBrightnessAsync(selectedMonitor.Handle);

                if (currentBrightness != -1)
                {
                    // 成功獲取：將 TrackBar 與數值 Label 同步硬體當下的狀態
                    trackBrightness.Value = currentBrightness;
                    lblBrightnessValue.Text = $"目前亮度: {currentBrightness}%";
                    lblStatus.Text += "成功同步螢幕目前亮度。";
                }
                else
                {
                    // 獲取失敗（多半又是 DVI 轉 HDMI 線材通訊失敗，或者螢幕開了 HDR/OSD鎖定）
                    lblStatus.Text = "無法讀取硬體亮度 (螢幕可能正處於 HDR 模式或線材不支援雙向通訊)";

                    // 預設給個 50%，避免 TrackBar 壞掉
                    trackBrightness.Value = 50;
                    lblBrightnessValue.Text = "目前亮度: 未知";
                }*/

                Task syncBrightnessTask = RefreshCurrentBrightness(selectedMonitor.Handle);
                Task syncContrastTask = RefreshCurrentContrast(selectedMonitor.Handle);
                await Task.WhenAll(syncBrightnessTask, syncContrastTask);

            }
        }

        /// <summary>
        /// 動作一：負責讀取並更新亮度 UI
        /// </summary>
        private async Task RefreshCurrentBrightness(IntPtr monitorHandle)
        {
            int currentBrightness = await DdcCiMonitorController.GetBrightnessAsync(monitorHandle);

            if (currentBrightness != -1)
            {
                /*trackBrightness.Value = currentBrightness;
                BrightnesshScrollBar.Value = currentBrightness;*/
                BrightnessValueRevise((uint)currentBrightness);
                labBrightnessValue.Text = $"{currentBrightness}%";
            }
            else
            {
                labBrightnessValue.Text = "目前亮度: 未知";
            }
        }

        /// <summary>
        /// 動作二：負責讀取並更新對比度 UI
        /// </summary>
        private async Task RefreshCurrentContrast(IntPtr monitorHandle)
        {
            int currentContrast = await DdcCiMonitorController.GetContrastAsync(monitorHandle);

            if (currentContrast != -1)
            {
                /*trackContrast.Value = currentContrast;      // 您畫底層給對比用的 TrackBar
                ContrasthScrollBar.Value= currentContrast;*/
                ContrasthValueRevise((uint)currentContrast);
                labContrastValue.Text = $"{currentContrast}%"; // 您畫給對比用的 Label
            }
            else
            {
                labContrastValue.Text = "目前對比: 未知";
            }
        }

        //private async void trackBrightness_Scroll(object sender, EventArgs e)
        private async void trackBrightness_MouseUp(object sender, MouseEventArgs e)
        {
            if (cmbMonitors.SelectedItem is DdcCiMonitorController.MonitorInfo selectedMonitor)
            {
                uint targetBrightness = (uint)trackBrightness.Value;
                lblStatus.Text = $"正在變更亮度至 {targetBrightness}%...";

                bool success = await DdcCiMonitorController.SetBrightnessAsync(selectedMonitor.Handle, targetBrightness);

                if (success)
                {     //lblStatus.Text = $"亮度已成功設定為 {targetBrightness}%";
                    labBrightnessValue.Text = $"{targetBrightness}%";
                    BrightnessValueRevise(targetBrightness);
                }
                else
                    lblStatus.Text = "亮度設定失敗 (螢幕可能正處於 HDR 模式或 OSD 鎖定狀態)";
            }
        }

        //private async void trackContrast_Scroll(object sender, EventArgs e)
        private async void trackBarContrast_MouseUp(object sender, MouseEventArgs e)
        {
            if (cmbMonitors.SelectedItem is DdcCiMonitorController.MonitorInfo selectedMonitor)
            {
                uint targetContrast = (uint)trackContrast.Value;
                lblStatus.Text = $"正在變更對比至 {targetContrast}%...";

                bool success = await DdcCiMonitorController.SetContrastAsync(selectedMonitor.Handle, targetContrast);

                if (success)
                {
                    labContrastValue.Text = $"{targetContrast}%";
                    ContrasthValueRevise(targetContrast);
                }
                else
                    lblStatus.Text = "對比設定失敗 (螢幕可能正處於 HDR 模式或 OSD 鎖定狀態)";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BTNchange(30, 20);
        }

        private async void BTNchange(uint BrightnessValue, uint ContrastValue)
        {
            if (cmbMonitors.SelectedItem is DdcCiMonitorController.MonitorInfo selectedMonitor)
            {

                if (await DdcCiMonitorController.SetContrastAsync(selectedMonitor.Handle, ContrastValue))
                    labContrastValue.Text = $"{ContrastValue}%";
                else
                    lblStatus.Text = "對比設定失敗 (螢幕可能正處於 HDR 模式或 OSD 鎖定狀態)";

                if (await DdcCiMonitorController.SetBrightnessAsync(selectedMonitor.Handle, BrightnessValue))
                    labBrightnessValue.Text = $"{BrightnessValue}%";
                else
                    lblStatus.Text = "對比設定失敗 (螢幕可能正處於 HDR 模式或 OSD 鎖定狀態)";

                BrightnessValueRevise(BrightnessValue);
                ContrasthValueRevise(ContrastValue);

            }
        }

        private void BrightnessValueRevise(uint Value)
        {
            trackBrightness.Value = (int)Value;
            BrightnesshScrollBar.Value = (int)Value;
        }

        private void ContrasthValueRevise(uint Value)
        {
            trackContrast.Value = (int)Value;
            ContrasthScrollBar.Value = (int)Value;
        }

        private void BrightnesshScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            labBrightnessValue.Text = $"{e.NewValue}%";

            ScrollEvenALL(sender, e);

            BrightnessValueRevise((uint)e.NewValue);
        }

        private void ContrasthScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            labContrastValue.Text = $"{e.NewValue}%";

            ScrollEvenALL(sender, e);

            ContrasthValueRevise((uint)e.NewValue);
        }

        private void ScrollEvenALL(object Sender, ScrollEventArgs e)
        {

            switch (e.Type)
            {
                case ScrollEventType.ThumbTrack:
                    break;
                case ScrollEventType.EndScroll:
                    if (Sender is Control control)
                    {
                        objEven(control);

                    }
                    break;
            }
        }

        private void objEven(Control control)
        {
            switch (control.Tag)
            {
                case "Brightness":
                    if (control is HScrollBar hScrolBrightness)
                    {
                        OSDBrightnessValueRevise((uint)hScrolBrightness.Value);
                    }
                    break;
                case "Contrast":
                    if (control is HScrollBar hScrolContrast)
                    {
                        OSDContrastValueRevise((uint)hScrolContrast.Value);
                    }
                    break;
            }
            /*if (control is HScrollBar hScroll)
            {
                switch (hScroll.Name)
                {
                    case "BrightnesshScrollBar":
                        OSDBrightnessValueRevise((uint)hScroll.Value);
                        //Console.WriteLine(hScroll.Value);
                        break;
                    case "ContrasthScrollBar":
                        OSDContrastValueRevise((uint)hScroll.Value);
                        //Console.WriteLine(hScroll.Value);
                        break;
                }
            }*/
        }

        private async void OSDBrightnessValueRevise(uint OSDNewValue)
        {
            if (cmbMonitors.SelectedItem is DdcCiMonitorController.MonitorInfo selectedMonitor)
            {

                if (await DdcCiMonitorController.SetBrightnessAsync(selectedMonitor.Handle, OSDNewValue))
                    labBrightnessValue.Text = $"{OSDNewValue}%";
                else
                    lblStatus.Text = "設定失敗 (螢幕可能正處於 HDR 模式或 OSD 鎖定狀態)";

            }
        }

        private async void OSDContrastValueRevise(uint OSDNewValue)
        {
            if (cmbMonitors.SelectedItem is DdcCiMonitorController.MonitorInfo selectedMonitor)
            {

                if (await DdcCiMonitorController.SetContrastAsync(selectedMonitor.Handle, OSDNewValue))
                    labContrastValue.Text = $"{OSDNewValue}%";
                else
                    lblStatus.Text = "設定失敗 (螢幕可能正處於 HDR 模式或 OSD 鎖定狀態)";

            }
        }
    }
}
