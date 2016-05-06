using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Server.Customs.Encryption
{
    internal class TwofishBase
    {
        public enum EncryptionDirection
        {
            Encrypting,
            Decrypting
        }

        public TwofishBase()
        {
        }

        protected int inputBlockSize = BLOCK_SIZE / 8;
        protected int outputBlockSize = BLOCK_SIZE / 8;

        private static uint f32(uint x, ref uint[] k32, int keyLen)
        {
            byte[] b = { b0(x), b1(x), b2(x), b3(x) };

            switch (((keyLen + 63) / 64) & 3)
            {
                case 0:
                    b[0] = (byte)(P8x8[P_04, b[0]] ^ b0(k32[3]));
                    b[1] = (byte)(P8x8[P_14, b[1]] ^ b1(k32[3]));
                    b[2] = (byte)(P8x8[P_24, b[2]] ^ b2(k32[3]));
                    b[3] = (byte)(P8x8[P_34, b[3]] ^ b3(k32[3]));
                    goto case 3;
                case 3:
                    b[0] = (byte)(P8x8[P_03, b[0]] ^ b0(k32[2]));
                    b[1] = (byte)(P8x8[P_13, b[1]] ^ b1(k32[2]));
                    b[2] = (byte)(P8x8[P_23, b[2]] ^ b2(k32[2]));
                    b[3] = (byte)(P8x8[P_33, b[3]] ^ b3(k32[2]));
                    goto case 2;
                case 2:
                    b[0] = P8x8[P_00, P8x8[P_01, P8x8[P_02, b[0]] ^ b0(k32[1])] ^ b0(k32[0])];
                    b[1] = P8x8[P_10, P8x8[P_11, P8x8[P_12, b[1]] ^ b1(k32[1])] ^ b1(k32[0])];
                    b[2] = P8x8[P_20, P8x8[P_21, P8x8[P_22, b[2]] ^ b2(k32[1])] ^ b2(k32[0])];
                    b[3] = P8x8[P_30, P8x8[P_31, P8x8[P_32, b[3]] ^ b3(k32[1])] ^ b3(k32[0])];
                    break;
            }

            return (uint)((M00(b[0]) ^ M01(b[1]) ^ M02(b[2]) ^ M03(b[3]))) ^
            (uint)((M10(b[0]) ^ M11(b[1]) ^ M12(b[2]) ^ M13(b[3])) << 8) ^
            (uint)((M20(b[0]) ^ M21(b[1]) ^ M22(b[2]) ^ M23(b[3])) << 16) ^
            (uint)((M30(b[0]) ^ M31(b[1]) ^ M32(b[2]) ^ M33(b[3])) << 24);
        }

        protected bool reKey(int keyLen, ref uint[] key32)
        {
            int i, k64Cnt;
            keyLength = keyLen;
            rounds = numRounds[(keyLen - 1) / 64];
            int subkeyCnt = ROUND_SUBKEYS + 2 * rounds;
            uint A, B;
            uint[] k32e = new uint[MAX_KEY_BITS / 64];
            uint[] k32o = new uint[MAX_KEY_BITS / 64];

            k64Cnt = (keyLen + 63) / 64;
            for (i = 0; i < k64Cnt; i++)
            {
                k32e[i] = key32[2 * i];
                k32o[i] = key32[2 * i + 1];
                sboxKeys[k64Cnt - 1 - i] = RS_MDS_Encode(k32e[i], k32o[i]);
            }

            for (i = 0; i < subkeyCnt / 2; i++)
            {
                A = f32((uint)(i * SK_STEP), ref k32e, keyLen);
                B = f32((uint)(i * SK_STEP + SK_BUMP), ref k32o, keyLen);
                B = ROL(B, 8);
                subKeys[2 * i] = A + B;
                subKeys[2 * i + 1] = ROL(A + 2 * B, SK_ROTL);
            }

            return true;
        }

        public void blockDecrypt(ref uint[] x)
        {
            uint t0, t1;
            uint[] xtemp = new uint[4];

            if (cipherMode == CipherMode.CBC)
            {
                x.CopyTo(xtemp, 0);
            }

            for (int i = 0; i < BLOCK_SIZE / 32; i++)
                x[i] ^= subKeys[OUTPUT_WHITEN + i];

            for (int r = rounds - 1; r >= 0; r--)
            {
                t0 = f32(x[0], ref sboxKeys, keyLength);
                t1 = f32(ROL(x[1], 8), ref sboxKeys, keyLength);

                x[2] = ROL(x[2], 1);
                x[2] ^= t0 + t1 + subKeys[ROUND_SUBKEYS + 2 * r];
                x[3] ^= t0 + 2 * t1 + subKeys[ROUND_SUBKEYS + 2 * r + 1];
                x[3] = ROR(x[3], 1);

                if (r > 0)
                {
                    t0 = x[0]; x[0] = x[2]; x[2] = t0;
                    t1 = x[1]; x[1] = x[3]; x[3] = t1;
                }
            }

            for (int i = 0; i < BLOCK_SIZE / 32; i++)
            {
                x[i] ^= subKeys[INPUT_WHITEN + i];
                if (cipherMode == CipherMode.CBC)
                {
                    x[i] ^= IV[i];
                    IV[i] = xtemp[i];
                }
            }
        }

        public void blockEncrypt(ref uint[] x)
        {
            uint t0, t1, tmp;

            for (int i = 0; i < BLOCK_SIZE / 32; i++)
            {
                x[i] ^= subKeys[INPUT_WHITEN + i];
                if (cipherMode == CipherMode.CBC)
                    x[i] ^= IV[i];
            }

            for (int r = 0; r < rounds; r++)
            {
                t0 = f32(x[0], ref sboxKeys, keyLength);
                t1 = f32(ROL(x[1], 8), ref sboxKeys, keyLength);

                x[3] = ROL(x[3], 1);
                x[2] ^= t0 + t1 + subKeys[ROUND_SUBKEYS + 2 * r];
                x[3] ^= t0 + 2 * t1 + subKeys[ROUND_SUBKEYS + 2 * r + 1];
                x[2] = ROR(x[2], 1);

                if (r < rounds - 1)
                {
                    tmp = x[0]; x[0] = x[2]; x[2] = tmp;
                    tmp = x[1]; x[1] = x[3]; x[3] = tmp;
                }
            }

            for (int i = 0; i < BLOCK_SIZE / 32; i++)
            {
                x[i] ^= subKeys[OUTPUT_WHITEN + i];
                if (cipherMode == CipherMode.CBC)
                {
                    IV[i] = x[i];
                }
            }

        }

        private int[] numRounds = { 0, ROUNDS_128, ROUNDS_192, ROUNDS_256 };


        static private uint RS_MDS_Encode(uint k0, uint k1)
        {
            uint i, j;
            uint r;

            for (i = r = 0; i < 2; i++)
            {
                r ^= (i > 0) ? k0 : k1;
                for (j = 0; j < 4; j++)
                    RS_rem(ref r);
            }
            return r;
        }

        protected uint[] sboxKeys = new uint[MAX_KEY_BITS / 64];
        protected uint[] subKeys = new uint[TOTAL_SUBKEYS];
        protected uint[] Key = { 0, 0, 0, 0, 0, 0, 0, 0 };
        protected uint[] IV = { 0, 0, 0, 0 };
        private int keyLength;
        private int rounds;
        protected CipherMode cipherMode = CipherMode.ECB;

        static private readonly int BLOCK_SIZE = 128;
        static private readonly int MAX_ROUNDS = 16;
        static private readonly int ROUNDS_128 = 16;
        static private readonly int ROUNDS_192 = 16;
        static private readonly int ROUNDS_256 = 16;
        static private readonly int MAX_KEY_BITS = 256;
        static private readonly int MIN_KEY_BITS = 128;

        static private readonly int INPUT_WHITEN = 0;
        static private readonly int OUTPUT_WHITEN = (INPUT_WHITEN + BLOCK_SIZE / 32);
        static private readonly int ROUND_SUBKEYS = (OUTPUT_WHITEN + BLOCK_SIZE / 32);
        static private readonly int TOTAL_SUBKEYS = (ROUND_SUBKEYS + 2 * MAX_ROUNDS);

        static private readonly uint SK_STEP = 0x02020202u;
        static private readonly uint SK_BUMP = 0x01010101u;
        static private readonly int SK_ROTL = 9;

        static private readonly uint RS_GF_FDBK = 0x14D;
        static private void RS_rem(ref uint x)
        {
            byte b = (byte)(x >> 24);
            uint g2 = (uint)(((b << 1) ^ (((b & 0x80) == 0x80) ? RS_GF_FDBK : 0)) & 0xFF);
            uint g3 = (uint)(((b >> 1) & 0x7F) ^ (((b & 1) == 1) ? RS_GF_FDBK >> 1 : 0) ^ g2);
            x = (x << 8) ^ (g3 << 24) ^ (g2 << 16) ^ (g3 << 8) ^ b;
        }

        static private readonly int MDS_GF_FDBK = 0x169;	/* primitive polynomial for GF(256)*/
        static private int LFSR1(int x)
        {
            return (((x) >> 1) ^ ((((x) & 0x01) == 0x01) ? MDS_GF_FDBK / 2 : 0));
        }

        static private int LFSR2(int x)
        {
            return (((x) >> 2) ^ ((((x) & 0x02) == 0x02) ? MDS_GF_FDBK / 2 : 0) ^
                ((((x) & 0x01) == 0x01) ? MDS_GF_FDBK / 4 : 0));
        }

        static private int Mx_1(int x)
        {
            return x;
        }

        static private int Mx_X(int x)
        {
            return x ^ LFSR2(x);
        }

        static private int Mx_Y(int x)
        {
            return x ^ LFSR1(x) ^ LFSR2(x);
        }

        static private int M00(int x)
        {
            return Mul_1(x);
        }
        static private int M01(int x)
        {
            return Mul_Y(x);
        }
        static private int M02(int x)
        {
            return Mul_X(x);
        }
        static private int M03(int x)
        {
            return Mul_X(x);
        }

        static private int M10(int x)
        {
            return Mul_X(x);
        }
        static private int M11(int x)
        {
            return Mul_Y(x);
        }
        static private int M12(int x)
        {
            return Mul_Y(x);
        }
        static private int M13(int x)
        {
            return Mul_1(x);
        }

        static private int M20(int x)
        {
            return Mul_Y(x);
        }
        static private int M21(int x)
        {
            return Mul_X(x);
        }
        static private int M22(int x)
        {
            return Mul_1(x);
        }
        static private int M23(int x)
        {
            return Mul_Y(x);
        }

        static private int M30(int x)
        {
            return Mul_Y(x);
        }
        static private int M31(int x)
        {
            return Mul_1(x);
        }
        static private int M32(int x)
        {
            return Mul_Y(x);
        }
        static private int M33(int x)
        {
            return Mul_X(x);
        }

        static private int Mul_1(int x)
        {
            return Mx_1(x);
        }
        static private int Mul_X(int x)
        {
            return Mx_X(x);
        }
        static private int Mul_Y(int x)
        {
            return Mx_Y(x);
        }

        static private readonly int P_00 = 1;
        static private readonly int P_01 = 0;
        static private readonly int P_02 = 0;
        static private readonly int P_03 = (P_01 ^ 1);
        static private readonly int P_04 = 1;

        static private readonly int P_10 = 0;
        static private readonly int P_11 = 0;
        static private readonly int P_12 = 1;
        static private readonly int P_13 = (P_11 ^ 1);
        static private readonly int P_14 = 0;

        static private readonly int P_20 = 1;
        static private readonly int P_21 = 1;
        static private readonly int P_22 = 0;
        static private readonly int P_23 = (P_21 ^ 1);
        static private readonly int P_24 = 0;

        static private readonly int P_30 = 0;
        static private readonly int P_31 = 1;
        static private readonly int P_32 = 1;
        static private readonly int P_33 = (P_31 ^ 1);
        static private readonly int P_34 = 1;

        static private byte[,] P8x8 = 
		{
				{
				0xA9, 0x67, 0xB3, 0xE8, 0x04, 0xFD, 0xA3, 0x76, 
				0x9A, 0x92, 0x80, 0x78, 0xE4, 0xDD, 0xD1, 0x38, 
				0x0D, 0xC6, 0x35, 0x98, 0x18, 0xF7, 0xEC, 0x6C, 
				0x43, 0x75, 0x37, 0x26, 0xFA, 0x13, 0x94, 0x48, 
				0xF2, 0xD0, 0x8B, 0x30, 0x84, 0x54, 0xDF, 0x23, 
				0x19, 0x5B, 0x3D, 0x59, 0xF3, 0xAE, 0xA2, 0x82, 
				0x63, 0x01, 0x83, 0x2E, 0xD9, 0x51, 0x9B, 0x7C, 
				0xA6, 0xEB, 0xA5, 0xBE, 0x16, 0x0C, 0xE3, 0x61, 
				0xC0, 0x8C, 0x3A, 0xF5, 0x73, 0x2C, 0x25, 0x0B, 
				0xBB, 0x4E, 0x89, 0x6B, 0x53, 0x6A, 0xB4, 0xF1, 
				0xE1, 0xE6, 0xBD, 0x45, 0xE2, 0xF4, 0xB6, 0x66, 
				0xCC, 0x95, 0x03, 0x56, 0xD4, 0x1C, 0x1E, 0xD7, 
				0xFB, 0xC3, 0x8E, 0xB5, 0xE9, 0xCF, 0xBF, 0xBA, 
				0xEA, 0x77, 0x39, 0xAF, 0x33, 0xC9, 0x62, 0x71, 
				0x81, 0x79, 0x09, 0xAD, 0x24, 0xCD, 0xF9, 0xD8, 
				0xE5, 0xC5, 0xB9, 0x4D, 0x44, 0x08, 0x86, 0xE7, 
				0xA1, 0x1D, 0xAA, 0xED, 0x06, 0x70, 0xB2, 0xD2, 
				0x41, 0x7B, 0xA0, 0x11, 0x31, 0xC2, 0x27, 0x90, 
				0x20, 0xF6, 0x60, 0xFF, 0x96, 0x5C, 0xB1, 0xAB, 
				0x9E, 0x9C, 0x52, 0x1B, 0x5F, 0x93, 0x0A, 0xEF, 
				0x91, 0x85, 0x49, 0xEE, 0x2D, 0x4F, 0x8F, 0x3B, 
				0x47, 0x87, 0x6D, 0x46, 0xD6, 0x3E, 0x69, 0x64, 
				0x2A, 0xCE, 0xCB, 0x2F, 0xFC, 0x97, 0x05, 0x7A, 
				0xAC, 0x7F, 0xD5, 0x1A, 0x4B, 0x0E, 0xA7, 0x5A, 
				0x28, 0x14, 0x3F, 0x29, 0x88, 0x3C, 0x4C, 0x02, 
				0xB8, 0xDA, 0xB0, 0x17, 0x55, 0x1F, 0x8A, 0x7D, 
				0x57, 0xC7, 0x8D, 0x74, 0xB7, 0xC4, 0x9F, 0x72, 
				0x7E, 0x15, 0x22, 0x12, 0x58, 0x07, 0x99, 0x34, 
				0x6E, 0x50, 0xDE, 0x68, 0x65, 0xBC, 0xDB, 0xF8, 
				0xC8, 0xA8, 0x2B, 0x40, 0xDC, 0xFE, 0x32, 0xA4, 
				0xCA, 0x10, 0x21, 0xF0, 0xD3, 0x5D, 0x0F, 0x00, 
				0x6F, 0x9D, 0x36, 0x42, 0x4A, 0x5E, 0xC1, 0xE0
			},
			{
				0x75, 0xF3, 0xC6, 0xF4, 0xDB, 0x7B, 0xFB, 0xC8, 
				0x4A, 0xD3, 0xE6, 0x6B, 0x45, 0x7D, 0xE8, 0x4B, 
				0xD6, 0x32, 0xD8, 0xFD, 0x37, 0x71, 0xF1, 0xE1, 
				0x30, 0x0F, 0xF8, 0x1B, 0x87, 0xFA, 0x06, 0x3F, 
				0x5E, 0xBA, 0xAE, 0x5B, 0x8A, 0x00, 0xBC, 0x9D, 
				0x6D, 0xC1, 0xB1, 0x0E, 0x80, 0x5D, 0xD2, 0xD5, 
				0xA0, 0x84, 0x07, 0x14, 0xB5, 0x90, 0x2C, 0xA3, 
				0xB2, 0x73, 0x4C, 0x54, 0x92, 0x74, 0x36, 0x51, 
				0x38, 0xB0, 0xBD, 0x5A, 0xFC, 0x60, 0x62, 0x96, 
				0x6C, 0x42, 0xF7, 0x10, 0x7C, 0x28, 0x27, 0x8C, 
				0x13, 0x95, 0x9C, 0xC7, 0x24, 0x46, 0x3B, 0x70, 
				0xCA, 0xE3, 0x85, 0xCB, 0x11, 0xD0, 0x93, 0xB8, 
				0xA6, 0x83, 0x20, 0xFF, 0x9F, 0x77, 0xC3, 0xCC, 
				0x03, 0x6F, 0x08, 0xBF, 0x40, 0xE7, 0x2B, 0xE2, 
				0x79, 0x0C, 0xAA, 0x82, 0x41, 0x3A, 0xEA, 0xB9, 
				0xE4, 0x9A, 0xA4, 0x97, 0x7E, 0xDA, 0x7A, 0x17, 
				0x66, 0x94, 0xA1, 0x1D, 0x3D, 0xF0, 0xDE, 0xB3, 
				0x0B, 0x72, 0xA7, 0x1C, 0xEF, 0xD1, 0x53, 0x3E, 
				0x8F, 0x33, 0x26, 0x5F, 0xEC, 0x76, 0x2A, 0x49, 
				0x81, 0x88, 0xEE, 0x21, 0xC4, 0x1A, 0xEB, 0xD9, 
				0xC5, 0x39, 0x99, 0xCD, 0xAD, 0x31, 0x8B, 0x01, 
				0x18, 0x23, 0xDD, 0x1F, 0x4E, 0x2D, 0xF9, 0x48, 
				0x4F, 0xF2, 0x65, 0x8E, 0x78, 0x5C, 0x58, 0x19, 
				0x8D, 0xE5, 0x98, 0x57, 0x67, 0x7F, 0x05, 0x64, 
				0xAF, 0x63, 0xB6, 0xFE, 0xF5, 0xB7, 0x3C, 0xA5, 
				0xCE, 0xE9, 0x68, 0x44, 0xE0, 0x4D, 0x43, 0x69, 
				0x29, 0x2E, 0xAC, 0x15, 0x59, 0xA8, 0x0A, 0x9E, 
				0x6E, 0x47, 0xDF, 0x34, 0x35, 0x6A, 0xCF, 0xDC, 
				0x22, 0xC9, 0xC0, 0x9B, 0x89, 0xD4, 0xED, 0xAB, 
				0x12, 0xA2, 0x0D, 0x52, 0xBB, 0x02, 0x2F, 0xA9, 
				0xD7, 0x61, 0x1E, 0xB4, 0x50, 0x04, 0xF6, 0xC2, 
				0x16, 0x25, 0x86, 0x56, 0x55, 0x09, 0xBE, 0x91
			}
		};

        private static uint ROL(uint x, int n)
        {
            return (((x) << ((n) & 0x1F)) | (x) >> (32 - ((n) & 0x1F)));
        }
        private static uint ROR(uint x, int n)
        {
            return (((x) >> ((n) & 0x1F)) | ((x) << (32 - ((n) & 0x1F))));
        }
        protected static byte b0(uint x)
        {
            return (byte)(x);
        }
        protected static byte b1(uint x)
        {
            return (byte)((x >> 8));
        }
        protected static byte b2(uint x)
        {
            return (byte)((x >> 16));
        }
        protected static byte b3(uint x)
        {
            return (byte)((x >> 24));
        }
    }
}
