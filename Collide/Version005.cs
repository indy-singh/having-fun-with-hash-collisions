using System.Diagnostics;
using System.Text;

namespace Collide;

public sealed class Version005 : IVersion
{
    public void Go(bool bench = false)
    {
        Console.WriteLine(GetType().Name);
        Console.WriteLine("Bench mode? " + bench);
        var sw = Stopwatch.StartNew();
        var i = 0;

        Span<byte> stringBytes = stackalloc byte[8];
        var random = new Random();
        //var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".Select(x => (byte)x).ToArray();

        while (true)
        {
            stringBytes.Clear();
            //stringBytes[0] = chars[random.Next(0, chars.Length)];
            //stringBytes[1] = chars[random.Next(0, chars.Length)];
            //stringBytes[2] = chars[random.Next(0, chars.Length)];
            //stringBytes[3] = chars[random.Next(0, chars.Length)];
            //stringBytes[4] = chars[random.Next(0, chars.Length)];
            //stringBytes[5] = chars[random.Next(0, chars.Length)];
            //stringBytes[6] = chars[random.Next(0, chars.Length)];
            //stringBytes[7] = chars[random.Next(0, chars.Length)];

            random.NextBytes(stringBytes);

            ReadOnlySpan<byte> stringBytes1 = stringBytes;
            var int32 = MurmurHash.MurmurHash3.Hash32(ref stringBytes1, 0);

            if (int32 == 1228476406)
            {
                Console.WriteLine(string.Join(", ", stringBytes.ToArray()));
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