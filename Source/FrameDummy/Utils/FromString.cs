// Copyright (c) 2013-present The Hello World Writer (https://www.thehelloworldwriter.com).
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace FrameDummy;

/// <summary>
/// A method to execute on the success of parsing a string to two integer values.
/// </summary>
/// <param name="firstValue">The first integer in the pair.</param>
/// <param name="secondValue">The second integer in the pair.</param>
public delegate void IntPairAction(int firstValue, int secondValue);

/// <summary>
/// Translates string representations to different objects (Colors, etc.).
/// </summary>
public static class FromString
{
    /// <summary>
    /// Invokes an action delegate if a string is not null and not empty, or another action delegate otherwise.
    /// </summary>
    /// <param name="value">The string value to test.</param>
    /// <param name="successAction">The action delegate to be executed if the string is not null and not empty.</param>
    /// <param name="failAction">The action delegate to be executed if the string is null or empty.</param>
    public static void IfNonemptyString(string value, Action<string>? successAction, Action<string>? failAction)
    {
        if (!string.IsNullOrEmpty(value))
        {
            successAction?.Invoke(value);
        }
        else
        {
            failAction?.Invoke(value);
        }
    }

    /// <summary>
    /// Tries to parse a string value to an integer value, and executes a specified action delegate on success, or
    /// another action delegate on failure.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="successAction">The action delegate to execute on success.</param>
    /// <param name="failAction">The action delegate to execute on failure.</param>
    public static void IfInt(string value, Action<int>? successAction, Action<string>? failAction)
    {
        if (int.TryParse(value, out int i))
        {
            SafeInvoke(successAction, i);
        }
        else
        {
            SafeInvoke(failAction, value);
        }
    }

    /// <summary>
    /// Tries to parse a string value to two integer value, and executes a specified action delegate on success, or
    /// another action delegate on failure.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="separator">The character that separates the two integer values.</param>
    /// <param name="successAction">The action delegate to execute on success.</param>
    /// <param name="failAction">The action delegate to execute on failure.</param>
    public static void IfIntPair(string value, char separator, IntPairAction? successAction, Action<string>? failAction)
    {
        string[] pair = value.Split(separator);
        if ((pair.Length > 1) && int.TryParse(pair[0].Trim(), out int i) && int.TryParse(pair[1].Trim(), out int j))
        {
            if (successAction != null)
            {
                successAction(i, j);
                return;
            }
        }

        SafeInvoke(failAction, value);
    }

    /// <summary>
    /// Tries to parse a string value to an boolean value, and executes a specified action delegate on success, or
    /// another action delegate on failure.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="successAction">The action delegate to execute on success.</param>
    /// <param name="failAction">The action delegate to execute on failure.</param>
    public static void IfBool(string value, Action<bool>? successAction, Action<string>? failAction)
    {
        if (bool.TryParse(value, out bool b))
        {
            SafeInvoke(successAction, b);
        }
        else
        {
            SafeInvoke(failAction, value);
        }
    }

