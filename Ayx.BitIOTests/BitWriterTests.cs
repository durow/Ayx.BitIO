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
    public class BitWriterTests
    {
        [TestMethod()]
        public void WriteByteTest()
        {
            var writer = new BitWriter();
            writer.WriteByte(65);
            writer.WriteByte(53, 7);

            Assert.AreEqual(15, writer.Length);
            Assert.AreEqual("010000010110101", writer.BinString.ToString());
        }

        [TestMethod()]
        public void WriteIntTest()
        {
            var writer = new BitWriter();
            writer.WriteInt(154);
            writer.WriteInt(287, 12);

            Assert.AreEqual(28, writer.Length);
            Assert.AreEqual("0000000010011010000100011111", writer.BinString.ToString());
        }

        [TestMethod()]
        public void WriteCharTest()
        {
            var writer = new BitWriter();
            writer.WriteChar('A');
            writer.WriteChar('B', 8);

            Assert.AreEqual(15, writer.Length);
            Assert.AreEqual("100000101000010", writer.BinString.ToString());
        }

        [TestMethod()]
        public void WriteBoolTest()
        {
            var writer = new BitWriter();
            writer.WriteBool(false);
            writer.WriteBool(true, 2);

            Assert.AreEqual(3, writer.Length);
            Assert.AreEqual("001", writer.BinString.ToString());
        }

        [TestMethod()]
        public void WriteBinaryStringTest()
        {
            var writer = new BitWriter();
            writer.WriteBinaryString("10010");
            writer.WriteBinaryString("00101");

            Assert.AreEqual("1001000101", writer.BinString.ToString());
        }

        [TestMethod()]
        public void GetBytesTest()
        {
            var writer = new BitWriter();
            writer.WriteBinaryString("10010");
            writer.WriteBinaryString("00101");
            var test = writer.GetBytes();

            Assert.AreEqual(145, test[0]);
            Assert.AreEqual(64, test[1]);
        }

        [TestMethod()]
        public void GetBinStringTest()
        {
            var writer = new BitWriter();
            writer.WriteBinaryString("10010");
            writer.WriteBinaryString("00101");
            var test = writer.GetBinString();

            Assert.AreEqual("1001000101000000", test);
        }
    }
}