namespace Tennis.API.Services.Encryption
{
    public interface IEncryptionService
    {
        public string Encrypt(string text);
        public string Decrypt(string text);
    }
}
