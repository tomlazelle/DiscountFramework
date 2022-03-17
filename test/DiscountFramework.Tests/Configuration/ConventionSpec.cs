using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace DiscountFramework.Tests.Configuration
{
    public abstract class ConventionSpec:ISubjectBase
    {
        protected IFixture _fixture;

        protected ConventionSpec()
        {
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization
            {
                ConfigureMembers = true
            });
        }

        public abstract void FixtureSetup();
        
        public virtual void FixtureTearDown()
        {
         
        }
    }
}