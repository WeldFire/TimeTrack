namespace TimeTrack {
    partial class InputOverwriteForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.listOverwrites = new System.Windows.Forms.ListView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtMatch = new System.Windows.Forms.TextBox();
            this.txtReplace = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listOverwrites
            // 
            this.listOverwrites.Dock = System.Windows.Forms.DockStyle.Top;
            this.listOverwrites.GridLines = true;
            this.listOverwrites.LabelEdit = true;
            this.listOverwrites.Location = new System.Drawing.Point(0, 0);
            this.listOverwrites.Name = "listOverwrites";
            this.listOverwrites.Size = new System.Drawing.Size(416, 210);
            this.listOverwrites.TabIndex = 0;
            this.listOverwrites.UseCompatibleStateImageBehavior = false;
            this.listOverwrites.View = System.Windows.Forms.View.Details;
            this.listOverwrites.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.onDoubleClick);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(329, 214);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtMatch
            // 
            this.txtMatch.Location = new System.Drawing.Point(12, 216);
            this.txtMatch.Name = "txtMatch";
            this.txtMatch.Size = new System.Drawing.Size(148, 20);
            this.txtMatch.TabIndex = 2;
            this.txtMatch.Text = "Regex Match Query";
            this.txtMatch.TextChanged += new System.EventHandler(this.txtMatch_TextChanged);
            // 
            // txtReplace
            // 
            this.txtReplace.Location = new System.Drawing.Point(166, 216);
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.Size = new System.Drawing.Size(153, 20);
            this.txtReplace.TabIndex = 3;
            this.txtReplace.Text = "Replacement Text";
            this.txtReplace.TextChanged += new System.EventHandler(this.txtReplace_TextChanged);
            // 
            // InputOverwriteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 242);
            this.Controls.Add(this.txtReplace);
            this.Controls.Add(this.txtMatch);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.listOverwrites);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "InputOverwriteForm";
            this.Text = "InputOverwriteForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InputOverwriteForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listOverwrites;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtMatch;
        private System.Windows.Forms.TextBox txtReplace;

        public System.Windows.Forms.MouseEventHandler onItemSelected {
            get;
            set;
        }
    }
}