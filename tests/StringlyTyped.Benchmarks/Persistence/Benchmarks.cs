using System.Collections.Generic;
using System.Text.Json;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Jobs;
using StringlyTyped.Benchmarks.Persistence;

namespace StringlyTyped.Benchmarks.Perf
{
    [SimpleJob(RuntimeMoniker.Net461)]
    [SimpleJob(RuntimeMoniker.NetCoreApp20)]
    [SimpleJob(RuntimeMoniker.NetCoreApp21)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [NativeMemoryProfiler]
    [MemoryDiagnoser]
    public class PersistenceBenchmarks
    {
        [Benchmark]
        public string Serialising()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Converters = { new ValueObjectConverterFactory() }
            };
            Container containers = BuildContainer();


            return JsonSerializer.Serialize(containers, options);
        }


        [Benchmark]
        public Container Deserialising()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Converters = { new ValueObjectConverterFactory() }
            };

            Container container = BuildContainer();

            string s = JsonSerializer.Serialize(container, options);

            return JsonSerializer.Deserialize<Container>(s, options);
        }

        private Container BuildContainer()
        {
            var c = new Container();

            for (int j = 0; j < 100; j++)
            {
                c.Vo1_ints.Add(Vo_int_1.From(j));
                c.Vo2_ints.Add(Vo_int_2.From(j));
                c.Vo3_ints.Add(Vo_int_3.From(j));
                c.Vo4_ints.Add(Vo_int_4.From(j));
                c.Vo5_ints.Add(Vo_int_5.From(j));
                c.Vo6_ints.Add(Vo_int_6.From(j));
                c.Vo7_ints.Add(Vo_int_7.From(j));
                c.Vo8_ints.Add(Vo_int_8.From(j));
                c.Vo9_ints.Add(Vo_int_9.From(j));
                c.Vo10_ints.Add(Vo_int_10.From(j));

                c.Vo1_doubles.Add(Vo_double_1.From(j));
                c.Vo2_doubles.Add(Vo_double_2.From(j));
                c.Vo3_doubles.Add(Vo_double_3.From(j));
                c.Vo4_doubles.Add(Vo_double_4.From(j));
                c.Vo5_doubles.Add(Vo_double_5.From(j));
                c.Vo6_doubles.Add(Vo_double_6.From(j));
                c.Vo7_doubles.Add(Vo_double_7.From(j));
                c.Vo8_doubles.Add(Vo_double_8.From(j));
                c.Vo9_doubles.Add(Vo_double_9.From(j));
                c.Vo10_doubles.Add(Vo_double_10.From(j));

                c.Vo1_strings.Add(Vo_string_1.From($"{1}-{j}"));
                c.Vo2_strings.Add(Vo_string_2.From($"{1}-{j}"));
                c.Vo3_strings.Add(Vo_string_3.From($"{1}-{j}"));
                c.Vo4_strings.Add(Vo_string_4.From($"{1}-{j}"));
                c.Vo5_strings.Add(Vo_string_5.From($"{1}-{j}"));
                c.Vo6_strings.Add(Vo_string_6.From($"{1}-{j}"));
                c.Vo7_strings.Add(Vo_string_7.From($"{1}-{j}"));
                c.Vo8_strings.Add(Vo_string_8.From($"{1}-{j}"));
                c.Vo9_strings.Add(Vo_string_9.From($"{1}-{j}"));
                c.Vo10_strings.Add(Vo_string_10.From($"{1}-{j}"));
            }

            return c;
        }
    }
}