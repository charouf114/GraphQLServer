using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieReviews;
using MovieReviews.Database;
using MovieReviews.GraphQL.Types;
using Movies.Service.GraphQL;

namespace IntegrationTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureSpecificServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseInMemoryDatabase(databaseName: "IntegrationDatabase"));
        }

        public override void ConfigureGraphQLTypes(ContainerBuilder builder)
        {
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
    }
}
