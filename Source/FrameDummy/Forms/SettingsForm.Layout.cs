// Copyright (c) 2013-2026 The Hello World Writer (https://www.thehelloworldwriter.com).
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

namespace FrameDummy;

/// <summary>Layout half of SettingsForm: control field declarations and BuildLayout. Behavior lives in SettingsForm.cs.</summary>
public partial class SettingsForm
{
    // Tab structure.
    TabControl _tabControl = null!;
    TabPage _frameTabPage = null!;
    TabPage _contentTabPage = null!;
    TabPage _prankTabPage = null!;
    TabPage _aboutTabPage = null!;

    // Frame tab controls.
    Label _titleLabel = null!;
    TextBox _titleTextBox = null!;
    Label _iconLabel = null!;
    TextBox _iconTextBox = null!;
    Button _iconBrowseButton = null!;
    Button _iconDefaultButton = null!;
    Label _borderLabel = null!;
    ComboBox _borderComboBox = null!;
    Label _opacityLabel = null!;
    TrackBar _opacityTrackBar = null!;
    CheckBox _controlBoxCheck = null!;
    CheckBox _showIconCheck = null!;
    CheckBox _minimizeBoxCheck = null!;
    CheckBox _maximizeBoxCheck = null!;
    CheckBox _showInTaskbarCheck = null!;
    CheckBox _topmostCheck = null!;

    // Content tab controls.
    Label _imageLabel = null!;
    TextBox _imageTextBox = null!;
    Button _imageBrowseButton = null!;
    Button _imageClearButton = null!;
    Label _sizingLabel = null!;
    ComboBox _imageSizingComboBox = null!;
    Button _autoSizeButton = null!;
    Label _colorLabel = null!;
    Label _colorValueLabel = null!;
    Button _colorBrowseButton = null!;
    Button _colorRandomButton = null!;
    CheckBox _colorTransparentCheck = null!;

    // Prank tab controls.
    Label _commandLabel = null!;
    TextBox _commandTextBox = null!;
    Label _prankIntroLabel = null!;
    CheckBox _prankNoRightClickCheck = null!;
    CheckBox _prankNoHotkeyCheck = null!;
    CheckBox _prankNoCloseCheck = null!;

    // About tab controls.
    Label _aboutNameLabel = null!;
    Label _aboutVersionLabel = null!;
    Label _aboutCopyrightLabel = null!;
    LinkLabel _aboutUrlLink = null!;

    /// <summary>Builds the entire layout: form properties, tab control, and each tab page's contents.</summary>
    void BuildLayout()
    {
        AccessibleName = Strings.SettingsTitle;
        BackColor = Color.Gainsboro;
        ClientSize = new Size(620, 380);
        Font = SystemFonts.MessageBoxFont!;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        KeyPreview = true;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(SettingsForm);
        Padding = new Padding(20);
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = Strings.SettingsTitle;

        BuildTabControl();
        BuildFrameTab();
        BuildContentTab();
        BuildPrankTab();
        BuildAboutTab();

        Controls.Add(_tabControl);
    }

    /// <summary>Creates the four TabPages and the TabControl that hosts them.</summary>
    void BuildTabControl()
    {
        _frameTabPage = new TabPage { Text = Strings.TabFrame, Padding = new Padding(3), UseVisualStyleBackColor = true };
        _contentTabPage = new TabPage { Text = Strings.TabContent, Padding = new Padding(3), UseVisualStyleBackColor = true };
        _prankTabPage = new TabPage { Text = Strings.TabPrank, Padding = new Padding(3), UseVisualStyleBackColor = true };
        _aboutTabPage = new TabPage { Text = Strings.TabAbout, Padding = new Padding(3), UseVisualStyleBackColor = true };

        _tabControl = new TabControl { Dock = DockStyle.Fill, Name = "settingsTabControl" };
        _tabControl.TabPages.Add(_frameTabPage);
        _tabControl.TabPages.Add(_contentTabPage);
        _tabControl.TabPages.Add(_prankTabPage);
        _tabControl.TabPages.Add(_aboutTabPage);
    }

