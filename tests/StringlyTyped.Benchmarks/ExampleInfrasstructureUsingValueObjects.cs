using StringlyTyped.Benchmarks.UsingValueObjects;
using System;

namespace StringlyTyped.Benchmarks.UsingPrimitives
{
    internal class ExampleInfrastructureUsingValueObjects
    {
        internal void Run()
        {
            int found = 0;

            for (int i = 0; i < 1_000_000; i++)
            {
                var c = CustomerId.From(i);
                if (c.Value % 3 == 0)
                {
                    if(Process(c))
                    {
                        ++found;
                    }
                }
            }
        }

        private bool Process(CustomerId c) => c.Value % 5 == 0;
    }
}