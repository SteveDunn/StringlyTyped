using System;
using System.Collections.Generic;

namespace StringlyTyped.SmallTests
{
    public class Age : ValueObject<int, Age>
    {
        public override string ValidationErrors() => Value >= 18 ? string.Empty : "Must be 18 or over";
    }

    public class Dave : ValueObject<string, Dave>
    {
        public override string ValidationErrors() => Value.StartsWith("dave ", StringComparison.OrdinalIgnoreCase) ||
                                                     Value.StartsWith("david ", StringComparison.OrdinalIgnoreCase)
            ? ""
            : "must be a dave or david";
    }

    /// <summary>
    /// A Value Object that is not supported
    /// </summary>
    public class Daves : ValueObject<List<Dave>, Daves>
    {
        public override string ValidationErrors() => Value.Count > 0 ? "" : "no dave's found";
    }

    public class Footballer : ValueObject<string, Footballer>
    {
    }

    public class BandMember : ValueObject<string, BandMember>
    {
    }

    public class Name : ValueObject<string, Name>
    {
    }

    public class Score : ValueObject<int, Score>
    {
        public override string ValidationErrors() => Value >= 0 ? string.Empty : "Score must be zero or more";
    }

    public class BadValueType : ValueObject<int, BadValueType>
    {
    }

    public class Anything : ValueObject<int, Anything>
    {
    }

    public class EightiesDate : ValueObject<DateTime, EightiesDate>
    {
        public override string ValidationErrors() => Value.Year is >= 1980 and <= 1989 ? string.Empty : "Must be a date in the 80's";
    }

    public class CaseSensitiveName : ValueObject<string, CaseSensitiveName>
    {
        public override string ValidationErrors() => Value.Length < 7 ? string.Empty : "A name must be less than 7 characters";
    }
}