using BenchmarkDotNet.Attributes;
using System.Text;

[MemoryDiagnoser]
public class HashBench
{
    private readonly byte[] _bytes;
    private readonly int _iterations;

    public HashBench()
    {
        _bytes = Encoding.ASCII.GetBytes("2e1a73fd-33e5-a890-dee8-6650a763e4cc");
        _iterations = 1_000_000;
    }

    [Benchmark]
    public void A_FastHashes()
    {
        ReadOnlySpan<byte> asSpan = _bytes.AsSpan();

        var murmurHash32 = new FastHashes.MurmurHash32();

        for (int i = 0; i < _iterations; i++)
        {
            BitConverter.ToInt32(murmurHash32.ComputeHash(asSpan));
        }
    }

    [Benchmark]
    public void B_OrenAndStu()
    {
        ReadOnlySpan<byte> asSpan = _bytes.AsSpan();

        for (int i = 0; i < _iterations; i++)
        {
            Collide.OrenAndStu.Hash(asSpan);
        }
    }

    [Benchmark]
    public void C_HashDepot()
    {
        ReadOnlySpan<byte> asSpan = _bytes.AsSpan();

        for (int i = 0; i < _iterations; i++)
        {
            HashDepot.MurmurHash3.Hash32(asSpan, 0);
        }
    }

    [Benchmark]
    public void D_MurmurHash_Net()
    {
        ReadOnlySpan<byte> asSpan = _bytes.AsSpan();

        for (int i = 0; i < _iterations; i++)
        {
            MurmurHash.Net.MurmurHash3.Hash32(asSpan, 0);
        }
    }

    [Benchmark]
    public void E_MurmurHash_net_core()
    {
        var create32 = Murmur.MurmurHash.Create32(0);

        for (int i = 0; i < _iterations; i++)
        {
            BitConverter.ToInt32(create32.ComputeHash(_bytes));
        }
    }

    [Benchmark]
    public void F_System_Data_HashFunction_MurmurHash()
    {
        var murmurHash3 = System.Data.HashFunction.MurmurHash.MurmurHash3Factory.Instance.Create(new System.Data.HashFunction.MurmurHash.MurmurHash3Config
        {
            Seed = 0,
        });

        for (int i = 0; i < _iterations; i++)
        {
            
            BitConverter.ToInt32(murmurHash3.ComputeHash(_bytes).Hash);
        }
    }

    [Benchmark]
    public void G_JeremyEspresso_MurmurHash()
    {
        ReadOnlySpan<byte> asSpan = _bytes.AsSpan();

        for (int i = 0; i < _iterations; i++)
        {
            MurmurHash.MurmurHash3.Hash32(ref asSpan, 0);
        }
    }
}