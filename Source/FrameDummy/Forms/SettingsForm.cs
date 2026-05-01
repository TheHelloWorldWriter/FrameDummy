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
        this.Font = SystemFonts.MessageBoxFont;

        // Required method for designer support
        this.InitializeComponent();

        // Add frame border styles
        foreach (FormBorderStyle style in Enum.GetValues<FormBorderStyle>())
        {
            _borderComboBox.Items.Add(style.ToString());
        }

        _borderComboBox.SelectedIndex = 4;

        // Add frame border styles
        foreach (PictureBoxSizeMode sizeMode in Enum.GetValues<PictureBoxSizeMode>())
        {
            if (sizeMode != PictureBoxSizeMode.AutoSize)
            {
                _imageSizingComboBox.Items.Add(sizeMode.ToString());
            }
        }

        _imageSizingComboBox.SelectedIndex = 3;

        _titleTextBox.Text = Strings.DefaultTitle;
        _aboutVersionLabel.Text = string.Format(CultureInfo.CurrentCulture, _aboutVersionLabel.Text, Application.ProductVersion);
        string year = Math.Max(DateTime.Today.Year, 2015).ToString();
        _aboutCopyrightLabel.Text = string.Format(CultureInfo.CurrentCulture, _aboutCopyrightLabel.Text, year);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Updates the path of the image file.
    /// </summary>
    /// <param name="filePath">The image file name.</param>
    public void UpdateImageFilePath(string filePath)
    {
        _imageTextBox.Text = filePath;
    }

    /// <summary>
    /// Loads the layout from the default configuration file.
    /// </summary>
    public void LoadLayout()
    {
        Settings s = SettingsStore.Load();

        _titleTextBox.Text = s.Title;
        DoLoadIcon(s.IconPath ?? string.Empty);
        _borderComboBox.SelectedIndex = (int)s.Border;
        _opacityTrackBar.Value = s.Opacity;

        _controlBoxCheck.Checked = s.ControlBox;
        _showIconCheck.Checked = s.ShowIcon;
        _minimizeBoxCheck.Checked = s.MinimizeBox;
        _maximizeBoxCheck.Checked = s.MaximizeBox;
        _showInTaskbarCheck.Checked = s.ShowInTaskbar;
        _topmostCheck.Checked = s.TopMost;

        _commandTextBox.Text = s.PrankCommand;

        DoLoadImage(s.ImagePath ?? string.Empty);
        // PictureBoxSizeMode.AutoSize (=3) is excluded from the combo, so enum values past it shift down by one.
        int sizingIdx = (int)s.ImageSizing;
        if (s.ImageSizing > PictureBoxSizeMode.AutoSize) sizingIdx -= 1;
        _imageSizingComboBox.SelectedIndex = sizingIdx;
        _colorValueLabel.BackColor = ColorTranslator.FromHtml(s.Color);
        _colorTransparentCheck.Checked = s.ColorTransparent;

        if (s.Bounds is { } b) MainForm.TheMainForm.Bounds = new Rectangle(b.X, b.Y, b.Width, b.Height);
        if (s.Maximized) MainForm.TheMainForm.WindowState = FormWindowState.Maximized;
    }

    /// <summary>
    /// Saves the layout to the default configuration file.
    /// </summary>
    public void SaveLayout()
    {
        // PictureBoxSizeMode.AutoSize (=3) is excluded from the combo, so combo indices at/past it shift up by one to recover the enum value.
        int sizingIdx = _imageSizingComboBox.SelectedIndex;
        PictureBoxSizeMode imageSizing = sizingIdx >= (int)PictureBoxSizeMode.AutoSize
            ? (PictureBoxSizeMode)(sizingIdx + 1)
            : (PictureBoxSizeMode)sizingIdx;

        Rectangle bounds = MainForm.TheMainForm.WindowState == FormWindowState.Normal
            ? MainForm.TheMainForm.Bounds
            : MainForm.TheMainForm.RestoreBounds;

        Settings settings = new()
        {
            Title = _titleTextBox.Text,
            IconPath = _iconTextBox.Text,
            Border = (FormBorderStyle)_borderComboBox.SelectedIndex,
            Opacity = _opacityTrackBar.Value,
            ControlBox = _controlBoxCheck.Checked,
            ShowIcon = _showIconCheck.Checked,
            MinimizeBox = _minimizeBoxCheck.Checked,
            MaximizeBox = _maximizeBoxCheck.Checked,
            ShowInTaskbar = _showInTaskbarCheck.Checked,
            TopMost = _topmostCheck.Checked,
            PrankCommand = _commandTextBox.Text,
            ImagePath = _imageTextBox.Text,
            ImageSizing = imageSizing,
            Color = ColorTranslator.ToHtml(_colorValueLabel.BackColor),
            ColorTransparent = _colorTransparentCheck.Checked,
            Bounds = new WindowBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height),
            Maximized = MainForm.TheMainForm.WindowState == FormWindowState.Maximized,
        };

        SettingsStore.Save(settings);
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
        _colorValueLabel.Height = _colorBrowseButton.Height;
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
    private void OnTitleChanged(object? sender, EventArgs e)
    {
        MainForm.TheMainForm.Text = _titleTextBox.Text;
    }

    /// <summary>
    /// Event -> Icon Browse Button -> Click
    /// Opens a File Dialog and allows the user to select a new frame icon.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void OnIconBrowseClicked(object? sender, EventArgs e)
    {
        _openIconDialog.InitialDirectory = Path.GetDirectoryName(_iconTextBox.Text);
        if (_openIconDialog.ShowDialog(this) == DialogResult.OK)
        {
            this.DoLoadIcon(_openIconDialog.FileName);
        }
    }

    /// <summary>
    /// Event -> Icon Default Button -> Click
    /// Restores the default frame icon.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void OnIconDefaultClicked(object? sender, EventArgs e)
    {
        _iconTextBox.Text = Strings.DefaultIcon;
        MainForm.TheMainForm.RestoreIcon();
    }

    /// <summary>
    /// Event -> Border Combo Box - Selected Index Changed
    /// Updates the frame style in real-time.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void OnBorderChanged(object? sender, EventArgs e)
    {
        MainForm.TheMainForm.FormBorderStyle = Enum.Parse<FormBorderStyle>(_borderComboBox.SelectedItem.ToString());
    }

    /// <summary>
    /// Event -> Opacity Track Bar - Value Changed
    /// Updates the opacity of the main frame form in real-time.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void OnOpacityChanged(object? sender, EventArgs e)
    {
        MainForm.TheMainForm.Opacity = (double)_opacityTrackBar.Value / 100;
        _opacityLabel.Text = string.Format(CultureInfo.CurrentCulture, "Opacity:\r\n{0:0%}", MainForm.TheMainForm.Opacity);
    }

    /// <summary>
    /// Event -> Frame Check Boxes - Checked Changed
    /// Updates frame options in real-time.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void OnFrameCheckChanged(object? sender, EventArgs e)
    {
        if (sender == _controlBoxCheck)
        {
            MainForm.TheMainForm.ControlBox = _controlBoxCheck.Checked;
        }
        else if (sender == _showIconCheck)
        {
            MainForm.TheMainForm.ShowIcon = _showIconCheck.Checked;
        }
        else if (sender == _minimizeBoxCheck)
        {
            MainForm.TheMainForm.MinimizeBox = _minimizeBoxCheck.Checked;
        }
        else if (sender == _maximizeBoxCheck)
        {
            MainForm.TheMainForm.MaximizeBox = _maximizeBoxCheck.Checked;
        }
        else if (sender == _showInTaskbarCheck)
        {
            MainForm.TheMainForm.ShowInTaskbar = _showInTaskbarCheck.Checked;
        }
        else if (sender == _topmostCheck)
        {
            MainForm.TheMainForm.TopMost = _topmostCheck.Checked;
        }
    }

    /// <summary>
    /// Event -> Command Text Box -> Text Changed
    /// Updates the dummy form command.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void OnCommandChanged(object? sender, EventArgs e)
    {
        MainForm.TheMainForm.Cursor = string.IsNullOrEmpty(_commandTextBox.Text) ? Cursors.Default : Cursors.Hand;
    }

    #endregion

    #region Events - Transparent or Image Mode Settings

    /// <summary>
    /// Event -> Image Browse Button -> Click
    /// Opens a File Dialog and allows the user to select the image.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void OnImageBrowseClicked(object? sender, EventArgs e)
    {
        _openImageDialog.InitialDirectory = Path.GetDirectoryName(_imageTextBox.Text);
        if (_openImageDialog.ShowDialog(this) == DialogResult.OK)
        {
            this.DoLoadImage(_openImageDialog.FileName);
        }
    }

    /// <summary>
    /// Event -> Image Clear Button -> Click
    /// Clears the current image.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void OnImageClearClicked(object? sender, EventArgs e)
    {
        MainForm.TheMainForm.SetImage(null);
        _imageTextBox.Text = Strings.NoImage;
        GC.Collect();
    }

    /// <summary>
    /// Event -> Image Size Mode Combo Box -> SelectedIndexChanged
    /// Sets a new picture size mode to the main form image.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void OnImageSizingChanged(object? sender, EventArgs e)
    {
        MainForm.TheMainForm.SetSizeMode(Enum.Parse<PictureBoxSizeMode>(_imageSizingComboBox.SelectedItem.ToString()));
    }

    /// <summary>
    /// Event -> Auto Size button -> Click
    /// Auto sizes the frame form based on the size of the image.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void OnAutoSizeClicked(object? sender, EventArgs e)
    {
        MainForm.TheMainForm.DoAutoSize();
    }

    /// <summary>
    /// Event -> Color Value Label -> BackColor Changed
    /// Updates the background color of the main frame form when the background color is changed in Settings.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void OnColorValueChanged(object? sender, EventArgs e)
    {
        MainForm.TheMainForm.SetColor(_colorValueLabel.BackColor, _colorTransparentCheck.Checked);
    }

    /// <summary>
    /// Event -> Color Random Button -> Click
    /// Sets the main form picture box background color to a new random color.
    /// </summary>
    private void OnColorRandomClicked(object? sender, EventArgs e)
    {
        _colorValueLabel.BackColor = Utils.RandomColor();
    }

    /// <summary>
    /// Event -> Color Browse Button -> Click
    /// Opens a Color Dialog Box and allows the user to select a new color.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void OnColorBrowseClicked(object? sender, EventArgs e)
    {
        _colorDialog.Color = _colorValueLabel.BackColor;
        if (_colorDialog.ShowDialog(this) == DialogResult.OK)
        {
            _colorValueLabel.BackColor = _colorDialog.Color;
        }
    }

    /// <summary>
    /// Event -> Color Transparent Check Box -> Check Changed
    /// Enables or disables the color transparency of the main frame form.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void OnColorTransparentChanged(object? sender, EventArgs e)
    {
        MainForm.TheMainForm.TransparencyKey = _colorTransparentCheck.Checked ? _colorValueLabel.BackColor : Color.Empty;
    }

    #endregion

    #region Events - About

    /// <summary>
    /// Event -> Url Link Label -> Link Clicked
    /// Open the developer home page.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void OnAboutUrlClicked(object? sender, LinkLabelLinkClickedEventArgs e)
    {
        Process.Start(_aboutUrlLink.Text);
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
                _iconTextBox.Text = iconFilePath;
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
                _imageTextBox.Text = imageFilePath;
            }
        }
    }

    #endregion
}

