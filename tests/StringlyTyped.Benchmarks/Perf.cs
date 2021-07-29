using System.Text.Json;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;

namespace StringlyTyped.Benchmarks.Perf
{
    public class CustomerId : ValueObject<int, CustomerId> { }

    public class FirstName : ValueObject<string, FirstName> { }

    public class Surname : ValueObject<string, Surname> { }

    public class Customer1
    {
        public CustomerId CustomerId { get; init; }
        public FirstName FirstName { get; init; }
        public Surname Surname { get; init; }
    }

    public class Customer2
    {
        public CustomerId CustomerId { get; init; }
        public FirstName FirstName { get; init; }
        public Surname Surname { get; init; }
    }

    public class Customer3
    {
        public CustomerId CustomerId { get; init; }
        public FirstName FirstName { get; init; }
        public Surname Surname { get; init; }
    }

    [NativeMemoryProfiler]
    [MemoryDiagnoser]
    public class PerfBenchmarks
    {
        [Benchmark]
        public void Serialising()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Converters = { new ValueObjectConverterFactory() }
            };

            Customer1 c1 = new Customer1
            {
                CustomerId = CustomerId.From(111),
                FirstName = FirstName.From("Fred"),
                Surname = Surname.From("Flintstone")
            };

            Customer2 c2 = new Customer2
            {
                CustomerId = CustomerId.From(222),
                FirstName = FirstName.From("Barney"),
                Surname = Surname.From("Rubble")
            };

            Customer3 c3 = new Customer3
            {
                CustomerId = CustomerId.From(333),
                FirstName = FirstName.From("Wilma"),
                Surname = Surname.From("Flintstone")
            };

            for (int i = 0; i < 1_000_000; i++)
            {
                _ = JsonSerializer.Serialize(c1, options);
                _ = JsonSerializer.Serialize(c2, options);
                _ = JsonSerializer.Serialize(c3, options);
            }
        }

        [Benchmark]
        public void Deserialising()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Converters = { new ValueObjectConverterFactory() }
            };

            Customer1 c1 = new Customer1
            {
                CustomerId = CustomerId.From(111),
                FirstName = FirstName.From("Fred"),
                Surname = Surname.From("Flintstone")
            };

            Customer2 c2 = new Customer2
            {
                CustomerId = CustomerId.From(222),
                FirstName = FirstName.From("Barney"),
                Surname = Surname.From("Rubble")
            };

            Customer3 c3 = new Customer3
            {
                CustomerId = CustomerId.From(333),
                FirstName = FirstName.From("Wilma"),
                Surname = Surname.From("Flintstone")
            };

            string s1 = JsonSerializer.Serialize(c1, options);
            string s2 = JsonSerializer.Serialize(c2, options);
            string s3 = JsonSerializer.Serialize(c3, options);

            for (int i = 0; i < 1_000_000; i++)
            {
                JsonSerializer.Deserialize<Customer1>(s1, options);
                JsonSerializer.Deserialize<Customer2>(s2, options);
                JsonSerializer.Deserialize<Customer3>(s3, options);
            }
        }
    }
}