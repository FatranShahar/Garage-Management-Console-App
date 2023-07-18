using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public enum eColors
    {
        white = 1,
        black,
        yellow,
        red
    }
    public enum eDoorAmount
    {
        two = 2,
        three,
        four,
        five
    }
    public class Car : Vehicles
    {
        private eColors m_carColor;
        private eDoorAmount m_doorsNumber;
        internal static class CarWheels
        {
            internal const int k_numOfWheelsInCar = 5;
            internal const float k_maxWheelPressue = 33f;
        }
        internal static class fuelCarDetails
        {
            internal const Fuel.eFuelType k_fuelOfCar = Fuel.eFuelType.Octan95;
            internal const float  k_maxFuelCapacity = 46f ;
        }
        internal static class ElectricCarDetails
        {
            internal const float k_maxBatteryInHours =5.2f ;
        }
        public Car(string i_licenseNumber, Energy i_energyType, int i_numOfWheels, float i_maxAirPressure) :base(i_licenseNumber,i_energyType, i_numOfWheels, i_maxAirPressure)
        {
            AddExtraDetailsToVechileDict();
        }
        public eDoorAmount numOfDoors
        {
            get
            {
                return m_doorsNumber;
            }
            set
            {
                m_doorsNumber = value;
            }
        }
        public eColors carColor
        {
            get
            {
                return m_carColor;
            }
            set
            {
                m_carColor = value;
            }
        }
        public void AddExtraDetailsToVechileDict()
        {
            m_extraDetailsOfVehicle.Add("Car color", "insert car color (# option):\n1.White.\n2.black.\n3.yellow.\n4.red.");
            m_extraDetailsOfVehicle.Add("Doors amount", "insert number of door (2,3,4,5):");
        }
        public override void SetSingleData(string i_key,string i_value)
        {
            if(i_key == "Car color")
            {
                UpdateCarColor(i_value);
            }
            else if(i_key == "Doors amount")
            {
                UpdateCarDoors(i_value);
            }
            else if (i_key == "Wheel manufacturer")
            {
                UpdateCarWheelsManufacturer(i_value);
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
        public void UpdateCarWheelsManufacturer(string i_manufacture)
        {
            foreach (Wheel wheel in base.VehicleWheels)
            {
                wheel.manufactureName = i_manufacture;
            }
        }
        public void UpdateCarColor(string i_value)
        {
            eColors carColorSelect;
            bool isParseSucceed;
            int colorOption;
            if(!int.TryParse(i_value,out colorOption))
            {
                throw new FormatException("Can't convert input to color");
            }
            isParseSucceed = Enum.TryParse<eColors>(i_value.ToString(), out carColorSelect);
            if (!isParseSucceed)
            {
                throw new Exception("You picked unvailable number");
            }
            else
            {
                carColor = carColorSelect;
            }
        }
        public void UpdateCarDoors(string i_value)
        {
            eDoorAmount doorAmountSelect;
            bool isParseSucceed;
            isParseSucceed = Enum.TryParse<eDoorAmount>(i_value, out doorAmountSelect);
            if (!isParseSucceed)
            {
                throw new FormatException("Can't convert input to color");
            }
            else
            {
                numOfDoors = doorAmountSelect;
            }
        }
        public override StringBuilder PresentAllVehicleData()
        {
            StringBuilder carData = new StringBuilder();
            carData.AppendFormat($"Vehicle Type: {this.GetType().Name}.\n");
            carData.Append(base.PresentAllVehicleData());
            carData.AppendFormat($"Amount of doors: {numOfDoors}.\n");
            carData.AppendFormat($"Car color: {Enum.GetName(typeof(eColors), m_carColor)}.\n");
            return carData;
        }
    }
}