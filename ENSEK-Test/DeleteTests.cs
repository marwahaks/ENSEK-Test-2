using RestSharp;

namespace ENSEK_Test
{
    public class DeleteTests(string baseUrl)
    {
        private string BaseUrl { get; } = baseUrl;

        //DEFECT: Internal Server Error
        public void DeleteOrder()
        {
            //hardcoded as this is test data and we can get back to it - if i had more time, i would get run GetOrders extract one order ID and pass that.
            string endpoint = $"/ENSEK/orders/2cdd6f69-95df-437e-b4d3-e772472db8de";
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(endpoint);
            var response = client.Delete(request);
        }

    }
}
