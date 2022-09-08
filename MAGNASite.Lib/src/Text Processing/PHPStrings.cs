/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using Force.Crc32;
using HtmlAgilityPack;
using SprintfNET;
using System.Globalization;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Vereyon.Web;

namespace MAGNASite.Lib
{
    /// <summary>
    /// Strings
    /// </summary>
    internal partial class PHPStrings
    {
        readonly static HashAlgorithm
            HashMd5 = HashAlgorithm.Create("MD5"),
            HashSha256 = HashAlgorithm.Create("SHA256"),
            HashSha512 = HashAlgorithm.Create("SHA512");

        readonly static char[] pointSlash = new char[] { '.', '/' };

        static Dictionary<string, object> localeConv;

        static Dictionary<string, object> LocaleConv
        {
            get
            {
                if (localeConv == null)
                {
                    string 
                        p_sign_posn = string.Empty, 
                        n_sign_posn = string.Empty;

                    List<string>
                        grouping = new List<string>(2),
                        mon_grouping = new List<string>(2);

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        const int expectChars = 10;
                        var sb = new StringBuilder(expectChars);
                        UnsafeNativeMethods.GetLocaleInfoEx(Thread.CurrentThread.CurrentCulture.Name, UnsafeNativeMethods.LCTYPE.LOCALE_IPOSSIGNPOSN, sb, expectChars);
                        p_sign_posn = sb.ToString();
                        sb.Clear();
                        UnsafeNativeMethods.GetLocaleInfoEx(Thread.CurrentThread.CurrentCulture.Name, UnsafeNativeMethods.LCTYPE.LOCALE_INEGSIGNPOSN, sb, expectChars);
                        n_sign_posn = sb.ToString();
                        sb.Clear();
                        UnsafeNativeMethods.GetLocaleInfoEx(Thread.CurrentThread.CurrentCulture.Name, UnsafeNativeMethods.LCTYPE.LOCALE_SGROUPING, sb, expectChars);
                        grouping = sb.ToString().Split(";").ToList();
                        sb.Clear();
                        UnsafeNativeMethods.GetLocaleInfoEx(Thread.CurrentThread.CurrentCulture.Name, UnsafeNativeMethods.LCTYPE.LOCALE_SMONGROUPING, sb, expectChars);
                        mon_grouping = sb.ToString().Split(";").ToList();
                        sb.Clear();
                    }

                    localeConv = new Dictionary<string, object>
                    {
                        {"decimal_point", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator }, // Decimal point character
                        {"thousands_sep", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator }, // Thousands separator
                        {"grouping", grouping }, //  Array containing numeric groupings
                        {"int_curr_symbol", Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol }, //   International currency symbol (i.e. USD)
                        {"currency_symbol", Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol }, //   Local currency symbol (i.e. $)
                        {"mon_decimal_point", Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator }, // Monetary decimal point character
                        {"mon_thousands_sep", Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator }, // Monetary thousands separator
                        {"mon_grouping", mon_grouping }, //  Array containing monetary groupings
                        {"positive_sign", Thread.CurrentThread.CurrentCulture.NumberFormat.PositiveSign }, // Sign for positive values
                        {"negative_sign", Thread.CurrentThread.CurrentCulture.NumberFormat.NegativeSign }, // Sign for negative values
                        {"int_frac_digits", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalDigits }, //   International fractional digits
                        {"frac_digits", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalDigits }, //   Local fractional digits
                        {"p_cs_precedes", Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyPositivePattern == 0 || Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyPositivePattern == 2 }, // true if currency_symbol precedes a positive value, false if it succeeds one
                        {"p_sep_by_space", Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyPositivePattern == 1 || Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyPositivePattern==3 }, // true if a space separates currency_symbol from a positive value, false otherwise
                        {"n_cs_precedes", Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyNegativePattern == 0 || Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyNegativePattern==2 }, // true if currency_symbol precedes a negative value, false if it succeeds one
                        {"n_sep_by_space", Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyNegativePattern==1||Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyNegativePattern==3 }, //  true if a space separates currency_symbol from a negative value, false otherwise
                        {" p_sign_posn", p_sign_posn},
                        /*
                        0 - Parentheses surround the quantity and currency_symbol
                        1 - The sign string precedes the quantity and currency_symbol
                        2 - The sign string succeeds the quantity and currency_symbol
                        3 - The sign string immediately precedes the currency_symbol
                        4 - The sign string immediately succeeds the currency_symbol
                        */
                        {"n_sign_posn", n_sign_posn },
                        /*
                        0 - Parentheses surround the quantity and currency_symbol
                        1 - The sign string precedes the quantity and currency_symbol
                        2 - The sign string succeeds the quantity and currency_symbol
                        3 - The sign string immediately precedes the currency_symbol
                        4 - The sign string immediately succeeds the currency_symbol
                        */
                    };
                }

                return localeConv;
            }
        }

        CancellationTokenSource CTS;

        public PHPStrings()
        {
            CTS = new CancellationTokenSource(PHPIniFile.max_execution_time * 1000);
        }

        public PHPStrings(CancellationTokenSource cts)
        {
            CTS = cts;
        }

        static byte[] ascii;

        /// <summary>
        /// Cached ASCII set
        /// </summary>
        static byte[] Ascii 
        {
            get
            {
                if (ascii == null)
                {
                    ascii = new byte[256];
                    for (byte i = 0; i < 256 / 4; i += 4)
                    {
                        ascii[i] = i;
                        ascii[i + 1] = i;
                        ascii[i + 2] = i;
                        ascii[i + 3] = i;
                    }
                }

                return ascii;
            } 
        }

        /// <summary>
        /// Repeates given hash, given number of times, over input data
        /// </summary>
        /// <param name="iterCount">Number of times to repeat the hashing.</param>
        /// <param name="input">The data to hash.</param>
        /// <param name="hash">The hasing algorithm.</param>
        /// <returns>Hashed data.</returns>
        private string IteratedHash(int iterCount, MemoryStream input, HashAlgorithm hash)
        {
            var i = 0;
            Task<byte[]> t;

            do
            {
                t = hash.ComputeHashAsync(input, CTS.Token); // TODO EXT DES
                _ = Task.WhenAny(t);

                if (t.IsCanceled)
                {
                    return null;
                }

                input.SetLength(0);
                input.Write(t.Result);
            } while (i++ < iterCount);

            return t.IsCanceled ? null : Encoding.Default.GetString(t.Result);
        }

        /// <summary>
        /// Quote string with slashes in a C style
        /// </summary>
        /// <param name="@string">The string to be escaped.</param>
        /// <param name="characters">A list of characters to be escaped. If characters contains characters \n, \r etc., they are converted in C-like style, while other non-alphanumeric characters with ASCII codes lower than 32 and higher than 126 converted to octal representation.</param>
        /// <returns>Returns the escaped string.</returns>
        public static string addcslashes(string @string, string characters) 
        {
            return @string; // TODO
        }

        /// <summary>
        /// Quote string with slashes
        /// </summary>
        /// <param name="@string">The string to be escaped.</param>
        /// <returns>Returns the escaped string.</returns>
        public static string addslashes(string @string)
        {
            return string.IsNullOrEmpty(@string) ? 
                @string : 
                @string.Replace("\\", @"\\").Replace("'", @"\'").Replace("\"", "\\\"").Replace("\0", "\\0");
        }

        /// <summary>
        /// Convert binary data into hexadecimal representation
        /// </summary>
        /// <param name="@string">A string.</param>
        /// <returns>Returns the hexadecimal representation of the given string.</returns>
        public static string bin2hex(string @string)
        {
            return string.IsNullOrEmpty(@string) ?
                @string :
                BitConverter.ToString(Encoding.Default.GetBytes(@string));
        }

        /// <summary>
        /// Alias of rtrim
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <param name="characters">You can also specify the characters you want to strip, by means of the characters parameter. Simply list all characters that you want to be stripped. With .. you can specify a range of characters.</param>
        /// <returns>Returns the modified string.</returns>
        public static string chop(string @string, string characters = " \n\r\t\v\x00") => rtrim(@string, characters);

        /// <summary>
        /// Generate a single-byte string from a number
        /// </summary>
        /// <param name="codepoint">An integer between 0 and 255.</param>
        /// <returns>A single-character string containing the specified byte.</returns>
        public static string chr(int codepoint)
        {
            return ((char)(codepoint & 255)).ToString();
        }

        /// <summary>
        /// Split a string into smaller chunks
        /// </summary>
        /// <param name="@string">The string to be chunked.</param>
        /// <param name="length">The chunk length.</param>
        /// <param name="separator">The line ending sequence.</param>
        /// <returns>Returns the chunked string.</returns>
        public static string chunk_split(string @string, int length = 76, string separator = "\r\n")
        {
            if (string.IsNullOrEmpty(@string) || @string.Length < length)
            {
                return @string;
            }

            var sb = new StringBuilder();
            for (int i = length, j = 0; i < @string.Length; i += length, j += length)
            {
                sb.Append(@string.AsSpan(j, System.Math.Min(i, @string.Length))).Append(separator);
            }

            return sb.ToString();
        }

