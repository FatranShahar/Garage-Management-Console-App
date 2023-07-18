using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Ex03.ConsoleUI
{
    public class OutputMessages
    {
        public void WelcomeMessage()
        {
            StringBuilder welcomeMessage = new StringBuilder();
            welcomeMessage.Append("Welcome to my Garage App!");
            Console.WriteLine(welcomeMessage);
        }
        public void GoodbyeMessage()
        {
            StringBuilder goodbyeMessage = new StringBuilder();
            goodbyeMessage.Append("Thank you for coming, good bye:)");
            Console.WriteLine(goodbyeMessage);
            System.Threading.Thread.Sleep(1250);
            
        }
        public void PrintGarageFunctionsOptionsMenu()
        {
            StringBuilder functionMessage = new StringBuilder();
            functionMessage.AppendLine("Please enter the garage function you need:");
            functionMessage.AppendLine("1. Enter new vehicle to the garage/change car status");
            functionMessage.AppendLine("2. Print list of vehicles in the garage (can be filtered by status)");
            functionMessage.AppendLine("3. Change vehicle status");
            functionMessage.AppendLine("4. Inflate wheels");
            functionMessage.AppendLine("5. Refuel a non electrical car");
            functionMessage.AppendLine("6. Charge an electric vehicle");
            functionMessage.Append("7. Print vehicle details");
            Console.WriteLine(functionMessage);
        }
        public string GetFunctionChoice()
        {
            PrintGarageFunctionsOptionsMenu();
            string choice = System.Console.ReadLine();
            while (!IsValidChoise(choice, 1, 7))
            {
                System.Console.WriteLine("Invalid input, please re-enter your choice");
                choice = System.Console.ReadLine();
            }
            return choice;
        }
        public void VehicleChooseMessage()
        {
            StringBuilder menuMessage = new StringBuilder();
            menuMessage.AppendLine("What type of vehicle did you bring for us today?");
            menuMessage.AppendLine("Enter the number of your vehicle:");
            menuMessage.AppendLine("1. Fuel Car");
            menuMessage.AppendLine("2. Electric Car");
            menuMessage.AppendLine("3. Fuel Motorcycle");
            menuMessage.AppendLine("4. Electric Motorcycle");
            menuMessage.Append("5. Fuel Truck");
            Console.WriteLine(menuMessage);
        }
        public string GetVehicleType()
        {
            string userVehicleType;
            VehicleChooseMessage();
            userVehicleType = System.Console.ReadLine();
            while (!IsValidChoise(userVehicleType, 1, 5))
            {
                System.Console.WriteLine("Invalid input, please re-enter your choice");
                userVehicleType = System.Console.ReadLine();
            }
            return userVehicleType;
        }
        public bool IsValidChoise(string i_choice, int i_rangeStart, int i_rangeEnd)
        {
            bool valid = false;
            int num;
            if (int.TryParse(i_choice, out num))
            {
                if (num >= i_rangeStart && num <= i_rangeEnd)
                {
                    valid = true;
                }
            }
            return valid;
        }
        public string ReceiveLicenseFromUser()
        {
            System.Console.WriteLine("Please enter a license number");
            return System.Console.ReadLine();
        }
        public string FeulOption()
        {
            System.Console.WriteLine("You chose the fuel option");
            string licenseNumber = ReceiveLicenseFromUser();
            return licenseNumber;
        }
        public void PrintFuelMenu()
        {
            StringBuilder fuelMenuMessage = new StringBuilder();
            fuelMenuMessage.AppendLine("1. Soler");
            fuelMenuMessage.AppendLine("2. Octan95");
            fuelMenuMessage.AppendLine("3. Octan98");
            fuelMenuMessage.Append("4. Octan96");
            Console.WriteLine(fuelMenuMessage);
        }
        public string PrintStatusMenuAndChooseStatuse()
        {
            StringBuilder fuelMenuMessage = new StringBuilder();
            fuelMenuMessage.AppendLine("1. In repair");
            fuelMenuMessage.AppendLine("2. Repaired but yet paid");
            fuelMenuMessage.AppendLine("3. Repaired and paid");
            Console.WriteLine(fuelMenuMessage);
            string userStatusChoice = System.Console.ReadLine();
            while (!IsValidChoise(userStatusChoice, 1, 3))
            {
                System.Console.WriteLine("Invalid input, please re-enter your choice");
                userStatusChoice = System.Console.ReadLine();
            }
            return userStatusChoice;
        }
        public string PrintGarageOptions()
        {
            StringBuilder actionInTheGarageMessage = new StringBuilder();
            actionInTheGarageMessage.AppendLine("Choose which garage action you need: ");
            actionInTheGarageMessage.AppendLine("1. Add new vehicle to the garage");
            actionInTheGarageMessage.AppendLine("2. Print vehicles in the garage by status");
            actionInTheGarageMessage.AppendLine("3. Change vehicle status");
            actionInTheGarageMessage.AppendLine("4. Pump wheels");
            actionInTheGarageMessage.AppendLine("5. Fuel vehicle that run on gas");
            actionInTheGarageMessage.AppendLine("6. Charge a battery of an electric vehicle");
            actionInTheGarageMessage.AppendLine("7. Print data by license number");
            actionInTheGarageMessage.Append("0. Leave garage");
            Console.WriteLine(actionInTheGarageMessage);
            string operation = System.Console.ReadLine();
            while (!IsValidChoise(operation, 0, 7))
            {
                System.Console.WriteLine("Invalid input, please re-enter your choice");
                operation = System.Console.ReadLine();
            }
            return operation;
        }
    }
}