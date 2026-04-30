// Copyright (c) 2013-2026 The Hello World Writer (https://www.thehelloworldwriter.com).
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;

using Windows.Win32;
using Windows.Win32.UI.WindowsAndMessaging;

namespace FrameDummy;

/// <summary>
/// The main FrameDummy frame: a top-level window that looks and behaves like a real one and shows an image
/// or solid color (with optional click-through transparency). Owns the persisted Settings record and applies
/// it to the form on load; saves it on close.
/// </summary>
public class MainForm : Form
{
    /// <summary>The currently active settings. Replaced (with-expression) when the user changes a value.</summary>
    Settings _settings = new();

    /// <summary>Default frame icon, extracted once from the .exe at construction. Restored when the user clears a custom icon.</summary>
    Icon? _defaultIcon;

    /// <summary>Currently displayed custom icon, or null when the default icon is in use.</summary>
    Icon? _customIcon;

    /// <summary>The single child PictureBox that fills the form and renders the image / background color.</summary>
    PictureBox _pictureBox = null!;

    /// <summary>Initializes the main form: builds layout, extracts the default icon from the .exe.</summary>
    public MainForm()
    {
        BuildLayout();
    }

    /// <summary>Configures form-level properties and the single PictureBox child. No settings applied here; that happens in OnLoad.</summary>
    void BuildLayout()
    {
        AccessibleName = "FrameDummy";
        AccessibleDescription = "A fake desktop window frame for displaying images or screen areas.";
        AllowDrop = true;
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.LightSlateGray;
        ClientSize = new Size(687, 451);
        DoubleBuffered = true;
        Font = SystemFonts.MessageBoxFont!;
        KeyPreview = true;
        Name = nameof(MainForm);
        StartPosition = FormStartPosition.CenterScreen;
        Text = Strings.DefaultTitle;
        TopMost = true;
        TransparencyKey = Color.LightSlateGray;

        // Extract the default icon from the running .exe (the .ico is set via <ApplicationIcon> in the csproj).
        try
        {
            _defaultIcon = Icon.ExtractAssociatedIcon(Environment.ProcessPath ?? string.Empty);
            if (_defaultIcon is not null) Icon = _defaultIcon;
        }
        catch (Exception ex) when (ex is IOException or ArgumentException)
        {
            // Fall through with the framework's default form icon if extraction fails.
        }

        _pictureBox = new PictureBox
        {
            AccessibleName = "Frame content",
            Dock = DockStyle.Fill,
            Name = "FramePictureBox",
            SizeMode = PictureBoxSizeMode.Zoom,
            TabStop = false,
        };
        _pictureBox.MouseClick += OnFrameMouseClick;
        Controls.Add(_pictureBox);
    }

