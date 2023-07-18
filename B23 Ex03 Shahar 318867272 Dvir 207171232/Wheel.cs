using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_manufacturerName;
        private float m_currentAirPressure;
        private readonly float r_maxAirPressure;
        public Wheel(string i_manufacturer, float i_CurrentAirPressure, float i_maxAirPressure)
        {
            m_manufacturerName = i_manufacturer;
            r_maxAirPressure = i_maxAirPressure;
            m_currentAirPressure = i_CurrentAirPressure;
        }
        public Wheel(float i_maxAirPressure)
        {
            r_maxAirPressure = i_maxAirPressure;
        }
        public string manufactureName
        {
            get
            {
                return m_manufacturerName;
            }
            set
            {
                m_manufacturerName = value; 
            }
        }
        public float currentPSI
        {
            get
            {
                return m_currentAirPressure;
            }
            set
            {
                if(value <= r_maxAirPressure)
                {
                    m_currentAirPressure = value;
                }
            }
        }
        public float maxPSI
        {
            get
            {
                return r_maxAirPressure;
            }
        }
        public void Pump(float i_amountOfPSItoADD)
        {
            float newWheelPSI = currentPSI + i_amountOfPSItoADD;
            if(newWheelPSI > maxPSI)
            {
                throw new ValueOutOfRangeException(0, maxPSI, newWheelPSI, "Air Pressure");
            }
            currentPSI = newWheelPSI;
        }
    }
}