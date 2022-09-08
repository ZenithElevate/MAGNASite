/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */

namespace MAGNASite.Lib
{
    internal enum JsonErrors
    {
        JSON_ERROR_NONE = 0,                   // No error has occurred.																																																																	
        JSON_ERROR_DEPTH = 1,                  // The maximum stack depth has been exceeded.																																																												
        JSON_ERROR_STATE_MISMATCH = 2,         // Occurs with underflow or with the modes mismatch.																																																											
        JSON_ERROR_CTRL_CHAR = 3,              // Control character error, possibly incorrectly encoded.																																																									
        JSON_ERROR_SYNTAX = 4,                 // Syntax error.																																																																				
        JSON_ERROR_UTF8 = 5,                   // Malformed UTF-8 characters, possibly incorrectly encoded.																																																									
        JSON_ERROR_RECURSION = 6,              // The object or array passed to json_encode() include recursive references and cannot be encoded. If the JSON_PARTIAL_OUTPUT_ON_ERROR option was given, null will be encoded in the place of the recursive reference.																		
        JSON_ERROR_INF_OR_NAN = 7,             // The value passed to json_encode() includes either NAN or INF. If the JSON_PARTIAL_OUTPUT_ON_ERROR option was given, 0 will be encoded in the place of these special numbers.																												
        JSON_ERROR_UNSUPPORTED_TYPE = 8,       // A value of an unsupported type was given to json_encode(), such as a resource. If the JSON_PARTIAL_OUTPUT_ON_ERROR option was given, null will be encoded in the place of the unsupported value.																							
        JSON_ERROR_INVALID_PROPERTY_NAME = 9,  // A key starting with \u0000 character was in the string passed to json_decode() when decoding a JSON object into a PHP object.																																								
        JSON_ERROR_UTF16 = 10,                 // Single unpaired UTF-16 surrogate in unicode escape contained in the JSON string passed to json_decode().				// The following constants can be combined to form options for json_decode().																						
    }
}