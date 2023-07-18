using System;
using System.Collections.Generic;
using Ex03.GarageLogic;
using System.Text;

namespace Ex03.ConsoleUI
{
    public class GarageManager
    {
        Dictionary<string, GarageTicket> m_Tickets;
        VehicleCreator m_vehicleMaker;
        OutputMessages m_output;
        public enum eVehicleStatus
        {
            InRepair = 1,
            Repaired,
            PaidAndDone
        }
        public class GarageTicket
        {
            string m_carOwner;
            string m_ownerPhoneNumber;
            eVehicleStatus m_carStatus;
            Vehicles m_vechile;
            public GarageTicket(string i_name, string i_number, Vehicles i_vehicle)
            {
                m_carOwner = i_name;
                m_ownerPhoneNumber = i_number;
                m_carStatus = eVehicleStatus.InRepair;
                m_vechile = i_vehicle;
            }
            public string name
            {
                get
                {
                    return m_carOwner;
                }
                set
                {
                    m_carOwner = value;
                }
            }
            public string number
            {
                get
                {
                    return m_ownerPhoneNumber;
                }
                set
                {
                    m_ownerPhoneNumber = value;
                }
            }
            public eVehicleStatus status
            {
                get
                {
                    return m_carStatus;
                }
                set
                {
                    m_carStatus = value;
                }
            }
            public Vehicles vehicle
            {
                get
                {
                    return m_vechile;
                }
            }
        }
        public GarageManager()
        {
            m_Tickets = new Dictionary<string, GarageTicket>();
            m_output = new OutputMessages();
            m_vehicleMaker = new VehicleCreator();
        }
        public void AddTicket()
        {
            System.Console.WriteLine("enter license number:");
            string license = System.Console.ReadLine();
            if (!m_Tickets.ContainsKey(license))
            {
                System.Console.WriteLine("enter name:");
                string name = System.Console.ReadLine();
                System.Console.WriteLine("enter phone number:");
                string number = System.Console.ReadLine();
                m_output.VehicleChooseMessage();
                string vehicleType = System.Console.ReadLine();
                while (!m_output.IsValidChoise(vehicleType, 1, 5))
                {
                    System.Console.WriteLine("Invalid input, please re-enter your choice");
                    vehicleType = System.Console.ReadLine();
                }
                Vehicles vehicle = m_vehicleMaker.CreateNewVehicle(license, vehicleType);
                UpdateVehicleDictionaryDetails(vehicle);
                GarageTicket ticket = new GarageTicket(name, number, vehicle);
                m_Tickets.Add(license, ticket);
            }
            else
            {
                m_Tickets[license].status = eVehicleStatus.InRepair;
            }
        }
        public void PrintVehiclesByStatus()
        {
            System.Console.WriteLine("Please choose the status you would to filter by:");
            string status = m_output.PrintStatusMenuAndChooseStatuse();
            eVehicleStatus filter = eVehicleStatus.InRepair;
            switch (status)
            {
                case "1":
                    filter = eVehicleStatus.InRepair;
                    break;
                case "2":
                    filter = eVehicleStatus.Repaired;
                    break;
                case "3":
                    filter = eVehicleStatus.PaidAndDone;
                    break;
                default:
                    break;
            }
            foreach (KeyValuePair<string, GarageTicket> ticket in m_Tickets)
            {
                if (ticket.Value.status == filter)
                {
                    System.Console.WriteLine(ticket.Key);
                }
            }
        }
        public void ChangeCarStatus()
        {
            CheckIfGargeEmpty();
            string license = m_output.ReceiveLicenseFromUser();
            eVehicleStatus updateStatus;
            while (!m_Tickets.ContainsKey(license))
            {
                System.Console.WriteLine("Invalid input, please enter a license number");
                license = System.Console.ReadLine();
            }
            System.Console.WriteLine("Please eneter the vehicle status");
            string status = m_output.PrintStatusMenuAndChooseStatuse();
            while (!m_output.IsValidChoise(status, 1, 3))
            {
                System.Console.WriteLine("Invalid input, please re-enter your choice number");
                status = System.Console.ReadLine();
            }
            bool flag = Enum.TryParse<eVehicleStatus>(status, out updateStatus);
            m_Tickets[license].status = updateStatus;
            
        }
        public void UpdateVehicleDictionaryDetails(Vehicles i_vehicle)
        {
            List<string> keysToUpdate = new List<string>(i_vehicle.extraDetails.Keys);
            foreach (string key in keysToUpdate)
            {
                Console.WriteLine(i_vehicle.extraDetails[key]);
                string updateInfo = Console.ReadLine();
                i_vehicle.extraDetails[key] = updateInfo;
                i_vehicle.SetSingleData(key, updateInfo);
            }
        }
        public void PumpVehicleWheels()
        {
            CheckIfGargeEmpty();
            string license = m_output.ReceiveLicenseFromUser();
            while (!m_Tickets.ContainsKey(license))
            {
                System.Console.WriteLine("Invalid input, please enter a license number");
                license = System.Console.ReadLine();
            }
            foreach (Wheel vehicleWheel in m_Tickets[license].vehicle.VehicleWheels)
            {
                vehicleWheel.Pump(vehicleWheel.maxPSI - vehicleWheel.currentPSI);
            }
        }
        public void FuelVehicle()
        {
            CheckIfGargeEmpty();
            System.Console.WriteLine("You chose the fuel option");
            string license = m_output.ReceiveLicenseFromUser();
            while (!m_Tickets.ContainsKey(license))
            {
                System.Console.WriteLine("Invalid input, please enter a license number");
                license = System.Console.ReadLine();
            }
            Fuel fuelOfticket = m_Tickets[license].vehicle.VechileEnergy as Fuel;
            if (fuelOfticket != null)
            {
                m_output.PrintFuelMenu();
                string fuelType = System.Console.ReadLine();
                while (!m_output.IsValidChoise(fuelType, 1, 4))
                {
                    System.Console.WriteLine("Invalid input, please enter a fuel type");
                    fuelType = System.Console.ReadLine();
                }
                Fuel.eFuelType efuel;
                Enum.TryParse<Fuel.eFuelType>(fuelType, out efuel);
                if (fuelOfticket.fuel != efuel)
                {
                    throw new ArgumentException("there is no match between fuel type you chose and fuel type of the vehicle");
                }
            }
            else
            {
                throw new ArgumentException("can't insert fuel to electric vehicle");
            }
            System.Console.WriteLine("Please insert amount of Fuel you want to fill (in Liters): ");
            string amount = System.Console.ReadLine();
            float fuelAmount;
            while (!float.TryParse(amount, out fuelAmount))
            {
                System.Console.WriteLine("Invalid input, please enter a amount");
                amount = System.Console.ReadLine();
            }
            m_Tickets[license].vehicle.VechileEnergy.LoadEnergy(fuelAmount);
        }
        public void ChargeBattery()
        {
            CheckIfGargeEmpty();
            System.Console.WriteLine("You chose the charge battery option");
            string license = m_output.ReceiveLicenseFromUser();
            while (!m_Tickets.ContainsKey(license))
            {
                System.Console.WriteLine("Invalid input, please enter a license number");
                license = System.Console.ReadLine();
            }
            Electric isElectric = m_Tickets[license].vehicle.VechileEnergy as Electric;
            if (isElectric == null)
            {
                throw new FormatException("can't charge a non electric car");
            }
            System.Console.WriteLine("Please insert number of minutes you want to charge the battery");
            string minutesInput = System.Console.ReadLine();
            float chargeTime;
            while (!float.TryParse(minutesInput, out chargeTime))
            {
                System.Console.WriteLine("Invalid input, please enter a amount");
                minutesInput = System.Console.ReadLine();
            }
            m_Tickets[license].vehicle.VechileEnergy.LoadEnergy(chargeTime / 60);
        }
        public void Run()
        {
            GarageManager garage = new GarageManager();
            garage.m_output.WelcomeMessage();
            string operation = garage.m_output.PrintGarageOptions();
            while (operation != "0")
            {
                try
                {
                    ExecuteOperation(operation);
                }
                catch (ValueOutOfRangeException ex)
                {
                    System.Console.WriteLine(ex.GetExceptionMessage());
                }
                catch (ArgumentException ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
                operation = garage.m_output.PrintGarageOptions();
            }
            garage.m_output.GoodbyeMessage();
        }
        public void ExecuteOperation(string i_operation)
        {
            switch (i_operation)
            {
                case "1":
                    AddTicket();
                    break;
                case "2":
                    PrintVehiclesByStatus();
                    break;
                case "3":
                    ChangeCarStatus();
                    break;
                case "4":
                    PumpVehicleWheels();
                    break;
                case "5":
                    FuelVehicle();
                    break;
                case "6":
                    ChargeBattery();
                    break;
                case "7":
                    PrintVehicleInfo();
                    break;
                default:
                    throw new Exception("invalid input");
            }
        }
        public void PrintVehicleInfo()
        {
            StringBuilder vehicleInfo = new StringBuilder();
            if (m_Tickets.Count != 0)
            {
                string license = m_output.ReceiveLicenseFromUser();
                while (!m_Tickets.ContainsKey(license))
                {
                    System.Console.WriteLine("Invalid input, please enter a license number");
                    license = System.Console.ReadLine();
                }
                vehicleInfo.AppendFormat($"Owner name: {m_Tickets[license].name}.\n");
                vehicleInfo.AppendFormat($"Owner phone-number: {m_Tickets[license].number}.\n");
                vehicleInfo.AppendFormat($"Vehicle status: {m_Tickets[license].status}.\n");
                vehicleInfo.Append(m_Tickets[license].vehicle.PresentAllVehicleData());
                System.Console.WriteLine("Here is all the information:\n");

                System.Console.WriteLine(vehicleInfo);
            }
            else
            {
                throw new Exception("The Garage is empty");
            }
        }
        public void CheckIfGargeEmpty()
        {
            if(m_Tickets.Count == 0)
            {
                throw new Exception("The Garage is empty");
            }
        }
    }   
}
