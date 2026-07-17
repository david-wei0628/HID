using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Management; // 用於讀取 WMI 螢幕硬體資訊
using System.Windows.Forms;
using System.Drawing;

public class DdcCiMonitorController
{
    #region Win32 API 宣告

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct PHYSICAL_MONITOR
    {
        public IntPtr hPhysicalMonitor;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szPhysicalMonitorDescription;
    }

    // 用於列舉所有邏輯螢幕
    private delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    [DllImport("user32.dll")]
    private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumDelegate lpfnEnum, IntPtr dwData);

    [DllImport("dxva2.dll", SetLastError = true)]
    private static extern bool GetNumberOfPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, out uint pdwNumberOfPhysicalMonitors);

    [DllImport("dxva2.dll", SetLastError = true)]
    private static extern bool GetPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, uint dwPhysicalMonitorArraySize, [Out] PHYSICAL_MONITOR[] pPhysicalMonitorArray);

    [DllImport("dxva2.dll", SetLastError = true)]
    private static extern bool DestroyPhysicalMonitors(uint dwPhysicalMonitorArraySize, PHYSICAL_MONITOR[] pPhysicalMonitorArray);

    // 高階 API 亮度
    [DllImport("dxva2.dll", SetLastError = true)]
    private static extern bool SetMonitorBrightness(IntPtr hMonitor, uint dwNewBrightness);

    [DllImport("dxva2.dll", SetLastError = true)]
    private static extern bool GetMonitorBrightness(IntPtr hMonitor, out uint pdwMinimumBrightness, out uint pdwCurrentBrightness, out uint pdwMaximumBrightness);

    // 高階 API 對比
    [DllImport("dxva2.dll", SetLastError = true)]
    private static extern bool SetMonitorContrast(IntPtr hMonitor, uint dwNewBrightness);

    [DllImport("dxva2.dll", SetLastError = true)]
    private static extern bool GetMonitorContrast(IntPtr hMonitor, out uint pdwMinimumBrightness, out uint pdwCurrentBrightness, out uint pdwMaximumBrightness);

    // 低階 API (控制輸入源、色彩模式等)
    [DllImport("dxva2.dll", SetLastError = true)]
    private static extern bool LowLevelSetVCPFeature(IntPtr hMonitor, byte bVCPCode, uint dwNewValue);

    // 補上 MonitorFromScreen 需要使用的 Win32 底層 API
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr MonitorFromPoint(System.Drawing.Point pt, uint dwFlags);

    #endregion

    // 儲存目前偵測到的實體螢幕控制代碼與描述
    public class MonitorInfo
    {
        public IntPtr Handle { get; set; }
        public string Name { get; set; }
        public override string ToString() => Name;
    }

    /// <summary>
    /// 補上缺失的輔助方法，將 WinForm 的 Screen 物件安全轉換為 HMONITOR
    /// </summary>
    private static IntPtr MonitorFromScreen(Screen screen)
    {
        // 取螢幕中心點座標
        var centerPoint = new System.Drawing.Point(
            screen.Bounds.Left + screen.Bounds.Width / 2,
            screen.Bounds.Top + screen.Bounds.Height / 2
        );
        // 1 代表 MONITOR_DEFAULTTONEAREST
        return MonitorFromPoint(centerPoint, 1);
    }

    /*/// <summary>
    /// 取得目前系統連接的所有實體螢幕列表 (建議在 Task.Run 中執行)
    /// </summary>
    public static List<MonitorInfo> GetAllMonitors()
    {
        var monitorList = new List<MonitorInfo>();

        EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, (IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData) =>
        {
            if (GetNumberOfPhysicalMonitorsFromHMONITOR(hMonitor, out uint physicalCount))
            {
                var physicalArray = new PHYSICAL_MONITOR[physicalCount];
                if (GetPhysicalMonitorsFromHMONITOR(hMonitor, physicalCount, physicalArray))
                {
                    foreach (var phys in physicalArray)
                    {
                        monitorList.Add(new MonitorInfo
                        {
                            Handle = phys.hPhysicalMonitor, // 注意：這裡為了簡化範例直接抓取，實務上這些 Handle 在用完後需被妥善管理
                            Name = phys.szPhysicalMonitorDescription
                        });
                    }
                }
            }
            return true;
        }, IntPtr.Zero);

        return monitorList;
    }*/

    public static List<MonitorInfo> GetAllMonitors()
    {
        var monitorList = new List<MonitorInfo>();

        // 1. 先透過 WMI 抓取全電腦所有連接的實體螢幕硬體型號
        var hardwareNames = GetMonitorHardwareNames();

        int index = 0;
        foreach (var screen in Screen.AllScreens)
        {
            IntPtr hMonitor = MonitorFromScreen(screen);

            if (hMonitor != IntPtr.Zero && GetNumberOfPhysicalMonitorsFromHMONITOR(hMonitor, out uint physicalCount))
            {
                var physicalArray = new PHYSICAL_MONITOR[physicalCount];
                if (GetPhysicalMonitorsFromHMONITOR(hMonitor, physicalCount, physicalArray))
                {
                    foreach (var phys in physicalArray)
                    {
                        // 2. 優先使用 WMI 抓到的實體型號，如果抓不到，才用原本系統的描述
                        string realName = phys.szPhysicalMonitorDescription;
                        Console.WriteLine(realName);
                        if (index < hardwareNames.Count && !string.IsNullOrEmpty(hardwareNames[index]))
                        {
                            realName = hardwareNames[index];
                        }

                        monitorList.Add(new MonitorInfo
                        {
                            Handle = phys.hPhysicalMonitor,
                            Name = realName // 這裡就會是精準的型號                            
                        });
                        index++;
                    }
                }
            }
        }
        return monitorList;
    }

    /// <summary>
    /// 輔助方法：透過 WMI 讀取螢幕晶片的實體型號名稱
    /// </summary>
    private static List<string> GetMonitorHardwareNames()
    {
        var names = new List<string>();
        try
        {
            // 查詢 Windows 的 WmiMonitorID 類別
            using (var searcher = new ManagementObjectSearcher(@"Root\W_M_I", "SELECT * FROM WmiMonitorID"))
            {
                foreach (ManagementObject mo in searcher.Get())
                {
                    // UserFriendlyName 儲存的是 EDID 裡面的實體型號代碼（為不連續的通訊陣列）
                    ushort[] nameArray = (ushort[])mo["UserFriendlyName"];
                    if (nameArray != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (ushort c in nameArray)
                        {
                            if (c == 0) break;
                            sb.Append((char)c);
                        }
                        string displayName = sb.ToString().Trim();
                        // 如果型號前面沒有品牌，補上 ASUS (非必要，看個人喜好)
                        if (!string.IsNullOrEmpty(displayName))
                        {
                            names.Add(displayName);
                        }
                    }
                }
            }
        }
        catch
        {
            // 萬一 WMI 被系統禁用，回傳空列表，不影響原本 DDC/CI 運作
        }
        return names;
    }

    /// <summary>
    /// 非同步獲取特定螢幕的當前亮度
    /// </summary>
    /// <returns>回傳當前亮度 (0~100)，若失敗則回傳 -1</returns>
    public static Task<int> GetBrightnessAsync(IntPtr hPhysicalMonitor)
    {
        return Task.Run(() =>
        {
            uint min, current, max;
            // 呼叫 Win32 API 讀取硬體數值
            if (GetMonitorBrightness(hPhysicalMonitor, out min, out current, out max))
            {
                return (int)current; // 成功則回傳目前亮度
            }
            return -1; // 失敗（例如螢幕不支援、HDR開啟或線材阻斷）
        });
    }

    /// <summary>
    /// 非同步調整特定螢幕的亮度
    /// </summary>
    public static Task<bool> SetBrightnessAsync(IntPtr hPhysicalMonitor, uint brightness)
    {
        return Task.Run(() =>
        {
            if (brightness > 100) brightness = 100;
            // DDC/CI 硬體通訊需要時間，丟到背景執行緒處理
            return SetMonitorBrightness(hPhysicalMonitor, brightness);
        });
    }

     /// <summary>
    /// 非同步獲取特定螢幕的當前對比
    /// </summary>
    /// <returns>回傳當前對比 (0~100)，若失敗則回傳 -1</returns>
    public static Task<int> GetContrastAsync(IntPtr hPhysicalMonitor)
    {
        return Task.Run(() =>
        {
            uint min, current, max;
            // 呼叫 Win32 API 讀取硬體數值
            if (GetMonitorContrast(hPhysicalMonitor, out min, out current, out max))
            {
                return (int)current; // 成功則回傳目前對比
            }
            return -1; // 失敗（例如螢幕不支援、HDR開啟或線材阻斷）
        });
    }

    /// <summary>
    /// 非同步調整特定螢幕的對比
    /// </summary>
    public static Task<bool> SetContrastAsync(IntPtr hPhysicalMonitor, uint contrast)
    {
        return Task.Run(() =>
        {
            if (contrast > 100) contrast = 100;
            // DDC/CI 硬體通訊需要時間，丟到背景執行緒處理
            return SetMonitorContrast(hPhysicalMonitor, contrast);
        });
    }

    /// <summary>
    /// 非同步切換 ASUS 螢幕的輸入源 (VCP 0x60)
    /// </summary>
    /// <param name="sourceCode">ASUS 常見代碼 - 3: HDMI1, 4: HDMI2, 15: DisplayPort1, 16: DisplayPort2, 17: USB-C</param>
    public static Task<bool> SwitchInputAsync(IntPtr hPhysicalMonitor, uint sourceCode)
    {
        return Task.Run(() =>
        {
            // 0x60 為 VESA 標準的輸入源切換代碼
            return LowLevelSetVCPFeature(hPhysicalMonitor, 0x60, sourceCode);
        });
    }
}