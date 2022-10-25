using GraphQL;
using GraphQL.Types;
using MovieReviews.Database;
using MovieReviews.GraphQL.Types;
using MovieReviews.Services;
using Movies.service.Common.Models;
using Movies.Service.Common.Models;
using Movies.Service.Common.Models.Login.SignUp;
using System;

namespace MovieReviews.GraphQL
{
    public class MutationObject : ObjectGraphType<object>
    {
        public MutationObject(IReviewRepository reviewRepository, IMovieRepository movieRepository, IDrinkRepository drinkRepository, IUserService userService)
        {
            Name = "Mutations";
            Description = "The base mutation for all the entities in our object graph.";

            FieldAsync<ReviewObject, Review>(
                "addReview",
                "Add review to a movie.",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id",
                        Description = "The unique GUID of the movie."
                    },
                    new QueryArgument<NonNullGraphType<ReviewInputObject>>
                    {
                        Name = "review",
                        Description = "Review for the movie."
                    }),
                context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var review = context.GetArgument<Review>("review");
                    return reviewRepository.AddReviewToMovieAsync(id, review);
                });

            FieldAsync<MovieObject, Movie>(
                "addMovie",
                "Add a movie.",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<MovieInputObject>>
                    {
                        Name = "movie",
                        Description = "movie."
                    }),
                context =>
                {
                    var movie = context.GetArgument<Movie>("movie");
                    return movieRepository.AddMovie(movie);
                });

            FieldAsync<DrinkObject, Drink>(
                 "addDrink",
                 "Add a drink.",
                 new QueryArguments(
                     new QueryArgument<NonNullGraphType<DrinkInputObject>>
                     {
                         Name = "drink",
                         Description = "drink."
                     }),
                 context =>
                 {
                     var drink = context.GetArgument<Drink>("drink");
                     return drinkRepository.AddCoffe(drink);
                 });

            FieldAsync<AuthenticateObject, AuthenticateResponse>(
                "Authenticate",
                "Authenticate with UserName And Password",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<AuthenticateRequestInputObject>>
                    {
                        Name = "AuthenticateRequest",
                        Description = "AuthenticateRequest."
                    }),
                context =>
                {
                    var model = context.GetArgument<AuthenticateRequest>("AuthenticateRequest");
                    return userService.Authenticate(model);
                });

            FieldAsync<SignUpObject, SignUpResponse>(
               "SignUp",
               "Sign Up",
               new QueryArguments(
                   new QueryArgument<NonNullGraphType<SignUpInputObject>>
                   {
                       Name = "SignUpRequest",
                       Description = "SignUpRequest."
                   }),
               context =>
               {
                   var model = context.GetArgument<SignUpInput>("SignUpRequest");
                   return userService.SignUp(model);
               });

            FieldAsync<AuthenticateObject, AuthenticateResponse>(
               "SignIn",
               "Authenticate with UserName And Password",
               new QueryArguments(
                   new QueryArgument<NonNullGraphType<AuthenticateRequestInputObject>>
                   {
                       Name = "AuthenticateRequest",
                       Description = "AuthenticateRequest."
                   }),
               context =>
               {
                   var model = context.GetArgument<AuthenticateRequest>("AuthenticateRequest");
                   return userService.Authenticate(model);
               });
        }
    }
}