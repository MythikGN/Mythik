using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Server.Customs.Encryption
{
	internal class TwofishEncryption : TwofishBase, ICryptoTransform
	{
		public TwofishEncryption(int keyLen, ref byte[] key, ref byte[] iv, CipherMode cMode, EncryptionDirection direction)
		{
			for (int i=0;i<key.Length/4;i++)
			{
				Key[i] = (uint)( key[i*4+3]<<24) | (uint)(key[i*4+2] << 16) | (uint)(key[i*4+1] << 8) | (uint)(key[i*4+0]);
			}

			cipherMode = cMode;

			if (cipherMode == CipherMode.CBC)
			{
				for (int i=0;i<4;i++)
				{
					IV[i] = (uint)( iv[i*4+3]<<24) | (uint)(iv[i*4+2] << 16) | (uint)(iv[i*4+1] << 8) | (uint)(iv[i*4+0]);
				}
			}

			encryptionDirection = direction;
			reKey(keyLen,ref Key);
		}

		public void Dispose()
		{
		}

		public int TransformBlock(
			byte[] inputBuffer,
			int inputOffset,
			int inputCount,
			byte[] outputBuffer,
			int outputOffset
			)
		{			
			uint[] x=new uint[4];

			for (int i=0;i<4;i++)
			{
				x[i]= (uint)(inputBuffer[i*4+3+inputOffset]<<24) | (uint)(inputBuffer[i*4+2+inputOffset] << 16) | 
					(uint)(inputBuffer[i*4+1+inputOffset] << 8) | (uint)(inputBuffer[i*4+0+inputOffset]);

			}

			if (encryptionDirection == EncryptionDirection.Encrypting)
			{
				blockEncrypt(ref x);
			}
			else
			{
				blockDecrypt(ref x);
			}

			for (int i=0;i<4;i++)
			{
				outputBuffer[i*4+0+outputOffset] = b0(x[i]);
				outputBuffer[i*4+1+outputOffset] = b1(x[i]);
				outputBuffer[i*4+2+outputOffset] = b2(x[i]);
				outputBuffer[i*4+3+outputOffset] = b3(x[i]);
			}


			return inputCount;
		}

		public byte[] TransformFinalBlock(
			byte[] inputBuffer,
			int inputOffset,
			int inputCount
			)
		{
			byte[] outputBuffer;// = new byte[0];
			
			if (inputCount>0)
			{
				outputBuffer = new byte[16]; // blocksize
				uint[] x=new uint[4];

				for (int i=0;i<4;i++)
				{
					x[i]= (uint)(inputBuffer[i*4+3+inputOffset]<<24) | (uint)(inputBuffer[i*4+2+inputOffset] << 16) | 
						(uint)(inputBuffer[i*4+1+inputOffset] << 8) | (uint)(inputBuffer[i*4+0+inputOffset]);

				}

				if (encryptionDirection == EncryptionDirection.Encrypting)
				{
					blockEncrypt(ref x);
				}
				else
				{
					blockDecrypt(ref x);
				}

				for (int i=0;i<4;i++)
				{
					outputBuffer[i*4+0] = b0(x[i]);
					outputBuffer[i*4+1] = b1(x[i]);
					outputBuffer[i*4+2] = b2(x[i]);
					outputBuffer[i*4+3] = b3(x[i]);
				}
			}
			else
			{
				outputBuffer = new byte[0];
			}
			
			return outputBuffer;
		}

		private bool canReuseTransform = true;
		public bool CanReuseTransform
		{
			get
			{
				return canReuseTransform;
			}
		}

		private bool canTransformMultipleBlocks = false;
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return canTransformMultipleBlocks;
			}
		}

		public int InputBlockSize
		{
			get
			{
				return inputBlockSize;
			}
		}

		public int OutputBlockSize
		{
			get
			{
				return outputBlockSize;
			}
		}

		private EncryptionDirection encryptionDirection;
	}
}