    /// <summary>Window Frame tab: title, icon, border, opacity, six frame-toggle checkboxes.</summary>
    void BuildFrameTab()
    {
        _titleLabel = MakeFieldLabel(Strings.LabelTitle);
        _titleTextBox = new TextBox { Dock = DockStyle.Fill, Margin = new Padding(3) };
        _titleTextBox.TextChanged += OnTitleChanged;

        _iconLabel = MakeFieldLabel(Strings.LabelIcon);
        _iconTextBox = new TextBox { Dock = DockStyle.Fill, ReadOnly = true, Text = Strings.DefaultIcon, Margin = new Padding(3) };

        _iconBrowseButton = new Button { Text = Strings.ButtonBrowse, AutoSize = true, Margin = new Padding(3) };
        _iconBrowseButton.Click += OnIconBrowseClicked;
        _iconDefaultButton = new Button { Text = Strings.ButtonIconDefault, AutoSize = true, Margin = new Padding(3) };
        _iconDefaultButton.Click += OnIconDefaultClicked;

        var iconButtons = MakeButtonRow(_iconBrowseButton, _iconDefaultButton);

        _borderLabel = MakeFieldLabel(Strings.LabelBorder);
        _borderComboBox = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(3) };
        foreach (FormBorderStyle style in Enum.GetValues<FormBorderStyle>()) _borderComboBox.Items.Add(style);
        _borderComboBox.SelectedIndexChanged += OnBorderChanged;

        _opacityLabel = new Label
        {
            Text = string.Format(Strings.LabelOpacityFormat, 1.0),
            AutoSize = true,
            Anchor = AnchorStyles.Left | AnchorStyles.Right,
            Margin = new Padding(3),
        };
        _opacityTrackBar = new TrackBar
        {
            Dock = DockStyle.Fill,
            Minimum = 1,
            Maximum = 100,
            Value = 100,
            TickFrequency = 2,
            Margin = new Padding(3),
        };
        _opacityTrackBar.ValueChanged += OnOpacityChanged;

        _controlBoxCheck = MakeFrameCheck(Strings.CheckControlBox, OnControlBoxChanged);
        _showIconCheck = MakeFrameCheck(Strings.CheckShowIcon, OnShowIconChanged);
        _minimizeBoxCheck = MakeFrameCheck(Strings.CheckMinimizeBox, OnMinimizeBoxChanged);
        _maximizeBoxCheck = MakeFrameCheck(Strings.CheckMaximizeBox, OnMaximizeBoxChanged);
        _showInTaskbarCheck = MakeFrameCheck(Strings.CheckShowInTaskbar, OnShowInTaskbarChanged);
        _topmostCheck = MakeFrameCheck(Strings.CheckTopmost, OnTopmostChanged);

        var checkGrid = new TableLayoutPanel
        {
            ColumnCount = 3,
            RowCount = 2,
            AutoSize = true,
            Anchor = AnchorStyles.Left | AnchorStyles.Right,
            Margin = new Padding(0, 12, 0, 0),
        };
        checkGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        checkGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        checkGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
        checkGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        checkGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        checkGrid.Controls.Add(_controlBoxCheck, 0, 0);
        checkGrid.Controls.Add(_showIconCheck, 1, 0);
        checkGrid.Controls.Add(_minimizeBoxCheck, 2, 0);
        checkGrid.Controls.Add(_maximizeBoxCheck, 0, 1);
        checkGrid.Controls.Add(_showInTaskbarCheck, 1, 1);
        checkGrid.Controls.Add(_topmostCheck, 2, 1);

        var tlp = MakeTwoColumnTLP();
        for (int i = 0; i < 6; i++) tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        tlp.Controls.Add(_titleLabel, 0, 0);
        tlp.Controls.Add(_titleTextBox, 1, 0);
        tlp.Controls.Add(_iconLabel, 0, 1);
        tlp.Controls.Add(_iconTextBox, 1, 1);
        tlp.Controls.Add(iconButtons, 1, 2);
        tlp.Controls.Add(_borderLabel, 0, 3);
        tlp.Controls.Add(_borderComboBox, 1, 3);
        tlp.Controls.Add(_opacityLabel, 0, 4);
        tlp.Controls.Add(_opacityTrackBar, 1, 4);
        tlp.Controls.Add(checkGrid, 0, 5);
        tlp.SetColumnSpan(checkGrid, 2);

