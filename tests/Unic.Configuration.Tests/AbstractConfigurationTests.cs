namespace Unic.Configuration.Tests
{
    using System;
    using Moq;
    using NUnit.Framework;
    using Unic.Configuration.Converter;

    public class AbstractConfigurationTests
    {
        [TestFixture]
        public class AddValueMethod
        {
            [Test]
            public void CanSetStringValueOnStringProperty()
            {
                // arrange
                var converterMock = new Mock<AbstractConverter<string>>();
                converterMock.Setup(converter => converter.Convert(It.IsAny<IConfigurationField>()))
                    .Returns((IConfigurationField x) => "Test Value");

                var testProperty = typeof(TestableConfiguration).GetProperty("TestProperty");
                var testableConfiguration = new TestableConfiguration();

                // act
                testableConfiguration.AddValue(testProperty, new ConfigurationField(), converterMock.Object);

                // assert
                Assert.AreEqual("Test Value", testableConfiguration.TestProperty);
            }

            [Test]
            public void CannotSetIntValueOnStringProperty()
            {
                // arrange
                var converterMock = new Mock<AbstractConverter<int>>();
                converterMock.Setup(converter => converter.Convert(It.IsAny<IConfigurationField>()))
                    .Returns((IConfigurationField x) => 1);

                var testProperty = typeof(TestableConfiguration).GetProperty("TestProperty");
                var testableConfiguration = new TestableConfiguration();

                // assert
                Assert.Throws<ArgumentException>(() => testableConfiguration.AddValue(testProperty, new ConfigurationField(), converterMock.Object));
            }
        }

        public class HasValueForMethod
        {
            [Test]
            public void ReturnsFalseWhenNoValueIsAdded()
            {
                // arrange
                var testableConfiguration = new TestableConfiguration();

                // act
                var hasValue = testableConfiguration.HasValueFor<TestableConfiguration, string>(p => p.TestProperty);

                // assert
                Assert.False(hasValue);
            }

            [Test]
            public void ReturnsTrueWhenValueIsAdded()
            {
                // arrange
                var converterMock = new Mock<AbstractConverter<string>>();
                converterMock.Setup(converter => converter.Convert(It.IsAny<IConfigurationField>()))
                    .Returns((IConfigurationField x) => "Test Value");

                var testProperty = typeof(TestableConfiguration).GetProperty("TestProperty");
                var testableConfiguration = new TestableConfiguration();
                testableConfiguration.AddValue(testProperty, new ConfigurationField { Value = "Test Value" }, converterMock.Object);
                
                // act
                var hasValue = testableConfiguration.HasValueFor<TestableConfiguration, string>(p => p.TestProperty);

                // assert
                Assert.True(hasValue);
            }

            [TestCase("Test Value", Result = true)]
            [TestCase("", Result = false)]
            [TestCase(null, Result = false)]
            public bool ChecksConfigurationFieldValue(string fieldValue)
            {
                // arrange
                var converterMock = new Mock<AbstractConverter<string>>();
                converterMock.Setup(converter => converter.Convert(It.IsAny<IConfigurationField>()))
                    .Returns((IConfigurationField x) => "Test Value");

                var testProperty = typeof(TestableConfiguration).GetProperty("TestProperty");
                var testableConfiguration = new TestableConfiguration();
                testableConfiguration.AddValue(testProperty, new ConfigurationField { Value = fieldValue }, converterMock.Object);

                // act
                var hasValue = testableConfiguration.HasValueFor<TestableConfiguration, string>(p => p.TestProperty);

                // assert
                return hasValue;
            }
        }

        private class TestableConfiguration : AbstractConfiguration
        {
            public string TestProperty { get; set; }
        }
    }
}
