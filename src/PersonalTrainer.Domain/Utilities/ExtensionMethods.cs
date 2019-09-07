using System;
using System.Collections.Generic;
using System.Linq;

namespace Figroll.PersonalTrainer.Domain.Utilities
{
    public static class ExtensionMethods
    {
        private static readonly Random RNG = RandomProvider.GetThreadRandom();

        public static void Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var element in source)
            {
                action(element);
            }
        }

        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            var list = enumerable as IList<T> ?? enumerable.ToList();
            return !list.Any() ? default(T) : list.ElementAt(RNG.Next(0, list.Count));
        }

        public static T Random<T>(this IEnumerable<T> enumerable, int bias)
        {
            var list = enumerable as IList<T> ?? enumerable.ToList();
            return !list.Any() ? default(T) : list.ElementAt(RNG.Next(0, list.Count - bias) + bias);
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;

            while (n > 1)
            {
                n--;
                var k = RNG.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
        {
            var shuffledCopy = list.ToList();
            shuffledCopy.Shuffle();

            return shuffledCopy;
        }

        public static bool CoinFlipIsHeads(this Random rng)
        {
            return rng.Next(2) == 0;
        }

        public static bool CoinFlipIsTails(this Random rng)
        {
            return rng.Next(2) == 1;
        }

        public static bool IsPercentageChance(this Random rng, int percentageChance)
        {
            // zero based so 99 + 1 to make percentage chance more obvious.
            return rng.Next(99) + 1 <= percentageChance;
        }

        public static int ToMilliseconds(this int seconds)
        {
            return seconds * 1000;
        }

        public static int ToSeconds(this int milliseconds)
        {
            return milliseconds / 1000;
        }
    }
}