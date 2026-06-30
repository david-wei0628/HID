using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HID
{
    public partial class Form2 : Form
    {
        private List<DdcCiMonitorController.MonitorInfo> _monitors;
        private string A_CUT = null;

        public Form2()
        {
            InitializeComponent();
        }

        private async void Form2_Load(object sender, EventArgs e)
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(A_CUT);
        }

    }
}
