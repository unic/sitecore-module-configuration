namespace Unic.Configuration.Facts.Converter
{
    using System;
    using System.Collections.Generic;
    using Sitecore.Data.Items;
    using Unic.Configuration.Converter;
    using Unic.Configuration.Exceptions;
    using Xunit;

    public class ConverterFactoryFacts
    {
        public class TheGetMethod
        {
            [Fact]
            public void CanFindBooleanConverter()
            {
                // act
                var converter = ConverterFactory.Get(typeof(bool));

                // assert
                Assert.IsType<BooleanConverter>(converter);
            }

            [Fact]
            public void CanFindConfigurationFieldConverterByInterface()
            {
                // act
                var converter = ConverterFactory.Get(typeof(IConfigurationField));

                // assert
                Assert.IsType<ConfigurationFieldConverter>(converter);
            }

            [Fact]
            public void CanFindConfigurationFieldConverterByClass()
            {
                // act
                var converter = ConverterFactory.Get(typeof(ConfigurationField));

                // assert
                Assert.IsType<ConfigurationFieldConverter>(converter);
            }

            [Fact]
            public void CanFindDateTimeConverter()
            {
                // act
                var converter = ConverterFactory.Get(typeof(DateTime));

                // assert
                Assert.IsType<DateTimeConverter>(converter);
            }

            [Fact]
            public void CanFindDoubleConverter()
            {
                // act
                var converter = ConverterFactory.Get(typeof(double));

                // assert
                Assert.IsType<DoubleConverter>(converter);
            }

            [Fact]
            public void CanFindIntConverter()
            {
                // act
                var converter = ConverterFactory.Get(typeof(int));

                // assert
                Assert.IsType<IntConverter>(converter);
            }

            [Fact]
            public void CanFindItemConverter()
            {
                // act
                var converter = ConverterFactory.Get(typeof(Item));

                // assert
                Assert.IsType<ItemConverter>(converter);
            }

            [Fact]
            public void CanFindItemsConverterUsingIEnumerable()
            {
                // act
                var converter = ConverterFactory.Get(typeof(IEnumerable<Item>));

                // assert
                Assert.IsType<ItemsConverter>(converter);
            }

            [Fact]
            public void CanFindItemsConverterUsingArray()
            {
                // act
                var converter = ConverterFactory.Get(typeof(Item[]));

                // assert
                Assert.IsType<ItemsConverter>(converter);
            }

            [Fact]
            public void CanFindItemsConverterUsingList()
            {
                // act
                var converter = ConverterFactory.Get(typeof(List<Item>));

                // assert
                Assert.IsType<ItemsConverter>(converter);
            }

            [Fact]
            public void CanFindStringConverter()
            {
                // act
                var converter = ConverterFactory.Get(typeof(string));

                // assert
                Assert.IsType<StringConverter>(converter);
            }

            [Fact]
            public void ThrowsConverterNotFoundExceptionForNotRegisteredDataTypes()
            {
                // assert
                Assert.Throws<ConverterNotFoundException>(() => ConverterFactory.Get(typeof(ConverterFactoryFacts)));
            }
        }

        public class TheRegisterConverterMethod
        {
            [Fact]
            public void CanRegisterANewConverter()
            {
                // arrange
                var converterStub = new TestableObjectConverter();

                // assert
                Assert.Throws<ConverterNotFoundException>(() => ConverterFactory.Get(typeof(object)));

                // act
                ConverterFactory.RegisterConverter(converterStub);
                var objectConverter = ConverterFactory.Get(typeof(object));

                // assert
                Assert.NotNull(objectConverter);
                Assert.IsType<TestableObjectConverter>(objectConverter);
                Assert.IsAssignableFrom<AbstractConverter<object>>(objectConverter);
            }

            [Fact]
            public void CanReplaceAConverter()
            {
                // arrange
                var converterStub = new TestableStringConverter();

                // act
                var stringConverter = ConverterFactory.Get(typeof(string));

                // assert
                Assert.NotNull(stringConverter);
                Assert.IsType<StringConverter>(stringConverter);

                // act
                ConverterFactory.RegisterConverter(converterStub);
                var newConverter = ConverterFactory.Get(typeof(string));

                // assert
                Assert.NotNull(newConverter);
                Assert.IsType<TestableStringConverter>(newConverter);
                Assert.IsAssignableFrom<AbstractConverter<string>>(newConverter);
            }

            private class TestableObjectConverter : AbstractConverter<object>
            {
                public override object Convert(IConfigurationField field)
                {
                    return null;
                }
            }

            private class TestableStringConverter : AbstractConverter<string>
            {
                public override string Convert(IConfigurationField field)
                {
                    return null;
                }
            }
        }
    }
}
