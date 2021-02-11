using System;
using System.Globalization;

namespace TwoDiceRoll
{
    class Program
    {
        private readonly Random Rand = new Random();
        private readonly NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat; 

        public void ShowBanner()
        {
            string banner = @"
***************** ACME Industries Rodent Sciences Division *****************
*                                                                          *
*                      Dice Rolling Simulation v. 1.1                      * 
*                                                                          *
*                    One row per possible roll of dice                     * 
*                                                                          *
*                        Includes Bonus Features!!                         *
****************************************************************************
            ";

            Console.WriteLine(banner);
        }

        public void Run(){

            int columnWidth = 15;

            int numRolls;

            Console.Write("How many rolls would you like to simulate >>  ");
            if(!int.TryParse(Console.ReadLine(), out numRolls) || !( numRolls >= 1))
            {
                Console.Write("Invalid input, defulting to 100,000.\n");
                numRolls = 100000;
            }
            
            int[] frequencies = new int[11];
            int[] percentages = new int[11];
            
            // Simulate the dice rolls
            for(int roll = 0; roll < numRolls; roll++)
            {
                // Generate random dice roll in range 1 - 6 and find the sum
                int dice1Roll = Rand.Next(1, 7); 
                int dice2Roll = Rand.Next(1, 7);
                int rollSum = dice1Roll + dice2Roll;

                int rollIndex = rollSum - 2;
                frequencies[rollIndex]++;
            }
            
            // Write simulation heading
            Console.Write(" ");
            Console.Write("Roll".PadLeft(columnWidth - 1 ));
            Console.Write(" ");
            Console.Write("Count".PadLeft(columnWidth - 1));
            Console.Write(" ");
            Console.WriteLine("Pct".PadLeft(columnWidth - 1));

        
            nfi.PercentDecimalDigits = 2;
            double totalPercent = 0;

            // Write simulation results
            for(int outcome = 2; outcome <= 12;outcome++)
            {
                int count = frequencies[outcome - 2];
                double percent = ((double) count) / numRolls;

                Console.Write($"{outcome}".PadLeft(columnWidth));
                Console.Write($"{count}".PadLeft(columnWidth));
                Console.WriteLine(percent.ToString("P", nfi).PadLeft(columnWidth));

                totalPercent += percent;
            }

            nfi.PercentDecimalDigits = 0;

            // Write the totals
            Console.Write($"{numRolls}".PadLeft(columnWidth * 2));
            Console.WriteLine(totalPercent.ToString("P", nfi).PadLeft(columnWidth));


            Console.Write("\nTo run another simulation, type \"Y\": ");
            ConsoleKey response = Console.ReadKey(false).Key;
            if(response == ConsoleKey.Y)
            {
                Console.WriteLine();
                Run();
            }
            else
            {
                Console.Write("\nPress any key to end program...");
                Console.ReadKey();
            }
        }

        public static void Main(string[] args)
        {
            Program simulation = new Program();
            simulation.ShowBanner();
            simulation.Run();
        }
    }
}
