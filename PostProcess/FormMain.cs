using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using SharpTools;
using System.Threading;

namespace PostProcess
{
    /// <summary>
    /// Main form for inputting settings and making crop selection
    /// </summary>
    public partial class FormMain : Form
    {
        Image currentImage;
        Point selectionTopLeft;
        Point selectionTopRight;
        Point selectionBottomLeft;
        Point selectionBottomRight;
        PictureBox activeSelectionBox;
        bool fieldChangedEventsEnabled = false;
        bool currentlyProcessingImages = false;
        Settings settings;
        Thread processThread;

        /// <summary>
        /// Initialize form components, translations and settings
        /// </summary>
        public FormMain(Settings settings)
        {
            InitializeComponent();
            this.settings = settings;
            this.Text = Program.Title;
            this.Icon = Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);
            comboBoxInputPaging.Items.AddRange(new[] { "single", "double", "double-rtl" });
            comboBoxInputExtension.Items.AddRange(new[] { "jpg", "jpeg", "png", "bmp" });
            comboBoxOutputExtension.Items.AddRange(new[] { "jpg", "jpeg", "png", "bmp" });
            comboBoxImageRotate.Items.AddRange(new[] { "0", "90", "180", "270" });
            FormMain_Resize(this, EventArgs.Empty);
            panelSelection.Select();

            groupBoxInput.Text = Translations.Get("gui_group_input_config");
            labelInputExtension.Text = Translations.Get("gui_lbl_type");            
            labelInputPaging.Text = Translations.Get("gui_lbl_paging");
            labelInputDir.Text = Translations.Get("gui_lbl_folder");
            groupBoxOutput.Text = Translations.Get("gui_group_output_config");
            labelOutputDir.Text = Translations.Get("gui_lbl_folder");
            labelOutputExtension.Text = Translations.Get("gui_lbl_type");
            labelOutputNaming.Text = Translations.Get("gui_lbl_naming");
            groupBoxBasicTransform.Text = Translations.Get("gui_group_basic_transforms");
            labelImageRotate.Text = Translations.Get("gui_lbl_rotate");
            checkBoxReorder.Text = Translations.Get("gui_box_reorder");
            checkBoxBackFlip.Text = Translations.Get("gui_box_backflip");
            groupBoxCrop.Text = Translations.Get("gui_group_crop");
            labelCropBottom.Text = Translations.Get("gui_lbl_bottom");
            labelCropTop.Text = Translations.Get("gui_lbl_top");
            labelCropLeft.Text = Translations.Get("gui_lbl_left");
            labelCropRight.Text = Translations.Get("gui_lbl_right");
            buttonLaunch.Text = Translations.Get("gui_btn_launch");

