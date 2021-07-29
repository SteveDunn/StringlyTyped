using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StringlyTyped
{
    public class ValueObjectConverterFactory : JsonConverterFactory
    {
        private static readonly ConcurrentDictionary<Type, bool> _canConvertLookup = new();

        public override bool CanConvert(Type typeToConvert)
        {
            return _canConvertLookup.GetOrAdd(typeToConvert, CanConvertInternal);
            
            static bool CanConvertInternal(Type typeToConvert)
            {
                Type? baseType = typeToConvert.BaseType;

                if (baseType == null)
                {
                    return false;
                }

                if (!baseType.IsGenericType)
                {
                    return false;
                }

                if (baseType.GetGenericTypeDefinition() != typeof(ValueObject<,>))
                {
                    return false;
                }

                return true;
            }
        }

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            Type typeOfValueObject = typeToConvert;
            Type typeOfPrimitive = typeToConvert.BaseType!.GenericTypeArguments[0];
            
            Type typeOfConverterToCreate = typeof(ValueObjectConverterInner<,>).MakeGenericType(typeOfValueObject, typeOfPrimitive);

            JsonConverter? converter = Activator.CreateInstance(
                typeOfConverterToCreate,
                new object[] { options }) as JsonConverter;

            return converter;
        }

        [SuppressMessage("Microsoft.Usage", "CA1812:*", Justification = "It is instantiated by Reflection")]
        private class ValueObjectConverterInner<TValueType, TPrimitive> : JsonConverter<TValueType>
        where TPrimitive : notnull
            where TValueType : ValueObject<TPrimitive, TValueType>
        {
            private readonly JsonConverter<TValueType>? _valueConverter;
            private readonly Type _destinationType = typeof(TValueType);
            private readonly MethodInfo? _builderMethod;

            public ValueObjectConverterInner(JsonSerializerOptions options)
            {
                // For performance, use the existing converter if available.
                var jsonConverter = options.GetConverter(typeof(JsonConverter<TValueType>));

                _valueConverter = jsonConverter as JsonConverter<TValueType>;

                _builderMethod = _destinationType.GetMethod(
                                     "From",
                                     BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Static) ??
                                 throw new InvalidOperationException("Cannot find the From method on the ValueObject");
            }

            public override TValueType Read(ref Utf8JsonReader reader,
                Type typeToConvert,
                JsonSerializerOptions options)
            {
                object? v;

                if (_valueConverter != null)
                {
                    reader.Read();
                    v = _valueConverter.Read(ref reader, typeof(TPrimitive), options);
                }
                else
                {
                    v = JsonSerializer.Deserialize<TPrimitive>(ref reader, options);
                    //v = JsonSerializer.Deserialize<TPrimitive>(ref reader, options);
                }

                if (v == null)
                {
                    throw new InvalidOperationException($"No value to read for value object '{typeof(TValueType)}'");
                }

                try
                {
                    var result = _builderMethod?.Invoke(null, new[] { v }) 
                                 ?? throw new InvalidOperationException(
                        $"Value object cannot be converted from a {typeof(TValueType)} as there is no public static 'From' method defined.");

                    return (TValueType) result;
                }
                catch (Exception e) when (e is TargetInvocationException &&
                                          e.InnerException is ValueObjectValidationException)
                {
                    throw e.InnerException;
                }
            }

            public override void Write(Utf8JsonWriter writer, TValueType value, JsonSerializerOptions options) => 
                JsonSerializer.Serialize(writer, value.Value);
        }
    }
}



