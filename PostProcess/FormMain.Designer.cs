namespace PostProcess
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            this.pictureBoxSelection = new System.Windows.Forms.PictureBox();
            this.panelSelection = new System.Windows.Forms.Panel();
            this.pictureBoxTopLeft = new System.Windows.Forms.PictureBox();
            this.pictureBoxTopRight = new System.Windows.Forms.PictureBox();
            this.pictureBoxBottomLeft = new System.Windows.Forms.PictureBox();
            this.pictureBoxBottomRight = new System.Windows.Forms.PictureBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.comboBoxInputExtension = new System.Windows.Forms.ComboBox();
            this.labelInputExtension = new System.Windows.Forms.Label();
            this.comboBoxInputPaging = new System.Windows.Forms.ComboBox();
            this.labelInputPaging = new System.Windows.Forms.Label();
            this.labelInputDir = new System.Windows.Forms.Label();
            this.buttonInputDir = new System.Windows.Forms.Button();
            this.textBoxInputDir = new System.Windows.Forms.TextBox();
            this.checkBoxBackFlip = new System.Windows.Forms.CheckBox();
            this.groupBoxOutput = new System.Windows.Forms.GroupBox();
            this.comboBoxOutputExtension = new System.Windows.Forms.ComboBox();
            this.labelOutputExtension = new System.Windows.Forms.Label();
            this.textBoxOutputNaming = new System.Windows.Forms.TextBox();
            this.labelOutputDir = new System.Windows.Forms.Label();
            this.buttonOutputDir = new System.Windows.Forms.Button();
            this.labelOutputNaming = new System.Windows.Forms.Label();
            this.textBoxOutputDir = new System.Windows.Forms.TextBox();
            this.checkBoxReorder = new System.Windows.Forms.CheckBox();
            this.groupBoxBasicTransform = new System.Windows.Forms.GroupBox();
            this.comboBoxImageRotate = new System.Windows.Forms.ComboBox();
            this.labelImageRotate = new System.Windows.Forms.Label();
            this.numericCropBottom = new System.Windows.Forms.NumericUpDown();
            this.labelCropBottom = new System.Windows.Forms.Label();
            this.numericCropTop = new System.Windows.Forms.NumericUpDown();
            this.labelCropTop = new System.Windows.Forms.Label();
            this.numericCropRight = new System.Windows.Forms.NumericUpDown();
            this.labelCropRight = new System.Windows.Forms.Label();
            this.numericCropLeft = new System.Windows.Forms.NumericUpDown();
            this.labelCropLeft = new System.Windows.Forms.Label();
            this.groupBoxCrop = new System.Windows.Forms.GroupBox();
            this.buttonLaunch = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelection)).BeginInit();
            this.panelSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTopLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTopRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBottomLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBottomRight)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.groupBoxInput.SuspendLayout();
            this.groupBoxOutput.SuspendLayout();
            this.groupBoxBasicTransform.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCropBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCropTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCropRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCropLeft)).BeginInit();
            this.groupBoxCrop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxSelection
            // 
            this.pictureBoxSelection.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBoxSelection.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxSelection.Name = "pictureBoxSelection";
            this.pictureBoxSelection.Size = new System.Drawing.Size(100, 50);
            this.pictureBoxSelection.TabIndex = 0;
            this.pictureBoxSelection.TabStop = false;
            this.pictureBoxSelection.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxSelection_MouseClick);
            this.pictureBoxSelection.MouseLeave += new System.EventHandler(this.pictureBoxSelection_MouseLeave);
            this.pictureBoxSelection.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxSelection_MouseMove);
            // 
            // panelSelection
            // 
            this.panelSelection.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelSelection.AutoScroll = true;
            this.panelSelection.Controls.Add(this.pictureBoxSelection);
            this.panelSelection.Location = new System.Drawing.Point(0, 0);
            this.panelSelection.Name = "panelSelection";
            this.panelSelection.Size = new System.Drawing.Size(200, 100);
            this.panelSelection.TabIndex = 0;
            // 
            // pictureBoxTopLeft
            // 
            this.pictureBoxTopLeft.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBoxTopLeft.Location = new System.Drawing.Point(16, 24);
            this.pictureBoxTopLeft.Name = "pictureBoxTopLeft";
            this.pictureBoxTopLeft.Size = new System.Drawing.Size(85, 85);
            this.pictureBoxTopLeft.TabIndex = 2;
            this.pictureBoxTopLeft.TabStop = false;
            this.pictureBoxTopLeft.Click += new System.EventHandler(this.pictureBoxCrop_Click);
            // 
            // pictureBoxTopRight
            // 
            this.pictureBoxTopRight.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBoxTopRight.Location = new System.Drawing.Point(107, 24);
            this.pictureBoxTopRight.Name = "pictureBoxTopRight";
            this.pictureBoxTopRight.Size = new System.Drawing.Size(85, 85);
            this.pictureBoxTopRight.TabIndex = 3;
            this.pictureBoxTopRight.TabStop = false;
            this.pictureBoxTopRight.Click += new System.EventHandler(this.pictureBoxCrop_Click);
            // 
            // pictureBoxBottomLeft
            // 
            this.pictureBoxBottomLeft.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBoxBottomLeft.Location = new System.Drawing.Point(16, 115);
            this.pictureBoxBottomLeft.Name = "pictureBoxBottomLeft";
            this.pictureBoxBottomLeft.Size = new System.Drawing.Size(85, 85);
            this.pictureBoxBottomLeft.TabIndex = 4;
            this.pictureBoxBottomLeft.TabStop = false;
            this.pictureBoxBottomLeft.Click += new System.EventHandler(this.pictureBoxCrop_Click);
            // 
            // pictureBoxBottomRight
            // 
            this.pictureBoxBottomRight.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBoxBottomRight.Location = new System.Drawing.Point(107, 115);
            this.pictureBoxBottomRight.Name = "pictureBoxBottomRight";
            this.pictureBoxBottomRight.Size = new System.Drawing.Size(85, 85);
            this.pictureBoxBottomRight.TabIndex = 5;
            this.pictureBoxBottomRight.TabStop = false;
            this.pictureBoxBottomRight.Click += new System.EventHandler(this.pictureBoxCrop_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.AutoSize = false;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 739);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.Controls.Add(this.comboBoxInputExtension);
            this.groupBoxInput.Controls.Add(this.labelInputExtension);
            this.groupBoxInput.Controls.Add(this.comboBoxInputPaging);
            this.groupBoxInput.Controls.Add(this.labelInputPaging);
            this.groupBoxInput.Controls.Add(this.labelInputDir);
            this.groupBoxInput.Controls.Add(this.buttonInputDir);
            this.groupBoxInput.Controls.Add(this.textBoxInputDir);
            this.groupBoxInput.Location = new System.Drawing.Point(789, 12);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Size = new System.Drawing.Size(207, 105);
            this.groupBoxInput.TabIndex = 1;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "GROUP_INPUT_CONFIG";
            // 
            // comboBoxInputExtension
            // 
            this.comboBoxInputExtension.FormattingEnabled = true;
            this.comboBoxInputExtension.Location = new System.Drawing.Point(54, 73);
            this.comboBoxInputExtension.Name = "comboBoxInputExtension";
            this.comboBoxInputExtension.Size = new System.Drawing.Size(145, 21);
            this.comboBoxInputExtension.TabIndex = 6;
            this.comboBoxInputExtension.SelectedIndexChanged += new System.EventHandler(this.comboBoxInputExtension_TextChanged);
            this.comboBoxInputExtension.TextChanged += new System.EventHandler(this.comboBoxInputExtension_TextChanged);
            // 
            // labelInputExtension
            // 
            this.labelInputExtension.Location = new System.Drawing.Point(3, 76);
            this.labelInputExtension.Name = "labelInputExtension";
            this.labelInputExtension.Size = new System.Drawing.Size(50, 13);
            this.labelInputExtension.TabIndex = 5;
            this.labelInputExtension.Text = "LBL_TYPE";
            this.labelInputExtension.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // comboBoxInputPaging
            // 
            this.comboBoxInputPaging.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInputPaging.FormattingEnabled = true;
            this.comboBoxInputPaging.Location = new System.Drawing.Point(54, 46);
            this.comboBoxInputPaging.Name = "comboBoxInputPaging";
            this.comboBoxInputPaging.Size = new System.Drawing.Size(145, 21);
            this.comboBoxInputPaging.TabIndex = 4;
            this.comboBoxInputPaging.TextChanged += new System.EventHandler(this.comboBoxInputPaging_TextChanged);
            // 
            // labelInputPaging
            // 
            this.labelInputPaging.Location = new System.Drawing.Point(3, 49);
            this.labelInputPaging.Name = "labelInputPaging";
            this.labelInputPaging.Size = new System.Drawing.Size(50, 13);
            this.labelInputPaging.TabIndex = 3;
            this.labelInputPaging.Text = "LBL_PAGING";
            this.labelInputPaging.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelInputDir
            // 
            this.labelInputDir.Location = new System.Drawing.Point(3, 23);
            this.labelInputDir.Name = "labelInputDir";
            this.labelInputDir.Size = new System.Drawing.Size(50, 13);
            this.labelInputDir.TabIndex = 0;
            this.labelInputDir.Text = "LBL_FOLDER";
            this.labelInputDir.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonInputDir
            // 
            this.buttonInputDir.Location = new System.Drawing.Point(174, 19);
            this.buttonInputDir.Name = "buttonInputDir";
            this.buttonInputDir.Size = new System.Drawing.Size(25, 22);
            this.buttonInputDir.TabIndex = 2;
            this.buttonInputDir.Text = "...";
            this.buttonInputDir.UseVisualStyleBackColor = true;
            this.buttonInputDir.Click += new System.EventHandler(this.buttonInputDir_Click);
            // 
            // textBoxInputDir
            // 
            this.textBoxInputDir.Location = new System.Drawing.Point(54, 20);
            this.textBoxInputDir.Name = "textBoxInputDir";
            this.textBoxInputDir.Size = new System.Drawing.Size(117, 20);
            this.textBoxInputDir.TabIndex = 1;
            this.textBoxInputDir.TextChanged += new System.EventHandler(this.textBoxDir_TextChanged);
            // 
            // checkBoxBackFlip
            // 
            this.checkBoxBackFlip.AutoSize = true;
            this.checkBoxBackFlip.Location = new System.Drawing.Point(17, 71);
            this.checkBoxBackFlip.Name = "checkBoxBackFlip";
            this.checkBoxBackFlip.Size = new System.Drawing.Size(104, 17);
            this.checkBoxBackFlip.TabIndex = 3;
            this.checkBoxBackFlip.Text = "BOX_BACKFLIP";
            this.checkBoxBackFlip.UseVisualStyleBackColor = true;
            // 
            // groupBoxOutput
            // 
            this.groupBoxOutput.Controls.Add(this.comboBoxOutputExtension);
            this.groupBoxOutput.Controls.Add(this.labelOutputExtension);
            this.groupBoxOutput.Controls.Add(this.textBoxOutputNaming);
            this.groupBoxOutput.Controls.Add(this.labelOutputDir);
            this.groupBoxOutput.Controls.Add(this.buttonOutputDir);
            this.groupBoxOutput.Controls.Add(this.labelOutputNaming);
            this.groupBoxOutput.Controls.Add(this.textBoxOutputDir);
            this.groupBoxOutput.Location = new System.Drawing.Point(789, 123);
            this.groupBoxOutput.Name = "groupBoxOutput";
            this.groupBoxOutput.Size = new System.Drawing.Size(207, 105);
            this.groupBoxOutput.TabIndex = 2;
            this.groupBoxOutput.TabStop = false;
            this.groupBoxOutput.Text = "GROUP_OUTPUT_CONFIG";
            // 
            // comboBoxOutputExtension
            // 
            this.comboBoxOutputExtension.FormattingEnabled = true;
            this.comboBoxOutputExtension.Location = new System.Drawing.Point(54, 73);
            this.comboBoxOutputExtension.Name = "comboBoxOutputExtension";
            this.comboBoxOutputExtension.Size = new System.Drawing.Size(145, 21);
            this.comboBoxOutputExtension.TabIndex = 6;
            // 
            // labelOutputExtension
            // 
            this.labelOutputExtension.Location = new System.Drawing.Point(3, 76);
            this.labelOutputExtension.Name = "labelOutputExtension";
            this.labelOutputExtension.Size = new System.Drawing.Size(50, 13);
            this.labelOutputExtension.TabIndex = 5;
            this.labelOutputExtension.Text = "LBL_TYPE";
            this.labelOutputExtension.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxOutputNaming
            // 
            this.textBoxOutputNaming.Location = new System.Drawing.Point(54, 46);
            this.textBoxOutputNaming.Name = "textBoxOutputNaming";
            this.textBoxOutputNaming.Size = new System.Drawing.Size(145, 20);
            this.textBoxOutputNaming.TabIndex = 4;
            // 
            // labelOutputDir
            // 
            this.labelOutputDir.Location = new System.Drawing.Point(3, 23);
            this.labelOutputDir.Name = "labelOutputDir";
            this.labelOutputDir.Size = new System.Drawing.Size(50, 13);
            this.labelOutputDir.TabIndex = 0;
            this.labelOutputDir.Text = "LBL_FOLDER";
            this.labelOutputDir.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonOutputDir
            // 
            this.buttonOutputDir.Location = new System.Drawing.Point(174, 19);
            this.buttonOutputDir.Name = "buttonOutputDir";
            this.buttonOutputDir.Size = new System.Drawing.Size(25, 22);
            this.buttonOutputDir.TabIndex = 2;
            this.buttonOutputDir.Text = "...";
            this.buttonOutputDir.UseVisualStyleBackColor = true;
            this.buttonOutputDir.Click += new System.EventHandler(this.buttonOutputDir_Click);
            // 
            // labelOutputNaming
            // 
            this.labelOutputNaming.Location = new System.Drawing.Point(3, 49);
            this.labelOutputNaming.Name = "labelOutputNaming";
            this.labelOutputNaming.Size = new System.Drawing.Size(50, 13);
            this.labelOutputNaming.TabIndex = 3;
            this.labelOutputNaming.Text = "LBL_NAMING";
            this.labelOutputNaming.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxOutputDir
            // 
            this.textBoxOutputDir.Location = new System.Drawing.Point(54, 20);
            this.textBoxOutputDir.Name = "textBoxOutputDir";
            this.textBoxOutputDir.Size = new System.Drawing.Size(117, 20);
            this.textBoxOutputDir.TabIndex = 1;
            this.textBoxOutputDir.TextChanged += new System.EventHandler(this.textBoxDir_TextChanged);
            // 
            // checkBoxReorder
            // 
            this.checkBoxReorder.AutoSize = true;
            this.checkBoxReorder.Location = new System.Drawing.Point(17, 48);
            this.checkBoxReorder.Name = "checkBoxReorder";
            this.checkBoxReorder.Size = new System.Drawing.Size(108, 17);
            this.checkBoxReorder.TabIndex = 2;
            this.checkBoxReorder.Text = "BOX_REORDER";
            this.checkBoxReorder.UseVisualStyleBackColor = true;
            // 
            // groupBoxBasicTransform
            // 
            this.groupBoxBasicTransform.Controls.Add(this.comboBoxImageRotate);
            this.groupBoxBasicTransform.Controls.Add(this.labelImageRotate);
            this.groupBoxBasicTransform.Controls.Add(this.checkBoxBackFlip);
            this.groupBoxBasicTransform.Controls.Add(this.checkBoxReorder);
            this.groupBoxBasicTransform.Location = new System.Drawing.Point(789, 234);
            this.groupBoxBasicTransform.Name = "groupBoxBasicTransform";
            this.groupBoxBasicTransform.Size = new System.Drawing.Size(207, 100);
            this.groupBoxBasicTransform.TabIndex = 3;
            this.groupBoxBasicTransform.TabStop = false;
            this.groupBoxBasicTransform.Text = "GROUP_BASIC_TRANSFORMS";
            // 
            // comboBoxImageRotate
            // 
            this.comboBoxImageRotate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxImageRotate.FormattingEnabled = true;
            this.comboBoxImageRotate.Location = new System.Drawing.Point(54, 19);
            this.comboBoxImageRotate.Name = "comboBoxImageRotate";
            this.comboBoxImageRotate.Size = new System.Drawing.Size(145, 21);
            this.comboBoxImageRotate.TabIndex = 1;
            this.comboBoxImageRotate.SelectedIndexChanged += new System.EventHandler(this.comboBoxImageRotate_SelectedIndexChanged);
            // 
            // labelImageRotate
            // 
            this.labelImageRotate.Location = new System.Drawing.Point(3, 22);
            this.labelImageRotate.Name = "labelImageRotate";
            this.labelImageRotate.Size = new System.Drawing.Size(50, 13);
            this.labelImageRotate.TabIndex = 0;
            this.labelImageRotate.Text = "LBL_ROTATE";
            this.labelImageRotate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // numericCropBottom
            // 
            this.numericCropBottom.Location = new System.Drawing.Point(76, 296);
            this.numericCropBottom.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericCropBottom.Name = "numericCropBottom";
            this.numericCropBottom.Size = new System.Drawing.Size(106, 20);
            this.numericCropBottom.TabIndex = 7;
            this.numericCropBottom.ValueChanged += new System.EventHandler(this.numericCrop_ValueChanged);
            // 
            // labelCropBottom
            // 
            this.labelCropBottom.Location = new System.Drawing.Point(4, 298);
            this.labelCropBottom.Name = "labelCropBottom";
            this.labelCropBottom.Size = new System.Drawing.Size(66, 13);
            this.labelCropBottom.TabIndex = 6;
            this.labelCropBottom.Text = "LBL_BOTTOM";
            this.labelCropBottom.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // numericCropTop
            // 
            this.numericCropTop.Location = new System.Drawing.Point(76, 270);
            this.numericCropTop.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericCropTop.Name = "numericCropTop";
            this.numericCropTop.Size = new System.Drawing.Size(106, 20);
            this.numericCropTop.TabIndex = 5;
            this.numericCropTop.ValueChanged += new System.EventHandler(this.numericCrop_ValueChanged);
            // 
            // labelCropTop
            // 
            this.labelCropTop.Location = new System.Drawing.Point(4, 272);
            this.labelCropTop.Name = "labelCropTop";
            this.labelCropTop.Size = new System.Drawing.Size(66, 13);
            this.labelCropTop.TabIndex = 4;
            this.labelCropTop.Text = "LBL_TOP";
            this.labelCropTop.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // numericCropRight
            // 
            this.numericCropRight.Location = new System.Drawing.Point(76, 244);
            this.numericCropRight.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericCropRight.Name = "numericCropRight";
            this.numericCropRight.Size = new System.Drawing.Size(106, 20);
            this.numericCropRight.TabIndex = 3;
            this.numericCropRight.ValueChanged += new System.EventHandler(this.numericCrop_ValueChanged);
            // 
            // labelCropRight
            // 
            this.labelCropRight.Location = new System.Drawing.Point(4, 246);
            this.labelCropRight.Name = "labelCropRight";
            this.labelCropRight.Size = new System.Drawing.Size(66, 13);
            this.labelCropRight.TabIndex = 2;
            this.labelCropRight.Text = "LBL_RIGHT";
            this.labelCropRight.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // numericCropLeft
            // 
            this.numericCropLeft.Location = new System.Drawing.Point(76, 218);
            this.numericCropLeft.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericCropLeft.Name = "numericCropLeft";
            this.numericCropLeft.Size = new System.Drawing.Size(106, 20);
            this.numericCropLeft.TabIndex = 1;
            this.numericCropLeft.ValueChanged += new System.EventHandler(this.numericCrop_ValueChanged);
            // 
            // labelCropLeft
            // 
            this.labelCropLeft.Location = new System.Drawing.Point(4, 220);
            this.labelCropLeft.Name = "labelCropLeft";
            this.labelCropLeft.Size = new System.Drawing.Size(66, 13);
            this.labelCropLeft.TabIndex = 0;
            this.labelCropLeft.Text = "LBL_LEFT";
            this.labelCropLeft.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBoxCrop
            // 
            this.groupBoxCrop.Controls.Add(this.numericCropBottom);
            this.groupBoxCrop.Controls.Add(this.pictureBoxTopLeft);
            this.groupBoxCrop.Controls.Add(this.labelCropBottom);
            this.groupBoxCrop.Controls.Add(this.pictureBoxBottomLeft);
            this.groupBoxCrop.Controls.Add(this.numericCropTop);
            this.groupBoxCrop.Controls.Add(this.pictureBoxBottomRight);
            this.groupBoxCrop.Controls.Add(this.labelCropTop);
            this.groupBoxCrop.Controls.Add(this.pictureBoxTopRight);
            this.groupBoxCrop.Controls.Add(this.numericCropRight);
            this.groupBoxCrop.Controls.Add(this.labelCropLeft);
            this.groupBoxCrop.Controls.Add(this.labelCropRight);
            this.groupBoxCrop.Controls.Add(this.numericCropLeft);
            this.groupBoxCrop.Location = new System.Drawing.Point(789, 340);
            this.groupBoxCrop.Name = "groupBoxCrop";
            this.groupBoxCrop.Size = new System.Drawing.Size(207, 335);
            this.groupBoxCrop.TabIndex = 4;
            this.groupBoxCrop.TabStop = false;
            this.groupBoxCrop.Text = "GROUP_CROP";
            // 
            // buttonLaunch
            // 
            this.buttonLaunch.Location = new System.Drawing.Point(789, 683);
            this.buttonLaunch.Name = "buttonLaunch";
            this.buttonLaunch.Size = new System.Drawing.Size(209, 45);
            this.buttonLaunch.TabIndex = 7;
            this.buttonLaunch.Text = "BTN_LAUNCH";
            this.buttonLaunch.UseVisualStyleBackColor = true;
            this.buttonLaunch.Click += new System.EventHandler(this.buttonLaunch_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 761);
            this.Controls.Add(this.buttonLaunch);
            this.Controls.Add(this.groupBoxCrop);
            this.Controls.Add(this.groupBoxBasicTransform);
            this.Controls.Add(this.groupBoxOutput);
            this.Controls.Add(this.groupBoxInput);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panelSelection);
            this.MinimumSize = new System.Drawing.Size(1024, 800);
            this.Name = "FormMain";
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelection)).EndInit();
            this.panelSelection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTopLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTopRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBottomLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBottomRight)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            this.groupBoxOutput.ResumeLayout(false);
            this.groupBoxOutput.PerformLayout();
            this.groupBoxBasicTransform.ResumeLayout(false);
            this.groupBoxBasicTransform.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCropBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCropTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCropRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCropLeft)).EndInit();
            this.groupBoxCrop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxSelection;
        private System.Windows.Forms.Panel panelSelection;
        private System.Windows.Forms.PictureBox pictureBoxTopLeft;
        private System.Windows.Forms.PictureBox pictureBoxTopRight;
        private System.Windows.Forms.PictureBox pictureBoxBottomLeft;
        private System.Windows.Forms.PictureBox pictureBoxBottomRight;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.Button buttonInputDir;
        private System.Windows.Forms.TextBox textBoxInputDir;
        private System.Windows.Forms.Label labelInputDir;
        private System.Windows.Forms.ComboBox comboBoxInputPaging;
        private System.Windows.Forms.Label labelInputPaging;
        private System.Windows.Forms.ComboBox comboBoxInputExtension;
        private System.Windows.Forms.Label labelInputExtension;
        private System.Windows.Forms.CheckBox checkBoxBackFlip;
        private System.Windows.Forms.GroupBox groupBoxOutput;
        private System.Windows.Forms.CheckBox checkBoxReorder;
        private System.Windows.Forms.ComboBox comboBoxOutputExtension;
        private System.Windows.Forms.Label labelOutputExtension;
        private System.Windows.Forms.TextBox textBoxOutputNaming;
        private System.Windows.Forms.Label labelOutputNaming;
        private System.Windows.Forms.Label labelOutputDir;
        private System.Windows.Forms.Button buttonOutputDir;
        private System.Windows.Forms.TextBox textBoxOutputDir;
        private System.Windows.Forms.GroupBox groupBoxBasicTransform;
        private System.Windows.Forms.ComboBox comboBoxImageRotate;
        private System.Windows.Forms.Label labelImageRotate;
        private System.Windows.Forms.NumericUpDown numericCropLeft;
        private System.Windows.Forms.Label labelCropLeft;
        private System.Windows.Forms.NumericUpDown numericCropBottom;
        private System.Windows.Forms.Label labelCropBottom;
        private System.Windows.Forms.NumericUpDown numericCropTop;
        private System.Windows.Forms.Label labelCropTop;
        private System.Windows.Forms.NumericUpDown numericCropRight;
        private System.Windows.Forms.Label labelCropRight;
        private System.Windows.Forms.GroupBox groupBoxCrop;
        private System.Windows.Forms.Button buttonLaunch;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

