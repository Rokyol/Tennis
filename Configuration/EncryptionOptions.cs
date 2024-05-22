namespace Tennis.API.Configuration
{
    public class EncryptionOptions
    {
        public static string Section = "Application:Encryption";
        public string EncryptionKey { get; set; }
    }
}