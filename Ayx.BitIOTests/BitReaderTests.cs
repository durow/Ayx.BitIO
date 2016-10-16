using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ayx.BitIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayx.BitIO.Tests
{
    [TestClass()]
    public class BitReaderTests
    {
        [TestMethod()]
        public void ReadByteTest()
        {
            var reader = new BitReader(TestData.TestBytes);
            var test1 = reader.ReadByte();
            Assert.AreEqual(8, reader.Position);
            Assert.AreEqual(65, test1);

            var test2 = reader.ReadByte();
            Assert.AreEqual(16, reader.Position);
            Assert.AreEqual(66, test2);

            var test3 = reader.ReadByte(24);
            Assert.AreEqual(16, reader.Position);
            Assert.AreEqual(100, test3);
        }

        [TestMethod()]
        public void ReadIntTest()
        {
            var reader = new BitReader(TestData.TestBin);
            var test1 = reader.ReadInt(5);
            Assert.AreEqual(5, reader.Position);
            Assert.AreEqual(22, test1);

            var test2 = reader.ReadInt(6);
            Assert.AreEqual(11, reader.Position);
            Assert.AreEqual(60, test2);

            var test3 = reader.ReadInt(15, 7);
            Assert.AreEqual(11, reader.Position);
            Assert.AreEqual(27, test3);
        }

        [TestMethod()]
        public void ReadBoolTest()
        {
            var reader = new BitReader(TestData.TestBin);
            var test1 = reader.ReadBool();
            Assert.AreEqual(1, reader.Position);
            Assert.AreEqual(true, test1);

            var test2 = reader.ReadBool();
            Assert.AreEqual(2, reader.Position);
            Assert.AreEqual(false, test2);

            var test3 = reader.ReadBool(15);
            Assert.AreEqual(2, reader.Position);
            Assert.AreEqual(false, test3);
        }

        [TestMethod()]
        public void ReadBinStringTest()
        {
            var reader = new BitReader(TestData.TestBin);
            var test1 = reader.ReadBinString(4);
            Assert.AreEqual(4, reader.Position);
            Assert.AreEqual("1011", test1);

            var test2 = reader.ReadBinString(5);
            Assert.AreEqual(9, reader.Position);
            Assert.AreEqual("01111", test2);

            var test3 = reader.ReadBinString(18, 6);
            Assert.AreEqual(9, reader.Position);
            Assert.AreEqual("101101", test3);
        }

        [TestMethod()]
        public void ReadCharTest()
        {
            var reader = BitReader.FromString(TestData.TestString, Encoding.ASCII);
            var test1 = reader.ReadChar(8);
            Assert.AreEqual(8, reader.Position);
            Assert.AreEqual('A', test1);

            var test2 = reader.ReadChar(8);
            Assert.AreEqual(16, reader.Position);
            Assert.AreEqual('B', test2);

            var test3 = reader.ReadChar(32, 8);
            Assert.AreEqual(16, reader.Position);
            Assert.AreEqual('E', test3);
        }

        [TestMethod()]
        public void FromHexTest()
        {
            var expected = "10100001001001000011111011111111";
            var reader = BitReader.FromHex(TestData.HexString);
            Assert.AreEqual(expected, reader.BinString.ToString());
        }
    }
}