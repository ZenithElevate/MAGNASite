/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using System.Security.Cryptography;
using System.Text;

namespace MAGNASite.Lib
{
    /// <summary>
    /// CSPRNG
    /// </summary>
    internal class PHPCSPRNG
    {
        /// <summary>
        /// Generates cryptographically secure pseudo-random bytes
        /// </summary>
        /// <param name="length">The length of the random string that should be returned in bytes.</param>
        /// <returns>Returns a string containing the requested number of cryptographically secure random bytes.</returns>
        public static string random_bytes(int length)
        {
            return Encoding.Default.GetString(RandomNumberGenerator.GetBytes(length));
        }

        /// <summary>
        /// Generates cryptographically secure pseudo-random integers
        /// </summary>
        /// <param name="min">The lowest value to be returned, which must be PHP_INT_MIN or higher.</param>
        /// <param name="max">The highest value to be returned, which must be less than or equal to PHP_INT_MAX.</param>
        /// <returns>Returns a cryptographically secure random integer in the range min to max, inclusive.</returns>
        public static int random_int(int min, int max)
        {
            return RandomNumberGenerator.GetInt32(min, max);
        }
    }
}
