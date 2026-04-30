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

    // Layout panels.
    TableLayoutPanel _frameTLP = null!;
    TableLayoutPanel _frameChecksTLP = null!;
    FlowLayoutPanel _iconButtonsFLP = null!;
    TableLayoutPanel _contentTLP = null!;
    FlowLayoutPanel _imageButtonsFLP = null!;
    TableLayoutPanel _sizingRowTLP = null!;
    FlowLayoutPanel _colorButtonsFLP = null!;
    TableLayoutPanel _prankTLP = null!;
    FlowLayoutPanel _aboutFLP = null!;

    /// <summary>Builds the entire layout. Three-phase Designer-style flow: instantiate everything, configure each control in its labeled section, then add the tab control to the form.</summary>
    void BuildLayout()
    {
        InstantiateAll();
        SuspendLayout();
        ConfigureForm();
        ConfigureTabControl();
        ConfigureFrameTab();
        ConfigureContentTab();
        ConfigurePrankTab();
        ConfigureAboutTab();
        Controls.Add(_tabControl);
        ResumeLayout(false);
        PerformLayout();
    }

    /// <summary>Phase 1: create every control and layout panel up front, before any configuration.</summary>
    void InstantiateAll()
    {
        _tabControl = new TabControl();
        _frameTabPage = new TabPage();
        _contentTabPage = new TabPage();
        _prankTabPage = new TabPage();
        _aboutTabPage = new TabPage();

        _titleLabel = new Label();
        _titleTextBox = new TextBox();
        _iconLabel = new Label();
        _iconTextBox = new TextBox();
        _iconBrowseButton = new Button();
        _iconDefaultButton = new Button();
        _borderLabel = new Label();
        _borderComboBox = new ComboBox();
        _opacityLabel = new Label();
        _opacityTrackBar = new TrackBar();
        _controlBoxCheck = new CheckBox();
        _showIconCheck = new CheckBox();
        _minimizeBoxCheck = new CheckBox();
        _maximizeBoxCheck = new CheckBox();
        _showInTaskbarCheck = new CheckBox();
        _topmostCheck = new CheckBox();

        _imageLabel = new Label();
        _imageTextBox = new TextBox();
        _imageBrowseButton = new Button();
        _imageClearButton = new Button();
        _sizingLabel = new Label();
        _imageSizingComboBox = new ComboBox();
        _autoSizeButton = new Button();
        _colorLabel = new Label();
        _colorValueLabel = new Label();
        _colorBrowseButton = new Button();
        _colorRandomButton = new Button();
        _colorTransparentCheck = new CheckBox();

        _commandLabel = new Label();
        _commandTextBox = new TextBox();
        _prankIntroLabel = new Label();
        _prankNoRightClickCheck = new CheckBox();
        _prankNoHotkeyCheck = new CheckBox();
        _prankNoCloseCheck = new CheckBox();

        _aboutNameLabel = new Label();
        _aboutVersionLabel = new Label();
        _aboutCopyrightLabel = new Label();
        _aboutUrlLink = new LinkLabel();

        _frameTLP = new TableLayoutPanel();
        _frameChecksTLP = new TableLayoutPanel();
        _iconButtonsFLP = new FlowLayoutPanel();
        _contentTLP = new TableLayoutPanel();
        _imageButtonsFLP = new FlowLayoutPanel();
        _sizingRowTLP = new TableLayoutPanel();
        _colorButtonsFLP = new FlowLayoutPanel();
        _prankTLP = new TableLayoutPanel();
        _aboutFLP = new FlowLayoutPanel();
    }

    /// <summary>Form-level properties: the SettingsForm itself.</summary>
    void ConfigureForm()
    {
        // SettingsForm
        AccessibleName = Strings.SettingsTitle;
        AutoScaleDimensions = new SizeF(8F, 16F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowOnly;
        BackColor = Color.Gainsboro;
        ClientSize = new Size(640, 460);
        Font = SystemFonts.MessageBoxFont!;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        KeyPreview = true;
        MaximizeBox = false;
        MinimizeBox = false;
        MinimumSize = new Size(640, 480);
        Name = nameof(SettingsForm);
        Padding = new Padding(20);
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = Strings.SettingsTitle;
    }

    /// <summary>Tab control + four tab pages.</summary>
    void ConfigureTabControl()
    {
        // _tabControl
        _tabControl.Dock = DockStyle.Fill;
        _tabControl.Name = "settingsTabControl";
        _tabControl.TabPages.Add(_frameTabPage);
        _tabControl.TabPages.Add(_contentTabPage);
        _tabControl.TabPages.Add(_prankTabPage);
        _tabControl.TabPages.Add(_aboutTabPage);

        // _frameTabPage
        _frameTabPage.Padding = new Padding(3);
        _frameTabPage.Text = Strings.TabFrame;
        _frameTabPage.UseVisualStyleBackColor = true;

        // _contentTabPage
        _contentTabPage.Padding = new Padding(3);
        _contentTabPage.Text = Strings.TabContent;
        _contentTabPage.UseVisualStyleBackColor = true;

        // _prankTabPage
        _prankTabPage.Padding = new Padding(3);
        _prankTabPage.Text = Strings.TabPrank;
        _prankTabPage.UseVisualStyleBackColor = true;

        // _aboutTabPage
        _aboutTabPage.Padding = new Padding(3);
        _aboutTabPage.Text = Strings.TabAbout;
        _aboutTabPage.UseVisualStyleBackColor = true;
    }

    /// <summary>Window Frame tab: title, icon, border, opacity, six frame-toggle checkboxes.</summary>
    void ConfigureFrameTab()
    {
        // _frameTLP (outer 2-column grid for the tab)
        _frameTLP.ColumnCount = 2;
        _frameTLP.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _frameTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        _frameTLP.Dock = DockStyle.Fill;
        _frameTLP.Padding = new Padding(20, 20, 20, 15);
        _frameTLP.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _frameTLP.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _frameTLP.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _frameTLP.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _frameTLP.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _frameTLP.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _frameTLP.Controls.Add(_titleLabel, 0, 0);
        _frameTLP.Controls.Add(_titleTextBox, 1, 0);
        _frameTLP.Controls.Add(_iconLabel, 0, 1);
        _frameTLP.Controls.Add(_iconTextBox, 1, 1);
        _frameTLP.Controls.Add(_iconButtonsFLP, 1, 2);
        _frameTLP.Controls.Add(_borderLabel, 0, 3);
        _frameTLP.Controls.Add(_borderComboBox, 1, 3);
        _frameTLP.Controls.Add(_opacityLabel, 0, 4);
        _frameTLP.Controls.Add(_opacityTrackBar, 1, 4);
        _frameTLP.Controls.Add(_frameChecksTLP, 0, 5);
        _frameTLP.SetColumnSpan(_frameChecksTLP, 2);

        // _frameChecksTLP (nested 3x2 grid for the 6 frame-toggle checkboxes)
        _frameChecksTLP.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _frameChecksTLP.AutoSize = true;
        _frameChecksTLP.ColumnCount = 3;
        _frameChecksTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        _frameChecksTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        _frameChecksTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
        _frameChecksTLP.Margin = new Padding(0, 12, 0, 0);
        _frameChecksTLP.RowCount = 2;
        _frameChecksTLP.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _frameChecksTLP.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _frameChecksTLP.Controls.Add(_controlBoxCheck, 0, 0);
        _frameChecksTLP.Controls.Add(_showIconCheck, 1, 0);
        _frameChecksTLP.Controls.Add(_minimizeBoxCheck, 2, 0);
        _frameChecksTLP.Controls.Add(_maximizeBoxCheck, 0, 1);
        _frameChecksTLP.Controls.Add(_showInTaskbarCheck, 1, 1);
        _frameChecksTLP.Controls.Add(_topmostCheck, 2, 1);

        // _iconButtonsFLP (icon Browse + Default row)
        _iconButtonsFLP.Anchor = AnchorStyles.Left;
        _iconButtonsFLP.AutoSize = true;
        _iconButtonsFLP.FlowDirection = FlowDirection.LeftToRight;
        _iconButtonsFLP.Margin = new Padding(0, 0, 0, 12);
        _iconButtonsFLP.Controls.Add(_iconBrowseButton);
        _iconButtonsFLP.Controls.Add(_iconDefaultButton);

        // _titleLabel
        _titleLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _titleLabel.AutoSize = true;
        _titleLabel.Margin = new Padding(3);
        _titleLabel.MinimumSize = new Size(80, 0);
        _titleLabel.Text = Strings.LabelTitle;

        // _titleTextBox
        _titleTextBox.Dock = DockStyle.Fill;
        _titleTextBox.Margin = new Padding(3, 3, 3, 12);
        _titleTextBox.TextChanged += OnTitleChanged;

        // _iconLabel
        _iconLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _iconLabel.AutoSize = true;
        _iconLabel.Margin = new Padding(3);
        _iconLabel.MinimumSize = new Size(80, 0);
        _iconLabel.Text = Strings.LabelIcon;

        // _iconTextBox
        _iconTextBox.Dock = DockStyle.Fill;
        _iconTextBox.Margin = new Padding(3, 3, 3, 4);
        _iconTextBox.ReadOnly = true;
        _iconTextBox.Text = Strings.DefaultIcon;

        // _iconBrowseButton
        _iconBrowseButton.AutoSize = true;
        _iconBrowseButton.Margin = new Padding(3);
        _iconBrowseButton.Text = Strings.ButtonBrowse;
        _iconBrowseButton.Click += OnIconBrowseClicked;

        // _iconDefaultButton
        _iconDefaultButton.AutoSize = true;
        _iconDefaultButton.Margin = new Padding(3);
        _iconDefaultButton.Text = Strings.ButtonIconDefault;
        _iconDefaultButton.Click += OnIconDefaultClicked;

        // _borderLabel
        _borderLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _borderLabel.AutoSize = true;
        _borderLabel.Margin = new Padding(3);
        _borderLabel.MinimumSize = new Size(80, 0);
        _borderLabel.Text = Strings.LabelBorder;

        // _borderComboBox
        _borderComboBox.Dock = DockStyle.Fill;
        _borderComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _borderComboBox.Margin = new Padding(3, 3, 3, 20);
        foreach (FormBorderStyle style in Enum.GetValues<FormBorderStyle>()) _borderComboBox.Items.Add(style);
        _borderComboBox.SelectedIndexChanged += OnBorderChanged;

        // _opacityLabel
        _opacityLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _opacityLabel.AutoSize = true;
        _opacityLabel.Margin = new Padding(3);
        _opacityLabel.Text = string.Format(Strings.LabelOpacityFormat, 1.0);

        // _opacityTrackBar
        _opacityTrackBar.Dock = DockStyle.Fill;
        _opacityTrackBar.Margin = new Padding(3, 3, 3, 12);
        _opacityTrackBar.Maximum = 100;
        _opacityTrackBar.Minimum = 1;
        _opacityTrackBar.TickFrequency = 2;
        _opacityTrackBar.Value = 100;
        _opacityTrackBar.ValueChanged += OnOpacityChanged;

        // _controlBoxCheck
        _controlBoxCheck.Anchor = AnchorStyles.Left;
        _controlBoxCheck.AutoSize = true;
        _controlBoxCheck.Margin = new Padding(3);
        _controlBoxCheck.Text = Strings.CheckControlBox;
        _controlBoxCheck.CheckedChanged += OnControlBoxChanged;

        // _showIconCheck
        _showIconCheck.Anchor = AnchorStyles.Left;
        _showIconCheck.AutoSize = true;
        _showIconCheck.Margin = new Padding(3);
        _showIconCheck.Text = Strings.CheckShowIcon;
        _showIconCheck.CheckedChanged += OnShowIconChanged;

        // _minimizeBoxCheck
        _minimizeBoxCheck.Anchor = AnchorStyles.Left;
        _minimizeBoxCheck.AutoSize = true;
        _minimizeBoxCheck.Margin = new Padding(3);
        _minimizeBoxCheck.Text = Strings.CheckMinimizeBox;
        _minimizeBoxCheck.CheckedChanged += OnMinimizeBoxChanged;

        // _maximizeBoxCheck
        _maximizeBoxCheck.Anchor = AnchorStyles.Left;
        _maximizeBoxCheck.AutoSize = true;
        _maximizeBoxCheck.Margin = new Padding(3);
        _maximizeBoxCheck.Text = Strings.CheckMaximizeBox;
        _maximizeBoxCheck.CheckedChanged += OnMaximizeBoxChanged;

        // _showInTaskbarCheck
        _showInTaskbarCheck.Anchor = AnchorStyles.Left;
        _showInTaskbarCheck.AutoSize = true;
        _showInTaskbarCheck.Margin = new Padding(3);
        _showInTaskbarCheck.Text = Strings.CheckShowInTaskbar;
        _showInTaskbarCheck.CheckedChanged += OnShowInTaskbarChanged;

        // _topmostCheck
        _topmostCheck.Anchor = AnchorStyles.Left;
        _topmostCheck.AutoSize = true;
        _topmostCheck.Margin = new Padding(3);
        _topmostCheck.Text = Strings.CheckTopmost;
        _topmostCheck.CheckedChanged += OnTopmostChanged;

        _frameTabPage.Controls.Add(_frameTLP);
    }

    /// <summary>Window Content tab: image, sizing, color.</summary>
    void ConfigureContentTab()
    {
        // _contentTLP (outer 2-column grid for the tab)
        _contentTLP.ColumnCount = 2;
        _contentTLP.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _contentTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        _contentTLP.Dock = DockStyle.Fill;
        _contentTLP.Padding = new Padding(20, 20, 20, 15);
        _contentTLP.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _contentTLP.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _contentTLP.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _contentTLP.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _contentTLP.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _contentTLP.Controls.Add(_imageLabel, 0, 0);
        _contentTLP.Controls.Add(_imageTextBox, 1, 0);
        _contentTLP.Controls.Add(_imageButtonsFLP, 1, 1);
        _contentTLP.Controls.Add(_sizingLabel, 0, 2);
        _contentTLP.Controls.Add(_sizingRowTLP, 1, 2);
        _contentTLP.Controls.Add(_colorLabel, 0, 3);
        _contentTLP.Controls.Add(_colorValueLabel, 1, 3);
        _contentTLP.Controls.Add(_colorButtonsFLP, 1, 4);

        // _imageButtonsFLP (image Browse + Clear row)
        _imageButtonsFLP.Anchor = AnchorStyles.Left;
        _imageButtonsFLP.AutoSize = true;
        _imageButtonsFLP.FlowDirection = FlowDirection.LeftToRight;
        _imageButtonsFLP.Margin = new Padding(0, 0, 0, 12);
        _imageButtonsFLP.Controls.Add(_imageBrowseButton);
        _imageButtonsFLP.Controls.Add(_imageClearButton);

        // _sizingRowTLP (sizing combobox stretches; autosize button hugs the right)
        _sizingRowTLP.AutoSize = true;
        _sizingRowTLP.ColumnCount = 2;
        _sizingRowTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        _sizingRowTLP.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _sizingRowTLP.Dock = DockStyle.Fill;
        _sizingRowTLP.Margin = new Padding(0, 0, 0, 15);
        _sizingRowTLP.RowCount = 1;
        _sizingRowTLP.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _sizingRowTLP.Controls.Add(_imageSizingComboBox, 0, 0);
        _sizingRowTLP.Controls.Add(_autoSizeButton, 1, 0);

        // _colorButtonsFLP (color Browse + Random + transparent row)
        _colorButtonsFLP.Anchor = AnchorStyles.Left;
        _colorButtonsFLP.AutoSize = true;
        _colorButtonsFLP.FlowDirection = FlowDirection.LeftToRight;
        _colorButtonsFLP.Margin = new Padding(0);
        _colorButtonsFLP.Controls.Add(_colorBrowseButton);
        _colorButtonsFLP.Controls.Add(_colorRandomButton);
        _colorButtonsFLP.Controls.Add(_colorTransparentCheck);

        // _imageLabel
        _imageLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _imageLabel.AutoSize = true;
        _imageLabel.Margin = new Padding(3);
        _imageLabel.MinimumSize = new Size(80, 0);
        _imageLabel.Text = Strings.LabelImage;

        // _imageTextBox
        _imageTextBox.Dock = DockStyle.Fill;
        _imageTextBox.Margin = new Padding(3, 3, 3, 4);
        _imageTextBox.ReadOnly = true;
        _imageTextBox.Text = Strings.NoImage;

        // _imageBrowseButton
        _imageBrowseButton.AutoSize = true;
        _imageBrowseButton.Margin = new Padding(3);
        _imageBrowseButton.Text = Strings.ButtonBrowse;
        _imageBrowseButton.Click += OnImageBrowseClicked;

        // _imageClearButton
        _imageClearButton.AutoSize = true;
        _imageClearButton.Margin = new Padding(3);
        _imageClearButton.Text = Strings.ButtonImageClear;
        _imageClearButton.Click += OnImageClearClicked;

        // _sizingLabel
        _sizingLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _sizingLabel.AutoSize = true;
        _sizingLabel.Margin = new Padding(3);
        _sizingLabel.MinimumSize = new Size(80, 0);
        _sizingLabel.Text = Strings.LabelSizing;

        // _imageSizingComboBox
        _imageSizingComboBox.Dock = DockStyle.Fill;
        _imageSizingComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _imageSizingComboBox.Margin = new Padding(3);
        foreach (PictureBoxSizeMode mode in Enum.GetValues<PictureBoxSizeMode>())
            if (mode != PictureBoxSizeMode.AutoSize) _imageSizingComboBox.Items.Add(mode);
        _imageSizingComboBox.SelectedIndexChanged += OnImageSizingChanged;

        // _autoSizeButton
        _autoSizeButton.AutoSize = true;
        _autoSizeButton.Margin = new Padding(3);
        _autoSizeButton.Text = Strings.ButtonAutosize;
        _autoSizeButton.Click += OnAutoSizeClicked;

        // _colorLabel
        _colorLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _colorLabel.AutoSize = true;
        _colorLabel.Margin = new Padding(3);
        _colorLabel.MinimumSize = new Size(80, 0);
        _colorLabel.Text = Strings.LabelColor;

        // _colorValueLabel (color preview swatch)
        _colorValueLabel.AutoSize = false;
        _colorValueLabel.BackColor = Color.LightSlateGray;
        _colorValueLabel.BorderStyle = BorderStyle.FixedSingle;
        _colorValueLabel.Dock = DockStyle.Fill;
        _colorValueLabel.Height = 24;
        _colorValueLabel.Margin = new Padding(3, 3, 3, 4);
        _colorValueLabel.BackColorChanged += OnColorValueChanged;

        // _colorBrowseButton
        _colorBrowseButton.AutoSize = true;
        _colorBrowseButton.Margin = new Padding(3);
        _colorBrowseButton.Text = Strings.ButtonBrowse;
        _colorBrowseButton.Click += OnColorBrowseClicked;

        // _colorRandomButton
        _colorRandomButton.AutoSize = true;
        _colorRandomButton.Margin = new Padding(3);
        _colorRandomButton.Text = Strings.ButtonColorRandom;
        _colorRandomButton.Click += OnColorRandomClicked;

        // _colorTransparentCheck
        _colorTransparentCheck.Anchor = AnchorStyles.Left;
        _colorTransparentCheck.AutoSize = true;
        _colorTransparentCheck.Margin = new Padding(12, 6, 3, 3);
        _colorTransparentCheck.Text = Strings.CheckColorTransparent;
        _colorTransparentCheck.CheckedChanged += OnColorTransparentChanged;

        _contentTabPage.Controls.Add(_contentTLP);
    }

    /// <summary>Prank Mode tab: command, intro label, three prank checkboxes.</summary>
    void ConfigurePrankTab()
    {
        // _prankTLP (single-column stack for the tab)
        _prankTLP.ColumnCount = 1;
        _prankTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        _prankTLP.Dock = DockStyle.Fill;
        _prankTLP.Padding = new Padding(20, 20, 20, 15);
        _prankTLP.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _prankTLP.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _prankTLP.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _prankTLP.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _prankTLP.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _prankTLP.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _prankTLP.Controls.Add(_commandLabel, 0, 0);
        _prankTLP.Controls.Add(_commandTextBox, 0, 1);
        _prankTLP.Controls.Add(_prankIntroLabel, 0, 2);
        _prankTLP.Controls.Add(_prankNoRightClickCheck, 0, 3);
        _prankTLP.Controls.Add(_prankNoHotkeyCheck, 0, 4);
        _prankTLP.Controls.Add(_prankNoCloseCheck, 0, 5);

        // _commandLabel
        _commandLabel.Anchor = AnchorStyles.Left;
        _commandLabel.AutoSize = true;
        _commandLabel.Margin = new Padding(3, 3, 3, 0);
        _commandLabel.Text = Strings.LabelPrankCommand;

        // _commandTextBox
        _commandTextBox.Dock = DockStyle.Fill;
        _commandTextBox.Margin = new Padding(3, 3, 3, 12);
        _commandTextBox.TextChanged += OnCommandChanged;

        // _prankIntroLabel
        _prankIntroLabel.Anchor = AnchorStyles.Left;
        _prankIntroLabel.AutoSize = true;
        _prankIntroLabel.Margin = new Padding(3, 6, 3, 6);
        _prankIntroLabel.Text = Strings.LabelPrankIntro;

        // _prankNoRightClickCheck
        _prankNoRightClickCheck.Anchor = AnchorStyles.Left;
        _prankNoRightClickCheck.AutoSize = true;
        _prankNoRightClickCheck.Margin = new Padding(3, 3, 3, 6);
        _prankNoRightClickCheck.Text = Strings.CheckPrankNoRightClick;
        _prankNoRightClickCheck.CheckedChanged += OnPrankNoRightClickChanged;

        // _prankNoHotkeyCheck
        _prankNoHotkeyCheck.Anchor = AnchorStyles.Left;
        _prankNoHotkeyCheck.AutoSize = true;
        _prankNoHotkeyCheck.Margin = new Padding(3, 3, 3, 6);
        _prankNoHotkeyCheck.Text = Strings.CheckPrankNoHotkey;
        _prankNoHotkeyCheck.CheckedChanged += OnPrankNoHotkeyChanged;

        // _prankNoCloseCheck
        _prankNoCloseCheck.Anchor = AnchorStyles.Left;
        _prankNoCloseCheck.AutoSize = true;
        _prankNoCloseCheck.Margin = new Padding(3, 3, 3, 6);
        _prankNoCloseCheck.Text = Strings.CheckPrankNoClose;
        _prankNoCloseCheck.CheckedChanged += OnPrankNoCloseChanged;

        _prankTabPage.Controls.Add(_prankTLP);
    }

    /// <summary>About tab: app name, version, copyright, URL link, vertically stacked.</summary>
    void ConfigureAboutTab()
    {
        // _aboutFLP (TopDown stack for the tab)
        _aboutFLP.Dock = DockStyle.Fill;
        _aboutFLP.FlowDirection = FlowDirection.TopDown;
        _aboutFLP.Padding = new Padding(20);
        _aboutFLP.WrapContents = false;
        _aboutFLP.Controls.Add(_aboutNameLabel);
        _aboutFLP.Controls.Add(_aboutVersionLabel);
        _aboutFLP.Controls.Add(_aboutCopyrightLabel);
        _aboutFLP.Controls.Add(_aboutUrlLink);

        // _aboutNameLabel
        _aboutNameLabel.AutoSize = true;
        _aboutNameLabel.Font = new Font(Font.FontFamily, 12F, FontStyle.Bold);
        _aboutNameLabel.Margin = new Padding(3, 3, 3, 12);
        _aboutNameLabel.Text = Strings.AppName;

        // _aboutVersionLabel (text set in OnShown so we read the live ProductVersion)
        _aboutVersionLabel.AutoSize = true;
        _aboutVersionLabel.Margin = new Padding(3);

        // _aboutCopyrightLabel (text set in OnShown so we read the live current year)
        _aboutCopyrightLabel.AutoSize = true;
        _aboutCopyrightLabel.Margin = new Padding(3, 3, 3, 12);

        // _aboutUrlLink
        _aboutUrlLink.AutoSize = true;
        _aboutUrlLink.Margin = new Padding(3);
        _aboutUrlLink.Text = Strings.AboutUrl;
        _aboutUrlLink.LinkClicked += OnAboutUrlClicked;

        _aboutTabPage.Controls.Add(_aboutFLP);
    }
}
