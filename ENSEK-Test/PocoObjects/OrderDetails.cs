namespace ENSEK_Test.PocoObjects
{
    public  class OrderDetails
    {
        public decimal PurchasedQuantity { get; set; }
        public string UnitType { get; set; } 
        public decimal Cost { get; set; }
        public long UnitsRemaining { get; set; }
        public Guid OrderId { get; set; }
    }
}
