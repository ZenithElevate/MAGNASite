/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using System.Text;

namespace MAGNASite.Lib
{
    /// <summary>
    /// URLs
    /// </summary>
    internal class PHPUrls
    {
        public const int PHP_URL_SCHEME = 0; //                                                                                                                                                    
        public const int PHP_URL_HOST = 1; // Outputs the hostname of the URL parsed.                                                                                                            
        public const int PHP_URL_PORT = 2; // Outputs the port of the URL parsed.                                                                                                                
        public const int PHP_URL_USER = 3; // Outputs the user of the URL parsed.                                                                                                                
        public const int PHP_URL_PASS = 4; // Outputs the password of the URL parsed.                                                                                                            
        public const int PHP_URL_PATH = 5; // Outputs the path of the URL parsed.                                                                                                                
        public const int PHP_URL_QUERY = 6; // Outputs the query string of the URL parsed.                                                                                                        
        public const int PHP_URL_FRAGMENT = 7; // Outputs the fragment (string after the hashmark #) of the URL parsed. //The following constants are meant to be used with http_build_query().      
        public const int PHP_QUERY_RFC1738 = 1; // Encoding is performed per » RFC 1738 and the application/x-www-form-urlencoded media type, which implies that spaces are encoded as plus (+) signs.
        public const int PHP_QUERY_RFC3986 = 2; // Encoding is performed according to » RFC 3986, and spaces will be percent encoded (%20).

        /// <summary>
        /// Decodes data encoded with MIME base64
        /// </summary>
        /// <param name="string">The encoded data.</param>
        /// <param name="strict">If the strict parameter is set to true then the base64_decode() function will return false if the input contains character from outside the base64 alphabet. Otherwise invalid characters will be silently discarded.</param>
        /// <returns>Returns the decoded data or false on failure. The returned data may be binary.</returns>
        public static (string, bool) base64_decode(string @string, bool strict = false)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return (@string, false);
            }

            return (Encoding.Default.GetString(Convert.FromBase64String(@string)), true);
        }

        /// <summary>
        /// Encodes data with MIME base64
        /// </summary>
        /// <param name="string">The data to encode.</param>
        /// <returns>The encoded data, as a string.</returns>
        public static string base64_encode(string @string)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return @string;
            }

            return Convert.ToBase64String(Encoding.Default.GetBytes(@string));
        }

        public static void get_headers() { } // Fetches all the headers sent by the server in response to an HTTP request
        public static void get_meta_tags() { } // Extracts all meta tag content attributes from a file and returns an array
        public static void http_build_query() { } // Generate URL-encoded query string
        public static void parse_url() { } // Parse a URL and return its components
        public static void rawurldecode() { } // Decode URL-encoded strings
        public static void rawurlencode() { } // URL-encode according to RFC 3986
        public static void urldecode() { } // Decodes URL-encoded string
        public static void urlencode() { } // URL-encodes string
    }
}