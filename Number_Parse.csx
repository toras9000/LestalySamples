#r "nuget: Lestaly, 0.62.0"
#nullable enable
using Lestaly;

Console.WriteLine($"TryParseHex : 0x1234   => (uint) {"0x1234".TryParseHex<uint>()}");
Console.WriteLine($"TryParseHex : 0x1234   => (byte) {"0x1234".TryParseHex<byte>()}");
Console.WriteLine($"TryParseHex :   1234h  => (uint) {"1234h".TryParseHex<uint>()}");
Console.WriteLine($"TryParseHex :   1234   => (uint) {"1234".TryParseHex<uint>()}");
Console.WriteLine();
Console.WriteLine($"TryParseHexNumber :   1234 => {"1234".TryParseHexNumber<uint>()}");
Console.WriteLine($"TryParseHexNumber : 0x1234 => {"0x1234".TryParseHexNumber<uint>()}");
Console.WriteLine();
Console.WriteLine($"TryParseBin : 0b11010101101   => (uint) {"0b11010101101".TryParseBin<uint>()}");
Console.WriteLine($"TryParseBin : 0b11010101101   => (byte) {"0b11010101101".TryParseBin<byte>()}");
Console.WriteLine($"TryParseBin : 0b00000101101   => (uint) {"0b00000101101".TryParseBin<uint>()}");
Console.WriteLine($"TryParseBin : 0b00000101101   => (byte) {"0b00000101101".TryParseBin<byte>()}");
Console.WriteLine($"TryParseBin : 0b110_1010_1101 => (uint) {"0b110_1010_1101".TryParseBin<uint>()}");
Console.WriteLine();
Console.WriteLine($"TryParseBinNumber :   11010101101 => {"11010101101".TryParseBinNumber<uint>()}");
Console.WriteLine($"TryParseBinNumber : 0b11010101101 => {"0b11010101101".TryParseBinNumber<uint>()}");
Console.WriteLine();
Console.WriteLine($"TryParseNumberWithPrefix :   123 => {"123".TryParseNumberWithPrefix<uint>()}");
Console.WriteLine($"TryParseNumberWithPrefix : 0x123 => {"0x123".TryParseNumberWithPrefix<uint>()}");
Console.WriteLine($"TryParseNumberWithPrefix :   110 => {"110".TryParseNumberWithPrefix<uint>()}");
Console.WriteLine($"TryParseNumberWithPrefix : 0b110 => {"0b110".TryParseNumberWithPrefix<uint>()}");
