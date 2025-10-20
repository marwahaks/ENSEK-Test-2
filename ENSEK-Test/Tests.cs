namespace ENSEK_Test
{
    [TestFixture]
    public class Tests
    {
        private readonly GetTests GetTests;
        private readonly PostTests PostTests;
        private readonly PutTests PutTests;
        private readonly DeleteTests DeleteTests;

        public Tests()
        {
            string _baseUrl = "https://qacandidatetest.ensek.io";
            GetTests = new GetTests(_baseUrl);
            PostTests = new PostTests(_baseUrl);
            PutTests = new PutTests(_baseUrl);
            DeleteTests = new DeleteTests(_baseUrl); 
        }

        [Test]
        [Category("GET")]
        public void GetOrderData()
        {
            GetTests.GetOrders();
        }    
                
        [Test]
        [Category("GET")]
        public void GetPastOrderData()
        {
            GetTests.GetOrdersPriorCurrDate();
        }    

        [Test]
        [Category("GET")]
        public void GetSingleOrdersData()
        {
            GetTests.GetSingleOrder();
        }        
        
        [Test]
        [Category("GET")]
        public void GetEnergyData()
        {
            GetTests.GetEnergy();
        }        
        
        [Test]
        [Category("POST")]
        public void ResetTestData_Unauthorised()
        {
            PostTests.PostResetData_Unauthorised();
        }
                     
        [Test]
        [Category("POST")]
        public void ResetTestData()
        {
            var bearer = PostTests.Login();
            PostTests.PostResetData_Authorised(bearer);
        }
                
        [Test]
        [Category("POST")]
        public void Login()
        {
            PostTests.Login();
        }  
        
        [Test]
        [Category("PUT")]
        public void PurchaseGasEnergyVerification()
        {
            PutTests.PurchaseQuantity(1,10);
        }             
        
        [Test]
        [Category("PUT")]
        public void PurchaseElectricEnergyVerification()
        {
            PutTests.PurchaseQuantity(3,10);
        }      
                
        [Test]
        [Category("PUT")]
        public void PurchaseOilEnergyVerification()
        {
            PutTests.PurchaseQuantity(4,10);
        }      
                
        [Test]
        [Category("PUT")]
        public void PurchaseNuclearEnergyVerification()
        {
            PutTests.PurchaseQuantity(2,10);
        }      
        
        [Test]
        [Category("PUT")]
        public void UpdateOrder()
        {
            PutTests.UpdateOrder();
        }     
        
        [Test]
        [Category("DELETE")]
        public void DeleteOrder()
        {
            DeleteTests.DeleteOrder();
        }
    }
}