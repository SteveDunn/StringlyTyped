using System;
using System.Collections.Generic;

namespace StringlyTyped.SmallTests
{
    public class Age : ValueObject<int, Age>
    {
        public override Validation Validate() => Value >= 18 ? Validation.Ok : Validation.Invalid("Must be 18 or over");
    }

    public class Dave : ValueObject<string, Dave>
    {
        public override Validation Validate() => Value.StartsWith("dave ", StringComparison.OrdinalIgnoreCase) ||
                                                 Value.StartsWith("david ", StringComparison.OrdinalIgnoreCase)
            ? Validation.Ok
            : Validation.Invalid("must be a dave or david");
    }

    /// <summary>
    /// A Value Object that is not supported
    /// </summary>
    public class Daves : ValueObject<List<Dave>, Daves>
    {
        public override Validation Validate() => Value.Count > 0 ? Validation.Ok : Validation.Invalid("no dave's found");
    }

    public class Score : ValueObject<int, Score>
    {
        public override Validation Validate() => Value >= 0 ? Validation.Ok : Validation.Invalid("Score must be zero or more");
    }

    public class Anything : ValueObject<int, Anything>
    {
    }

    public class EightiesDate : ValueObject<DateTime, EightiesDate>
    {
        public override Validation Validate() => Value.Year is >= 1980 and <= 1989 ? Validation.Ok : Validation.Invalid("Must be a date in the 80's");
    }

    public class PlayerNumber : ValueObject<int, PlayerNumber>
    {
        public override Validation Validate() => 
            Value is >=1 and <=12  ? Validation.Ok : Validation.Invalid("Player number mst be between 1 and 12.");

        public static readonly PlayerNumber Unassigned = From(0, true);
    }

    public class MinimalValidation : ValueObject<int, MinimalValidation>
    {
        public override Validation Validate() => 
            Value != 1 ? Validation.Ok : Validation.Invalid();
    }
}