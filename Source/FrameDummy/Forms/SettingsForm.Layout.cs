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

    // Layout panels (one FlowLayoutPanel per tab, matching v2's structure).
    FlowLayoutPanel _frameFLP = null!;
    FlowLayoutPanel _contentFLP = null!;
    FlowLayoutPanel _prankFLP = null!;
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

        _frameFLP = new FlowLayoutPanel();
        _contentFLP = new FlowLayoutPanel();
        _prankFLP = new FlowLayoutPanel();
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
        BackColor = Color.Gainsboro;
        ClientSize = new Size(623, 371);
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
        _frameTabPage.BackColor = Color.Transparent;
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

    /// <summary>Window Frame tab: title, icon, border, opacity, six frame-toggle checkboxes (FlowLayoutPanel layout, ported from v2).</summary>
    void ConfigureFrameTab()
    {
        // _frameFLP (FlowLayoutPanel hosting all frame-tab controls)
        _frameFLP.BackColor = Color.FromArgb(240, 240, 240);
        _frameFLP.Dock = DockStyle.Fill;
        _frameFLP.Padding = new Padding(20, 20, 20, 15);
        _frameFLP.Controls.Add(_titleLabel);
        _frameFLP.Controls.Add(_titleTextBox);
        _frameFLP.Controls.Add(_iconLabel);
        _frameFLP.Controls.Add(_iconTextBox);
        _frameFLP.Controls.Add(_iconBrowseButton);
        _frameFLP.Controls.Add(_iconDefaultButton);
        _frameFLP.Controls.Add(_borderLabel);
        _frameFLP.Controls.Add(_borderComboBox);
        _frameFLP.Controls.Add(_opacityLabel);
        _frameFLP.Controls.Add(_opacityTrackBar);
        _frameFLP.Controls.Add(_controlBoxCheck);
        _frameFLP.Controls.Add(_showIconCheck);
        _frameFLP.Controls.Add(_minimizeBoxCheck);
        _frameFLP.Controls.Add(_maximizeBoxCheck);
        _frameFLP.Controls.Add(_showInTaskbarCheck);
        _frameFLP.Controls.Add(_topmostCheck);
        _frameFLP.SetFlowBreak(_titleTextBox, true);
        _frameFLP.SetFlowBreak(_iconTextBox, true);
        _frameFLP.SetFlowBreak(_iconDefaultButton, true);
        _frameFLP.SetFlowBreak(_borderComboBox, true);
        _frameFLP.SetFlowBreak(_opacityTrackBar, true);
        _frameFLP.SetFlowBreak(_topmostCheck, true);

        // _titleLabel
        _titleLabel.AutoSize = true;
        _titleLabel.MinimumSize = new Size(80, 0);
        _titleLabel.Text = Strings.LabelTitle;

        // _titleTextBox
        _titleTextBox.Margin = new Padding(3, 0, 0, 12);
        _titleTextBox.Size = new Size(430, 22);
        _titleTextBox.TextChanged += OnTitleChanged;

        // _iconLabel
        _iconLabel.AutoSize = true;
        _iconLabel.MinimumSize = new Size(80, 0);
        _iconLabel.Text = Strings.LabelIcon;

        // _iconTextBox
        _iconTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _iconTextBox.Margin = new Padding(3, 0, 3, 4);
        _iconTextBox.ReadOnly = true;
        _iconTextBox.Size = new Size(430, 22);
        _iconTextBox.Text = Strings.DefaultIcon;

        // _iconBrowseButton
        _iconBrowseButton.AutoSize = true;
        _iconBrowseButton.Margin = new Padding(88, 0, 3, 12);
        _iconBrowseButton.Size = new Size(76, 27);
        _iconBrowseButton.Text = Strings.ButtonBrowse;
        _iconBrowseButton.UseVisualStyleBackColor = true;
        _iconBrowseButton.Click += OnIconBrowseClicked;

        // _iconDefaultButton
        _iconDefaultButton.AutoSize = true;
        _iconDefaultButton.Margin = new Padding(3, 0, 3, 12);
        _iconDefaultButton.Size = new Size(76, 27);
        _iconDefaultButton.Text = Strings.ButtonIconDefault;
        _iconDefaultButton.UseVisualStyleBackColor = true;
        _iconDefaultButton.Click += OnIconDefaultClicked;

        // _borderLabel
        _borderLabel.AutoSize = true;
        _borderLabel.MinimumSize = new Size(80, 0);
        _borderLabel.Text = Strings.LabelBorder;

        // _borderComboBox
        _borderComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _borderComboBox.FormattingEnabled = true;
        _borderComboBox.Margin = new Padding(3, 0, 0, 20);
        _borderComboBox.Size = new Size(430, 24);
        foreach (FormBorderStyle style in Enum.GetValues<FormBorderStyle>()) _borderComboBox.Items.Add(style);
        _borderComboBox.SelectedIndexChanged += OnBorderChanged;

        // _opacityLabel
        _opacityLabel.AutoSize = true;
        _opacityLabel.MinimumSize = new Size(75, 0);
        _opacityLabel.Text = string.Format(Strings.LabelOpacityFormat, 1.0);

        // _opacityTrackBar
        _opacityTrackBar.Margin = new Padding(0, 0, 0, 12);
        _opacityTrackBar.Maximum = 100;
        _opacityTrackBar.Minimum = 1;
        _opacityTrackBar.Size = new Size(438, 56);
        _opacityTrackBar.TickFrequency = 2;
        _opacityTrackBar.Value = 100;
        _opacityTrackBar.ValueChanged += OnOpacityChanged;

        // _controlBoxCheck
        _controlBoxCheck.AutoSize = true;
        _controlBoxCheck.MinimumSize = new Size(160, 0);
        _controlBoxCheck.Text = Strings.CheckControlBox;
        _controlBoxCheck.UseVisualStyleBackColor = true;
        _controlBoxCheck.CheckedChanged += OnControlBoxChanged;

        // _showIconCheck
        _showIconCheck.AutoSize = true;
        _showIconCheck.MinimumSize = new Size(160, 0);
        _showIconCheck.Text = Strings.CheckShowIcon;
        _showIconCheck.UseVisualStyleBackColor = true;
        _showIconCheck.CheckedChanged += OnShowIconChanged;

        // _minimizeBoxCheck
        _minimizeBoxCheck.AutoSize = true;
        _minimizeBoxCheck.MinimumSize = new Size(160, 0);
        _minimizeBoxCheck.Text = Strings.CheckMinimizeBox;
        _minimizeBoxCheck.UseVisualStyleBackColor = true;
        _minimizeBoxCheck.CheckedChanged += OnMinimizeBoxChanged;

        // _maximizeBoxCheck
        _maximizeBoxCheck.AutoSize = true;
        _maximizeBoxCheck.MinimumSize = new Size(160, 0);
        _maximizeBoxCheck.Text = Strings.CheckMaximizeBox;
        _maximizeBoxCheck.UseVisualStyleBackColor = true;
        _maximizeBoxCheck.CheckedChanged += OnMaximizeBoxChanged;

        // _showInTaskbarCheck
        _showInTaskbarCheck.AutoSize = true;
        _showInTaskbarCheck.MinimumSize = new Size(160, 0);
        _showInTaskbarCheck.Text = Strings.CheckShowInTaskbar;
        _showInTaskbarCheck.UseVisualStyleBackColor = true;
        _showInTaskbarCheck.CheckedChanged += OnShowInTaskbarChanged;

        // _topmostCheck
        _topmostCheck.AutoSize = true;
        _topmostCheck.MinimumSize = new Size(160, 0);
        _topmostCheck.Text = Strings.CheckTopmost;
        _topmostCheck.UseVisualStyleBackColor = true;
        _topmostCheck.CheckedChanged += OnTopmostChanged;

        _frameTabPage.Controls.Add(_frameFLP);
    }

    /// <summary>Window Content tab: image, sizing, color (FlowLayoutPanel layout, ported from v2).</summary>
    void ConfigureContentTab()
    {
        // _contentFLP (FlowLayoutPanel hosting all content-tab controls)
        _contentFLP.BackColor = Color.FromArgb(240, 240, 240);
        _contentFLP.Dock = DockStyle.Fill;
        _contentFLP.Padding = new Padding(20);
        _contentFLP.Controls.Add(_imageLabel);
        _contentFLP.Controls.Add(_imageTextBox);
        _contentFLP.Controls.Add(_imageBrowseButton);
        _contentFLP.Controls.Add(_imageClearButton);
        _contentFLP.Controls.Add(_sizingLabel);
        _contentFLP.Controls.Add(_imageSizingComboBox);
        _contentFLP.Controls.Add(_autoSizeButton);
        _contentFLP.Controls.Add(_colorLabel);
        _contentFLP.Controls.Add(_colorValueLabel);
        _contentFLP.Controls.Add(_colorBrowseButton);
        _contentFLP.Controls.Add(_colorRandomButton);
        _contentFLP.Controls.Add(_colorTransparentCheck);
        _contentFLP.SetFlowBreak(_imageTextBox, true);
        _contentFLP.SetFlowBreak(_imageClearButton, true);
        _contentFLP.SetFlowBreak(_autoSizeButton, true);
        _contentFLP.SetFlowBreak(_colorValueLabel, true);

        // _imageLabel
        _imageLabel.AutoSize = true;
        _imageLabel.MinimumSize = new Size(80, 0);
        _imageLabel.Text = Strings.LabelImage;

        // _imageTextBox
        _imageTextBox.Margin = new Padding(3, 0, 3, 4);
        _imageTextBox.ReadOnly = true;
        _imageTextBox.Size = new Size(429, 22);
        _imageTextBox.Text = Strings.NoImage;

        // _imageBrowseButton
        _imageBrowseButton.AutoSize = true;
        _imageBrowseButton.Margin = new Padding(88, 0, 3, 12);
        _imageBrowseButton.Size = new Size(76, 27);
        _imageBrowseButton.Text = Strings.ButtonBrowse;
        _imageBrowseButton.UseVisualStyleBackColor = true;
        _imageBrowseButton.Click += OnImageBrowseClicked;

        // _imageClearButton
        _imageClearButton.AutoSize = true;
        _imageClearButton.Margin = new Padding(3, 0, 3, 12);
        _imageClearButton.Size = new Size(87, 27);
        _imageClearButton.Text = Strings.ButtonImageClear;
        _imageClearButton.UseVisualStyleBackColor = true;
        _imageClearButton.Click += OnImageClearClicked;

        // _sizingLabel
        _sizingLabel.AutoSize = true;
        _sizingLabel.MinimumSize = new Size(80, 0);
        _sizingLabel.Text = Strings.LabelSizing;

        // _imageSizingComboBox
        _imageSizingComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _imageSizingComboBox.FormattingEnabled = true;
        _imageSizingComboBox.Margin = new Padding(3, 2, 3, 15);
        _imageSizingComboBox.Size = new Size(336, 24);
        foreach (PictureBoxSizeMode mode in Enum.GetValues<PictureBoxSizeMode>())
            if (mode != PictureBoxSizeMode.AutoSize) _imageSizingComboBox.Items.Add(mode);
        _imageSizingComboBox.SelectedIndexChanged += OnImageSizingChanged;

        // _autoSizeButton
        _autoSizeButton.AutoSize = true;
        _autoSizeButton.Margin = new Padding(3, 0, 3, 0);
        _autoSizeButton.Size = new Size(87, 27);
        _autoSizeButton.Text = Strings.ButtonAutosize;
        _autoSizeButton.UseVisualStyleBackColor = true;
        _autoSizeButton.Click += OnAutoSizeClicked;

        // _colorLabel
        _colorLabel.AutoSize = true;
        _colorLabel.MinimumSize = new Size(80, 0);
        _colorLabel.Text = Strings.LabelColor;

        // _colorValueLabel (color preview swatch)
        _colorValueLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _colorValueLabel.BackColor = Color.LightSlateGray;
        _colorValueLabel.Margin = new Padding(3, 0, 3, 4);
        _colorValueLabel.Size = new Size(429, 22);
        _colorValueLabel.BackColorChanged += OnColorValueChanged;

        // _colorBrowseButton
        _colorBrowseButton.AutoSize = true;
        _colorBrowseButton.Margin = new Padding(88, 0, 3, 0);
        _colorBrowseButton.Size = new Size(76, 27);
        _colorBrowseButton.Text = Strings.ButtonBrowse;
        _colorBrowseButton.UseVisualStyleBackColor = true;
        _colorBrowseButton.Click += OnColorBrowseClicked;

        // _colorRandomButton
        _colorRandomButton.AutoSize = true;
        _colorRandomButton.Margin = new Padding(3, 0, 3, 0);
        _colorRandomButton.Size = new Size(87, 27);
        _colorRandomButton.Text = Strings.ButtonColorRandom;
        _colorRandomButton.UseVisualStyleBackColor = true;
        _colorRandomButton.Click += OnColorRandomClicked;

        // _colorTransparentCheck
        _colorTransparentCheck.AutoSize = true;
        _colorTransparentCheck.Margin = new Padding(6, 4, 3, 0);
        _colorTransparentCheck.Text = Strings.CheckColorTransparent;
        _colorTransparentCheck.UseVisualStyleBackColor = true;
        _colorTransparentCheck.CheckedChanged += OnColorTransparentChanged;

        _contentTabPage.Controls.Add(_contentFLP);
    }

    /// <summary>Prank Mode tab: command, intro label, three prank checkboxes (FlowLayoutPanel layout, ported from v2).</summary>
    void ConfigurePrankTab()
    {
        // _prankFLP (FlowLayoutPanel hosting all prank-tab controls)
        _prankFLP.BackColor = Color.FromArgb(240, 240, 240);
        _prankFLP.Dock = DockStyle.Fill;
        _prankFLP.Padding = new Padding(20);
        _prankFLP.Controls.Add(_commandLabel);
        _prankFLP.Controls.Add(_commandTextBox);
        _prankFLP.Controls.Add(_prankIntroLabel);
        _prankFLP.Controls.Add(_prankNoRightClickCheck);
        _prankFLP.Controls.Add(_prankNoHotkeyCheck);
        _prankFLP.Controls.Add(_prankNoCloseCheck);
        _prankFLP.SetFlowBreak(_commandLabel, true);
        _prankFLP.SetFlowBreak(_commandTextBox, true);
        _prankFLP.SetFlowBreak(_prankIntroLabel, true);
        _prankFLP.SetFlowBreak(_prankNoRightClickCheck, true);
        _prankFLP.SetFlowBreak(_prankNoHotkeyCheck, true);

        // _commandLabel
        _commandLabel.AutoSize = true;
        _commandLabel.Margin = new Padding(0);
        _commandLabel.Text = Strings.LabelPrankCommand;

        // _commandTextBox
        _commandTextBox.Margin = new Padding(3, 3, 0, 15);
        _commandTextBox.Size = new Size(516, 22);
        _commandTextBox.TextChanged += OnCommandChanged;

        // _prankIntroLabel
        _prankIntroLabel.AutoSize = true;
        _prankIntroLabel.Margin = new Padding(0, 0, 0, 15);
        _prankIntroLabel.Text = Strings.LabelPrankIntro;

        // _prankNoRightClickCheck
        _prankNoRightClickCheck.AutoSize = true;
        _prankNoRightClickCheck.Text = Strings.CheckPrankNoRightClick;
        _prankNoRightClickCheck.UseVisualStyleBackColor = true;
        _prankNoRightClickCheck.CheckedChanged += OnPrankNoRightClickChanged;

        // _prankNoHotkeyCheck
        _prankNoHotkeyCheck.AutoSize = true;
        _prankNoHotkeyCheck.Text = Strings.CheckPrankNoHotkey;
        _prankNoHotkeyCheck.UseVisualStyleBackColor = true;
        _prankNoHotkeyCheck.CheckedChanged += OnPrankNoHotkeyChanged;

        // _prankNoCloseCheck
        _prankNoCloseCheck.AutoSize = true;
        _prankNoCloseCheck.Text = Strings.CheckPrankNoClose;
        _prankNoCloseCheck.UseVisualStyleBackColor = true;
        _prankNoCloseCheck.CheckedChanged += OnPrankNoCloseChanged;

        _prankTabPage.Controls.Add(_prankFLP);
    }

    /// <summary>About tab: app name, version, copyright, URL link, vertically stacked (FlowLayoutPanel TopDown, ported from v2).</summary>
    void ConfigureAboutTab()
    {
        // _aboutFLP (TopDown stack for the tab)
        _aboutFLP.BackColor = Color.FromArgb(240, 240, 240);
        _aboutFLP.Dock = DockStyle.Fill;
        _aboutFLP.FlowDirection = FlowDirection.TopDown;
        _aboutFLP.Padding = new Padding(20);
        _aboutFLP.Controls.Add(_aboutNameLabel);
        _aboutFLP.Controls.Add(_aboutVersionLabel);
        _aboutFLP.Controls.Add(_aboutCopyrightLabel);
        _aboutFLP.Controls.Add(_aboutUrlLink);

        // _aboutNameLabel
        _aboutNameLabel.AutoSize = true;
        _aboutNameLabel.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
        _aboutNameLabel.Margin = new Padding(3, 0, 3, 10);
        _aboutNameLabel.Text = Strings.AppName;

        // _aboutVersionLabel (text set in OnShown so we read the live ProductVersion)
        _aboutVersionLabel.AutoSize = true;
        _aboutVersionLabel.Margin = new Padding(3, 0, 3, 3);

        // _aboutCopyrightLabel (text set in OnShown so we read the live current year)
        _aboutCopyrightLabel.AutoSize = true;
        _aboutCopyrightLabel.Margin = new Padding(3, 0, 3, 10);

        // _aboutUrlLink
        _aboutUrlLink.AutoSize = true;
        _aboutUrlLink.Margin = new Padding(3);
        _aboutUrlLink.TabStop = true;
        _aboutUrlLink.Text = Strings.AboutUrl;
        _aboutUrlLink.LinkClicked += OnAboutUrlClicked;

        _aboutTabPage.Controls.Add(_aboutFLP);
    }
}
