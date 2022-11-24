using GraphQL.Types;
using MovieReviews.Database;
using Movies.service.Common.Models;
using Movies.Service.GraphQL;

namespace MovieReviews.GraphQL.Types
{
    public sealed class DrinkObject : ObjectGraphType<Drink>
    {
        public DrinkObject(IReviewRepository repository)
        {
            Name = nameof(Drink);
            Description = "A Drink in the collection";

            Field(d => d.BadgeId).Description("Identifier of the Drink");
            Field(d => d.SucreCount).Description("The Number Of The Sucre");
            Field(d => d.HasMug).Description("Has a Mug");
            Field(
                name: "DrinkType",
                description: "Drink Type (Coffe, Tea...)",
                type: typeof(DrinkTypeEnumType),
                resolve: d => d.Source.DrinkType);

            Field(
                name: "User",
                description: "Owner of the Drink",
                type: typeof(UserObject),
                resolve: d =>
                {
                    return d.Source.User;
                });

            Field(
                name: "Character",
                description: "Character",
                type: typeof(CharacterInterface),
                resolve: d =>
                {
                    return d.Source.character ?? new Droid();
                });


            Field(
                name: "Secret",
                description: "Secret",
                type: typeof(TestSecretType),
                resolve: d =>
                {
                    return new TestSecret() { Secret = "50000" };
                });


        }
    }
}