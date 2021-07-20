using System.Collections.Generic;

namespace StringlyTyped.Benchmarks.UsingPrimitives
{
    public class Customer
    {
        public int CustomerId { get; init; }
    }

    internal class ExampleDomainUsingPrimitives
    {
        List<Customer> _list = new List<Customer>(1_000_000);

        public ExampleDomainUsingPrimitives()
        {
        }

        internal void Run()
        {
            for (int i = 0; i < 1000; i++)
            {
                _list.Add(new Customer { CustomerId = i });
            }
        }
    }
}