using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

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
            ConstructorInfo ctor = typeof(TDerived)
                .GetTypeInfo()
                .DeclaredConstructors
                .Single();

            var argsExp = new Expression[0];
            NewExpression newExp = Expression.New(ctor, argsExp);
            LambdaExpression lambda = Expression.Lambda(typeof(Func<TDerived>), newExp);

            Factory = (Func<TDerived>)lambda.Compile();
        }

        public virtual string ValidationErrors() => string.Empty;

        public static TDerived From(T value)
        {
            if (value is null)
            {
                throw new ValueObjectValidationException("Value is null.");
            }

            if (value is ICollection)
            {
                throw new NotSupportedException("Collections are not supported.");
            }
            
            TDerived x = Factory();
            x.Value = value;

            string errors = x.ValidationErrors();
            
            if (!string.IsNullOrEmpty(errors))
            {
                throw new ValueObjectValidationException(errors);
            }

            return x;
        }

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