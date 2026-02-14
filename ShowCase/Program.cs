using EncrypterLibrary;

var key = Security.GeneratePassword(true, true, true, true, 32);
var iv = Security.GeneratePassword(true, true, true, true, 16);

Console.WriteLine($"Key: {key}");
Console.WriteLine($"IV: {iv}");

if (key is not null && iv is not null)
{
    var encrypted = "Hello my dear !".EncryptMyData(key, iv);
    Console.WriteLine($"Encrypted text: {encrypted}");

    if (encrypted is not null)
    {
        var decrypted = encrypted.DecryptMyData(key, iv);
        Console.WriteLine($"Decrypted text: {decrypted}");
    }

    Console.WriteLine($"Password 1: {Security.GeneratePassword(true, true, true, true, 16)}");
    Console.WriteLine($"Password 2: {Security.GeneratePassword(false, true, false, true, 12)}");
    Console.WriteLine($"Password 3: {Security.GeneratePassword(true, true, false, false, 25)}");
    Console.WriteLine($"Password 4: {Security.GeneratePassword(false, false, true, true, 12)}");
    Console.WriteLine($"Password 5: {Security.GeneratePassword(true, false, false, false, 12)}");
    Console.WriteLine($"Password 6: {Security.GeneratePassword(false, true, false, false, 12)}");
    Console.WriteLine($"Password with prefix: {Security.GeneratePasswordWithPrefix(Security.GeneratePassword(true, true, false, false, 25)!, "xert", "_")}");
}
