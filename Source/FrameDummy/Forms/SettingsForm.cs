// Copyright (c) 2013-2026 The Hello World Writer (https://www.thehelloworldwriter.com).
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

using System.ComponentModel;
using System.Diagnostics;

namespace FrameDummy;

/// <summary>
/// Settings dialog: a tabbed modeless window over the shared Settings ObservableObject. Most controls
/// data-bind directly to VM properties (two-way, OnPropertyChanged update mode), so user edits flow
/// through the VM and out to MainForm via its bindings - no per-property callback needed. A handful of
/// one-shot actions (Browse / Default / Random / Autosize / URL) keep their event handlers because they
/// open dialogs or invoke side effects beyond a property write. Layout lives in SettingsForm.Layout.cs.
/// </summary>
public partial class SettingsForm : Form
{
    /// <summary>Shared settings model; both forms hold a reference to the same instance.</summary>
    readonly Settings _vm;

    /// <summary>Reference to MainForm for the one direct call we still need (DoAutoSize on Autosize-button click).</summary>
    readonly MainForm _mainForm;

    /// <summary>RNG used by the "Random" color button.</summary>
    readonly Random _random = new();

    /// <summary>Lazy file-open dialog for picking a custom icon.</summary>
    OpenFileDialog? _openIconDialog;

    /// <summary>Lazy file-open dialog for picking a custom image.</summary>
    OpenFileDialog? _openImageDialog;

    /// <summary>Lazy color-picker dialog.</summary>
    ColorDialog? _colorDialog;

    /// <summary>Constructs the dialog over a shared VM and a MainForm reference (used only for the Autosize one-shot).</summary>
    public SettingsForm(Settings vm, MainForm mainForm)
    {
        _vm = vm;
        _mainForm = mainForm;
        BuildLayout();
        WireBindings();
    }

