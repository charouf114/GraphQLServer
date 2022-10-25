using Autofac;
using GraphQL.Server;
using GraphQL.Server.Ui.Altair;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieReviews.Database;
using MovieReviews.GraphQL;
using MovieReviews.Middleware;
using MovieReviews.Services;
using Movies.service.Common.Models;
using Movies.Service.Common.Helpers;
using Movies.Service.GraphQL;

namespace MovieReviews
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureSpecificServices(services);

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
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // configure DI for application services

            services
                .AddGraphQL(
                    (options, provider) =>
                    {
                        // Load GraphQL Server configurations
                        var graphQLOptions = Configuration
                            .GetSection("GraphQL")
                            .Get<GraphQLOptions>();
                        options.ComplexityConfiguration = graphQLOptions.ComplexityConfiguration;
                        //options.EnableMetrics = graphQLOptions.EnableMetrics;
                        options.EnableMetrics = false;
                        // Log errors
                        var logger = provider.GetRequiredService<ILogger<Startup>>();
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
        public virtual void ConfigureSpecificServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                           options.UseSqlServer(
                               Configuration.GetConnectionString("DefaultConnection"),
                               b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }


        public virtual void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MovieRepository>().As<IMovieRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DrinkRepository>().As<IDrinkRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ReviewRepository>().As<IReviewRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DocumentWriter>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<QueryObject>().AsSelf().SingleInstance();
            builder.RegisterType<MovieReviewSchema>().AsSelf().SingleInstance();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseGraphQL<MovieReviewSchema>();
            // Enables Altair UI at path /
            app.UseGraphQLAltair(new GraphQLAltairOptions { Path = "/" });


            app.UseHttpsRedirection();
        }
    }
}