using Movies.Service.Common.Models;
using System.Net.Http;
using System.Net.Http.Json;
using Xunit;

namespace IntegrationTests
{
    [Collection("Integration")]
    public class SignUpTests
    {
        private readonly HttpClient _client;

        public SignUpTests(IntegrationFixture fixture)
        {
            _client = fixture.Client;
        }

        public class GraphQLObj
        {
            public string OperationType { get; set; }
            public string query { get; set; }
            public string Variable { get; set; }
        }

        public class Data<T>
        {
            public T signUp { get; set; }
        }

        public class Root<T> where T : class
        {
            public Data<T> data { get; set; }
        }

        [Fact]
        public async void SignUpTest()
        {
            var mutation = "mutation {\r\n  signUp(\r\n    signUpRequest: {\r\n      firstName: \"string\"\r\n      lastName: \"string\"\r\n      mail: \"string@string.com\"\r\n      phoneNumber: \"string\"\r\n      password: \"string\"\r\n      reTypedPassword: \"string\"\r\n      function: \"string\"\r\n      roles: \"string\"\r\n    }\r\n  ) {\r\n    accessToken\r\n    firstName\r\n    id\r\n    lastName\r\n    refreshToken\r\n    username\r\n  }\r\n}\r\n";

            var a = new GraphQLObj
            {
                query = mutation,
            };

            using (var postRentalResponse = await _client.PostAsJsonAsync($"/graphql", a))
            {
                var stringResult = await postRentalResponse.Content.ReadAsStringAsync();
                var postResult = await postRentalResponse.Content.ReadFromJsonAsync<Root<SignUpResponse>>();

                Assert.True(postRentalResponse.IsSuccessStatusCode);
            }
        }
    }
}
