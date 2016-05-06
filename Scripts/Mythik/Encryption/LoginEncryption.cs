using System;
using System.Collections;

namespace Server.Customs.Encryption
{
    public class LoginEncryption : IClientEncryption
    {
        private String name;
        private uint key1;
        private uint key2;
        private uint table1;
        private uint table2;

        public String Name
        {
            get
            {
                return name;
            }
        }

        public uint Key1
        {
            get
            {
                return key1;
            }
        }

        public uint Key2
        {
            get
            {
                return key2;
            }
        }

        public bool init(uint seed, byte[] buffer, int offset, int length)
        {
            if (length < 62)
                return false;


            byte[] packet = new byte[62];

            uint orgTable1 = (((~seed) ^ 0x00001357) << 16) | ((seed ^ 0xffffaaaa) & 0x0000ffff);
            uint orgTable2 = ((seed ^ 0x43210000) >> 16) | (((~seed) ^ 0xabcdffff) & 0xffff0000);

            for (int i = 0; i < Configuration.LoginKeys.Length; ++i)
            {
                Buffer.BlockCopy(buffer, offset, packet, 0, 62);
                table1 = orgTable1;
                table2 = orgTable2;
                key1 = Configuration.LoginKeys[i].Key1;
                key2 = Configuration.LoginKeys[i].Key2;

                clientDecrypt(ref packet, packet.Length);

                if (packet[0] == 0x80 && packet[30] == 0x00 && packet[60] == 0x00)
                {
                    bool valid = true;

                    for (int j = 1; j < 31; j++)
                        if ((!char.IsLetterOrDigit((char)packet[j]) && packet[j] != 0) || (!char.IsLetterOrDigit((char)packet[j+30]) && packet[j+30] != 0))
                        {
                            valid = false;
                            break;
                        }

                    if (!valid) continue; //need to continue the outer loop

                    table1 = orgTable1;
                    table2 = orgTable2;
                    key1 = Configuration.LoginKeys[i].Key1;
                    key2 = Configuration.LoginKeys[i].Key2;
                    name = Configuration.LoginKeys[i].Name;
                    return true;
                }
            }

            return false;
        }

        public void init(uint seed, uint key1, uint key2)
        {
            table1 = (((~seed) ^ 0x00001357) << 16) | ((seed ^ 0xffffaaaa) & 0x0000ffff);
            table2 = ((seed ^ 0x43210000) >> 16) | (((~seed) ^ 0xabcdffff) & 0xffff0000);
            this.key1 = key1;
            this.key2 = key2;
        }

        public void serverEncrypt(ref byte[] buffer, int length)
        {
        }

        public void clientDecrypt(ref byte[] buffer, int length)
        {
            uint eax, ecx, edx, esi;
            for (int i = 0; i < length; ++i)
            {
                buffer[i] = (byte)(buffer[i] ^ (byte)(table1 & 0xFF));
                edx = table2;
                esi = table1 << 31;
                eax = table2 >> 1;
                eax |= esi;
                eax ^= key1 - 1;
                edx <<= 31;
                eax >>= 1;
                ecx = table1 >> 1;
                eax |= esi;
                ecx |= edx;
                eax ^= key1;
                ecx ^= key2;
                table1 = ecx;
                table2 = eax;
            }
        }
    }
}
