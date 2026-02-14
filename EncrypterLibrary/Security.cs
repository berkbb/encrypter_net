namespace EncrypterLibrary;

using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Security helpers for password generation.
/// </summary>
public static class Security
{
    private const string UpperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string LowerChars = "abcdefghijklmnopqrstuvwxyz";
    private const string NumberChars = "0123456789";
    private const string SpecialChars = "!#$%&@*^";

    /// <summary>
    /// Generates a random password based on enabled character groups.
    /// </summary>
    public static string? GeneratePassword(
        bool isNumberActive,
        bool isSpecialCharacherActive,
        bool isUpperCaseActive,
        bool isLowerCaseActive,
        int length)
    {
        if (length <= 0)
        {
            return null;
        }

        var pools = new List<string>(capacity: 4);
        if (isLowerCaseActive) pools.Add(LowerChars);
        if (isUpperCaseActive) pools.Add(UpperChars);
        if (isNumberActive) pools.Add(NumberChars);
        if (isSpecialCharacherActive) pools.Add(SpecialChars);

        if (pools.Count == 0)
        {
            return null;
        }

        var builder = new StringBuilder(length);
        for (var i = 0; i < length; i++)
        {
            var selectedPool = pools[RandomNumberGenerator.GetInt32(pools.Count)];
            builder.Append(selectedPool[RandomNumberGenerator.GetInt32(selectedPool.Length)]);
        }

        return builder.ToString();
    }

    /// <summary>
    /// Generates password text with prefix and delimiter.
    /// </summary>
    public static string? GeneratePasswordWithPrefix(string password, string prefix, string prefixDelimiter)
    {
        if (password is null || prefix is null || prefixDelimiter is null)
        {
            return null;
        }

        return $"{prefix}{prefixDelimiter}{password}";
    }

    // Backward-compatible wrappers for legacy naming.
    public static string? generatePassword(
        bool isNumberActive,
        bool isSpecialCharacherActive,
        bool isUpperCaseActive,
        bool isLowerCaseActive,
        int length) =>
        GeneratePassword(isNumberActive, isSpecialCharacherActive, isUpperCaseActive, isLowerCaseActive, length);

    public static string? generatePasswordwithPrefix(string password, string prefix, string prefixDelimiter) =>
        GeneratePasswordWithPrefix(password, prefix, prefixDelimiter);
}