            toolTip.SetToolTip(groupBoxInput, Translations.Get("gui_tooltip_input"));
            toolTip.SetToolTip(labelInputDir, Translations.Get("gui_tooltip_indir"));
            toolTip.SetToolTip(textBoxInputDir, Translations.Get("gui_tooltip_indir"));
            toolTip.SetToolTip(buttonInputDir, Translations.Get("gui_tooltip_indir"));
            toolTip.SetToolTip(labelInputPaging, Translations.Get("gui_tooltip_inpaging"));
            toolTip.SetToolTip(comboBoxInputPaging, Translations.Get("gui_tooltip_inpaging"));
            toolTip.SetToolTip(labelInputExtension, Translations.Get("gui_tooltip_inext"));
            toolTip.SetToolTip(comboBoxInputExtension, Translations.Get("gui_tooltip_inext"));
            toolTip.SetToolTip(groupBoxOutput, Translations.Get("gui_tooltip_output"));
            toolTip.SetToolTip(labelOutputDir, Translations.Get("gui_tooltip_outdir"));
            toolTip.SetToolTip(textBoxOutputDir, Translations.Get("gui_tooltip_outdir"));
            toolTip.SetToolTip(buttonOutputDir, Translations.Get("gui_tooltip_outdir"));
            toolTip.SetToolTip(labelOutputNaming, Translations.Get("gui_tooltip_basename"));
            toolTip.SetToolTip(textBoxOutputNaming, Translations.Get("gui_tooltip_basename"));
            toolTip.SetToolTip(labelOutputExtension, Translations.Get("gui_tooltip_outext"));
            toolTip.SetToolTip(comboBoxOutputExtension, Translations.Get("gui_tooltip_outext"));
            toolTip.SetToolTip(groupBoxBasicTransform, Translations.Get("gui_tooltip_basic_transforms"));
            toolTip.SetToolTip(labelImageRotate, Translations.Get("gui_tooltip_rotate"));
            toolTip.SetToolTip(comboBoxImageRotate, Translations.Get("gui_tooltip_rotate"));
            toolTip.SetToolTip(checkBoxReorder, Translations.Get("gui_tooltip_reorder"));
            toolTip.SetToolTip(checkBoxBackFlip, Translations.Get("gui_tooltip_backflip"));
            toolTip.SetToolTip(groupBoxCrop, Translations.Get("gui_tooltip_crop"));
            toolTip.SetToolTip(pictureBoxTopLeft, Translations.Get("gui_tooltip_corner_top_left"));
            toolTip.SetToolTip(pictureBoxTopRight, Translations.Get("gui_tooltip_corner_top_right"));
            toolTip.SetToolTip(pictureBoxBottomLeft, Translations.Get("gui_tooltip_corner_bottom_left"));
            toolTip.SetToolTip(pictureBoxBottomRight, Translations.Get("gui_tooltip_corner_bottom_right"));
            toolTip.SetToolTip(labelCropLeft, Translations.Get("gui_tooltip_crop_left"));
            toolTip.SetToolTip(numericCropLeft, Translations.Get("gui_tooltip_crop_left"));
            toolTip.SetToolTip(labelCropRight, Translations.Get("gui_tooltip_crop_right"));
            toolTip.SetToolTip(numericCropRight, Translations.Get("gui_tooltip_crop_right"));
            toolTip.SetToolTip(labelCropTop, Translations.Get("gui_tooltip_crop_top"));
            toolTip.SetToolTip(numericCropTop, Translations.Get("gui_tooltip_crop_top"));
            toolTip.SetToolTip(labelCropBottom, Translations.Get("gui_tooltip_crop_bottom"));
            toolTip.SetToolTip(numericCropBottom, Translations.Get("gui_tooltip_crop_bottom"));
            toolTip.SetToolTip(buttonLaunch, Translations.Get("gui_tooltip_launch"));

            textBoxInputDir.Text = settings.InputDir;
            comboBoxInputPaging.Text = settings.InputPaging;
            textBoxOutputDir.Text = settings.OutputDir;
            comboBoxInputExtension.Text = settings.InputExt;
            comboBoxOutputExtension.Text = settings.OutputExt;
            comboBoxImageRotate.Text = settings.RotateAngle;
            numericCropLeft.Value = settings.CropLeft;
            numericCropRight.Value = settings.CropRight;
            numericCropTop.Value = settings.CropTop;
            numericCropBottom.Value = settings.CropBottom;
            textBoxOutputNaming.Text = settings.OutputNaming;
            checkBoxReorder.Checked = settings.Reorder;
            checkBoxBackFlip.Checked = settings.Backflip;

            fieldChangedEventsEnabled = true;
            ReloadImagePanels();
        }

        /// <summary>
        /// Resize form components according to new window size
        /// </summary>
        private void FormMain_Resize(object sender, EventArgs e)
        {
            panelSelection.Bounds = new Rectangle(0, 0, this.Width - 245, this.Height - 64);
            groupBoxInput.Location = new Point(this.Width - 235, groupBoxInput.Location.Y);
            groupBoxOutput.Location = new Point(this.Width - 235, groupBoxOutput.Location.Y);
            groupBoxBasicTransform.Location = new Point(this.Width - 235, groupBoxBasicTransform.Location.Y);
            groupBoxCrop.Location = new Point(this.Width - 235, groupBoxCrop.Location.Y);
            buttonLaunch.Location = new Point(this.Width - 236, this.Height - 117);
            statusStrip.Width = this.Width - 32;
        }

