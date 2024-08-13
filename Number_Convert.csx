#r "nuget: Lestaly, 0.67.0"
#nullable enable
using Lestaly;

Console.WriteLine($"ToHumanize : 1024 => {1024.ToHumanize(si: false)}");
Console.WriteLine($"ToHumanize : 1024 => {1024.ToHumanize(si: true)}");
Console.WriteLine($"ToHumanize : 12345678 => {12345678.ToHumanize(si: false)}");
Console.WriteLine($"ToHumanize : 12345678 => {12345678.ToHumanize(si: true)}");
Console.WriteLine();
Console.WriteLine($"ToBinaryString : 65535 => {65535.ToBinaryString()}");
Console.WriteLine($"ToBinaryString : 65535 => {((ushort)65535).ToBinaryString()}");
Console.WriteLine($"ToBinaryString : 65535 => {((ushort)65535).ToBinaryString("B", sepa: 4)}");
Console.WriteLine();
Console.WriteLine($"ToHexString : 65535 => {65535.ToHexString()}");
Console.WriteLine($"ToHexString : 65535 => {((uint)65535).ToHexString()}");
Console.WriteLine($"ToHexString : 65535 => {((uint)65535).ToHexString("", sepa: 0)}");
