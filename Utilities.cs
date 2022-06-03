using System;
using System.Collections.Generic;
using System.Text;

namespace HammerBitcoin
{
    class Utilities
    {
        static readonly Random rand = new Random(Guid.NewGuid().GetHashCode());

        public static int year = 1;
        public static int employees = 100;
        public static int cash = 2800;
        public static int computers = 1000;
        public static int computerPrice;

        public static int starved = 0;
        public static int marketCrashVictims = 0;
        public static int newEmployees = 5;
        public static int cashMined = 3000;
        public static int bitcoinGeneratedPerComputer = 3;
        public static int amountStolenByHackers = 200;

        public static int cashPaidToEmployees;
        public static int computersMaintained;

        public const string OGH = "O Great Gill Bates";
        /**
       * Allows the player to buy computers.
       * 
       * If a valid amount is entered, the available cash is reduced accordingly.
       */
        public static void BuyComputers()
        {
            int computersToBuy;
            string question = "How many computers will you buy? ";

            computersToBuy = GetNumber(question);
            int cost = computerPrice * computersToBuy;
            while (cost > cash)
            {
                Jest($"We have but {cash} bitcoins of cash, not {cost}!");
                computersToBuy = GetNumber(question);
                cost = computerPrice * computersToBuy;
            }
            cash = cash - cost;
            computers = computers + computersToBuy;
            Console.WriteLine($"{OGH}, you now have {computers} computers");
            Console.WriteLine($"and {cash} bitcoins of cash.");
        }

        /**
        * Tells player that the request cannot be fulfilled.
        *
        * @param  message The reason the request cannot be fulfilled.
        */
        public static void Jest(string message)
        {
            Console.WriteLine($"{OGH}, you are dreaming!");
            Console.WriteLine(message);
        }

        /**
        * Allows the player to sell computers.
        * 
        * Available cash will be increased by the value of the computers sold.
        */
        public static void SellComputers()
        {
            string question = "How many computers will you sell? ";
            int computersToSell = GetNumber(question);

            while (computersToSell > computers)
            {
                Jest($"The corporation has only {computers} computers!");
                computersToSell = GetNumber(question);
            }
            cash = cash + computerPrice * computersToSell;
            computers = computers - computersToSell;
            Console.WriteLine($"{OGH}, you now have {computers} computers");
            Console.WriteLine($"and {cash} bitcoins of cash.");
        }

        /**
        * Allows the player to decide how much cash to use to feed people.
        * 
        * If a valid amount is entered, the available cash is reduced accordingly.
        */
        public static void PayEmployees()
        {
            string question = "How much bitcoin will you distribute to the employees? ";
            cashPaidToEmployees = GetNumber(question);

            while (cashPaidToEmployees > cash)
            {
                Jest($"We have but {cash} bitcoins!");
                cashPaidToEmployees = GetNumber(question);
            }
            cash = cash - cashPaidToEmployees;
            Console.WriteLine($"{OGH}, {cash} bitcoins remain.");
        }

        /**
        * Allows the user to choose how much to spend on maintenance.
        */
        public static void MaintainComputers()
        {
            string question = "How many bitcoins will you allocate for maintenance? ";
            int maintenanceAmount = 0;
            bool haveGoodAnswer = false;

            while (!haveGoodAnswer)
            {
                maintenanceAmount = GetNumber(question);
                if (maintenanceAmount > cash)
                {
                    Jest($"We have but {cash} bitcoins left!");
                }
                else if (maintenanceAmount > 2 * computers)
                {
                    Jest($"We have but {computers} computers available for mining!");
                }
                else if (maintenanceAmount > 20 * employees)
                {
                    Jest($"We have but {employees} people to maintain the computers!");
                }
                else
                {
                    haveGoodAnswer = true;
                }
            }
            computersMaintained = maintenanceAmount / 2;
            // Be nice to the player!  If they enter an odd number, give them the extra bitcoin back.
            cash = cash - computersMaintained * 2;  // can re-write as cash -= computersMaintained * 2;
            Console.WriteLine($"{OGH}, we now have {cash} bitcoins in storage.");
        }

        /**
        * Checks for crash, and counts the victims.
        *
        * @return The number of victims of the crash.
        */
        public static int CheckForCrash()
        {
            int victims;
            if (rand.NextDouble() < 0.15)
            {
                Console.WriteLine("*** A terrible market crash wipes out half of the corporation's employees! ***");
                victims = employees / 2;
            }
            else
            {
                victims = 0;
            }
            return victims;
        }

