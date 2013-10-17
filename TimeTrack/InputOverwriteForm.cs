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
    public partial class InputOverwriteForm :Form {
        public string watermarkRegex = "Regex Match Query";
        public string watermarkReplace = "Replacement Text";
        public InputOverwriteForm() {
            InitializeComponent();

            // Add columns
            this.listOverwrites.Columns.Add("Match Regex", -2, HorizontalAlignment.Left);
            this.listOverwrites.Columns.Add("Replacement Text", -2, HorizontalAlignment.Left);

            //Add saved elements
            foreach(KeyValuePair<string, string> item in Program.replacementList) {
                var newItem = new ListViewItem(new[] { item.Key, item.Value });
                this.listOverwrites.Items.Add(newItem);
            }
        }

        private void txtReplace_TextChanged(object sender, EventArgs e) {
            if(this.txtReplace.Text.Length == 0) {
                this.txtReplace.Text = watermarkReplace;
            } else if(this.txtReplace.Text != watermarkReplace) {
                if(this.txtReplace.Text.Contains(watermarkReplace)) {
                    this.txtReplace.Text = this.txtReplace.Text.Replace(watermarkReplace, "");
                    this.txtReplace.SelectionStart = this.txtReplace.Text.Length;
                    this.txtReplace.SelectionLength = 0;
                }
            }
        }

        private void txtMatch_TextChanged(object sender, EventArgs e) {
            if(this.txtMatch.Text.Length == 0) {
                this.txtMatch.Text = watermarkRegex;
            } else if(this.txtMatch.Text != watermarkRegex) {
                if(this.txtMatch.Text.Contains(watermarkRegex)) {
                    this.txtMatch.Text = this.txtMatch.Text.Replace(watermarkRegex, "");
                    this.txtMatch.SelectionStart = this.txtMatch.Text.Length;
                    this.txtMatch.SelectionLength = 0;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            if(this.txtReplace.Text != this.watermarkReplace && this.txtMatch.Text != this.watermarkRegex) {
                var item = new ListViewItem(new[] { this.txtMatch.Text.ToString(), this.txtReplace.Text.ToString() });
                this.txtMatch.Text = "";
                this.txtReplace.Text = "";
                this.listOverwrites.Items.Add(item);
            }
        }

        private void InputOverwriteForm_FormClosing(object sender, FormClosingEventArgs e) {
            Dictionary<string, string> inputOverwrite = new Dictionary<string, string>();
            foreach(ListViewItem item in this.listOverwrites.Items) {
                inputOverwrite.Add(item.SubItems[0].Text.ToString(), item.SubItems[1].Text.ToString());
            }
            Program.replacementList.Clear();
            Program.replacementList = inputOverwrite;

            Settings.Serialize(Program.replacementList, Settings.replacementListFile);
        }

        private void onDoubleClick(object sender, MouseEventArgs e) {
            DialogResult result = MessageBox.Show("Would you like to delete this item?", "Are you sure?", MessageBoxButtons.YesNo);
            if(result == System.Windows.Forms.DialogResult.Yes) {
                this.listOverwrites.Items.Remove(this.listOverwrites.SelectedItems[0]);
            }
        }
    }
}
