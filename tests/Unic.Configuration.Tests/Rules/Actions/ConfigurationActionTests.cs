namespace Unic.Configuration.Tests.Rules.Actions
{
    using NUnit.Framework;
    using Unic.Configuration.Core.Rules;
    using Unic.Configuration.Core.Rules.Actions;

    public class ConfigurationActionTests
    {
        [TestFixture]
        public class TheApplyMethod
        {
            [Test]
            public void SetsRuleContextAsValid()
            {
                // arrange
                var configurationRuleContext = new ConfigurationRuleContext { IsValid = false };
                var configurationAction = new ConfigurationAction<ConfigurationRuleContext>();

                // act
                configurationAction.Apply(configurationRuleContext);

                // assert
                Assert.True(configurationRuleContext.IsValid);
            }
        }
    }
}
