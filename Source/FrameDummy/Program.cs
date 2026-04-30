// Copyright (c) 2013-2026 The Hello World Writer (https://www.thehelloworldwriter.com).
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

using System.Text.Json;

namespace FrameDummy;

/// <summary>Application entry point. Configures global WinForms defaults, loads the settings VM, and runs the main form.</summary>
static class Program
{
    /// <summary>Main entry point. Initializes high-DPI mode, dark-mode follow-system, loads the shared Settings VM from disk, and runs MainForm over it.</summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.SetColorMode(SystemColorMode.System);

        Settings vm;
        try
        {
            vm = SettingsStore.Load();
        }
        catch (Exception ex) when (ex is IOException or JsonException or UnauthorizedAccessException)
        {
            MessageBox.Show(
                string.Format(Strings.SettingsLoadErrorFormat, ex.Message),
                Application.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            vm = new Settings();
        }

        Application.Run(new MainForm(vm));
    }
}
