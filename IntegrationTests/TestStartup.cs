using GraphQL.Server.Ui.Altair;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieReviews.GraphQL;
using MovieReviews.Middleware;

namespace IntegrationTests
{
    public class TestStartup
    {
        public IConfiguration Configuration;

        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
        }

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
