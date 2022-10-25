using GraphQL.Types;
using Movies.service.Common.Models;

namespace Movies.Service.GraphQL
{
    public class DrinkResponseType : ObjectGraphType<Response>
    {
        public DrinkResponseType()
        {
            Name = nameof(Response);
            Description = "A Base Response";
            Field(r => r.ResultCode).Description("ResultCode");
        }
    }
}
