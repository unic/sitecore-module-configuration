namespace Unic.Configuration.Tests
{
    using NUnit.Framework;
    using Unic.Configuration.Core;

    public class TypeResolverTests
    {
        [TestFixture]
        public class ConfigurationProperty
        {
            [Test]
            public void ReturnsTheSameInstance()
            {
                // act
                var configurationManager1 = TypeResolver.Configuration;
                var configurationManager2 = TypeResolver.Configuration;

                // assert
                Assert.AreSame(configurationManager1, configurationManager2);
            }

            [Test]
            public void DoesNotReturnNull()
            {
                // act
                var configurationManager = TypeResolver.Configuration;

                // assert
                Assert.NotNull(configurationManager);
            }
        }
    }

    public class SettingsProperty
    {
        [Test]
        public void ReturnsTheSameInstance()
        {
            // act
            var settings1 = TypeResolver.Settings;
            var settings2 = TypeResolver.Settings;

            // assert
            Assert.AreSame(settings1, settings2);
        }

        [Test]
        public void DoesNotReturnNull()
        {
            // act
            var settings = TypeResolver.Settings;

            // assert
            Assert.NotNull(settings);
        }
    }
}