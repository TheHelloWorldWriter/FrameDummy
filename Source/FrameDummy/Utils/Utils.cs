// Copyright (c) 2013-2026 The Hello World Writer (https://www.thehelloworldwriter.com).
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

namespace FrameDummy;

/// <summary>General-purpose static helpers shared across the app.</summary>
internal static class Utils
{
    /// <summary>Returns a random opaque color with each RGB channel uniformly sampled across 0..255. Uses Random.Shared (thread-safe, no instance management).</summary>
    internal static Color RandomColor() => Color.FromArgb(Random.Shared.Next(256), Random.Shared.Next(256), Random.Shared.Next(256));
}
