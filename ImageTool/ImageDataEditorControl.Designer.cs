namespace Chadsoft.CTools.Image
{
    partial class ImageDataEditorControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageDataEditorControl));
            this.zoomHeaderLabel = new System.Windows.Forms.Label();
            this.zoomTrackBar = new System.Windows.Forms.TrackBar();
            this.viewsLabel = new System.Windows.Forms.Label();
            this.viewComboBox = new System.Windows.Forms.ComboBox();
            this.displayPanel = new System.Windows.Forms.Panel();
            this.displayPictureBox = new System.Windows.Forms.PictureBox();
            this.mipLabel = new System.Windows.Forms.Label();
            this.formatLabel = new System.Windows.Forms.Label();
            this.dimensionsLabel = new System.Windows.Forms.Label();
            this.mipNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.reformatButton = new System.Windows.Forms.Button();
            this.importButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.explanationToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.zoomValueLabel = new System.Windows.Forms.Label();
            this.formatProgressBar = new System.Windows.Forms.ProgressBar();
            this.FileOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.FileSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.formatterBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.displayVScrollBar = new System.Windows.Forms.VScrollBar();
            this.displayHScrollBar = new System.Windows.Forms.HScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBar)).BeginInit();
            this.displayPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mipNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // zoomHeaderLabel
            // 
            resources.ApplyResources(this.zoomHeaderLabel, "zoomHeaderLabel");
            this.zoomHeaderLabel.Name = "zoomHeaderLabel";
            // 
            // zoomTrackBar
            // 
            resources.ApplyResources(this.zoomTrackBar, "zoomTrackBar");
            this.zoomTrackBar.LargeChange = 32;
            this.zoomTrackBar.Maximum = 127;
            this.zoomTrackBar.Minimum = -128;
            this.zoomTrackBar.Name = "zoomTrackBar";
            this.zoomTrackBar.TickFrequency = 32;
            this.zoomTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.explanationToolTip.SetToolTip(this.zoomTrackBar, resources.GetString("zoomTrackBar.ToolTip"));
            this.zoomTrackBar.Scroll += new System.EventHandler(this.zoomTrackBar_Scroll);
            // 
            // viewsLabel
            // 
            resources.ApplyResources(this.viewsLabel, "viewsLabel");
            this.viewsLabel.Name = "viewsLabel";
            // 
            // viewComboBox
            // 
            this.viewComboBox.FormattingEnabled = true;
            this.viewComboBox.Items.AddRange(new object[] {
            resources.GetString("viewComboBox.Items"),
            resources.GetString("viewComboBox.Items1"),
            resources.GetString("viewComboBox.Items2"),
            resources.GetString("viewComboBox.Items3"),
            resources.GetString("viewComboBox.Items4"),
            resources.GetString("viewComboBox.Items5")});
            resources.ApplyResources(this.viewComboBox, "viewComboBox");
            this.viewComboBox.Name = "viewComboBox";
            this.explanationToolTip.SetToolTip(this.viewComboBox, resources.GetString("viewComboBox.ToolTip"));
            this.viewComboBox.SelectedIndexChanged += new System.EventHandler(this.viewComboBox_SelectedIndexChanged);
            // 
            // displayPanel
            // 
            resources.ApplyResources(this.displayPanel, "displayPanel");
            this.displayPanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.displayPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.displayPanel.Controls.Add(this.displayPictureBox);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.displayPanel_Paint);
            this.displayPanel.Resize += new System.EventHandler(this.displayPanel_Resize);
            // 
            // displayPictureBox
            // 
            resources.ApplyResources(this.displayPictureBox, "displayPictureBox");
            this.displayPictureBox.Name = "displayPictureBox";
            this.displayPictureBox.TabStop = false;
            // 
            // mipLabel
            // 
            resources.ApplyResources(this.mipLabel, "mipLabel");
            this.mipLabel.Name = "mipLabel";
            // 
            // formatLabel
            // 
            resources.ApplyResources(this.formatLabel, "formatLabel");
            this.formatLabel.Name = "formatLabel";
            this.formatLabel.Tag = "Format: {0}";
            this.explanationToolTip.SetToolTip(this.formatLabel, resources.GetString("formatLabel.ToolTip"));
            // 
            // dimensionsLabel
            // 
            resources.ApplyResources(this.dimensionsLabel, "dimensionsLabel");
            this.dimensionsLabel.Name = "dimensionsLabel";
            this.dimensionsLabel.Tag = "Dimensions: {0}x{1}";
            this.explanationToolTip.SetToolTip(this.dimensionsLabel, resources.GetString("dimensionsLabel.ToolTip"));
            // 
            // mipNumericUpDown
            // 
            resources.ApplyResources(this.mipNumericUpDown, "mipNumericUpDown");
            this.mipNumericUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.mipNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.mipNumericUpDown.Name = "mipNumericUpDown";
            this.explanationToolTip.SetToolTip(this.mipNumericUpDown, resources.GetString("mipNumericUpDown.ToolTip"));
            this.mipNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.mipNumericUpDown.ValueChanged += new System.EventHandler(this.mipNumericUpDown_ValueChanged);
            // 
            // reformatButton
            // 
            resources.ApplyResources(this.reformatButton, "reformatButton");
            this.reformatButton.Image = global::Chadsoft.CTools.Image.Properties.Resources.transform_scale;
            this.reformatButton.Name = "reformatButton";
            this.explanationToolTip.SetToolTip(this.reformatButton, resources.GetString("reformatButton.ToolTip"));
            this.reformatButton.UseVisualStyleBackColor = true;
            this.reformatButton.Click += new System.EventHandler(this.reformatButton_Click);
            // 
            // importButton
            // 
            resources.ApplyResources(this.importButton, "importButton");
            this.importButton.Image = global::Chadsoft.CTools.Image.Properties.Resources.folder;
            this.importButton.Name = "importButton";
            this.explanationToolTip.SetToolTip(this.importButton, resources.GetString("importButton.ToolTip"));
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // exportButton
            // 
            resources.ApplyResources(this.exportButton, "exportButton");
            this.exportButton.Image = global::Chadsoft.CTools.Image.Properties.Resources.disk;
            this.exportButton.Name = "exportButton";
            this.explanationToolTip.SetToolTip(this.exportButton, resources.GetString("exportButton.ToolTip"));
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // explanationToolTip
            // 
            this.explanationToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.explanationToolTip.ToolTipTitle = "Help";
            // 
            // zoomValueLabel
            // 
            resources.ApplyResources(this.zoomValueLabel, "zoomValueLabel");
            this.zoomValueLabel.Name = "zoomValueLabel";
            this.zoomValueLabel.Tag = "{0:#0}%";
            this.explanationToolTip.SetToolTip(this.zoomValueLabel, resources.GetString("zoomValueLabel.ToolTip"));
            // 
            // formatProgressBar
            // 
            resources.ApplyResources(this.formatProgressBar, "formatProgressBar");
            this.formatProgressBar.Name = "formatProgressBar";
            this.formatProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.explanationToolTip.SetToolTip(this.formatProgressBar, resources.GetString("formatProgressBar.ToolTip"));
            // 
            // FileOpenFileDialog
            // 
            resources.ApplyResources(this.FileOpenFileDialog, "FileOpenFileDialog");
            // 
            // FileSaveFileDialog
            // 
            resources.ApplyResources(this.FileSaveFileDialog, "FileSaveFileDialog");
            // 
            // formatterBackgroundWorker
            // 
            this.formatterBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.formatterBackgroundWorker_DoWork);
            this.formatterBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.formatterBackgroundWorker_RunWorkerCompleted);
            // 
            // displayVScrollBar
            // 
            resources.ApplyResources(this.displayVScrollBar, "displayVScrollBar");
            this.displayVScrollBar.LargeChange = 1;
            this.displayVScrollBar.Maximum = 0;
            this.displayVScrollBar.Name = "displayVScrollBar";
            this.displayVScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBar_Scroll);
            // 
            // displayHScrollBar
            // 
            resources.ApplyResources(this.displayHScrollBar, "displayHScrollBar");
            this.displayHScrollBar.LargeChange = 1;
            this.displayHScrollBar.Maximum = 0;
            this.displayHScrollBar.Name = "displayHScrollBar";
            this.displayHScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBar_Scroll);
            // 
            // ImageDataEditorControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.displayHScrollBar);
            this.Controls.Add(this.displayVScrollBar);
            this.Controls.Add(this.formatProgressBar);
            this.Controls.Add(this.zoomValueLabel);
            this.Controls.Add(this.reformatButton);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.mipNumericUpDown);
            this.Controls.Add(this.dimensionsLabel);
            this.Controls.Add(this.formatLabel);
            this.Controls.Add(this.displayPanel);
            this.Controls.Add(this.viewComboBox);
            this.Controls.Add(this.mipLabel);
            this.Controls.Add(this.viewsLabel);
            this.Controls.Add(this.zoomTrackBar);
            this.Controls.Add(this.zoomHeaderLabel);
            this.Name = "ImageDataEditorControl";
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBar)).EndInit();
            this.displayPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.displayPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mipNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label zoomHeaderLabel;
        private System.Windows.Forms.TrackBar zoomTrackBar;
        private System.Windows.Forms.Label viewsLabel;
        private System.Windows.Forms.ComboBox viewComboBox;
        private System.Windows.Forms.Panel displayPanel;
        private System.Windows.Forms.Label mipLabel;
        private System.Windows.Forms.PictureBox displayPictureBox;
        private System.Windows.Forms.Label formatLabel;
        private System.Windows.Forms.Label dimensionsLabel;
        private System.Windows.Forms.NumericUpDown mipNumericUpDown;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Button reformatButton;
        private System.Windows.Forms.ToolTip explanationToolTip;
        private System.Windows.Forms.Label zoomValueLabel;
        internal System.Windows.Forms.OpenFileDialog FileOpenFileDialog;
        internal System.Windows.Forms.SaveFileDialog FileSaveFileDialog;
        private System.Windows.Forms.ProgressBar formatProgressBar;
        private System.ComponentModel.BackgroundWorker formatterBackgroundWorker;
        private System.Windows.Forms.VScrollBar displayVScrollBar;
        private System.Windows.Forms.HScrollBar displayHScrollBar;
    }
}
