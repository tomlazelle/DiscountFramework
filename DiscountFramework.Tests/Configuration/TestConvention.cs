using System;
using System.Reflection;
using Fixie;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Fixture = Fixie.Fixture;

namespace DiscountFramework.Tests.Configuration
{
    public class TestConvention : Convention
    {
        public TestConvention()
        {
            Classes.Where(x => x.Name.ToLower().EndsWith("test"));

            ClassExecution.CreateInstancePerClass();

            Methods.Where(x =>
                x.IsVoid() &&
                x.IsPublic &&
                x.Name != "FixtureSetup" &&
                x.Name != "FixtureTeardown");

            FixtureExecution.Wrap<AMFixtureBehavior>();
        }
    }

    public class AMFixtureBehavior : FixtureBehavior
    {
        public void Execute(Fixture context, Action next)
        {
            var registry = new Ploeh.AutoFixture.Fixture().Customize(new AutoNSubstituteCustomization());


            context.Instance.GetType().TryInvoke("FixtureSetup", context.Instance, registry);

            next();

            context.Instance.GetType().TryInvoke("FixtureTeardown", context.Instance);
        }
    }

    public static class BehaviorBuilderExtensions
    {
        public static void TryField(this Fixture context, string fieldName, object fieldValue)
        {
            var lifecycleMethod = context.Class.Type.GetField(fieldName, BindingFlags.Public | BindingFlags.Instance);

            if (lifecycleMethod == null)
                return;

            try
            {
                lifecycleMethod.SetValue(context.Instance, fieldValue);
            }
            catch (TargetInvocationException exception)
            {
                throw new PreservedException(exception.InnerException);
            }
        }

        public static void TryInvoke(this Type type, string method, object instance, IFixture fixture = null)
        {
            var lifecycleMethod = type.GetMethod(method);
            //                type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
            //                    .SingleOrDefault(x => ReflectionExtensions.HasSignature(x, typeof(void), method));

            if (lifecycleMethod == null)
                return;

            try
            {
                if (fixture == null)
                {
                    lifecycleMethod.Invoke(instance, null);
                }
                else
                {
                    lifecycleMethod.Invoke(instance, new object[]
                    {
                        fixture
                    });
                }
            }
            catch (TargetInvocationException exception)
            {
                throw new PreservedException(exception.InnerException);
            }
        }
    }

    public abstract class BaseTest
    {
        public abstract void FixtureSetup(IFixture registry);
        public abstract void FixtureTeardown();
    }
}