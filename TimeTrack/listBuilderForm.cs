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
    public partial class listBuilderForm :Form {
        string watermark = "Insert new item";

        public listBuilderForm() {
            InitializeComponent();

            foreach(string item in Program.titleOfProcess) {
                this.listBox.Items.Add(item);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            string inputText = txtInput.Text.ToString();
            if(!string.IsNullOrEmpty(inputText) && inputText != watermark) {
                listBox.Items.Add(inputText);
                txtInput.Text = "";
            }
        }

        private void onItemSelected(object sender, MouseEventArgs e) {
            int index = this.listBox.IndexFromPoint(e.Location);//Find which item was clicked

            //Make sure the item exists
            if(index != System.Windows.Forms.ListBox.NoMatches) {
                DialogResult result = MessageBox.Show("Would you like to delete this item?", "Are you sure?", MessageBoxButtons.YesNo);
                if(result == System.Windows.Forms.DialogResult.Yes) {
                    this.listBox.Items.RemoveAt(index);
                }
            }
        }

        private void onTextChange(object sender, EventArgs e) {
            if(this.txtInput.Text.Length == 0) {
                this.txtInput.Text = watermark;
            } else if(this.txtInput.Text != watermark){
                if(this.txtInput.Text.Contains(watermark)) {
                    this.txtInput.Text = this.txtInput.Text.Replace(watermark, "");
                    this.txtInput.SelectionStart = this.txtInput.Text.Length;
                    this.txtInput.SelectionLength = 0;
                }
            }
        }

        private void onClose(object sender, FormClosingEventArgs e) {
            List<string> newTitleOfProcess = new List<string>();
            foreach(string item in this.listBox.Items) {
                newTitleOfProcess.Add(item);
            }
            Program.titleOfProcess.Clear();
            Program.titleOfProcess = newTitleOfProcess;

            Settings.Serialize(Program.titleOfProcess, Settings.TitleOfProcessesFile);
        }
    }
}
