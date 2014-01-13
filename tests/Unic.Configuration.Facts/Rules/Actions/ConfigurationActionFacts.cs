namespace Unic.Configuration.Facts.Rules.Actions
{
    using Unic.Configuration.Rules;
    using Unic.Configuration.Rules.Actions;
    using Xunit;

    public class ConfigurationActionFacts
    {
        public class TheApplyMethod
        {
            [Fact]
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
