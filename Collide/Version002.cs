using System.Diagnostics;
using System.Text;

namespace Collide;

public sealed class Version002 : IVersion
{
    public void Go(bool bench = false)
    {
        Console.WriteLine(GetType().Name);
        Console.WriteLine("Bench mode? " + bench);
        var murmurHash32 = new FastHashes.MurmurHash32();

        var sw = Stopwatch.StartNew();
        var i = 0;

        Span<byte> guidBytes = stackalloc byte[16];
        var random = new Random();

        while (true)
        {
            guidBytes.Clear();
            random.NextBytes(guidBytes);

            var guidString = new Guid(guidBytes).ToString();
            var stringBytes = Encoding.ASCII.GetBytes(guidString);
            var computeHash = murmurHash32.ComputeHash(stringBytes);
            var int32 = BitConverter.ToInt32(computeHash);

            if (int32 == 1228476406)
            {
                Console.WriteLine(guidString);
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