        public static void convert_cyr_string() { } // Convert from one Cyrillic character set to another

        /// <summary>
        /// Decode a uuencoded string
        /// </summary>
        /// <param name="@string">The uuencoded data.</param>
        /// <returns>Returns the decoded data as a string or false on failure.</returns>
        public static string convert_uudecode(string @string)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return @string;
            }

            return @string; // TODO
        }

        /// <summary>
        /// Uuencode a string
        /// </summary>
        /// <param name="@string"></param>
        /// <returns></returns>
        public static string convert_uuencode(string @string)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return @string;
            }

            return @string; // TODO
        }

        /// <summary>
        /// Return information about characters used in a string
        /// </summary>
        /// <param name="@string">The examined string.</param>
        /// <param name="mode">See return values.</param>
        /// <returns> Depending on mode count_chars() returns one of the following:<br />
        /// 0 - an array with the byte-value as key and the frequency of every byte as value.<br />
        /// 1 - same as 0 but only byte-values with a frequency greater than zero are listed.<br />
        /// 2 - same as 0 but only byte-values with a frequency equal to zero are listed.<br />
        /// 3 - a string containing all unique characters is returned.<br />
        /// 4 - a string containing all not used characters is returned.</returns>
        public static (Dictionary<byte, int>, string) count_chars(string @string, int mode = 0) 
        {
            if (!string.IsNullOrEmpty(@string))
            {
                return (null, null);
            }

            switch (mode)
            {
                case 0: // an array with the byte-value as key and the frequency of every byte as value.
                    var result = new Dictionary<byte, int>(256);

                    for (var i = 0; i < 256; i++)
                    {
                        result.Add(Ascii[i], @string.Count(c => c == Ascii[i]));
                    }

                    return (result, null);
                case 1: // same as 0 but only byte-values with a frequency greater than zero are listed.
                    result = new Dictionary<byte, int>(256);

                    for (var i = 0; i < 256; i++)
                    {
                        var freq = @string.Count(c => c == Ascii[i]);
                        if (freq > 0)
                        {
                            result.Add(Ascii[i], freq);
                        }
                    }

                    return (result, null);
                case 2: // same as 0 but only byte-values with a frequency equal to zero are listed.
                    result = new Dictionary<byte, int>(256);

                    for (var i = 0; i < 256; i++)
                    {
                        var freq = @string.Count(c => c == Ascii[i]);
                        if (freq == 0)
                        {
                            result.Add(Ascii[i], freq);
                        }
                    }

                    return (result, null);
                case 3: // a string containing all unique characters is returned.
                    var sb = new StringBuilder();
                    for (var i = 0; i < 256; i++)
                    {
                        if (@string.Contains(Ascii[i].ToString()))
                        {
                            sb.Append(Ascii[i]);
                        }
                    }
                    return (null, sb.ToString());
                case 4: // a string containing all not used characters is returned.
                    sb = new StringBuilder();
                    for (var i = 0; i < 256; i++)
                    {
                        if (!@string.Contains(Ascii[i].ToString()))
                        {
                            sb.Append(Ascii[i]);
                        }
                    }
                    return (null, sb.ToString());
                default:
                    return (null, null);
            }
        }

        /// <summary>
        /// Calculates the crc32 polynomial of a string
        /// </summary>
        /// <param name="@string">The data.</param>
        /// <returns>Returns the crc32 checksum of string as an integer.</returns>
        public static long crc32(string @string) 
        {
            if (string.IsNullOrEmpty(@string))
            {
                return 0;
            }

            var crc32 = new Crc32Algorithm();
            using (var input = new MemoryStream(Encoding.Default.GetBytes(@string)))
            {
                var t = crc32.ComputeHashAsync(input);
                _ = Task.WhenAny(t);
                return Convert.ToInt32(t.Result);
            }
        }

        /// <summary>
        /// One-way string hashing
        /// </summary>
        /// <param name="@string">The string to be hashed.</param>
        /// <param name="salt">A salt string to base the hashing on. If not provided, the behaviour is defined by the algorithm implementation and can lead to unexpected results.</param>
        /// <returns>Returns the hashed string or a string that is shorter than 13 characters and is guaranteed to differ from the salt on failure.</returns>
        public string crypt(string @string, string salt)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return @string;
            }

            if (salt.Length == 2 &&
                ((salt[0] >= '0' && salt[0] <= '9') || (salt[0] >= 'a' && salt[0] <= 'z') || (salt[0] >= 'A' && salt[0] <= 'Z')) &&
                ((salt[1] >= '0' && salt[1] <= '9') || (salt[1] >= 'a' && salt[1] <= 'z') || (salt[1] >= 'a' && salt[1] <= 'z'))
            )
            {
                /* 
                 * CRYPT_STD_DES - Standard DES-based hash with a two character salt from the alphabet "./0-9A-Za-z".
                 * Using invalid characters in the salt will cause crypt() to fail.
                 */
                using (var input = new MemoryStream(Encoding.Default.GetBytes(@string + salt.Trim(pointSlash))))
                {
                    return IteratedHash(1, input, HashSha256); // TODO Std Des
                }
            }
            else if (salt.Length == 9 && salt[0] == '_')
            {
                /*
                 * CRYPT_EXT_DES - Extended DES-based hash. The "salt" is a 9-character string consisting of an underscore 
                 * followed by 4 characters of iteration count and 4 characters of salt. Each of these 4-character strings 
                 * encode 24 bits, least significant character first. The values 0 to 63 are encoded as ./0-9A-Za-z.
                 * Using invalid characters in the salt will cause crypt() to fail. 
                 */
                var actualSalt = salt.Substring(5, 4);
                using (var input = new MemoryStream(Encoding.Default.GetBytes(@string + actualSalt)))
                {
                    return IteratedHash(Convert.ToInt32(salt.Substring(1, 4).Trim(pointSlash)), input, HashSha256); // TODO Ext Des
                }
            }
            else if (salt.Length == 12 && salt.StartsWith("$1$"))
            {
                /*
                 * CRYPT_MD5 - MD5 hashing with a twelve character salt starting with $1$ 
                 */
                using (var input = new MemoryStream(Encoding.Default.GetBytes(@string + salt)))
                {
                    return IteratedHash(1, input, HashMd5);
                }
            }
            else if (salt.Length == 28 &
                (salt.StartsWith("$2a$") || salt.StartsWith("$2x$") || salt.StartsWith("$2y$")) &&
                salt[4] >= '0' && salt[4] <= '3' && salt[5] >= '0' && salt[5] <= '9'
            )
            {
                /*
                 * CRYPT_BLOWFISH - Blowfish hashing with a salt as follows: "$2a$", "$2x$" or "$2y$", 
                 * a two digit cost parameter, "$", and 22 characters from the alphabet "./0-9A-Za-z". 
                 * Using characters outside of this range in the salt will cause crypt() to return a zero-length string. 
                 * The two digit cost parameter is the base-2 logarithm of the iteration count
                 * for the underlying Blowfish-based hashing algorithm and must be in range 04-31, 
                 * values outside this range will cause crypt() to fail.
                 * "$2x$" hashes are potentially weak;
                 * "$2a$" hashes are compatible and mitigate this weakness.
                 * For new hashes, "$2y$" should be used. 
                 */
                var parts = salt.Split('$', StringSplitOptions.RemoveEmptyEntries);

                using (var input = new MemoryStream(Encoding.Default.GetBytes(string.Concat(@string, parts[2])))) // salt.AsSpan(6, 16)
                {
                    return IteratedHash(Convert.ToInt32(parts[1].Trim(pointSlash)), input, HashSha256); // TODO Blowfish
                }
            }
            else if (salt.Length <= 37 && salt.StartsWith("$5$"))
            {
                /*
                 * CRYPT_SHA256 - SHA-256 hash with a sixteen character salt prefixed with $5$. 
                 * If the salt string starts with 'rounds=<N>$', the numeric value of N is used to indicate 
                 * how many times the hashing loop should be executed, much like the cost parameter on Blowfish.
                 * The default number of rounds is 5000, there is a minimum of 1000 and a maximum of 999,999,999.
                 * Any selection of N outside this range will be truncated to the nearest limit. 
                 */
                var parts = salt.Substring(9, 30).Split('$');

                using (var input = new MemoryStream(Encoding.Default.GetBytes(@string + parts[1])))
                {
                    return IteratedHash(Convert.ToInt32(parts[0]), input, HashSha256);
                }
            }
            else if (salt.Length <= 37 && salt.StartsWith("$6$"))
            {
                /*
                 * CRYPT_SHA512 - SHA-512 hash with a sixteen character salt prefixed with $6$.
                 * If the salt string starts with 'rounds=<N>$', the numeric value of N is used to indicate 
                 * how many times the hashing loop should be executed, much like the cost parameter on Blowfish. 
                 * The default number of rounds is 5000, there is a minimum of 1000 and a maximum of 999,999,999. 
                 * Any selection of N outside this range will be truncated to the nearest limit. 
                 */
                var parts = salt.Substring(9, 30).Split('$');

                using (var input = new MemoryStream(Encoding.Default.GetBytes(@string + parts[1])))
                {
                    return IteratedHash(Convert.ToInt32(parts[0]), input, HashSha512);
                }
            }

            return null;
        }

        /// <summary>
        /// Output one or more strings
        /// </summary>
        /// <param name="@string">One or more string expressions to output, separated by commas. Non-string values will be coerced to strings, even when the strict_types directive is enabled.</param>
        /// <returns>No value is returned in PHP. Concatenated parameters returned in MAGNASite.</returns>
        public static string echo(params string[] @string)
        {
            return string.Join(null, @string);
        }

        /// <summary>
        /// Split a string by a string
        /// </summary>
        /// <param name="separator">The boundary string.</param>
        /// <param name="@string">The input string.</param>
        /// <param name="limit">If limit is set and positive, the returned array will contain a maximum of limit elements with the last element containing the rest of string.<br />
        /// If the limit parameter is negative, all components except the last -limit are returned.<br />
        /// If the limit parameter is zero, then this is treated as 1.</param>
        /// <returns></returns>
        public static IEnumerable<string> explode(string separator, string @string, int limit = PHPConstants.PHP_INT_MAX)
        {
            if (string.IsNullOrEmpty(separator) || string.IsNullOrEmpty(@string))
            {
                return null;
            }

            return @string.Split(separator, limit, StringSplitOptions.None).ToArray();
        }

        /// <summary>
        /// Write a formatted string to a stream
        /// </summary>
        /// <param name="stream">A file system pointer resource that is typically created using fopen().</param>
        /// <param name="format">The format string is composed of zero or more directives: ordinary characters (excluding %) that are copied directly to the result and conversion specifications, each of which results in fetching its own parameter.</param>
        /// <param name="values">Values that correspond to the format elements.</param>
        /// <returns>Returns the length of the string written. </returns>
        public static int fprintf(FileStream stream, string format, params object[] values) 
        {
            if (string.IsNullOrEmpty(format) || values == null)
            {
                return 0;
            }

            var len = values.Select(v => v.ToString()).Aggregate(0, (len, s) => len + s.Length); // TODO format and write
            return len;
        }

        public static Dictionary<string, string> get_html_translation_table(int table = (int)HtmlEntities.HTML_SPECIALCHARS, int flags = (int)HtmlDocumentTypes.ENT_QUOTES | (int)HtmlDocumentTypes.ENT_SUBSTITUTE | (int)HtmlDocumentTypes.ENT_HTML401, string encoding = "UTF-8")
        {
            if ((HtmlEntities)table == HtmlEntities.HTML_ENTITIES)
            {
                return HTML_ENTITIES.GetEntities((HtmlDocumentTypes)flags).ToDictionary(x => x.Key, x => x.Value);
            }

            return HTML_SPECIALCHARS.GetEntities((HtmlDocumentTypes)flags).ToDictionary(x => x.Key, x => x.Value);
        } // Returns the translation table used by htmlspecialchars and htmlentities

        /// <summary>
        /// Convert logical Hebrew text to visual text
        /// </summary>
        /// <param name="@string">A Hebrew input string.</param>
        /// <param name="max_chars_per_line">This optional parameter indicates maximum number of characters per line that will be returned.</param>
        /// <returns>Returns the visual string.</returns>
        public static string hebrev(string @string, int max_chars_per_line = 0) 
        {
            return @string; // TODO
        }

        /// <summary>
        /// Convert logical Hebrew text to visual text with newline conversion
        /// </summary>
        /// <param name="@string">A Hebrew input string.</param>
        /// <param name="max_chars_per_line">This optional parameter indicates maximum number of characters per line that will be returned.</param>
        /// <returns>Returns the visual string.</returns>
        [Obsolete("Warning: This function has been DEPRECATED as of PHP 7.4.0, and REMOVED as of PHP 8.0.0. Relying on this function is highly discouraged.")]
        public static string hebrevc(string hebrew_text, int max_chars_per_line = 0) 
        { 
            return hebrew_text;
        }

        /// <summary>
        /// Decodes a hexadecimally encoded binary string
        /// </summary>
        /// <param name="@string">Hexadecimal representation of data.</param>
        /// <returns>Returns the binary representation of the given data or false on failure.</returns>
        public static (string, bool) hex2bin(string @string)
        {
            return (null, false);
        }

        /// <summary>
        /// Convert HTML entities to their corresponding characters
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <param name="flags">A bitmask of one or more of the following flags, which specify how to handle quotes and which document type to use. The default is ENT_QUOTES | ENT_SUBSTITUTE | ENT_HTML401.</param>
        /// <param name="encoding">An optional argument defining the encoding used when converting characters.</param>
        /// <returns>Returns the decoded string.</returns>
        public static string html_entity_decode(string @string, int flags = (int)HtmlDocumentTypes.ENT_QUOTES | (int)HtmlDocumentTypes.ENT_SUBSTITUTE | (int)HtmlDocumentTypes.ENT_HTML401, string encoding = null)
        {
            return WebUtility.HtmlDecode(@string); // TODO
        }

        /// <summary>
        /// Convert all applicable characters to HTML entities
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <param name="flags">A bitmask of one or more of the following flags, which specify how to handle quotes and which document type to use. The default is ENT_QUOTES | ENT_SUBSTITUTE | ENT_HTML401.</param>
        /// <param name="encoding">An optional argument defining the encoding used when converting characters.</param>
        /// <param name="double_encode">When double_encode is turned off PHP will not encode existing html entities. The default is to convert everything.</param>
        /// <returns>Returns the encoded string.</returns>
        public static string htmlentities(string @string, int flags = (int)HtmlDocumentTypes.ENT_QUOTES | (int)HtmlDocumentTypes.ENT_SUBSTITUTE | (int)HtmlDocumentTypes.ENT_HTML401, string encoding = null, bool double_encode = true)
        {
            return WebUtility.HtmlEncode(@string); // TODO
        }

        /// <summary>
        /// Convert special HTML entities back to characters
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <param name="flags">A bitmask of one or more of the following flags, which specify how to handle quotes and which document type to use. The default is ENT_QUOTES | ENT_SUBSTITUTE | ENT_HTML401.</param>
        /// <returns>Returns the decoded string.</returns>
        public static string htmlspecialchars_decode(string @string, int flags = (int)HtmlDocumentTypes.ENT_QUOTES | (int)HtmlDocumentTypes.ENT_SUBSTITUTE | (int)HtmlDocumentTypes.ENT_HTML401)
        {
            return WebUtility.HtmlDecode(@string); // TODO
        }

        /// <summary>
        /// Convert special characters to HTML entities
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <param name="flags">A bitmask of one or more of the following flags, which specify how to handle quotes and which document type to use. The default is ENT_QUOTES | ENT_SUBSTITUTE | ENT_HTML401.</param>
        /// <param name="encoding">An optional argument defining the encoding used when converting characters.</param>
        /// <param name="double_encode">When double_encode is turned off PHP will not encode existing html entities. The default is to convert everything.</param>
        /// <returns>Returns the encoded string.</returns>
        public static string htmlspecialchars(string @string, int flags = (int)HtmlDocumentTypes.ENT_QUOTES | (int)HtmlDocumentTypes.ENT_SUBSTITUTE | (int)HtmlDocumentTypes.ENT_HTML401, string encoding = null, bool double_encode = true)
        {
            return WebUtility.HtmlEncode(@string); // TODO
        }

        /// <summary>
        /// Join array elements with a string
        /// </summary>
        /// <param name="separator">Optional. Defaults to an empty string.</param>
        /// <param name="array">The array of strings to implode.</param>
        /// <returns>Returns a string containing a string representation of all the array elements in the same order, with the separator string between each element.</returns>
        public static string implode(string separator, Dictionary<string, object> array)
        {
            return array == null ? null : string.Join(separator, array.Values);
        }

        /// <summary>
        /// Join array elements with a string
        /// </summary>
        /// <param name="array">The array of strings to implode.</param>
        /// <returns>Returns a string containing a string representation of all the array elements in the same order, with the separator string between each element.</returns>
        public static string implode(Dictionary<string, object> array)
        {
            return array == null ? null : string.Join(string.Empty, array.Values);
        }

        /// <summary>
        /// Join array elements with a string
        /// </summary>
        /// <param name="separator">Optional. Defaults to an empty string.</param>
        /// <param name="array">The array of strings to implode.</param>
        /// <returns>Returns a string containing a string representation of all the array elements in the same order, with the separator string between each element.</returns>
        public static string join(string separator, Dictionary<string, object> array) => implode(separator, array); // Alias of implode

        /// <summary>
        /// Join array elements with a string
        /// </summary>
        /// <param name="array">The array of strings to implode.</param>
        /// <returns>Returns a string containing a string representation of all the array elements in the same order, with the separator string between each element.</returns>
        public static string join(Dictionary<string, object> array) => implode(array); // Alias of implode

        /// <summary>
        /// Make a string's first character lowercase
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <returns>Returns the resulting string.</returns>
        public static string lcfirst(string @string)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return null;
            }

            var buf = @string.ToCharArray();
            buf[0] = char.ToLower(buf[0]);
            return new string(buf);
        }

        /// <summary>
        /// Calculate Levenshtein distance between two strings
        /// </summary>
        /// <param name="string1">One of the strings being evaluated for Levenshtein distance.</param>
        /// <param name="string2">One of the strings being evaluated for Levenshtein distance.</param>
        /// <param name="insertion_cost">Defines the cost of insertion.</param>
        /// <param name="replacement_cost">Defines the cost of replacement.</param>
        /// <param name="deletion_cost">Defines the cost of deletion.</param>
        /// <returns>This function returns the Levenshtein-Distance between the two argument strings or -1, if one of the argument strings is longer than the limit of 255 characters.</returns>
        public static int levenshtein(string string1, string string2, int insertion_cost = 1, int replacement_cost = 1, int deletion_cost = 1)
        {
            if (string.IsNullOrEmpty(string1) && string.IsNullOrEmpty(string2))
            {
                return -1;
            }

            if (string1.Length > 255 || string2.Length > 255)
            {
                return -1;
            }

            return 0; // TODO
        }

        public static Dictionary<string, object> localeconv() 
        {
            return LocaleConv;
        } // Get numeric formatting information

        /// <summary>
        /// Strip whitespace (or other characters) from the beginning of a string
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <param name="characters">You can also specify the characters you want to strip, by means of the characters parameter. Simply list all characters that you want to be stripped. With .. you can specify a range of characters.</param>
        /// <returns>Returns the modified string.</returns>
        public static string ltrim(string @string, string characters = " \n\r\t\v\x00")
        {
            return string.IsNullOrEmpty(@string) ?
                @string :
                @string.TrimStart(characters.ToCharArray());
        }

        /// <summary>
        /// Calculates the md5 hash of a given file
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="binary">When true, returns the digest in raw binary format with a length of 16.</param>
        /// <returns>Returns a string on success, false otherwise.</returns>
        public static (string, bool) md5_file(string filename, bool binary = false) 
        {
            if (string.IsNullOrEmpty(filename))
            {
                return (null, false);
            }

            if (File.Exists(filename))
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(filename))
                    {
                        var hash = md5.ComputeHash(stream);
                        return (binary ? Encoding.Default.GetString(hash) : Convert.ToHexString(hash), true);
                    }
                }
            }

            return (null, false);
        }

        public static string md5(string @string, bool binary = false)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.Default.GetBytes(@string));
                return binary ? Encoding.Default.GetString(hash) : Convert.ToHexString(hash);
            }
        } // Calculate the md5 hash of a string

        /// <summary>
        /// Calculate the metaphone key of a string
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <param name="max_phonemes">This parameter restricts the returned metaphone key to max_phonemes characters in length. However, the resulting phonemes are always transcribed completely, so the resulting string length may be slightly longer than max_phonemes. The default value of 0 means no restriction.</param>
        /// <returns>Returns the metaphone key as a string.</returns>
        public static string metaphone(string @string, int max_phonemes = 0)
        {
            return @string; // TODO
        }

        /// <summary>
        /// Formats a number as a currency string
        /// </summary>
        /// <param name="format">The format specification consists of the following sequence:</param>
        /// <param name="number">The number to be formatted.</param>
        /// <returns>Returns the formatted string. Characters before and after the formatting string will be returned unchanged. Non-numeric number causes returning null and emitting E_WARNING.</returns>
        [Obsolete("Warning: This function has been DEPRECATED as of PHP 7.4.0, and REMOVED as of PHP 8.0.0. Relying on this function is highly discouraged.")]
        public static string money_format(string format, float number)
        {
            if (number == float.NaN)
            {
                return null; // TODO
            }

            try
            {
                return number.ToString(format, CultureInfo.CurrentCulture);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Query language and locale information
        /// </summary>
        /// <param name="item">item may be an integer value of the element or the constant name of the element. The following is a list of constant names for item that may be used and their description. Some of these constants may not be defined or hold no value for certain locales.</param>
        /// <returns>Returns the element as a string, or false if item is not valid.</returns>
        public static (string, bool) nl_langinfo(int item)
        {
            return (null, false); // TODO
        }

        /// <summary>
        /// Inserts HTML line breaks before all newlines in a string
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <param name="use_xhtml">Whether to use XHTML compatible line breaks or not.</param>
        /// <returns>Returns the altered string.</returns>
        public static string nl2br(string @string, bool use_xhtml = true)
        {
            var br = use_xhtml ? "<br />" : "<br>";
            return string.IsNullOrEmpty(@string) ?
                @string :
                @string.Replace("\r\n", $"{br}\r\n").
                    Replace("\n\r", $"{br}\n\r").
                    Replace("\n", $"{br}\n").
                    Replace("\r", $"{br}\r");
        }

        /// <summary>
        /// Format a number with grouped thousands
        /// </summary>
        /// <param name="num">The number being formatted.</param>
        /// <param name="decimals">Sets the number of decimal digits. If 0, the decimal_separator is omitted from the return value.</param>
        /// <param name="decimal_separator">Sets the separator for the decimal point.</param>
        /// <param name="thousands_separator">Sets the thousands separator.</param>
        /// <returns>A formatted version of num.</returns>
        public static string number_format(float num,    int decimals = 0,    string decimal_separator = ".",    string thousands_separator = ",")
        {
            return num.ToString($"#{thousands_separator}###{decimal_separator}{(decimals > 0 ? Enumerable.Repeat("#", decimals) : string.Empty)}", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert the first byte of a string to a value between 0 and 255
        /// </summary>
        /// <param name="character">A character.</param>
        /// <returns>An integer between 0 and 255.</returns>
        public static int ord(string character)
        {
            return string.IsNullOrEmpty(character) ? 0 : character[0];
        }

        /// <summary>
        /// Parses the string into variables
        /// </summary>
        /// <param name="@string"></param>
        /// <param name="result"></param>
        [Obsolete("Warning: Using this function without the result parameter is highly DISCOURAGED and DEPRECATED as of PHP 7.2. As of PHP 8.0.0, the result parameter is mandatory.")]
        public static void parse_str(string @string, Dictionary<string, object> result) 
        {
            if (!string.IsNullOrEmpty(@string))
            {
                if (result != null)
                {
                    var parameters = @string.Split(";");
                    foreach (var p in parameters)
                    {
                        var kvp = p.Split("=");
                        result.Add(kvp[0], kvp[1]);
                    }
                }

                // TODO
            }
        }

        /// <summary>
        /// Output a string
        /// </summary>
        /// <param name="expression">The expression to be output. Non-string values will be coerced to strings, even when the strict_types directive is enabled.</param>
        /// <returns>Returns 1, always.</returns>
        public static int print(string expression)
        {
            // TODO

            return 1;
        }

        /// <summary>
        /// Output a formatted string
        /// </summary>
        /// <param name="format">The format string is composed of zero or more directives: ordinary characters (excluding %) that are copied directly to the result and conversion specifications, each of which results in fetching its own parameter.</param>
        /// <param name="values">Corresponding values.</param>
        /// <returns>Returns the length of the outputted string.</returns>
        public static int printf(string format, params object[] values)
        {
            return -1; // TODO
        }

        /// <summary>
        /// Convert a quoted-printable string to an 8 bit string
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <returns>Returns the 8-bit binary string.</returns>
        public static string quoted_printable_decode(string @string)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return null;
            }

            return null; // TODO
        }

        /// <summary>
        /// Convert a 8 bit string to a quoted-printable string
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <returns>Returns the encoded string.</returns>
        public static string quoted_printable_encode(string @string)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return null;
            }

            return null; // TODO
        }

        /// <summary>
        /// Quote meta characters
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <returns>Returns the string with meta characters quoted, or false if an empty string is given as string.</returns>
        public static (string, bool) quotemeta(string @string)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return (null, false);
            }

            return (@string.
                Replace(@".", @"\.").
                Replace(@"\", @"\\").
                Replace(@"+", @"\+").
                Replace(@"*", @"\*").
                Replace(@"?", @"\?").
                Replace(@"[", @"\[").
                Replace(@"^", @"\^").
                Replace(@"]", @"\]").
                Replace(@"(", @"\(").
                Replace(@"$", @"\$").
                Replace(@")", @"\)"), true);
        }

        /// <summary>
        /// Strip whitespace (or other characters) from the end of a string
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <param name="characters">You can also specify the characters you want to strip, by means of the characters parameter. Simply list all characters that you want to be stripped. With .. you can specify a range of characters.</param>
        /// <returns>Returns the modified string.</returns>
        public static string rtrim(string @string, string characters = " \n\r\t\v\x00")
        {
            return string.IsNullOrEmpty(@string) ?
                @string :
                @string.TrimEnd(characters.ToCharArray());
        }

        /// <summary>
        /// Set locale information
        /// </summary>
        /// <param name="category">is a named constant specifying the category of the functions affected by the locale setting:</param>
        /// <param name="locales">If locales is the empty string "", the locale names will be set from the values of environment variables with the same names as the above categories, or from "LANG".<br />
        /// If locales is "0", the locale setting is not affected, only the current setting is returned.<br />
        /// If locales is followed by additional parameters then each parameter is tried to be set as new locale until success.This is useful if a locale is known under different names on different systems or for providing a fallback for a possibly not available locale.</param>
        /// <param name="rest">Optional string parameters to try as locale settings until success.</param>
        /// <returns>Returns the new current locale, or false if the locale functionality is not implemented on your platform, the specified locale does not exist or the category name is invalid.</returns>
        public static (string, bool) setlocale(int category, string locales, params string[] rest) 
        {
            return (null, false); // TODO
        }

        /// <summary>
        /// Set locale information
        /// </summary>
        /// <param name="category"></param>
        /// <param name="locale_array">Each array element is tried to be set as new locale until success. This is useful if a locale is known under different names on different systems or for providing a fallback for a possibly not available locale.</param>
        /// <returns>Returns the new current locale, or false if the locale functionality is not implemented on your platform, the specified locale does not exist or the category name is invalid.</returns>
        public static (string, bool) setlocale(int category, params string[] locale_array)
        {
            return (null, false); // TODO
        }

        /// <summary>
        /// Calculate the sha1 hash of a file
        /// </summary>
        /// <param name="filename">The filename of the file to hash.</param>
        /// <param name="binary">When true, returns the digest in raw binary format with a length of 20.</param>
        /// <returns>Returns a string on success, false otherwise.</returns>
        public static (string, bool) sha1_file(string filename, bool binary = false)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return (null, false);
            }

            if (File.Exists(filename))
            {
                using (var sha1 = SHA1.Create())
                {
                    using (var stream = File.OpenRead(filename))
                    {
                        var hash = sha1.ComputeHash(stream);
                        return (binary ? Encoding.Default.GetString(hash) : Convert.ToHexString(hash), true);
                    }
                }
            }

            return (null, false);
        }

        /// <summary>
        /// Calculate the sha1 hash of a string
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <param name="binary">If the optional binary is set to true, then the sha1 digest is instead returned in raw binary format with a length of 20, otherwise the returned value is a 40-character hexadecimal number.</param>
        /// <returns>Returns the sha1 hash as a string.</returns>
        public static string sha1(string @string, bool binary = false)
        {
            using (var sha1 = SHA1.Create())
            {
                var hash = sha1.ComputeHash(Encoding.Default.GetBytes(@string));
                return binary ? Encoding.Default.GetString(hash) : Convert.ToHexString(hash);
            }
        }

        /// <summary>
        /// Calculate the similarity between two strings
        /// </summary>
        /// <param name="string1">The first string.</param>
        /// <param name="string2">The second string. </param>
        /// <param name="percent">By passing a reference as third argument, similar_text() will calculate the similarity in percent, by dividing the result of similar_text() by the average of the lengths of the given strings times 100.</param>
        /// <returns>Returns the number of matching chars in both strings.</returns>
        public static int similar_text(string string1, string string2, float? percent = null)
        {
            return string.IsNullOrEmpty(string1) ? 0 : string1.Intersect(string2).Count();
        }

        /// <summary>
        /// Calculate the soundex key of a string
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <returns>Returns the soundex key as a string with four characters. If at least one letter is contained in string, the returned string starts with a letter. Otherwise "0000" is returned.</returns>
        public static string soundex(string @string)
        {
            return @string; // TODO
        }

        /// <summary>
        /// Return a formatted string
        /// </summary>
        /// <param name="format">The format string is composed of zero or more directives: ordinary characters (excluding %) that are copied directly to the result and conversion specifications, each of which results in fetching its own parameter.</param>
        /// <param name="values">Values to format.</param>
        /// <returns>Returns a string produced according to the formatting string format.</returns>
        public static string sprintf(string format, params object[] values) 
        {
            return StringFormatter.PrintF(format, values);
        } 

        public static void sscanf() { } // Parses input from a string according to a format

        /// <summary>
        /// Determine if a string contains a given substring
        /// </summary>
        /// <param name="haystack">The string to search in.</param>
        /// <param name="needle">The substring to search for in the haystack.</param>
        /// <returns>Returns true if needle is in haystack, false otherwise.</returns>
        public static bool str_contains(string haystack, string needle) 
        {
            return string.IsNullOrEmpty(haystack) ? false :
                string.IsNullOrEmpty(needle) ? false : haystack.Contains(needle);
        }

        /// <summary>
        /// Checks if a string ends with a given substring
        /// </summary>
        /// <param name="haystack">The string to search in.</param>
        /// <param name="needle">The substring to search for at the end of the haystack.</param>
        /// <returns>Returns true if haystack ends with needle, false otherwise.</returns>
        public static bool str_ends_with(string haystack, string needle) 
        {
            return string.IsNullOrEmpty(haystack) ? false :
                string.IsNullOrEmpty(needle) ? false : haystack.EndsWith(needle);
        }

        /// <summary>
        /// Parse a CSV string into an array
        /// </summary>
        /// <param name="@string">The string to parse.</param>
        /// <param name="separator">Set the field delimiter (one single-byte character only).</param>
        /// <param name="enclosure">Set the field enclosure character (one single-byte character only).</param>
        /// <param name="escape">Set the escape character (at most one single-byte character). Defaults as a backslash (\) An empty string ("") disables the proprietary escape mechanism.</param>
        /// <returns>Returns an indexed array containing the fields read.</returns>
        public static IEnumerable<string> str_getcsv(string @string, string separator = ",", string enclosure = "\"", string escape = "\\")
        {
            if (string.IsNullOrEmpty(@string))
            {
                return null;
            }

            return @string.Split(separator, StringSplitOptions.None).Select(s => {
                var result = s.StartsWith(escape) ? s.Substring(1, s.Length) : s;
                return result.EndsWith(escape) ? result.Substring(0, result.Length - 1) : result;
            });
        }

        // Case-insensitive version of str_replace
        public static string @string_ireplace(string @string, int length, string pad_string = " ", int pad_type = (int)PaddingTypes.STR_PAD_RIGHT)
        {
            return null;
        }

        /// <summary>
        /// Pad a string to a certain length with another string
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <param name="length">If the value of length is negative, less than, or equal to the length of the input string, no padding takes place, and string will be returned.</param>
        /// <param name="pad_string">Note: The pad_string may be truncated if the required number of padding characters can't be evenly divided by the pad_string's length.</param>
        /// <param name="pad_type">Optional argument pad_type can be STR_PAD_RIGHT, STR_PAD_LEFT, or STR_PAD_BOTH. If pad_type is not specified it is assumed to be STR_PAD_RIGHT.</param>
        /// <returns>Returns the padded string.</returns>
        public static string @string_pad(string @string, int length, string pad_string = " ", int pad_type = (int)PaddingTypes.STR_PAD_RIGHT)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return null;
            }

            switch ((PaddingTypes)pad_type)
            {
                case PaddingTypes.STR_PAD_LEFT:
                    return @string.PadLeft(length, pad_string[0]); // TODO
                case PaddingTypes.STR_PAD_RIGHT:
                    return @string.PadRight(length, pad_string[0]); // TODO
                case PaddingTypes.STR_PAD_BOTH:
                    return @string.PadLeft(length / 2, pad_string[0]).PadRight(length, pad_string[0]); // TODO
            }

            return null;
        } // Pad a string to a certain length with another string

        /// <summary>
        /// Repeat a string
        /// </summary>
        /// <param name="@string">The string to be repeated.</param>
        /// <param name="times">Number of time the string string should be repeated.</param>
        /// <returns></returns>
        public static string @string_repeat(string @string, int times)
        {
            if (string.IsNullOrEmpty(@string) || times <= 0)
            {
                return @string;
            }

            if (@string.Length == 1)
            {
                return new string(@string[0], times);
            }

            var sb = new StringBuilder(@string.Length * times);

            for (var i = 0; i < times; i++)
            {
                sb.Append(@string);
            }

            return sb.ToString();
        }

        public static void str_replace() { } // Replace all occurrences of the search string with the replacement string
        public static void str_rot13() { } // Perform the rot13 transform on a string
        public static void str_shuffle() { } // Randomly shuffles a string
        public static IEnumerable<string> str_split(string @string, int length = 1)
        {
            if (string.IsNullOrEmpty(@string) || length == 0)
            {
                return null;
            }

            return @string.Chunk(length).Select(x => new string(x));
        } // Convert a string to an array

        /// <summary>
        /// Checks if a string starts with a given substring
        /// </summary>
        /// <param name="haystack">The string to search in.</param>
        /// <param name="needle">The substring to search for in the haystack.</param>
        /// <returns>Returns true if haystack begins with needle, false otherwise.</returns>
        public static bool str_starts_with(string haystack, string needle)
        {
            return string.IsNullOrEmpty(haystack) ? false :
                string.IsNullOrEmpty(needle) ? false : haystack.StartsWith(needle);
        }

        /// <summary>
        /// Return information about words used in a string
        /// </summary>
        /// <param name="@string">The string.</param>
        /// <param name="format">Specify the return value of this function. The current supported values are:</param>
        /// <param name="characters">A list of additional characters which will be considered as 'word'.</param>
        /// <returns>Returns an array or an integer, depending on the format chosen.</returns>
        public static (int, Dictionary<int, string>) str_word_count(string @string, int format = 0, string characters = null) 
        {
            if (string.IsNullOrEmpty(@string))
            {
                return (0, null);
            }

            switch (format)
            {
                case 0:
                    return (@string.Split(new char[] { ' ', '\t', '\r', '\n' }).GroupBy(w => w).Count(), null);
                case 1:
                case 2:
                    var i = 0;
                    return (0, @string.Split(new char[] { ' ', '\t', '\r', '\n' }).ToDictionary(w => i++, w => w));
            }

            return (0, null);
        }

        /// <summary>
        /// Binary safe case-insensitive string comparison
        /// </summary>
        /// <param name="string1">The first string.</param>
        /// <param name="string2">The second string.</param>
        /// <returns>Returns < 0 if string1 is less than string2; > 0 if string1 is greater than string2, and 0 if they are equal.</returns>
        public static int strcasecmp(string string1, string string2) 
        {
            if (string.IsNullOrEmpty(string1))
            {
                return -1;
            }

            return string1.ToUpperInvariant().CompareTo(string2.ToUpperInvariant());
        }

        /// <summary>
        /// Alias of strstr
        /// </summary>
        /// <param name="haystack">The input string.</param>
        /// <param name="needle">Prior to PHP 8.0.0, if needle is not a string, it is converted to an integer and applied as the ordinal value of a character. This behavior is deprecated as of PHP 7.3.0, and relying on it is highly discouraged. Depending on the intended behavior, the needle should either be explicitly cast to string, or an explicit call to chr() should be performed.</param>
        /// <param name="before_needle">If true, strstr() returns the part of the haystack before the first occurrence of the needle (excluding the needle). </param>
        /// <returns>Returns the portion of string, or false if needle is not found.</returns>
        public static (string, bool) strchr(string haystack, string needle, bool before_needle = false) => strstr(haystack, needle, before_needle);

        /// <summary>
        /// Binary safe string comparison
        /// </summary>
        /// <param name="string1">The first string.</param>
        /// <param name="string2">The second string.</param>
        /// <returns>Returns < 0 if string1 is less than string2; > 0 if string1 is greater than string2, and 0 if they are equal.</returns>
        public static int strcmp(string string1, string string2)
        {
            if (string.IsNullOrEmpty(string1))
            {
                return -1;
            }

            return string1.CompareTo(string2);
        }

        /// <summary>
        /// Locale based string comparison
        /// </summary>
        /// <param name="string1">The first string.</param>
        /// <param name="string2">The second string.</param>
        /// <returns>Returns < 0 if string1 is less than string2; > 0 if string1 is greater than string2, and 0 if they are equal.</returns>
        public static int strcoll(string string1, string string2) 
        {
            if (string.IsNullOrEmpty(string1))
            {
                return -1;
            }

            return string1.CompareTo(string2);
        }

        /// <summary>
        /// Find length of initial segment not matching mask
        /// </summary>
        /// <param name="@string">The string to examine.</param>
        /// <param name="characters">The string containing every disallowed character.</param>
        /// <param name="offset">The position in string to start searching.</param>
        /// <param name="length">The length of the segment from string to examine.</param>
        /// <returns>Returns the length of the initial segment of string which consists entirely of characters not in characters.</returns>
        public static int strcspn(string @string, string characters, int offset = 0, int? length = null)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return 0;
            }

            if (string.IsNullOrEmpty(characters))
            {
                return @string.Length;
            }

            if (offset == 0)
            {
                return @string.IndexOfAny(characters.ToCharArray());
            }

            return @string.Substring(offset, length ?? @string.Length).
                IndexOfAny(characters.ToCharArray());
        }

        /// <summary>
        /// Strip HTML and PHP tags from a string
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <param name="allowed_tags">You can use the optional second parameter to specify tags which should not be stripped. These are either given as string, or as of PHP 7.4.0, as array. Refer to the example below regarding the format of this parameter.</param>
        /// <returns>Returns the stripped string.</returns>
        public static string @stringip_tags(string @string, string allowed_tags = null)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return @string;
            }

            if (string.IsNullOrEmpty(allowed_tags))
            {
                var doc = new HtmlDocument();
                doc.Load(@string);
                doc.DocumentNode.SelectNodes("//comment()")?.ToList().ForEach(n => n.Remove());
                return WebUtility.HtmlDecode(doc.DocumentNode.InnerText);
            }

            var san = HtmlSanitizer.SimpleHtml5DocumentSanitizer();
            foreach (var t in allowed_tags.Split(" "))
            {
                san.Tag(t);
            }
            return san.Sanitize(@string); // TODO: works on fragments only, removes tags with their contents on documents
        }

        /// <summary>
        /// Strip HTML and PHP tags from a string
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <param name="allowed_tags">You can use the optional second parameter to specify tags which should not be stripped. These are either given as string, or as of PHP 7.4.0, as array. Refer to the example below regarding the format of this parameter.</param>
        /// <returns>Returns the stripped string.</returns>
        public static string @stringip_tags(string @string, IEnumerable<string> allowed_tags)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return @string;
            }

            if (allowed_tags == null)
            {
                var doc = new HtmlDocument();
                doc.Load(@string);
                doc.DocumentNode.SelectNodes("//comment()")?.ToList().ForEach(n => n.Remove());
                return WebUtility.HtmlDecode(doc.DocumentNode.InnerText);
            }

            var san = HtmlSanitizer.SimpleHtml5DocumentSanitizer();
            foreach (var t in allowed_tags)
            {
                san.Tag(t);
            }
            return san.Sanitize(@string); // TODO: works on fragments only, removes tags with their contents on documents
        }

        public static void stripcslashes() { } // Un-quote string quoted with addcslashes

        /// <summary>
        /// Find the position of the first occurrence of a case-insensitive substring in a string
        /// </summary>
        /// <param name="haystack">The string to search in.</param>
        /// <param name="needle">Note that the needle may be a string of one or more characters.</param>
        /// <param name="offset">If specified, search will start this number of characters counted from the beginning of the string. If the offset is negative, the search will start this number of characters counted from the end of the string.</param>
        /// <returns> Returns the position of where the needle exists relative to the beginning of the haystack string (independent of offset). Also note that string positions start at 0, and not 1.<br />
        /// Returns false if the needle was not found.</returns>
        public static (int, bool) stripos(string haystack, string needle, int offset = 0)
        {
            if (string.IsNullOrEmpty(haystack) || string.IsNullOrEmpty(needle))
            {
                return (0, false);
            }

            return (haystack.ToUpperInvariant().IndexOf(needle.ToUpperInvariant()), true);
        }

        public static void stripslashes() { } // Un-quotes a quoted string

        /// <summary>
        /// Case-insensitive strstr
        /// </summary>
        /// <param name="haystack"></param>
        /// <param name="needle"></param>
        /// <param name="before_needle"></param>
        /// <returns></returns>
        public static (string, bool) stristr(string haystack, string needle, bool before_needle = false) 
        {
            if (string.IsNullOrEmpty(haystack) || string.IsNullOrEmpty(needle))
            {
                return (null, false);
            }

            var pos = haystack.ToUpperInvariant().IndexOf(needle.ToUpperInvariant());
            return (before_needle ? haystack.Substring(0, pos - 1) : haystack.Substring(pos, haystack.Length), true);
        }

        /// <summary>
        /// Get string length
        /// </summary>
        /// <param name="@string">The string being measured for length.</param>
        /// <returns>The length of the string on success, and 0 if the string is empty.</returns>
        public static int strlen(string @string)
        {
            return string.IsNullOrEmpty(@string) ? 0 : @string.Length;
        }

        /// <summary>
        /// Case insensitive string comparisons using a "natural order" algorithm
        /// </summary>
        /// <param name="string1">The first string.</param>
        /// <param name="string2">The second string.</param>
        /// <returns>Similar to other string comparison functions, this one returns < 0 if string1 is less than string2 > 0 if string1 is greater than string2, and 0 if they are equal.</returns>
        public static int strnatcasecmp(string string1, string string2) 
        {
            if (string.IsNullOrEmpty(string1))
            {
                return string.IsNullOrEmpty(string2) ? 0 : -1;
            }

            return string1.ToUpperInvariant().CompareTo(string2.ToUpperInvariant());
        }

        /// <summary>
        /// String comparisons using a "natural order" algorithm
        /// </summary>
        /// <param name="string1">The first string.</param>
        /// <param name="string2">The second string.</param>
        /// <returns>Similar to other string comparison functions, this one returns < 0 if string1 is less than string2 > 0 if string1 is greater than string2, and 0 if they are equal.</returns>
        public static int strnatcmp(string string1, string string2)
        {
            if (string.IsNullOrEmpty(string1))
            {
                return string.IsNullOrEmpty(string2) ? 0 : -1;
            }

            return string1.CompareTo(string2);
        }

        /// <summary>
        /// Binary safe case-insensitive string comparison of the first n characters
        /// </summary>
        /// <param name="string1">The first string.</param>
        /// <param name="string2">The second string.</param>
        /// <param name="length">The length of strings to be used in the comparison.</param>
        /// <returns>Returns < 0 if string1 is less than string2; > 0 if string1 is greater than string2, and 0 if they are equal.</returns>
        public static int strncasecmp(string string1, string string2, int length) => strncmp(string1, string2, length);

        /// <summary>
        /// Binary safe string comparison of the first n characters
        /// </summary>
        /// <param name="string1">The first string.</param>
        /// <param name="string2">The second string.</param>
        /// <param name="length">The length of strings to be used in the comparison.</param>
        /// <returns>Returns < 0 if string1 is less than string2; > 0 if string1 is greater than string2, and 0 if they are equal.</returns>
        public static int strncmp(string string1, string string2, int length)
        {
            if (string.IsNullOrEmpty(string1))
            {
                return string.IsNullOrEmpty(string2) ? 0 : -1;
            }

            return string1.Substring(0, length).ToUpperInvariant().
                CompareTo(string2.Substring(0, length).ToUpperInvariant());
        }

        /// <summary>
        /// Search a string for any of a set of characters
        /// </summary>
        /// <param name="@string">The string where characters is looked for.</param>
        /// <param name="characters">This parameter is case sensitive.</param>
        /// <returns>Returns a string starting from the character found, or false if it is not found.</returns>
        public static (string, bool) strpbrk(string @string, string characters) 
        {
            if (string.IsNullOrEmpty(@string))
            {
                return (@string, false);
            }

            return (@string.Substring(@string.IndexOfAny(characters.ToCharArray()), @string.Length), true);
        }

        /// <summary>
        /// Find the position of the first occurrence of a substring in a string
        /// </summary>
        /// <param name="haystack">The string to search in.</param>
        /// <param name="needle">Prior to PHP 8.0.0, if needle is not a string, it is converted to an integer and applied as the ordinal value of a character. This behavior is deprecated as of PHP 7.3.0, and relying on it is highly discouraged. Depending on the intended behavior, the needle should either be explicitly cast to string, or an explicit call to chr() should be performed.</param>
        /// <param name="offset">If specified, search will start this number of characters counted from the beginning of the string. If the offset is negative, the search will start this number of characters counted from the end of the string.</param>
        /// <returns> Returns the position of where the needle exists relative to the beginning of the haystack string (independent of offset). Also note that string positions start at 0, and not 1.<br/>
        /// Returns false if the needle was not found.</returns>
        public static (int, bool) strpos(string haystack, string needle, int offset = 0)
        {
            if (string.IsNullOrEmpty(haystack) || string.IsNullOrEmpty(needle))
            {
                return (0, false);
            }

            return (haystack.Substring(offset, haystack.Length).IndexOf(needle), true);
        }

        /// <summary>
        /// Find the last occurrence of a character in a string
        /// </summary>
        /// <param name="haystack">The string to search in.</param>
        /// <param name="needle">If needle contains more than one character, only the first is used.This behavior is different from that of strstr().<br />
        /// Prior to PHP 8.0.0, if needle is not a string, it is converted to an integer and applied as the ordinal value of a character.This behavior is deprecated as of PHP 7.3.0, and relying on it is highly discouraged. Depending on the intended behavior, the needle should either be explicitly cast to string, or an explicit call to chr() should be performed.</param>
        /// <returns>This function returns the portion of string, or false if needle is not found.</returns>
        public static (string, bool) strrchr(string haystack, string needle) 
        {
            if (string.IsNullOrEmpty(haystack) || string.IsNullOrEmpty(needle))
            {
                return (haystack, false);
            }

            return (haystack.Substring(haystack.LastIndexOf(needle), haystack.Length), true);
        }

        /// <summary>
        /// Reverse a string
        /// </summary>
        /// <param name="@string">The string to be reversed.</param>
        /// <returns>Returns the reversed string.</returns>
        public static string @stringrev(string @string) 
        {
            if (string.IsNullOrEmpty(@string))
            {
                return @string;
            }

            return @string.ReverseGraphemeClusters();
        }

        /// <summary>
        /// Find the position of the last occurrence of a case-insensitive substring in a string
        /// </summary>
        /// <param name="haystack">The string to search in.</param>
        /// <param name="needle">Prior to PHP 8.0.0, if needle is not a string, it is converted to an integer and applied as the ordinal value of a character. This behavior is deprecated as of PHP 7.3.0, and relying on it is highly discouraged. Depending on the intended behavior, the needle should either be explicitly cast to string, or an explicit call to chr() should be performed.</param>
        /// <param name="offset">If zero or positive, the search is performed left to right skipping the first offset bytes of the haystack.<br />
        /// If negative, the search is performed right to left skipping the last offset bytes of the haystack and searching for the first occurrence of needle.<br/>
        /// Note: This is effectively looking for the last occurrence of needle before the last offset bytes.</param>
        /// <returns>Returns the position where the needle exists relative to the beginnning of the haystack string (independent of search direction or offset).<br/>
        /// Returns false if the needle was not found.</returns>
        public static (int, bool) strripos(string haystack, string needle, int offset = 0) 
        {
            if (string.IsNullOrEmpty(haystack) || string.IsNullOrEmpty(needle))
            {
                return (0, false);
            }

            return (haystack.Substring(offset, haystack.Length).ToUpperInvariant().LastIndexOf(needle.ToUpperInvariant()), true);
        }

        /// <summary>
        /// Find the position of the last occurrence of a substring in a string
        /// </summary>
        /// <param name="haystack">The string to search in.</param>
        /// <param name="needle">Prior to PHP 8.0.0, if needle is not a string, it is converted to an integer and applied as the ordinal value of a character. This behavior is deprecated as of PHP 7.3.0, and relying on it is highly discouraged. Depending on the intended behavior, the needle should either be explicitly cast to string, or an explicit call to chr() should be performed.</param>
        /// <param name="offset">If zero or positive, the search is performed left to right skipping the first offset bytes of the haystack.<br />
        /// If negative, the search is performed right to left skipping the last offset bytes of the haystack and searching for the first occurrence of needle.<br/>
        /// Note: This is effectively looking for the last occurrence of needle before the last offset bytes.</param>
        /// <returns>Returns the position where the needle exists relative to the beginnning of the haystack string (independent of search direction or offset).<br/>
        /// Returns false if the needle was not found.</returns>
        public static (int, bool) strrpos(string haystack, string needle, int offset = 0)
        {
            if (string.IsNullOrEmpty(haystack) || string.IsNullOrEmpty(needle))
            {
                return (0, false);
            }

            return (haystack.Substring(offset, haystack.Length).ToUpperInvariant().LastIndexOf(needle.ToUpperInvariant()), true);
        }

        /// <summary>
        /// Finds the length of the initial segment of a string consisting entirely of characters contained within a given mask
        /// </summary>
        /// <param name="@string">The string to examine.</param>
        /// <param name="characters">The list of allowable characters.</param>
        /// <param name="offset">The position in string to start searching.</param>
        /// <param name="length">The length of the segment from string to examine.</param>
        /// <returns>Returns the length of the initial segment of string which consists entirely of characters in characters.</returns>
        public static int strspn(string @string, string characters, int offset = 0, int? length = null)
        {
            return 0; // TODO
        }

        /// <summary>
        /// Translate characters or replace substrings
        /// </summary>
        /// <param name="haystack">The input string.</param>
        /// <param name="needle">Prior to PHP 8.0.0, if needle is not a string, it is converted to an integer and applied as the ordinal value of a character. This behavior is deprecated as of PHP 7.3.0, and relying on it is highly discouraged. Depending on the intended behavior, the needle should either be explicitly cast to string, or an explicit call to chr() should be performed.</param>
        /// <param name="before_needle">If true, strstr() returns the part of the haystack before the first occurrence of the needle (excluding the needle). </param>
        /// <returns>Returns the portion of string, or false if needle is not found.</returns>
        public static (string, bool) strstr(string haystack, string needle, bool before_needle = false)
        {
            if (string.IsNullOrEmpty(haystack) || string.IsNullOrEmpty(needle))
            {
                return (null, false);
            }

            var pos = haystack.IndexOf(needle);
            return (before_needle ? haystack.Substring(0, pos - 1) : haystack.Substring(pos, haystack.Length), true);
        }

        /// <summary>
        /// Tokenize string
        /// </summary>
        /// <param name="@string">The string being split up into smaller strings (tokens).</param>
        /// <param name="token">The delimiter used when splitting up string.</param>
        /// <returns>A string token, or false if no more tokens are available.</returns>
        public static (IEnumerable<string>, bool) strtok(string @string, string token)
        {
            if (string.IsNullOrEmpty(@string) || string.IsNullOrEmpty(token))
            {
                return (null, false);
            }

            return (@string.Split(token, StringSplitOptions.RemoveEmptyEntries), true);
        }

        /// <summary>
        /// Make a string lowercase
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <returns>Returns the lowercased string.</returns>
        public static string @stringtolower(string @string)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return @string;
            }

            return @string.ToLower();
        }

        /// <summary>
        /// Make a string uppercase
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <returns>Returns the lowercased string.</returns>
        public static string @stringtoupper(string @string)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return @string;
            }

            return @string.ToUpper();
        }

        /// <summary>
        /// Find the first occurrence of a string
        /// </summary>
        /// <param name="@string">The string being translated.</param>
        /// <param name="from">The string being translated to to.</param>
        /// <param name="to">The string replacing from.</param>
        /// <returns>Returns the translated string.</returns>
        public static string @stringtr(string @string, string from, string to)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return @string;
            }

            return @string.Replace(from, to);
        }

        /// <summary>
        /// Find the first occurrence of a string
        /// </summary>
        /// <param name="@string">The string being translated.</param>
        /// <param name="replace_pairs">The replace_pairs parameter may be used instead of to and from, in which case it's an array in the form array('from' => 'to', ...).</param>
        /// <returns>Returns the translated string.</returns>
        public static string @stringtr(string @string, Dictionary<string, string> replace_pairs)
        {
            if (string.IsNullOrEmpty(@string) || replace_pairs == null)
            {
                return @string;
            }

            foreach (var rp in replace_pairs)
            {
                if (string.IsNullOrEmpty(rp.Key))
                {
                    // TODO: E_WARNING
                    continue;
                }

                @string = @string.Replace(rp.Key, rp.Value);
            }

            return @string;
        }

        /// <summary>
        /// Binary safe comparison of two strings from an offset, up to length characters
        /// </summary>
        /// <param name="haystack">The main string being compared.</param>
        /// <param name="needle">The secondary string being compared.</param>
        /// <param name="offset">The start position for the comparison. If negative, it starts counting from the end of the string.</param>
        /// <param name="length">The length of the comparison. The default value is the largest of the length of the needle compared to the length of haystack minus the offset.</param>
        /// <param name="case_insensitive">If case_insensitive is true, comparison is case insensitive.</param>
        /// <returns>Returns < 0 if haystack from position offset is less than needle, > 0 if it is greater than needle, and 0 if they are equal. If offset is equal to (prior to PHP 7.2.18, 7.3.5) or greater than the length of haystack, or the length is set and is less than 0, substr_compare() prints a warning and returns false.</returns>
        public static int substr_compare(string haystack, string needle, int offset, int? length = null, bool case_insensitive = false)
        {
            if (string.IsNullOrEmpty(haystack))
            {
                return 0;
            }

            return case_insensitive ?
                haystack.Substring(offset, length ?? haystack.Length).ToUpperInvariant().CompareTo(needle.ToUpperInvariant()) :
                haystack.Substring(offset, length ?? haystack.Length).CompareTo(needle);
        }

        /// <summary>
        /// Count the number of substring occurrences
        /// </summary>
        /// <param name="haystack">The string to search in.</param>
        /// <param name="needle">The substring to search for.</param>
        /// <param name="offset">The offset where to start counting. If the offset is negative, counting starts from the end of the string.</param>
        /// <param name="length">The maximum length after the specified offset to search for the substring. It outputs a warning if the offset plus the length is greater than the haystack length. A negative length counts from the end of haystack.</param>
        /// <returns>This function returns an int.</returns>
        public static int substr_count(string haystack, string needle, int offset = 0, int? length = null)
        {
            if (string.IsNullOrEmpty(haystack))
            {
                return 0;
            }

            return haystack.Split(needle).Count() - 1;
        }

        /// <summary>
        /// Replace text within a portion of a string
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <param name="replace">The replacement string.</param>
        /// <param name="offset">If offset is non-negative, the replacing will begin at the offset'th offset into string.<br/>
        /// If offset is negative, the replacing will begin at the offset'th character from the end of string.</param>
        /// <param name="length">If given and is positive, it represents the length of the portion of string which is to be replaced. If it is negative, it represents the number of characters from the end of string at which to stop replacing. If it is not given, then it will default to strlen( string ); i.e. end the replacing at the end of string. Of course, if length is zero then this function will have the effect of inserting replace into string at the given offset offset.</param>
        /// <returns>The result string is returned. If string is an array then array is returned.</returns>
        public static string substr_replace(string @string, string replace, int offset, int? length = null)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return @string;
            }

            switch (System.Math.Sign(offset))
            {
                case -1:
                    return @string; // TODO
                case 0:
                    return @string; // TODO
                case 1:
                    return @string; // TODO
            }

            return @string; // TODO
        }

        /// <summary>
        /// Return part of a string
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <param name="offset">Offset.</param>
        /// <param name="length">How many characters.</param>
        /// <returns>Returns the extracted part of string, or an empty string.</returns>
        public static string substr(string @string, int offset, int? length = null) 
        {
            if (string.IsNullOrEmpty(@string))
            {
                return @string;
            }

            return @string.Substring(offset, length ?? @string.Length);
        }

        /// <summary>
        /// Strip whitespace (or other characters) from the beginning and end of a string
        /// </summary>
        /// <param name="@string">The string that will be trimmed.</param>
        /// <param name="characters">Optionally, the stripped characters can also be specified using the characters parameter. Simply list all characters that you want to be stripped. With .. you can specify a range of characters.</param>
        /// <returns></returns>
        public static string trim(string @string, string characters = null) 
        {
            if (string.IsNullOrEmpty(@string))
            {
                return @string;
            }

            return @string.Trim(characters.ToArray());
        }

        /// <summary>
        /// Make a string's first character uppercase
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <returns>Returns the modified string.</returns>
        public static string ucfirst(string @string)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return null;
            }

            var buf = @string.ToCharArray();
            buf[0] = char.ToUpper(buf[0]);
            return new string(buf);
        }

        /// <summary>
        /// Uppercase the first character of each word in a string
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <param name="separators">The optional separators contains the word separator characters.</param>
        /// <returns>Returns the modified string.</returns>
        public static string ucwords(string @string, string separators = " \t\r\n\f\v")
        {
            if (string.IsNullOrEmpty(@string))
            {
                return @string;
            }

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(@string);
        }

        /// <summary>
        /// Converts a string from UTF-8 to ISO-8859-1, replacing invalid or unrepresentable characters
        /// </summary>
        /// <param name="@string">A UTF-8 encoded string.</param>
        /// <returns>Returns the ISO-8859-1 translation of string.</returns>
        public static string utf8_decode(string @string)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return @string;
            }

            byte[] utf8Bytes = Encoding.UTF8.GetBytes(@string);
            byte[] ISO88591 = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("ISO-8859-1"), utf8Bytes);
            return Encoding.UTF8.GetString(ISO88591);
        }

        /// <summary>
        /// Converts a string from ISO-8859-1 to UTF-8
        /// </summary>
        /// <param name="@string">An ISO-8859-1 string.</param>
        /// <returns>Returns the UTF-8 translation of string.</returns>
        public static string utf8_encode(string @string)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return @string;
            }

            byte[] iso88591Bytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(@string);
            byte[] utf8bytes = Encoding.Convert(Encoding.GetEncoding("ISO-8859-1"), Encoding.UTF8, iso88591Bytes);
            return Encoding.UTF8.GetString(utf8bytes);
        } 
        public static void vfprintf() { } // Write a formatted string to a stream
        public static void vprintf() { } // Output a formatted string
        public static void vsprintf() { } // Return a formatted string

        /// <summary>
        /// Wraps a string to a given number of characters
        /// </summary>
        /// <param name="@string">The input string.</param>
        /// <param name="width">The number of characters at which the string will be wrapped.</param>
        /// <param name="Break">The line is broken using the optional break parameter.</param>
        /// <param name="cut_long_words">If the cut_long_words is set to true, the string is always wrapped at or before the specified width. So if you have a word that is larger than the given width, it is broken apart. (See second example). When false the function does not split the word even if the width is smaller than the word width.</param>
        /// <returns>Returns the given string wrapped at the specified length.</returns>
        public static string wordwrap(string @string, int width = 75, string Break = "\n", bool cut_long_words = false)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return @string;
            }

            if (cut_long_words)
            {
                return string.Join(Break, @string.Chunk(width));
            }

            return @string; // TODO
        }
    }
}
