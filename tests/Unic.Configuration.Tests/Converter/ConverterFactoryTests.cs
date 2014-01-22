namespace Unic.Configuration.Tests.Converter
{
    using NUnit.Framework;
    using Unic.Configuration.Converter;

    public class ConverterFactoryTests
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
        }
    }
}