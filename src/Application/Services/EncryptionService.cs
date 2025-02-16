using System.Security.Cryptography;
using System.Text;
using Domain.Services;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class EncryptionService : IEncryptionService
{
    private readonly byte[] _key;

    public EncryptionService(IOptions<EncryptionOptions> options)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options), "Options cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(options.Value.EncryptionKey))
        {
            throw new ArgumentException("Encryption key cannot be empty.");
        }

        _key = NormalizeKey(options.Value.EncryptionKey);
    }

    public string Encrypt(string textToEncrypt)
    {
        if (string.IsNullOrEmpty(textToEncrypt))
        {
            return textToEncrypt;
        }

        using var aes = Aes.Create();
        aes.Key = _key;
        aes.GenerateIV();

        using MemoryStream ms = new();
        ms.Write(aes.IV, 0, aes.IV.Length);

        using (CryptoStream cs = new(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
        using (StreamWriter sw = new(cs))
        {
            sw.Write(textToEncrypt);
        }

        return Convert.ToBase64String(ms.ToArray());
    }

    public string Decrypt(string textToDecrypt)
    {
        if (string.IsNullOrEmpty(textToDecrypt))
        {
            return textToDecrypt;
        }

        byte[] encryptedData = Convert.FromBase64String(textToDecrypt);

        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = encryptedData.Take(16).ToArray();

        using MemoryStream ms = new(encryptedData, 16, encryptedData.Length - 16);
        using CryptoStream cs = new(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using StreamReader sr = new(cs);

        return sr.ReadToEnd();
    }

    private static byte[] NormalizeKey(string key)
    {
        return SHA256.HashData(Encoding.UTF8.GetBytes(key));
    }
}
