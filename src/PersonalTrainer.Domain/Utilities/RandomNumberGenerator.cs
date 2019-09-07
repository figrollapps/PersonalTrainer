using System;
using Figroll.PersonalTrainer.Domain.API;

namespace Figroll.PersonalTrainer.Domain.Utilities
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Random random;

        public RandomNumberGenerator()
        {
            random = RandomProvider.GetThreadRandom();
        }

        public bool CoinFlipIsHeads() => random.CoinFlipIsHeads();
        public bool CoinFlipIsTails() => random.CoinFlipIsTails();
        public bool IsPercentageChance(int percentageChance) => random.IsPercentageChance(percentageChance);

        // Next is upper bound exclusive so make it inclusive
        // which is more intuitive for non-programmers.
        public int Between(int min, int max) => random.Next(min, max + 1);

        public int Between(int min, int max, int step)
        {
            var minSteps = min / step;
            var maxSteps = max / step;

            return Between(minSteps, maxSteps) * step;
        }
    }
}