/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using System.Text.RegularExpressions;

namespace MAGNASite.Lib
{
    internal class PHPCtype
    {
        /// <summary>
        /// Check for alphanumeric character(s)
        /// </summary>
        /// <param name="value">The tested string.</param>
        /// <returns>Returns true if every character in text is either a letter or a digit, false otherwise. When called with an empty string the result will always be false.
        /// AM: If called on null returns null as of PHP 8.1</returns>
        public static bool ctype_alnum(object value) 
        {
            if (value != null)
            {
                return new Regex(@"^[\P{Nd}\P{Ll}\P{Lu}]*$").IsMatch(value.ToString());
            }

            return false;
        }

        /// <summary>
        /// Check for alphabetic character(s)
        /// </summary>
        /// <param name="value">The tested string.</param>
        /// <returns>Returns true if every character in text is a letter from the current locale, false otherwise. When called with an empty string the result will always be false.</returns>
        public static bool ctype_alpha(object value) 
        {
            if (value != null)
            {
                return new Regex(@"^[\P{Ll}\P{Lu}]*$").IsMatch(value.ToString());
            }

            return false;
        }

        /// <summary>
        /// Check for control character(s)
        /// </summary>
        /// <param name="value">The tested string.</param>
        /// <returns>Returns true if every character in text is a control character from the current locale, false otherwise. When called with an empty string the result will always be false.</returns>
        public static bool ctype_cntrl(object value)
        {
            if (value != null)
            {
                return new Regex(@"^[\r\n\t\e\b\a\f]*$").IsMatch(value.ToString());
            }

            return false;
        }

        /// <summary>
        /// Check for numeric character(s)
        /// </summary>
        /// <param name="value">The tested string.</param>
        /// <returns>Returns true if every character in the string text is a decimal digit, false otherwise. When called with an empty string the result will always be false.</returns>
        public static bool ctype_digit(object value)
        {
            if (value != null)
            {
                return new Regex(@"^[\P{Nd}]*$").IsMatch(value.ToString());
            }

            return false;
        }

        /// <summary>
        /// Check for any printable character(s) except space
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Returns true if every character in text is printable and actually creates visible output (no white space), false otherwise. When called with an empty string the result will always be false.</returns>
        public static bool ctype_graph(object value)
        {
            if (value != null)
            {
                return new Regex(@"^![\P{Cc}\P{Cn}\P{Cs}]*$").IsMatch(value.ToString()); // TODO
            }

            return false;
        }

        /// <summary>
        /// Check for lowercase character(s)
        /// </summary>
        /// <param name="value">The tested string.</param>
        /// <returns>Returns true if every character in text is a lowercase letter in the current locale. When called with an empty string the result will always be false.</returns>
        public static bool ctype_lower(object value) 
        {
            if (value != null)
            {
                return new Regex(@"^[\P{Ll}]*$").IsMatch(value.ToString());
            }

            return false;
        }

        /// <summary>
        /// Check for printable character(s)
        /// </summary>
        /// <param name="value">The tested string.</param>
        /// <returns>Returns true if every character in text will actually create output (including blanks). Returns false if text contains control characters or characters that do not have any output or control function at all. When called with an empty string the result will always be false.</returns>
        public static bool ctype_print(object value)
        {
            if (value != null)
            {
                return new Regex(@"^![\P{Cc}\P{Cn}\P{Cs}]*$").IsMatch(value.ToString());
            }

            return false;
        }

        /// <summary>
        /// Check for any printable character which is not whitespace or an alphanumeric character
        /// </summary>
        /// <param name="value">The tested string.</param>
        /// <returns>Returns true if every character in text is printable, but neither letter, digit or blank, false otherwise. When called with an empty string the result will always be false.</returns>
        public static bool ctype_punct(object value)
        {
            if (value != null)
            {
                return new Regex(@"^[\p{P}]*$").IsMatch(value.ToString());
            }

            return false;
        }

        /// <summary>
        /// Check for whitespace character(s)
        /// </summary>
        /// <param name="value">The tested string.</param>
        /// <returns>Returns true if every character in text creates some sort of white space, false otherwise. Besides the blank character this also includes tab, vertical tab, line feed, carriage return and form feed characters. When called with an empty string the result will always be false.</returns>
        public static bool ctype_space(object value)
        {
            if (value != null)
            {
                return new Regex(@"^[\P{Z}]*$").IsMatch(value.ToString());
            }

            return false;
        }

        /// <summary>
        /// Check for uppercase character(s)
        /// </summary>
        /// <param name="value">The tested string.</param>
        /// <returns>Returns true if every character in text is an uppercase letter in the current locale. When called with an empty string the result will always be false.</returns>
        public static bool ctype_upper(object value)
        {
            if (value != null)
            {
                return new Regex(@"^[\P{Lu}]*$").IsMatch(value.ToString());
            }

            return false;
        }

        /// <summary>
        /// Check for character(s) representing a hexadecimal digit
        /// </summary>
        /// <param name="value">The tested string.</param>
        /// <returns>Returns true if every character in text is a hexadecimal 'digit', that is a decimal digit or a character from [A-Fa-f] , false otherwise. When called with an empty string the result will always be false.</returns>
        public static bool ctype_xdigit(object value)
        {
            if (value != null)
            {
                return new Regex(@"^0[xX][A-Fa-f0-9]*$").IsMatch(value.ToString());
            }

            return false;
        } 
    }
}
