using System;

namespace MathHelperLibrary
{
    public static class MathHelper
    {
        /// <summary>
        /// Generates random integer.
        /// </summary>
        /// <param name="min">The min value of a random integer.</param>
        /// <param name="max">The max value of a random integer.</param>
        /// <returns></returns>
        public static int GenerateRandomInteger(int min, int max)
        {
            if (min > max)
            {
                throw new ArgumentException("The min value is greater than max value.");
            }

            var random = new Random();
            return random.Next(min, max);
        }

        /// <summary>
        /// Generates an array of random integers.
        /// </summary>
        /// <param name="min">The min value of a random integer.</param>
        /// <param name="max">The max value of a random integer.</param>
        /// <param name="count">The size of an array.</param>
        /// <returns></returns>
        public static int[] GenerateRandomIntegers(int min, int max, int count)
        {
            if (count <= 0)
            {
                throw new ArgumentException("The count is less than zero or equal to it.");
            }

            var result = new int[count];
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                result[i] = random.Next(min, max);
            }

            return result;
        }
    }
}
