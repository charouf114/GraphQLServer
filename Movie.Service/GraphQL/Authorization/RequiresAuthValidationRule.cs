using GraphQL.Language.AST;
using GraphQL.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Service.GraphQL
{
    public class RequiresAuthValidationRule : IValidationRule
    {
        public Task<INodeVisitor> ValidateAsync(ValidationContext context)
        {
            var userContext = context.UserContext as MyGraphQLUserContext;
            var authenticated = userContext.User?.Identity.IsAuthenticated ?? false;

            INodeVisitor result = new EnterLeaveListener(_ =>
            {
                _.Match<Operation>(op =>
                {
                    if (op.OperationType == OperationType.Mutation && !authenticated)
                    {
                        context.ReportError(new ValidationError(
                            context.Document.OriginalQuery,
                            "6.1.1", // the rule number of this validation error corresponding to the paragraph number from the official specification
                            $"Authorization is required to access {op.Name}.",
                            op)
                        { Code = "auth-required" });
                    }
                });

                // this could leak info about hidden fields in error messages
                // it would be better to implement a filter on the schema so it
                // acts as if they just don't exist vs. an auth denied error
                // - filtering the schema is not currently supported
                _.Match<Field>(fieldAst =>
                {
                    var fieldDef = context.TypeInfo.GetFieldDef();
                    if (fieldDef.RequiresPermissions() &&
                        (!authenticated || !fieldDef.CanAccess(userContext.User.Claims.Select(x => x.Value))))
                    {
                        context.ReportError(new ValidationError(
                            context.Document.OriginalQuery,
                            "6.1.1", // the rule number of this validation error corresponding to the paragraph number from the official specification
                            $"You are not authorized to run this query.",
                            fieldAst)
                        { Code = "auth-required" });
                    }
                });
            }); ;

            return Task.FromResult(result);
        }
    }
}
