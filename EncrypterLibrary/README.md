# Encrypter for .NET

[![NuGet Version](https://img.shields.io/nuget/v/Encrypter_NET?color=informational&logo=nuget)](https://www.nuget.org/packages/Encrypter_NET/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Encrypter_NET?color=brightgreen&logo=nuget)](https://www.nuget.org/packages/Encrypter_NET/)
[![License](https://img.shields.io/github/license/berkbb/encrypter_net?color=important)](https://www.nuget.org/packages/Encrypter_NET/)

Encrypt / Decrypt strings with given Key and IV for .NET.

## Target Framework

- .NET 10 (`net10.0`)

## Features

- Encrypt / Decrypt string data with Key and IV.
- AES-CTR mode with URL-safe Base64 output.
- Built-in random password generator.
- Prefix helper for generated passwords.

## Usage

```csharp
using EncrypterLibrary;

var key = Security.GeneratePassword(true, true, true, true, 32);
var iv = Security.GeneratePassword(true, true, true, true, 16);

var encrypted = "Hello my dear !".EncryptMyData(key!, iv!);
var decrypted = encrypted?.DecryptMyData(key!, iv!);
```

## Notes

- Key length must be exactly `32`.
- IV length must be exactly `16`.
- Invalid inputs return `null`.
