using System;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoMapper;
using DiscountFramework.Configuration;
using Fixie;

namespace DiscountFramework.Tests.Configuration
{
    public abstract class Subject<TClassUnderTest> : ISubjectBase
        where TClassUnderTest : class
    {
        private TClassUnderTest _sut;

        protected IFixture _fixture;

        protected TClassUnderTest Sut
        {
            get { return _sut ?? (_sut = new Lazy<TClassUnderTest>(() => _fixture.Create<TClassUnderTest>()).Value); }
        }

        protected Subject()
        {
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization { GenerateDelegates = true });
            Register(new MapperConfiguration(x=>x.AddProfile<MappingSetup>()).CreateMapper());
        }

        public virtual void FixtureSetup()
        {
        }

        public virtual void FixtureTearDown()
        {
            _fixture.Dispose();
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