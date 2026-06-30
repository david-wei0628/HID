using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

//namespace DisplayHybridController
namespace HID
{
    public partial class Form1 : Form
    {
        // --- 核心組件 ---
        //private MonitorManager _monitor;
        private ControlMode _currentMode = ControlMode.DdcCi;
        private string _activeProcessName = "Unknown";

        public Form1()
        {
            InitializeComponent();

            //FormUI();//自訂邊框

            /*
            _monitor = new MonitorManager();
            _monitor.Initialize(); // 取得初始螢幕狀態
            */

            RegisterRawInput(this.Handle);
            SetupAppTracker();
        }

        private void FormUI()
        {
            this.TopMost = true;//視窗最上層
            this.FormBorderStyle = FormBorderStyle.None;//無邊框
        }

        #region 1. Raw Input 解析 (ParseRawInputDelta)

        private void RegisterRawInput(IntPtr hwnd)
        {
            RAWINPUTDEVICE[] rid = new RAWINPUTDEVICE[1];
            rid[0].usUsagePage = 0x01;
            rid[0].usUsage = 0x02; // 監聽滑鼠類裝置 (多數旋鈕模擬此類)
            rid[0].dwFlags = 0x00000100; // RIDEV_INPUTSINK: 即使視窗沒焦點也能收
            rid[0].hwndTarget = hwnd;

            if (!RegisterRawInputDevices(rid, (uint)rid.Length, (uint)Marshal.SizeOf(rid[0])))
                throw new Exception("無法註冊 Raw Input 裝置");
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_INPUT = 0x00FF;
            if (m.Msg == WM_INPUT)
            {
                int delta = ParseRawInputDelta(m.LParam);
                if (delta != 0) DispatchCommand(delta);
            }
            base.WndProc(ref m);
        }

        private int ParseRawInputDelta(IntPtr lParam)
        {
            uint dwSize = 0;
            GetRawInputData(lParam, 0x10000003, IntPtr.Zero, ref dwSize, (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER)));

            IntPtr buffer = Marshal.AllocHGlobal((int)dwSize);
            try
            {
                if (GetRawInputData(lParam, 0x10000003, buffer, ref dwSize, (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER))) == dwSize)
                {
                    RAWINPUT raw = (RAWINPUT)Marshal.PtrToStructure(buffer, typeof(RAWINPUT));
                    if (raw.header.dwType == 0) // RIM_TYPEMOUSE
                    {
                        if ((raw.mouse.usButtonFlags & 0x0400) != 0) // RI_MOUSE_WHEEL
                        {
                            short wheelDelta = (short)raw.mouse.usButtonData;
                            return wheelDelta / 120; // 標準捲動增量為 120
                        }
                    }
                }
            }
            finally { Marshal.FreeHGlobal(buffer); }
            return 0;
        }
        #endregion

        #region 2. 介面更新 (UpdateUI)

        private void UpdateUI()
        {
            if (this.InvokeRequired) { this.Invoke(new Action(UpdateUI)); return; }

            // 視覺化反饋
            lblCurrentApp.Text = $"目標軟體: {_activeProcessName}";

            if (_currentMode == ControlMode.DdcCi)
            {
                this.BackColor = Color.FromArgb(45, 45, 48); // 深灰色 (螢幕模式)
                lblModeTitle.Text = "【 螢幕控制模式 】";
                lblModeTitle.ForeColor = Color.Cyan;
                //pbBrightness.Value = (int)_monitor.CurrentBrightness;
            }
            else
            {
                this.BackColor = Color.FromArgb(30, 60, 30); // 深綠色 (軟體模式)
                lblModeTitle.Text = $"【 {_activeProcessName} 快捷模式 】";
                lblModeTitle.ForeColor = Color.Lime;
            }
        }
        #endregion

        #region 3. 核心分發與切換

        private void DispatchCommand(int delta)
        {
            if (_currentMode == ControlMode.DdcCi)
            {
                /*_monitor.AdjustBrightness(delta * 5); // 每次轉動增減 5% */
                UpdateUI();
            }
            else
            {
                // 軟體快捷鍵邏輯
                if (_activeProcessName.Contains("ZBrush"))
                    SendKeys.SendWait(delta > 0 ? "]" : "[");
                else if (_activeProcessName.Contains("Photoshop"))
                    SendKeys.SendWait(delta > 0 ? "{RIGHT}" : "{LEFT}");
            }
        }

        private void SetupAppTracker()
        {
            Timer t = new Timer { Interval = 1000 };
            t.Tick += (s, e) => {
                IntPtr hwnd = GetForegroundWindow();
                GetWindowThreadProcessId(hwnd, out uint pid);
                try
                {
                    string name = Process.GetProcessById((int)pid).ProcessName;
                    if (_activeProcessName != name)
                    {
                        _activeProcessName = name;
                        UpdateUI();
                    }
                }
                catch { }
            };
            t.Start();
        }
        #endregion

        // --- 必要的 Win32 結構體定義 (簡化版) ---
        [StructLayout(LayoutKind.Explicit)]
        public struct RAWINPUT
        {
            [FieldOffset(0)] public RAWINPUTHEADER header;
            [FieldOffset(16)] public RAWMOUSE mouse;
        }
        public struct RAWINPUTHEADER { public uint dwType; public uint dwSize; public IntPtr hDevice; public IntPtr wParam; }
        public struct RAWMOUSE { public ushort usFlags; public ushort usButtonFlags; public ushort usButtonData; public uint ulRawButtons; public int lLastX; public int lLastY; public uint ulExtraInformation; }

        [DllImport("user32.dll")] private static extern bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevices, uint uiNumDevices, uint cbSize);
        [DllImport("user32.dll")] private static extern uint GetRawInputData(IntPtr hRawInput, uint uiCommand, IntPtr pData, ref uint pcbSize, uint cbSizeHeader);
        [StructLayout(LayoutKind.Sequential)] public struct RAWINPUTDEVICE { public ushort usUsagePage; public ushort usUsage; public uint dwFlags; public IntPtr hwndTarget; }
        [DllImport("user32.dll")] private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")] private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
    }

    public enum ControlMode { DdcCi, AppShortcut }
}