        /// <summary>
        /// Reload image panels using first image of input directory.
        /// When input settings are invalid, image panels are unloaded.
        /// </summary>
        private void ReloadImagePanels()
        {
            if (Directory.Exists(textBoxInputDir.Text))
            {
                string[] files = Directory.GetFiles(textBoxInputDir.Text, "*." + comboBoxInputExtension.Text);
                if (files.Length > 0)
                {
                    RotateFlipType rotateSetting = RotateFlipType.RotateNoneFlipNone;
                    switch (comboBoxImageRotate.Text)
                    {
                        case "0": rotateSetting = RotateFlipType.RotateNoneFlipNone; break;
                        case "90": rotateSetting = RotateFlipType.Rotate90FlipNone; break;
                        case "180": rotateSetting = RotateFlipType.Rotate180FlipNone; break;
                        case "270": rotateSetting = RotateFlipType.Rotate270FlipNone; break;
                        default: rotateSetting = RotateFlipType.RotateNoneFlipNone; break;
                    }

                    if (currentImage != null)
                        currentImage.Dispose();

                    currentImage = Bitmap.FromFile(files[0]);
                    currentImage.RotateFlip(rotateSetting);
                    pictureBoxSelection.Size = currentImage.Size;
                    pictureBoxTopLeft.Image = new Bitmap(pictureBoxTopLeft.Bounds.Width, pictureBoxTopLeft.Bounds.Height);
                    pictureBoxTopRight.Image = new Bitmap(pictureBoxTopRight.Bounds.Width, pictureBoxTopRight.Bounds.Height);
                    pictureBoxBottomLeft.Image = new Bitmap(pictureBoxBottomLeft.Bounds.Width, pictureBoxBottomLeft.Bounds.Height);
                    pictureBoxBottomRight.Image = new Bitmap(pictureBoxBottomRight.Bounds.Width, pictureBoxBottomRight.Bounds.Height);
                    SetActiveSelectionBox(pictureBoxTopLeft);
                    UpdateSelectionFromCrop();
                    RedrawSelection();
                }
            }
            else
            {
                if (currentImage != null)
                    currentImage.Dispose();
                if (pictureBoxSelection.Image != null)
                    pictureBoxSelection.Image.Dispose();
                pictureBoxSelection.Image = null;
                pictureBoxTopLeft.Image = null;
                pictureBoxTopRight.Image = null;
                pictureBoxBottomLeft.Image = null;
                pictureBoxBottomRight.Image = null;
                pictureBoxSelection.Size = Size.Empty;
                currentImage = null;
            }
        }

        /// <summary>
        /// Redraw manual selection (red) and resulting crop selection (green).
        /// </summary>
        private void RedrawSelection()
        {
            if (currentImage != null)
            {
                Image drawnImage = new Bitmap(currentImage);
                using (Graphics gfx = Graphics.FromImage(drawnImage))
                using (Pen penRed = new Pen(new SolidBrush(Color.Red)))
                using (Pen penLime = new Pen(new SolidBrush(Color.Lime)))
                using (Pen penBlue = new Pen(new SolidBrush(Color.Blue)))
                {
                    gfx.DrawLines(penRed, new[] {
                        selectionTopLeft,
                        selectionTopRight,
                        selectionBottomRight,
                        selectionBottomLeft,
                        selectionTopLeft
                    });
                    gfx.DrawLines(penLime, new[]{
                        new Point((int)numericCropLeft.Value, (int)numericCropTop.Value),
                        new Point(drawnImage.Width - (int)numericCropRight.Value - 1, (int)numericCropTop.Value),
                        new Point(drawnImage.Width - (int)numericCropRight.Value - 1, drawnImage.Height - (int)numericCropBottom.Value - 1),
                        new Point((int)numericCropLeft.Value, drawnImage.Height - (int)numericCropBottom.Value - 1),
                        new Point((int)numericCropLeft.Value, (int)numericCropTop.Value)
                    });
                    if (comboBoxInputPaging.Text.Contains("double"))
                    {
                        gfx.DrawLine(penBlue,
                            new Point(
                                (int)numericCropLeft.Value + ((drawnImage.Width - (int)numericCropRight.Value - 1 - (int)numericCropLeft.Value) / 2),
                                (int)numericCropTop.Value + 1),
                            new Point(
                                (int)numericCropLeft.Value + ((drawnImage.Width - (int)numericCropRight.Value - 1 - (int)numericCropLeft.Value) / 2),
                                drawnImage.Height - (int)numericCropBottom.Value - 2)
                        );
                    }
                }
                Image oldImage = pictureBoxSelection.Image;
                pictureBoxSelection.Image = drawnImage;
                if (oldImage != null)
                    oldImage.Dispose();
            }
        }

        /// <summary>
        /// Change active crop selection corner and highlight associated picture box
        /// </summary>
        private void SetActiveSelectionBox(PictureBox box)
        {
            pictureBoxTopLeft.BorderStyle = BorderStyle.None;
            pictureBoxTopRight.BorderStyle = BorderStyle.None;
            pictureBoxBottomLeft.BorderStyle = BorderStyle.None;
            pictureBoxBottomRight.BorderStyle = BorderStyle.None;

            if (box != null)
                box.BorderStyle = BorderStyle.FixedSingle;

            activeSelectionBox = box;
        }

