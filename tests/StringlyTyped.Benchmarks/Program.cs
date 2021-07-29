using BenchmarkDotNet.Running;
using StringlyTyped.Benchmarks.Perf;

namespace StringlyTyped.Benchmarks
{
    class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<PerfBenchmarks>();
            //BenchmarkRunner.Run<Benchmarks>();
        }
    }
}
