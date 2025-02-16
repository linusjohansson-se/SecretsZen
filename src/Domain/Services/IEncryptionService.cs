namespace Domain.Services;

public interface IEncryptionService
{
    string Encrypt(string textToEncrypt);

    string Decrypt(string textToDecrypt);
}
