using System;
using AutoMapper;
using DiscountFramework.Common;
using DiscountFramework.Configuration;
using DiscountFramework.Tests.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace DiscountFramework.Tests.Fixtures
{
    public abstract class DomainSubject<T> : Subject<T> where T : class
    {
        protected IServiceCollection _serviceCollection = new ServiceCollection();

        public override void FixtureSetup()
        {
            _serviceCollection.AddSingleton(new MapperConfiguration(x => x.AddProfile<MappingSetup>()).CreateMapper());
            _serviceCollection.AddTransient<T>();
            _serviceCollection.AddProfile<DomainSetup>();
            _serviceCollection.AddSingleton(CreateStore());
            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        private IDocumentStore CreateStore()
        {
            var databaseName = "DiscountData";

            var result = new DocumentStore
            {
                Database = databaseName,
                Urls = new[] { "http://localhost:8080" }
            }.Initialize();

            EnsureDatabaseExists(databaseName, result);

            return result;
        }

        private void EnsureDatabaseExists(string databaseName, IDocumentStore store)
        {
            if (string.IsNullOrWhiteSpace(databaseName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(databaseName));

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
    }
}