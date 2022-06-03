using System;
using static HammerBitcoin.Utilities;

namespace HammerBitcoin
{
    class BitcoinMiner
    {
        
        /**
        * Prints the introductory paragraph.
        */
        public void PrintIntroductoryParagraph()
        {
            Console.Clear();
            Console.WriteLine("Congratulations! You are the newest CEO of Make Me Rich, Inc, elected for a ten year term.");
            Console.WriteLine("Your duties are to dispense living expenses for employees, direct mining of bitcoin, and");
            Console.WriteLine("buy and sell computers as needed to support the corporation.");
            Console.WriteLine("Watch out for hackers and market crashes!");
            Console.WriteLine();
            Console.WriteLine("Cash is the general currency, measured in bitcoins.");
            Console.WriteLine();
            Console.WriteLine("The following will help you in your decisions:");
            Console.WriteLine("   * Each employee needs at least 20 bitcoins converted to cash per year to survive");
            Console.WriteLine("   * Each employee can maintain at most 10 computers");
            Console.WriteLine("   * It takes 2 bitcoins to pay for electricity to mine bitcoin on a computer.");
            Console.WriteLine("   * The market price for computers fluctuates yearly");
            Console.WriteLine();
            Console.WriteLine("Lead the team wisely and you will be showered with appreciation at the end of your term.");
            Console.WriteLine("Do it poorly and you will be terminated!");
        }

        /**
        * Allows the user to play the game.
        */
        public void Play()
        {
            bool stillInOffice = true;

            PrintIntroductoryParagraph();

            while (year <= 10 && stillInOffice)
            {
                computerPrice = UpdateComputerPrice();
                PrintSummary();
                BuyComputers();
                SellComputers();
                PayEmployees();
                MaintainComputers();

                marketCrashVictims = CheckForCrash();
                employees = employees - marketCrashVictims;

                if (CountStarvedEmployees() >= 45)
                {
                    stillInOffice = false;
                }

                newEmployees = CountNewHires();
                employees += newEmployees;
                cash += MineBitcoin(computersMaintained);
                CheckForHackers();

                computerPrice = UpdateComputerPrice();
                year = year + 1;
            }
            PrintFinalScore();
        }

        /**
        * Prints the year-end summary.
        */
        public void PrintSummary()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("___________________________________________________________________");
            Console.WriteLine("\n" + OGH + "!");
            Console.WriteLine($"You are in year {year} of your ten year rule.");
            if (marketCrashVictims > 0)
            {
                Console.WriteLine($"A terrible market crash wiped out {marketCrashVictims} of your team.");
            }
            Console.WriteLine($"In the previous year {starved} of your team starved to death.");
            Console.WriteLine($"In the previous year {newEmployees} employee(s) got employed by the corporation.");
            Console.WriteLine($"The employee head count is now " + employees);
            Console.WriteLine($"We mined {cashMined} bitcoins at {bitcoinGeneratedPerComputer} bitcoins per computer.");
            if (amountStolenByHackers > 0)
            {
                Console.WriteLine($"*** Hackers stole {amountStolenByHackers} bitcoins, leaving {cash} bitcoins in your online wallet.");
            }
            else
            {
                Console.WriteLine($"We have {cash} bitcoins of cash in storage.");
            }
            Console.WriteLine($"The corporation owns {computers} computers for mining.");
            Console.WriteLine($"Computers currently cost {computerPrice} bitcoins each.");
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
