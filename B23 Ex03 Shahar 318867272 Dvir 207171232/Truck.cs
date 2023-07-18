using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class  Truck : Vehicles
    {
        internal static class TruckWheels
        {
            internal const int k_numOfWheelsInTruck = 14;
            internal const float k_maxWheelPressue = 26f;
        }
        internal static class fuelTruckDetails
        {
            internal const Fuel.eFuelType k_fuelOfTruck = Fuel.eFuelType.Soler;
            internal const float k_maxFuelCapacity = 135f;
        }
        public enum eHazardMaterialsTransportaion
        {
            Yes = 1,
            No
        }
        private eHazardMaterialsTransportaion m_transportHazardMaterials;
        private float m_cargoVol;
        public Truck( string i_licenseNumber, Energy i_energyType, int i_numOfWheels, float i_maxAirPressure) :base(i_licenseNumber, i_energyType, i_numOfWheels, i_maxAirPressure)
        {
            AddExtraDetailsToVechileDict();
        }
        public void AddExtraDetailsToVechileDict()
        {
            m_extraDetailsOfVehicle.Add("Hazard materials", "insert if the truck tranis hazard materials(option # number):\n1.Yes\n2.No");
            m_extraDetailsOfVehicle.Add("Cargo vol", "insert cargo volume");
        }
        public override void SetSingleData(string i_key, string i_value)
        {
            if (i_key == "Hazard materials")
            {
                UpdateTruckHazardMaterials(i_value);
            }
            else if (i_key == "Cargo vol")
            {
                UpdateTruckCargoVol(i_value);
            }
            else if (i_key == "Wheel manufacturer") 
            {
                UpdateTruckWheelsManufacturer(i_value);
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
        public void UpdateTruckWheelsManufacturer(string i_value)
        {
            foreach (Wheel wheel in base.VehicleWheels)
            {
                wheel.manufactureName = i_value;
            }
        }
        public void UpdateTruckHazardMaterials(string i_value)
        {
            eHazardMaterialsTransportaion hazardMatsOption;
            bool isParseSucceed;
            int option;
            if(!int.TryParse(i_value,out option))
            {
                throw new FormatException("Can't convert input to int");
            }
            isParseSucceed = Enum.TryParse<eHazardMaterialsTransportaion>(i_value, out hazardMatsOption);
            if (!isParseSucceed)
            {
                throw new ArgumentException("invalid choice");
            }
            else
            {
                m_transportHazardMaterials = hazardMatsOption;
            }
        }
        public void UpdateTruckCargoVol(string i_value)
        {
            float cargoVolOption;
            bool isParseSucceed;
            isParseSucceed = float.TryParse(i_value, out cargoVolOption);
            if (!isParseSucceed)
            {
                throw new FormatException("Can't convert input to truck cargo volume");
            }
            else
            {
                m_cargoVol = cargoVolOption;
            }
        }
        public override StringBuilder PresentAllVehicleData()
        {
            StringBuilder truckData = new StringBuilder();
            truckData.AppendFormat($"Vehicle Type: {this.GetType().Name}.\n");
            truckData.Append(base.PresentAllVehicleData());
            truckData.AppendFormat($"Cargo volume: {m_cargoVol}.\n");
            truckData.AppendFormat($"Truck transports hazard materials: {Enum.GetName(typeof(eHazardMaterialsTransportaion), m_transportHazardMaterials)}.\n");
            return truckData;
        }
    }
}
