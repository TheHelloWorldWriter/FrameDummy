// Copyright (c) 2013-2026 The Hello World Writer (https://www.thehelloworldwriter.com).
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

namespace FrameDummy;

/// <summary>
/// User-facing strings, centralized in one place. Keeps string literals out of UI files
/// and makes future localization a matter of swapping this class.
/// </summary>
public static class AppStrings
{
    // // MainForm.

    /// <summary>Initial frame title before the user has set a custom one.</summary>
    public const string DefaultTitle = "FrameDummy - Press Ctrl+S for settings";

    /// <summary>Placeholder shown in the image-path field after the user pastes an image from the clipboard.</summary>
    public const string PastedImage = "(pasted image)";

    /// <summary>Placeholder shown in the icon-path field when the default app icon is in use.</summary>
    public const string DefaultIcon = "(default icon)";

    /// <summary>Placeholder shown in the image-path field when no image has been chosen.</summary>
    public const string NoImage = "(no image)";

    // // Error messages (format strings).

    /// <summary>Format string for the icon-load error MessageBox. {0} = file path.</summary>
    public const string IconLoadErrorFormat = "Cannot load icon from\n{0}\n\nThis is not a valid icon file, or the file does not exist.";

    /// <summary>Format string for the image-load error MessageBox (unsupported format or corrupt file). {0} = file path.</summary>
    public const string ImageLoadErrorFormat = "{0}\n\nCannot load this image file. This is not a valid image file, or its format is not currently supported.";

    /// <summary>Format string for the image-not-found error MessageBox. {0} = file path.</summary>
    public const string ImageNotFoundErrorFormat = "Cannot load image from\n{0}\n\nThe file does not exist.";

    /// <summary>Format string for the prank-command error MessageBox. {0} = command, {1} = exception message.</summary>
    public const string CommandErrorFormat = "Error running\n{0}\n\n{1}";

    // /// <summary>Format string for the settings-load error MessageBox. {0} = exception message.</summary>
    // public const string SettingsLoadErrorFormat = "Failed to load settings:\n\n{0}";

    // /// <summary>Format string for the settings-save error MessageBox shown on close. {0} = exception message.</summary>
    // public const string SettingsSaveErrorFormat = "Failed to save settings:\n\n{0}\n\nClose anyway?";

    // // SettingsForm.

    // /// <summary>Title of the Settings window.</summary>
    // public const string SettingsTitle = "Settings";

    // /// <summary>Label common to several "browse for a file" buttons.</summary>
    // public const string ButtonBrowse = "Browse...";

    // // Frame tab.

    // /// <summary>Tab page label for the frame settings tab.</summary>
    // public const string TabFrame = "Window Frame";

    // /// <summary>Caption for the title text-box label.</summary>
    // public const string LabelTitle = "Title:";

    // /// <summary>Caption for the icon path label.</summary>
    // public const string LabelIcon = "Icon:";

    // /// <summary>Caption for the icon "Default" button (restores the application's default icon).</summary>
    // public const string ButtonIconDefault = "Default";

    // /// <summary>Caption for the border-style label.</summary>
    // public const string LabelBorder = "Border:";

    // /// <summary>Format string for the opacity label. {0} = opacity as a percentage 0..1 (formatted with "0%").</summary>
    // public const string LabelOpacityFormat = "Opacity:\n{0:0%}";

    // /// <summary>Caption for the Control Box checkbox.</summary>
    // public const string CheckControlBox = "Control Box";

    // /// <summary>Caption for the Show Icon checkbox.</summary>
    // public const string CheckShowIcon = "Show Icon";

    // /// <summary>Caption for the Minimize Box checkbox.</summary>
    // public const string CheckMinimizeBox = "Minimize Box";

    // /// <summary>Caption for the Maximize Box checkbox.</summary>
    // public const string CheckMaximizeBox = "Maximize Box";

    // /// <summary>Caption for the Show In Taskbar checkbox.</summary>
    // public const string CheckShowInTaskbar = "Show In Taskbar";

    // /// <summary>Caption for the Topmost checkbox.</summary>
    // public const string CheckTopmost = "Topmost";

    // // Content tab.

    // /// <summary>Tab page label for the content tab.</summary>
    // public const string TabContent = "Window Content";

    // /// <summary>Caption for the image path label.</summary>
    // public const string LabelImage = "Image:";

    // /// <summary>Caption for the image "Clear" button.</summary>
    // public const string ButtonImageClear = "Clear";

    // /// <summary>Caption for the image-sizing label.</summary>
    // public const string LabelSizing = "Sizing:";

    // /// <summary>Caption for the autosize button (mnemonic on A).</summary>
    // public const string ButtonAutosize = "&Autosize";

    // /// <summary>Caption for the color label.</summary>
    // public const string LabelColor = "Color:";

    // /// <summary>Caption for the color "Random" button.</summary>
    // public const string ButtonColorRandom = "Random";

    // /// <summary>Caption for the color-transparent checkbox.</summary>
    // public const string CheckColorTransparent = "Color is transparent";

    // // Prank tab.

    // /// <summary>Tab page label for the prank tab.</summary>
    // public const string TabPrank = "Prank Mode";

    // /// <summary>Caption for the prank command label.</summary>
    // public const string LabelPrankCommand = "Program or link to open on window click:";

    // /// <summary>Caption for the prank intro label.</summary>
    // public const string LabelPrankIntro = "Use these settings to hide all clues of the identity of the fake window:";

    // /// <summary>Caption for the "no settings on right-click" prank checkbox.</summary>
    // public const string CheckPrankNoRightClick = "Don't show Settings on right-click";

    // /// <summary>Caption for the "no settings on Ctrl+S" prank checkbox.</summary>
    // public const string CheckPrankNoHotkey = "Don't show Settings on Ctrl+S";

    // /// <summary>Caption for the "no close" prank checkbox.</summary>
    // public const string CheckPrankNoClose = "Don't allow the dummy window to be closed";

    // // About tab.

    // /// <summary>Tab page label for the about tab.</summary>
    // public const string TabAbout = "About";

    // /// <summary>Application name shown on the About tab.</summary>
    // public const string AppName = "FrameDummy";

    // /// <summary>Format string for the version label. {0} = product version.</summary>
    // public const string AboutVersionFormat = "Version {0}";

    // /// <summary>Format string for the copyright label. {0} = current year.</summary>
    // public const string AboutCopyrightFormat = "Copyright © 2013-{0} The Hello World Writer. All Rights Reserved.";

    // /// <summary>Project home URL shown as a clickable link.</summary>
    // public const string AboutUrl = "https://www.thehelloworldwriter.com";

    // // Dialog filters.

    // /// <summary>Title of the icon-file open dialog.</summary>
    // public const string IconDialogTitle = "Select Icon";

    // /// <summary>Title of the image-file open dialog.</summary>
    // public const string ImageDialogTitle = "Select Image";

    // /// <summary>Filter string for the icon-file open dialog.</summary>
    // public const string IconDialogFilter = "Icon files (*.ico)|*.ico|All image files|*.bmp;*.gif;*.jpg;*.jpeg;*.png;*.tif;*.tiff;*.ico|All files (*.*)|*.*";

    // /// <summary>Filter string for the image-file open dialog.</summary>
    // public const string ImageDialogFilter = "Image files|*.bmp;*.gif;*.jpg;*.jpeg;*.png;*.tif;*.tiff|All files (*.*)|*.*";
}
