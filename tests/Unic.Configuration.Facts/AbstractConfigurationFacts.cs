namespace Unic.Configuration.Facts
{
    using System;
    using Moq;
    using Unic.Configuration.Converter;
    using Xunit;
    using Xunit.Extensions;

    public class AbstractConfigurationFacts
    {
        public class AddValueMethod
        {
            [Fact]
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
                Assert.Equal("Test Value", testableConfiguration.TestProperty);
            }

            [Fact]
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
            [Fact]
            public void ReturnsFalseWhenNoValueIsAdded()
            {
                // arrange
                var testableConfiguration = new TestableConfiguration();

                // act
                var hasValue = testableConfiguration.HasValueFor<TestableConfiguration, string>(p => p.TestProperty);

                // assert
                Assert.False(hasValue);
            }

            [Fact]
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

            [Theory, 
            InlineData("Test Value", true),
            InlineData("", false),
            InlineData(null, false)
            ]
            public void ChecksConfigurationFieldValue(string fieldValue, bool expected)
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
                Assert.Equal(hasValue, expected);
            }
        }

        private class TestableConfiguration : AbstractConfiguration
        {
            public string TestProperty { get; set; }
        }
    }
}
