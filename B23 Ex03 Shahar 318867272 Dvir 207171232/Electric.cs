using System.Text;

namespace Ex03.GarageLogic
{
    public class Electric : Energy
    {
        public Electric(float i_eneryLeft, float i_maxCapacity) : base(i_eneryLeft, i_maxCapacity) { }
        public void charge(float i_timeToChargeInMinutes)
        {
            base.LoadEnergy(i_timeToChargeInMinutes / 60f);
        }
        public override StringBuilder PresentAllEnergyData()
        {
            StringBuilder electricData = new StringBuilder();
            electricData.AppendFormat($"Current power amount: {energyLeft} hours.\n");
            electricData.AppendFormat($"Electric battery capacity: {maxCapacity} hours.\n");
            electricData.AppendFormat($"Available battery percentage: {energyPrecent}%.\n");
            return electricData;
        }
    }
}
