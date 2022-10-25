using System;
using System.Collections.Generic;
using GraphQL;
using GraphQL.Types;
using Movies.service.Common.Models;
using MovieReviews.Database;
using MovieReviews.GraphQL.Types;

namespace MovieReviews.GraphQL
{
    public class QueryObject : ObjectGraphType<object>
    {
        public QueryObject(IMovieRepository movieRepository, IDrinkRepository drinkRepository)
        {
            Name = "Queries";
            Description = "The base query for all the entities in our object graph.";

            FieldAsync<MovieObject, Movie>(
                "movie",
                "Gets a movie by its unique identifier.",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id",
                        Description = "The unique GUID of the movie."
                    }),
                context => movieRepository.GetMovieByIdAsync(context.GetArgument("id", Guid.Empty)));

            FieldAsync<ListGraphType<MovieObject>, List<Movie>>(
                "movies",
                "Gets all movies by its unique identifier.",
                null,
                context => movieRepository.GetAllMovies());

            FieldAsync<DrinkObject, Drink>(
                "LastDrink",
                "Get Last Drink Per Badge Id",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "badgeId",
                        Description = "The unique GUID of the movie."
                    }),

                context =>
                {
                    var badgeId = context.GetArgument("badgeId", "");
                    var drink = new Drink();
                    drink.BadgeId = badgeId;
                    return drinkRepository.GetLatestCoffe(drink);
                });
        }
    }
}