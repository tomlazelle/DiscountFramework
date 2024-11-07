namespace Discount.Tests.Configuration;

public interface ISubjectBase : IDisposable
{
    void FixtureSetup();
    void FixtureTearDown();
}