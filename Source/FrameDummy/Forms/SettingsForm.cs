// Copyright (c) 2013-2026 The Hello World Writer (https://www.thehelloworldwriter.com).
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

using System.ComponentModel;
using System.Diagnostics;

namespace FrameDummy;

/// <summary>
/// Settings dialog: a tabbed modeless window that mirrors the current Settings record into UI controls
/// and routes any user edit back to the owner via a constructor-injected Action callback. Layout lives
/// in SettingsForm.Layout.cs; this file holds state, behavior, and event handlers.
/// </summary>
public partial class SettingsForm : Form
{
    /// <summary>Current settings view shown by the dialog. Replaced on each user edit and on Refresh.</summary>
    Settings _settings;

    /// <summary>Callback invoked on any user-driven settings change. The owner (MainForm) applies the new record.</summary>
    readonly Action<Settings> _onChanged;

    /// <summary>RNG used by the "Random" color button.</summary>
    readonly Random _random = new();

    /// <summary>When true, control change events do not fire callbacks. Set during Populate to avoid event storms.</summary>
    bool _suppressEvents;

    /// <summary>Lazy file-open dialog for picking a custom icon.</summary>
    OpenFileDialog? _openIconDialog;

    /// <summary>Lazy file-open dialog for picking a custom image.</summary>
    OpenFileDialog? _openImageDialog;

    /// <summary>Lazy color-picker dialog.</summary>
    ColorDialog? _colorDialog;

    /// <summary>Raised when the user clicks the Autosize button. The owner sizes its frame to the loaded image.</summary>
    public event EventHandler? AutoSizeRequested;

    /// <summary>Constructs the dialog with an initial Settings snapshot and a callback for user edits.</summary>
    public SettingsForm(Settings initial, Action<Settings> onChanged)
    {
        _settings = initial;
        _onChanged = onChanged;
        BuildLayout();
        Populate(initial);
    }

    /// <summary>Refreshes the dialog from a new Settings snapshot. Called by the owner after self-modifying (drag-drop, paste).</summary>
    public void Refresh(Settings newSettings)
    {
        _settings = newSettings;
        Populate(newSettings);
    }

    /// <summary>Fills every control from the given Settings record. Suppresses change events for the duration so no callback storm.</summary>
    void Populate(Settings s)
    {
        _suppressEvents = true;
        try
        {
            _titleTextBox.Text = s.Title;
            _iconTextBox.Text = string.IsNullOrEmpty(s.IconPath) ? Strings.DefaultIcon : s.IconPath;
            _borderComboBox.SelectedItem = s.Border;
            _opacityTrackBar.Value = Math.Clamp(s.Opacity, _opacityTrackBar.Minimum, _opacityTrackBar.Maximum);
            _opacityLabel.Text = string.Format(Strings.LabelOpacityFormat, s.Opacity / 100.0);
            _controlBoxCheck.Checked = s.ControlBox;
            _showIconCheck.Checked = s.ShowIcon;
            _minimizeBoxCheck.Checked = s.MinimizeBox;
            _maximizeBoxCheck.Checked = s.MaximizeBox;
            _showInTaskbarCheck.Checked = s.ShowInTaskbar;
            _topmostCheck.Checked = s.TopMost;

            _imageTextBox.Text = string.IsNullOrEmpty(s.ImagePath) ? Strings.NoImage : s.ImagePath;
            _imageSizingComboBox.SelectedItem = s.ImageSizing;
            _colorValueLabel.BackColor = ColorTranslator.FromHtml(s.Color);
            _colorTransparentCheck.Checked = s.ColorTransparent;

            _commandTextBox.Text = s.PrankCommand;
            _prankNoRightClickCheck.Checked = s.PrankNoSettingsRightClick;
            _prankNoHotkeyCheck.Checked = s.PrankNoSettingsHotkey;
            _prankNoCloseCheck.Checked = s.PrankNoClose;
        }
        finally
        {
            _suppressEvents = false;
        }
    }

    /// <summary>Applies a transform to the current settings and notifies the owner via the callback. No-op while events are suppressed.</summary>
    void Apply(Func<Settings, Settings> transform)
    {
        if (_suppressEvents) return;
        _settings = transform(_settings);
        _onChanged(_settings);
    }

