// Copyright (c) 2013-2026 The Hello World Writer (https://www.thehelloworldwriter.com).
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrameDummy;

/// <summary>
/// Persisted user settings for FrameDummy. Loaded once at startup and saved once at shutdown via <see cref="SettingsStore"/>.
/// Properties are init-only; replace the record (with-expression or new instance) to apply changes.
/// </summary>
public sealed record Settings
{
    // Frame tab.
    public string Title { get; init; } = "FrameDummy - Press Ctrl+S for settings";
    public string? IconPath { get; init; }
    public FormBorderStyle Border { get; init; } = FormBorderStyle.Sizable;
    public int Opacity { get; init; } = 100;
    public bool ControlBox { get; init; } = true;
    public bool ShowIcon { get; init; } = true;
    public bool MinimizeBox { get; init; } = true;
    public bool MaximizeBox { get; init; } = true;
    public bool ShowInTaskbar { get; init; } = true;
    public bool TopMost { get; init; } = true;

    // Content tab.
    public string? ImagePath { get; init; }
    public PictureBoxSizeMode ImageSizing { get; init; } = PictureBoxSizeMode.Zoom;

    /// <summary>Background color in HTML form: a named color ("LightSlateGray") or "#RRGGBB". Decoded with <see cref="System.Drawing.ColorTranslator"/>.</summary>
    public string Color { get; init; } = "LightSlateGray";
    public bool ColorTransparent { get; init; } = true;

    // Prank tab.
    public string PrankCommand { get; init; } = string.Empty;
    public bool PrankNoSettingsRightClick { get; init; }
    public bool PrankNoSettingsHotkey { get; init; }
    public bool PrankNoClose { get; init; }

    // Window state.
    public WindowBounds? Bounds { get; init; }
    public bool Maximized { get; init; }
}

/// <summary>Saved position and size of the main form. Maps to <see cref="System.Drawing.Rectangle"/> at the call site.</summary>
public sealed record WindowBounds(int X, int Y, int Width, int Height);

/// <summary>
/// Loads and saves <see cref="Settings"/> to a JSON file. The file path is portable-first
/// (next to the executable, when that file already exists) with an %AppData% fallback for installed scenarios.
/// </summary>
public static class SettingsStore
{
    const string FileName = "framedummy.json";
    const string Company = "TheHelloWorldWriter";
    const string Product = "FrameDummy";

    static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new JsonStringEnumConverter() },
    };

    /// <summary>Resolved settings file path. Computed once at startup; portable-first, AppData fallback.</summary>
    public static string FilePath { get; } = ResolvePath();

    /// <summary>Loads settings from <see cref="FilePath"/>. Returns defaults if the file is missing or unreadable.</summary>
    public static Settings Load()
    {
        if (!File.Exists(FilePath)) return new Settings();

        try
        {
            return JsonSerializer.Deserialize<Settings>(File.ReadAllText(FilePath), Options) ?? new Settings();
        }
        catch (Exception ex) when (ex is IOException or JsonException or UnauthorizedAccessException)
        {
            // Suppress at the I/O boundary: a corrupt or unreadable settings file should not block startup.
            Debug.WriteLine($"Settings load failed ({FilePath}): {ex.Message}");
            return new Settings();
        }
    }

    /// <summary>Saves settings to <see cref="FilePath"/>, creating the AppData directory if needed. Errors are logged and suppressed.</summary>
    public static void Save(Settings settings)
    {
        try
        {
            var dir = Path.GetDirectoryName(FilePath);
            if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
            File.WriteAllText(FilePath, JsonSerializer.Serialize(settings, Options));
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            // Suppress at the I/O boundary: a save failure on shutdown should not crash the app or block close.
            Debug.WriteLine($"Settings save failed ({FilePath}): {ex.Message}");
        }
    }

    static string ResolvePath()
    {
        var portable = Path.Combine(AppContext.BaseDirectory, FileName);
        if (File.Exists(portable)) return portable;

        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            Company, Product, FileName);
    }
}
