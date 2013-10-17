namespace TimeTrack {
    partial class LogOverviewForm {
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
            this.components = new System.ComponentModel.Container();
            this.logView = new System.Windows.Forms.ListView();
            this.lblSelTotal = new System.Windows.Forms.Label();
            this.totalTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // logView
            // 
            this.logView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logView.LabelWrap = false;
            this.logView.Location = new System.Drawing.Point(0, 0);
            this.logView.Name = "logView";
            this.logView.ShowGroups = false;
            this.logView.Size = new System.Drawing.Size(261, 122);
            this.logView.TabIndex = 5;
            this.logView.UseCompatibleStateImageBehavior = false;
            this.logView.View = System.Windows.Forms.View.List;
            // 
            // lblSelTotal
            // 
            this.lblSelTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelTotal.AutoSize = true;
            this.lblSelTotal.Location = new System.Drawing.Point(190, 100);
            this.lblSelTotal.Name = "lblSelTotal";
            this.lblSelTotal.Size = new System.Drawing.Size(0, 13);
            this.lblSelTotal.TabIndex = 6;
            // 
            // totalTimer
            // 
            this.totalTimer.Enabled = true;
            this.totalTimer.Interval = 1000;
            this.totalTimer.Tick += new System.EventHandler(this.totalTimer_Tick);
            // 
            // LogOverviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 122);
            this.Controls.Add(this.lblSelTotal);
            this.Controls.Add(this.logView);
            this.Name = "LogOverviewForm";
            this.Text = "LogOverviewForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView logView;
        private System.Windows.Forms.Label lblSelTotal;
        private System.Windows.Forms.Timer totalTimer;
    }
}