    /// <summary>
    /// Tries to parse a string value to an double value, and executes a specified action delegate on success, or
    /// another action delegate on failure.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="successAction">The action delegate to execute on success.</param>
    /// <param name="failAction">The action delegate to execute on failure.</param>
    public static void IfDouble(string value, Action<double>? successAction, Action<string>? failAction)
    {
        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double d))
        {
            SafeInvoke(successAction, d);
        }
        else
        {
            SafeInvoke(failAction, value);
        }
    }

    /// <summary>
    /// Tries to parse a string value to a enumeration value, and executes a specified action delegate on success, or
    /// another action delegate on failure.
    /// </summary>
    /// <typeparam name="T">The type of the enumeration to parse.</typeparam>
    /// <param name="value">The string value to parse.</param>
    /// <param name="successAction">The action delegate to execute on success.</param>
    /// <param name="failAction">The action delegate to execute on failure.</param>
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We invoke an failure handler on exceptions.")]
    public static void IfEnum<T>(string value, Action<T>? successAction, Action<string>? failAction)
    {
        try
        {
            T enumValue = (T)Enum.Parse(typeof(T), value);
            SafeInvoke(successAction, enumValue);
        }
        catch (Exception)
        {
            SafeInvoke(failAction, value);
        }
    }

    /// <summary>
    /// Tries to parse a string value to a TimeSpan value, and executes a specified action delegate on success, or
    /// another action delegate on failure.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="successAction">The action delegate to execute on success.</param>
    /// <param name="failAction">The action delegate to execute on failure.</param>
    public static void IfTimeSpan(string value, Action<TimeSpan>? successAction, Action<string>? failAction)
    {
        if (TimeSpan.TryParse(value, out TimeSpan timeSpan))
        {
            SafeInvoke(successAction, timeSpan);
        }
        else
        {
            SafeInvoke(failAction, value);
        }
    }

    /// <summary>
    /// Tries to translate an HTML color representation to a Color structure, and executes a specified
    /// delegate on success, or another specified delegate on failure.
    /// </summary>
    /// <param name="value">The HTML color representation to translate.</param>
    /// <param name="successAction">The delegate to execute on success.</param>
    /// <param name="failAction">The delegate to execute on failure.</param>
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We invoke an failure handler on exceptions.")]
    public static void IfHtmlColor(string value, Action<Color>? successAction, Action<string>? failAction)
    {
        try
        {
            Color color = ColorTranslator.FromHtml(value);
            if (!color.IsEmpty)
            {
                SafeInvoke(successAction, color);
            }
            else
            {
                SafeInvoke(failAction, value);
            }
        }
        catch
        {
            SafeInvoke(failAction, value);
        }
    }

    /// <summary>
    /// Tries to parse a string value to a Point value, and executes a specified action delegate on success, or
    /// another action delegate on failure.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="successAction">The action delegate to execute on success.</param>
    /// <param name="failAction">The action delegate to execute on failure.</param>
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We invoke an failure handler on exceptions.")]
    public static void IfPoint(string value, Action<Point>? successAction, Action<string>? failAction)
    {
        try
        {
            Point point = (Point)new PointConverter().ConvertFromInvariantString(value)!;
            SafeInvoke(successAction, point);
        }
        catch (Exception)
        {
            SafeInvoke(failAction, value);
        }
    }

    /// <summary>
    /// Tries to parse a string value to a Rectangle value, and executes a specified action delegate on success, or
    /// another action delegate on failure.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="successAction">The action delegate to execute on success.</param>
    /// <param name="failAction">The action delegate to execute on failure.</param>
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We invoke an failure handler on exceptions.")]
    public static void IfRectangle(string value, Action<Rectangle>? successAction, Action<string>? failAction)
    {
        try
        {
            Rectangle rect = (Rectangle)new RectangleConverter().ConvertFromInvariantString(value)!;
            SafeInvoke(successAction, rect);
        }
        catch (Exception)
        {
            SafeInvoke(failAction, value);
        }
    }

    /// <summary>
    /// Tries to parse a string value to a Keys value, and executes a specified action delegate on success, or
    /// another action delegate on failure.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="successAction">The action delegate to execute on success.</param>
    /// <param name="failAction">The action delegate to execute on failure.</param>
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We invoke an failure handler on exceptions.")]
    public static void IfKeys(string value, Action<Keys>? successAction, Action<string>? failAction)
    {
        try
        {
            Keys keys = (Keys)new KeysConverter().ConvertFromString(value)!;
            SafeInvoke(successAction, keys);
        }
        catch (Exception)
        {
            SafeInvoke(failAction, value);
        }
    }

    /// <summary>
    /// Invokes an action delegate only if the delegate is not null.
    /// </summary>
    /// <typeparam name="T">The type of the parameter for the action delegate.</typeparam>
    /// <param name="action">The action delegate to invoke.</param>
    /// <param name="parameter">The parameter for the action delegate.</param>
    private static void SafeInvoke<T>(Action<T>? action, T parameter)
    {
        action?.Invoke(parameter);
    }
}
