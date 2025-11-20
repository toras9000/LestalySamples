#r "nuget: Lestaly.General, 0.112.0"
#nullable enable
using Lestaly;

WriteLine($"ToHumanize : 1024 => {1024.ToHumanize(si: false)}");
WriteLine($"ToHumanize : 1024 => {1024.ToHumanize(si: true)}");
WriteLine($"ToHumanize : 12345678 => {12345678.ToHumanize(si: false)}");
WriteLine($"ToHumanize : 12345678 => {12345678.ToHumanize(si: true)}");
WriteLine();
WriteLine($"ToBinaryString : 65535 => {65535.ToBinaryString()}");
WriteLine($"ToBinaryString : 65535 => {((ushort)65535).ToBinaryString()}");
WriteLine($"ToBinaryString : 65535 => {((ushort)65535).ToBinaryString("B", sepa: 4)}");
WriteLine();
WriteLine($"ToHexString : 65535 => {65535.ToHexString()}");
WriteLine($"ToHexString : 65535 => {((uint)65535).ToHexString()}");
WriteLine($"ToHexString : 65535 => {((uint)65535).ToHexString("", sepa: 0)}");
