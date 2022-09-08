/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using System.Globalization;

namespace MAGNASite.Lib
{
    internal static partial class Extensions
    {
        /// <summary>
        /// Splits a unicode strings into characters or grapheme clusters
        /// from https://stackoverflow.com/a/15111719
        /// </summary>
        /// <param name="s">The string to split</param>
        /// <returns>The collection of characters or grapheme clusters</returns>
        public static IEnumerable<string> GraphemeClusters(this string s)
        {
            var enumerator = StringInfo.GetTextElementEnumerator(s);
            while (enumerator.MoveNext())
            {
                yield return (string)enumerator.Current;
            }
        }

        /// <summary>
        /// Reverses characters or grapheme clusters in a string
        /// from https://stackoverflow.com/a/15111719
        /// </summary>
        /// <param name="s">The string to reverse</param>
        /// <returns>The reversed string</returns>
        public static string ReverseGraphemeClusters(this string s)
        {
            return string.Join("", s.GraphemeClusters().Reverse().ToArray());
        }
    }
}
