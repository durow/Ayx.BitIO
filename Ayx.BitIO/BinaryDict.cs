/*
 * Author:durow
 * Description:store the binary of byte
 * Date:2021.02.22
 */

namespace Ayx.BitIO
{
    public class BinaryDict
    {
        private byte[][] binArray = new byte[256][];
        public bool IsInverse { get; private set; }

        public BinaryDict(bool isInverse = false)
        {
            IsInverse = isInverse;
            InitDict();
        }

        private void InitDict()
        {
            for (int i = 0; i < 256; i++)
            {
                binArray[i] = new byte[8];
                var num = i;

                for (int j = 0; j < 8; j++)
                {
                    var bit = num & 128;

                    if (IsInverse)
                        binArray[i][j] = (byte)(bit == 0 ? 1 : 0);
                    else
                        binArray[i][j] = (byte)(bit == 0 ? 0 : 1);

                    num <<= 1;
                }
            }
        }

        /// <summary>
        /// Get bin by byte and bit position
        /// </summary>
        /// <param name="b">byte number</param>
        /// <param name="binIndex">bit position</param>
        /// <returns></returns>
        public byte GetBit(int b, int binIndex)
        {
            return binArray[b][binIndex];
        }

        /// <summary>
        /// Get binary of byte
        /// </summary>
        /// <param name="b">byte number</param>
        /// <returns></returns>
        public byte[] GetBit(int b)
        {
            return binArray[b];
        }
    }
}
