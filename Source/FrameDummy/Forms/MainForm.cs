// Copyright (c) 2013-2026 The Hello World Writer (https://www.thehelloworldwriter.com).
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace FrameDummy;

/// <summary>
/// The main frame form.
/// </summary>
public partial class MainForm : Form
{
    #region Fields

    /// <summary>
    /// The default frame icon.
    /// </summary>
    private Icon defaultIcon;

    /// <summary>
    /// The settings form.
    /// </summary>
    private SettingsForm settingsForm;

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the MainForm class
    /// </summary>
    public MainForm()
    {
        // Set the form's font to the default operating system font (Segoe UI on Vista)
        Font = SystemFonts.MessageBoxFont;

        // Required method for designer support
        InitializeComponent();

        MainForm.TheMainForm = this;
        Text = Strings.DefaultTitle;
        settingsForm = new SettingsForm();
        defaultIcon = Icon;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the main form.
    /// </summary>
    internal static MainForm TheMainForm { get; private set; }

    #endregion

    #region Public Methods

    /// <summary>
    /// Loads a new icon.
    /// </summary>
    /// <param name="iconFilePath">The icon file path.</param>
    /// <returns>True on success, false otherwise.</returns>
    public bool LoadIcon(string iconFilePath)
    {
        try
        {
            if (Path.GetExtension(iconFilePath).Equals(".ICO", StringComparison.CurrentCultureIgnoreCase))
            {
                MainForm.TheMainForm.Icon = new Icon(iconFilePath);
            }
            else
            {
                Bitmap bitmap = (Bitmap)Image.FromFile(iconFilePath);
                Icon newIcon = Icon.FromHandle(bitmap.GetHicon());
                MainForm.TheMainForm.Icon = newIcon;
                NativeMethods.DestroyIcon(newIcon.Handle);
            }

            return true;
        }
        catch (ArgumentException)
        {
            MessageBox.Show(
                this,
                string.Format(CultureInfo.CurrentCulture, Strings.IconLoadErrorFormat, iconFilePath),
                Application.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        return false;
    }

    /// <summary>
    /// Restores the default frame icon.
    /// </summary>
    public void RestoreIcon()
    {
        Icon = defaultIcon;
    }

    /// <summary>
    /// Sets the background color.
    /// </summary>
    /// <param name="color">A color value.</param>
    /// <param name="transparent">True if the color is transparent, false otherwise.</param>
    public void SetColor(Color color, bool transparent)
    {
        BackColor = pictureBox.BackColor = color;
        if (transparent)
        {
            MainForm.TheMainForm.TransparencyKey = color;
        }
    }

    /// <summary>
    /// Sets a new image.
    /// </summary>
    /// <param name="image">The new background image.</param>
    public void SetImage(Image image)
    {
        pictureBox.Image = image;
        GC.Collect();
    }

    /// <summary>
    /// Loads an image from a file, and sets it as the background image.
    /// </summary>
    /// <param name="imageFile">The file that contains the new background image.</param>
    /// <returns>True if the image was loaded successfully, false otherwise.</returns>
    public bool LoadImage(string imageFile)
    {
        try
        {
            Image image = Image.FromFile(imageFile);
            SetImage(image);
            return true;
        }
        catch (OutOfMemoryException)
        {
            MessageBox.Show(
                this,
                string.Format(CultureInfo.CurrentCulture, Strings.ImageLoadErrorFormat, imageFile),
                Application.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (FileNotFoundException)
        {
            MessageBox.Show(
                this,
                string.Format(CultureInfo.CurrentCulture, Strings.ImageNotFoundErrorFormat, imageFile),
                Application.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        return false;
    }

    /// <summary>
    /// Sets a new size mode for the picture box.
    /// </summary>
    /// <param name="sizeMode">A SizeMode value.</param>
    public void SetSizeMode(PictureBoxSizeMode sizeMode)
    {
        pictureBox.SizeMode = sizeMode;
    }

    /// <summary>
    /// Auto sizes the frame form based on the size of the image.
    /// </summary>
    public void DoAutoSize()
    {
        ClientSize = pictureBox.PreferredSize;
    }

    #endregion

    #region Form Events - Key Down, Mouse Click and Drag and Drop Functionality

    /// <summary>
    /// Event -> Main Form -> Key Down
    /// Pastes an image when the user presses Ctrl+V.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Keyboard event data.</param>
    private void EventMainFormKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control)
        {
            switch (e.KeyCode)
            {
                case Keys.S:
                    if (!settingsForm.prankNoSettingsHotkeyCheckBox.Checked)
                    {
                        ToggleSettings();
                    }

                    break;
                case Keys.V:
                    PasteImage();
                    break;
                case Keys.A:
                    DoAutoSize();
                    break;
            }
        }
    }

    /// <summary>
    /// Event -> Main Form and Picture Box -> Mouse Click
    /// Shows or hides the Settings form when the user right-clicks the mouse.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Mouse event data.</param>
    private void EventMainFormMouseClick(object sender, MouseEventArgs e)
    {
        switch (e.Button)
        {
            case MouseButtons.Left:
                string command = settingsForm.commandTextBox.Text;
                if (!string.IsNullOrEmpty(command))
                {
                    try
                    {
                        Process.Start(command);
                    }
                    catch (Exception ex)
                    {
                        string message = string.Format(
                            CultureInfo.CurrentCulture,
                            Strings.CommandErrorFormat,
                            command,
                            ex.Message);
                        MessageBox.Show(
                            this,
                            message,
                            Application.ProductName,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }

                break;
            case MouseButtons.Right:
                if (!settingsForm.prankNoSettingsRightClickCheckBox.Checked)
                {
                    ToggleSettings();
                }

                break;
        }
    }

    /// <summary>
    /// Informs the system that the form accepts drag and drop operations with files or folders.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Drag and drop event data.</param>
    private void EventMainFormDragEnter(object sender, DragEventArgs e)
    {
        e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None;
    }

    /// <summary>
    /// Gets the image file that was dragged and dropped on the form and loads the image.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Drag and drop event data.</param>
    private void EventMainFormDragDrop(object sender, DragEventArgs e)
    {
        string[] fileItems = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        if (fileItems.Length > 0)
        {
            if (LoadImage(fileItems[0]))
            {
                settingsForm.UpdateImageFilePath(fileItems[0]);
            }
        }
    }

    /// <summary>
    /// Event -> Main Form -> Form Shown
    /// Loads the layout when the main form is first shown.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Empty event data.</param>
    private void EventMainFormShown(object sender, EventArgs e)
    {
        settingsForm.LoadLayout();
    }

    /// <summary>
    /// Event -> Main Form -> Form Closing
    /// Does not allow the main form to close if the prank setting is on.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Form closing event data.</param>
    private void EventMainFormFormClosing(object sender, FormClosingEventArgs e)
    {
        if (settingsForm.prankNoCloseCheckBox.Checked)
        {
            e.Cancel = true;
        }
    }

    /// <summary>
    /// Event -> Main Form -> Form Closed
    /// Saves the layout when the main form is closed.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Form closed event data.</param>
    private void EventMainFormFormClosed(object sender, FormClosedEventArgs e)
    {
        settingsForm.SaveLayout();
    }

    #endregion

    #region Extra Functionality

    /// <summary>
    /// Shows or hides the Settings form.
    /// </summary>
    private void ToggleSettings()
    {
        if (!settingsForm.Visible)
        {
            settingsForm.Show(this);
        }
        else
        {
            settingsForm.Hide();
        }
    }

    /// <summary>
    /// Switches to Image Mode, and sets the image to the image or image file available in the clipboard.
    /// </summary>
    private void PasteImage()
    {
        if (Clipboard.ContainsImage())
        {
            SetImage(Clipboard.GetImage());
            settingsForm.UpdateImageFilePath(Strings.PastedImage);
        }
        else if (Clipboard.ContainsFileDropList())
        {
            string imageFile = Clipboard.GetFileDropList()[0];
            if (LoadImage(imageFile))
            {
                settingsForm.UpdateImageFilePath(imageFile);
            }
        }
    }

    #endregion

    #region Native Methods

    /// <summary>
    /// Native methods signatures.
    /// </summary>
    private static class NativeMethods
    {
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool DestroyIcon(IntPtr handle);
    }

    #endregion
}

