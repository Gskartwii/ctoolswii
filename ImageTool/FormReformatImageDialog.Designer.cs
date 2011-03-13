namespace Chadsoft.CTools.Image
{
    partial class FormReformatImageDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReformatImageDialog));
            this.formatLabel = new System.Windows.Forms.Label();
            this.formatComboBox = new System.Windows.Forms.ComboBox();
            this.widthLabel = new System.Windows.Forms.Label();
            this.widthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.heightLabel = new System.Windows.Forms.Label();
            this.heightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.mipMapLevelsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.mipLevelsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mipMapLevelsNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // formatLabel
            // 
            this.formatLabel.AutoSize = true;
            this.formatLabel.Location = new System.Drawing.Point(13, 13);
            this.formatLabel.Name = "formatLabel";
            this.formatLabel.Size = new System.Drawing.Size(45, 13);
            this.formatLabel.TabIndex = 0;
            this.formatLabel.Text = "Format: ";
            // 
            // formatComboBox
            // 
            this.formatComboBox.FormattingEnabled = true;
            this.formatComboBox.Location = new System.Drawing.Point(75, 10);
            this.formatComboBox.Name = "formatComboBox";
            this.formatComboBox.Size = new System.Drawing.Size(123, 21);
            this.formatComboBox.TabIndex = 1;
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(13, 39);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(38, 13);
            this.widthLabel.TabIndex = 2;
            this.widthLabel.Text = "Width:";
            // 
            // widthNumericUpDown
            // 
            this.widthNumericUpDown.Location = new System.Drawing.Point(75, 37);
            this.widthNumericUpDown.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.widthNumericUpDown.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.widthNumericUpDown.Name = "widthNumericUpDown";
            this.widthNumericUpDown.Size = new System.Drawing.Size(123, 20);
            this.widthNumericUpDown.TabIndex = 3;
            this.widthNumericUpDown.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.widthNumericUpDown.ValueChanged += new System.EventHandler(this.widthHeightNumericUpDown_ValueChanged);
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(13, 65);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(41, 13);
            this.heightLabel.TabIndex = 2;
            this.heightLabel.Text = "Height:";
            // 
            // heightNumericUpDown
            // 
            this.heightNumericUpDown.Location = new System.Drawing.Point(75, 63);
            this.heightNumericUpDown.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.heightNumericUpDown.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.heightNumericUpDown.Name = "heightNumericUpDown";
            this.heightNumericUpDown.Size = new System.Drawing.Size(123, 20);
            this.heightNumericUpDown.TabIndex = 3;
            this.heightNumericUpDown.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.heightNumericUpDown.ValueChanged += new System.EventHandler(this.widthHeightNumericUpDown_ValueChanged);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(123, 112);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(42, 112);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // mipMapLevelsNumericUpDown
            // 
            this.mipMapLevelsNumericUpDown.Location = new System.Drawing.Point(75, 89);
            this.mipMapLevelsNumericUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.mipMapLevelsNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.mipMapLevelsNumericUpDown.Name = "mipMapLevelsNumericUpDown";
            this.mipMapLevelsNumericUpDown.Size = new System.Drawing.Size(123, 20);
            this.mipMapLevelsNumericUpDown.TabIndex = 6;
            this.mipMapLevelsNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // mipLevelsLabel
            // 
            this.mipLevelsLabel.AutoSize = true;
            this.mipLevelsLabel.Location = new System.Drawing.Point(13, 91);
            this.mipLevelsLabel.Name = "mipLevelsLabel";
            this.mipLevelsLabel.Size = new System.Drawing.Size(56, 13);
            this.mipLevelsLabel.TabIndex = 5;
            this.mipLevelsLabel.Text = "Mip Maps:";
            // 
            // FormReformatImageDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 141);
            this.Controls.Add(this.mipMapLevelsNumericUpDown);
            this.Controls.Add(this.mipLevelsLabel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.heightNumericUpDown);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.widthNumericUpDown);
            this.Controls.Add(this.widthLabel);
            this.Controls.Add(this.formatComboBox);
            this.Controls.Add(this.formatLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormReformatImageDialog";
            this.Text = "Reformat Image";
            ((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mipMapLevelsNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label formatLabel;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        public System.Windows.Forms.ComboBox formatComboBox;
        public System.Windows.Forms.NumericUpDown widthNumericUpDown;
        public System.Windows.Forms.NumericUpDown heightNumericUpDown;
        public System.Windows.Forms.NumericUpDown mipMapLevelsNumericUpDown;
        private System.Windows.Forms.Label mipLevelsLabel;
    }
}