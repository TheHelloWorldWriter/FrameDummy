// Copyright (c) 2013-present The Hello World Writer (https://www.thehelloworldwriter.com).
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

namespace FrameDummy;

/// <summary>
/// App settings, such as INI file keys and file names. Keeps these values out of the main code and
/// makes it easier to change them in the future if needed.
/// </summary>
public static class AppSettings
{
    /// <summary>
    /// The INI settings file name. The runtime resolves the actual path.
    /// </summary>
    public const string IniFileName = "framedummy.ini";

    /// <summary>
    /// The key used to store the window's maximized state in the INI file.
    /// </summary>
    public const string MaximizedKey = "maximized";
}
