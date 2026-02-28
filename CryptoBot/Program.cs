using System;
namespace CryptoTradingBot
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] hourlyPrices = { 61, 68, 66, 62, 65, 64, 68, 63, 67 };
            // random altcoin prices in the market
            int[] altcoinPrices = { 120, 450, 310, 50, 800, 200, 150, 400 };
            int userBudget = 1000;
            Console.WriteLine("=== CRYPTO ALGO-TRADING SYSTEM v1.1 ===");
            int volatilityIndex = CalculateVolatility(hourlyPrices);
            Console.WriteLine($"[ANALYSIS] Market Volatility Index: {volatilityIndex}");
            int bestInvestment = OptimizePortfolio(altcoinPrices, userBudget);
            Console.WriteLine($"[INVESTMENT] Best 3-coin combo: {bestInvestment}$ (Target: {userBudget}$)");
        }
        public static int CalculateVolatility(int[] prices) {
        }
        public static int OptimizePortfolio(int[] coins, int target)
        {
            Array.Sort(coins); // sorting so we can use the two pointers techinque
            int closestTotal = coins[0] + coins[1] + coins[2];
            for (int i = 0; i < coins.Length - 2; i++)
            {
                int L = i + 1, R = coins.Length - 1;
                while (L < R)
                {
                    int currentTotal = coins[i] + coins[L] + coins[R];
                    if (Math.Abs(target - currentTotal) < Math.Abs(target - closestTotal))
                        closestTotal = currentTotal;
                    if (currentTotal < target) L++;
                    else R--;
                }
            }
            return closestTotal;
        }
    }
}
