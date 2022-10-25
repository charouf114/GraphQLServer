using Autofac;
using Autofac.Extensions.DependencyInjection;
using GraphQL.NewtonsoftJson;
using GraphQL.Server;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieReviews.Database;
using MovieReviews.GraphQL;
using MovieReviews.GraphQL.Types;
using MovieReviews.Services;
using Movies.service.Common.Models;
using Movies.Service.Common.Helpers;
using Movies.Service.GraphQL;
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

            Action<ContainerBuilder> aaa = ConfigureTestContainer;
            Client = Host.CreateDefaultBuilder()
                        .UseServiceProviderFactory(new AutofacServiceProviderFactory(aaa))
                        .ConfigureWebHostDefaults(webHostBuilder =>
                        {
                            webHostBuilder
                            .UseTestServer()
                            .UseStartup<TestStartup>()
                            .ConfigureTestServices(ConfigureTestServices)
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

        public void ConfigureTestServices(IServiceCollection services)
        {
            // Mock The DB
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseInMemoryDatabase(databaseName: "IntegrationDatabase"));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddSingleton<EnumerationGraphType<DrinkType>, DrinkTypeEnumType>();
            services.AddSingleton<ObjectGraphType<Response>, DrinkResponseType>();
            services.AddSingleton<InterfaceGraphType<ICharacter>, CharacterInterface>();
            services.AddSingleton<ObjectGraphType<Droid>, DroidType>();
            services.AddSingleton<IValidationRule, RequiresAuthValidationRule>();
            services.AddScoped<IUserService, UserService>();

            services.AddAuthorization();
            services.AddAuthentication();

            // configure strongly typed settings object
            services.Configure<AppSettings>(config.GetSection("AppSettings"));

            // configure DI for application services

            services
                .AddGraphQL(
                    (options, provider) =>
                    {
                        // Load GraphQL Server configurations
                        var graphQLOptions = config
                            .GetSection("GraphQL")
                            .Get<GraphQLOptions>();
                        options.ComplexityConfiguration = graphQLOptions.ComplexityConfiguration;
                        //options.EnableMetrics = graphQLOptions.EnableMetrics;
                        options.EnableMetrics = false;
                        // Log errors
                        var logger = provider.GetRequiredService<ILogger<TestStartup>>();
                        options.UnhandledExceptionDelegate = ctx =>
                            logger.LogError("{Error} occurred", ctx.OriginalException.Message);
                    })
                .AddUserContextBuilder(httpContext => new MyGraphQLUserContext(httpContext.User))
                // Adds all graph types in the current assembly with a singleton lifetime.
                .AddGraphTypes()
                // Add GraphQL data loader to reduce the number of calls to our repository. https://graphql-dotnet.github.io/docs/guides/dataloader/
                .AddDataLoader()
                .AddSystemTextJson();
        }

        public void ConfigureTestContainer(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MovieRepository>().As<IMovieRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DrinkRepository>().As<IDrinkRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ReviewRepository>().As<IReviewRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DocumentWriter>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<QueryObject>().AsSelf().SingleInstance();
            builder.RegisterType<MutationObject>().AsSelf().SingleInstance();
            builder.RegisterType<MovieReviewSchema>().AsSelf().SingleInstance();
            builder.RegisterType<AutofacServiceProvider>().As<IServiceProvider>().InstancePerLifetimeScope();

            // GraphQL Types
            builder.RegisterType<TestSecretType>().AsSelf().SingleInstance();
            builder.RegisterType<DrinkTypeEnumType>().AsSelf().SingleInstance();
            builder.RegisterType<CharacterInterface>().AsSelf().SingleInstance();
            builder.RegisterType<DrinkResponseType>().AsSelf().SingleInstance();
            builder.RegisterType<DroidType>().AsSelf().SingleInstance();
            builder.RegisterType<AuthenticateObject>().AsSelf().SingleInstance();
            builder.RegisterType<AuthenticateRequestInputObject>().AsSelf().SingleInstance();
            builder.RegisterType<DrinkInputObject>().AsSelf().SingleInstance();
            builder.RegisterType<DrinkObject>().AsSelf().SingleInstance();
            builder.RegisterType<MovieInputObject>().AsSelf().SingleInstance();
            builder.RegisterType<MovieObject>().AsSelf().SingleInstance();
            builder.RegisterType<ReviewInputObject>().AsSelf().SingleInstance();
            builder.RegisterType<ReviewObject>().AsSelf().SingleInstance();
            builder.RegisterType<SignUpInputObject>().AsSelf().SingleInstance();
            builder.RegisterType<SignUpObject>().AsSelf().SingleInstance();
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

