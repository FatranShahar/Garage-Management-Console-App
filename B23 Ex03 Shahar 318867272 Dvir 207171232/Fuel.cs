using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Fuel : Energy
    {
        public enum eFuelType
        {
            Soler = 1,
            Octan95,
            Octan98,
            Octan96,
        }
        private readonly eFuelType m_fuelType;
        public Fuel(float i_energyLeft,float i_maxCapacity,eFuelType i_fuelType) : base(i_energyLeft, i_maxCapacity)
        {
            m_fuelType = i_fuelType;
        }
        public eFuelType fuel
        {
            get
            {
                return m_fuelType;
            }
        }
        public void FillTank(float i_amountToFillInLiters,eFuelType i_fulType)
        {
            if(fuel != i_fulType)
            {
                throw new ArgumentException("The fuel type you asked for and the vehicle fuel type does not match");
            }
            else
            {
                base.LoadEnergy(i_amountToFillInLiters);         
            }
        }
        public override StringBuilder PresentAllEnergyData()
        {
            StringBuilder fuelData = new StringBuilder();
            
            fuelData.AppendFormat($"Fuel type:{Enum.GetName(typeof(eFuelType), m_fuelType)}.\n");
            fuelData.AppendFormat($"Current fuel amount: {energyLeft} liters.\n");
            fuelData.AppendFormat($"Fuel tank capacity: {maxCapacity} liters.\n");
            fuelData.AppendFormat($"Available fuel percentage: {energyPrecent}%.\n");
            return fuelData;
        }
    }
}
