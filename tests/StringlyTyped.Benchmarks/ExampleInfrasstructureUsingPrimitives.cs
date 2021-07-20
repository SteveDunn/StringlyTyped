using System;

namespace StringlyTyped.Benchmarks.UsingPrimitives
{
    internal class ExampleInfrastructureUsingPrimitives
    {
        internal void Run()
        {
            int found = 0;
            for (int i = 0; i < 1_000_000; i++)
            {
                if (i % 3 == 0)
                {
                    if(Process(i))
                    {
                        ++found;
                    }
                }
            }
        }

        private bool Process(int i) => i % 5 == 0;
    }
}