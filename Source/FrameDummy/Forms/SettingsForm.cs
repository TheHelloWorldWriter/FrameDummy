// Copyright (c) 2013-2026 The Hello World Writer (https://www.thehelloworldwriter.com).
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Globalization;

namespace FrameDummy;

/// <summary>
/// The settings form.
/// </summary>
public partial class SettingsForm : Form
{
    #region Fields

    /// <summary>
    /// A random number generator used to create random colors
    /// </summary>
    private Random random;

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the SettingsForm class
    /// </summary>
    public SettingsForm()
    {
        // Set the form's font to the default operating system font (Segoe UI on Vista)
        this.Font = SystemFonts.MessageBoxFont;

        // Required method for designer support
        this.InitializeComponent();

        // Initialize the random number generator used to create random colors
        this.random = new Random();

        // Add frame border styles
        foreach (FormBorderStyle style in Enum.GetValues(typeof(FormBorderStyle)))
        {
            this.borderComboBox.Items.Add(style.ToString());
        }

        this.borderComboBox.SelectedIndex = 4;

        // Add frame border styles
        foreach (PictureBoxSizeMode sizeMode in Enum.GetValues(typeof(PictureBoxSizeMode)))
        {
            if (sizeMode != PictureBoxSizeMode.AutoSize)
            {
                this.imageSizingComboBox.Items.Add(sizeMode.ToString());
            }
        }

        this.imageSizingComboBox.SelectedIndex = 3;

        this.titleTextBox.Text = Strings.DefaultTitle;
        this.versionLabel.Text = string.Format(CultureInfo.CurrentCulture, this.versionLabel.Text, Application.ProductVersion);
        string year = Math.Max(DateTime.Today.Year, 2015).ToString();
        this.copyrightLabel.Text = string.Format(CultureInfo.CurrentCulture, this.copyrightLabel.Text, year);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Updates the path of the image file.
    /// </summary>
    /// <param name="filePath">The image file name.</param>
    public void UpdateImageFilePath(string filePath)
    {
        this.imageTextBox.Text = filePath;
    }

    /// <summary>
    /// Loads the layout from the default configuration file.
    /// </summary>
    public void LoadLayout()
    {
        VerySimpleIni iniFile = new VerySimpleIni(AppSettings.IniFileName, Application.ExecutablePath, Application.CompanyName, Application.ProductName, false);

        if (iniFile.Load())
        {
            this.titleTextBox.Text = iniFile.GetValue(this.titleTextBox.Name, this.titleTextBox.Text);
            this.DoLoadIcon(iniFile.GetValue(this.iconTextBox.Name));
            FromString.IfInt(iniFile.GetValue(this.borderComboBox.Name), value => { this.borderComboBox.SelectedIndex = value; }, null);
            FromString.IfInt(iniFile.GetValue(this.opacityTrackBar.Name), value => { this.opacityTrackBar.Value = value; }, null);

            FromString.IfBool(iniFile.GetValue(this.controlCheckBox.Name), value => { this.controlCheckBox.Checked = value; }, null);
            FromString.IfBool(iniFile.GetValue(this.iconCheckBox.Name), value => { this.iconCheckBox.Checked = value; }, null);
            FromString.IfBool(iniFile.GetValue(this.minimizeCheckBox.Name), value => { this.minimizeCheckBox.Checked = value; }, null);
            FromString.IfBool(iniFile.GetValue(this.maximizeCheckBox.Name), value => { this.maximizeCheckBox.Checked = value; }, null);
            FromString.IfBool(iniFile.GetValue(this.taskbarCheckBox.Name), value => { this.taskbarCheckBox.Checked = value; }, null);
            FromString.IfBool(iniFile.GetValue(this.topmostCheckBox.Name), value => { this.topmostCheckBox.Checked = value; }, null);

            this.commandTextBox.Text = iniFile.GetValue(this.commandTextBox.Name, string.Empty);

            this.DoLoadImage(iniFile.GetValue(this.imageTextBox.Name));
            FromString.IfInt(iniFile.GetValue(this.imageSizingComboBox.Name), value => { this.imageSizingComboBox.SelectedIndex = value; }, null);
            FromString.IfHtmlColor(iniFile.GetValue(this.colorValueLabel.Name), value => { this.colorValueLabel.BackColor = value; }, null);
            FromString.IfBool(iniFile.GetValue(this.colorTransparentCheckBox.Name), value => { this.colorTransparentCheckBox.Checked = value; }, null);

            FromString.IfRectangle(iniFile.GetValue(MainForm.TheMainForm.Name), value => { MainForm.TheMainForm.Bounds = value; }, null);
            FromString.IfBool(
                iniFile.GetValue(AppSettings.MaximizedKey),
                value =>
                {
                    if (value)
                    {
                        MainForm.TheMainForm.WindowState = FormWindowState.Maximized;
                    }
                },
                null);
        }
    }

    /// <summary>
    /// Saves the layout to the default configuration file.
    /// </summary>
    public void SaveLayout()
    {
        VerySimpleIni iniFile = new VerySimpleIni(AppSettings.IniFileName, Application.ExecutablePath, Application.CompanyName, Application.ProductName, true);

        if (iniFile.IsReady)
        {
            iniFile.SetValue(this.titleTextBox.Name, this.titleTextBox.Text);
            iniFile.SetValue(this.iconTextBox.Name, this.iconTextBox.Text);
            iniFile.SetValue(this.borderComboBox.Name, this.borderComboBox.SelectedIndex);
            iniFile.SetValue(this.opacityTrackBar.Name, this.opacityTrackBar.Value);

            iniFile.SetValue(this.controlCheckBox.Name, this.controlCheckBox.Checked);
            iniFile.SetValue(this.iconCheckBox.Name, this.iconCheckBox.Checked);
            iniFile.SetValue(this.minimizeCheckBox.Name, this.minimizeCheckBox.Checked);
            iniFile.SetValue(this.maximizeCheckBox.Name, this.maximizeCheckBox.Checked);
            iniFile.SetValue(this.taskbarCheckBox.Name, this.taskbarCheckBox.Checked);
            iniFile.SetValue(this.topmostCheckBox.Name, this.topmostCheckBox.Checked);

            iniFile.SetValue(this.commandTextBox.Name, this.commandTextBox.Text);

            iniFile.SetValue(this.imageTextBox.Name, this.imageTextBox.Text);
            iniFile.SetValue(this.imageSizingComboBox.Name, this.imageSizingComboBox.SelectedIndex);
            iniFile.SetValue(this.colorValueLabel.Name, ColorTranslator.ToHtml(this.colorValueLabel.BackColor));
            iniFile.SetValue(this.colorTransparentCheckBox.Name, this.colorTransparentCheckBox.Checked);

            Rectangle bounds = MainForm.TheMainForm.WindowState == FormWindowState.Normal ? MainForm.TheMainForm.Bounds : MainForm.TheMainForm.RestoreBounds;
            iniFile.SetValue(MainForm.TheMainForm.Name, new RectangleConverter().ConvertToInvariantString(bounds));
            iniFile.SetValue(AppSettings.MaximizedKey, MainForm.TheMainForm.WindowState == FormWindowState.Maximized);

            try
            {
                iniFile.Save();
            }
            catch
            {
                // Ignore configuration save errors
            }
        }
    }

    #endregion

    #region Events - Form

    /// <summary>
    /// Event -> Form - Form Shown
    /// Initializes control values and sets the position of the form, when the settings form is first shown.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Keyboard event data.</param>
    private void EventSettingsFormShown(object sender, EventArgs e)
    {
        /*Screen screen = Screen.FromControl(MainForm.TheMainForm);
        this.Location = new Point(
            Math.Min(screen.WorkingArea.Width - this.Width, MainForm.TheMainForm.Right + 4),
            Math.Min(screen.WorkingArea.Height - this.Height, MainForm.TheMainForm.Top));*/
        this.colorValueLabel.Height = this.colorBrowseButton.Height;
    }

    /// <summary>
    /// Event -> Form - Form Key Down
    /// Hides the form when the user presses the Escape key.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Keyboard event data.</param>
    private void EventSettingsFormKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            this.Hide();
            e.SuppressKeyPress = true;
        }
    }

    /// <summary>
    /// Event -> Form - Form Closing
    /// Hides the form instead of closing it.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Form closing event data.</param>
    private void EventSettingsFormFormClosing(object sender, FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing)
        {
            this.Hide();
            e.Cancel = true;
        }
    }

    #endregion

    #region Events - Frame Settings

    /// <summary>
    /// Event -> Title Text Box - Text Changed
    /// Updates the frame title text in real-time.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventTitleTextBoxTextChanged(object sender, EventArgs e)
    {
        MainForm.TheMainForm.Text = this.titleTextBox.Text;
    }

    /// <summary>
    /// Event -> Icon Browse Button -> Click
    /// Opens a File Dialog and allows the user to select a new frame icon.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventIconBrowseButtonClick(object sender, EventArgs e)
    {
        this.openIconDialog.InitialDirectory = Path.GetDirectoryName(this.iconTextBox.Text);
        if (this.openIconDialog.ShowDialog(this) == DialogResult.OK)
        {
            this.DoLoadIcon(this.openIconDialog.FileName);
        }
    }

    /// <summary>
    /// Event -> Icon Default Button -> Click
    /// Restores the default frame icon.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventIconDefaultButtonClick(object sender, EventArgs e)
    {
        this.iconTextBox.Text = Strings.DefaultIcon;
        MainForm.TheMainForm.RestoreIcon();
    }

    /// <summary>
    /// Event -> Border Combo Box - Selected Index Changed
    /// Updates the frame style in real-time.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventBorderComboBoxSelectedIndexChanged(object sender, EventArgs e)
    {
        MainForm.TheMainForm.FormBorderStyle = (FormBorderStyle)Enum.Parse(typeof(FormBorderStyle), this.borderComboBox.SelectedItem.ToString());
    }

    /// <summary>
    /// Event -> Opacity Track Bar - Value Changed
    /// Updates the opacity of the main frame form in real-time.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventOpacityTrackBarValueChanged(object sender, EventArgs e)
    {
        MainForm.TheMainForm.Opacity = (double)this.opacityTrackBar.Value / 100;
        this.opacityLabel.Text = string.Format(CultureInfo.CurrentCulture, "Opacity:\r\n{0:0%}", MainForm.TheMainForm.Opacity);
    }

    /// <summary>
    /// Event -> Frame Check Boxes - Checked Changed
    /// Updates frame options in real-time.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventFrameCheckBoxesCheckedChanged(object sender, EventArgs e)
    {
        if (sender == this.controlCheckBox)
        {
            MainForm.TheMainForm.ControlBox = this.controlCheckBox.Checked;
        }
        else if (sender == this.iconCheckBox)
        {
            MainForm.TheMainForm.ShowIcon = this.iconCheckBox.Checked;
        }
        else if (sender == this.minimizeCheckBox)
        {
            MainForm.TheMainForm.MinimizeBox = this.minimizeCheckBox.Checked;
        }
        else if (sender == this.maximizeCheckBox)
        {
            MainForm.TheMainForm.MaximizeBox = this.maximizeCheckBox.Checked;
        }
        else if (sender == this.taskbarCheckBox)
        {
            MainForm.TheMainForm.ShowInTaskbar = this.taskbarCheckBox.Checked;
        }
        else if (sender == this.topmostCheckBox)
        {
            MainForm.TheMainForm.TopMost = this.topmostCheckBox.Checked;
        }
    }

    /// <summary>
    /// Event -> Command Text Box -> Text Changed
    /// Updates the dummy form command.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventCommandTextBoxTextChanged(object sender, EventArgs e)
    {
        MainForm.TheMainForm.Cursor = string.IsNullOrEmpty(this.commandTextBox.Text) ? Cursors.Default : Cursors.Hand;
    }

    #endregion

    #region Events - Transparent or Image Mode Settings

    /// <summary>
    /// Event -> Image Browse Button -> Click
    /// Opens a File Dialog and allows the user to select the image.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventImageBrowseButtonClick(object sender, EventArgs e)
    {
        this.openImageDialog.InitialDirectory = Path.GetDirectoryName(this.imageTextBox.Text);
        if (this.openImageDialog.ShowDialog(this) == DialogResult.OK)
        {
            this.DoLoadImage(this.openImageDialog.FileName);
        }
    }

    /// <summary>
    /// Event -> Image Clear Button -> Click
    /// Clears the current image.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventImageClearButtonClick(object sender, EventArgs e)
    {
        MainForm.TheMainForm.SetImage(null);
        this.imageTextBox.Text = Strings.NoImage;
        GC.Collect();
    }

    /// <summary>
    /// Event -> Image Size Mode Combo Box -> SelectedIndexChanged
    /// Sets a new picture size mode to the main form image.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventImageSizingComboBoxSelectedIndexChanged(object sender, EventArgs e)
    {
        MainForm.TheMainForm.SetSizeMode((PictureBoxSizeMode)Enum.Parse(typeof(PictureBoxSizeMode), this.imageSizingComboBox.SelectedItem.ToString()));
    }

    /// <summary>
    /// Event -> Auto Size button -> Click
    /// Auto sizes the frame form based on the size of the image.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventAutoSizeButtonClick(object sender, EventArgs e)
    {
        MainForm.TheMainForm.DoAutoSize();
    }

    /// <summary>
    /// Event -> Color Value Label -> BackColor Changed
    /// Updates the background color of the main frame form when the background color is changed in Settings.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventColorValueLabelBackColorChanged(object sender, EventArgs e)
    {
        MainForm.TheMainForm.SetColor(this.colorValueLabel.BackColor, this.colorTransparentCheckBox.Checked);
    }

    /// <summary>
    /// Event -> Color Random Button -> Click
    /// Sets the main form picture box background color to a new random color.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventColorRandomButtonClick(object sender, EventArgs e)
    {
        this.colorValueLabel.BackColor = Color.FromArgb(this.random.Next(256), this.random.Next(256), this.random.Next(256));
    }

    /// <summary>
    /// Event -> Color Browse Button -> Click
    /// Opens a Color Dialog Box and allows the user to select a new color.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventColorBrowseButtonClick(object sender, EventArgs e)
    {
        this.colorDialog.Color = this.colorValueLabel.BackColor;
        if (this.colorDialog.ShowDialog(this) == DialogResult.OK)
        {
            this.colorValueLabel.BackColor = this.colorDialog.Color;
        }
    }

    /// <summary>
    /// Event -> Color Transparent Check Box -> Check Changed
    /// Enables or disables the color transparency of the main frame form.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventColorTransparentCheckBoxCheckedChanged(object sender, EventArgs e)
    {
        MainForm.TheMainForm.TransparencyKey = this.colorTransparentCheckBox.Checked ? this.colorValueLabel.BackColor : Color.Empty;
    }

    #endregion

    #region Events - About

    /// <summary>
    /// Event -> Url Link Label -> Link Clicked
    /// Open the developer home page.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventUrlLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        Process.Start(this.urlLinkLabel.Text);
    }

    #endregion

    #region Icon and Image Settings Functionality

    /// <summary>
    /// Loads a new icon.
    /// </summary>
    /// <param name="iconFilePath">The icon file path.</param>
    private void DoLoadIcon(string iconFilePath)
    {
        if (!(string.IsNullOrEmpty(iconFilePath) || iconFilePath.Equals(Strings.DefaultIcon)))
        {
            if (MainForm.TheMainForm.LoadIcon(iconFilePath))
            {
                this.iconTextBox.Text = iconFilePath;
            }
        }
    }

    /// <summary>
    /// Loads a new image.
    /// </summary>
    /// <param name="imageFilePath">The image file path.</param>
    private void DoLoadImage(string imageFilePath)
    {
        if (!(string.IsNullOrEmpty(imageFilePath) || imageFilePath.Equals(Strings.NoImage) || imageFilePath.Equals(Strings.PastedImage)))
        {
            if (MainForm.TheMainForm.LoadImage(imageFilePath))
            {
                this.imageTextBox.Text = imageFilePath;
            }
        }
    }

    #endregion
}

