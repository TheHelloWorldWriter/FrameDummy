// Copyright (c) 2013-2026 The Hello World Writer (https://www.thehelloworldwriter.com).
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

using System.ComponentModel;
using System.Diagnostics;

using Windows.Win32;
using Windows.Win32.UI.WindowsAndMessaging;

namespace FrameDummy;

/// <summary>
/// The main FrameDummy frame: a top-level window that looks and behaves like a real one and shows an image
/// or solid color (with optional click-through transparency). Holds a reference to the shared Settings VM
/// (loaded by Program before constructing the form), binds Form properties to it, and listens for changes
/// to the two side-effect properties (IconPath, ImagePath) via a small PropertyChanged dispatcher. Hosts
/// the modeless SettingsForm for live editing.
/// </summary>
public class MainForm : Form
{
    /// <summary>Shared settings model; both forms hold a reference to the same instance.</summary>
    readonly Settings _vm;

    /// <summary>Default frame icon, extracted once from the .exe at construction. Restored when the user clears a custom icon.</summary>
    Icon? _defaultIcon;

    /// <summary>Currently displayed custom icon, or null when the default icon is in use.</summary>
    Icon? _customIcon;

    /// <summary>The single child PictureBox that fills the form and renders the image / background color.</summary>
    PictureBox _pictureBox = null!;

    /// <summary>The settings dialog. Created eagerly in the constructor; hidden (not closed) on user close.</summary>
    readonly SettingsForm _settingsForm = null!;

    /// <summary>Last successfully loaded icon path; used to revert <see cref="Settings.IconPath"/> when a load fails.</summary>
    string _lastValidIconPath = string.Empty;

    /// <summary>Last successfully loaded image path (or the PastedImage sentinel); used to revert on load failure.</summary>
    string _lastValidImagePath = string.Empty;

    /// <summary>Re-entry guard during a path-revert: blocks the dispatcher from re-attempting a load while we're rolling back a failed one.</summary>
    bool _applyingPath;

    /// <summary>Initializes the main form: builds layout, creates the SettingsForm, wires bindings, subscribes to VM PropertyChanged.</summary>
    public MainForm(Settings vm)
    {
        _vm = vm;
        BuildLayout();
        _settingsForm = new SettingsForm(_vm, this);
        WireBindings();

        // Subscribe AFTER bindings so the dispatcher doesn't fire during the initial bind-time push.
        _vm.PropertyChanged += OnVmPropertyChanged;
    }

    /// <summary>Configures form-level properties and the single PictureBox child. No settings applied here; bindings handle that in the constructor, OnLoad applies the side-effect properties.</summary>
    void BuildLayout()
    {
        AccessibleName = "FrameDummy";
        AccessibleDescription = "A fake desktop window frame for displaying images or screen areas.";
        AllowDrop = true;
        AutoScaleDimensions = new SizeF(8F, 16F);
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

    /// <summary>Connects Form properties to VM properties. One-way (VM is the source of truth); these Form properties are not user-editable from this form.</summary>
    void WireBindings()
    {
        DataBindings.Add(nameof(Text), _vm, nameof(Settings.Title));
        DataBindings.Add(nameof(FormBorderStyle), _vm, nameof(Settings.Border));
        DataBindings.Add(nameof(Opacity), _vm, nameof(Settings.OpacityFraction));
        DataBindings.Add(nameof(ControlBox), _vm, nameof(Settings.ControlBox));
        DataBindings.Add(nameof(ShowIcon), _vm, nameof(Settings.ShowIcon));
        DataBindings.Add(nameof(MinimizeBox), _vm, nameof(Settings.MinimizeBox));
        DataBindings.Add(nameof(MaximizeBox), _vm, nameof(Settings.MaximizeBox));
        DataBindings.Add(nameof(ShowInTaskbar), _vm, nameof(Settings.ShowInTaskbar));
        DataBindings.Add(nameof(TopMost), _vm, nameof(Settings.TopMost));
        DataBindings.Add(nameof(BackColor), _vm, nameof(Settings.Color));
        DataBindings.Add(nameof(TransparencyKey), _vm, nameof(Settings.EffectiveTransparencyKey));
        DataBindings.Add(nameof(Cursor), _vm, nameof(Settings.EffectiveCursor));
        _pictureBox.DataBindings.Add(nameof(PictureBox.BackColor), _vm, nameof(Settings.Color));
        _pictureBox.DataBindings.Add(nameof(PictureBox.SizeMode), _vm, nameof(Settings.ImageSizing));
    }

    /// <summary>Fires before the form is first painted. Applies the side-effect VM properties (icon, image, window bounds, maximized state) that aren't covered by data-bindings.</summary>
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ApplyIcon();
        ApplyImage();
        if (_vm.Bounds is { } b) Bounds = new Rectangle(b.X, b.Y, b.Width, b.Height);
        if (_vm.Maximized) WindowState = FormWindowState.Maximized;
    }

    /// <summary>Fires before the form actually closes. Honors prank-no-close, captures bounds, and saves settings.</summary>
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
        if (e.Cancel) return;

        if (_vm.PrankNoClose)
        {
            e.Cancel = true;
            return;
        }

