using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeTrack {
    public partial class LogOverviewForm :Form {
        Dictionary<String, TimeSpan> dictionary;
        public LogOverviewForm() {
            InitializeComponent();
            loadLog();
        }

        private void loadLog() {
            dictionary = Settings.DeserializeAllDictionaries(Settings.LogFile);
            
            updateLogView();
        }

        private Dictionary<String, TimeSpan> sortDictionary(Dictionary<String, TimeSpan> input) {
            List<KeyValuePair<String, TimeSpan>> listToSort = input.ToList();

            listToSort.Sort((firstPair, secondPair) => firstPair.Key.CompareTo(secondPair.Key));

            return listToSort.ToDictionary(item => item.Key, item => item.Value);
        }

        private void updateLogView() {
            string logText = "";
            int longestWidth = 0;

            Dictionary<string, TimeSpan> viewDictionary = this.sortDictionary(MainForm.collapseDictionary(this.dictionary));

            foreach(var element in viewDictionary) {
                logText += string.Format("{0}, {1}\n", element.Key.ToString(), element.Value.ToString());
            }

            ListViewItem[] selectedList = new ListViewItem[this.logView.SelectedItems.Count];
            this.logView.SelectedItems.CopyTo(selectedList, 0);

            this.logView.Clear();
            foreach(string item in logText.Split('\n')) {
                ListViewItem currentItem = this.logView.Items.Add(item);

                string itemName = item.Split(',')[0];

                //Reselect all previously selected lines
                foreach(ListViewItem selectedItem in selectedList) {
                    if(itemName == selectedItem.Text.Split(',')[0]) {
                        currentItem.Selected = true;
                        break;
                    }
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

        private void totalTimer_Tick(object sender, EventArgs e) {
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
    }
}