    /// <summary>Hides instead of closing on user-driven close so the dialog state persists across toggles.</summary>
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing)
        {
            Hide();
            e.Cancel = true;
        }
        base.OnFormClosing(e);
    }

    /// <summary>Escape hides the dialog (same as the close button) so a single keystroke dismisses it.</summary>
    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (e.KeyCode == Keys.Escape)
        {
            Hide();
            e.SuppressKeyPress = true;
        }
    }

    /// <summary>Populates the About tab's dynamic version and copyright lines on first show.</summary>
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        _aboutVersionLabel.Text = string.Format(Strings.AboutVersionFormat, Application.ProductVersion);
        _aboutCopyrightLabel.Text = string.Format(Strings.AboutCopyrightFormat, DateTime.Today.Year);
    }

    // Frame tab event handlers.

    /// <summary>Title text changed: push the new title into the settings.</summary>
    void OnTitleChanged(object? sender, EventArgs e) => Apply(s => s with { Title = _titleTextBox.Text });

    /// <summary>Icon Browse clicked: open a file dialog and (on OK) push the chosen path into the settings.</summary>
    void OnIconBrowseClicked(object? sender, EventArgs e)
    {
        _openIconDialog ??= new OpenFileDialog { Title = Strings.IconDialogTitle, Filter = Strings.IconDialogFilter };
        var startDir = string.IsNullOrEmpty(_settings.IconPath) ? null : Path.GetDirectoryName(_settings.IconPath);
        if (!string.IsNullOrEmpty(startDir)) _openIconDialog.InitialDirectory = startDir;
        if (_openIconDialog.ShowDialog(this) == DialogResult.OK)
            Apply(s => s with { IconPath = _openIconDialog.FileName });
    }

    /// <summary>Icon Default clicked: clear the IconPath so the owner restores the application's default icon.</summary>
    void OnIconDefaultClicked(object? sender, EventArgs e) => Apply(s => s with { IconPath = string.Empty });

    /// <summary>Border style changed: push the new enum value into the settings.</summary>
    void OnBorderChanged(object? sender, EventArgs e)
    {
        if (_borderComboBox.SelectedItem is FormBorderStyle border) Apply(s => s with { Border = border });
    }

    /// <summary>Opacity slider changed: update the formatted label and push the new percentage into the settings.</summary>
    void OnOpacityChanged(object? sender, EventArgs e)
    {
        var value = _opacityTrackBar.Value;
        _opacityLabel.Text = string.Format(Strings.LabelOpacityFormat, value / 100.0);
        Apply(s => s with { Opacity = value });
    }

    /// <summary>Control Box checkbox changed.</summary>
    void OnControlBoxChanged(object? sender, EventArgs e) => Apply(s => s with { ControlBox = _controlBoxCheck.Checked });

    /// <summary>Show Icon checkbox changed.</summary>
    void OnShowIconChanged(object? sender, EventArgs e) => Apply(s => s with { ShowIcon = _showIconCheck.Checked });

    /// <summary>Minimize Box checkbox changed.</summary>
    void OnMinimizeBoxChanged(object? sender, EventArgs e) => Apply(s => s with { MinimizeBox = _minimizeBoxCheck.Checked });

    /// <summary>Maximize Box checkbox changed.</summary>
    void OnMaximizeBoxChanged(object? sender, EventArgs e) => Apply(s => s with { MaximizeBox = _maximizeBoxCheck.Checked });

    /// <summary>Show In Taskbar checkbox changed.</summary>
    void OnShowInTaskbarChanged(object? sender, EventArgs e) => Apply(s => s with { ShowInTaskbar = _showInTaskbarCheck.Checked });

    /// <summary>Topmost checkbox changed.</summary>
    void OnTopmostChanged(object? sender, EventArgs e) => Apply(s => s with { TopMost = _topmostCheck.Checked });

    // Content tab event handlers.

    /// <summary>Image Browse clicked: open a file dialog and (on OK) push the chosen path into the settings.</summary>
    void OnImageBrowseClicked(object? sender, EventArgs e)
    {
        _openImageDialog ??= new OpenFileDialog { Title = Strings.ImageDialogTitle, Filter = Strings.ImageDialogFilter };
        var startDir = string.IsNullOrEmpty(_settings.ImagePath) || _settings.ImagePath == Strings.PastedImage
            ? null
            : Path.GetDirectoryName(_settings.ImagePath);
        if (!string.IsNullOrEmpty(startDir)) _openImageDialog.InitialDirectory = startDir;
        if (_openImageDialog.ShowDialog(this) == DialogResult.OK)
            Apply(s => s with { ImagePath = _openImageDialog.FileName });
    }

    /// <summary>Image Clear clicked: clear ImagePath so the owner removes the current image.</summary>
    void OnImageClearClicked(object? sender, EventArgs e) => Apply(s => s with { ImagePath = string.Empty });

    /// <summary>Image Sizing combo changed.</summary>
    void OnImageSizingChanged(object? sender, EventArgs e)
    {
        if (_imageSizingComboBox.SelectedItem is PictureBoxSizeMode mode) Apply(s => s with { ImageSizing = mode });
    }

    /// <summary>Autosize button clicked: raises AutoSizeRequested so the owner can size its frame to the current image.</summary>
    void OnAutoSizeClicked(object? sender, EventArgs e) => AutoSizeRequested?.Invoke(this, EventArgs.Empty);

    /// <summary>Color value label background changed (after Browse or Random updated it): push the new color into the settings.</summary>
    void OnColorValueChanged(object? sender, EventArgs e)
    {
        Apply(s => s with { Color = ColorTranslator.ToHtml(_colorValueLabel.BackColor) });
    }

    /// <summary>Color Browse clicked: open a color picker initialized to the current color and (on OK) update the value label, which fires OnColorValueChanged.</summary>
    void OnColorBrowseClicked(object? sender, EventArgs e)
    {
        _colorDialog ??= new ColorDialog();
        _colorDialog.Color = _colorValueLabel.BackColor;
        if (_colorDialog.ShowDialog(this) == DialogResult.OK)
            _colorValueLabel.BackColor = _colorDialog.Color;
    }

    /// <summary>Color Random clicked: pick a random RGB color and assign to the value label, which fires OnColorValueChanged.</summary>
    void OnColorRandomClicked(object? sender, EventArgs e)
    {
        _colorValueLabel.BackColor = Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256));
    }

    /// <summary>Color transparency checkbox changed.</summary>
    void OnColorTransparentChanged(object? sender, EventArgs e) => Apply(s => s with { ColorTransparent = _colorTransparentCheck.Checked });

    // Prank tab event handlers.

    /// <summary>Prank command text changed.</summary>
    void OnCommandChanged(object? sender, EventArgs e) => Apply(s => s with { PrankCommand = _commandTextBox.Text });

    /// <summary>"Don't show Settings on right-click" prank checkbox changed.</summary>
    void OnPrankNoRightClickChanged(object? sender, EventArgs e) => Apply(s => s with { PrankNoSettingsRightClick = _prankNoRightClickCheck.Checked });

    /// <summary>"Don't show Settings on Ctrl+S" prank checkbox changed.</summary>
    void OnPrankNoHotkeyChanged(object? sender, EventArgs e) => Apply(s => s with { PrankNoSettingsHotkey = _prankNoHotkeyCheck.Checked });

    /// <summary>"Don't allow close" prank checkbox changed.</summary>
    void OnPrankNoCloseChanged(object? sender, EventArgs e) => Apply(s => s with { PrankNoClose = _prankNoCloseCheck.Checked });

    // About tab event handlers.

    /// <summary>About URL clicked: launch the link in the user's default browser via the shell.</summary>
    void OnAboutUrlClicked(object? sender, LinkLabelLinkClickedEventArgs e)
    {
        try
        {
            // UseShellExecute = true is required on .NET Core+/.NET 5+ to launch URLs.
            Process.Start(new ProcessStartInfo(Strings.AboutUrl) { UseShellExecute = true });
        }
        catch (Exception ex) when (ex is Win32Exception or InvalidOperationException or FileNotFoundException)
        {
            MessageBox.Show(this, string.Format(Strings.CommandErrorFormat, Strings.AboutUrl, ex.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
