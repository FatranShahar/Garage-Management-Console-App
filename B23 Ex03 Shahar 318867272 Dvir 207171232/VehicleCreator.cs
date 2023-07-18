using System;
namespace Ex03.GarageLogic
{
    public class VehicleCreator
    {
        public enum eVehicleTypes
        {
            fuelCar = 1,
            electricCar,
            fuelMotorbike,
            electricMotorbike,
            fuelTruck
        }
        public Vehicles CreateNewVehicle(string i_licenseID, string i_vehicleType)
        {
            Vehicles newVehicle;
            eVehicleTypes vehicleType;
            vehicleType = GetAppropriateVehicleType(i_vehicleType);
            Energy AppropriateEnergy = GetAppropriateVehicleEnergy(vehicleType);
            if (vehicleType == eVehicleTypes.electricCar || vehicleType == eVehicleTypes.fuelCar)
            {
                newVehicle = new Car(i_licenseID, AppropriateEnergy, Car.CarWheels.k_numOfWheelsInCar, Car.CarWheels.k_maxWheelPressue);
            }
            else if (vehicleType == eVehicleTypes.electricMotorbike || vehicleType == eVehicleTypes.fuelMotorbike)
            {
                newVehicle = new Motorbike(i_licenseID, AppropriateEnergy, Motorbike.MotorbikeWheels.k_numOfWheelsInMotorbike, Motorbike.MotorbikeWheels.k_maxWheelPressue);
            }
            else if(vehicleType == eVehicleTypes.fuelTruck)
            {
                newVehicle = new Truck(i_licenseID, AppropriateEnergy, Truck.TruckWheels.k_numOfWheelsInTruck, Truck.TruckWheels.k_maxWheelPressue);
            }
            else
            {
                newVehicle = null;
                throw new Exception("unavaildable vehicle type");
            }
            return newVehicle;
        }
        public eVehicleTypes GetAppropriateVehicleType(string i_value)
        {
            int type = int.Parse(i_value);
            eVehicleTypes vehicleType = (eVehicleTypes)type;
            return vehicleType;
        }
        public Energy GetAppropriateVehicleEnergy(eVehicleTypes i_vehicle)
        {
            Energy vehicleEnergy;
            if(i_vehicle == eVehicleTypes.electricCar)
            {
                vehicleEnergy = new Electric(0f, Car.ElectricCarDetails.k_maxBatteryInHours);
            }
            else if(i_vehicle == eVehicleTypes.electricMotorbike)
            {
                vehicleEnergy = new Electric(0f, Motorbike.ElectricMotorbikeDetails.k_maxBatteryInHours);
            }
            else if(i_vehicle == eVehicleTypes.fuelCar)
            {
                vehicleEnergy = new Fuel(0f, Car.fuelCarDetails.k_maxFuelCapacity,Car.fuelCarDetails.k_fuelOfCar);
            }
            else if(i_vehicle == eVehicleTypes.fuelMotorbike)
            {
                vehicleEnergy = new Fuel(0f, Motorbike.FuelMotorbikeDetails.k_maxFuelCapacity, Motorbike.FuelMotorbikeDetails.k_fuelOfMotorbike);
            }
            else if(i_vehicle == eVehicleTypes.fuelTruck)
            {
                vehicleEnergy = new Fuel(0f, Truck.fuelTruckDetails.k_maxFuelCapacity, Truck.fuelTruckDetails.k_fuelOfTruck);
            }
            else
            {
                vehicleEnergy = null;
                throw new Exception("unavailable energy type");
            }
            return vehicleEnergy;
        }
        public static void Main() { }
    }
}
