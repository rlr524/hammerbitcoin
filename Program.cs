using System;
using static HammerBitcoin.Utilities;

namespace HammerBitcoin
{
    class Program
    {
        static void Main(string[] args)
        {
            bool playAgain = true;
            while(playAgain)
            {
                BitcoinMiner game = new BitcoinMiner();
                game.Play();
                playAgain = GetYesOrNo("Would you like to play again?");
            }
            Console.WriteLine("Goodbye");
        }
    }
}
