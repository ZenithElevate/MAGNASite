/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using Newtonsoft.Json;

namespace MAGNASite.Lib
{

    internal interface JsonSerializable
    {
        /* Methods */
        public object jsonSerialize();
    }

    internal class JsonException : Exception
    {
        /* Inherited properties */
        protected string message = "";
        private string @string = "";
        protected int code;
        protected string file = "";
        protected int line;
        private object[] trace;
        private Exception previous = null;
        /* Inherited methods */
        public string getMessage() => Message;
        public Exception getPrevious() => previous;
        public int getCode() => code;
        public string getFile() => file;
        public int getLine() => line;
        public object[] getTrace() => new object[] { StackTrace };
        public string getTraceAsString()=> StackTrace;
        public string __toString() => ToString();
        private void __clone() => MemberwiseClone();
    }

    /// <summary>
    /// JSON functions
    /// </summary>
    internal class PHPJson : ErrorAwareBaseClass
    {
        public const int JSON_BIGINT_AS_STRING = 2;             // Decodes large integers as their original string value.																																																									
        public const int JSON_OBJECT_AS_ARRAY = 1;              // Decodes JSON objects as PHP array. This option can be added automatically by calling json_decode() with the second parameter equal to true.							//The following constants can be combined to form options for json_encode().										
        public const int JSON_HEX_TAG = 1;                      // All < and > are converted to \u003C and \u003E.																																																											
        public const int JSON_HEX_AMP = 2;                      // All & are converted to \u0026.																																																															
        public const int JSON_HEX_APOS = 4;                     // All ' are converted to \u0027.																																																															
        public const int JSON_HEX_QUOT = 8;                     // All " are converted to \u0022.																																																															
        public const int JSON_FORCE_OBJECT = 16;                // Outputs an object rather than an array when a non-associative array is used. Especially useful when the recipient of the output is expecting an object and the array is empty.																											
        public const int JSON_NUMERIC_CHECK = 32;               // Encodes numeric strings as numbers.																																																														
        public const int JSON_PRETTY_PRINT = 128;               // Use whitespace in returned data to format it.																																																												
        public const int JSON_UNESCAPED_SLASHES = 64;           // Don't escape /.																																																																			
        public const int JSON_UNESCAPED_UNICODE = 256;          // Encode multibyte Unicode characters literally (default is to escape as \uXXXX).																																																			
        public const int JSON_PARTIAL_OUTPUT_ON_ERROR = 512;    // Substitute some unencodable values instead of failing.																																																									
        public const int JSON_PRESERVE_ZERO_FRACTION = 1024;    // Ensures that float values are always encoded as a float value.																																																							

        /// <summary>
        /// Decodes a JSON string
        /// </summary>
        /// <param name="json">The json string being decoded.</param>
        /// <param name="associative">When true, JSON objects will be returned as associative arrays; when false, JSON objects will be returned as objects. When null, JSON objects will be returned as associative arrays or objects depending on whether JSON_OBJECT_AS_ARRAY is set in the flags.</param>
        /// <param name="depth">Maximum nesting depth of the structure being decoded. The value must be greater than 0, and less than or equal to 2147483647.</param>
        /// <param name="flags">Bitmask of JSON_BIGINT_AS_STRING, JSON_INVALID_UTF8_IGNORE, JSON_INVALID_UTF8_SUBSTITUTE, JSON_OBJECT_AS_ARRAY, JSON_THROW_ON_ERROR. The behaviour of these constants is described on the JSON constants page.</param>
        /// <returns>Returns the value encoded in json in appropriate PHP type. Values true, false and null are returned as true, false and null respectively. null is returned if the json cannot be decoded or if the encoded data is deeper than the nesting limit.</returns>
        public object json_decode(string json, bool? associative = null, int depth = 512, int flags = 0)
        {
            var result = JsonConvert.DeserializeObject(json); // TODO
            var i = 0;
            return associative ?? false ?
                result is IEnumerable<object> ? (result as IEnumerable<object>).ToDictionary(x => i++.ToString(), x => x) : result :
                result;
        }

        /// <summary>
        /// Returns the JSON representation of a value
        /// </summary>
        /// <param name="value">The value being encoded. Can be any type except a resource.</param>
        /// <param name="flags">Bitmask consisting of JSON_FORCE_OBJECT, JSON_HEX_QUOT, JSON_HEX_TAG, JSON_HEX_AMP, JSON_HEX_APOS, JSON_INVALID_UTF8_IGNORE, JSON_INVALID_UTF8_SUBSTITUTE, JSON_NUMERIC_CHECK, JSON_PARTIAL_OUTPUT_ON_ERROR, JSON_PRESERVE_ZERO_FRACTION, JSON_PRETTY_PRINT, JSON_UNESCAPED_LINE_TERMINATORS, JSON_UNESCAPED_SLASHES, JSON_UNESCAPED_UNICODE, JSON_THROW_ON_ERROR. The behaviour of these constants is described on the JSON constants page.</param>
        /// <param name="depth">Set the maximum depth. Must be greater than zero.</param>
        /// <returns>Returns a JSON encoded string on success or false on failure.</returns>
        public (string, bool) json_encode(object value, int flags = 0, int depth = 512)
        {
            try
            {
                return (JsonConvert.SerializeObject(value), true);
            }
            catch (Exception)
            {
                return (null, false);
            }
        }

        /// <summary>
        /// Returns the error string of the last json_encode() or json_decode() call
        /// </summary>
        /// <returns>Returns the error message on success, or "No error" if no error has occurred.</returns>
        public string json_last_error_msg()
        {
            return Errors.LastOrDefault() ?? "No error";
        }

        /// <summary>
        /// Returns the last error occurred
        /// </summary>
        /// <returns>Returns an integer, the value can be one of the following constants:</returns>
        public int json_last_error()
        {
            return (int)JsonErrors.JSON_ERROR_NONE; // TODO
        }
    }
}