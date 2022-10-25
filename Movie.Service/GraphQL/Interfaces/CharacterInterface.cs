using GraphQL.Types;
using MovieReviews.GraphQL.Types;
using Movies.service.Common.Models;
using System;

namespace Movies.Service.GraphQL
{
    public class CharacterInterface : InterfaceGraphType<ICharacter>
    {
        public CharacterInterface(DroidType droidType)
        {
            Name = "Character";
            Field(d => d.Id).Description("The id of the character.");
            Field(d => d.Name).Description("The name of the character.");
            Field<ListGraphType<CharacterInterface>>("Friends");

            ResolveType = obj =>
            {
                if (obj is Droid)
                {
                    return droidType;
                }
                throw new ArgumentOutOfRangeException($"Could not resolve graph type for {obj.GetType().Name}");
            };
        }
    }

}
