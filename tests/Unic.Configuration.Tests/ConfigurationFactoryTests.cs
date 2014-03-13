namespace Unic.Configuration.Tests
{
    using System;
    using NUnit.Framework;

    public class ConfigurationFactoryTests
    {
        [TestFixture]
        public class TheCreateMethod
        {
            [TestCase("")]
            [TestCase("dummy")]
            public void WillReturnNullForUnknownType(string fullType)
            {
                // act
                var configuration = ConfigurationFactory.Create(fullType, null);

                // assert
                Assert.IsNull(configuration);
            }

            [Test]
            public void WillThrowExceptionForNullArgument()
            {
                // assert
                Assert.Throws<ArgumentNullException>(() => ConfigurationFactory.Create(null, null));
            }
        }
    }
}
