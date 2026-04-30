// Copyright (c) 2013-2026 The Hello World Writer (https://www.thehelloworldwriter.com).
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

using System.Text.Json;
using System.Text.Json.Serialization;

using CommunityToolkit.Mvvm.ComponentModel;

namespace FrameDummy;

/// <summary>
/// User settings for FrameDummy: the single source of truth for runtime state and the JSON model on disk.
/// Both forms observe the same instance; the source generator emits INotifyPropertyChanged plumbing for each
/// [ObservableProperty] field, so changes propagate to bound controls automatically. The setter guard inside
/// each generated property means observers fire only on real changes - which is what makes data-binding immune
/// to the focus-stealing problem that occurs when imperative code unconditionally re-sets style-affecting
/// Form properties.
/// </summary>
public sealed partial class Settings : ObservableObject
{
    // Frame tab.

    /// <summary>Frame title shown in the title bar of the fake window.</summary>
    [ObservableProperty] string _title = "FrameDummy - Press Ctrl+S for settings";

    /// <summary>Path to a custom icon file (.ico or image), or empty for the default application icon.</summary>
    [ObservableProperty] string _iconPath = string.Empty;

    /// <summary>Frame border style; controls whether the window has a border, sizable edges, etc.</summary>
    [ObservableProperty] FormBorderStyle _border = FormBorderStyle.Sizable;

    /// <summary>Window opacity as a percentage from 1 (nearly invisible) to 100 (fully opaque). Bind controls to this; bind Form.Opacity to OpacityFraction.</summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(OpacityFraction))]
    [NotifyPropertyChangedFor(nameof(OpacityLabel))]
    int _opacity = 100;

    /// <summary>Whether the frame shows the control box (the close / minimize / maximize button cluster).</summary>
    [ObservableProperty] bool _controlBox = true;

    /// <summary>Whether the frame shows its icon in the title bar.</summary>
    [ObservableProperty] bool _showIcon = true;

    /// <summary>Whether the minimize button is enabled in the control box.</summary>
    [ObservableProperty] bool _minimizeBox = true;

    /// <summary>Whether the maximize button is enabled in the control box.</summary>
    [ObservableProperty] bool _maximizeBox = true;

    /// <summary>Whether the frame appears in the Windows taskbar.</summary>
    [ObservableProperty] bool _showInTaskbar = true;

    /// <summary>Whether the frame stays above all other windows.</summary>
    [ObservableProperty] bool _topMost = true;

    // Content tab.

    /// <summary>Path to the displayed image, the special PastedImage sentinel, or empty when no image is shown.</summary>
    [ObservableProperty] string _imagePath = string.Empty;

    /// <summary>How the image is sized inside the frame (Normal, StretchImage, CenterImage, Zoom).</summary>
    [ObservableProperty] PictureBoxSizeMode _imageSizing = PictureBoxSizeMode.Zoom;

    /// <summary>Background color of the frame; bound directly to BackColor on MainForm and the swatch in SettingsForm.</summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(EffectiveTransparencyKey))]
    Color _color = Color.LightSlateGray;

    /// <summary>Whether the background color is treated as transparent (becomes click-through). Combines with Color into EffectiveTransparencyKey.</summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(EffectiveTransparencyKey))]
    bool _colorTransparent = true;

    // Prank tab.

    /// <summary>Program path or URL to launch on left-click; empty string disables click-through behavior.</summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(EffectiveCursor))]
    string _prankCommand = string.Empty;

    /// <summary>Prank: hide the Settings form even when the user right-clicks the frame.</summary>
    [ObservableProperty] bool _prankNoSettingsRightClick;

    /// <summary>Prank: hide the Settings form even when the user presses Ctrl+S.</summary>
    [ObservableProperty] bool _prankNoSettingsHotkey;

    /// <summary>Prank: refuse to close the frame, making it appear stuck to the user.</summary>
    [ObservableProperty] bool _prankNoClose;

    // Window state.

    /// <summary>Saved window position and size; null means use the default centered location.</summary>
    [ObservableProperty] WindowBounds? _bounds;

    /// <summary>Whether the frame was maximized at last save.</summary>
    [ObservableProperty] bool _maximized;

    // Computed (derived) properties - read-only, [JsonIgnore] so they are not serialized.

    /// <summary>Opacity as a 0.0-1.0 fraction for direct binding to Form.Opacity.</summary>
    [JsonIgnore]
    public double OpacityFraction => Opacity / 100.0;

    /// <summary>Formatted opacity label for the slider caption ("Opacity:\n100%").</summary>
    [JsonIgnore]
    public string OpacityLabel => $"Opacity:\n{Opacity}%";

    /// <summary>Color when transparency is on; Color.Empty otherwise. Bound to Form.TransparencyKey.</summary>
    [JsonIgnore]
    public Color EffectiveTransparencyKey => ColorTransparent ? Color : System.Drawing.Color.Empty;

    /// <summary>Hand cursor when a prank command is set; default cursor otherwise. Bound to Form.Cursor.</summary>
    [JsonIgnore]
    public Cursor EffectiveCursor => string.IsNullOrEmpty(PrankCommand) ? Cursors.Default : Cursors.Hand;
}

/// <summary>Saved position and size of the main form. Maps to a Rectangle at the call site.</summary>
public sealed record WindowBounds(int X, int Y, int Width, int Height);

/// <summary>Json converter that serializes a System.Drawing.Color as the HTML form (named color or "#RRGGBB") and parses it back.</summary>
public sealed class ColorJsonConverter : JsonConverter<Color>
{
    /// <summary>Reads an HTML color string; falls back to Black if the value is null or empty.</summary>
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var s = reader.GetString();
        return string.IsNullOrEmpty(s) ? Color.Black : ColorTranslator.FromHtml(s);
    }

    /// <summary>Writes a Color as the HTML form: a named color when known, otherwise "#RRGGBB".</summary>
    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        => writer.WriteStringValue(ColorTranslator.ToHtml(value));
}

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

    /// <summary>Cached JsonSerializer options: indented output, null-omitting, enums-as-strings, Color via ColorJsonConverter.</summary>
    static readonly JsonSerializerOptions s_options = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters =
        {
            new JsonStringEnumConverter(),
            new ColorJsonConverter(),
        },
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
