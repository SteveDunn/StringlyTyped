using System;

namespace StringlyTyped.Examples
{
    internal static class ValidationExample
    {
        public static void Run()
        {
            string[] names = new[] { "Dave Grohl", "David Beckham", "Fred Flintstone" };

            var processor = new DaveProcessor();

            foreach (string name in names)
            {
                try
                {
                    processor.Process(Dave.From(name));
                }
                catch(ValueObjectValidationException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }

    public class Dave : ValueObject<string, Dave>
    {
        public override string ValidationErrors() => Value.StartsWith("dave ", StringComparison.OrdinalIgnoreCase) ||
                                                     Value.StartsWith("david ", StringComparison.OrdinalIgnoreCase)
            ? ""
            : $"must be a dave or david - {Value} is neither.";
    }

    internal class DaveProcessor
    {
        internal void Process(Dave dave) => Console.WriteLine($"Processing {dave}");
    }
}