        _frameTabPage.Controls.Add(tlp);
    }

    /// <summary>Window Content tab: image, sizing, color.</summary>
    void BuildContentTab()
    {
        _imageLabel = MakeFieldLabel(Strings.LabelImage);
        _imageTextBox = new TextBox { Dock = DockStyle.Fill, ReadOnly = true, Text = Strings.NoImage, Margin = new Padding(3) };

        _imageBrowseButton = new Button { Text = Strings.ButtonBrowse, AutoSize = true, Margin = new Padding(3) };
        _imageBrowseButton.Click += OnImageBrowseClicked;
        _imageClearButton = new Button { Text = Strings.ButtonImageClear, AutoSize = true, Margin = new Padding(3) };
        _imageClearButton.Click += OnImageClearClicked;
        var imageButtons = MakeButtonRow(_imageBrowseButton, _imageClearButton);

        _sizingLabel = MakeFieldLabel(Strings.LabelSizing);
        _imageSizingComboBox = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(3) };
        foreach (PictureBoxSizeMode mode in Enum.GetValues<PictureBoxSizeMode>())
            if (mode != PictureBoxSizeMode.AutoSize) _imageSizingComboBox.Items.Add(mode);
        _imageSizingComboBox.SelectedIndexChanged += OnImageSizingChanged;

        _autoSizeButton = new Button { Text = Strings.ButtonAutosize, AutoSize = true, Margin = new Padding(3) };
        _autoSizeButton.Click += OnAutoSizeClicked;

        var sizingRow = new TableLayoutPanel
        {
            ColumnCount = 2,
            RowCount = 1,
            Dock = DockStyle.Fill,
            AutoSize = true,
            Margin = new Padding(0),
        };
        sizingRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        sizingRow.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        sizingRow.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        sizingRow.Controls.Add(_imageSizingComboBox, 0, 0);
        sizingRow.Controls.Add(_autoSizeButton, 1, 0);

        _colorLabel = MakeFieldLabel(Strings.LabelColor);
        _colorValueLabel = new Label
        {
            AutoSize = false,
            BackColor = Color.LightSlateGray,
            BorderStyle = BorderStyle.FixedSingle,
            Dock = DockStyle.Fill,
            Height = 24,
            Margin = new Padding(3),
        };
        _colorValueLabel.BackColorChanged += OnColorValueChanged;

        _colorBrowseButton = new Button { Text = Strings.ButtonBrowse, AutoSize = true, Margin = new Padding(3) };
        _colorBrowseButton.Click += OnColorBrowseClicked;
        _colorRandomButton = new Button { Text = Strings.ButtonColorRandom, AutoSize = true, Margin = new Padding(3) };
        _colorRandomButton.Click += OnColorRandomClicked;
        _colorTransparentCheck = new CheckBox
        {
            Text = Strings.CheckColorTransparent,
            AutoSize = true,
            Anchor = AnchorStyles.Left,
            Margin = new Padding(12, 6, 3, 3),
        };
        _colorTransparentCheck.CheckedChanged += OnColorTransparentChanged;
        var colorButtons = MakeButtonRow(_colorBrowseButton, _colorRandomButton, _colorTransparentCheck);

        var tlp = MakeTwoColumnTLP();
        for (int i = 0; i < 5; i++) tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        tlp.Controls.Add(_imageLabel, 0, 0);
        tlp.Controls.Add(_imageTextBox, 1, 0);
        tlp.Controls.Add(imageButtons, 1, 1);
        tlp.Controls.Add(_sizingLabel, 0, 2);
        tlp.Controls.Add(sizingRow, 1, 2);
        tlp.Controls.Add(_colorLabel, 0, 3);
        tlp.Controls.Add(_colorValueLabel, 1, 3);
        tlp.Controls.Add(colorButtons, 1, 4);

        _contentTabPage.Controls.Add(tlp);
    }

    /// <summary>Prank Mode tab: command, intro label, three prank checkboxes.</summary>
    void BuildPrankTab()
    {
        _commandLabel = new Label { Text = Strings.LabelPrankCommand, AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(3, 3, 3, 0) };
        _commandTextBox = new TextBox { Dock = DockStyle.Fill, Margin = new Padding(3, 3, 3, 12) };
        _commandTextBox.TextChanged += OnCommandChanged;

        _prankIntroLabel = new Label { Text = Strings.LabelPrankIntro, AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(3, 6, 3, 6) };
        _prankNoRightClickCheck = MakePrankCheck(Strings.CheckPrankNoRightClick, OnPrankNoRightClickChanged);
        _prankNoHotkeyCheck = MakePrankCheck(Strings.CheckPrankNoHotkey, OnPrankNoHotkeyChanged);
        _prankNoCloseCheck = MakePrankCheck(Strings.CheckPrankNoClose, OnPrankNoCloseChanged);

        var tlp = new TableLayoutPanel
        {
            ColumnCount = 1,
            Dock = DockStyle.Fill,
            Padding = new Padding(20, 20, 20, 15),
        };
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (int i = 0; i < 5; i++) tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        tlp.Controls.Add(_commandLabel, 0, 0);
        tlp.Controls.Add(_commandTextBox, 0, 1);
        tlp.Controls.Add(_prankIntroLabel, 0, 2);
        tlp.Controls.Add(_prankNoRightClickCheck, 0, 3);
        tlp.Controls.Add(_prankNoHotkeyCheck, 0, 4);
        tlp.Controls.Add(_prankNoCloseCheck, 0, 5);
        tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        _prankTabPage.Controls.Add(tlp);
    }

    /// <summary>About tab: app name, version, copyright, URL link, vertically stacked.</summary>
    void BuildAboutTab()
    {
        _aboutNameLabel = new Label
        {
            Text = Strings.AppName,
            AutoSize = true,
            Font = new Font(Font.FontFamily, 12F, FontStyle.Bold),
            Margin = new Padding(3, 3, 3, 12),
        };
        _aboutVersionLabel = new Label { AutoSize = true, Margin = new Padding(3) };
        _aboutCopyrightLabel = new Label { AutoSize = true, Margin = new Padding(3, 3, 3, 12) };
        _aboutUrlLink = new LinkLabel { Text = Strings.AboutUrl, AutoSize = true, Margin = new Padding(3) };
        _aboutUrlLink.LinkClicked += OnAboutUrlClicked;

        var flow = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            Padding = new Padding(20),
            WrapContents = false,
        };
        flow.Controls.Add(_aboutNameLabel);
        flow.Controls.Add(_aboutVersionLabel);
        flow.Controls.Add(_aboutCopyrightLabel);
        flow.Controls.Add(_aboutUrlLink);

        _aboutTabPage.Controls.Add(flow);
    }

    /// <summary>Builds a Label sized to act as a left-side caption for a single-line input.</summary>
    static Label MakeFieldLabel(string text) => new()
    {
        Text = text,
        AutoSize = true,
        MinimumSize = new Size(80, 0),
        Anchor = AnchorStyles.Left | AnchorStyles.Right,
        Margin = new Padding(3),
    };

    /// <summary>Builds a CheckBox for the Frame tab's 6-checkbox grid, wired to the supplied event handler.</summary>
    static CheckBox MakeFrameCheck(string text, EventHandler handler)
    {
        var c = new CheckBox { Text = text, AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(3) };
        c.CheckedChanged += handler;
        return c;
    }

    /// <summary>Builds a CheckBox for the Prank tab (single-column, full-width row), wired to the supplied event handler.</summary>
    static CheckBox MakePrankCheck(string text, EventHandler handler)
    {
        var c = new CheckBox { Text = text, AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(3, 3, 3, 6) };
        c.CheckedChanged += handler;
        return c;
    }

    /// <summary>Wraps the given controls in a left-to-right FlowLayoutPanel suitable for a row of buttons in a TLP cell.</summary>
    static FlowLayoutPanel MakeButtonRow(params Control[] controls)
    {
        var flp = new FlowLayoutPanel
        {
            AutoSize = true,
            FlowDirection = FlowDirection.LeftToRight,
            Margin = new Padding(0),
            Anchor = AnchorStyles.Left,
        };
        flp.Controls.AddRange(controls);
        return flp;
    }

    /// <summary>Builds the standard 2-column TableLayoutPanel used by Frame and Content tabs (label column AutoSize, control column 100%).</summary>
    static TableLayoutPanel MakeTwoColumnTLP()
    {
        var tlp = new TableLayoutPanel
        {
            ColumnCount = 2,
            Dock = DockStyle.Fill,
            Padding = new Padding(20, 20, 20, 15),
        };
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        return tlp;
    }
}
