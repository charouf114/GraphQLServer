using GraphQL.Types;
using Movies.service.Common.Models;

namespace MovieReviews.GraphQL.Types
{
    public sealed class MovieInputObject : InputObjectGraphType<Movie>
    {
        public MovieInputObject()
        {
            Name = "MovieInput";
            Description = "A movie in the collection";

            Field(m => m.Id).Description("Identifier of the movie");
            Field(m => m.Name).Description("Name of the movie");
        }
    }
}