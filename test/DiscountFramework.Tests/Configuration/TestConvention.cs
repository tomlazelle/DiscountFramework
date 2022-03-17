using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Fixie;

namespace DiscountFramework.Tests.Configuration
{
    public class DiscountTestProject : ITestProject
    {
        public void Configure(TestConfiguration configuration, TestEnvironment environment)
        {
            configuration.Conventions.Add<TestConvention, ExecutionConvention>();
        }
    }
    public class ExecutionConvention : IExecution
    {
        public async Task Run(TestSuite testSuite)
        {

            foreach (var testClass in testSuite.TestClasses)
            {
                foreach (var test in testClass.Tests)
                {
                    if (test.Name.EndsWith("_Skipped"))
                    {
                        continue;
                    }

                    var instance = testClass.Construct();

                    ReflectionExtensions.TryInvoke(testClass.Type, "FixtureSetup", instance);

                    await test.Run(instance);

                    ReflectionExtensions.TryInvoke(testClass.Type, "FixtureTearDown", instance);
                }
            }


        }
    }
    public class TestConvention : IDiscovery
    {



        public IEnumerable<Type> TestClasses(IEnumerable<Type> concreteClasses)
        => concreteClasses.Where(x =>
                                     x.Name.EndsWith("test") || 
                                     x.Name.EndsWith("tests") || 
                                     x.Name.EndsWith("Test") || 
                                     x.Name.EndsWith("Tests")
                                );


        public IEnumerable<MethodInfo> TestMethods(IEnumerable<MethodInfo> publicMethods)
        => publicMethods.Where(
                               x => x.IsPublic &&
                                    x.Name != "FixtureSetup" &&
                                    x.Name != "FixtureTearDown")
                .OrderBy(x => x, new DeclarationOrderComparer());


    }
}