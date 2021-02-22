/*
 * Author:durow
 * Description:Read bit by stream
 * Date:2021.02.22
 */

using System;
using System.IO;

namespace Ayx.BitIO
{
    public class BitStreamReader : IDisposable
    {
        private BinaryDict dict;
        private int offset;
        private byte[] bitArray;
        private long streamLen;

        public Stream BaseStream { get; private set; }
        public bool IsInverse => dict.IsInverse;
        public bool CanRead => offset != 0 || BaseStream.Position != streamLen;
        public long BitLength { get; private set; }
        public long BitPosition
        {
            get
            {
                if (BaseStream.Length == 0 && offset == 0)
                    return 0;

                return (BaseStream.Position - 1) * 8 + offset;
            }
            set
            {
                BaseStream.Position = value / 8;
                offset = (int)(value % 8);
                if (offset != 0)
                    UpdateBitArray();
            }
        }

        public BitStreamReader(byte[] data, bool inverse = false)
            : this(new MemoryStream(data), inverse)
        { }

        public BitStreamReader(string filename, bool inverse = false)
            :this(new FileStream(filename,FileMode.Open), inverse)
        { }

        public BitStreamReader(Stream stream, bool inverse = false)
        {
            this.BaseStream = stream;
            dict = new BinaryDict(inverse);
            streamLen = stream.Length;
            BitLength = streamLen * 8;
        }

        private void UpdateBitArray()
        {
            var b = BaseStream.ReadByte();
            bitArray = dict.GetBit(b);
        }

        /// <summary>
        /// Read 1 bit from stream
        /// </summary>
        /// <returns></returns>
        public byte ReadBit()
        {
            if (offset == 0)
                UpdateBitArray();

            var result = bitArray[offset];
            MoveBitPosition();

            return result;
        }

        /// <summary>
        /// Read bits from stream to buff
        /// </summary>
        /// <param name="buff"></param>
        /// <returns>Number of bit that actually read</returns>
        public int ReadBits(byte[] buff)
        {
            if (buff == null)
                return 0;

            for (int i = 0; i < buff.Length; i++)
            {
                if (!CanRead)
                    return i;

                buff[i] = ReadBit();
            }

            return buff.Length;
        }

        private void MoveBitPosition()
        {
            offset++;
            if (offset == 8)
                offset = 0;
        }

        /// <summary>
        /// Close the reader
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            BaseStream?.Dispose();
        }
    }
}
