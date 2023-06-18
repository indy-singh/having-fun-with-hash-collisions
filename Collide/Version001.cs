using System.Diagnostics;
using System.Text;

namespace Collide;

public sealed class Version001 : IVersion
{
    public void Go(bool bench = false)
    {
        Console.WriteLine(GetType().Name);
        Console.WriteLine("Bench mode? " + bench);
        var murmurHash32 = new FastHashes.MurmurHash32();

        var sw = Stopwatch.StartNew();
        var i = 0;

        while (true)
        {
            var guidString = Guid.NewGuid().ToString();
            var stringBytes = Encoding.ASCII.GetBytes(guidString);
            var computeHash = murmurHash32.ComputeHash(stringBytes);
            var int32 = BitConverter.ToInt32(computeHash);

            if (int32 == 1228476406)
            {
                Console.WriteLine(stringBytes);
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