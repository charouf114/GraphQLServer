using GraphQL.Types;

namespace Movies.Service.GraphQL
{
    public class TestSecretType : ObjectGraphType<TestSecret>
    {
        public TestSecretType()
        {
            this.RequirePermission("READ_ONLY");
            Field(x => x.Secret).RequirePermission("Akora");
        }
    }

    public class TestSecret
    {
        public string Secret { get; set; }
    }
}
