using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using Server.Accounting;
using Server;
using Server.Network;
using Server.Misc;

namespace Server.Customs.Encryption
{
    public class Encryption : IPacketEncoder
    {
        private uint m_Seed;
        private bool m_Seeded;
        private ByteQueue m_Buffer;
        private IClientEncryption m_Encryption;
        private bool m_AlreadyRelayed;

        public Encryption()
        {
            m_AlreadyRelayed = false;
            m_Encryption = null;
            m_Buffer = new ByteQueue();
            m_Seeded = false;
            m_Seed = 0;
        }

        static public void Initialize()
        {
            if (Configuration.Enabled)
            {
                NetState.CreatedCallback = new NetStateCreatedCallback(NetStateCreated);
                PacketHandlers.Register(0xA0, 3, false, new OnPacketReceive(HookedPlayServer));

                EventSink.ClientVersionReceived += EventSink_ClientVersionReceived;
            }
        }

        static void EventSink_ClientVersionReceived(ClientVersionReceivedArgs e)
        {
            Console.WriteLine("Client version for {0}: {1}", e.State.Address, e.Version);
        }

        public static void NetStateCreated(NetState state)
        {
            state.PacketEncoder = new Encryption();
        }

        public static void HookedPlayServer(NetState state, PacketReader pvSrc)
        {
            PacketHandlers.PlayServer(state, pvSrc);

            Encryption context = (Encryption)(state.PacketEncoder);
            context.m_AlreadyRelayed = true;
        }

        public void EncodeOutgoingPacket(NetState to, ref byte[] buffer, ref int length)
        {
            if (m_Encryption != null)
            {
                m_Encryption.serverEncrypt(ref buffer, length);
                return;
            }
        }

        public void RejectNoEncryption(NetState ns)
        {
            Console.WriteLine("Client: {0}: Unencrypted client detected, disconnected", ns);

            ns.Send(new AsciiMessage(Server.Serial.MinusOne, -1, MessageType.Label, 0x35, 3, "System", "Unencrypted connections are not allowed on this server."));
            ns.Send(new AccountLoginRej(ALRReason.BadComm));
            ns.Dispose(true);
        }

