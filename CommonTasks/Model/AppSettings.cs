namespace CommonTasks.Model;

public struct AppSettings
{
    public AESSettings AES { get; set; }

    public struct AESSettings
    {
        public string PublicKey { get; set; }
        public string SecretKey { get; set; }
    }
}
