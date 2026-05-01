// Copyright (c) 2013-2026 The Hello World Writer (https://www.thehelloworldwriter.com).
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

namespace FrameDummy;

/// <summary>Application entry point. Configures global WinForms defaults and runs the main form.</summary>
static class Program
{
    /// <summary>Main entry point. Initializes high-DPI mode, dark-mode follow-system, and runs the MainForm.</summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.SetColorMode(SystemColorMode.System);
        Application.Run(new MainForm());
    }
}