        // Capture window bounds and maximized flag before saving so the next launch restores them.
        var bounds = WindowState == FormWindowState.Normal ? Bounds : RestoreBounds;
        _vm.Bounds = new WindowBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
        _vm.Maximized = WindowState == FormWindowState.Maximized;

        try
        {
            SettingsStore.Save(_vm);
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

    /// <summary>Disposes the icons and child SettingsForm we own. The PictureBox image is owned by the box and gets disposed by the framework.</summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _settingsForm?.Dispose();
            _customIcon?.Dispose();
            _defaultIcon?.Dispose();
        }
        base.Dispose(disposing);
    }

    /// <summary>Dispatches the two side-effect VM properties (paths to disk-backed resources) that bindings can't handle.</summary>
    void OnVmPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (_applyingPath) return;
        switch (e.PropertyName)
        {
            case nameof(Settings.IconPath): ApplyIcon(); break;
            case nameof(Settings.ImagePath): ApplyImage(); break;
        }
    }

    /// <summary>Applies the current IconPath: restores the default icon when empty; loads from disk otherwise. On load failure, shows an error and reverts the VM property to the last good path.</summary>
    void ApplyIcon()
    {
        var path = _vm.IconPath;
        if (string.IsNullOrEmpty(path))
        {
            _customIcon?.Dispose();
            _customIcon = null;
            if (_defaultIcon is not null) Icon = _defaultIcon;
            _lastValidIconPath = string.Empty;
            return;
        }

        try
        {
            LoadIcon(path);
            _lastValidIconPath = path;
        }
        catch (Exception ex) when (ex is IOException or ArgumentException or FileNotFoundException)
        {
            ShowError(string.Format(Strings.IconLoadErrorFormat, path));
            _applyingPath = true;
            try { _vm.IconPath = _lastValidIconPath; }
            finally { _applyingPath = false; }
        }
    }

    /// <summary>Applies the current ImagePath: clears when empty; skips when the PastedImage sentinel is set (image already on the PictureBox); loads from disk otherwise. Reverts on load failure.</summary>
    void ApplyImage()
    {
        var path = _vm.ImagePath;
        if (string.IsNullOrEmpty(path))
        {
            SetImage(null);
            _lastValidImagePath = string.Empty;
            return;
        }

        if (path == Strings.PastedImage)
        {
            // Image is already on the PictureBox - the paste handler set it directly before flipping the path sentinel.
            _lastValidImagePath = path;
            return;
        }

        try
        {
            LoadImage(path);
            _lastValidImagePath = path;
        }
        catch (FileNotFoundException)
        {
            ShowError(string.Format(Strings.ImageNotFoundErrorFormat, path));
            _applyingPath = true;
            try { _vm.ImagePath = _lastValidImagePath; }
            finally { _applyingPath = false; }
        }
        catch (OutOfMemoryException)
        {
            ShowError(string.Format(Strings.ImageLoadErrorFormat, path));
            _applyingPath = true;
            try { _vm.ImagePath = _lastValidImagePath; }
            finally { _applyingPath = false; }
        }
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

    /// <summary>Shrinks or grows the form's client area to match the loaded image's preferred size. Public so SettingsForm's Autosize button can call it directly.</summary>
    public void DoAutoSize()
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
                if (!_vm.PrankNoSettingsHotkey) ToggleSettings();
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
                if (!string.IsNullOrEmpty(_vm.PrankCommand)) RunPrankCommand(_vm.PrankCommand);
                break;
            case MouseButtons.Right:
                if (!_vm.PrankNoSettingsRightClick) ToggleSettings();
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

    /// <summary>Sets the dropped image path on the VM; the dispatcher loads the image and bindings update SettingsForm's textbox.</summary>
    protected override void OnDragDrop(DragEventArgs drgevent)
    {
        base.OnDragDrop(drgevent);
        if (drgevent.Data?.GetData(DataFormats.FileDrop, false) is not string[] files || files.Length == 0) return;
        _vm.ImagePath = files[0];
    }

    /// <summary>Pastes a clipboard image (or the first file in a clipboard file-drop list) as the frame image.</summary>
    void PasteImage()
    {
        if (Clipboard.ContainsImage())
        {
            var image = Clipboard.GetImage();
            if (image is null) return;
            SetImage(image);                     // place the image directly...
            _vm.ImagePath = Strings.PastedImage; // ...then flip the path sentinel; dispatcher sees the sentinel and skips reload.
            return;
        }

        if (!Clipboard.ContainsFileDropList()) return;
        var imageFile = Clipboard.GetFileDropList()[0];
        if (imageFile is null) return;
        _vm.ImagePath = imageFile;  // dispatcher will load.
    }

    /// <summary>Shows or hides the SettingsForm. The dialog is modeless and owned by this form.</summary>
    void ToggleSettings()
    {
        if (_settingsForm.Visible)
            _settingsForm.Hide();
        else
            _settingsForm.Show(this);
    }

    /// <summary>Shows a message-box error tied to this form, captioned with the application product name.</summary>
    void ShowError(string message)
    {
        MessageBox.Show(this, message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
