/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib
{
    /// <summary>
    /// Custom comparer for array_intersect_uassoc()
    /// </summary>
    class CustomKvpComparer : IEqualityComparer<KeyValuePair<string, object>>
    {
        readonly Func<KeyValuePair<string, object>, KeyValuePair<string, object>, int> Comparer;

        int StandardComparer(KeyValuePair<string, object> a, KeyValuePair<string, object> b)
        {
            var keyCompare = a.Key.ToUpperInvariant().CompareTo(b.Key.ToUpperInvariant());
            if (keyCompare == 0)
            {
                int valueCompare = 0;
                if (double.TryParse(a.ToString(), out double doublea) && double.TryParse(b.ToString(), out double doubleb))
                {
                    valueCompare = Math.Sign(doublea.CompareTo(doubleb));
                }
                else
                {
                    valueCompare = a.Value.ToString().CompareTo(b.Value.ToString());
                }

                return valueCompare;
            }
            else
            {
                return keyCompare;
            }
        }

        /// <summary>
        /// Instantiates custom comparer using user-supplied comparison function
        /// </summary>
        /// <param name="comparer">The user-supplied custom comparer function.</param>
        public CustomKvpComparer(Func<KeyValuePair<string, object>, KeyValuePair<string, object>, int>? comparer) => Comparer = comparer ?? StandardComparer;

        /// <summary>
        /// Determines whether the specified keys are equal.
        /// </summary>
        /// <remarks>Uses custom comparer function.</remarks>
        /// <param name="x">The first key to compare.</param>
        /// <param name="y">The second key to compare.</param>
        /// <returns>True if specified key are equal, false otherwise.</returns>
        public bool Equals(KeyValuePair<string, object> x, KeyValuePair<string, object> y) => Comparer(x, y) == 0;

        public int GetHashCode(KeyValuePair<string, object> obj) => GetHashCode(obj);
    }
}