    /// <summary>Connects each user-editable control to the corresponding VM property. Two-way bindings use OnPropertyChanged so updates land per keystroke, not per validation event.</summary>
    void WireBindings()
    {
        // Frame tab.
        _titleTextBox.DataBindings.Add(nameof(TextBox.Text), _vm, nameof(Settings.Title), false, DataSourceUpdateMode.OnPropertyChanged);
        _iconTextBox.DataBindings.Add(nameof(TextBox.Text), _vm, nameof(Settings.IconPath), false, DataSourceUpdateMode.OnPropertyChanged);
        _borderComboBox.DataBindings.Add(nameof(ComboBox.SelectedItem), _vm, nameof(Settings.Border), false, DataSourceUpdateMode.OnPropertyChanged);
        _opacityTrackBar.DataBindings.Add(nameof(TrackBar.Value), _vm, nameof(Settings.Opacity), false, DataSourceUpdateMode.OnPropertyChanged);
        _opacityLabel.DataBindings.Add(nameof(Label.Text), _vm, nameof(Settings.OpacityLabel));
        _controlBoxCheck.DataBindings.Add(nameof(CheckBox.Checked), _vm, nameof(Settings.ControlBox), false, DataSourceUpdateMode.OnPropertyChanged);
        _showIconCheck.DataBindings.Add(nameof(CheckBox.Checked), _vm, nameof(Settings.ShowIcon), false, DataSourceUpdateMode.OnPropertyChanged);
        _minimizeBoxCheck.DataBindings.Add(nameof(CheckBox.Checked), _vm, nameof(Settings.MinimizeBox), false, DataSourceUpdateMode.OnPropertyChanged);
        _maximizeBoxCheck.DataBindings.Add(nameof(CheckBox.Checked), _vm, nameof(Settings.MaximizeBox), false, DataSourceUpdateMode.OnPropertyChanged);
        _showInTaskbarCheck.DataBindings.Add(nameof(CheckBox.Checked), _vm, nameof(Settings.ShowInTaskbar), false, DataSourceUpdateMode.OnPropertyChanged);
        _topmostCheck.DataBindings.Add(nameof(CheckBox.Checked), _vm, nameof(Settings.TopMost), false, DataSourceUpdateMode.OnPropertyChanged);

        // Content tab.
        _imageTextBox.DataBindings.Add(nameof(TextBox.Text), _vm, nameof(Settings.ImagePath), false, DataSourceUpdateMode.OnPropertyChanged);
        _imageSizingComboBox.DataBindings.Add(nameof(ComboBox.SelectedItem), _vm, nameof(Settings.ImageSizing), false, DataSourceUpdateMode.OnPropertyChanged);
        _colorValueLabel.DataBindings.Add(nameof(Label.BackColor), _vm, nameof(Settings.Color), false, DataSourceUpdateMode.OnPropertyChanged);
        _colorTransparentCheck.DataBindings.Add(nameof(CheckBox.Checked), _vm, nameof(Settings.ColorTransparent), false, DataSourceUpdateMode.OnPropertyChanged);

        // Prank tab.
        _commandTextBox.DataBindings.Add(nameof(TextBox.Text), _vm, nameof(Settings.PrankCommand), false, DataSourceUpdateMode.OnPropertyChanged);
        _prankNoRightClickCheck.DataBindings.Add(nameof(CheckBox.Checked), _vm, nameof(Settings.PrankNoSettingsRightClick), false, DataSourceUpdateMode.OnPropertyChanged);
        _prankNoHotkeyCheck.DataBindings.Add(nameof(CheckBox.Checked), _vm, nameof(Settings.PrankNoSettingsHotkey), false, DataSourceUpdateMode.OnPropertyChanged);
        _prankNoCloseCheck.DataBindings.Add(nameof(CheckBox.Checked), _vm, nameof(Settings.PrankNoClose), false, DataSourceUpdateMode.OnPropertyChanged);
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

    // One-shot handlers - everything else is bindings.

    /// <summary>Icon Browse clicked: open a file dialog and (on OK) push the chosen path into the VM. The MainForm dispatcher loads the icon.</summary>
    void OnIconBrowseClicked(object? sender, EventArgs e)
    {
        _openIconDialog ??= new OpenFileDialog { Title = Strings.IconDialogTitle, Filter = Strings.IconDialogFilter };
        var startDir = string.IsNullOrEmpty(_vm.IconPath) ? null : Path.GetDirectoryName(_vm.IconPath);
        if (!string.IsNullOrEmpty(startDir)) _openIconDialog.InitialDirectory = startDir;
        if (_openIconDialog.ShowDialog(this) == DialogResult.OK) _vm.IconPath = _openIconDialog.FileName;
    }

    /// <summary>Icon Default clicked: clear the IconPath so MainForm restores the application's default icon.</summary>
    void OnIconDefaultClicked(object? sender, EventArgs e) => _vm.IconPath = string.Empty;

    /// <summary>Image Browse clicked: open a file dialog and (on OK) push the chosen path into the VM.</summary>
    void OnImageBrowseClicked(object? sender, EventArgs e)
    {
        _openImageDialog ??= new OpenFileDialog { Title = Strings.ImageDialogTitle, Filter = Strings.ImageDialogFilter };
        var startDir = string.IsNullOrEmpty(_vm.ImagePath) || _vm.ImagePath == Strings.PastedImage
            ? null
            : Path.GetDirectoryName(_vm.ImagePath);
        if (!string.IsNullOrEmpty(startDir)) _openImageDialog.InitialDirectory = startDir;
        if (_openImageDialog.ShowDialog(this) == DialogResult.OK) _vm.ImagePath = _openImageDialog.FileName;
    }

    /// <summary>Image Clear clicked: clear the ImagePath so MainForm removes the current image.</summary>
    void OnImageClearClicked(object? sender, EventArgs e) => _vm.ImagePath = string.Empty;

    /// <summary>Autosize clicked: ask MainForm to size its frame to the loaded image's preferred size.</summary>
    void OnAutoSizeClicked(object? sender, EventArgs e) => _mainForm.DoAutoSize();

    /// <summary>Color Browse clicked: open the color picker initialized to the current color and (on OK) write back to the VM.</summary>
    void OnColorBrowseClicked(object? sender, EventArgs e)
    {
        _colorDialog ??= new ColorDialog();
        _colorDialog.Color = _vm.Color;
        if (_colorDialog.ShowDialog(this) == DialogResult.OK) _vm.Color = _colorDialog.Color;
    }

    /// <summary>Color Random clicked: pick a random RGB color and write to the VM.</summary>
    void OnColorRandomClicked(object? sender, EventArgs e)
        => _vm.Color = Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256));

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
