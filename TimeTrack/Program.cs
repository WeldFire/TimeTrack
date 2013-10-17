using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeTrack {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static Dictionary<string, string> replacementList;
        public static Dictionary<string, TimeSpan> Log;
        public static List<string> titleOfProcess;
        public static string daySession;

        [STAThread]
        static void Main() {
            Program.daySession = Settings.todaysName(Settings.LogFile);
            Program.setup();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            Program.breakdown();
        }

        private static void setup() {
            Settings.setup();
            Program.Log = Settings.DeserializeDictionary(Program.daySession);
            Program.titleOfProcess = Settings.DeserializeList(Settings.TitleOfProcessesFile);
            Program.replacementList = Settings.DeserializeStringDictionary(Settings.replacementListFile);
        }

        private static void breakdown() {
            Settings.Serialize(Program.Log, Program.daySession);
        }
    }
}
