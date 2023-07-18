using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicles
    {
        private string m_modelName;
        private readonly string r_licenseNumber;
        private Energy m_vechileEnergy;
        List<Wheel> m_wheels;
        protected Dictionary<string, string> m_extraDetailsOfVehicle;
        public Vehicles(string i_licenseNumber, Energy i_energyType, int i_numberOfWheels, float i_maxAirPressure)
        {
            m_wheels = new List<Wheel>(i_numberOfWheels);
            for (int i = 0; i < i_numberOfWheels; i++)
            {
                m_wheels.Add(new Wheel(i_maxAirPressure));
            }
            r_licenseNumber = i_licenseNumber;
            m_vechileEnergy = i_energyType;
            initDict();
        }
        public string ModelName
        {
            get
            {
                return m_modelName;
            }
            set
            {
                m_modelName = value;
            }
        }
        public string LicenseID
        {
            get
            {
                return r_licenseNumber;
            }
        }
        public Energy VechileEnergy
        {
            get
            {
                return m_vechileEnergy;
            }
        }
        public List<Wheel> VehicleWheels
        {
            get
            {
                return m_wheels;
            }
            set
            {
                m_wheels = value;
            }
        }
        public void initDict()
        {
            m_extraDetailsOfVehicle = new Dictionary<string, string>();
            m_extraDetailsOfVehicle.Add("Model Name", "insert vechile model name");
            m_extraDetailsOfVehicle.Add("Wheel manufacturer", "insert wheels manufacturer name");
            m_extraDetailsOfVehicle.Add("Wheel PSI", "insert wheels air pressure");
            m_extraDetailsOfVehicle.Add("Energy left", "insert energy left in vehcile(fuel or electric) in liters or minutes");
        }
        public Dictionary<string, string> extraDetails
        {
            get
            {
                return m_extraDetailsOfVehicle;
            }
            set
            {
                m_extraDetailsOfVehicle = value;
            }
        }
        public virtual void SetSingleData(string i_key, string i_value)
        {
            if (m_extraDetailsOfVehicle.ContainsKey(i_key) == false)
            {
                throw new Exception();
            }
            else if (i_key == "Energy left")
            {
                UpdateRemainEnergy(i_value);
            }
            else
            {
                ModelName = i_value;
            }
        }
        public void UpdateVehcileWheels(string i_manufactureName, float i_wheelMaxPressure, int i_amoutOfWheels, float i_currentPressure)
        {
            m_wheels = new List<Wheel>(i_amoutOfWheels);
            for (int i = 0; i < m_wheels.Count; i++)
            {
                m_wheels.Add(new Wheel(i_manufactureName, i_currentPressure, i_wheelMaxPressure));
            }
        }
        public float convertWheelPSI(string i_currentPSI)
        {
            float currentAirPressure;
            if (!float.TryParse(i_currentPSI, out currentAirPressure))
            {
                throw new FormatException("can't convert input to Air Pressure");
            }
            return currentAirPressure;
        }
        public void UpdateRemainEnergy(string i_remainEnergy)
        {
            float remainEnergy;
            if (float.TryParse(i_remainEnergy, out remainEnergy))
            {
                if (m_vechileEnergy is Electric)
                {
                    remainEnergy /= 60;
                }
                ValueOutOfRangeException ex = new ValueOutOfRangeException(0, m_vechileEnergy.maxCapacity, remainEnergy, "energy");
                if(ex.IsValidValue())
                {
                    m_vechileEnergy.energyLeft = remainEnergy;
                    m_vechileEnergy.UpdateEnergyPrecent();
                }
                else
                {
                    throw ex;
                }
            }
            else
            {
                throw new FormatException("can't convert input to Energy (fuel or electricity");
            }
        }
        public void UpdateVehicleCurrentPSI(string PSI)
        {
            float currentAirPressure;
            if (float.TryParse(PSI, out currentAirPressure))
            {
                ValueOutOfRangeException ex = new ValueOutOfRangeException(0, this.VehicleWheels[0].maxPSI, currentAirPressure, "Air Pressure");
                if (ex.IsValidValue())
                {
                    foreach (Wheel wheel in VehicleWheels)
                    {
                        wheel.currentPSI = currentAirPressure;
                    }
                }
                else
                {
                    throw ex;
                }
            }
            else
            {
                throw new FormatException("can't convert input to Air Pressure");
            }
        }
        public virtual StringBuilder PresentAllVehicleData()
        {
            StringBuilder vehicleData = new StringBuilder();
            vehicleData.AppendFormat($"Licesne number: {LicenseID}.\n");
            vehicleData.AppendFormat($"Model name: {ModelName}.\n");
            vehicleData.AppendFormat($"Wheels manufacturer name: {m_wheels[0].manufactureName}.\n");
            vehicleData.AppendFormat($"Amount of wheels: {m_wheels.Count}.\n");
            vehicleData.AppendFormat($"Wheels current PSI: {m_wheels[0].currentPSI}.\n");
            vehicleData.AppendFormat($"Wheels Max PSI: {m_wheels[0].maxPSI}.\n");
            vehicleData.AppendFormat($"Energy Type: {m_vechileEnergy.GetType().Name}.\n");
            vehicleData.Append(m_vechileEnergy.PresentAllEnergyData());
            return vehicleData;
        }

        public void Method()
        {
            throw new System.NotImplementedException();
        }
    }
}