        /// <summary>
        /// Determine new crop values by calculating the largest rectangular area fitting into selection polygon
        /// </summary>
        private void UpdateCropFromSelection()
        {
            fieldChangedEventsEnabled = false;
            numericCropTop.Value = Math.Max(selectionTopLeft.Y, selectionTopRight.Y);
            numericCropBottom.Value = currentImage.Height - Math.Min(selectionBottomLeft.Y, selectionBottomRight.Y) - 1;
            numericCropLeft.Value = Math.Max(selectionTopLeft.X, selectionBottomLeft.X);
            numericCropRight.Value = currentImage.Width - Math.Min(selectionTopRight.X, selectionBottomRight.X) - 1;
            fieldChangedEventsEnabled = true;
        }

        /// <summary>
        /// Replace selection polygon using the rectangular crop area obtained from crop values and image size
        /// </summary>
        private void UpdateSelectionFromCrop()
        {
            selectionTopLeft = new Point((int)numericCropLeft.Value, (int)numericCropTop.Value);
            selectionTopRight = new Point(currentImage.Width - (int)numericCropRight.Value - 1, (int)numericCropTop.Value);
            selectionBottomLeft = new Point((int)numericCropLeft.Value, currentImage.Height - (int)numericCropBottom.Value - 1);
            selectionBottomRight = new Point(currentImage.Width - (int)numericCropRight.Value - 1, currentImage.Height - (int)numericCropBottom.Value - 1);

            UpdateSelectionPictureBox(pictureBoxTopLeft, selectionTopLeft);
            UpdateSelectionPictureBox(pictureBoxTopRight, selectionTopRight);
            UpdateSelectionPictureBox(pictureBoxBottomLeft, selectionBottomLeft);
            UpdateSelectionPictureBox(pictureBoxBottomRight, selectionBottomRight);
        }

        /// <summary>
        /// Generate preview of selected coordinates
        /// </summary>
        private void UpdateSelectionPictureBox(PictureBox boxToUpdate, Point coords)
        {
            if (boxToUpdate != null && boxToUpdate.Image != null)
            {
                Image img = boxToUpdate.Image;
                using (Graphics gfx = Graphics.FromImage(img))
                using (SolidBrush brush = new SolidBrush(Color.Black))
                using (Pen pen = new Pen(new SolidBrush(Color.Red)))
                {
                    Rectangle srcRegion = new Rectangle(coords.X - (boxToUpdate.Width / 8), coords.Y - (boxToUpdate.Height / 8), boxToUpdate.Width / 4, boxToUpdate.Height / 4);
                    Rectangle dstRegion = new Rectangle(Point.Empty, boxToUpdate.Size);
                    gfx.FillRectangle(brush, dstRegion);
                    gfx.DrawImage(currentImage, dstRegion, srcRegion, GraphicsUnit.Pixel);
                    gfx.DrawLine(pen, new Point((boxToUpdate.Width / 2), 0), new Point((boxToUpdate.Width / 2), boxToUpdate.Height));
                    gfx.DrawLine(pen, new Point(0, (boxToUpdate.Height / 2)), new Point(boxToUpdate.Width, (boxToUpdate.Height / 2)));
                    boxToUpdate.Image = img;
                }
            }
        }

        /// <summary>
        /// Mouse click on image: Update coordinates of active corner and move to next corner
        /// </summary>
        private void pictureBoxSelection_MouseClick(object sender, MouseEventArgs e)
        {
            if (!currentlyProcessingImages && activeSelectionBox != null && currentImage != null)
            {
                if (activeSelectionBox == pictureBoxTopLeft)
                {
                    selectionTopLeft = new Point(e.X, e.Y);
                    SetActiveSelectionBox(pictureBoxTopRight);
                }
                else if (activeSelectionBox == pictureBoxTopRight)
                {
                    selectionTopRight = new Point(e.X, e.Y);
                    SetActiveSelectionBox(pictureBoxBottomRight);
                }
                else if (activeSelectionBox == pictureBoxBottomRight)
                {
                    selectionBottomRight = new Point(e.X, e.Y);
                    SetActiveSelectionBox(pictureBoxBottomLeft);
                }
                else if (activeSelectionBox == pictureBoxBottomLeft)
                {
                    selectionBottomLeft = new Point(e.X, e.Y);
                    SetActiveSelectionBox(pictureBoxTopLeft);
                }
                UpdateCropFromSelection();
                RedrawSelection();
            }
        }

