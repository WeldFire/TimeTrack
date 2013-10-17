using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrack {
    public class Settings {
        public static string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TimeTrack";
        public static string ScreenShotDirectory = directoryPath + "\\screenshots";
        public const string LogFile = "time.db";
        public const string replacementListFile = "replacement.lst";
        public const string TitleOfProcessesFile = "tOp.lst";
        public const string SettingsFile = "settings.ini";

        public static string loadFile(string name) {
            string returned = "";

            // Write the string to a file.
            try {
                System.IO.StreamReader file = new System.IO.StreamReader(directoryPath + "\\" + name);
                returned = file.ReadToEnd();

                file.Close();
            } catch(DirectoryNotFoundException) {
                //Throw away the excepton for now....
            } catch(FileNotFoundException) {
                //Throw away the excepton for now....
            }

            return returned;
        }

        public static void saveFile(string name, string data) {
            setup(name);

            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter(directoryPath + "\\" + name);
            file.Write(data);

            file.Close();
        }

        public static void backupFile(string name) {
            setup(name);

            if(File.Exists(directoryPath + "\\" + name)) {
                try {
                    File.Copy(directoryPath + "\\" + name, directoryPath + "\\" + name + ".bak", true);
                } catch(IOException ex) {
                    Debug.WriteLine("File in use\n" + ex.Message);
                }
            }
        }

        public static string todaysName(string name) {
            DateTime today = DateTime.Now;

            return today.ToString("MM-dd-yy") + "_" + name;
        }

        public static void Serialize(Dictionary<string, string> dictionary, string name) {
            setup(name);

            Stream file = File.OpenWrite(directoryPath + "\\" + name);

            BinaryWriter writer = new BinaryWriter(file);
            writer.Write(dictionary.Count);
            foreach(var kvp in dictionary) {
                writer.Write(kvp.Key);
                writer.Write(kvp.Value.ToString());
            }
            writer.Flush();

            file.Close();
        }

        public static void Serialize(Dictionary<string, TimeSpan> dictionary, string name) {
            setup(name);

            Stream file = File.OpenWrite(directoryPath + "\\" + name);

            BinaryWriter writer = new BinaryWriter(file);
            writer.Write(dictionary.Count);
            foreach(var kvp in dictionary) {
                writer.Write(kvp.Key);
                writer.Write(kvp.Value.ToString());
            }
            writer.Flush();

            file.Close();
        }

        public static void Serialize(List<string> list, string name) {
            setup(name);

            Stream file = File.OpenWrite(directoryPath + "\\" + name);

            BinaryWriter writer = new BinaryWriter(file);
            writer.Write(list.Count);
            foreach(string line in list) {
                writer.Write(line);
            }
            writer.Flush();

            file.Close();
        }

        public static Dictionary<string, TimeSpan> DeserializeAllDictionaries(string name) {
            Dictionary<string, TimeSpan> dictionary = new Dictionary<string, TimeSpan>();
            foreach(string file in Directory.GetFiles(directoryPath, "*" + name)) {
                string fileName = file.Substring(file.LastIndexOf('\\') + 1);
                Dictionary<string, TimeSpan> smallDictionary = Settings.DeserializeDictionary(fileName);

                foreach(KeyValuePair<string, TimeSpan> pair in smallDictionary) {
                    if(dictionary.ContainsKey(pair.Key)) {
                        dictionary[pair.Key] = dictionary[pair.Key].Add(pair.Value);
                    } else {
                        dictionary.Add(pair.Key, pair.Value);
                    }
                }
            }
            return dictionary;
        }

        public static Dictionary<string, string> DeserializeStringDictionary(string name) {
            Dictionary<string, string> dictionary;

            if(File.Exists(directoryPath + "\\" + name)) {
                Stream file = File.OpenRead(directoryPath + "\\" + name);

                BinaryReader reader = new BinaryReader(file);
                int count = reader.ReadInt32();
                dictionary = new Dictionary<string, string>(count);
                for(int n = 0; n < count; n++) {
                    var key = reader.ReadString();
                    var value = reader.ReadString();
                    dictionary.Add(key, value);
                }
                file.Close();
            } else {
                dictionary = new Dictionary<string, string>();
            }

            return dictionary;
        }

        public static Dictionary<string, TimeSpan> DeserializeDictionary(string name) {
            Dictionary<string, TimeSpan> dictionary;

            if(File.Exists(directoryPath + "\\" + name)) {
                Stream file = File.OpenRead(directoryPath + "\\" + name);

                BinaryReader reader = new BinaryReader(file);
                int count = reader.ReadInt32();
                dictionary = new Dictionary<string, TimeSpan>(count);
                for(int n = 0; n < count; n++) {
                    var key = reader.ReadString();
                    var value = reader.ReadString();
                    dictionary.Add(key, TimeSpan.Parse(value));
                }
                file.Close();
            } else {
                dictionary = new Dictionary<string, TimeSpan>();   
            }

            return dictionary;
        }

        public static List<string> DeserializeList(string name) {
            List<string> list;

            if(File.Exists(directoryPath + "\\" + name)) {
                Stream file = File.OpenRead(directoryPath + "\\" + name);

                BinaryReader reader = new BinaryReader(file);
                int count = reader.ReadInt32();
                list = new List<string>();

                for(int n = 0; n < count; n++) {
                    var line = reader.ReadString();
                    list.Add(line);
                }
                file.Close();
            } else {
                list = new List<string>();
            }

            return list;
        }

        public static void setup() {
            if(!Directory.Exists(Settings.directoryPath)) {
                Directory.CreateDirectory(Settings.directoryPath);
            }

            if(!Directory.Exists(Settings.ScreenShotDirectory)) {
                Directory.CreateDirectory(Settings.ScreenShotDirectory);
            }
        }

        private static void setup(string name) {
            if(!File.Exists(Settings.directoryPath + "\\" + name)) {
                File.Create(Settings.directoryPath + "\\" + name).Close();
            }
        }
    }
}
