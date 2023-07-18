using System;
using System.Text;

namespace Ex03.GarageLogic
{ 
    public abstract class Energy
    {
        private float m_EnergyLeft;
        private float m_EnergyPrecent;
        private readonly float m_MaxCapacity;
        public Energy(float i_eneryLeft,float i_maxCapacity)
        {
            m_EnergyLeft = i_eneryLeft;
            m_MaxCapacity = i_maxCapacity;
            UpdateEnergyPrecent();
        }
        public void LoadEnergy(float i_amountToFill)
        {
            if (i_amountToFill < 0)
            {
                throw new Exception("Invalid negative input");
            }
            float newEnergyAmountInVehicle = m_EnergyLeft + (i_amountToFill);
            if (newEnergyAmountInVehicle > m_MaxCapacity)
            {
                throw new ValueOutOfRangeException(0, m_MaxCapacity, newEnergyAmountInVehicle, "Energy");
            }
            else
            {
                energyLeft = newEnergyAmountInVehicle;
                UpdateEnergyPrecent();
            }
        }
        public void UpdateEnergyPrecent()
        {
             float EnergyPrecent = (m_EnergyLeft / m_MaxCapacity) * 100;
             m_EnergyPrecent = (float)Math.Round(EnergyPrecent, 1);
        }
        public float energyLeft
        {
            get
            {
                return m_EnergyLeft;
            }
            set
            {
                m_EnergyLeft = value;
            }
        }
        public float energyPrecent
        {
            get
            {
                return m_EnergyPrecent;
            }
            set
            {
                m_EnergyPrecent = value;
            }
        }
        public float maxCapacity
        {
            get
            {
                return m_MaxCapacity;
            }
        }
        public abstract StringBuilder PresentAllEnergyData();
    }
}