        /// <summary>
        /// Mouse moves on image: Update preview to hovered coordinates
        /// </summary>
        private void pictureBoxSelection_MouseMove(object sender, MouseEventArgs e)
        {
            if (!currentlyProcessingImages)
            {
                statusLabel.Text = String.Format("{0}, {1}", e.X, e.Y);

                if (activeSelectionBox == pictureBoxTopLeft)
                    statusLabel.Text += " - Active Crop: Top-Left";
                else if (activeSelectionBox == pictureBoxTopRight)
                    statusLabel.Text += " - Active Crop: Top-Right";
                else if (activeSelectionBox == pictureBoxBottomRight)
                    statusLabel.Text += " - Active Crop: Bottom-Left";
                else if (activeSelectionBox == pictureBoxBottomLeft)
                    statusLabel.Text += " - Active Crop: Bottom-Right";

                UpdateSelectionPictureBox(activeSelectionBox, new Point(e.X, e.Y));
            }
        }

        /// <summary>
        /// Mouse leaves image: Revert preview to selected coordinates
        /// </summary>
        private void pictureBoxSelection_MouseLeave(object sender, EventArgs e)
        {
            if (!currentlyProcessingImages && activeSelectionBox != null)
            {
                if (activeSelectionBox == pictureBoxTopLeft)
                    UpdateSelectionPictureBox(activeSelectionBox, selectionTopLeft);
                else if (activeSelectionBox == pictureBoxTopRight)
                    UpdateSelectionPictureBox(activeSelectionBox, selectionTopRight);
                else if (activeSelectionBox == pictureBoxBottomRight)
                    UpdateSelectionPictureBox(activeSelectionBox, selectionBottomRight);
                else if (activeSelectionBox == pictureBoxBottomLeft)
                    UpdateSelectionPictureBox(activeSelectionBox, selectionBottomRight);
            }
        }

        /// <summary>
        /// Browse for input directory
        /// </summary>
        private void buttonInputDir_Click(object sender, EventArgs e)
        {
            BrowseFolder(textBoxInputDir);
        }

        /// <summary>
        /// Browse for output directory
        /// </summary>
        private void buttonOutputDir_Click(object sender, EventArgs e)
        {
            BrowseFolder(textBoxOutputDir);
        }

