/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using System.Text.RegularExpressions;

namespace MAGNASite.Lib
{
    /// <summary>
    /// Regular expressions
    /// </summary>
    internal class PHPPCRE
    {
        const string specials = @".\+*?[^]$(){}=!<>|:-";

        /// <summary>
        /// Quote regular expression characters
        /// </summary>
        /// <param name="str">The input string.</param>
        /// <param name="delimiter">If the optional delimiter is specified, it will also be escaped. This is useful for escaping the delimiter that is required by the PCRE functions. The / is the most commonly used delimiter. </param>
        /// <returns>Returns the quoted (escaped) string. </returns>
        public static string preg_quote(string @string, string delimiter = null)
        {
            @string = @string.Replace(@"\0", @"\000");

            foreach (var c in specials)
            {
                @string = @string.Replace(c.ToString(), $@"\{c}");
            }

            return @string.Replace(delimiter, @"\");
        }

        /// <summary>
        /// Perform a regular expression match.
        /// Searches subject for a match to the regular expression given in pattern. 
        /// </summary>
        /// <param name="pattern">The pattern to search for, as a string.</param>
        /// <param name="subject">The input string.</param>
        /// <param name="matches">If matches is provided, then it is filled with the results of search. $matches[0] will contain the text that matched the full pattern, $matches[1] will have the text that matched the first captured parenthesized subpattern, and so on.</param>
        /// <param name="flags"> flags can be a combination of the following flags: PREG_OFFSET_CAPTURE, PREG_UNMATCHED_AS_NULL.
        /// <param name="offset">Normally, the search starts from the beginning of the subject string. The optional parameter offset can be used to specify the alternate place from which to start the search (in bytes).</param>
        /// <returns>preg_match() returns 1 if the pattern matches given subject, 0 if it does not, or false on failure.</returns>
        public int preg_match(string pattern, string subject, string[]? matches = null, int flags = 0, int offset = 0)
        {
            var result = 0;
            var r = new Regex(pattern);
            var m = r.Match(offset == 0 ? subject : subject.Substring(offset, subject.Length - 1));
            while (m.Success)
            {
                result = 1;
                if (matches != null)
                {
                    matches.Append(m.Value);
                }
            }

            return result;
        }
    }
}
