namespace Unic.Configuration.Converter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Data.Items;
    using Unic.Configuration.Exceptions;

    /// <summary>
    /// The converter factory class.
    /// </summary>
    public static class ConverterFactory
    {
        /// <summary>
        /// The converters.
        /// </summary>
        private static readonly IDictionary<Type, IConverter> Converters = new Dictionary<Type, IConverter>
                                                                      {
                                                                          { typeof(string), new StringConverter() },
                                                                          { typeof(bool), new BooleanConverter() },
                                                                          { typeof(int), new IntConverter() },
                                                                          { typeof(double), new DoubleConverter() },
                                                                          { typeof(DateTime), new DateTimeConverter() },
                                                                          { typeof(Item), new ItemConverter() },
                                                                          { typeof(IEnumerable<Item>), new ItemsConverter() },
                                                                          { typeof(IConfigurationField), new ConfigurationFieldConverter() }
                                                                      };

        /// <summary>
        /// Gets the converter by a specified name.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The converter.
        /// </returns>
        public static IConverter Get(Type type)
        {
            // look for direct match of type
            if (Converters.ContainsKey(type)) return Converters[type];

            // try to find a compatible converter
            foreach (var converter in Converters.Where(converter => converter.Key.IsAssignableFrom(type)))
            {
                return converter.Value;
            }
            
            throw new ConverterNotFoundException(type);
        }

        /// <summary>
        /// Registers the converter.
        /// </summary>
        /// <param name="converter">The converter.</param>
        public static void RegisterConverter(IConverter converter)
        {
            var type = converter.ConversionType;
            if (Converters.ContainsKey(type))
            {
                Converters[type] = converter;
            }
            else
            {
                Converters.Add(type, converter);
            }
        }
    }
}
