using System.Buffers.Binary;

namespace Collide;

public static class Ugly
{
    public static void GuidBytesToRegularBytes(ReadOnlySpan<byte> guidBytes, Span<byte> p)
    {
        var k = guidBytes[15]; // hoist bounds checks
        var a = BinaryPrimitives.ReadInt32LittleEndian(guidBytes);
        var b = BinaryPrimitives.ReadInt16LittleEndian(guidBytes.Slice(4));
        var c = BinaryPrimitives.ReadInt16LittleEndian(guidBytes.Slice(6));
        var d = guidBytes[8];
        var e = guidBytes[9];
        var f = guidBytes[10];
        var g = guidBytes[11];
        var h = guidBytes[12];
        var i = guidBytes[13];
        var j = guidBytes[14];

        var nextLoc = HexsToChars(p, a >> 24, a >> 16, 0);
        nextLoc = HexsToChars(p, a >> 8, a, nextLoc);
        p[nextLoc++] = (byte)'-';
        nextLoc = HexsToChars(p, b >> 8, b, nextLoc);
        p[nextLoc++] = (byte)'-';
        nextLoc = HexsToChars(p, c >> 8, c, nextLoc);
        p[nextLoc++] = (byte)'-';
        nextLoc = HexsToChars(p, d, e, nextLoc);
        p[nextLoc++] = (byte)'-';
        nextLoc = HexsToChars(p, f, g, nextLoc);
        nextLoc = HexsToChars(p, h, i, nextLoc);
        HexsToChars(p, j, k, nextLoc);
    }

    public static byte ToCharLower(int value)
    {
        value &= 0xF;
        value += '0';

        if (value > '9')
        {
            value += ('a' - ('9' + 1));
        }

        return (byte)value;
    }

    private static int HexsToChars(Span<byte> guidChars, int a, int b, int start)
    {
        guidChars[start + 0] = ToCharLower(a >> 4);
        guidChars[start + 1] = ToCharLower(a);

        guidChars[start + 2] = ToCharLower(b >> 4);
        guidChars[start + 3] = ToCharLower(b);

        return start + 4;
    }
}