        /**
        * Counts how many people starved, and removes them from the employees.
        * 
        * @return  The percent of employees who starved.
        */
        public static int CountStarvedEmployees()
        {  // TODO: Has side effects
            int employeesPaid = cashPaidToEmployees / 20;
            int percentStarved = 0;
            if (employeesPaid >= employees)
            {
                starved = 0;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("The corporation's employees are well fed and happy.");
            }
            else
            {
                starved = employees - employeesPaid;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"{starved} employees starved to death.");
                percentStarved = (100 * starved) / employees;
                employees = employees - starved;
            }
            Console.ResetColor();
            return percentStarved;
        }

        /**
        * Counts how many new employees joined the company.
        *
        * @return The number of new hires.
        */
        public static int CountNewHires()
        {
            int newEmployees;
            if (starved > 0)
            {
                newEmployees = 0;
            }
            else
            {
                newEmployees = (20 * computers + cash) / (100 * employees) + 1;
            }
            return newEmployees;
        }

        /**
        * Determines the harvest, and collects the new cash.
        * 
        * Computers mine a random number of bitcoin each year, from 1 to 5.
        * 
        * @return  The amount of bitcoin mined.
        */
        public static int MineBitcoin(int computers)
        {
            bitcoinGeneratedPerComputer = rand.Next(1, 6);
            cashMined = bitcoinGeneratedPerComputer * computers;
            return cashMined;
        }

        /**
        * Checks if hackers get into the system, and determines how much they stole.
        */
        public static void CheckForHackers()
        {
            if (rand.Next(100) < 40)
            {
                int percentHacked = 10 + rand.Next(21);
                Console.WriteLine($"*** Hackers steal  {percentHacked} percent of your bitcoins! ***");
                amountStolenByHackers = (percentHacked * cash) / 100;
                cash = cash - amountStolenByHackers;
            }
            else
            {
                amountStolenByHackers = 0;
            }
        }

        /**
        * Randomly sets the new price of computers.
        *
        * @return The new price of a computer.
        * 
        * The price fluctuates from 17 to 26 bitcoin per computer.
        */
        public static int UpdateComputerPrice()
        {
            return 17 + rand.Next(10);
        }

        /**
        * Prints an evaluation at the end of a game.
        */
        public static void PrintFinalScore()
        {
            Console.Clear();
            if (starved >= (45 * employees) / 100)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"O Once-Great Hammurabi,");
                Console.WriteLine($"{starved} of your team starved during the last year of your incompetent reign!");
                Console.WriteLine("The few who remain hacked your bank account and changed your password, effectively evicting you!");
                Console.WriteLine();
                Console.WriteLine("Your final rating: TERRIBLE.");
                Console.ResetColor();
                return;
            }

            int computerScore = computers;
            if (20 * employees < computerScore)
            {
                computerScore = 20 * employees;
            }

            if (computerScore < 600)
            {
                Console.WriteLine($"Congratulations, {OGH}");
                Console.WriteLine("You have ruled wisely but not well.");
                Console.WriteLine("You have led your people through ten difficult years,");
                Console.WriteLine($"but your corporations assets have shrunk to a mere {computers} computers.");
                Console.WriteLine();
                Console.WriteLine("Your final rating: ADEQUATE.");
            }
            else if (computerScore < 800)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Congratulations, {OGH}.");
                Console.WriteLine("You  have ruled wisely, and shown the online world that its possible to make money in cryptocurrency.");
                Console.WriteLine();
                Console.WriteLine("Your final rating: GOOD.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Congratulations, {OGH}");
                Console.WriteLine("You  have ruled wisely and well, and expanded your holdings while keeping your team happy.");
                Console.WriteLine("Altogether, a most impressive job!");
                Console.WriteLine();
                Console.WriteLine("Your final rating: SUPERB.");
            }
            Console.ResetColor();
        }

        /**
        * Prints the given message (which should ask the user for some integral
        * quantity), and returns the number entered by the user. If the user's
        * response isn't an integer, the question is repeated until the user does
        * give an integer response.
        *
        * @param message
        *            The request to present to the user.
        * @return The user's numeric response.
        */
        public static int GetNumber(string message)
        {
            while (true)
            {
                Console.Write(message);
                string userInput = Console.ReadLine();
                try
                {
                    return int.Parse(userInput);
                }
                catch (Exception)
                {
                    Console.WriteLine($"{userInput} isn't a number!");
                }
            }
        }

        /**
        * Returns a boolean response to a yes/no question.
        *
        * @param string The question to be asked.
        * @return True if the answer was yes, False if no.
        */
        public static bool GetYesOrNo(string question)
        {
            char answer;
            while (true)  // infinite loop.  return will exit the method, thus terminating the loop
            {
                Console.Write($"{question} ");
                answer = Console.ReadKey(true).KeyChar;
                answer = char.ToLower(answer);
                if (answer.Equals('y'))
                    return true;
                if (answer.Equals('n'))
                    return false;
            }
        }
    }
}
