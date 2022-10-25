using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MovieReviews.GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Xunit;

namespace IntegrationTests
{

    [CollectionDefinition("Integration")]
    public sealed class IntegrationFixture : IDisposable, ICollectionFixture<IntegrationFixture>
    {
        public HttpClient Client { get; }

        public IConfigurationRoot config { get; set; }
        public IntegrationFixture()
        {
            config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.test.json")
            .AddEnvironmentVariables()
            .Build(); ;

            Client = Host.CreateDefaultBuilder()
                        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                        .ConfigureWebHostDefaults(webHostBuilder =>
                        {
                            webHostBuilder
                            .UseTestServer()
                            .UseStartup<TestStartup>()
                            .UseContentRoot(AppContext.BaseDirectory)
                            .UseUrls("http://localhost:5000");
                        })
                        .StartAsync().Result.GetTestClient();

            // When The case of Simple DI
            //TestServer _server = new TestServer(new WebHostBuilder()
            //    .UseStartup<TestStartup2>()
            //    .ConfigureTestServices(ConfigureTestServices)
            //    .ConfigureTestContainer<ContainerBuilder>(ConfigureTestContainer));

            //Client = _server.CreateClient();
        }

        public void RegisterGraphQLTypes(ContainerBuilder builder)
        {
            var graphQLAssembly = Assembly.GetAssembly(typeof(MovieObject));
            var L = new HashSet<string>() { "EnumerationGraphType`1", "InterfaceGraphType`1", "ObjectGraphType`1", "InputObjectGraphType`1" };

            var graphQLTypes = graphQLAssembly.GetTypes()
                .Where(t => L.Contains(t.GetTypeInfo()?.BaseType?.Name))
                .ToList();

            foreach (var t in graphQLTypes)
            {
                // Find a way to register types with reflexion
                //builder.RegisterType<t>().AsSelf().InstancePerLifetimeScope();
            }
        }
        public void Dispose()
        {
            Client.Dispose();
        }
    }
}

