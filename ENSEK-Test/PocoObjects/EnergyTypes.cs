namespace ENSEK_Test.PocoObjects
{
    public class EnergyTypes
    {
        public EnergyData Electric { get; set; }

        public EnergyData Gas { get; set; }

        public EnergyData Nuclear { get; set; }

        public EnergyData Oil { get; set; }

        public EnergyData? FindEnergySourceById(int targetId)
        {
            return new[] { this.Electric, this.Gas, this.Nuclear, this.Oil }
                         .FirstOrDefault(x => x.EnergyId == targetId);
        }
    }
}
