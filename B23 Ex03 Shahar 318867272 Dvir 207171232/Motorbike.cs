using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public enum eLicenseType
    {
        B1 = 1,
        AA,
        A2,
        A1
    }
    public class Motorbike : Vehicles
    {
        private eLicenseType m_licenseType;
        private int m_engineVol;

        internal static class MotorbikeWheels
        {
            internal const int k_numOfWheelsInMotorbike = 2;
            internal const float k_maxWheelPressue = 31f;
        }
        internal static class FuelMotorbikeDetails
        {
            internal const Fuel.eFuelType k_fuelOfMotorbike = Fuel.eFuelType.Octan98;
            internal const float k_maxFuelCapacity = 6.4f;
        }
        internal static class ElectricMotorbikeDetails
        {
            internal const float k_maxBatteryInHours = 2.6f;
        }
        public Motorbike( string i_licenseNumber, Energy i_energyType, int i_numOfWheels, float i_maxAirPressure) :base(i_licenseNumber, i_energyType, i_numOfWheels, i_maxAirPressure)
        {
            AddExtraDetailsToVechileDict();
        }
        public void AddExtraDetailsToVechileDict()
        {
            m_extraDetailsOfVehicle.Add("License type", "insert license type (# option):\n1.B1.\n2.AA.\n3.A2.\n4.A1.\n");
            m_extraDetailsOfVehicle.Add("Engine vol", "insert engine volume: ");
        }
        public override void SetSingleData(string i_key, string i_value)
        {
            if(i_key == "License type")
            {
                UpdateMotorbikeLicense(i_value);
            }
            else if(i_key == "Engine vol")
            {
                UpdateMotorbikeEngineVol(i_value);
            }
            else if (i_key == "Wheel manufacturer") 
            {
                UpdateMotorbikeWheelsManufacturer(i_value);
            }
            else if (i_key == "Wheel PSI")
            {
                UpdateVehicleCurrentPSI(i_value);
            }
            else
            {
                base.SetSingleData(i_key, i_value);
            }
        }
        public void UpdateMotorbikeWheelsManufacturer(string i_value)
        {
            foreach (Wheel wheel in base.VehicleWheels)
            {
                wheel.manufactureName = i_value;
            }
        }
        public void UpdateMotorbikeLicense(string i_value)
        {
            eLicenseType licenseOption;
            bool isParseSucceed;
            isParseSucceed = Enum.TryParse<eLicenseType>(i_value, out licenseOption);
            if(!isParseSucceed)
            {
                throw new FormatException("Can't convert input to motorbike license type");
            }
            else
            {
                m_licenseType = licenseOption;
            }
        }
        public void UpdateMotorbikeEngineVol(string i_value)
        {
            int engineVol;
            bool isParseSucceed;
            isParseSucceed = int.TryParse(i_value, out engineVol);
            if (!isParseSucceed)
            {
                throw new FormatException("Can't convert input to motorbike engine volume");
            }
            else
            {
                m_engineVol = engineVol;
            }
        }
        public override StringBuilder PresentAllVehicleData()
        {
            StringBuilder motorbikeData = new StringBuilder();
            motorbikeData.AppendFormat($"Vehicle Type: {this.GetType().Name}.\n");
            motorbikeData.Append(base.PresentAllVehicleData());
            motorbikeData.AppendFormat($"License type: {Enum.GetName(typeof(eLicenseType), m_licenseType)}.\n");
            motorbikeData.AppendFormat($"Engine volume: {m_engineVol}.\n");
            return motorbikeData;
        }
    }
}
