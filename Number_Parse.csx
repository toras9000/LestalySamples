#r "nuget: Lestaly, 0.100.0"
#nullable enable
using Lestaly;

WriteLine($"TryParseHex : 0x1234   => (uint) {"0x1234".TryParseHex<uint>()}");
WriteLine($"TryParseHex : 0x1234   => (byte) {"0x1234".TryParseHex<byte>()}");
WriteLine($"TryParseHex :   1234h  => (uint) {"1234h".TryParseHex<uint>()}");
WriteLine($"TryParseHex :   1234   => (uint) {"1234".TryParseHex<uint>()}");
WriteLine();
WriteLine($"TryParseHexNumber :   1234 => {"1234".TryParseHexNumber<uint>()}");
WriteLine($"TryParseHexNumber : 0x1234 => {"0x1234".TryParseHexNumber<uint>()}");
WriteLine();
WriteLine($"TryParseBin : 0b11010101101   => (uint) {"0b11010101101".TryParseBin<uint>()}");
WriteLine($"TryParseBin : 0b11010101101   => (byte) {"0b11010101101".TryParseBin<byte>()}");
WriteLine($"TryParseBin : 0b00000101101   => (uint) {"0b00000101101".TryParseBin<uint>()}");
WriteLine($"TryParseBin : 0b00000101101   => (byte) {"0b00000101101".TryParseBin<byte>()}");
WriteLine($"TryParseBin : 0b110_1010_1101 => (uint) {"0b110_1010_1101".TryParseBin<uint>()}");
WriteLine();
WriteLine($"TryParseBinNumber :   11010101101 => {"11010101101".TryParseBinNumber<uint>()}");
WriteLine($"TryParseBinNumber : 0b11010101101 => {"0b11010101101".TryParseBinNumber<uint>()}");
WriteLine();
WriteLine($"TryParseNumberWithPrefix :   123 => {"123".TryParseNumberWithPrefix<uint>()}");
WriteLine($"TryParseNumberWithPrefix : 0x123 => {"0x123".TryParseNumberWithPrefix<uint>()}");
WriteLine($"TryParseNumberWithPrefix :   110 => {"110".TryParseNumberWithPrefix<uint>()}");
WriteLine($"TryParseNumberWithPrefix : 0b110 => {"0b110".TryParseNumberWithPrefix<uint>()}");
WriteLine();
WriteLine($"TryParseHumanized :  123K      => {"123K".TryParseHumanized()}");
WriteLine($"TryParseHumanized :  123M      => {"123M".TryParseHumanized()}");
WriteLine($"TryParseHumanized :  123M (si) => {"123M".TryParseHumanized(si: true)}");
