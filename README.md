# Ayx.BitIO
BitReader and BitWriter to read and write bit stream

## Install package
```
PM>Install-Package Ayx.BitIO
```

## Create a BitReader and read data
You can create a BitReader from bytes[],raw binary string,string with encoding and hex string.
``` C#
byte[] buff = { 12, 23, 4, 125 };
var reader1 = new BitReader(buff);
var reader2 = new  BitReader("1010010110100101");
var reader3 = BitReader.FromHexString("E2F12325A6FF");
var reader4 = BitReader.FromString("This is a test string",Encoding.ASCII);
```

If you use ReadXXX method without offset parameter, read start offset is reader.Position and reader.Position will auto moved.
``` C#
var reader = new BitReader("100101001001001100");
var i = reader.ReadInt(3); //i==4, and reader.Position add 3, reader.Position == 3
var b = reader.ReadBool(); //b==true, and reader.Position add 1, reader.Position == 4
var c = reader.ReadChar(8); //read 8bit as a character, c=='I', reader.Position == 12
var bin = reader.ReadBinString(5); //bin=="00110", reader.Position == 17
```

If you use ReadXXX method with offset parameter, reader.Position will not move automatically.
``` C#
var reader = new BitReader("100101001001001100");
var i = reader.ReadInt(6,3); //i==1, reader.Position == 0
var b = reader.ReadBool(5); //b==true, reader.Position == 0
var c = reader.ReadChar(11,7); //read 7bit as a character, c=='L', reader.Position == 0
var bin = reader.ReadBinString(6,5); //bin=="00100", reader.Position == 0
```
## Create a BitWriter and write data
Create a BitWriter is very simple
``` C#
var writer = new BitWriter();
```

Write data is simple too
``` C#
writer.WriteInt(25,6); //write 25 to 6bit, it is "011001"
writer.WriteChar('A',7); //write character A to 7bit, it is "1000001"
writer.WriteBool(true); //write true to 1 bit, it is "1"
writer.WriteBinaryString("10010"); //write raw binary string
//at this time,the data in writer is "0110011000001110010"
```
After write some data,you can get the result you want
``` C#
var rawBin = reader.BinString.ToString(); //get the binary string, result is:"0110011000001110010"
var bin = reader.GetBinaryString(); //get binary string with 8bit align,result is:"011001100000111001000000"
var buff = reader.GetBytes(); //get 8bit aligned binary string to bytes, result is: { 102, 14, 64 }
```
