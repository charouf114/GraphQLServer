using GraphQL.Types;
using Movies.service.Common.Models;
using System.Collections.Generic;

namespace Movies.Service.GraphQL
{
    public class DroidType : ObjectGraphType<Droid>
    {
        public DroidType()
        {
            Name = "Droid";
            Description = "A mechanical creature in the Star Wars universe.";

            Field(d => d.Id).Description("The id of the droid.");
            Field(d => d.Name).Description("The name of the droid.");

            Field<ListGraphType<CharacterInterface>>(
              "friends",
              resolve: context =>
              {
                  var userContext = context.UserContext as MyGraphQLUserContext;
                  return new List<ICharacter>() { new Droid() { Name = "Gaida Gaida" }, new Droid() { Name = "Aziz Yor9ed 3la Zliz" } };
              }
            );
            Field(d => d.PrimaryFunction, nullable: true).Description("The primary function of the droid.");

            Interface<CharacterInterface>();
        }
    }
}