    /// <summary>Fires before the form is first painted. Loads settings from disk and applies them so there is no flicker between defaults and loaded state.</summary>
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        try
        {
            _settings = SettingsStore.Load();
            ApplySettings();
        }
        catch (Exception ex) when (ex is IOException or JsonException or UnauthorizedAccessException)
        {
            ShowError(string.Format(Strings.SettingsLoadErrorFormat, ex.Message));
        }
    }

    /// <summary>Fires before the form actually closes. Honors prank-no-close, captures bounds, and saves settings.</summary>
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
        if (e.Cancel) return;

        if (_settings.PrankNoClose)
        {
            e.Cancel = true;
            return;
        }

        // Capture window bounds and maximized flag before saving so the next launch restores them.
        var bounds = WindowState == FormWindowState.Normal ? Bounds : RestoreBounds;
        _settings = _settings with
        {
            Bounds = new WindowBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height),
            Maximized = WindowState == FormWindowState.Maximized,
        };

        try
        {
            SettingsStore.Save(_settings);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            var result = MessageBox.Show(
                this,
                string.Format(Strings.SettingsSaveErrorFormat, ex.Message),
                Application.ProductName,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (result == DialogResult.No) e.Cancel = true;
        }
    }

    /// <summary>Disposes the icons we own (default and custom). The PictureBox image is owned by the box and gets disposed by the framework.</summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _customIcon?.Dispose();
            _defaultIcon?.Dispose();
        }
        base.Dispose(disposing);
    }

    /// <summary>Applies every property of the current Settings record to the form, PictureBox, and resources.</summary>
    void ApplySettings()
    {
        Text = _settings.Title;
        FormBorderStyle = _settings.Border;
        Opacity = _settings.Opacity / 100.0;
        ControlBox = _settings.ControlBox;
        ShowIcon = _settings.ShowIcon;
        MinimizeBox = _settings.MinimizeBox;
        MaximizeBox = _settings.MaximizeBox;
        ShowInTaskbar = _settings.ShowInTaskbar;
        TopMost = _settings.TopMost;
        Cursor = string.IsNullOrEmpty(_settings.PrankCommand) ? Cursors.Default : Cursors.Hand;
        _pictureBox.SizeMode = _settings.ImageSizing;

        var color = ColorTranslator.FromHtml(_settings.Color);
        BackColor = color;
        _pictureBox.BackColor = color;
        TransparencyKey = _settings.ColorTransparent ? color : Color.Empty;

        if (!string.IsNullOrEmpty(_settings.IconPath))
        {
            try { LoadIcon(_settings.IconPath); }
            catch (Exception ex) when (ex is IOException or ArgumentException or FileNotFoundException)
            {
                ShowError(string.Format(Strings.IconLoadErrorFormat, _settings.IconPath));
            }
        }

        if (!string.IsNullOrEmpty(_settings.ImagePath) && _settings.ImagePath != Strings.PastedImage)
        {
            try { LoadImage(_settings.ImagePath); }
            catch (FileNotFoundException)
            {
                ShowError(string.Format(Strings.ImageNotFoundErrorFormat, _settings.ImagePath));
            }
            catch (OutOfMemoryException)
            {
                ShowError(string.Format(Strings.ImageLoadErrorFormat, _settings.ImagePath));
            }
        }

        if (_settings.Bounds is { } b) Bounds = new Rectangle(b.X, b.Y, b.Width, b.Height);
        if (_settings.Maximized) WindowState = FormWindowState.Maximized;
    }

    /// <summary>Loads a custom icon from a file. .ico files load directly; other image formats go via Bitmap.GetHicon and are cleaned up after cloning.</summary>
    void LoadIcon(string iconFilePath)
    {
        Icon newIcon;
        if (Path.GetExtension(iconFilePath).Equals(".ico", StringComparison.OrdinalIgnoreCase))
        {
            newIcon = new Icon(iconFilePath);
        }
        else
        {
            using var bitmap = new Bitmap(iconFilePath);
            var hIcon = bitmap.GetHicon();
            try
            {
                // Icon.FromHandle does not own the handle, so clone before destroying it.
                using var fromHandle = Icon.FromHandle(hIcon);
                newIcon = (Icon)fromHandle.Clone();
            }
            finally
            {
                PInvoke.DestroyIcon((HICON)hIcon);
            }
        }

        _customIcon?.Dispose();
        _customIcon = newIcon;
        Icon = newIcon;
    }

    /// <summary>Loads an image from disk into the frame. Throws on I/O or format errors; the caller surfaces user-facing messages.</summary>
    void LoadImage(string imageFile)
    {
        var image = Image.FromFile(imageFile);
        SetImage(image);
    }

    /// <summary>Replaces the frame's image, disposing the previous one to release GDI+ resources promptly.</summary>
    void SetImage(Image? image)
    {
        var previous = _pictureBox.Image;
        _pictureBox.Image = image;
        previous?.Dispose();
    }

    /// <summary>Shrinks or grows the form's client area to match the loaded image's preferred size.</summary>
    void DoAutoSize()
    {
        ClientSize = _pictureBox.PreferredSize;
    }

    /// <summary>Handles Ctrl+S (toggle settings), Ctrl+V (paste image), Ctrl+A (autosize). Honors the prank-no-hotkey flag for Ctrl+S.</summary>
    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (!e.Control) return;
        switch (e.KeyCode)
        {
            case Keys.S:
                if (!_settings.PrankNoSettingsHotkey) ToggleSettings();
                break;
            case Keys.V:
                PasteImage();
                break;
            case Keys.A:
                DoAutoSize();
                break;
        }
    }

    /// <summary>Handles left-click (run prank command) and right-click (toggle settings, unless suppressed by prank flag).</summary>
    void OnFrameMouseClick(object? sender, MouseEventArgs e)
    {
        switch (e.Button)
        {
            case MouseButtons.Left:
                if (!string.IsNullOrEmpty(_settings.PrankCommand)) RunPrankCommand(_settings.PrankCommand);
                break;
            case MouseButtons.Right:
                if (!_settings.PrankNoSettingsRightClick) ToggleSettings();
                break;
        }
    }

    /// <summary>Launches the configured prank command via the shell so URLs and arbitrary file paths work on .NET 6+.</summary>
    void RunPrankCommand(string command)
    {
        try
        {
            // UseShellExecute = true is required on .NET Core+/.NET 5+ to launch URLs and non-exe files.
            Process.Start(new ProcessStartInfo(command) { UseShellExecute = true });
        }
        catch (Exception ex) when (ex is Win32Exception or InvalidOperationException or FileNotFoundException)
        {
            ShowError(string.Format(Strings.CommandErrorFormat, command, ex.Message));
        }
    }

    /// <summary>Accepts file drops (image files); rejects everything else.</summary>
    protected override void OnDragEnter(DragEventArgs drgevent)
    {
        base.OnDragEnter(drgevent);
        drgevent.Effect = drgevent.Data?.GetDataPresent(DataFormats.FileDrop) == true
            ? DragDropEffects.All
            : DragDropEffects.None;
    }

    /// <summary>Loads the first dropped file as the frame image and remembers its path in the settings.</summary>
    protected override void OnDragDrop(DragEventArgs drgevent)
    {
        base.OnDragDrop(drgevent);
        if (drgevent.Data?.GetData(DataFormats.FileDrop, false) is not string[] files || files.Length == 0) return;

        try
        {
            LoadImage(files[0]);
            _settings = _settings with { ImagePath = files[0] };
        }
        catch (FileNotFoundException)
        {
            ShowError(string.Format(Strings.ImageNotFoundErrorFormat, files[0]));
        }
        catch (OutOfMemoryException)
        {
            ShowError(string.Format(Strings.ImageLoadErrorFormat, files[0]));
        }
    }

    /// <summary>Pastes a clipboard image (or the first file in a clipboard file-drop list) as the frame image.</summary>
    void PasteImage()
    {
        if (Clipboard.ContainsImage())
        {
            var image = Clipboard.GetImage();
            if (image is null) return;
            SetImage(image);
            _settings = _settings with { ImagePath = Strings.PastedImage };
            return;
        }

        if (!Clipboard.ContainsFileDropList()) return;
        var imageFile = Clipboard.GetFileDropList()[0];
        if (imageFile is null) return;

        try
        {
            LoadImage(imageFile);
            _settings = _settings with { ImagePath = imageFile };
        }
        catch (FileNotFoundException)
        {
            ShowError(string.Format(Strings.ImageNotFoundErrorFormat, imageFile));
        }
        catch (OutOfMemoryException)
        {
            ShowError(string.Format(Strings.ImageLoadErrorFormat, imageFile));
        }
    }

    /// <summary>Stub: opens or hides the Settings form. Surfaces a placeholder message until SettingsForm is ported.</summary>
    void ToggleSettings()
    {
        // TODO: open SettingsForm and pass _settings + a callback that updates _settings and re-applies.
        MessageBox.Show(this, "Settings dialog not yet implemented in this build.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>Shows a message-box error tied to this form, captioned with the application product name.</summary>
    void ShowError(string message)
    {
        MessageBox.Show(this, message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
