using GraphQL.Types;
using Movies.service.Common.Models;
using Movies.Service.GraphQL;

namespace MovieReviews.GraphQL.Types
{
    public sealed class DrinkInputObject : InputObjectGraphType<Drink>
    {
        public DrinkInputObject()
        {
            Name = "DrinkInput";
            Description = "A Drink in the collection";

            Field(d => d.BadgeId).Description("Identifier of the Drink");
            Field(d => d.SucreCount).Description("The Number Of The Sucre");
            Field(d => d.HasMug).Description("Has a Mug");
            Field(
                name: "DrinkType",
                description: "Drink Type (Coffe, Tea...)",
                type: typeof(DrinkTypeEnumType),
                resolve: d => d.Source.DrinkType);
        }
    }
}