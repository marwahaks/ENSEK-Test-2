using Newtonsoft.Json.Linq;
using RestSharp;

namespace ENSEK_Test
{
    public class PostTests(string baseUrl)
    {
        private string BaseUrl { get; } = baseUrl;

        public void PostResetData_Unauthorised()
        {
            string endpoint = "/ENSEK/reset";
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(endpoint);
            try
            {
                client.Post(request);
            }
            catch (HttpRequestException e)
            {
                if (e.Message.Contains("Unauthorized"))
                {
                    Assert.Pass("Reset correctly failed with an unauthorised exception");
                }
            }
            Assert.Fail("Reset did not failed with an unauthorised exception");
        }

        public void PostResetData_Authorised(JToken Bearer)
        {
            string endpoint = "/ENSEK/reset";
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(endpoint);
            var bearerToken = Bearer.ToString();

            request.AddHeader("Authorization", $"Bearer {bearerToken}");
            try
            {
                client.Post(request);
            }
            catch (HttpRequestException e)
            {
                if (e.Message.Contains("Unauthorized"))
                {
                    Assert.Fail("Reset failed with an unauthorised exception");
                }
            }
            catch(Exception e)
            {
                Assert.Fail("Reset failed with an unknown exception: " + e.Message);

            }
            Assert.Pass("Reset worked");
        }

        public JToken? Login()
        {
            string endpoint = "/ENSEK/login";

            var body = new
            {
                username = "test",
                password = "testing"
            };
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(endpoint).AddJsonBody(body);
            var response = client.Post(request);
            var json = JObject.Parse(response.Content);
            return json.SelectToken("access_token");
        }
    }
}
