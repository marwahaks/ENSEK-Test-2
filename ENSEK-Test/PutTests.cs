using ENSEK_Test.PocoObjects;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text.RegularExpressions;

namespace ENSEK_Test
{
    public class PutTests(string baseUrl)
    {
        private string BaseUrl { get; } = baseUrl;
       
        //DEFECT: Not sure what the body id refers to, passing in order id or energy id does not work
        public void UpdateOrder()
        {
            string endpoint = $"/ENSEK/orders/2cdd6f69-95df-437e-b4d3-e772472db8de";
            var client = new RestClient(BaseUrl);

            var body = new
            {
                id = "2cdd6f69-95df-437e-b4d3-e772472db8de",
                quantity = 500,
                energy_id = 1
            };
            var request = new RestRequest(endpoint).AddJsonBody(body);
            var response = client.Put(request);

        }

        //DEFECT: in the message purchase amount and remaining quantity are flipped
        public void PurchaseQuantity(int energyId, int quantity)
        {
            string endpoint = $"/ENSEK/buy/{energyId}/{quantity}";
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(endpoint);
            var response = client.Put(request);

            var json = JObject.Parse(response.Content);
            var message = json.SelectToken("message")?.ToString();
            var energyDetails = GetEnergyDetails(energyId);
            if (message.ToLower().Contains("there is no"))
            {
                throw new Exception("There is no fuel to purchase, update quantity and re run");
            }
            var orderDetails = ExtractValues(message);
            VerifyPurchaseOrder(orderDetails, energyDetails, energyId);

        }

        private EnergyData? GetEnergyDetails(int energyId)
        {
            var getTests = new GetTests(BaseUrl);
            var energy = getTests.GetEnergy();

            var energyDetails = energy.FindEnergySourceById(energyId);

            return energyDetails;
        }

        //DEFECT: Cannot get a newly created order
        private void VerifyPurchaseOrder(OrderDetails od, EnergyData energyDetails, int energyId)
        {
            var CostCalc = energyDetails.PricePerUnit * od.PurchasedQuantity;

            Assert.That(od.UnitType, Is.EqualTo(energyDetails.UnitType), "The unit type are different for the energy purchased");
            Assert.That(od.Cost, Is.EqualTo(CostCalc), "The total costs are incorrect for the energy purchased");
            Assert.That(od.UnitsRemaining, Is.EqualTo(energyDetails.QuantityOfUnits), "The units remaining are incorrect for the energy purchased");
        }

        private OrderDetails ExtractValues(string message)
        {
            const string pattern = @"purchased\s+(?<Quantity>[\d\.\-]+)\s+(?<Unit>\S+)\s+at\s+a\s+cost\s+of\s+(?<Cost>[\d\.]+)\s+there\s+are\s+(?<RemainingUnits>[\d\-]+)\s+units\s+remaining\. Your\s+order\s*id\s+is\s+(?<OrderId>[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12})\.";
            Match match = Regex.Match(message, pattern);

            var details = new OrderDetails
            {
                PurchasedQuantity = decimal.Parse(match.Groups["Quantity"].Value),
                UnitType = match.Groups["Unit"].Value,
                Cost = decimal.Parse(match.Groups["Cost"].Value),
                UnitsRemaining = long.Parse(match.Groups["RemainingUnits"].Value),
                OrderId = Guid.Parse(match.Groups["OrderId"].Value)
            };

            return details;
        }
    }
}
