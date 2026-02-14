namespace EncrypterLibrary;

using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Encryption/decryption extension methods.
/// </summary>
public static class StringEncryptionExtensions
{
    /// <summary>
    /// Encrypts the input string by using AES-CTR and returns URL-escaped Base64 text.
    /// Key must be 32 chars and IV must be 16 chars.
    /// </summary>
    public static string? EncryptMyData(this string input, string key, string iv)
    {
        try
        {
            Validate(key, iv);
            var plainBytes = Encoding.UTF8.GetBytes(input);
            var cipherBytes = AesCtrCrypt(plainBytes, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));
            return Uri.EscapeDataString(Convert.ToBase64String(cipherBytes));
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Decrypts URL-escaped Base64 text by using AES-CTR.
    /// Key must be 32 chars and IV must be 16 chars.
    /// </summary>
    public static string? DecryptMyData(this string input, string key, string iv)
    {
        try
        {
            Validate(key, iv);
            var decodedInput = Uri.UnescapeDataString(input);
            var cipherBytes = Convert.FromBase64String(decodedInput);
            var plainBytes = AesCtrCrypt(cipherBytes, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));
            return Encoding.UTF8.GetString(plainBytes);
        }
        catch
        {
            return null;
        }
    }

    // Backward-compatible wrappers for legacy naming.
    public static string? encryptMyData(this string input, string key, string iv) =>
        EncryptMyData(input, key, iv);

    public static string? decryptMyData(this string input, string key, string iv) =>
        DecryptMyData(input, key, iv);

    private static void Validate(string key, string iv)
    {
        if (key.Length != 32 || iv.Length != 16)
        {
            throw new ArgumentException("Length error. Check Key and IV Length.");
        }
    }

    private static byte[] AesCtrCrypt(byte[] input, byte[] key, byte[] iv)
    {
        var output = new byte[input.Length];
        var counter = (byte[])iv.Clone();
        var keyStream = new byte[16];

        using var aes = Aes.Create();
        aes.Mode = CipherMode.ECB;
        aes.Padding = PaddingMode.None;
        aes.Key = key;

        using var encryptor = aes.CreateEncryptor();

        var offset = 0;
        while (offset < input.Length)
        {
            encryptor.TransformBlock(counter, 0, counter.Length, keyStream, 0);

            var blockSize = Math.Min(16, input.Length - offset);
            for (var i = 0; i < blockSize; i++)
            {
                output[offset + i] = (byte)(input[offset + i] ^ keyStream[i]);
            }

            IncrementCounter(counter);
            offset += blockSize;
        }

        return output;
    }

    private static void IncrementCounter(byte[] counter)
    {
        for (var i = counter.Length - 1; i >= 0; i--)
        {
            if (++counter[i] != 0)
            {
                break;
            }
        }
    }
}
