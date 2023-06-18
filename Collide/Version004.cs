using System.Diagnostics;
using System.Text;

namespace Collide;

public sealed class Version004 : IVersion
{
    public void Go(bool bench = false)
    {
        Console.WriteLine(GetType().Name);
        Console.WriteLine("Bench mode? " + bench);
        var sw = Stopwatch.StartNew();
        var i = 0;

        Span<byte> guidBytes = stackalloc byte[16];
        Span<byte> stringBytes = stackalloc byte[36];
        var random = new Random();

        while (true)
        {
            guidBytes.Clear();
            stringBytes.Clear();
            random.NextBytes(guidBytes);

            Ugly.GuidBytesToRegularBytes(guidBytes, stringBytes);

            ReadOnlySpan<byte> stringBytes1 = stringBytes;
            var int32 = MurmurHash.MurmurHash3.Hash32(ref stringBytes1, 0);

            if (int32 == 1228476406)
            {
                Console.WriteLine(Encoding.ASCII.GetString(stringBytes));
            }

            if (bench)
            {
                ++i;

                if (sw.Elapsed.TotalSeconds >= 10)
                {
                    Console.WriteLine(i.ToString("N0"));
                    return;
                }
            }
        }
    }
}