        /// <summary>
        /// Browse for a directory and write resulting path to the provided text box
        /// </summary>
        /// <param name="dirTextBox">Text box to update</param>
        private void BrowseFolder(TextBox dirTextBox)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = dirTextBox.Text;
            if (dlg.ShowDialog() == DialogResult.OK)
                dirTextBox.Text = dlg.SelectedPath;
        }

        /// <summary>
        /// Input or Output directory changed: Truncate if relative to working directory, and reload image panels if applicable
        /// </summary>
        private void textBoxDir_TextChanged(object sender, EventArgs e)
        {
            TextBox textBoxDir = sender as TextBox;
            if (textBoxDir != null)
            {
                string currentDir = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar;
                if (textBoxDir.Text.StartsWith(currentDir))
                    textBoxDir.Text = textBoxDir.Text.Substring(currentDir.Length);
            }
            if (textBoxDir == textBoxInputDir)
            {
                ReloadImagePanels();
            }
        }

        /// <summary>
        /// Crop box clicked: Manual change of active crop box
        /// </summary>
        private void pictureBoxCrop_Click(object sender, EventArgs e)
        {
            if (!currentlyProcessingImages)
            {
                PictureBox senderPictureBox = sender as PictureBox;
                if (senderPictureBox != null)
                    SetActiveSelectionBox(senderPictureBox);
            }
        }

        /// <summary>
        /// Input paging changed: Redraw selection with new settings
        /// </summary>
        private void comboBoxInputPaging_TextChanged(object sender, EventArgs e)
        {
            if (fieldChangedEventsEnabled)
            {
                RedrawSelection();
            }
        }

        /// <summary>
        /// Input file extension changed: Reload input image with new settings
        /// </summary>
        private void comboBoxInputExtension_TextChanged(object sender, EventArgs e)
        {
            if (fieldChangedEventsEnabled)
            {
                ReloadImagePanels();
            }
        }

        /// <summary>
        /// Rotation setting changed: Reload input image with new settings
        /// </summary>
        private void comboBoxImageRotate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fieldChangedEventsEnabled)
            {
                ReloadImagePanels();
            }
        }

        /// <summary>
        /// Crop value changed: Recalculate and redraw selection
        /// </summary>
        private void numericCrop_ValueChanged(object sender, EventArgs e)
        {
            if (fieldChangedEventsEnabled)
            {
                UpdateSelectionFromCrop();
                RedrawSelection();
            }
        }

        /// <summary>
        /// Set/Unset processing mode (thread-safe)
        /// </summary>
        private void SetProcessing(bool processing)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetProcessing(processing)));
            }
            currentlyProcessingImages = processing;

            textBoxInputDir.Enabled = !processing;
            buttonInputDir.Enabled = !processing;
            comboBoxInputPaging.Enabled = !processing;
            comboBoxInputExtension.Enabled = !processing;
            textBoxOutputDir.Enabled = !processing;
            buttonOutputDir.Enabled = !processing;
            textBoxOutputNaming.Enabled = !processing;
            comboBoxOutputExtension.Enabled = !processing;
            comboBoxImageRotate.Enabled = !processing;
            checkBoxReorder.Enabled = !processing;
            checkBoxBackFlip.Enabled = !processing;
            numericCropLeft.Enabled = !processing;
            numericCropRight.Enabled = !processing;
            numericCropTop.Enabled = !processing;
            numericCropBottom.Enabled = !processing;

            buttonLaunch.Text = processing
                ? Translations.Get("gui_btn_cancel")
                : Translations.Get("gui_btn_launch");
        }

        /// <summary>
        /// Update status label (thread-safe)
        /// </summary>
        private void UpdateStatusLabel(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateStatusLabel(text)));
                return;
            }
            statusLabel.Text = text;
        }

        /// <summary>
        /// Show error message (thread-safe)
        /// </summary>
        private void ShowErrorMessage(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowErrorMessage(text)));
                return;
            }
            MessageBox.Show(text, Translations.Get("gui_error_occured"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            UpdateStatusLabel(Translations.Get("gui_error_occured") + " - " + text);
        }

        /// <summary>
        /// Click on Launch button: Write settings to Batch file and launch operation
        /// </summary>
        private void buttonLaunch_Click(object sender, EventArgs e)
        {
            if (currentlyProcessingImages)
            {
                if (processThread != null)
                {
                    processThread.Abort();
                    processThread = null;
                    SetProcessing(false);
                }
            }
            else
            {
                try
                {
                    SetProcessing(true);
                    settings.InputDir = textBoxInputDir.Text;
                    settings.InputPaging = comboBoxInputPaging.Text;
                    settings.OutputDir = textBoxOutputDir.Text;
                    settings.InputExt = comboBoxInputExtension.Text;
                    settings.OutputExt = comboBoxOutputExtension.Text;
                    settings.RotateAngle = comboBoxImageRotate.Text;
                    settings.CropLeft = (uint)numericCropLeft.Value;
                    settings.CropRight = (uint)numericCropRight.Value;
                    settings.CropTop = (uint)numericCropTop.Value;
                    settings.CropBottom = (uint)numericCropBottom.Value;
                    settings.OutputNaming = textBoxOutputNaming.Text.Trim();
                    settings.Reorder = checkBoxReorder.Checked;
                    settings.Backflip = checkBoxBackFlip.Checked;
                    Settings.WriteBatchFile(Program.BatName, settings);

                    processThread = new Thread(() =>
                    {
                        try
                        {
                            if (ImageProcessor.Run(settings, UpdateStatusLabel, ShowErrorMessage))
                            {
                                try
                                {
                                    new System.Media.SoundPlayer("C:\\WINDOWS\\Media\\chimes.wav").Play();
                                }
                                catch { /* Failed to play finish sound */ }
                            }
                        }
                        catch (Exception exception)
                        {
                            if (!(exception is ThreadAbortException))
                            {
                                ShowErrorMessage(exception.GetType() + ": " + exception.Message);
                            }
                        }
                        SetProcessing(false);
                    });
                    processThread.Start();
                }
                catch (Exception exception)
                {
                    ShowErrorMessage(exception.GetType() + ": " + exception.Message);
                    SetProcessing(false);
                }
            }
        }
    }
}
