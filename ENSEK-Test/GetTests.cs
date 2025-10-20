using ENSEK_Test.PocoObjects;
using RestSharp;
using System.Globalization;
using System.Net;

namespace ENSEK_Test
{
    public class GetTests(string baseUrl)
    {
        private string BaseUrl { get; } = baseUrl;

        public void GetOrders()
        {
            string endpoint = "/ENSEK/orders";
            var response = GetOrderResponse(endpoint);
        }

        public void GetOrdersPriorCurrDate()
        {
            string endpoint = "/ENSEK/orders";
            var response = GetOrderResponse(endpoint);
            var pastOrders = new List<OrderType>();

            foreach (var item in response)
            {
                if (ConvertTime(item.Time).Date < DateTime.Today.Date) //didn't take the time into account as requirement was date.
                {
                    pastOrders.Add(item);
                }
            }

            Assert.Pass($"There were {pastOrders.Count} orders before the current date");
        }
       
        //DEFECT: RETURNS WRONG ORDER
        public void GetSingleOrder()
        {
            //hardcoded as this is test data and we can get back to it - if i had more time, i would get run GetOrders extract one order ID and pass that.
            string endpoint = $"/ENSEK/orders/2cdd6f69-95df-437e-b4d3-e772472db8de";
            var response = GetOrderResponse(endpoint);
        }

        public EnergyTypes? GetEnergy()
        {
            string endpoint = "/ENSEK/energy";
            var response = GetEnergyResponse(endpoint);
            return response;
        }

        // I would refactor this, as it's not an elegant solution (I didn't want to create two methods that do similar actions
        internal List<OrderType>? GetOrderResponse(string endpoint)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(endpoint);
            dynamic response;

            try
            {
                response = client.Get<List<OrderType>>(request);
            }
            catch
            {
                var rep = client.Get<OrderType>(request);
                response = new List<OrderType> { rep };
            }
            return response;
        }
        
        private EnergyTypes? GetEnergyResponse(string endpoint)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(endpoint);
            var response = client.Get<EnergyTypes>(request);
            return response;
        }
        private DateTime ConvertTime(string time)
        {
            var splitTime = time.Split(',')[1].Trim();
            const string Format = "d MMM yyyy HH:mm:ss 'GMT'";
            var newDateTime = DateTime.ParseExact(splitTime, Format, CultureInfo.InvariantCulture);
            return newDateTime;
        }

    }
}
