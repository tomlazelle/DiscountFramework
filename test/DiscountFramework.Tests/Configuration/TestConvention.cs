using Fixie;

namespace DiscountFramework.Tests.Configuration
{
    public class TestConvention : Discovery, Execution
    {
        public TestConvention()
        {
            Classes.Where(x => x.Name.EndsWith("test") || x.Name.EndsWith("Test"));
            Methods.Where(x => (x.IsVoid() || x.IsAsync())
                              && x.IsPublic
                              && x.Name != "FixtureSetup"
                              && x.Name != "FixtureTearDown")
                .OrderBy(x => x, new DeclarationOrderComparer());
        }

        public void Execute(TestClass testClass)
        {
            var instance = testClass.Construct();

            testClass.Type.TryInvoke("FixtureSetup", instance);
            testClass.RunCases(testcase => testcase.Execute(instance));
            testClass.Type.TryInvoke("FixtureTearDown", instance);

            instance.Dispose();
        }
    }
}