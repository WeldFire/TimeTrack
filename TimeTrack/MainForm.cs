using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeTrack {
    public partial class MainForm :Form {
        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        private const uint WINEVENT_OUTOFCONTEXT = 0;
        private const uint EVENT_SYSTEM_FOREGROUND = 3;

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint GetWindowModuleFileName(IntPtr hwnd,
           StringBuilder lpszFileName, uint cchFileNameMax);



        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO {
            public static readonly int SizeOf = Marshal.SizeOf(typeof(LASTINPUTINFO));

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dwTime;
        }


        WinEventDelegate dele;
        IntPtr m_hhook;
        string forgroundApp;
        public const int idleSeconds = 10;

        public MainForm() {
            InitializeComponent();
            forgroundApp = "";

            bool takeScreenshot = false;
            if(bool.TryParse(Settings.loadFile(Settings.SettingsFile), out takeScreenshot)){
                this.takeScreenshotOnForegroundChangeToolStripMenuItem.Checked = takeScreenshot;
            }

            dele = new WinEventDelegate(WinEventProc);
            m_hhook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, dele, 0, 0, WINEVENT_OUTOFCONTEXT);
        }


        private string GetActiveWindowTitle() {
            const int nChars = 256;
            IntPtr handle = IntPtr.Zero;
            StringBuilder Buff = new StringBuilder(nChars);
            handle = GetForegroundWindow();

            if(GetWindowText(handle, Buff, nChars) > 0) {
                return Buff.ToString();
            }
            return null;
        }

        public Process GetCurrentProcess() {
            IntPtr handle = GetForegroundWindow();

            uint processID = 0;
            GetWindowThreadProcessId(handle, out processID);

            Process pro = null;
            try {
                pro = Process.GetProcessById((int)processID);
            }catch(Exception e){
                Debug.WriteLine(e.Message);
            }

            return pro;
        }

        public void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime) {
            this.onForegroundWindowChanged(hwnd);
        }

        public void onForegroundWindowChanged(IntPtr foregroundHwnd) {
            this.forgroundApp = getForgroundApp();

            if(this.takeScreenshotOnForegroundChangeToolStripMenuItem.Checked) {
                System.Threading.Thread.Sleep(1000);//Sleep for one second before taking a screenshot
                this.saveForgroundScreenshot(Settings.ScreenShotDirectory, this.forgroundApp, foregroundHwnd);
            }
        }

        public void saveForgroundScreenshot(string path, string windowName, IntPtr foregroundHwnd) {
            Bitmap foregroundImage = Win32.PrintWindow(foregroundHwnd);

            string filename = DateTime.Now.ToString("MM-dd-yy-hh-mm-ss") + "_" + windowName + ".jpg";

            foregroundImage.Save(path + "\\" + filename, ImageFormat.Jpeg);
            foregroundImage.Dispose();
        }

        private string getForgroundApp() {
            string forApp;
            string processname = "";
            Process retrievedProcess = this.GetCurrentProcess();
            if(retrievedProcess != null){
                processname = retrievedProcess.ProcessName;
            }
            if(!string.IsNullOrEmpty(processname) && !Program.titleOfProcess.Contains(processname)) {
                forApp = processname;
            } else {
                string activeWin = GetActiveWindowTitle();
                forApp = activeWin;
            }
            return forApp;
        }

        static int GetLastInputTime() {
            uint idleTime = 0;
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
            lastInputInfo.dwTime = 0;

            uint envTicks = (uint)Environment.TickCount;

            if(GetLastInputInfo(ref lastInputInfo)) {
                uint lastInputTick = lastInputInfo.dwTime;

                idleTime = envTicks - lastInputTick;
            }

            return (int)((idleTime > 0) ? (idleTime / 1000) : 0);
        }

        private void onSecondTick(object sender, EventArgs e) {
            int lastInputTime = GetLastInputTime();
            if(lastInputTime >= idleSeconds) {//Check if the user is idle
                DrawingControl.SuspendDrawing(this.logView);
                timeProgram("Total Idle Time");
                updateLogView();
                this.logView.Items.Insert(0, "In Idle: " + TimeSpan.FromSeconds(lastInputTime).ToString()).BackColor = Color.PaleVioletRed;
                DrawingControl.ResumeDrawing(this.logView);
            } else {
                if(!string.IsNullOrEmpty(forgroundApp)) {
                    timeProgram(forgroundApp);
                }
                DrawingControl.SuspendDrawing(this.logView);
                updateLogView();
                DrawingControl.ResumeDrawing(this.logView);
            }
        }

        private void timeProgram(string application) {
            if(!Program.Log.ContainsKey(application)) {
                Program.Log.Add(application, new TimeSpan());
            } else {
                Program.Log[application] = Program.Log[application].Add(TimeSpan.FromSeconds(1));
            }
        }

        public static string applyManualEdits(string name) {
            string newName = "";
            bool found = false;
            foreach(KeyValuePair<string, string> edit in Program.replacementList) {
                if(!string.IsNullOrEmpty(name) && Regex.IsMatch(name, edit.Key)) {
                    found = true;
                    Regex rgx = new Regex(edit.Key);
                    GroupCollection matchGroups = rgx.Match(name).Groups;
                    if(matchGroups.Count > 0) {
                        newName = edit.Value;
                        Regex templateRegex = new Regex(@"<(\w+)>");
                        foreach(Match itemMatch in templateRegex.Matches(edit.Value)) {
                            if(matchGroups[itemMatch.Groups[1].Value] != null) {
                                string pattern = itemMatch.Value;
                                string replacement = matchGroups[itemMatch.Groups[1].Value].Value;
                                newName = Regex.Replace(newName, pattern, replacement);
                            }
                        }
                    } else {
                        newName = rgx.Replace(name, edit.Value);
                    }
                }
            }

            if(found) {
                return newName;
            }

            return name;
        }

        public static Dictionary<string, TimeSpan> collapseDictionary(Dictionary<string, TimeSpan> dictionary) {
            Dictionary<string, TimeSpan> tempDictionary = new Dictionary<string, TimeSpan>();
            foreach(var element in dictionary) {
                if(tempDictionary.ContainsKey(MainForm.applyManualEdits(element.Key))) {
                    tempDictionary[MainForm.applyManualEdits(element.Key)] = tempDictionary[MainForm.applyManualEdits(element.Key)].Add(element.Value);
                } else {
                    tempDictionary.Add(MainForm.applyManualEdits(element.Key), element.Value);
                }
            }
            return tempDictionary;
        }

        private void updateLogView() {
            string logText = "";
            int longestWidth = 0;

            //Make sure that no elements repeat by collapsing the dictionary
            Dictionary<string, TimeSpan> tempDictionary = MainForm.collapseDictionary(Program.Log);

            //Build the manually edited items
            foreach(var element in tempDictionary) {
                logText += string.Format("{0}, {1}\n", MainForm.applyManualEdits(element.Key.ToString()), element.Value.ToString());
            }

            //Save the selected items in the view
            ListViewItem[] selectedList = new ListViewItem[this.logView.SelectedItems.Count];
            this.logView.SelectedItems.CopyTo(selectedList, 0);

            //Save the focused element name
            string lastFocusedItemName = "";
            if(this.logView.FocusedItem != null) {
                if(this.logView.FocusedItem.Name != "")
                    lastFocusedItemName = this.logView.FocusedItem.Name.Split(',')[0];
            }

            this.logView.Clear();
            foreach(string item in logText.Split('\n')) {
                ListViewItem currentItem = this.logView.Items.Add(item);

                string itemName = item.Split(',')[0];
                //Highlight the application in the forground
                if(itemName == MainForm.applyManualEdits(this.forgroundApp)) {
                    currentItem.BackColor = Color.Yellow;
                }

                //Reselect all previously selected lines
                foreach(ListViewItem selectedItem in selectedList) {
                    if(itemName == selectedItem.Text.Split(',')[0]) {
                        currentItem.Selected = true;
                        break;
                    }
                }

                //Set focused item
                if(itemName == lastFocusedItemName) {
                    currentItem.Focused = true;
                }

                //Find the largest size text to scale the form
                Size lineSize = TextRenderer.MeasureText(currentItem.Text, currentItem.Font);
                if(lineSize.Width > longestWidth) {
                    longestWidth = lineSize.Width;
                }
            }

            //Resize the form
            this.Width = longestWidth + 30;

            //Calculate selected time
            if(this.logView.SelectedItems.Count > 0) {
                TimeSpan total = new TimeSpan();
                foreach(ListViewItem selectedItem in this.logView.SelectedItems) {
                    string timeText = selectedItem.Text;
                    if(!string.IsNullOrEmpty(timeText)) {
                        timeText = timeText.Substring(timeText.LastIndexOf(',')+1).Trim();
                        TimeSpan timeSpanTime = TimeSpan.Parse(timeText);
                        total = total.Add(timeSpanTime);
                    }
                }
                this.lblSelTotal.Text = total.ToString();
            } else {
                this.lblSelTotal.Text = "";
            }
        }

        private void manualDisplayModifiersToolStripMenuItem_Click(object sender, EventArgs e) {
            Form f = new InputOverwriteForm();
            f.Show();
        }

        private void processTitleFilterToolStripMenuItem_Click(object sender, EventArgs e) {
            Form f = new listBuilderForm();
            f.Show();
        }

        private void onSaveTick(object sender, EventArgs e) {
            Settings.backupFile(Program.daySession);
            Settings.Serialize(Program.Log, Program.daySession);//Sometimes gets a file lock exception TODO investigate
        }

        private void generateTotalsToolStripMenuItem_Click(object sender, EventArgs e) {
            Form f = new LogOverviewForm();
            f.Show();
        }

        private void logDirectoryToolStripMenuItem_Click(object sender, EventArgs e) {
            Process.Start(Settings.directoryPath);
        }

        private void screenshotDirectoryToolStripMenuItem_Click(object sender, EventArgs e) {
            Process.Start(Settings.ScreenShotDirectory);
        }

        private void takeScreenshotOnForegroundChangeToolStripMenuItem_Click(object sender, EventArgs e) {
            ToolStripMenuItem thisItem = (ToolStripMenuItem)sender;

            thisItem.Checked = !thisItem.Checked;

            Settings.saveFile(Settings.SettingsFile, thisItem.Checked.ToString());
        }
    }

    class DrawingControl {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        private const int WM_SETREDRAW = 11;

        public static void SuspendDrawing(Control parent) {
            SendMessage(parent.Handle, WM_SETREDRAW, false, 0);
        }

        public static void ResumeDrawing(Control parent) {
            SendMessage(parent.Handle, WM_SETREDRAW, true, 0);
            parent.Refresh();
        }
    }
}
