using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace StringlyTyped
{
    /// <summary>
    /// Represents 'value objects'. These types are here in an attempt to stop 'Primitive Obsession' and 'Stringly Typed'
    /// (e.g. where objects are represented as primitives such as strings but where strings don't fully represent
    /// the entity).
    /// </summary>
    /// <typeparam name="T">The underlying primitive type, e.g. decimal, int, string</typeparam>
    /// <typeparam name="TDerived">The derived typed (your type) - required for the 'factory' From method.</typeparam>
    public abstract class ValueObject<T, TDerived> : IEquatable<ValueObject<T, TDerived>>, IEquatable<T>
        where T : notnull
        where TDerived : ValueObject<T, TDerived>
    {
        private static readonly Func<TDerived> Factory;

#pragma warning disable 8618
        public T Value { get; private set; }
#pragma warning restore 8618

        static ValueObject()
        {
            var constructors = typeof(TDerived)
                .GetTypeInfo()
                .DeclaredConstructors.Where(c => !c.IsStatic).ToList();

            if (constructors.Count == 0)
            {
                throw new ValueObjectSetupException($"No constructors for {typeof(TDerived).Name}");
            }

            if (constructors.Count > 1)
            {
                throw new ValueObjectSetupException($"Multiple constructors for {typeof(TDerived).Name}");
            }


            ConstructorInfo ctor = constructors.Single();

            NewExpression newExp = Expression.New(ctor, Array.Empty<Expression>());
            LambdaExpression lambda = Expression.Lambda(typeof(Func<TDerived>), newExp);

            Factory = (Func<TDerived>) lambda.Compile();
        }

        /// <summary>
        /// Validates the value provided.  Return `Validation.Ok` if validation passes, or
        /// `Validation.Invalid` if the data is invalid.
        /// <example>
        /// public override Validation Validate() =&gt; Value > 0 ? Validation.Ok : Validation.Invalid("invalid domain object");
        /// </example>
        /// If `Invalid` is returned, then a <see cref="ValueObjectValidationException"/> is thrown.
        /// </summary>
        /// <returns></returns>
        public virtual Validation Validate() => Validation.Ok;

        /// <summary>
        /// Builds an instance and allows derived classes to skip validation. Skipping validation
        /// is useful when using a `NullObject` for a domain object representing something
        /// that is uninitialised.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ignoreValidation"></param>
        /// <returns></returns>
        protected static TDerived From(T value, bool ignoreValidation)
        {
            if (value is null)
            {
                throw new ValueObjectValidationException("Value is null.");
            }

            if (value is ICollection)
            {
                throw new NotSupportedException("Collections are not supported.");
            }

            TDerived instance = Factory();
            instance.Value = value;

            Validation validation = ignoreValidation ? Validation.Ok : instance.Validate();

            if (validation != Validation.Ok)
            {
                throw new ValueObjectValidationException(validation.ErrorMessage);
            }

            return instance;
        }

        /// <summary>
        /// Builds an instance.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>An instance which is validated if validation is provided and succeeds.</returns>
        public static TDerived From(T value) => From(value, false);

        public bool Equals(ValueObject<T, TDerived>? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return GetType() == other.GetType() && EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public bool Equals(T? primitive) => Value.Equals(primitive);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((ValueObject<T, TDerived>) obj);
        }

        public static bool operator ==(ValueObject<T, TDerived> left, ValueObject<T, TDerived> right) => Equals(left, right);
        public static bool operator !=(ValueObject<T, TDerived> left, ValueObject<T, TDerived> right) => !Equals(left, right);

        public static bool operator ==(ValueObject<T, TDerived> left, T right) => Equals(left.Value, right);
        public static bool operator !=(ValueObject<T, TDerived> left, T right) => !Equals(left.Value, right);

        public static bool operator ==(T left, ValueObject<T, TDerived> right) => Equals(left, right.Value);
        public static bool operator !=(T left, ValueObject<T, TDerived> right) => !Equals(left, right.Value);

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = (int) 2166136261;
                // Suitable nullity checks etc, of course :)
                hash = (hash * 16777619) ^ Value.GetHashCode();
                hash = (hash * 16777619) ^ GetType().GetHashCode();
                hash = (hash * 16777619) ^ EqualityComparer<T>.Default.GetHashCode();
                return hash;
            }
        }

        public override string? ToString() => Value.ToString();
    }
}