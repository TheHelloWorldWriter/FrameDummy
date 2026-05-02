namespace FrameDummy
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.frameFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.iconLabel = new System.Windows.Forms.Label();
            this.iconTextBox = new System.Windows.Forms.TextBox();
            this.iconBrowseButton = new System.Windows.Forms.Button();
            this.iconDefaultButton = new System.Windows.Forms.Button();
            this.borderLabel = new System.Windows.Forms.Label();
            this.borderComboBox = new System.Windows.Forms.ComboBox();
            this.opacityLabel = new System.Windows.Forms.Label();
            this.opacityTrackBar = new System.Windows.Forms.TrackBar();
            this.controlCheckBox = new System.Windows.Forms.CheckBox();
            this.iconCheckBox = new System.Windows.Forms.CheckBox();
            this.minimizeCheckBox = new System.Windows.Forms.CheckBox();
            this.maximizeCheckBox = new System.Windows.Forms.CheckBox();
            this.taskbarCheckBox = new System.Windows.Forms.CheckBox();
            this.topmostCheckBox = new System.Windows.Forms.CheckBox();
            this.imageLabel = new System.Windows.Forms.Label();
            this.imageTextBox = new System.Windows.Forms.TextBox();
            this.imageBrowseButton = new System.Windows.Forms.Button();
            this.imageClearButton = new System.Windows.Forms.Button();
            this.layoutLabel = new System.Windows.Forms.Label();
            this.imageSizingComboBox = new System.Windows.Forms.ComboBox();
            this.autoSizeButton = new System.Windows.Forms.Button();
            this.colorLabel = new System.Windows.Forms.Label();
            this.colorValueLabel = new System.Windows.Forms.Label();
            this.colorBrowseButton = new System.Windows.Forms.Button();
            this.openIconDialog = new System.Windows.Forms.OpenFileDialog();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.contentFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.colorRandomButton = new System.Windows.Forms.Button();
            this.colorTransparentCheckBox = new System.Windows.Forms.CheckBox();
            this.settingsTabControl = new System.Windows.Forms.TabControl();
            this.frameTabPage = new System.Windows.Forms.TabPage();
            this.contentTabPage = new System.Windows.Forms.TabPage();
            this.prankTabPage = new System.Windows.Forms.TabPage();
            this.prankFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.commandLabel = new System.Windows.Forms.Label();
            this.commandTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.prankNoSettingsRightClickCheckBox = new System.Windows.Forms.CheckBox();
            this.prankNoSettingsHotkeyCheckBox = new System.Windows.Forms.CheckBox();
            this.prankNoCloseCheckBox = new System.Windows.Forms.CheckBox();
            this.aboutTabPage = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.aboutNameLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.copyrightLabel = new System.Windows.Forms.Label();
            this.urlLinkLabel = new System.Windows.Forms.LinkLabel();
            this.frameFlowLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opacityTrackBar)).BeginInit();
            this.contentFlowLayoutPanel.SuspendLayout();
            this.settingsTabControl.SuspendLayout();
            this.frameTabPage.SuspendLayout();
            this.contentTabPage.SuspendLayout();
            this.prankTabPage.SuspendLayout();
            this.prankFlowLayoutPanel.SuspendLayout();
            this.aboutTabPage.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            //
            // frameFlowLayoutPanel
            //
            this.frameFlowLayoutPanel.BackColor = System.Drawing.SystemColors.Control;
            this.frameFlowLayoutPanel.Controls.Add(this.titleLabel);
            this.frameFlowLayoutPanel.Controls.Add(this.titleTextBox);
            this.frameFlowLayoutPanel.Controls.Add(this.iconLabel);
            this.frameFlowLayoutPanel.Controls.Add(this.iconTextBox);
            this.frameFlowLayoutPanel.Controls.Add(this.iconBrowseButton);
            this.frameFlowLayoutPanel.Controls.Add(this.iconDefaultButton);
            this.frameFlowLayoutPanel.Controls.Add(this.borderLabel);
            this.frameFlowLayoutPanel.Controls.Add(this.borderComboBox);
            this.frameFlowLayoutPanel.Controls.Add(this.opacityLabel);
            this.frameFlowLayoutPanel.Controls.Add(this.opacityTrackBar);
            this.frameFlowLayoutPanel.Controls.Add(this.controlCheckBox);
            this.frameFlowLayoutPanel.Controls.Add(this.iconCheckBox);
            this.frameFlowLayoutPanel.Controls.Add(this.minimizeCheckBox);
            this.frameFlowLayoutPanel.Controls.Add(this.maximizeCheckBox);
            this.frameFlowLayoutPanel.Controls.Add(this.taskbarCheckBox);
            this.frameFlowLayoutPanel.Controls.Add(this.topmostCheckBox);
            this.frameFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frameFlowLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.frameFlowLayoutPanel.Name = "frameFlowLayoutPanel";
            this.frameFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(20, 20, 20, 15);
            this.frameFlowLayoutPanel.Size = new System.Drawing.Size(569, 296);
            this.frameFlowLayoutPanel.TabIndex = 0;
            //
            // titleLabel
            //
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(23, 20);
            this.titleLabel.MinimumSize = new System.Drawing.Size(80, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(80, 17);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Title:";
            //
            // titleTextBox
            //
            this.frameFlowLayoutPanel.SetFlowBreak(this.titleTextBox, true);
            this.titleTextBox.Location = new System.Drawing.Point(109, 20);
            this.titleTextBox.Margin = new System.Windows.Forms.Padding(3, 0, 0, 12);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(430, 22);
            this.titleTextBox.TabIndex = 1;
            this.titleTextBox.TextChanged += new System.EventHandler(this.EventTitleTextBoxTextChanged);
            //
            // iconLabel
            //
            this.iconLabel.AutoSize = true;
            this.iconLabel.Location = new System.Drawing.Point(23, 54);
            this.iconLabel.MinimumSize = new System.Drawing.Size(80, 0);
            this.iconLabel.Name = "iconLabel";
            this.iconLabel.Size = new System.Drawing.Size(80, 17);
            this.iconLabel.TabIndex = 2;
            this.iconLabel.Text = "Icon:";
            //
            // iconTextBox
            //
            this.iconTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.frameFlowLayoutPanel.SetFlowBreak(this.iconTextBox, true);
            this.iconTextBox.Location = new System.Drawing.Point(109, 54);
            this.iconTextBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 4);
            this.iconTextBox.Name = "iconTextBox";
            this.iconTextBox.ReadOnly = true;
            this.iconTextBox.Size = new System.Drawing.Size(430, 22);
            this.iconTextBox.TabIndex = 3;
            this.iconTextBox.Text = "(default icon)";
            //
            // iconBrowseButton
            //
            this.iconBrowseButton.AutoSize = true;
            this.iconBrowseButton.Location = new System.Drawing.Point(108, 80);
            this.iconBrowseButton.Margin = new System.Windows.Forms.Padding(88, 0, 3, 12);
            this.iconBrowseButton.Name = "iconBrowseButton";
            this.iconBrowseButton.Size = new System.Drawing.Size(76, 27);
            this.iconBrowseButton.TabIndex = 4;
            this.iconBrowseButton.Text = "Browse...";
            this.iconBrowseButton.UseVisualStyleBackColor = true;
            this.iconBrowseButton.Click += new System.EventHandler(this.EventIconBrowseButtonClick);
            //
            // iconDefaultButton
            //
            this.iconDefaultButton.AutoSize = true;
            this.frameFlowLayoutPanel.SetFlowBreak(this.iconDefaultButton, true);
            this.iconDefaultButton.Location = new System.Drawing.Point(190, 80);
            this.iconDefaultButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 12);
            this.iconDefaultButton.Name = "iconDefaultButton";
            this.iconDefaultButton.Size = new System.Drawing.Size(76, 27);
            this.iconDefaultButton.TabIndex = 15;
            this.iconDefaultButton.Text = "Default";
            this.iconDefaultButton.UseVisualStyleBackColor = true;
            this.iconDefaultButton.Click += new System.EventHandler(this.EventIconDefaultButtonClick);
            //
            // borderLabel
            //
            this.borderLabel.AutoSize = true;
            this.borderLabel.Location = new System.Drawing.Point(23, 119);
            this.borderLabel.MinimumSize = new System.Drawing.Size(80, 0);
            this.borderLabel.Name = "borderLabel";
            this.borderLabel.Size = new System.Drawing.Size(80, 17);
            this.borderLabel.TabIndex = 5;
            this.borderLabel.Text = "Border:";
            //
            // borderComboBox
            //
            this.borderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.frameFlowLayoutPanel.SetFlowBreak(this.borderComboBox, true);
            this.borderComboBox.FormattingEnabled = true;
            this.borderComboBox.Location = new System.Drawing.Point(109, 119);
            this.borderComboBox.Margin = new System.Windows.Forms.Padding(3, 0, 0, 20);
            this.borderComboBox.Name = "borderComboBox";
            this.borderComboBox.Size = new System.Drawing.Size(430, 24);
            this.borderComboBox.TabIndex = 6;
            this.borderComboBox.SelectedIndexChanged += new System.EventHandler(this.EventBorderComboBoxSelectedIndexChanged);
            //
            // opacityLabel
            //
            this.opacityLabel.AutoSize = true;
            this.opacityLabel.Location = new System.Drawing.Point(23, 163);
            this.opacityLabel.MinimumSize = new System.Drawing.Size(75, 0);
            this.opacityLabel.Name = "opacityLabel";
            this.opacityLabel.Size = new System.Drawing.Size(75, 34);
            this.opacityLabel.TabIndex = 7;
            this.opacityLabel.Text = "Opacity: \r\n100%";
            //
            // opacityTrackBar
            //
            this.frameFlowLayoutPanel.SetFlowBreak(this.opacityTrackBar, true);
            this.opacityTrackBar.Location = new System.Drawing.Point(101, 163);
            this.opacityTrackBar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.opacityTrackBar.Maximum = 100;
            this.opacityTrackBar.Minimum = 1;
            this.opacityTrackBar.Name = "opacityTrackBar";
            this.opacityTrackBar.Size = new System.Drawing.Size(438, 56);
            this.opacityTrackBar.TabIndex = 8;
            this.opacityTrackBar.TickFrequency = 2;
            this.opacityTrackBar.Value = 100;
            this.opacityTrackBar.ValueChanged += new System.EventHandler(this.EventOpacityTrackBarValueChanged);
            //
            // controlCheckBox
            //
            this.controlCheckBox.AutoSize = true;
            this.controlCheckBox.Checked = true;
            this.controlCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.controlCheckBox.Location = new System.Drawing.Point(23, 234);
            this.controlCheckBox.MinimumSize = new System.Drawing.Size(160, 0);
            this.controlCheckBox.Name = "controlCheckBox";
            this.controlCheckBox.Size = new System.Drawing.Size(160, 21);
            this.controlCheckBox.TabIndex = 9;
            this.controlCheckBox.Text = "Control Box";
            this.controlCheckBox.UseVisualStyleBackColor = true;
            this.controlCheckBox.CheckedChanged += new System.EventHandler(this.EventFrameCheckBoxesCheckedChanged);
            //
            // iconCheckBox
            //
            this.iconCheckBox.AutoSize = true;
            this.iconCheckBox.Checked = true;
            this.iconCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.iconCheckBox.Location = new System.Drawing.Point(189, 234);
            this.iconCheckBox.MinimumSize = new System.Drawing.Size(160, 0);
            this.iconCheckBox.Name = "iconCheckBox";
            this.iconCheckBox.Size = new System.Drawing.Size(160, 21);
            this.iconCheckBox.TabIndex = 10;
            this.iconCheckBox.Text = "Show Icon";
            this.iconCheckBox.UseVisualStyleBackColor = true;
            this.iconCheckBox.CheckedChanged += new System.EventHandler(this.EventFrameCheckBoxesCheckedChanged);
            //
            // minimizeCheckBox
            //
            this.minimizeCheckBox.AutoSize = true;
            this.minimizeCheckBox.Checked = true;
            this.minimizeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.minimizeCheckBox.Location = new System.Drawing.Point(355, 234);
            this.minimizeCheckBox.MinimumSize = new System.Drawing.Size(160, 0);
            this.minimizeCheckBox.Name = "minimizeCheckBox";
            this.minimizeCheckBox.Size = new System.Drawing.Size(160, 21);
            this.minimizeCheckBox.TabIndex = 11;
            this.minimizeCheckBox.Text = "Minimize Box";
            this.minimizeCheckBox.UseVisualStyleBackColor = true;
            this.minimizeCheckBox.CheckedChanged += new System.EventHandler(this.EventFrameCheckBoxesCheckedChanged);
            //
            // maximizeCheckBox
            //
            this.maximizeCheckBox.AutoSize = true;
            this.maximizeCheckBox.Checked = true;
            this.maximizeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.maximizeCheckBox.Location = new System.Drawing.Point(23, 261);
            this.maximizeCheckBox.MinimumSize = new System.Drawing.Size(160, 0);
            this.maximizeCheckBox.Name = "maximizeCheckBox";
            this.maximizeCheckBox.Size = new System.Drawing.Size(160, 21);
            this.maximizeCheckBox.TabIndex = 12;
            this.maximizeCheckBox.Text = "Maximize Box";
            this.maximizeCheckBox.UseVisualStyleBackColor = true;
            this.maximizeCheckBox.CheckedChanged += new System.EventHandler(this.EventFrameCheckBoxesCheckedChanged);
            //
            // taskbarCheckBox
            //
            this.taskbarCheckBox.AutoSize = true;
            this.taskbarCheckBox.Checked = true;
            this.taskbarCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.taskbarCheckBox.Location = new System.Drawing.Point(189, 261);
            this.taskbarCheckBox.MinimumSize = new System.Drawing.Size(160, 0);
            this.taskbarCheckBox.Name = "taskbarCheckBox";
            this.taskbarCheckBox.Size = new System.Drawing.Size(160, 21);
            this.taskbarCheckBox.TabIndex = 13;
            this.taskbarCheckBox.Text = "Show In Taskbar";
            this.taskbarCheckBox.UseVisualStyleBackColor = true;
            this.taskbarCheckBox.CheckedChanged += new System.EventHandler(this.EventFrameCheckBoxesCheckedChanged);
            //
            // topmostCheckBox
            //
            this.topmostCheckBox.AutoSize = true;
            this.topmostCheckBox.Checked = true;
            this.topmostCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.frameFlowLayoutPanel.SetFlowBreak(this.topmostCheckBox, true);
            this.topmostCheckBox.Location = new System.Drawing.Point(355, 261);
            this.topmostCheckBox.MinimumSize = new System.Drawing.Size(160, 0);
            this.topmostCheckBox.Name = "topmostCheckBox";
            this.topmostCheckBox.Size = new System.Drawing.Size(160, 21);
            this.topmostCheckBox.TabIndex = 14;
            this.topmostCheckBox.Text = "Topmost";
            this.topmostCheckBox.UseVisualStyleBackColor = true;
            this.topmostCheckBox.CheckedChanged += new System.EventHandler(this.EventFrameCheckBoxesCheckedChanged);
            //
            // imageLabel
            //
            this.imageLabel.AutoSize = true;
            this.imageLabel.Location = new System.Drawing.Point(23, 20);
            this.imageLabel.MinimumSize = new System.Drawing.Size(80, 0);
            this.imageLabel.Name = "imageLabel";
            this.imageLabel.Size = new System.Drawing.Size(80, 17);
            this.imageLabel.TabIndex = 0;
            this.imageLabel.Text = "Image:";
            //
            // imageTextBox
            //
            this.contentFlowLayoutPanel.SetFlowBreak(this.imageTextBox, true);
            this.imageTextBox.Location = new System.Drawing.Point(109, 20);
            this.imageTextBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 4);
            this.imageTextBox.Name = "imageTextBox";
            this.imageTextBox.ReadOnly = true;
            this.imageTextBox.Size = new System.Drawing.Size(429, 22);
            this.imageTextBox.TabIndex = 1;
            this.imageTextBox.Text = "(no image)";
            //
            // imageBrowseButton
            //
            this.imageBrowseButton.AutoSize = true;
            this.imageBrowseButton.Location = new System.Drawing.Point(108, 46);
            this.imageBrowseButton.Margin = new System.Windows.Forms.Padding(88, 0, 3, 12);
            this.imageBrowseButton.Name = "imageBrowseButton";
            this.imageBrowseButton.Size = new System.Drawing.Size(76, 27);
            this.imageBrowseButton.TabIndex = 2;
            this.imageBrowseButton.Text = "Browse...";
            this.imageBrowseButton.UseVisualStyleBackColor = true;
            this.imageBrowseButton.Click += new System.EventHandler(this.EventImageBrowseButtonClick);
            //
            // imageClearButton
            //
            this.imageClearButton.AutoSize = true;
            this.contentFlowLayoutPanel.SetFlowBreak(this.imageClearButton, true);
            this.imageClearButton.Location = new System.Drawing.Point(190, 46);
            this.imageClearButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 12);
            this.imageClearButton.Name = "imageClearButton";
            this.imageClearButton.Size = new System.Drawing.Size(87, 27);
            this.imageClearButton.TabIndex = 3;
            this.imageClearButton.Text = "Clear";
            this.imageClearButton.UseVisualStyleBackColor = true;
            this.imageClearButton.Click += new System.EventHandler(this.EventImageClearButtonClick);
            //
            // layoutLabel
            //
            this.layoutLabel.AutoSize = true;
            this.layoutLabel.Location = new System.Drawing.Point(23, 85);
            this.layoutLabel.MinimumSize = new System.Drawing.Size(80, 0);
            this.layoutLabel.Name = "layoutLabel";
            this.layoutLabel.Size = new System.Drawing.Size(80, 17);
            this.layoutLabel.TabIndex = 4;
            this.layoutLabel.Text = "Sizing:";
            //
            // imageSizingComboBox
            //
            this.imageSizingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imageSizingComboBox.FormattingEnabled = true;
            this.imageSizingComboBox.Location = new System.Drawing.Point(109, 87);
            this.imageSizingComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 15);
            this.imageSizingComboBox.Name = "imageSizingComboBox";
            this.imageSizingComboBox.Size = new System.Drawing.Size(336, 24);
            this.imageSizingComboBox.TabIndex = 5;
            this.imageSizingComboBox.SelectedIndexChanged += new System.EventHandler(this.EventImageSizingComboBoxSelectedIndexChanged);
            //
            // autoSizeButton
            //
            this.autoSizeButton.AutoSize = true;
            this.contentFlowLayoutPanel.SetFlowBreak(this.autoSizeButton, true);
            this.autoSizeButton.Location = new System.Drawing.Point(451, 85);
            this.autoSizeButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.autoSizeButton.Name = "autoSizeButton";
            this.autoSizeButton.Size = new System.Drawing.Size(87, 27);
            this.autoSizeButton.TabIndex = 6;
            this.autoSizeButton.Text = "&Autosize";
            this.autoSizeButton.UseVisualStyleBackColor = true;
            this.autoSizeButton.Click += new System.EventHandler(this.EventAutoSizeButtonClick);
            //
            // colorLabel
            //
            this.colorLabel.AutoSize = true;
            this.colorLabel.Location = new System.Drawing.Point(23, 126);
            this.colorLabel.MinimumSize = new System.Drawing.Size(80, 0);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(80, 17);
            this.colorLabel.TabIndex = 7;
            this.colorLabel.Text = "Color:";
            //
            // colorValueLabel
            //
            this.colorValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorValueLabel.BackColor = System.Drawing.Color.LightSlateGray;
            this.contentFlowLayoutPanel.SetFlowBreak(this.colorValueLabel, true);
            this.colorValueLabel.Location = new System.Drawing.Point(109, 126);
            this.colorValueLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 4);
            this.colorValueLabel.Name = "colorValueLabel";
            this.colorValueLabel.Size = new System.Drawing.Size(429, 22);
            this.colorValueLabel.TabIndex = 8;
            this.colorValueLabel.BackColorChanged += new System.EventHandler(this.EventColorValueLabelBackColorChanged);
            //
            // colorBrowseButton
            //
            this.colorBrowseButton.AutoSize = true;
            this.colorBrowseButton.Location = new System.Drawing.Point(108, 152);
            this.colorBrowseButton.Margin = new System.Windows.Forms.Padding(88, 0, 3, 0);
            this.colorBrowseButton.Name = "colorBrowseButton";
            this.colorBrowseButton.Size = new System.Drawing.Size(76, 27);
            this.colorBrowseButton.TabIndex = 9;
            this.colorBrowseButton.Text = "Browse...";
            this.colorBrowseButton.UseVisualStyleBackColor = true;
            this.colorBrowseButton.Click += new System.EventHandler(this.EventColorBrowseButtonClick);
            //
            // openIconDialog
            //
            this.openIconDialog.Filter = resources.GetString("openIconDialog.Filter");
            this.openIconDialog.Title = "Select Icon";
            //
            // openImageDialog
            //
            this.openImageDialog.Filter = resources.GetString("openImageDialog.Filter");
            this.openImageDialog.Title = "Select Image";
            //
            // contentFlowLayoutPanel
            //
            this.contentFlowLayoutPanel.BackColor = System.Drawing.SystemColors.Control;
            this.contentFlowLayoutPanel.Controls.Add(this.imageLabel);
            this.contentFlowLayoutPanel.Controls.Add(this.imageTextBox);
            this.contentFlowLayoutPanel.Controls.Add(this.imageBrowseButton);
            this.contentFlowLayoutPanel.Controls.Add(this.imageClearButton);
            this.contentFlowLayoutPanel.Controls.Add(this.layoutLabel);
            this.contentFlowLayoutPanel.Controls.Add(this.imageSizingComboBox);
            this.contentFlowLayoutPanel.Controls.Add(this.autoSizeButton);
            this.contentFlowLayoutPanel.Controls.Add(this.colorLabel);
            this.contentFlowLayoutPanel.Controls.Add(this.colorValueLabel);
            this.contentFlowLayoutPanel.Controls.Add(this.colorBrowseButton);
            this.contentFlowLayoutPanel.Controls.Add(this.colorRandomButton);
            this.contentFlowLayoutPanel.Controls.Add(this.colorTransparentCheckBox);
            this.contentFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentFlowLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.contentFlowLayoutPanel.Name = "contentFlowLayoutPanel";
            this.contentFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(20);
            this.contentFlowLayoutPanel.Size = new System.Drawing.Size(569, 296);
            this.contentFlowLayoutPanel.TabIndex = 1;
            //
            // colorRandomButton
            //
            this.colorRandomButton.AutoSize = true;
            this.colorRandomButton.Location = new System.Drawing.Point(190, 152);
            this.colorRandomButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.colorRandomButton.Name = "colorRandomButton";
            this.colorRandomButton.Size = new System.Drawing.Size(87, 27);
            this.colorRandomButton.TabIndex = 10;
            this.colorRandomButton.Text = "Random";
            this.colorRandomButton.UseVisualStyleBackColor = true;
            this.colorRandomButton.Click += new System.EventHandler(this.EventColorRandomButtonClick);
            //
            // colorTransparentCheckBox
            //
            this.colorTransparentCheckBox.AutoSize = true;
            this.colorTransparentCheckBox.Checked = true;
            this.colorTransparentCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.colorTransparentCheckBox.Location = new System.Drawing.Point(286, 156);
            this.colorTransparentCheckBox.Margin = new System.Windows.Forms.Padding(6, 4, 3, 0);
            this.colorTransparentCheckBox.Name = "colorTransparentCheckBox";
            this.colorTransparentCheckBox.Size = new System.Drawing.Size(154, 21);
            this.colorTransparentCheckBox.TabIndex = 11;
            this.colorTransparentCheckBox.Text = "Color is transparent";
            this.colorTransparentCheckBox.UseVisualStyleBackColor = true;
            this.colorTransparentCheckBox.CheckedChanged += new System.EventHandler(this.EventColorTransparentCheckBoxCheckedChanged);
            //
            // settingsTabControl
            //
            this.settingsTabControl.Controls.Add(this.frameTabPage);
            this.settingsTabControl.Controls.Add(this.contentTabPage);
            this.settingsTabControl.Controls.Add(this.prankTabPage);
            this.settingsTabControl.Controls.Add(this.aboutTabPage);
            this.settingsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsTabControl.Location = new System.Drawing.Point(20, 20);
            this.settingsTabControl.Name = "settingsTabControl";
            this.settingsTabControl.SelectedIndex = 0;
            this.settingsTabControl.Size = new System.Drawing.Size(583, 331);
            this.settingsTabControl.TabIndex = 3;
            //
            // frameTabPage
            //
            this.frameTabPage.BackColor = System.Drawing.Color.Transparent;
            this.frameTabPage.Controls.Add(this.frameFlowLayoutPanel);
            this.frameTabPage.Location = new System.Drawing.Point(4, 25);
            this.frameTabPage.Name = "frameTabPage";
            this.frameTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.frameTabPage.Size = new System.Drawing.Size(575, 302);
            this.frameTabPage.TabIndex = 0;
            this.frameTabPage.Text = "Window Frame";
            this.frameTabPage.UseVisualStyleBackColor = true;
            //
            // contentTabPage
            //
            this.contentTabPage.Controls.Add(this.contentFlowLayoutPanel);
            this.contentTabPage.Location = new System.Drawing.Point(4, 25);
            this.contentTabPage.Name = "contentTabPage";
            this.contentTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.contentTabPage.Size = new System.Drawing.Size(575, 302);
            this.contentTabPage.TabIndex = 1;
            this.contentTabPage.Text = "Window Content";
            this.contentTabPage.UseVisualStyleBackColor = true;
            //
            // prankTabPage
            //
            this.prankTabPage.Controls.Add(this.prankFlowLayoutPanel);
            this.prankTabPage.Location = new System.Drawing.Point(4, 25);
            this.prankTabPage.Name = "prankTabPage";
            this.prankTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.prankTabPage.Size = new System.Drawing.Size(575, 302);
            this.prankTabPage.TabIndex = 2;
            this.prankTabPage.Text = "Prank Mode";
            this.prankTabPage.UseVisualStyleBackColor = true;
            //
            // prankFlowLayoutPanel
            //
            this.prankFlowLayoutPanel.BackColor = System.Drawing.SystemColors.Control;
            this.prankFlowLayoutPanel.Controls.Add(this.commandLabel);
            this.prankFlowLayoutPanel.Controls.Add(this.commandTextBox);
            this.prankFlowLayoutPanel.Controls.Add(this.label1);
            this.prankFlowLayoutPanel.Controls.Add(this.prankNoSettingsRightClickCheckBox);
            this.prankFlowLayoutPanel.Controls.Add(this.prankNoSettingsHotkeyCheckBox);
            this.prankFlowLayoutPanel.Controls.Add(this.prankNoCloseCheckBox);
            this.prankFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prankFlowLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.prankFlowLayoutPanel.Name = "prankFlowLayoutPanel";
            this.prankFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(20);
            this.prankFlowLayoutPanel.Size = new System.Drawing.Size(569, 296);
            this.prankFlowLayoutPanel.TabIndex = 0;
            //
            // commandLabel
            //
            this.commandLabel.AutoSize = true;
            this.prankFlowLayoutPanel.SetFlowBreak(this.commandLabel, true);
            this.commandLabel.Location = new System.Drawing.Point(20, 20);
            this.commandLabel.Margin = new System.Windows.Forms.Padding(0);
            this.commandLabel.Name = "commandLabel";
            this.commandLabel.Size = new System.Drawing.Size(260, 17);
            this.commandLabel.TabIndex = 17;
            this.commandLabel.Text = "Program or link to open on window click:";
            //
            // commandTextBox
            //
            this.prankFlowLayoutPanel.SetFlowBreak(this.commandTextBox, true);
            this.commandTextBox.Location = new System.Drawing.Point(23, 40);
            this.commandTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 0, 15);
            this.commandTextBox.Name = "commandTextBox";
            this.commandTextBox.Size = new System.Drawing.Size(516, 22);
            this.commandTextBox.TabIndex = 18;
            this.commandTextBox.TextChanged += new System.EventHandler(this.EventCommandTextBoxTextChanged);
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.prankFlowLayoutPanel.SetFlowBreak(this.label1, true);
            this.label1.Location = new System.Drawing.Point(20, 77);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(440, 17);
            this.label1.TabIndex = 22;
            this.label1.Text = "Use these settings to hide all clues of the identity of the fake window:";
            //
            // prankNoSettingsRightClickCheckBox
            //
            this.prankNoSettingsRightClickCheckBox.AutoSize = true;
            this.prankFlowLayoutPanel.SetFlowBreak(this.prankNoSettingsRightClickCheckBox, true);
            this.prankNoSettingsRightClickCheckBox.Location = new System.Drawing.Point(23, 112);
            this.prankNoSettingsRightClickCheckBox.Name = "prankNoSettingsRightClickCheckBox";
            this.prankNoSettingsRightClickCheckBox.Size = new System.Drawing.Size(238, 21);
            this.prankNoSettingsRightClickCheckBox.TabIndex = 19;
            this.prankNoSettingsRightClickCheckBox.Text = "Don\'t show Settings on right-click";
            this.prankNoSettingsRightClickCheckBox.UseVisualStyleBackColor = true;
            //
            // prankNoSettingsHotkeyCheckBox
            //
            this.prankNoSettingsHotkeyCheckBox.AutoSize = true;
            this.prankFlowLayoutPanel.SetFlowBreak(this.prankNoSettingsHotkeyCheckBox, true);
            this.prankNoSettingsHotkeyCheckBox.Location = new System.Drawing.Point(23, 139);
            this.prankNoSettingsHotkeyCheckBox.Name = "prankNoSettingsHotkeyCheckBox";
            this.prankNoSettingsHotkeyCheckBox.Size = new System.Drawing.Size(216, 21);
            this.prankNoSettingsHotkeyCheckBox.TabIndex = 20;
            this.prankNoSettingsHotkeyCheckBox.Text = "Don\'t show Settings on Ctrl+S";
            this.prankNoSettingsHotkeyCheckBox.UseVisualStyleBackColor = true;
            //
            // prankNoCloseCheckBox
            //
            this.prankNoCloseCheckBox.AutoSize = true;
            this.prankNoCloseCheckBox.Location = new System.Drawing.Point(23, 166);
            this.prankNoCloseCheckBox.Name = "prankNoCloseCheckBox";
            this.prankNoCloseCheckBox.Size = new System.Drawing.Size(301, 21);
            this.prankNoCloseCheckBox.TabIndex = 21;
            this.prankNoCloseCheckBox.Text = "Don\'t allow the dummy window to be closed";
            this.prankNoCloseCheckBox.UseVisualStyleBackColor = true;
            //
            // aboutTabPage
            //
            this.aboutTabPage.Controls.Add(this.flowLayoutPanel1);
            this.aboutTabPage.Location = new System.Drawing.Point(4, 25);
            this.aboutTabPage.Name = "aboutTabPage";
            this.aboutTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.aboutTabPage.Size = new System.Drawing.Size(575, 302);
            this.aboutTabPage.TabIndex = 3;
            this.aboutTabPage.Text = "About";
            this.aboutTabPage.UseVisualStyleBackColor = true;
            //
            // flowLayoutPanel1
            //
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel1.Controls.Add(this.aboutNameLabel);
            this.flowLayoutPanel1.Controls.Add(this.versionLabel);
            this.flowLayoutPanel1.Controls.Add(this.copyrightLabel);
            this.flowLayoutPanel1.Controls.Add(this.urlLinkLabel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(20);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(569, 296);
            this.flowLayoutPanel1.TabIndex = 0;
            //
            // aboutNameLabel
            //
            this.aboutNameLabel.AutoSize = true;
            this.aboutNameLabel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutNameLabel.Location = new System.Drawing.Point(23, 20);
            this.aboutNameLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.aboutNameLabel.Name = "aboutNameLabel";
            this.aboutNameLabel.Size = new System.Drawing.Size(124, 23);
            this.aboutNameLabel.TabIndex = 0;
            this.aboutNameLabel.Text = "FrameDummy";
            //
            // versionLabel
            //
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(23, 53);
            this.versionLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(78, 17);
            this.versionLabel.TabIndex = 1;
            this.versionLabel.Text = "Version {0}";
            //
            // copyrightLabel
            //
            this.copyrightLabel.AutoSize = true;
            this.copyrightLabel.Location = new System.Drawing.Point(23, 73);
            this.copyrightLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.copyrightLabel.Name = "copyrightLabel";
            this.copyrightLabel.Size = new System.Drawing.Size(368, 17);
            this.copyrightLabel.TabIndex = 2;
            this.copyrightLabel.Text = "Copyright © 2013-{0} The Hello World Writer.";
            //
            // urlLinkLabel
            //
            this.urlLinkLabel.AutoSize = true;
            this.urlLinkLabel.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.urlLinkLabel.Location = new System.Drawing.Point(23, 100);
            this.urlLinkLabel.Name = "urlLinkLabel";
            this.urlLinkLabel.Size = new System.Drawing.Size(191, 17);
            this.urlLinkLabel.TabIndex = 3;
            this.urlLinkLabel.TabStop = true;
            this.urlLinkLabel.Text = "https://www.thehelloworldwriter.com";
            this.urlLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.EventUrlLinkLabelLinkClicked);
            //
            // SettingsForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(623, 371);
            this.Controls.Add(this.settingsTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EventSettingsFormFormClosing);
            this.Shown += new System.EventHandler(this.EventSettingsFormShown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EventSettingsFormKeyDown);
            this.frameFlowLayoutPanel.ResumeLayout(false);
            this.frameFlowLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opacityTrackBar)).EndInit();
            this.contentFlowLayoutPanel.ResumeLayout(false);
            this.contentFlowLayoutPanel.PerformLayout();
            this.settingsTabControl.ResumeLayout(false);
            this.frameTabPage.ResumeLayout(false);
            this.contentTabPage.ResumeLayout(false);
            this.prankTabPage.ResumeLayout(false);
            this.prankFlowLayoutPanel.ResumeLayout(false);
            this.prankFlowLayoutPanel.PerformLayout();
            this.aboutTabPage.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel frameFlowLayoutPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.Label iconLabel;
        private System.Windows.Forms.TextBox iconTextBox;
        private System.Windows.Forms.Button iconBrowseButton;
        private System.Windows.Forms.OpenFileDialog openIconDialog;
        private System.Windows.Forms.CheckBox controlCheckBox;
        private System.Windows.Forms.Label borderLabel;
        private System.Windows.Forms.ComboBox borderComboBox;
        private System.Windows.Forms.CheckBox minimizeCheckBox;
        private System.Windows.Forms.CheckBox maximizeCheckBox;
        private System.Windows.Forms.CheckBox iconCheckBox;
        private System.Windows.Forms.CheckBox taskbarCheckBox;
        private System.Windows.Forms.CheckBox topmostCheckBox;
        private System.Windows.Forms.Label imageLabel;
        private System.Windows.Forms.TextBox imageTextBox;
        private System.Windows.Forms.Button imageBrowseButton;
        private System.Windows.Forms.Label layoutLabel;
        private System.Windows.Forms.Label colorLabel;
        private System.Windows.Forms.Label colorValueLabel;
        private System.Windows.Forms.Button colorBrowseButton;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.FlowLayoutPanel contentFlowLayoutPanel;
        private System.Windows.Forms.Label opacityLabel;
        private System.Windows.Forms.TrackBar opacityTrackBar;
        private System.Windows.Forms.Button imageClearButton;
        private System.Windows.Forms.Button autoSizeButton;
        private System.Windows.Forms.Button colorRandomButton;
        private System.Windows.Forms.CheckBox colorTransparentCheckBox;
        private System.Windows.Forms.ComboBox imageSizingComboBox;
        private System.Windows.Forms.TabControl settingsTabControl;
        private System.Windows.Forms.TabPage frameTabPage;
        private System.Windows.Forms.TabPage contentTabPage;
        private System.Windows.Forms.TabPage prankTabPage;
        private System.Windows.Forms.FlowLayoutPanel prankFlowLayoutPanel;
        private System.Windows.Forms.Label commandLabel;
        private System.Windows.Forms.TabPage aboutTabPage;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label aboutNameLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label copyrightLabel;
        private System.Windows.Forms.LinkLabel urlLinkLabel;
        internal System.Windows.Forms.CheckBox prankNoCloseCheckBox;
        internal System.Windows.Forms.CheckBox prankNoSettingsRightClickCheckBox;
        internal System.Windows.Forms.CheckBox prankNoSettingsHotkeyCheckBox;
        internal System.Windows.Forms.TextBox commandTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button iconDefaultButton;

    }
}
