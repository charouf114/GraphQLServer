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

            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("code", "AZNDJEDLDLSXODKCKCKVKVKVKVKVVVDHHSQ");

            using (var postRentalResponse = await _client.PostAsJsonAsync($"/graphql", a))
            {
                var postResult = await postRentalResponse.Content.ReadFromJsonAsync<Root<SignUpResponse>>();
                var signUpResult = postResult.data.signUp;

                Assert.NotNull(signUpResult.Username);
                Assert.NotNull(signUpResult.FirstName);
                Assert.NotNull(signUpResult.LastName);
                Assert.NotNull(signUpResult.RefreshToken);
                Assert.NotNull(signUpResult.AccessToken);

                Assert.True(postRentalResponse.IsSuccessStatusCode);
            }

            using (var postRentalResponse = await _client.PostAsJsonAsync($"/graphql", a))
            {
                var postResult = await postRentalResponse.Content.ReadFromJsonAsync<Root<SignUpResponse>>();
                var signUpResult = postResult.data.signUp;

                Assert.Null(signUpResult);

                Assert.True(postRentalResponse.IsSuccessStatusCode);
            }
        }
    }
}
