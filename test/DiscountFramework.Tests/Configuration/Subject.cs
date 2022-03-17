using System;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountFramework.Tests.Configuration
{
    public abstract class Subject<TClassUnderTest> : ISubjectBase
        where TClassUnderTest : class
    {
        protected IFixture _fixture;
        protected IServiceProvider _serviceProvider;
        private TClassUnderTest _sut;

        protected TClassUnderTest Sut
        {
            get { return _sut ??= new Lazy<TClassUnderTest>(() =>
                            _serviceProvider != null ? 
                                _serviceProvider.GetService<TClassUnderTest>() : 
                                _fixture.Create<TClassUnderTest>()).Value; }
        }

        protected Subject()
        {
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization
            {
                GenerateDelegates = true
            });
        }

        public virtual void FixtureSetup()
        {

        }

        public virtual void FixtureTearDown()
        {
            
        }

        protected void Register<TInterface>(TInterface concreteType)
        {
            _fixture.Register<TInterface>(() => concreteType);
        }

        protected T MockType<T>()
        {
            return _fixture.Create<T>();
        }
    
    }


}