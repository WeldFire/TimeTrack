namespace TimeTrack {
    partial class MainForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screenshotDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateTotalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeScreenshotOnForegroundChangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processTitleFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualDisplayModifiersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTimer = new System.Windows.Forms.Timer(this.components);
            this.logView = new System.Windows.Forms.ListView();
            this.lblSelTotal = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.onSecondTick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(304, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.generateTotalsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logDirectoryToolStripMenuItem,
            this.screenshotDirectoryToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // logDirectoryToolStripMenuItem
            // 
            this.logDirectoryToolStripMenuItem.Name = "logDirectoryToolStripMenuItem";
            this.logDirectoryToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.logDirectoryToolStripMenuItem.Text = "Log Directory";
            this.logDirectoryToolStripMenuItem.Click += new System.EventHandler(this.logDirectoryToolStripMenuItem_Click);
            // 
            // screenshotDirectoryToolStripMenuItem
            // 
            this.screenshotDirectoryToolStripMenuItem.Name = "screenshotDirectoryToolStripMenuItem";
            this.screenshotDirectoryToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.screenshotDirectoryToolStripMenuItem.Text = "Screenshot Directory";
            this.screenshotDirectoryToolStripMenuItem.Click += new System.EventHandler(this.screenshotDirectoryToolStripMenuItem_Click);
            // 
            // generateTotalsToolStripMenuItem
            // 
            this.generateTotalsToolStripMenuItem.Name = "generateTotalsToolStripMenuItem";
            this.generateTotalsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.generateTotalsToolStripMenuItem.Text = "Generate Totals";
            this.generateTotalsToolStripMenuItem.Click += new System.EventHandler(this.generateTotalsToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.takeScreenshotOnForegroundChangeToolStripMenuItem,
            this.processTitleFilterToolStripMenuItem,
            this.manualDisplayModifiersToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // takeScreenshotOnForegroundChangeToolStripMenuItem
            // 
            this.takeScreenshotOnForegroundChangeToolStripMenuItem.Name = "takeScreenshotOnForegroundChangeToolStripMenuItem";
            this.takeScreenshotOnForegroundChangeToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.takeScreenshotOnForegroundChangeToolStripMenuItem.Text = "Take Screenshot on foreground change";
            this.takeScreenshotOnForegroundChangeToolStripMenuItem.Click += new System.EventHandler(this.takeScreenshotOnForegroundChangeToolStripMenuItem_Click);
            // 
            // processTitleFilterToolStripMenuItem
            // 
            this.processTitleFilterToolStripMenuItem.Name = "processTitleFilterToolStripMenuItem";
            this.processTitleFilterToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.processTitleFilterToolStripMenuItem.Text = "Process title filter";
            this.processTitleFilterToolStripMenuItem.Click += new System.EventHandler(this.processTitleFilterToolStripMenuItem_Click);
            // 
            // manualDisplayModifiersToolStripMenuItem
            // 
            this.manualDisplayModifiersToolStripMenuItem.Name = "manualDisplayModifiersToolStripMenuItem";
            this.manualDisplayModifiersToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.manualDisplayModifiersToolStripMenuItem.Text = "Manual display name edits";
            this.manualDisplayModifiersToolStripMenuItem.Click += new System.EventHandler(this.manualDisplayModifiersToolStripMenuItem_Click);
            // 
            // saveTimer
            // 
            this.saveTimer.Enabled = true;
            this.saveTimer.Interval = 300000;
            this.saveTimer.Tick += new System.EventHandler(this.onSaveTick);
            // 
            // logView
            // 
            this.logView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logView.LabelWrap = false;
            this.logView.Location = new System.Drawing.Point(0, 24);
            this.logView.Name = "logView";
            this.logView.ShowGroups = false;
            this.logView.Size = new System.Drawing.Size(304, 128);
            this.logView.TabIndex = 4;
            this.logView.UseCompatibleStateImageBehavior = false;
            this.logView.View = System.Windows.Forms.View.List;
            // 
            // lblSelTotal
            // 
            this.lblSelTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelTotal.AutoSize = true;
            this.lblSelTotal.Location = new System.Drawing.Point(228, 130);
            this.lblSelTotal.Name = "lblSelTotal";
            this.lblSelTotal.Size = new System.Drawing.Size(0, 13);
            this.lblSelTotal.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 152);
            this.Controls.Add(this.lblSelTotal);
            this.Controls.Add(this.logView);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "TimeTrack";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processTitleFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualDisplayModifiersToolStripMenuItem;
        private System.Windows.Forms.Timer saveTimer;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateTotalsToolStripMenuItem;
        private System.Windows.Forms.ListView logView;
        private System.Windows.Forms.Label lblSelTotal;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem screenshotDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeScreenshotOnForegroundChangeToolStripMenuItem;
    }
}

