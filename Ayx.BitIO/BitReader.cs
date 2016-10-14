﻿/*
 * Author:durow
 * Date:2016.10.14
 * Description:used to read data from bit stream.
 */

using System;
using System.Text;

namespace Ayx.BitIO
{
    public class BitReader
    {
        public readonly StringBuilder BinString;

        /// <summary>
        /// current position of reader
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// number of bit
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// remain number of bit
        /// </summary>
        public int Remain
        {
            get
            {
                return Length - Position;
            }
        }

        /// <summary>
        /// create a BitReader
        /// </summary>
        /// <param name="data">data to read</param>
        public BitReader(byte[] data)
        {
            Length = data.Length * 8;
            BinString = new StringBuilder(Length);

            for (int i = 0; i < data.Length; i++)
            {
                BinString.Append(ByteToBinString(data[i]));
            }

            Position = 0;
        }

        /// <summary>
        /// read 8 bit to byte from offset
        /// </summary>
        /// <param name="offset">start position</param>
        /// <returns></returns>
        public byte ReadByte(int offset)
        {
            var bin = BinString.ToString(offset, 8);
            return Convert.ToByte(bin, 2);
        }

        /// <summary>
        /// read 8 bit to byte from Position,and move Position with 8;
        /// </summary>
        /// <returns></returns>
        public byte ReadByte()
        {
            var result = ReadByte(Position);
            Position += 8;
            return result;
        }

        /// <summary>
        /// read {bitLength} bit to int from offset
        /// </summary>
        /// <param name="offset">offset</param>
        /// <param name="bitLength">bit number</param>
        /// <returns></returns>
        public int ReadInt(int offset, int bitLength)
        {
            var bin = BinString.ToString(offset, bitLength);
            return Convert.ToInt32(bin, 2);
        }

        /// <summary>
        /// read {bitLength} bit to int from Position,and move Position with {bitLength}
        /// </summary>
        /// <param name="bitLength">number of bit</param>
        /// <returns></returns>
        public int ReadInt(int bitLength)
        {
            var result = ReadInt(Position, bitLength);
            Position += bitLength;
            return result;
        }

        /// <summary>
        /// read {bitLength} to bool from offset
        /// </summary>
        /// <param name="offset">offset</param>
        /// <returns></returns>
        public bool ReadBool(int offset, int bitLength = 1)
        {
            var result = ReadInt(offset, bitLength);
            return offset != 0;
        }

        /// <summary>
        /// read {bitLength} to bool from Position, and move Position with {bitLength}
        /// </summary>
        /// <returns></returns>
        public bool ReadBool(int bitLength = 1)
        {
            var result = ReadBool(Position, bitLength);
            Position += bitLength;
            return result;
        }

        /// <summary>
        /// read {bitLength} binary string from offset
        /// </summary>
        /// <param name="offset">offset</param>
        /// <param name="bitLength">length of binary string</param>
        /// <returns></returns>
        public string ReadBinString(int offset, int bitLength)
        {
            return BinString.ToString(offset, bitLength);
        }

        /// <summary>
        /// read {bitLength} binary string from Position, and move position with {bitLength}
        /// </summary>
        /// <param name="bitLength">length of binary string</param>
        /// <returns></returns>
        public string ReadBinString(int bitLength)
        {
            var result = ReadBinString(Position, bitLength);
            Position += bitLength;
            return result;
        }

        /// <summary>
        /// read {bitLength} to char from offset
        /// </summary>
        /// <param name="offset">offset</param>
        /// <param name="bitLength">number of bit</param>
        /// <returns></returns>
        public char ReadChar(int offset, int bitLength = 7)
        {
            var b = ReadInt(offset, bitLength);
            return Convert.ToChar(b);
        }

        /// <summary>
        /// read {bitLength} to char from Position, and move Position with {bitLength}
        /// </summary>
        /// <param name="bitLength">number of bit</param>
        /// <returns></returns>
        public char ReadChar(int bitLength = 7)
        {
            var result = ReadChar(Position, bitLength);
            Position += bitLength;
            return result;
        }

        /// <summary>
        /// convert byte to 8 bit binary string
        /// </summary>
        /// <param name="b">byte value</param>
        /// <returns>8 bit binary string</returns>
        public static char[] ByteToBinString(byte b)
        {
            var result = new char[8];

            for (int i = 0; i < 8; i++)
            {
                var temp = b & 128;
                result[i] = temp == 0 ? '0' : '1';
                b = (byte)(b << 1);
            }

            return result;
        }
    }
}
