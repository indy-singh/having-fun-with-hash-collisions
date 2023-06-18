using System.Runtime.InteropServices;

namespace Collide;

public class OrenAndStu
{
    public static int Hash(ReadOnlySpan<byte> data)
    {
        const uint c1 = 0xcc9e2d51;
        const uint c2 = 0x1b873593;

        var ints = MemoryMarshal.Cast<byte, uint>(data);

        uint h1 = 0;
        var end = (data.Length >> 2); // /= 4
        uint k1;
        for (int i = 0; i < end; i++)
        {
            /* bitmagic hash */
            k1 = ints[i];
            k1 *= c1;
            k1 = Rotl32(k1, 15);
            k1 *= c2;

            h1 ^= k1;
            h1 = Rotl32(h1, 13);
            h1 = h1 * 5 + 0xe6546b64;
        }
        // handle remainder
        k1 = 0;
        var remainder = data.Length - end * sizeof(uint);
        if (remainder > 0)
        {
            for (int i = 0; i < remainder; i++)
            {
                k1 = data[i];
            }
            k1 *= c1;
            k1 = Rotl32(k1, 15);
            k1 *= c2;
            h1 ^= k1;
        }

        h1 ^= (uint)data.Length;
        h1 = Fmix(h1);

        return (int)h1;
    }

    private static uint Rotl32(uint x, byte r) => (x << r) | (x >> (32 - r));

    private static uint Fmix(uint h)
    {
        h ^= h >> 16;
        h *= 0x85ebca6b;
        h ^= h >> 13;
        h *= 0xc2b2ae35;
        h ^= h >> 16;
        return h;
    }
}

