using System;
using System.Collections.Generic;
using System.Text.Json;
using FluentAssertions;
using Xunit;

namespace StringlyTyped.SmallTests
{
    public class Serialisation
    {
        [Fact]
        public void serialising()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Converters = { new ValueObjectConverterFactory() }
            };

            Age age1 = Age.From(18);
            string ageAsJson = JsonSerializer.Serialize(age1, options);

            Age age2 = JsonSerializer.Deserialize<Age>(ageAsJson, options);
            age2.Value.Should().Be(18);

            Score score1 = Score.From(18);
            string scoreAsJson = JsonSerializer.Serialize(score1, options);
            Score score2 = JsonSerializer.Deserialize<Score>(scoreAsJson, options);
            score1.Equals(score2).Should().BeTrue();

            (age1 == age2).Should().BeTrue();
            age1.Equals(age2).Should().BeTrue();

            List<Dave> daves = new(new List<Dave>
            {
                Dave.From("david bowie"),
                Dave.From("david beckham"),
                Dave.From("dave grohl")
            });

            string davesAsText = JsonSerializer.Serialize(daves, options);

            var newDaves = JsonSerializer.Deserialize<List<Dave>>(davesAsText, options);

            object.ReferenceEquals(daves, newDaves).Should().BeFalse();

            newDaves.Count.Should().Be(daves.Count);
            newDaves.Should().ContainInOrder(daves);
        }

        [Fact]
        public void serialising_from_same_json()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Converters = { new ValueObjectConverterFactory() }
            };

            const string json = "18";

            Age d_age = JsonSerializer.Deserialize<Age>(json, options);
            Score d_score = JsonSerializer.Deserialize<Score>(json, options);

            d_age.Value.Should().Be(18);
            d_score.Value.Should().Be(18);
            
            object.ReferenceEquals(d_age, d_score).Should().BeFalse();
        }

        [Fact]
        public void serialising_from_now_invalid_value()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Converters = { new ValueObjectConverterFactory() }
            };

            Func<Age> act = () => JsonSerializer.Deserialize<Age>("4", options);

            act.Should().Throw<ValueObjectValidationException>();//.WithMessage("Must be 18 or over");
        }

        [Fact]
        public void roundtrip()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Converters = { new ValueObjectConverterFactory() }
            };

            Customer c = new Customer
            {
                CustomerId = CustomerId.From(123),
                FirstName = FirstName.From("Fred"),
                Surname = Surname.From("Flintstone")
            };

            var s = JsonSerializer.Serialize(c, options);
            var c2 = JsonSerializer.Deserialize<Customer>(s, options);

            c2.CustomerId.Value.Should().Be(123);
            c2.FirstName.Value.Should().Be("Fred");
            c2.Surname.Value.Should().Be("Flintstone");
        }

        public class CustomerId : ValueObject<int, CustomerId>
        {
        }

        public class FirstName : ValueObject<string, FirstName>
        {
        }

        public class Surname : ValueObject<string, Surname>
        {
        }

        public class Customer
        {
            public CustomerId CustomerId { get; init; }
            public FirstName FirstName { get; init; }
            public Surname Surname { get; init; }
        }


    }
}