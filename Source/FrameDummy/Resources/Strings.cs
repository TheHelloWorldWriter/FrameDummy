// Copyright (c) 2013-2026 The Hello World Writer (https://www.thehelloworldwriter.com).
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

namespace FrameDummy;

/// <summary>
/// User-facing strings, centralized in one place. Keeps string literals out of UI files
/// and makes future localization a matter of swapping this class.
/// </summary>
public static class Strings
{
    /// <summary>Initial frame title before the user has set a custom one.</summary>
    public const string DefaultTitle = "FrameDummy - Press Ctrl+S for settings";

    /// <summary>Placeholder shown in the image-path field after the user pastes an image from the clipboard.</summary>
    public const string PastedImage = "(pasted image)";

    /// <summary>Format string for the icon-load error MessageBox. {0} = file path.</summary>
    public const string IconLoadErrorFormat = "Cannot load icon from\n{0}\n\nThis is not a valid icon file, or the file does not exist.";

    /// <summary>Format string for the image-load error MessageBox (unsupported format or corrupt file). {0} = file path.</summary>
    public const string ImageLoadErrorFormat = "{0}\n\nCannot load this image file. This is not a valid image file, or its format is not currently supported.";

    /// <summary>Format string for the image-not-found error MessageBox. {0} = file path.</summary>
    public const string ImageNotFoundErrorFormat = "Cannot load image from\n{0}\n\nThe file does not exist.";

    /// <summary>Format string for the prank-command error MessageBox. {0} = command, {1} = exception message.</summary>
    public const string CommandErrorFormat = "Error running\n{0}\n\n{1}";

    /// <summary>Format string for the settings-load error MessageBox. {0} = exception message.</summary>
    public const string SettingsLoadErrorFormat = "Failed to load settings:\n\n{0}";

    /// <summary>Format string for the settings-save error MessageBox shown on close. {0} = exception message.</summary>
    public const string SettingsSaveErrorFormat = "Failed to save settings:\n\n{0}\n\nClose anyway?";
}
