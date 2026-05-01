// Copyright (c) 2013-2026 The Hello World Writer (https://www.thehelloworldwriter.com).
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

namespace FrameDummy;

/// <summary>
/// Provides methods to load and save settings from/to a simple INI file.
/// </summary>
public class VerySimpleIni
{
    /// <summary>
    /// The INI key=value separator.
    /// </summary>
    private static readonly char[] s_keyValueSeparator = ['='];

    /// <summary>
    /// The lines from the INI file containing the settings.
    /// </summary>
    private List<string>? _lines = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="VerySimpleIni"/> class, using a full configuration file pathname.
    /// </summary>
    /// <param name="fileName">The configuration file pathname.</param>
    /// <param name="force">Force using this file even if it does not exist.</param>
    public VerySimpleIni(string fileName, bool force)
    {
        FileName = force ? fileName : File.Exists(fileName) ? fileName : string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="VerySimpleIni"/> class.
    /// </summary>
    /// <param name="iniNameOnly">The name and extension part of the configuration file.</param>
    /// <param name="executablePath">The pathname of the program's executable.</param>
    /// <param name="companyName">The company name.</param>
    /// <param name="productName">The product name.</param>
    /// <param name="forceForRunDirectly">Force the creation of the configuration file if we are in Run Directly mode.</param>
    public VerySimpleIni(string iniNameOnly, string executablePath, string companyName, string productName, bool forceForRunDirectly)
    {
        // First check if we have an INI file in the directory where the program main executable
        // file is located (to support portable programs)
        string portableIniFile = FileName = Path.Combine(Path.GetDirectoryName(executablePath) ?? string.Empty, iniNameOnly);

        // If no INI file is found, try the program data directory from the ApplicationData
        // special directory for the current roaming user
        if (!File.Exists(FileName))
        {
            FileName = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                companyName + @"\" + productName + @"\" + iniNameOnly);

            // If again no INI file is found, we must be in Run Directly Mode, so we will not load or
            // save any configuration, expect for the case in which forceForRunDirectly is set
            if (!File.Exists(FileName))
            {
                FileName = forceForRunDirectly ? portableIniFile : string.Empty;
            }
        }
    }

    /// <summary>
    /// Gets the INI file path and name.
    /// </summary>
    public string FileName { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the INI file is ready for loading or saving.
    /// </summary>
    public bool IsReady
    {
        get
        {
            return !string.IsNullOrEmpty(FileName);
        }
    }

    /// <summary>
    /// Reads all the lines from the INI file.
    /// </summary>
    /// <returns>True if at least one line was read from the INI file, false otherwise.</returns>
    public bool Load()
    {
        if (IsReady)
        {
            // Read all the lines, trim off any white-space characters from each line and remove empty lines
            _lines = [.. File.ReadAllLines(FileName)];
            _lines = _lines.ConvertAll<string>(line => line.Trim());
            _lines.RemoveAll(line => { return string.IsNullOrEmpty(line); });
            return _lines.Count > 0;
        }

        return false;
    }

    /// <summary>
    /// Gets the value of a setting, or a default value if the setting does not exist.
    /// </summary>
    /// <param name="key">The setting name.</param>
    /// <param name="defaultValue">The default value to return.</param>
    /// <returns>The setting value.</returns>
    public string GetValue(string key, string defaultValue)
    {
        if ((_lines != null) && (_lines.Count > 0))
        {
            // Find the line that starts with the specified key ("key=value")
            int index = _lines.FindIndex(line => { return line.StartsWith(key, true, null); });
            if (index >= 0)
            {
                // If line is found, save a reference to it and remove it from the list, to ensure faster
                // future key look-ups
                string line = _lines[index];
                _lines.RemoveAt(index);

                // Split the line in the "name=value" format and return the "value" part
                string[] lineParts = line.Split(VerySimpleIni.s_keyValueSeparator, 2);
                if (lineParts.Length == 2)
                {
                    return lineParts[1].Trim();
                }
            }
        }

        return defaultValue;
    }

    /// <summary>
    /// Gets the value of a setting, or an empty string if the setting does not exist.
    /// </summary>
    /// <param name="key">The setting name.</param>
    /// <returns>The setting value.</returns>
    public string GetValue(string key)
    {
        return GetValue(key, string.Empty);
    }

    /// <summary>
    /// Clears all the settings.
    /// </summary>
    public void Clear()
    {
        _lines?.Clear();
    }

    /// <summary>
    /// Sets a setting value to a string value.
    /// </summary>
    /// <param name="key">The name of the setting.</param>
    /// <param name="value">The value of the setting.</param>
    public void SetValue(string key, string value)
    {
        _lines ??= [];

        _lines.Add(string.Concat(key, VerySimpleIni.s_keyValueSeparator[0].ToString(), value));
    }

    /// <summary>
    /// Sets a setting value to the default string representation of an object.
    /// </summary>
    /// <param name="key">The name of the setting.</param>
    /// <param name="value">The value of the setting.</param>
    public void SetValue(string key, object value)
    {
        SetValue(key, value.ToString() ?? String.Empty);
    }

    /// <summary>
    /// Saves all the settings to the INI file.
    /// </summary>
    /// <returns>True if the saving was successful, false otherwise.</returns>
    public bool Save()
    {
        if ((_lines != null) && (_lines.Count > 0) && IsReady)
        {
            File.WriteAllLines(FileName, [.. _lines]);
            return true;
        }

        return false;
    }
}

