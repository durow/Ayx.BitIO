using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayx.BitIO.Tests
{
    public class TestData
    {
        public static string TestBin = "10110111100000100110110110001101";
        public static string TestString = "ABcdE12345";
        public static byte[] TestBytes = Encoding.ASCII.GetBytes(TestString);
    }
}
