namespace Unic.Configuration.Facts
{
    using Xunit;

    public class TypeResolverFacts
    {
        public class ConfigurationProperty
        {
            [Fact]
            public void ReturnsTheSameInstance()
            {
                // act
                var configurationManager1 = TypeResolver.Configuration;
                var configurationManager2 = TypeResolver.Configuration;

                // assert
                Assert.Same(configurationManager1, configurationManager2);
            }

            [Fact]
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
        [Fact]
        public void ReturnsTheSameInstance()
        {
            // act
            var settings1 = TypeResolver.Settings;
            var settings2 = TypeResolver.Settings;

            // assert
            Assert.Same(settings1, settings2);
        }

        [Fact]
        public void DoesNotReturnNull()
        {
            // act
            var settings = TypeResolver.Settings;

            // assert
            Assert.NotNull(settings);
        }
    }
}