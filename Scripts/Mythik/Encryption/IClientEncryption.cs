namespace Server.Customs.Encryption
{
    public interface IClientEncryption
    {
        void serverEncrypt(ref byte[] buffer, int length);

        void clientDecrypt(ref byte[] buffer, int length);
    }
}
