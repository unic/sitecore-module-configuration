namespace Unic.Configuration.Tests.Converter
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sitecore.Data.Items;
    using Unic.Configuration.Converter;
    using Unic.Configuration.Exceptions;

    public class ConverterFactoryTests
    {
        [TestClass]
        public class TheGetMethod
        {
            [TestMethod]
            public void CanFindBooleanConverter()
            {
                // act
                var converter = ConverterFactory.Get(typeof(bool));

                // assert
                Assert.IsInstanceOfType(converter, typeof(BooleanConverter));
            }

            [TestMethod]
            public void CanFindConfigurationFieldConverterByInterface()
            {
                // act
                var converter = ConverterFactory.Get(typeof(IConfigurationField));

                // assert
                Assert.IsInstanceOfType(converter, typeof(ConfigurationFieldConverter));
            }

            [TestMethod]
            public void CanFindConfigurationFieldConverterByClass()
            {
                // act
                var converter = ConverterFactory.Get(typeof(ConfigurationField));

                // assert
                Assert.IsInstanceOfType(converter, typeof(ConfigurationFieldConverter));
            }

            [TestMethod]
            public void CanFindDateTimeConverter()
            {
                // act
                var converter = ConverterFactory.Get(typeof(DateTime));

                // assert
                Assert.IsInstanceOfType(converter, typeof(DateTimeConverter));
            }

            [TestMethod]
            public void CanFindDoubleConverter()
            {
                // act
                var converter = ConverterFactory.Get(typeof(double));

                // assert
                Assert.IsInstanceOfType(converter, typeof(DoubleConverter));
            }

            [TestMethod]
            public void CanFindIntConverter()
            {
                // act
                var converter = ConverterFactory.Get(typeof(int));

                // assert
                Assert.IsInstanceOfType(converter, typeof(IntConverter));
            }

            [TestMethod]
            public void CanFindItemConverter()
            {
                // act
                var converter = ConverterFactory.Get(typeof(Item));

                // assert
                Assert.IsInstanceOfType(converter, typeof(ItemConverter));
            }

            [TestMethod]
            public void CanFindItemsConverterUsingIEnumerable()
            {
                // act
                var converter = ConverterFactory.Get(typeof(IEnumerable<Item>));

                // assert
                Assert.IsInstanceOfType(converter, typeof(ItemsConverter));
            }

            [TestMethod]
            public void CanFindItemsConverterUsingArray()
            {
                // act
                var converter = ConverterFactory.Get(typeof(Item[]));

                // assert
                Assert.IsInstanceOfType(converter, typeof(ItemsConverter));
            }

            [TestMethod]
            public void CanFindItemsConverterUsingList()
            {
                // act
                var converter = ConverterFactory.Get(typeof(List<Item>));

                // assert
                Assert.IsInstanceOfType(converter, typeof(ItemsConverter));
            }

            [TestMethod]
            public void CanFindStringConverter()
            {
                // act
                var converter = ConverterFactory.Get(typeof(string));

                // assert
                Assert.IsInstanceOfType(converter, typeof(StringConverter));
            }

            [TestMethod]
            [ExpectedException(typeof(ConverterNotFoundException))]
            public void ThrowsConverterNotFoundExceptionForNotRegisteredDataTypes()
            {
                // act
                ConverterFactory.Get(typeof(ConverterFactoryTests));

                // assert
                Assert.Fail();
            }
        }

        [TestClass]
        public class TheRegisterConverterMethod
        {
            [TestMethod]
            [ExpectedException(typeof(ConverterNotFoundException))]
            public void CanRegisterANewConverter()
            {
                // arrange
                var converterStub = new TestableObjectConverter();

                // act (should throw exception
                ConverterFactory.Get(typeof(object));

                // act
                ConverterFactory.RegisterConverter(converterStub);
                var objectConverter = ConverterFactory.Get(typeof(object));

                // assert
                Assert.IsNotNull(objectConverter);
                Assert.IsInstanceOfType(objectConverter, typeof(TestableObjectConverter));
            }

            [TestMethod]
            public void CanReplaceAConverter()
            {
                // arrange
                var converterStub = new TestableStringConverter();

                // act
                var stringConverter = ConverterFactory.Get(typeof(string));

                // assert
                Assert.IsNotNull(stringConverter);
                Assert.IsInstanceOfType(stringConverter, typeof(StringConverter));

                // act
                ConverterFactory.RegisterConverter(converterStub);
                var newConverter = ConverterFactory.Get(typeof(string));

                // assert
                Assert.IsNotNull(newConverter);
                Assert.IsInstanceOfType(newConverter, typeof(TestableStringConverter));
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
