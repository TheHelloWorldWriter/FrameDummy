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
    #region Constructor

    /// <summary>
    /// Initializes a new instance of the SettingsForm class
    /// </summary>
    public SettingsForm()
    {
        // Set the form's font to the default operating system font (Segoe UI on Vista)
        Font = SystemFonts.MessageBoxFont;

        // Required method for designer support
        InitializeComponent();

        // Add frame border styles
        foreach (FormBorderStyle style in Enum.GetValues<FormBorderStyle>())
        {
            borderComboBox.Items.Add(style.ToString());
        }

        borderComboBox.SelectedIndex = 4;

        // Add frame border styles
        foreach (PictureBoxSizeMode sizeMode in Enum.GetValues<PictureBoxSizeMode>())
        {
            if (sizeMode != PictureBoxSizeMode.AutoSize)
            {
                imageSizingComboBox.Items.Add(sizeMode.ToString());
            }
        }

        imageSizingComboBox.SelectedIndex = 3;

        titleTextBox.Text = AppStrings.DefaultTitle;
        versionLabel.Text = string.Format(CultureInfo.CurrentCulture, versionLabel.Text, Application.ProductVersion);
        string year = Math.Max(DateTime.Today.Year, 2015).ToString();
        copyrightLabel.Text = string.Format(CultureInfo.CurrentCulture, copyrightLabel.Text, year);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Updates the path of the image file.
    /// </summary>
    /// <param name="filePath">The image file name.</param>
    public void UpdateImageFilePath(string filePath)
    {
        imageTextBox.Text = filePath;
    }

    /// <summary>
    /// Loads the layout from the default configuration file.
    /// </summary>
    public void LoadLayout()
    {
        VerySimpleIni iniFile = new(AppSettings.IniFileName, Application.ExecutablePath, Application.CompanyName ?? string.Empty, Application.ProductName ?? string.Empty, false);

        if (iniFile.Load())
        {
            titleTextBox.Text = iniFile.GetValue(titleTextBox.Name, titleTextBox.Text);
            DoLoadIcon(iniFile.GetValue(iconTextBox.Name));
            FromString.IfInt(iniFile.GetValue(borderComboBox.Name), value => { borderComboBox.SelectedIndex = value; }, null);
            FromString.IfInt(iniFile.GetValue(opacityTrackBar.Name), value => { opacityTrackBar.Value = value; }, null);

            FromString.IfBool(iniFile.GetValue(controlCheckBox.Name), value => { controlCheckBox.Checked = value; }, null);
            FromString.IfBool(iniFile.GetValue(iconCheckBox.Name), value => { iconCheckBox.Checked = value; }, null);
            FromString.IfBool(iniFile.GetValue(minimizeCheckBox.Name), value => { minimizeCheckBox.Checked = value; }, null);
            FromString.IfBool(iniFile.GetValue(maximizeCheckBox.Name), value => { maximizeCheckBox.Checked = value; }, null);
            FromString.IfBool(iniFile.GetValue(taskbarCheckBox.Name), value => { taskbarCheckBox.Checked = value; }, null);
            FromString.IfBool(iniFile.GetValue(topmostCheckBox.Name), value => { topmostCheckBox.Checked = value; }, null);

            commandTextBox.Text = iniFile.GetValue(commandTextBox.Name, string.Empty);

            DoLoadImage(iniFile.GetValue(imageTextBox.Name));
            FromString.IfInt(iniFile.GetValue(imageSizingComboBox.Name), value => { imageSizingComboBox.SelectedIndex = value; }, null);
            FromString.IfHtmlColor(iniFile.GetValue(colorValueLabel.Name), value => { colorValueLabel.BackColor = value; }, null);
            FromString.IfBool(iniFile.GetValue(colorTransparentCheckBox.Name), value => { colorTransparentCheckBox.Checked = value; }, null);

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
        VerySimpleIni iniFile = new(AppSettings.IniFileName, Application.ExecutablePath, Application.CompanyName ?? string.Empty, Application.ProductName ?? string.Empty, true);

        if (iniFile.IsReady)
        {
            iniFile.SetValue(titleTextBox.Name, titleTextBox.Text);
            iniFile.SetValue(iconTextBox.Name, iconTextBox.Text);
            iniFile.SetValue(borderComboBox.Name, borderComboBox.SelectedIndex);
            iniFile.SetValue(opacityTrackBar.Name, opacityTrackBar.Value);

            iniFile.SetValue(controlCheckBox.Name, controlCheckBox.Checked);
            iniFile.SetValue(iconCheckBox.Name, iconCheckBox.Checked);
            iniFile.SetValue(minimizeCheckBox.Name, minimizeCheckBox.Checked);
            iniFile.SetValue(maximizeCheckBox.Name, maximizeCheckBox.Checked);
            iniFile.SetValue(taskbarCheckBox.Name, taskbarCheckBox.Checked);
            iniFile.SetValue(topmostCheckBox.Name, topmostCheckBox.Checked);

            iniFile.SetValue(commandTextBox.Name, commandTextBox.Text);

            iniFile.SetValue(imageTextBox.Name, imageTextBox.Text);
            iniFile.SetValue(imageSizingComboBox.Name, imageSizingComboBox.SelectedIndex);
            iniFile.SetValue(colorValueLabel.Name, ColorTranslator.ToHtml(colorValueLabel.BackColor));
            iniFile.SetValue(colorTransparentCheckBox.Name, colorTransparentCheckBox.Checked);

            Rectangle bounds = MainForm.TheMainForm.WindowState == FormWindowState.Normal ? MainForm.TheMainForm.Bounds : MainForm.TheMainForm.RestoreBounds;
            iniFile.SetValue(MainForm.TheMainForm.Name, new RectangleConverter().ConvertToInvariantString(bounds) ?? string.Empty);
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
        colorValueLabel.Height = colorBrowseButton.Height;
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
            Hide();
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
            Hide();
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
        MainForm.TheMainForm.Text = titleTextBox.Text;
    }

    /// <summary>
    /// Event -> Icon Browse Button -> Click
    /// Opens a File Dialog and allows the user to select a new frame icon.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventIconBrowseButtonClick(object sender, EventArgs e)
    {
        openIconDialog.InitialDirectory = Path.GetDirectoryName(iconTextBox.Text);
        if (openIconDialog.ShowDialog(this) == DialogResult.OK)
        {
            DoLoadIcon(openIconDialog.FileName);
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
        iconTextBox.Text = AppStrings.DefaultIcon;
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
        MainForm.TheMainForm.FormBorderStyle = Enum.Parse<FormBorderStyle>(borderComboBox.SelectedItem!.ToString()!);
    }

    /// <summary>
    /// Event -> Opacity Track Bar - Value Changed
    /// Updates the opacity of the main frame form in real-time.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventOpacityTrackBarValueChanged(object sender, EventArgs e)
    {
        MainForm.TheMainForm.Opacity = (double)opacityTrackBar.Value / 100;
        opacityLabel.Text = string.Format(CultureInfo.CurrentCulture, "Opacity:\r\n{0:0%}", MainForm.TheMainForm.Opacity);
    }

    /// <summary>
    /// Event -> Frame Check Boxes - Checked Changed
    /// Updates frame options in real-time.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventFrameCheckBoxesCheckedChanged(object sender, EventArgs e)
    {
        if (sender == controlCheckBox)
        {
            MainForm.TheMainForm.ControlBox = controlCheckBox.Checked;
        }
        else if (sender == iconCheckBox)
        {
            MainForm.TheMainForm.ShowIcon = iconCheckBox.Checked;
        }
        else if (sender == minimizeCheckBox)
        {
            MainForm.TheMainForm.MinimizeBox = minimizeCheckBox.Checked;
        }
        else if (sender == maximizeCheckBox)
        {
            MainForm.TheMainForm.MaximizeBox = maximizeCheckBox.Checked;
        }
        else if (sender == taskbarCheckBox)
        {
            MainForm.TheMainForm.ShowInTaskbar = taskbarCheckBox.Checked;
        }
        else if (sender == topmostCheckBox)
        {
            MainForm.TheMainForm.TopMost = topmostCheckBox.Checked;
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
        MainForm.TheMainForm.Cursor = string.IsNullOrEmpty(commandTextBox.Text) ? Cursors.Default : Cursors.Hand;
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
        openImageDialog.InitialDirectory = Path.GetDirectoryName(imageTextBox.Text);
        if (openImageDialog.ShowDialog(this) == DialogResult.OK)
        {
            DoLoadImage(openImageDialog.FileName);
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
        imageTextBox.Text = AppStrings.NoImage;
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
        MainForm.TheMainForm.SetSizeMode(Enum.Parse<PictureBoxSizeMode>(imageSizingComboBox.SelectedItem!.ToString()!));
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
        MainForm.TheMainForm.SetColor(colorValueLabel.BackColor, colorTransparentCheckBox.Checked);
    }

    /// <summary>
    /// Event -> Color Random Button -> Click
    /// Sets the main form picture box background color to a new random color.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventColorRandomButtonClick(object sender, EventArgs e)
    {
        var random = Random.Shared;
        colorValueLabel.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
    }

    /// <summary>
    /// Event -> Color Browse Button -> Click
    /// Opens a Color Dialog Box and allows the user to select a new color.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventColorBrowseButtonClick(object sender, EventArgs e)
    {
        colorDialog.Color = colorValueLabel.BackColor;
        if (colorDialog.ShowDialog(this) == DialogResult.OK)
        {
            colorValueLabel.BackColor = colorDialog.Color;
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
        MainForm.TheMainForm.TransparencyKey = colorTransparentCheckBox.Checked ? colorValueLabel.BackColor : Color.Empty;
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
        Process.Start(new ProcessStartInfo(urlLinkLabel.Text) { UseShellExecute = true });
    }

    #endregion

    #region Icon and Image Settings Functionality

    /// <summary>
    /// Loads a new icon.
    /// </summary>
    /// <param name="iconFilePath">The icon file path.</param>
    private void DoLoadIcon(string iconFilePath)
    {
        if (!(string.IsNullOrEmpty(iconFilePath) || iconFilePath.Equals(AppStrings.DefaultIcon)))
        {
            if (MainForm.TheMainForm.LoadIcon(iconFilePath))
            {
                iconTextBox.Text = iconFilePath;
            }
        }
    }

    /// <summary>
    /// Loads a new image.
    /// </summary>
    /// <param name="imageFilePath">The image file path.</param>
    private void DoLoadImage(string imageFilePath)
    {
        if (!(string.IsNullOrEmpty(imageFilePath) || imageFilePath.Equals(AppStrings.NoImage) || imageFilePath.Equals(AppStrings.PastedImage)))
        {
            if (MainForm.TheMainForm.LoadImage(imageFilePath))
            {
                imageTextBox.Text = imageFilePath;
            }
        }
    }

    #endregion
}