        public void DecodeIncomingPacket(NetState from, ref byte[] buffer, ref int length)
        {
            if (m_Encryption != null)
            {
                if (m_AlreadyRelayed && m_Encryption is LoginEncryption)
                {
                    uint newSeed = ((((LoginEncryption)(m_Encryption)).Key1 + 1) ^ ((LoginEncryption)(m_Encryption)).Key2);

                    newSeed = ((newSeed >> 24) & 0xFF) | ((newSeed >> 8) & 0xFF00) | ((newSeed << 8) & 0xFF0000) | ((newSeed << 24) & 0xFF000000);

                    newSeed ^= m_Seed;

                    IClientEncryption newEncryption = new GameEncryption(newSeed);

                    newEncryption.clientDecrypt(ref buffer, length);

                    m_Encryption.clientDecrypt(ref buffer, length);

                    m_Encryption = newEncryption;
                    m_Seed = newSeed;

                    return;
                }

                m_Encryption.clientDecrypt(ref buffer, length);
                return;
            }

            bool handle = false;

            for (int i = 0; i < Listener.EndPoints.Length; i++)
            {
                IPEndPoint ipep = (IPEndPoint)Listener.EndPoints[i];

                if (((IPEndPoint)from.Socket.LocalEndPoint).Port == ipep.Port)
                    handle = true;
            }

            if (!handle)
            {
                m_Encryption = new NoEncryption();
                return;
            }


            m_Buffer.Enqueue(buffer, 0, length);
            length = 0;


            if (!m_Seeded)
            {
                if (m_Buffer.Length <= 3)
                {
                    Console.WriteLine("Encryption: Failed - Short Lenght");
                    return;
                }
                else if ((m_Buffer.Length == 83 || m_Buffer.Length == 21) && (m_Buffer.GetPacketID() == 239)) //New Client
                {
                    length = m_Buffer.Length;
                    byte[] m_Peek = new byte[21];
                    m_Buffer.Dequeue(m_Peek, 0, 21);

                    m_Seed = (uint)((m_Peek[1] << 24) | (m_Peek[2] << 16) | (m_Peek[3] << 8) | m_Peek[4]);
                    m_Seeded = true;

                    Buffer.BlockCopy(m_Peek, 0, buffer, 0, 21);


                    Console.WriteLine("Encryption: Passed - New Client");

                    if (length == 21)
                        return;

                    length = 21;
                }

                else if (m_Buffer.Length >= 4)
                {
                    byte[] m_Peek = new byte[4];
                    m_Buffer.Dequeue(m_Peek, 0, 4);

                    m_Seed = (uint)((m_Peek[0] << 24) | (m_Peek[1] << 16) | (m_Peek[2] << 8) | m_Peek[3]);
                    m_Seeded = true;

                    Buffer.BlockCopy(m_Peek, 0, buffer, 0, 4);
                    length = 4;

                    Console.WriteLine("Encryption: Passed - Old Client");
                }
                else
                {
                    Console.WriteLine("Encryption: Failed - It should never reach here");
                    return;
                }
            }

            if (m_Encryption == null)
            {
                int packetLength = m_Buffer.Length;
                int packetOffset = length;
                m_Buffer.Dequeue(buffer, length, packetLength);
                length += packetLength;

                if (packetLength >= 3)
                {
                    if (buffer[packetOffset] == 0xf1 && buffer[packetOffset + 1] == ((packetLength >> 8) & 0xFF) && buffer[packetOffset + 2] == (packetLength & 0xFF))
                    {
                        m_Encryption = new NoEncryption();
                        return;
                    }
                }

                if (packetLength == 62)
                {
                    if (buffer[packetOffset] == 0x80 && buffer[packetOffset + 30] == 0x00 && buffer[packetOffset + 60] == 0x00)
                    {
                        if (Configuration.AllowUnencryptedClients)
                        {
                            m_Encryption = new NoEncryption();
                        }
                        else
                        {
                            RejectNoEncryption(from);
                            from.Dispose();
                            return;
                        }
                    }
                    else
                    {
                        LoginEncryption encryption = new LoginEncryption();
                        if (encryption.init(m_Seed, buffer, packetOffset, packetLength))
                        {
                            Console.WriteLine("Client: {0}: Encrypted client detected, using keys of client {1}", from, encryption.Name);
                            m_Encryption = encryption;
                            byte[] packet = new byte[packetLength];
                            Buffer.BlockCopy(buffer, packetOffset, packet, 0, packetLength);
                            encryption.clientDecrypt(ref packet, packet.Length);
                            Buffer.BlockCopy(packet, 0, buffer, packetOffset, packetLength);
                        }
                        else
                        {
                            Console.WriteLine("Client: {0}: Detected an unknown client.", from);
                        }
                    }
                }
                else if (packetLength == 65)
                {
                    if (buffer[packetOffset] == '\x91' && buffer[packetOffset + 1] == ((m_Seed >> 24) & 0xFF) && buffer[packetOffset + 2] == ((m_Seed >> 16) & 0xFF) && buffer[packetOffset + 3] == ((m_Seed >> 8) & 0xFF) && buffer[packetOffset + 4] == (m_Seed & 0xFF))
                    {
                        if (Configuration.AllowUnencryptedClients)
                        {
                            m_Encryption = new NoEncryption();
                        }
                        else
                        {
                            RejectNoEncryption(from);
                            from.Dispose();
                        }
                    }
                    else
                    {
                        m_Encryption = new GameEncryption(m_Seed);

                        byte[] packet = new byte[packetLength];
                        Buffer.BlockCopy(buffer, packetOffset, packet, 0, packetLength);
                        m_Encryption.clientDecrypt(ref packet, packet.Length);
                        Buffer.BlockCopy(packet, 0, buffer, packetOffset, packetLength);
                    }
                }
                if (m_Encryption == null)
                {
                    Console.WriteLine("Encryption: Check - Waiting");
                    m_Buffer.Enqueue(buffer, packetOffset, packetLength);
                    length -= packetLength;
                    return;
                }
            }
        }
    }
}
