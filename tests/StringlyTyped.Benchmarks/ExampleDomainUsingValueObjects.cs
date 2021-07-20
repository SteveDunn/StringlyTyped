using StringlyTyped.Benchmarks.UsingPrimitives;
using System.Collections.Generic;

namespace StringlyTyped.Benchmarks.UsingValueObjects
{
    public class Customer
    {
        public CustomerId CustomerId { get; init; }
    }

    internal class ExampleDomainUsingValueObjects
    {
        List<Customer> _list = new List<Customer>(1_000_000);

        public ExampleDomainUsingValueObjects()
        {
        }

        internal void Run()
        {
            for (int i = 0; i < 1000; i++)
            {
                _list.Add(new Customer { CustomerId = CustomerId.From(i) });
            }
        }
    }
}