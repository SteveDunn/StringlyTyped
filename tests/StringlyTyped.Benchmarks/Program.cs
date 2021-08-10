using BenchmarkDotNet.Running;
using StringlyTyped.Benchmarks.Perf;

namespace StringlyTyped.Benchmarks
{
    class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<PersistenceBenchmarks>();
            BenchmarkRunner.Run<Benchmarks>();
        }
    }
}

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit {}
}