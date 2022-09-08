/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib
{
    /// <summary>
    /// Custom comparer for array_diff_uassoc()
    /// </summary>
    class CustomComparer : IEqualityComparer<string>
    {
        readonly Func<string, string, int> Comparer;

        int StandardComparer(string a, string b)
        {
            return a.ToUpperInvariant().
                CompareTo(b.ToUpperInvariant());
        }

        /// <summary>
        /// Instantiates custom comparer using user-supplied comparison function
        /// </summary>
        /// <param name="comparer">The user-supplied custom comparer function.</param>
        public CustomComparer(Func<string, string, int>? comparer) => Comparer = comparer == null ? StandardComparer : comparer;

        /// <summary>
        /// Determines whether the specified keys are equal.
        /// </summary>
        /// <remarks>Uses custom comparer function.</remarks>
        /// <param name="x">The first key to compare.</param>
        /// <param name="y">The second key to compare.</param>
        /// <returns>True if specified key are equal, false otherwise.</returns>
        public bool Equals(string x, string y) => Comparer(x, y) == 0;

        public int GetHashCode(string obj) => GetHashCode(obj);
    }
}