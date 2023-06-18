using System.Diagnostics;
using BenchmarkDotNet.Running;

namespace Collide
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppDomain.MonitoringIsEnabled = true;

            var dict = new Dictionary<string, Action>
            {
                ["1"] = () =>
                {
                    new Version001().Go();
                },
                ["2"] = () =>
                {
                    new Version002().Go();
                },
                ["3"] = () =>
                {
                    new Version003().Go();
                },
                ["4"] = () =>
                {
                    new Version004().Go();
                },
                ["5"] = () =>
                {
                    new Version005().Go();
                },
                ["1b"] = () =>
                {
                    new Version001().Go(true);
                },
                ["2b"] = () =>
                {
                    new Version002().Go(true);
                },
                ["3b"] = () =>
                {
                    new Version003().Go(true);
                },
                ["4b"] = () =>
                {
                    new Version004().Go(true);
                },
                ["5b"] = () =>
                {
                    new Version005().Go(true);
                },
                ["hashbench"] = () =>
                {
                    BenchmarkRunner.Run(typeof(Program).Assembly);
                },
            };

            if (args.Length == 1 && dict.ContainsKey(args[0]))
            {
                dict[args[0]]();
            }
            else
            {
                Console.WriteLine("Incorrect parameters");
                Console.WriteLine("Valid pameters:-");
                Console.WriteLine(string.Join(", ", dict.Keys));
                Environment.Exit(1);
            }

            Console.WriteLine($"Took: {AppDomain.CurrentDomain.MonitoringTotalProcessorTime.TotalMilliseconds:#,###} ms");
            Console.WriteLine($"Allocated: {AppDomain.CurrentDomain.MonitoringTotalAllocatedMemorySize / 1024:#,#} kb");
            Console.WriteLine($"Peak Working Set: {Process.GetCurrentProcess().PeakWorkingSet64 / 1024:#,#} kb");

            for (var index = 0; index <= GC.MaxGeneration; index++)
            {
                Console.WriteLine($"Gen {index} collections: {GC.CollectionCount(index)}");
            }

            Console.WriteLine(Environment.NewLine);
        }
    }
}
