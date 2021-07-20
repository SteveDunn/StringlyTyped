using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using StringlyTyped.Benchmarks.UsingPrimitives;
using StringlyTyped.Benchmarks.UsingValueObjects;

namespace StringlyTyped.Benchmarks
{
    [NativeMemoryProfiler]
    [MemoryDiagnoser]
    public class Benchmarks
    {
        [Benchmark]
        public void UsingPrimitivesInDomain()
        {
            new ExampleDomainUsingPrimitives().Run();
        }

        [Benchmark]
        public void UsingValueObjectsInDomain()
        {
            new ExampleDomainUsingValueObjects().Run();
        }

        [Benchmark]
        public void UsingPrimitivesInInfrastructure()
        {
            new ExampleInfrastructureUsingPrimitives().Run();
        }

        [Benchmark]
        public void UsingValueObjectsInInfrastructure()
        {
            new ExampleInfrastructureUsingValueObjects().Run();
        }
    }
}
