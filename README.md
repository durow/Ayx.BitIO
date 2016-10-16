# Ayx.BitIO
BitReader and BitWriter to read and write bit stream

## Install package
```
PM>Install-Package Ayx.BitIO
```

## Create a reader and read data
You can create a BitReader from bytes[],raw binary string,string with encoding and hex string.
If you use ReadXXX method without offset parameter, reader.Position will auto moved.
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
