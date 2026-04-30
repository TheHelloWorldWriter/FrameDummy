// Copyright (c) 2013-2026 The Hello World Writer (https://www.thehelloworldwriter.com).
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrameDummy;

/// <summary>
/// Persisted user settings for FrameDummy. Loaded once at startup and saved once at shutdown via SettingsStore.
/// Properties are init-only; replace the record (with-expression or new instance) to apply changes.
/// </summary>
public sealed record Settings
{
    // Frame tab.

    /// <summary>Frame title shown in the title bar of the fake window.</summary>
    public string Title { get; init; } = "FrameDummy - Press Ctrl+S for settings";

    /// <summary>Path to a custom icon file (.ico or image), or null for the default application icon.</summary>
    public string? IconPath { get; init; }

    /// <summary>Frame border style; controls whether the window has a border, sizable edges, etc.</summary>
    public FormBorderStyle Border { get; init; } = FormBorderStyle.Sizable;

    /// <summary>Window opacity as a percentage from 1 (nearly invisible) to 100 (fully opaque).</summary>
    public int Opacity { get; init; } = 100;

    /// <summary>Whether the frame shows the control box (the close / minimize / maximize button cluster).</summary>
    public bool ControlBox { get; init; } = true;

    /// <summary>Whether the frame shows its icon in the title bar.</summary>
    public bool ShowIcon { get; init; } = true;

    /// <summary>Whether the minimize button is enabled in the control box.</summary>
    public bool MinimizeBox { get; init; } = true;

    /// <summary>Whether the maximize button is enabled in the control box.</summary>
    public bool MaximizeBox { get; init; } = true;

    /// <summary>Whether the frame appears in the Windows taskbar.</summary>
    public bool ShowInTaskbar { get; init; } = true;

    /// <summary>Whether the frame stays above all other windows.</summary>
    public bool TopMost { get; init; } = true;

    // Content tab.

    /// <summary>Path to the displayed image, or null when no image is shown.</summary>
    public string? ImagePath { get; init; }

    /// <summary>How the image is sized inside the frame (Normal, StretchImage, CenterImage, Zoom).</summary>
    public PictureBoxSizeMode ImageSizing { get; init; } = PictureBoxSizeMode.Zoom;

    /// <summary>Background color in HTML form: a named color ("LightSlateGray") or "#RRGGBB". Decoded with ColorTranslator.</summary>
    public string Color { get; init; } = "LightSlateGray";

    /// <summary>Whether the background color is treated as transparent (becomes click-through).</summary>
    public bool ColorTransparent { get; init; } = true;

    // Prank tab.

    /// <summary>Program path or URL to launch on left-click; empty string disables click-through behavior.</summary>
    public string PrankCommand { get; init; } = string.Empty;

    /// <summary>Prank: hide the Settings form even when the user right-clicks the frame.</summary>
    public bool PrankNoSettingsRightClick { get; init; }

    /// <summary>Prank: hide the Settings form even when the user presses Ctrl+S.</summary>
    public bool PrankNoSettingsHotkey { get; init; }

    /// <summary>Prank: refuse to close the frame, making it appear stuck to the user.</summary>
    public bool PrankNoClose { get; init; }

    // Window state.

    /// <summary>Saved window position and size; null means use the default centered location.</summary>
    public WindowBounds? Bounds { get; init; }

    /// <summary>Whether the frame was maximized at last save.</summary>
    public bool Maximized { get; init; }
}

/// <summary>Saved position and size of the main form. Maps to a Rectangle at the call site.</summary>
public sealed record WindowBounds(int X, int Y, int Width, int Height);

/// <summary>
/// Loads and saves Settings to a JSON file. The file path is portable-first
/// (next to the executable, when that file already exists) with an %AppData% fallback for installed scenarios.
/// </summary>
public static class SettingsStore
{
    /// <summary>Settings file name; combined with the resolved directory at ResolvePath.</summary>
    const string FileName = "framedummy.json";

    /// <summary>Company-name component of the AppData fallback path.</summary>
    const string Company = "TheHelloWorldWriter";

    /// <summary>Product-name component of the AppData fallback path.</summary>
    const string Product = "FrameDummy";

    /// <summary>Cached JsonSerializer options: indented output, null-omitting, enums-as-strings.</summary>
    static readonly JsonSerializerOptions s_options = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new JsonStringEnumConverter() },
    };

    /// <summary>Resolved settings file path. Computed once at startup; portable-first, AppData fallback.</summary>
    public static string FilePath { get; } = ResolvePath();

    /// <summary>Loads settings from FilePath. Returns defaults if the file is missing; throws on I/O or JSON errors.</summary>
    public static Settings Load()
    {
        if (!File.Exists(FilePath)) return new Settings();
        return JsonSerializer.Deserialize<Settings>(File.ReadAllText(FilePath), s_options) ?? new Settings();
    }

    /// <summary>Saves settings to FilePath, creating the AppData directory if needed. Throws on I/O errors.</summary>
    public static void Save(Settings settings)
    {
        var dir = Path.GetDirectoryName(FilePath);
        if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
        File.WriteAllText(FilePath, JsonSerializer.Serialize(settings, s_options));
    }

    /// <summary>Returns the portable settings path (next to the executable) when that file exists; otherwise the %AppData% fallback path.</summary>
    static string ResolvePath()
    {
        var portable = Path.Combine(AppContext.BaseDirectory, FileName);
        if (File.Exists(portable)) return portable;

        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            Company, Product, FileName);
    }
}
