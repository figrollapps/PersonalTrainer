using System;
using System.Threading;

namespace Figroll.PersonalTrainer.Domain.Utilities
{
    public static class RandomProvider
    {
        private static int seed = Environment.TickCount;

        private static readonly ThreadLocal<Random> RandomWrapper =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

        public static Random GetThreadRandom()
        {
            return RandomWrapper.Value;
        }
    }
}