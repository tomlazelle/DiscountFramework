using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoMapper;
using DiscountApi;
using DiscountFramework.Common;
using DiscountFramework.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using Testcontainers.RavenDb;

namespace Discount.Tests.Configuration;

public static class ContainerFixture
{
    public static int RavenDBPort { get; set; }
    public static string RavendDbEndpoint { get; set; }
}

public class RavenDBTestContainerFixture : IAsyncLifetime
{
    private RavenDbContainer _ravenDbContainer = default!;

    public async Task InitializeAsync()
    {
        ContainerFixture.RavenDBPort = Random.Shared.Next(8000, 8999);
        _ravenDbContainer = new RavenDbBuilder()
            .WithPortBinding(ContainerFixture.RavenDBPort, 8080)
            .Build();

        await _ravenDbContainer.StartAsync();

        ContainerFixture.RavendDbEndpoint = _ravenDbContainer.GetConnectionString();
    }

    public async Task DisposeAsync()
    {
        await _ravenDbContainer.DisposeAsync();
    }
}

public class TestServerFixture : WebApplicationFactory<Startup>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
    }
}

public abstract class Integration<TClassUnderTest> : ISubjectBase
{
    public Integration(
        TestServerFixture testServerFixture,
        RavenDBTestContainerFixture ravenDBTestContainerFixture)
    {
    }

    public void FixtureSetup()
    {
    }

    public void FixtureTearDown()
    {
    }

    public void Dispose()
    {
        FixtureTearDown();
    }
}

public abstract class Subject<TClassUnderTest> : ISubjectBase
    where TClassUnderTest : class
{
    private readonly IServiceCollection _serviceCollection = new ServiceCollection();
    protected IFixture _fixture;
    protected IServiceProvider? _serviceProvider;
    private TClassUnderTest? _sut;

    protected Subject()
    {
        _fixture = new Fixture().Customize(new AutoNSubstituteCustomization
        {
            GenerateDelegates = true
        });

        _serviceCollection.AddSingleton<TClassUnderTest>();

        _serviceCollection.AddSingleton(CreateStore(ContainerFixture.RavendDbEndpoint));

        _serviceCollection.AddSingleton(new MapperConfiguration(x => x.AddProfile<MappingSetup>()).CreateMapper());
        _serviceCollection.AddProfile<DomainSetup>();

        FixtureSetup();

        _serviceProvider = _serviceCollection.BuildServiceProvider();
    }

    protected TClassUnderTest Sut
    {
        get
        {
            _sut ??= _serviceProvider.GetService<TClassUnderTest>();

            if (_sut == null)
            {
                throw new InvalidOperationException(
                    $"Service of type {typeof(TClassUnderTest)} not found in service provider");
            }

            return _sut;
        }
    }

    public virtual void FixtureSetup()
    {
    }

    public virtual void FixtureTearDown()
    {
    }

    public void Dispose()
    {
        FixtureTearDown();
    }

    private IDocumentStore CreateStore(string endpoint)
    {
        var databaseName = "DiscountData";

        var result = new DocumentStore
        {
            Database = databaseName,
            Urls = new[] {endpoint}
        }.Initialize();

        EnsureDatabaseExists(databaseName, result);

        return result;
    }

    private void EnsureDatabaseExists(string databaseName, IDocumentStore store)
    {
        if (string.IsNullOrWhiteSpace(databaseName))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(databaseName));
        }

        try
        {
            store.Maintenance.ForDatabase(databaseName).Send(new GetStatisticsOperation());
        }
        catch (DatabaseDoesNotExistException)
        {
            try
            {
                store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(databaseName)));
            }
            catch (ConcurrencyException)
            {
                // The database was already created before calling CreateDatabaseOperation
            }
        }
    }

    protected async Task SaveDiscounts(List<DiscountFramework.Discount> discounts)
    {
        try
        {
            var documentStore = _serviceProvider.GetRequiredService<IDocumentStore>();

            using (var session = documentStore.OpenAsyncSession())
            {
                foreach (var discount in discounts)
                {
                    await session.StoreAsync(discount);
                }

                await session.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    protected T MockType<T>()
    {
        return _fixture.Create<T>();
    }
}

[CollectionDefinition(nameof(DatabaseOnlyContainersCollection))]
public class DatabaseOnlyContainersCollection : ICollectionFixture<RavenDBTestContainerFixture>
{
}