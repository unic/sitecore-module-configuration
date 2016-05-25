namespace Unic.Configuration.Tests.Converter
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using Sitecore.Data.Items;
    using Unic.Configuration.Core;
    using Unic.Configuration.Core.Converter;
    using Unic.Configuration.Core.Exceptions;

    public class ConverterFactoryFacts
    {
        [TestFixture]
        public class TheGetMethod
        {
            [Test]
            public void CanFindBooleanConverter()
            {
                // act
                var converter = ConverterFactory.Get(typeof(bool));

                // assert
                Assert.IsInstanceOf<BooleanConverter>(converter);
            }

            [Test]
            public void CanFindConfigurationFieldConverterByInterface()
            {
                // act
                var converter = ConverterFactory.Get(typeof(IConfigurationField));

                // assert
                Assert.IsInstanceOf<ConfigurationFieldConverter>(converter);
            }

            [Test]
            public void CanFindConfigurationFieldConverterByClass()
            {
                // act
                var converter = ConverterFactory.Get(typeof(ConfigurationField));

                // assert
                Assert.IsInstanceOf<ConfigurationFieldConverter>(converter);
            }

            [Test]
            public void CanFindDateTimeConverter()
            {
                // act
                var converter = ConverterFactory.Get(typeof(DateTime));

                // assert
                Assert.IsInstanceOf<DateTimeConverter>(converter);
            }

            [Test]
            public void CanFindDoubleConverter()
            {
                // act
                var converter = ConverterFactory.Get(typeof(double));

                // assert
                Assert.IsInstanceOf<DoubleConverter>(converter);
            }

            [Test]
            public void CanFindIntConverter()
            {
                // act
                var converter = ConverterFactory.Get(typeof(int));

                // assert
                Assert.IsInstanceOf<IntConverter>(converter);
            }

            [Test]
            public void CanFindItemConverter()
            {
                // act
                var converter = ConverterFactory.Get(typeof(Item));

                // assert
                Assert.IsInstanceOf<ItemConverter>(converter);
            }

            [Test]
            public void CanFindItemsConverterUsingIEnumerable()
            {
                // act
                var converter = ConverterFactory.Get(typeof(IEnumerable<Item>));

                // assert
                Assert.IsInstanceOf<ItemsConverter>(converter);
            }

            [Test]
            public void CanFindItemsConverterUsingArray()
            {
                // act
                var converter = ConverterFactory.Get(typeof(Item[]));

                // assert
                Assert.IsInstanceOf<ItemsConverter>(converter);
            }

            [Test]
            public void CanFindItemsConverterUsingList()
            {
                // act
                var converter = ConverterFactory.Get(typeof(List<Item>));

                // assert
                Assert.IsInstanceOf<ItemsConverter>(converter);
            }

            [Test]
            public void CanFindStringConverter()
            {
                // act
                var converter = ConverterFactory.Get(typeof(string));

                // assert
                Assert.IsInstanceOf<StringConverter>(converter);
            }

            [Test]
            public void ThrowsConverterNotFoundExceptionForNotRegisteredDataTypes()
            {
                // assert
                Assert.Throws<ConverterNotFoundException>(() => ConverterFactory.Get(typeof(ConverterFactoryFacts)));
            }
        }

        public class TheRegisterConverterMethod
        {
            [Test]
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
                Assert.IsInstanceOf<TestableObjectConverter>(objectConverter);
                Assert.IsInstanceOf<AbstractConverter<object>>(objectConverter);
            }

            [Test]
            public void CanReplaceAConverter()
            {
                // arrange
                var converterStub = new TestableStringConverter();

                // act
                var stringConverter = ConverterFactory.Get(typeof(string));

                // assert
                Assert.NotNull(stringConverter);
                Assert.IsInstanceOf<StringConverter>(stringConverter);

                // act
                ConverterFactory.RegisterConverter(converterStub);
                var newConverter = ConverterFactory.Get(typeof(string));

                // assert
                Assert.NotNull(newConverter);
                Assert.IsInstanceOf<TestableStringConverter>(newConverter);
                Assert.IsInstanceOf<AbstractConverter<string>>(newConverter);
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
